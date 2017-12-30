Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing.Layout
Imports PdfSharp
Imports System.Data.OleDb
Imports System.IO

Module modPDF
    Public font8 As XFont = New XFont("Verdana", 8, XFontStyle.Regular)
    Public font10 As XFont = New XFont("Verdana", 10, XFontStyle.Regular)
    Public font12 As XFont = New XFont("Verdana", 12, XFontStyle.Regular)
    Public font14 As XFont = New XFont("Verdana", 14, XFontStyle.Regular)
    Public font16 As XFont = New XFont("Verdana", 16, XFontStyle.Regular)
    Public font18 As XFont = New XFont("Verdana", 18, XFontStyle.Regular)
    Public font20 As XFont = New XFont("Verdana", 20, XFontStyle.Regular)

    Public font8_bold As XFont = New XFont("Verdana", 8, XFontStyle.Bold)
    Public font10_bold As XFont = New XFont("Verdana", 10, XFontStyle.Bold)
    Public font12_bold As XFont = New XFont("Verdana", 12, XFontStyle.Bold)
    Public font14_bold As XFont = New XFont("Verdana", 14, XFontStyle.Bold)
    Public font16_bold As XFont = New XFont("Verdana", 16, XFontStyle.Bold)
    Public font18_bold As XFont = New XFont("Verdana", 18, XFontStyle.Bold)
    Public font20_bold As XFont = New XFont("Verdana", 20, XFontStyle.Bold)

    Public black_brush = XBrushes.Black


    Public black_penHalf As New Pen(Brushes.Black, 0.5)
    Public black_pen1 As New Pen(Brushes.Black, 1)
    Public black_pen2 As New Pen(Brushes.Black, 2)


    Public Sub CoverPage(ByVal document As PdfSharp.Pdf.PdfDocument, _
                         ByVal vstrTitle As String, _
                         ByVal vstrSubTitlePage As String)
        Dim rectBorderOuter As XRect
        Dim rectBorderInner As XRect

        Try
            ' Create an empty page
            Dim page As PdfPage = document.AddPage
            page.Orientation = PageOrientation.Portrait

            ' Get an XGraphics object for drawing
            Dim gfxCoverPage As XGraphics = XGraphics.FromPdfPage(page)

            rectBorderOuter = New XRect(30, 30, 550, 730)
            rectBorderInner = New XRect(35, 35, 540, 720)

            gfxCoverPage.DrawRectangle(black_pen2, rectBorderOuter)
            gfxCoverPage.DrawRectangle(black_penHalf, rectBorderInner)

            Dim image As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(IMAGE_DIRECTORY & "DNAPic2-Large.bmp")
            image.Interpolate = False
            gfxCoverPage.DrawImage(image, New Rectangle(50, 50, 60, 60)) 'Moves the bmp on the page itself


            gfxCoverPage.DrawString(vstrTitle, font20_bold, black_brush, New XRect(0, 200, page.Width, page.Height), XStringFormats.TopCenter)
            gfxCoverPage.DrawString(vstrSubTitlePage, font14_bold, black_brush, New XRect(0, 225, page.Width, page.Height), XStringFormats.TopCenter)
            gfxCoverPage.DrawString(Now.Date.ToShortDateString, font8, black_brush, New XRect(0, 245, page.Width, page.Height), XStringFormats.TopCenter)

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Public Sub PageBorder(ByVal page As PdfSharp.Pdf.PdfPage, _
                         ByVal vstrOrientation As String, _
                         gfx As XGraphics)
        Dim rectBorderOuter As XRect
        Dim rectBorderInner As XRect

        Try
            ' Get an XGraphics object for drawing
            ' Dim gfx As XGraphics = XGraphics.FromPdfPage(page)

            Select Case vstrOrientation
                Case "Landscape"
                    rectBorderOuter = New XRect(30, 30, 730, 550)
                    rectBorderInner = New XRect(35, 35, 720, 540)

                    gfx.DrawRectangle(black_pen2, rectBorderOuter)
                    gfx.DrawRectangle(black_penHalf, rectBorderInner)
                Case "Portrait"
                    rectBorderOuter = New XRect(30, 30, 550, 730)
                    rectBorderInner = New XRect(35, 35, 540, 720)
                    gfx.DrawRectangle(black_pen2, rectBorderOuter)
                    gfx.DrawRectangle(black_penHalf, rectBorderInner)
            End Select

            Dim image As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(IMAGE_DIRECTORY & "DNAPic2-Large.bmp")
            image.Interpolate = False
            gfx.DrawImage(image, New Rectangle(50, 50, 60, 60)) 'Moves the bmp on the page itself

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub



    Public Sub CreatePDF(ByVal vstrSNP As String, ByVal vstrFullPath As String, ByVal vstrSaveDirectory As String)

        '   Dim dsNotes As DataSet

        Try
            '*********************************************************************************
            '   THIS SUB AND modPDF AND report mnuMainReportsMissingSNPs_Click 
            '*********************************************************************************

            Dim document As PdfDocument = New PdfDocument
            '  Dim rectCover As XRect
            '  Dim rectCoverInner As XRect
            document.Info.Title = "Created with PDFsharp"


            ' Dim mybrush As New XLinearGradientBrush(New Point(0, 10), New Point(200, 10), Color.FromArgb(255, 255, 0, 0),  Color.FromArgb(255, 0, 0, 255))   'this
            Dim font As XFont = New XFont("Times New Roman", 9, XFontStyle.Bold)

            'Draw crossing lines
            Dim pen As XPen = New XPen(XColor.FromArgb(255, 0, 0))


            Call modPDF.CoverPage(document, "SNP Fact Sheet", vstrSNP)




            ' Create an empty page
            Dim page As PdfPage = document.AddPage
            page.Orientation = PageOrientation.Landscape


            ' Get an XGraphics object for drawing
            Dim gfx As XGraphics = XGraphics.FromPdfPage(page)

            'Get the border
            Call PageBorder(page, "Landscape", gfx)


            gfx.DrawString("Formation", font20_bold, black_brush, New XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter)
            Dim tf As XTextFormatter = New XTextFormatter(gfx)

            Dim intWidth As Integer = 60
            Dim MyText As String = ""

            Dim xPos As Integer = 40
            Dim xInterval As Integer = 20
            Dim xcoord As Integer = 200

            Dim strFormed As String = ""
            Dim strTMRCA As String = ""




            'Dim s As String = vstrFullPath

            '' Split string based on spaces.
            'Dim words As String() = s.Split(New Char() {" "c})
            'For i = 0 To words.Count - 1
            '    MyText = words(i)


            '    'Find the formation information
            '    ds = cDataAccess.GetBranchByBranchName(MyText)
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        If ds.Tables(0).Rows(0).IsNull("YBPFormed") = False Then
            '            strFormed = ds.Tables(0).Rows(0).Item("YBPFormed")
            '        Else
            '            strFormed = ""
            '        End If
            '        If ds.Tables(0).Rows(0).IsNull("YBPTMRCA") = False Then
            '            strTMRCA = ds.Tables(0).Rows(0).Item("YBPTMRCA")
            '        Else
            '            strTMRCA = ""
            '        End If
            '    Else

            '    End If


            '    If i < 9 Then  'We can only have 9 SNPs on a line

            '        If i = 0 Then   'Remove the Root

            '        ElseIf i = 1 Then
            '            xcoord = 90
            '            rect = New XRect(xcoord, 150, intWidth, 15)
            '            gfx.DrawRectangle(XBrushes.Green, rect)
            '            gfx.DrawString(">", font8_bold, XBrushes.Black, xcoord + 65, 160)


            '            'The Formed box
            '            Dim rectFormed As XRect
            '            rectFormed = New XRect(xcoord, 165, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectFormed)
            '            tf.DrawString(strFormed, font, XBrushes.Black, rectFormed, XStringFormats.TopLeft)

            '            Dim rectTMRCA As XRect
            '            rectTMRCA = New XRect(xcoord, 175, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectTMRCA)
            '            tf.DrawString(strTMRCA, font, XBrushes.Black, rectTMRCA, XStringFormats.TopLeft)

            '        ElseIf i = words.Count - 1 Then
            '            rect = New XRect(xcoord, 150, intWidth, 15)
            '            gfx.DrawRectangle(XBrushes.Red, rect)

            '            'The Formed box
            '            Dim rectFormed As XRect
            '            rectFormed = New XRect(xcoord, 165, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectFormed)
            '            tf.DrawString(strFormed, font, XBrushes.Black, rectFormed, XStringFormats.TopLeft)

            '            Dim rectTMRCA As XRect
            '            rectTMRCA = New XRect(xcoord, 175, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectTMRCA)
            '            tf.DrawString(strTMRCA, font, XBrushes.Black, rectTMRCA, XStringFormats.TopLeft)
            '        Else
            '            rect = New XRect(xcoord, 150, intWidth, 15)
            '            gfx.DrawRectangle(XBrushes.Green, rect)
            '            gfx.DrawString(">", font8_bold, XBrushes.Black, xcoord + 65, 160)

            '            'The Formed box
            '            Dim rectFormed As XRect
            '            rectFormed = New XRect(xcoord, 165, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectFormed)
            '            tf.DrawString(strFormed, font, XBrushes.Black, rectFormed, XStringFormats.TopLeft)

            '            Dim rectTMRCA As XRect
            '            rectTMRCA = New XRect(xcoord, 175, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectTMRCA)
            '            tf.DrawString(strTMRCA, font, XBrushes.Black, rectTMRCA, XStringFormats.TopLeft)
            '        End If
            '        '   xcoord = xcoord + intWidth + xInterval
            '        tf.DrawString("   " & MyText, font, XBrushes.White, rect, XStringFormats.TopLeft)
            '    Else     'THIS IS FOR THE SECOND LINE
            '        If i = 6 Then
            '            xcoord = 90
            '            rect = New XRect(xcoord, 200, intWidth, 15)
            '            gfx.DrawRectangle(XBrushes.Green, rect)
            '            gfx.DrawString(">", font8_bold, XBrushes.Black, xcoord + 65, 210)

            '            'The Formed box
            '            Dim rectFormed As XRect
            '            rectFormed = New XRect(xcoord, 215, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectFormed)
            '            tf.DrawString(strFormed, font, XBrushes.Black, rectFormed, XStringFormats.TopLeft)

            '            Dim rectTMRCA As XRect
            '            rectTMRCA = New XRect(xcoord, 225, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectTMRCA)
            '            tf.DrawString(strTMRCA, font, XBrushes.Black, rectTMRCA, XStringFormats.TopLeft)

            '        ElseIf i = words.Count - 1 Then
            '            rect = New XRect(xcoord, 200, intWidth, 15)
            '            gfx.DrawRectangle(XBrushes.Red, rect)


            '            'The Formed box
            '            Dim rectFormed As XRect
            '            rectFormed = New XRect(xcoord, 215, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectFormed)
            '            tf.DrawString(strFormed, font, XBrushes.Black, rectFormed, XStringFormats.TopLeft)

            '            Dim rectTMRCA As XRect
            '            rectTMRCA = New XRect(xcoord, 225, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectTMRCA)
            '            tf.DrawString(strTMRCA, font, XBrushes.Black, rectTMRCA, XStringFormats.TopLeft)
            '        Else
            '            rect = New XRect(xcoord, 200, intWidth, 15)
            '            gfx.DrawRectangle(XBrushes.Green, rect)
            '            gfx.DrawString(">", font8_bold, XBrushes.Black, xcoord + 65, 210)

            '            'The Formed box
            '            Dim rectFormed As XRect
            '            rectFormed = New XRect(xcoord, 215, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectFormed)
            '            tf.DrawString(strFormed, font, XBrushes.Black, rectFormed, XStringFormats.TopLeft)

            '            Dim rectTMRCA As XRect
            '            rectTMRCA = New XRect(xcoord, 225, intWidth, 10)
            '            gfx.DrawRectangle(black_penHalf, rectTMRCA)
            '            tf.DrawString(strTMRCA, font, XBrushes.Black, rectTMRCA, XStringFormats.TopLeft)
            '        End If

            '        tf.DrawString("   " & MyText, font, XBrushes.White, rect, XStringFormats.TopLeft)

            '    End If
            '    xcoord = xcoord + intWidth + xInterval

            '    strTMRCA = ""
            '    strFormed = ""

            'Next

            'gfx.DrawString("Formed", font8_bold, black_brush, 40, 172)
            'gfx.DrawString("TMRCA", font8_bold, black_brush, 40, 182)








            ''NEXT PAGE IS THE MAP

            ''   Call CreateSNPMap()
            'Application.DoEvents()

            ''Get the border
            '' Create an empty page
            'Dim pageMap As PdfPage = document.AddPage
            ''pageMap.Orientation = PageOrientation.


            '' Get an XGraphics object for drawing
            '' Dim gfx As XGraphics = XGraphics.FromPdfPage(page)

            'Call PageBorder(page, "Potrait", gfx)

            ''The SNP MAP
            ''     Call CreateMapFactSheet(Me.tvwTree.SelectedNode.Text)




            ''*********************************************************************************
            ''  NOTES SECTION
            ''*********************************************************************************
            'Dim dsNotes As DataSet
            'Dim intID As Integer = 0

            'dsNotes = cDataAccess.GetBranchByBranchName(vstrSNP)
            'If dsNotes.Tables(0).Rows.Count > 0 Then
            '    intID = dsNotes.Tables(0).Rows(0).Item("ID")

            'End If




            'Dim strAccQuery As String = "Select Notes from tblBranches where ID = " & intID
            ''  Dim cn As OleDb.OleDbConnection
            'Dim connMS As OleDbConnection
            'Try
            '    connMS = DataConn.GetConnection

            '    'Using Structure Simplifies Garbage Collection And Ensures That The Object Will Be Disposed Of Correctly Afterwards
            '    Using connMS
            '        'Create Command Object, To Make Use Of SQL Query
            '        Dim dbCommand As OleDbCommand = New OleDbCommand(strAccQuery, connMS)
            '        'Open Connection To The Database
            '        connMS.Open()


            '        'Reader Object To Traverse Through Found Records
            '        Dim oleAccReader As OleDbDataReader = dbCommand.ExecuteReader
            '        If oleAccReader.HasRows Then 'If The Reader Finds Rows
            '            While oleAccReader.Read() 'Retrieve The Content, For Each Match


            '                Dim file As Byte() = CType(oleAccReader("Notes"), Byte())
            '                Using ms As New System.IO.MemoryStream(file)
            '                    Dim sr As StreamReader = New StreamReader(ms)
            '                    Dim textSsS As String = sr.ReadToEnd()


            '                    ' Me.rtfDoc.Rtf = textSsS
            '                    ' tf.Text = textSsS
            '                    Dim pageNotes As PdfPage = document.AddPage
            '                    'pageMap.Orientation = PageOrientation.


            '                    ' Get an XGraphics object for drawing
            '                    ' Dim gfx As XGraphics = XGraphics.FromPdfPage(page)

            '                    Call PageBorder(pageNotes, "Potrait", gfx)

            '                    gfx.DrawString(textSsS, font8_bold, black_brush, 40, 182)




            '                End Using
            '            End While
            '        Else
            '            'No Match Was Found
            '            MessageBox.Show("No Rows Found.")
            '        End If
            '        'Close Reader Object
            '        oleAccReader.Close()
            '    End Using
        Catch ex As Exception
            '  Call WriteErrorLog("ERROR: frmPhoto/GetPhoto:" & ex.Message)
        End Try






        '' Save the document...
        'Dim filename As String = vstrSaveDirectory & vstrSNP & ".pdf"
        'document.Save(filename)



    End Sub

End Module
