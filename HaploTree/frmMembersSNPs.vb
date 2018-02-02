Public Class frmMembersSNPs
    Dim cDataAccess As New clsDataAccess
    Dim mintMemberID As Integer = 0


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim frmMembersSearch As New frmMembersSearch
        frmMembersSearch.ShowDialog()

        mintMemberID = frmMembersSearch.ID
        Call PopulateForm()
    End Sub

    Private Sub frmMembersSNPs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Public Sub PopulateForm()
        Dim ds As DataSet
        Dim dsPositions As DataSet

        'First remove all records from the listview
        Me.lvwSNPs.Clear()

        ds = cDataAccess.GetMemberByID(mintMemberID)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lblID.Text = mintMemberID

            If ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                Me.lblMemberName.Text = ds.Tables(0).Rows(0).Item("MemberName")
            Else
                Me.lblMemberName.Text = ""
            End If

            If ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                Me.lblFTDNAID.Text = ds.Tables(0).Rows(0).Item("FTDNAID")
            Else
                Me.lblFTDNAID.Text = ""
            End If

            If ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                Me.lblYFullID.Text = ds.Tables(0).Rows(0).Item("YFullID")
            Else
                Me.lblYFullID.Text = ""
            End If

            'Now see if this person has records stored
            dsPositions = cDataAccess.GetHg38VariantsByMemberID(mintMemberID)
            If dsPositions.Tables(0).Rows.Count > 0 Then
                Call FillListview(dsPositions)
            End If

        End If
    End Sub


    Public Sub FillListview(ByVal dsPositions As DataSet)
        Dim i As Integer = 0

        Dim itmListItem As ListViewItem
        Dim shtCntr As Short
        Try

            If dsPositions.Tables(0).Rows.Count > 0 Then
                'Do headers first
                Me.lvwSNPs.Clear()

                Me.lvwSNPs.Columns.Add("ID", 0, HorizontalAlignment.Left)
                Me.lvwSNPs.Columns.Add("Position", 80, HorizontalAlignment.Left)
                lvwSNPs.Columns.Add("Ref", 50, HorizontalAlignment.Left)
                lvwSNPs.Columns.Add("Alt", 50, HorizontalAlignment.Left)
                lvwSNPs.Columns.Add("Qual", 70, HorizontalAlignment.Left)
                lvwSNPs.Columns.Add("Filter", 60, HorizontalAlignment.Left)
                lvwSNPs.Columns.Add("Info", 140, HorizontalAlignment.Left)
                'lvwSNPs.Columns.Add("Format", 140, HorizontalAlignment.Left)
                lvwSNPs.Columns.Add("Mutation", 140, HorizontalAlignment.Left)

                For i = 0 To dsPositions.Tables(0).Rows.Count - 1
                    itmListItem = New ListViewItem()
                    itmListItem.Text = dsPositions.Tables(0).Rows(i).Item(0)

                    For shtCntr = 1 To dsPositions.Tables(0).Columns.Count - 1
                        Select Case dsPositions.Tables(0).Columns.Item(shtCntr).ColumnName()
                            Case "ID"
                                If dsPositions.Tables(0).Rows(i).Item(shtCntr) Is System.DBNull.Value = True Then
                                    itmListItem.SubItems.Add("")
                                Else
                                    If dsPositions.Tables(0).Rows(i).IsNull(shtCntr) = False Then
                                        itmListItem.SubItems.Add(dsPositions.Tables(0).Rows(i).Item(shtCntr))
                                    Else
                                        itmListItem.SubItems.Add("")
                                    End If
                                End If
                            Case "FK_MemberID"
                                'We shouldn't display this so do nothing with it.







                            Case Else
                                If dsPositions.Tables(0).Rows(i).IsNull(shtCntr) = False Then
                                    itmListItem.SubItems.Add(dsPositions.Tables(0).Rows(i).Item(shtCntr))
                                Else
                                    itmListItem.SubItems.Add("")
                                End If

                                'itmListItem.SubItems.Add("")
                        End Select
                    Next shtCntr
                    Me.lvwSNPs.Items.Add(itmListItem)


                    Select Case dsPositions.Tables(0).Rows(i).Item("Ref")
                        Case "T"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(2).BackColor = Color.Red
                            lvwSNPs.Items(i).SubItems(2).ForeColor = Color.White
                        Case "A"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(2).BackColor = Color.Green
                            lvwSNPs.Items(i).SubItems(2).ForeColor = Color.White
                        Case "G"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(2).BackColor = Color.Orange
                            lvwSNPs.Items(i).SubItems(2).ForeColor = Color.White
                        Case "C"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(2).BackColor = Color.DarkBlue
                            lvwSNPs.Items(i).SubItems(2).ForeColor = Color.White
                        Case Else
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(2).BackColor = Color.LightBlue
                            lvwSNPs.Items(i).SubItems(2).ForeColor = Color.White
                    End Select


                    Select Case dsPositions.Tables(0).Rows(i).Item("Alt")
                        Case "T"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(3).BackColor = Color.Red
                            lvwSNPs.Items(i).SubItems(3).ForeColor = Color.White
                        Case "A"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(3).BackColor = Color.Green
                            lvwSNPs.Items(i).SubItems(3).ForeColor = Color.White
                        Case "G"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(3).BackColor = Color.Orange
                            lvwSNPs.Items(i).SubItems(3).ForeColor = Color.White
                        Case "C"
                            lvwSNPs.Items(i).UseItemStyleForSubItems = False
                            lvwSNPs.Items(i).SubItems(3).BackColor = Color.DarkBlue
                            lvwSNPs.Items(i).SubItems(3).ForeColor = Color.White
                    End Select
                Next

                lblPassingPositions.Text = lvwSNPs.Items.Count

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




    End Sub

    Public Sub PaintCells()
        Dim item As ListViewItem
        For Each item In lvwSNPs.Items
            Select Case item.SubItems(2).Text
                Case "T"
                    item.SubItems(2).BackColor = Color.Red
                    item.SubItems(2).ForeColor = Color.White
                Case "A"
                    item.SubItems(2).BackColor = Color.Green
                    item.SubItems(2).ForeColor = Color.White
                Case "G"
                    item.SubItems(2).BackColor = Color.Orange
                    item.SubItems(2).ForeColor = Color.White
                Case "C"
                    item.SubItems(2).BackColor = Color.DarkBlue
                    item.SubItems(2).ForeColor = Color.White
                Case Else
                    item.SubItems(2).BackColor = Color.LightBlue
                    item.SubItems(2).ForeColor = Color.White
            End Select
        Next

        For Each item In lvwSNPs.Items
            Select Case item.SubItems(3).Text
                Case "T"
                    item.SubItems(3).BackColor = Color.Red
                    item.SubItems(3).ForeColor = Color.White
                Case "A"
                    item.SubItems(3).BackColor = Color.Green
                    item.SubItems(3).ForeColor = Color.White
                Case "G"
                    item.SubItems(3).BackColor = Color.Orange
                    item.SubItems(3).ForeColor = Color.White
                Case "C"
                    item.SubItems(3).BackColor = Color.DarkBlue
                    item.SubItems(3).ForeColor = Color.White
                Case Else
                    item.SubItems(3).BackColor = Color.LightBlue
                    item.SubItems(3).ForeColor = Color.White
            End Select
        Next
    End Sub

    Private Sub lvwSNPs_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvwSNPs.ColumnClick
        'Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Me.lvwSNPs.ListViewItemSorter = New ListViewItemComparer(e.Column)
        ' Call the sort method to manually sort.
        lvwSNPs.Sort()
    End Sub

    Private Sub lvwSNPs_MouseDown(sender As Object, e As MouseEventArgs) Handles lvwSNPs.MouseDown
        Dim selection As ListViewItem = lvwSNPs.GetItemAt(e.X, e.Y)

        'If the user selects an item in the ListView, set the variable
        If Not (selection Is Nothing) Then
            ' If lblPayperiod.Text.Trim.Length > 0 And lblStudentName.Text.Trim.Length > 0 Then
            If lvwSNPs.Items.Count > 0 Then
                Dim index As Integer = lvwSNPs.GetItemAt(e.X, e.Y).Index()
                Me.lblPosition.Text = Me.lvwSNPs.Items(index).SubItems(1).Text
                Me.lblRef.Text = Me.lvwSNPs.Items(index).SubItems(2).Text
                Me.lblAlt.Text = Me.lvwSNPs.Items(index).SubItems(3).Text
                Me.lblQuality.Text = Me.lvwSNPs.Items(index).SubItems(4).Text
                Me.lblFilter.Text = Me.lvwSNPs.Items(index).SubItems(5).Text
                Me.lblInfo.Text = Me.lvwSNPs.Items(index).SubItems(6).Text
                'Me.lblFormat.Text = Me.lvwSNPs.Items(index).SubItems(7).Text
                Me.lblMutation.Text = Me.lvwSNPs.Items(index).SubItems(7).Text

                'Find others with this SNP and put them in the listvew on the tabMembersWithSNP tabl
                Call OtherMemberswithSNP(Me.lblPosition.Text, Me.lblRef.Text, Me.lblAlt.Text)
            End If
        End If
    End Sub


    Public Sub OtherMembersWithSNP(ByVal vintPosition As Integer,
                                        ByVal vstrRef As String,
                                        ByVal vstrAlt As String)
        Dim i As Integer = 0

        Dim itmListItem As ListViewItem
        Dim shtCntr As Short
        Dim ds As DataSet
        lvwMembersWithSNP.Clear()
        ds = cDataAccess.GetSNPByPositionRefAlt(vintPosition, vstrRef, vstrAlt)
        If ds.Tables(0).Rows.Count > 0 Then

            Try
                'Do headers first
                Me.lvwMembersWithSNP.Clear()

                Me.lvwMembersWithSNP.Columns.Add("ID", 0, HorizontalAlignment.Left)
                Me.lvwMembersWithSNP.Columns.Add("Member Name", 120, HorizontalAlignment.Left)
                Me.lvwMembersWithSNP.Columns.Add("Position", 80, HorizontalAlignment.Left)
                lvwMembersWithSNP.Columns.Add("Ref", 50, HorizontalAlignment.Left)
                lvwMembersWithSNP.Columns.Add("Alt", 50, HorizontalAlignment.Left)
                lvwMembersWithSNP.Columns.Add("Qual", 70, HorizontalAlignment.Left)
                lvwMembersWithSNP.Columns.Add("Filter", 60, HorizontalAlignment.Left)


                For i = 0 To ds.Tables(0).Rows.Count - 1
                    itmListItem = New ListViewItem()
                    itmListItem.Text = ds.Tables(0).Rows(i).Item(0)
                    If ds.Tables(0).Rows(i).Item("MemberName") <> Me.lblMemberName.Text Then

                        For shtCntr = 1 To ds.Tables(0).Columns.Count - 1
                            Select Case ds.Tables(0).Columns.Item(shtCntr).ColumnName()
                                Case "ID" ', "MemberName"
                                    If ds.Tables(0).Rows(i).Item(shtCntr) Is System.DBNull.Value = True Then
                                        itmListItem.SubItems.Add("")
                                    Else
                                        If ds.Tables(0).Rows(i).IsNull(shtCntr) = False Then
                                            itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item(shtCntr))
                                        Else
                                            itmListItem.SubItems.Add("")
                                        End If
                                    End If
                                Case "Pos", "Ref", "Alt", "Qual", "Filter", "MemberName"
                                    If ds.Tables(0).Rows(i).IsNull(shtCntr) = False Then
                                        itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item(shtCntr))
                                    Else
                                        itmListItem.SubItems.Add("")
                                    End If

                                Case Else
                                    If ds.Tables(0).Rows(i).IsNull(shtCntr) = False Then
                                        itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item(shtCntr))
                                    Else
                                        itmListItem.SubItems.Add("")
                                    End If

                                    'itmListItem.SubItems.Add("")


                            End Select
                        Next shtCntr
                        Me.lvwMembersWithSNP.Items.Add(itmListItem)
                    End If
                Next


                lblPassingPositions.Text = lvwMembersWithSNP.Items.Count
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub





    Private Sub btnFindPosition_Click(sender As Object, e As EventArgs) Handles btnFindPosition.Click
        Dim item As ListViewItem

        lvwSNPs.BackColor = Color.White
        lvwSNPs.ForeColor = Color.Blue


        For Each item In lvwSNPs.Items
            item.SubItems(1).BackColor = Color.White
                item.SubItems(2).BackColor = Color.White
                item.SubItems(3).BackColor = Color.White
                item.SubItems(4).BackColor = Color.White
                item.SubItems(5).BackColor = Color.White
                item.SubItems(6).BackColor = Color.White
            item.SubItems(7).BackColor = Color.White
            item.SubItems(8).BackColor = Color.White
        Next

        Call PaintCells()


        For Each item In lvwSNPs.Items
            If item.SubItems(1).Text = Me.txtFindPosition.Text Then
                '     item.BackColor = Color.Red

                item.SubItems(1).BackColor = Color.Red
                item.SubItems(2).BackColor = Color.Red
                item.SubItems(3).BackColor = Color.Red
                item.SubItems(4).BackColor = Color.Red
                item.SubItems(5).BackColor = Color.Red
                item.SubItems(6).BackColor = Color.Red
                item.SubItems(7).BackColor = Color.Red
                item.SubItems(8).BackColor = Color.Red

                item.ForeColor = Color.Blue
                '  lvwSNPs.Focus()
                Exit For
            End If
        Next
    End Sub

    

    Private Sub lblMemberName_TextChanged(sender As Object, e As EventArgs) Handles lblMemberName.TextChanged
        Me.txtFindPosition.Enabled = True
        Me.btnFindPosition.Enabled = True
    End Sub
End Class