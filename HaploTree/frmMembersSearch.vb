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
        Dim shtCntr As Short

        Try
            If ds.Tables(0).Rows.Count > 0 Then
                'Do headers first
                Me.lvwMembers.Clear()
                For shtCntr = 0 To ds.Tables(0).Columns.Count - 1
                    lvwColumn = New ColumnHeader()

                    lvwMembers.Columns.Add(lvwColumn)

                    Select Case ds.Tables(0).Columns.Item(shtCntr).ColumnName()
                        Case "ID"   ', "MemberName"
                            'lvwColumn = New ColumnHeader()
                            'lvwColumn.Text = ds.Tables(0).Columns.Item(shtCntr).ColumnName()
                            'lvwMembers.Columns.Add(lvwColumn)
                            '         If ds.Tables(0).Columns.Item(shtCntr).ColumnName() = "ID" Then 'Hide columns staring with ID
                            lvwColumn.Width = 0
                        Case "YFullID"
                            lvwColumn.Text = ds.Tables(0).Columns.Item(shtCntr).ColumnName()
                            lvwColumn.Width = 120
                        Case "MemberName"
                            lvwColumn.Text = ds.Tables(0).Columns.Item(shtCntr).ColumnName()
                            lvwColumn.Width = 180
                        Case "FTDNAID"
                            lvwColumn.Text = ds.Tables(0).Columns.Item(shtCntr).ColumnName()
                            lvwColumn.Width = 120
                    End Select
                Next

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    itmListItem = New ListViewItem()
                    itmListItem.Text = ds.Tables(0).Rows(i).Item(0)

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
                            Case Else
                                If ds.Tables(0).Rows(i).IsNull(shtCntr) = False Then
                                    itmListItem.SubItems.Add(ds.Tables(0).Rows(i).Item(shtCntr))
                                Else
                                    itmListItem.SubItems.Add("")
                                End If

                                'itmListItem.SubItems.Add("")
                        End Select
                    Next shtCntr
                    Me.lvwMembers.Items.Add(itmListItem)
                Next
                Me.lblMembers.Text = lvwMembers.Items.Count
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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