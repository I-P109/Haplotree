Imports System.IO
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing.Layout

Public Class frmMain
    Dim cDataAccess As New clsDataAccess

    Private Sub Form_Closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub

    Private Sub mnuFileImport_Click(sender As Object, e As EventArgs) Handles mnuFileImport.Click
        Dim frmImport As New frmImport
        frmImport.MdiParent = Me
        frmImport.Show()

    End Sub

    Private Sub mnuEditMembersDetails_Click(sender As Object, e As EventArgs) Handles mnuEditMembersDetails.Click
        Dim frmMembers As New frmMembers
        frmMembers.MdiParent = Me
        frmMembers.Show()
    End Sub

    Private Sub mnuEditMembersLoadToTree_Click(sender As Object, e As EventArgs) Handles mnuEditMembersAnalyseVariants.Click
        Dim frmMembSearch As New frmMembersSearch

        frmMembSearch.btnEdit.Enabled = False
        frmMembSearch.ShowDialog()

        If frmMembSearch.ID > 0 Then AnalyseVariantData(frmMembSearch.ID)

    End Sub

    Private Sub UploadToTreeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuEditMembersUploadToTree.Click
        Dim frmMembSearch As New frmMembersSearch
        Dim formTree As New frmTree
        Dim Mb As New Member

        frmMembSearch.btnEdit.Enabled = False
        frmMembSearch.ShowDialog()

        Mb.LoadWithID(frmMembSearch.ID)
        If Mb.IsPlacedInTheTree = True Then
            MsgBox("Member " & Mb.Name & " is already placed in the tree")
        Else
            formTree.SelectOnly = True
            formTree.MdiParent = Me
            formTree.SelectedMemberID = frmMembSearch.ID
            formTree.Show()
        End If

    End Sub

    Private Sub mnuViewMembersSNPs_Click(sender As Object, e As EventArgs) Handles mnuViewMembersSNPs.Click
        Dim frmMembersSNPs As New frmMembersSNPs
        frmMembersSNPs.MdiParent = Me
        frmMembersSNPs.Show()
    End Sub

    Private Sub mnuViewTree_Click(sender As Object, e As EventArgs) Handles mnuViewTree.Click
        Dim frmTree As New frmTree
        frmTree.MdiParent = Me
        frmTree.SelectOnly = False
        frmTree.Show()
    End Sub

    Private Sub mnuReportsMembers_Click(sender As Object, e As EventArgs) Handles mnuReportsMembers.Click
        Call ReportMemberReport()
    End Sub

    Public Sub ReportMemberReport()
        Dim ds As DataSet
        Dim i As Integer = 0
        Dim intColumn1 As Integer = 100
        Dim intColumn2 As Integer = 150
        Dim intColumn3 As Integer = 250
        Dim intColumn4 As Integer = 350
        Dim intColumn5 As Integer = 450
        Dim intColumn6 As Integer = 550
        Dim intCurrentRow As Integer = 100
        Dim intSpacer As Integer = 15
        Dim strMemberName As String = ""

        Dim document As PdfDocument = New PdfDocument
        document.Info.Title = "Member Page"

        Call modPDF.CoverPage(document, "Member Page", "Members in the database")

        ' Create an empty page
        Dim pdfpage As PdfPage = document.AddPage

        ' Get an XGraphics object for drawing
        Dim graph As XGraphics = XGraphics.FromPdfPage(pdfpage)
        Dim image As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(IMAGE_DIRECTORY & "DNAPic2-Large.bmp")
        image.Interpolate = False

        'Make the page border
        Call modPDF.PageBorder(pdfpage, "Portrait", graph)

        'Get the data
        ds = cDataAccess.GetMembersAll
        If ds.Tables(0).Rows.Count > 0 Then
            '  graph.DrawString("Missing SNPs From Tree", font12_bold, brush_black, intColumn1, 480)
            graph.DrawString("Total: " & ds.Tables(0).Rows.Count, font10_bold, black_brush, intColumn5, 75)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).IsNull("MemberName") = False Then
                    strMemberName = ds.Tables(0).Rows(i).Item("MemberName")
                End If

                If i < 20 Then
                    graph.DrawString(strMemberName, font8, black_brush, intColumn1, intCurrentRow)
                End If

                If i = 20 Then
                    intCurrentRow = 100
                End If

                If i >= 20 Then
                    graph.DrawString(strMemberName, font8, black_brush, intColumn3, intCurrentRow)

                End If

                intCurrentRow = intCurrentRow + intSpacer
            Next
        End If

        'Make sure the directory exists
        Call CreateDirectory(REPORT_DIRECTORY)

        ' Save the document...
        Dim filename As String = REPORT_DIRECTORY & "MembersReport.pdf"
        document.Save(filename)

        ' ...and start a viewer.
        Process.Start(filename)
    End Sub

    Private Sub TestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestToolStripMenuItem.Click
        Dim frmTest As New frmTest
        frmTest.Show()
    End Sub

    Private Sub AllSNPsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllSNPsToolStripMenuItem.Click
        Dim frmAllMembersSNPs As New frmAllMembersSNPs
        frmAllMembersSNPs.MdiParent = Me
        frmAllMembersSNPs.Show()
    End Sub

    Private Sub mnuLoadMutationDbHG19_Click(sender As Object, e As EventArgs) Handles mnuLoadMutationDbHG19.Click
        LoadMutationsFromBigYHg19DB()
    End Sub

    Private Sub mnuLoadMemberDbHg19_Click(sender As Object, e As EventArgs) Handles mnuLoadMemberDbHg19.Click
        LoadMembersFromBigYHg19DB()
        LoadMembersDetailsFromBigYHg19DB()
    End Sub

    Private Sub mnuViewHaploTree_Click(sender As Object, e As EventArgs) Handles mnuViewHaploTree.Click
        Dim frmHaploTree As New frmHaploTree
        frmHaploTree.MdiParent = Me
        frmHaploTree.SelectionMode = False
        frmHaploTree.Show()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub mnuAddParentNodeID_Click(sender As Object, e As EventArgs) Handles mnuAddParentNodeID.Click
        AddParentNodeIDtotblMutations()
    End Sub

    Private Sub mnuRemoveAllParentNodes_Click(sender As Object, e As EventArgs) Handles mnuRemoveAllParentNodes.Click
        RemoveAllParenNodes()
    End Sub

    Private Sub mnuSetAllMutationsToPrivate_Click(sender As Object, e As EventArgs) Handles mnuSetAllMutationsToPrivate.Click
        SetAllMutationsToPrivate()
    End Sub

    Private Sub mnuLoadVariantsFromDbHG19_Click(sender As Object, e As EventArgs) Handles mnuLoadVariantsFromDbHG19.Click
        LoadMembersVariantFromBigYHg19DB()
    End Sub

    Private Sub mnuLoadPrivateMutFromDbHG19_Click(sender As Object, e As EventArgs) Handles mnuLoadPrivateMutFromDbHG19.Click
        FindPrivateMutationsFromBigYHg19DB()
    End Sub

    Private Sub mnuSetAllToHasMutationhg19_Click(sender As Object, e As EventArgs) Handles mnuSetAllToHasMutationhg19.Click
        SetAllMembersToHasVariant19()
    End Sub
End Class