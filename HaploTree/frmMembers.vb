Public Class frmMembers
    Dim cDataAccess As New clsDataAccess
    Dim mintMemberID As Integer

    Public Property ID() As Integer
        Get
            Return mintMemberID
        End Get
        Set(ByVal value As Integer)
            mintMemberID = value
        End Set
    End Property

    Private Sub frmFindMember_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                Me.lblID.Text = Me.lvwMembers.Items(index).SubItems(0).Text
                Me.txtName.Text = Me.lvwMembers.Items(index).SubItems(1).Text
                Me.txtFTDNAID.Text = Me.lvwMembers.Items(index).SubItems(2).Text
                Me.txtYFullID.Text = Me.lvwMembers.Items(index).SubItems(3).Text

            End If
        End If
    End Sub

    Private Sub lvwMembers_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvwMembers.MouseDoubleClick
        'Dim selection As ListViewItem = lvwMembers.GetItemAt(e.X, e.Y)
        'If Not (selection Is Nothing) Then
        '    Me.ID = mintMemberID
        '    Me.Close()
        'End If
    End Sub

    Private Sub lvwMembers_MouseDown(sender As Object, e As MouseEventArgs) Handles lvwMembers.MouseDown
        Dim selection As ListViewItem = lvwMembers.GetItemAt(e.X, e.Y)
        If Not (selection Is Nothing) Then
            mintMemberID = Me.lvwMembers.GetItemAt(e.X, e.Y).Text
        End If
    End Sub


    Public Sub ClearInfo()
        Me.lblID.Text = ""
        Me.txtName.Text = ""
        Me.txtFTDNAID.Text = ""
        Me.txtYFullID.Text = ""
    End Sub


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call ClearInfo()
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        If txtName.Text.Length > 0 Then
            Me.btnSave.Enabled = True

        Else
            Me.btnSave.Enabled = False

        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim blnExists As Boolean
        Dim intReturn As Integer

        msg = "Save this record?"   ' Define message.
        style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo
        title = "SAVE"   ' Define title.

        ' Display message.
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then   ' User chose Yes.
            Me.Cursor = Cursors.WaitCursor
            'Check to see if any of this information MAY exist in the database already.
            If lblID.Text.Length = 0 Then
                blnExists = AlreadyExists()
                If blnExists = False Then
                    intReturn = SaveRecord
                    If intReturn > 0 Then
                        MsgBox("Record has been saved!")
                    End If

                Else
                    MsgBox("A record already exists with this FTDNA IS or YFull ID. Record was NOT saved")
                End If
            Else
                intReturn = SaveRecord()
                If intReturn > 0 Then
                    MsgBox("Record has been saved!")
                End If

            End If

            Me.Cursor = Cursors.Default
            Call PopulateMembers()
            Call ClearInfo()
        End If

    End Sub


    Public Function AlreadyExists() As Boolean
        Dim ds As DataSet

        ds = cDataAccess.GetMemberByFTDNAID(Me.txtFTDNAID.Text.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If

        ds = cDataAccess.GetMemberByYFullID(Me.txtYFullID.Text.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function SaveRecord() As Integer
        Try
            If Me.lblID.Text.Length = 0 Then 'This is an insert
                Return cDataAccess.InsertMember(Me.txtName.Text.Trim, Me.txtFTDNAID.Text.Trim, Me.txtYFullID.Text.Trim)
            Else 'This is an update
                Return cDataAccess.UpdateMember(Me.txtName.Text.Trim, Me.txtFTDNAID.Text.Trim, Me.txtYFullID.Text.Trim, lblID.Text.Trim)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42353 ' Function doesn't return a value on all code paths

    Private Sub lvwMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwMembers.SelectedIndexChanged

    End Sub



    Private Sub lblID_TextChanged(sender As Object, e As EventArgs) Handles lblID.TextChanged
        If Me.lblID.Text.Length > 0 Then
            Me.btnDelete.Enabled = True
        End If
    End Sub

    Private Sub txtFTDNAID_TextChanged(sender As Object, e As EventArgs) Handles txtFTDNAID.TextChanged

    End Sub
End Class