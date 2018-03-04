Public Class frmMembersSearch
    Dim cDataAccess As New clsDataAccess
    Dim mintMemberID As Integer = 0


    Public Property ID() As Integer
        Get
            Return mintMemberID
        End Get
        Set(ByVal value As Integer)
            mintMemberID = value
        End Set
    End Property
    Private Sub frmMembersSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call PopulateMembers()
    End Sub


    Public Sub PopulateMembers()
        Dim ds As DataSet

        Me.lvwMembers.Clear()
        ds = cDataAccess.GetAllMembers
        If ds.Tables(0).Rows.Count > 0 Then
            Call FillListview(ds)
        End If

    End Sub



    Public Sub FillListview(ByVal ds As DataSet)
        Dim lvwColumn As ColumnHeader
        Dim itmListItem As ListViewItem
        Dim myCheckFont As New System.Drawing.Font("Wingdings", 12, FontStyle.Regular)

        'Try
        If ds.Tables(0).Rows.Count > 0 Then

            'Do headers first
            Me.lvwMembers.Clear()

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Width = 0
            lvwColumn.Text = "ID"

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Width = 180
            lvwColumn.Text = "MemberName"

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Text = "FTDNAID"
            lvwColumn.Width = 120

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Text = "YFullID"
            lvwColumn.Width = 120

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Text = "Hg19"
            lvwColumn.Width = 50
            lvwMembers.Columns(4).TextAlign = HorizontalAlignment.Center

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Text = "Hg38"
            lvwColumn.Width = 50
            lvwMembers.Columns(5).TextAlign = HorizontalAlignment.Center

            lvwColumn = New ColumnHeader()
            lvwMembers.Columns.Add(lvwColumn)
            lvwColumn.Text = "InTree"
            lvwColumn.Width = 60
            lvwMembers.Columns(6).TextAlign = HorizontalAlignment.Center


            For i = 0 To ds.Tables(0).Rows.Count - 1

                itmListItem = New ListViewItem()
                itmListItem.Text = ds.Tables(0).Rows(i).Item("ID")
                itmListItem.UseItemStyleForSubItems = False

                If ds.Tables(0).Rows(i).IsNull("MemberName") = False Then
                    itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item("MemberName"))
                Else
                    itmListItem.SubItems.Add("")
                End If


                If ds.Tables(0).Rows(i).IsNull("FTDNAID") = False Then
                    itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item("FTDNAID"))
                Else
                    itmListItem.SubItems.Add("")
                End If

                If ds.Tables(0).Rows(i).IsNull("YFullID") = False Then
                    itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item("YFullID"))
                Else
                    itmListItem.SubItems.Add("")
                End If

                If ds.Tables(0).Rows(i).IsNull("HasVariantHg19") = False Then
                    If ds.Tables(0).Rows(i).Item("HasVariantHg19").ToString = "True" Then
                        itmListItem.SubItems.Add(Chr(254))
                        itmListItem.SubItems.Item(4).ForeColor = Color.DarkBlue
                    Else
                        itmListItem.SubItems.Add(Chr(168))
                        itmListItem.SubItems.Item(4).ForeColor = Color.DarkRed
                    End If
                    itmListItem.SubItems.Item(4).Font = myCheckFont
                Else
                    itmListItem.SubItems.Add("")
                End If

                If ds.Tables(0).Rows(i).IsNull("HasVariantHg38") = False Then
                    If ds.Tables(0).Rows(i).Item("HasVariantHg38").ToString = "True" Then
                        itmListItem.SubItems.Add(Chr(254))
                        itmListItem.SubItems.Item(5).ForeColor = Color.DarkBlue
                    Else
                        itmListItem.SubItems.Add(Chr(168))
                        itmListItem.SubItems.Item(5).ForeColor = Color.DarkRed
                    End If
                    itmListItem.SubItems.Item(5).Font = myCheckFont
                Else
                    itmListItem.SubItems.Add("")
                End If

                If ds.Tables(0).Rows(i).IsNull("IsPlacedInTheTree") = False Then
                    If ds.Tables(0).Rows(i).Item("IsPlacedInTheTree").ToString = "True" Then
                        itmListItem.SubItems.Add(Chr(254))
                        itmListItem.SubItems.Item(6).ForeColor = Color.DarkBlue
                    Else
                        itmListItem.SubItems.Add(Chr(168))
                        itmListItem.SubItems.Item(6).ForeColor = Color.DarkRed
                    End If
                    itmListItem.SubItems.Item(6).Font = myCheckFont
                Else
                    itmListItem.SubItems.Add("")
                End If

                Me.lvwMembers.Items.Add(itmListItem)
            Next
            Me.lblMembers.Text = lvwMembers.Items.Count
        End If
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub lvwMembers_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvwMembers.ColumnClick
        'Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Me.lvwMembers.ListViewItemSorter = New ListViewItemComparer(e.Column)
        ' Call the sort method to manually sort.
        lvwMembers.Sort()
    End Sub

    Private Sub lvwMembers_MouseClick(sender As Object, e As MouseEventArgs) Handles lvwMembers.MouseClick
        Dim selection As ListViewItem = lvwMembers.GetItemAt(e.X, e.Y)

        'If the user selects an item in the ListView, set the variable
        If Not (selection Is Nothing) Then
            ' If lblPayperiod.Text.Trim.Length > 0 And lblStudentName.Text.Trim.Length > 0 Then
            If lvwMembers.Items.Count > 0 Then
                Dim index As Integer = lvwMembers.GetItemAt(e.X, e.Y).Index()
            End If
        End If
    End Sub

    Private Sub lvwMembers_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvwMembers.MouseDoubleClick
        Dim selection As ListViewItem = lvwMembers.GetItemAt(e.X, e.Y)
        If Not (selection Is Nothing) Then
            Me.ID = mintMemberID
            Me.Close()
        End If
    End Sub

    Private Sub lvwMembers_MouseDown(sender As Object, e As MouseEventArgs) Handles lvwMembers.MouseDown
        Dim selection As ListViewItem = lvwMembers.GetItemAt(e.X, e.Y)
        If Not (selection Is Nothing) Then
            mintMemberID = Me.lvwMembers.GetItemAt(e.X, e.Y).Text
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Me.ID = mintMemberID
        Me.Close()
    End Sub



    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim frmMembers As New frmMembers
        frmMembers.ShowDialog()
        Call PopulateMembers()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        mintMemberID = 0
        Me.Close()
    End Sub
End Class