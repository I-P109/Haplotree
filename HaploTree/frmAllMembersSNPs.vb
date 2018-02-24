Public Class frmAllMembersSNPs
    Private cDataAccess As New clsDataAccess
    Private MembersList As Member()
    Private MutationList As Mutation()
    Private PositionList As Position() 'must have same order as MutList
    Private NodeNameList As String()
    'Private NodeIDList As Integer()
    Private p_MemberSelected As Boolean
    Private p_MutationSelected As Boolean
    Private SelectedItms() As ListViewItem
    Private MoveAboveItem As Integer
    Private indexOfItemUnderMouseToDrop As Integer
    Private p_SelectedNodeID As Integer
    Private p_SelectedNodeName As String
    Private OriginalViewSorter As IComparer
    Private Rowindex As Integer
    Private Colindex As Integer

    Public Property SelectedNodeID As Integer
        Get
            Return p_SelectedNodeID
        End Get
        Set(value As Integer)
            p_SelectedNodeID = value
        End Set
    End Property

    Public Property MemberSelected As Boolean
        Get
            Return p_MemberSelected
        End Get
        Set(value As Boolean)
            p_MemberSelected = value
            If value = True Then
                ckbxMembers.Checked = True
            Else
                ckbxMembers.Checked = False
            End If
            If MemberSelected = True And MutationSelected = True Then
                'Me.btnFillTable.Enabled = True
            Else
                'Me.btnFillTable.Enabled = False
            End If
        End Set
    End Property

    Public Property MutationSelected As Boolean
        Get
            Return p_MutationSelected
        End Get
        Set(value As Boolean)
            p_MutationSelected = value
            If value = True Then
                ckbxMutations.Checked = True
            Else
                ckbxMutations.Checked = False
            End If
            If MemberSelected = True And MutationSelected = True Then
                'Me.btnFillTable.Enabled = True
            Else
                'Me.btnFillTable.Enabled = False
            End If
        End Set
    End Property

    Public Property SelectedNodeName As String
        Get
            Return p_SelectedNodeName
        End Get
        Set(value As String)
            p_SelectedNodeName = value
        End Set
    End Property

    Private Sub Form_Closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub

    Public Sub PopulateAllSNPFormWithBigYHg19()
        'First remove all records from the listview
        Me.lvwSNPs.Clear()

        'add columns
        Me.lvwSNPs.Columns.Add("Node Name", 100, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Mutation Name", 100, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Position", 80, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Ref", 40, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Alt", 40, HorizontalAlignment.Left)

        'add a column for each member
        If Not IsNothing(MembersList) Then
            If MembersList.Count > 0 Then
                Dim Memb As Member
                For Each Memb In MembersList
                    Me.lvwSNPs.Columns.Add(Memb.FTDNAKit, 50, HorizontalAlignment.Left)
                Next
            Else
                Me.lvwSNPs.Clear()
                Exit Sub
            End If
        Else
            Me.lvwSNPs.Clear()
            Exit Sub
        End If

        'fill table
        FillListViewWithBigYHg19()
        PaintCells()
    End Sub

    Public Sub PopulateAllSNPForm()
        'First remove all records from the listview
        Me.lvwSNPs.Clear()

        'add columns
        Me.lvwSNPs.Columns.Add("Node Name", 100, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Name", 100, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Position", 80, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Ref", 40, HorizontalAlignment.Left)
        Me.lvwSNPs.Columns.Add("Alt", 40, HorizontalAlignment.Left)

        'add a column for each member
        If Not IsNothing(MembersList) Then
            If MembersList.Count > 0 Then
                Dim Memb As Member
                For Each Memb In MembersList
                    Me.lvwSNPs.Columns.Add(Memb.FTDNAKit, 50, HorizontalAlignment.Left)
                Next
            Else
                Me.lvwSNPs.Clear()
                Exit Sub
            End If
        Else
            Me.lvwSNPs.Clear()
            Exit Sub
        End If

        'fill table
        FillListView()
        PaintCells()
    End Sub

    Public Sub FillListViewWithBigYHg19()
        Dim BigYHg19ds As DataSet
        Dim i As Integer = 0
        Dim itmListItem As ListViewItem
        Dim Cntr As Integer
        Dim NbMembers As Integer

        If Not IsNothing(MembersList) Then
            NbMembers = MembersList.Count
        Else
            NbMembers = 0
            Exit Sub
        End If

        Try
            BigYHg19ds = cDataAccess.GetBigYHg19MutationsFromMemberList(MembersList)
            If Not IsNothing(BigYHg19ds) Then
                If BigYHg19ds.Tables(0).Rows.Count > 0 Then
                    Dim str(BigYHg19ds.Tables(0).Rows.Count - 1, NbMembers - 1) As String
                    For i = 0 To BigYHg19ds.Tables(0).Rows.Count - 1
                        itmListItem = New ListViewItem()
                        itmListItem.Text = "No Node" 'item 0
                        itmListItem.Text = BigYHg19ds.Tables(0).Rows(i).Item("SNPName") 'item 1
                        itmListItem.SubItems.Add(BigYHg19ds.Tables(0).Rows(i).Item("PosHg19")) 'item 2
                        itmListItem.SubItems.Add(BigYHg19ds.Tables(0).Rows(i).Item("Reference")) 'item 3
                        itmListItem.SubItems.Add("")

                        For Cntr = 0 To NbMembers - 1
                            If BigYHg19ds.Tables(0).Rows(i).IsNull(3 + Cntr) = False Then
                                str(i, Cntr) = ""
                            ElseIf BigYHg19ds.Tables(0).Rows(i).Item(3 + Cntr) = "COVERED" Then
                                str(i, Cntr) = BigYHg19ds.Tables(0).Rows(i).Item("Reference")
                            Else
                                Dim str1 As String
                                str1 = BigYHg19ds.Tables(0).Rows(i).Item(3 + Cntr)
                                If str1.Contains("PASS") Then
                                    str(i, Cntr) = Strings.Left(str1, 1)
                                Else
                                    str(i, Cntr) = ""
                                End If
                            End If

                            itmListItem.SubItems.Add(str(i, Cntr))
                        Next

                        Me.lvwSNPs.Items.Add(itmListItem)
                    Next
                    Me.lblPassingPositions.Text = Me.lvwSNPs.Items.Count
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FillListView()
        Dim i As Integer = 0
        Dim itmListItem As ListViewItem
        Dim Cntr As Integer
        Dim NbMembers As Integer
        Dim NbMut As Integer

        If Not IsNothing(MembersList) Then
            NbMembers = MembersList.Count
        Else
            NbMembers = 0
            Exit Sub
        End If

        If Not IsNothing(MutationList) Then
            NbMut = MutationList.Count
        Else
            NbMut = 0
            Exit Sub
        End If

        Try
            If NbMut > 0 Then
                Dim str(NbMut - 1, NbMembers - 1) As String
                For i = 0 To NbMut - 1
                    itmListItem = New ListViewItem()
                    itmListItem.Text = NodeNameList(i)
                    itmListItem.SubItems.Add(MutationList(i).AllNames)
                    itmListItem.SubItems.Add(PositionList(i).PosHg38)
                    itmListItem.SubItems.Add(PositionList(i).AncestrallCall)
                    itmListItem.SubItems.Add(MutationList(i).AltCall)

                    For Cntr = 0 To NbMembers - 1
                        Dim AltCall As String

                        AltCall = MembersList(Cntr).GetFullAltCallAtPositionHg38(PositionList(i).PosHg38)
                        Select Case AltCall
                            Case "C", "T", "G", "A"
                                str(i, Cntr) = AltCall
                            Case Else
                                'check for putative mutation

                        End Select

                        itmListItem.SubItems.Add(str(i, Cntr))
                    Next

                    Me.lvwSNPs.Items.Add(itmListItem)
                Next
                Me.lblPassingPositions.Text = Me.lvwSNPs.Items.Count
                Me.OriginalViewSorter = lvwSNPs.ListViewItemSorter
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PaintCells()
        Dim item As ListViewItem
        Dim NbMembers As Integer
        Dim Cntr As Integer

        If Not IsNothing(MembersList) Then
            NbMembers = MembersList.Count
        Else
            NbMembers = 0
        End If

        For Each item In lvwSNPs.Items

            For Cntr = 0 To NbMembers + 1 '+1 and not -1 to include the ref and the mutation alt call columns: 2 additional columns
                Select Case item.SubItems(3 + Cntr).Text
                    Case "T"
                        item.UseItemStyleForSubItems = False
                        item.SubItems(3 + Cntr).BackColor = Color.Red
                        item.SubItems(3 + Cntr).ForeColor = Color.White
                    Case "A"
                        item.UseItemStyleForSubItems = False
                        item.SubItems(3 + Cntr).BackColor = Color.Green
                        item.SubItems(3 + Cntr).ForeColor = Color.White
                    Case "G"
                        item.UseItemStyleForSubItems = False
                        item.SubItems(3 + Cntr).BackColor = Color.Orange
                        item.SubItems(3 + Cntr).ForeColor = Color.White
                    Case "C"
                        item.UseItemStyleForSubItems = False
                        item.SubItems(3 + Cntr).BackColor = Color.DarkBlue
                        item.SubItems(3 + Cntr).ForeColor = Color.White
                    Case Else
                        item.UseItemStyleForSubItems = False
                        item.SubItems(3 + Cntr).BackColor = Color.LightBlue
                        item.SubItems(3 + Cntr).ForeColor = Color.White
                End Select
            Next
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

            If lvwSNPs.Items.Count > 0 Then
                Rowindex = lvwSNPs.GetItemAt(e.X, e.Y).Index()
                Colindex = GetColumnAtMousePosition(e.X)
                Me.MoveAboveItem = Rowindex
                'Find others with this SNP and put them in the listvew on the tabMembersWithSNP table
                Call OtherMembersWithSNP(Me.lvwSNPs.Items(Rowindex).SubItems(2).Text, Me.lvwSNPs.Items(Rowindex).SubItems(3).Text, Me.lvwSNPs.Items(Rowindex).SubItems(4).Text)
            End If
        End If
    End Sub


    Public Sub OtherMembersWithSNP(ByVal vintPosition As Integer,  'modify to get all members with variant 19 as well
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

                        End Select
                    Next shtCntr
                    Me.lvwMembersWithSNP.Items.Add(itmListItem)
                Next

                lblPassingPositions.Text = lvwMembersWithSNP.Items.Count
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnFindPosition_Click(sender As Object, e As EventArgs) Handles btnFindPosition.Click
        Dim item As ListViewItem
        Dim NbMembers As Integer
        Dim Cntr As Integer

        If Not IsNothing(MembersList) Then
            NbMembers = MembersList.Count
        Else
            NbMembers = 0
        End If

        lvwSNPs.BackColor = Color.White
        lvwSNPs.ForeColor = Color.Blue


        For Each item In lvwSNPs.Items
            For Cntr = 0 To NbMembers + 4 '+4 to include the node name, the mutation name, the position, the ref and the mutation alt call columns: 5 additional columns
                item.SubItems(Cntr).BackColor = Color.White
            Next
        Next

        Call PaintCells()

        For Each item In lvwSNPs.Items
            If item.SubItems(1).Text = Me.txtFindPosition.Text Then
                For Cntr = 0 To NbMembers + 4 '+4 to include the node name, the mutation name, the position, the ref and the mutation alt call columns: 5 additional columns
                    item.SubItems(Cntr).BackColor = Color.Red
                Next
                item.ForeColor = Color.Blue
                Exit For
            End If
        Next
    End Sub

    Private Sub frmAllMembersSNPs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.btnFillTable.Enabled = False
        Me.btnSelectMembers.Enabled = True
        Me.btnSelectMutations.Enabled = True
        Me.txtFindPosition.Enabled = False
        Me.btnFindPosition.Enabled = False
        Me.txtFindPosition.Text = ""
        Me.MembersList = Nothing
        Me.MutationList = Nothing
        Me.PositionList = Nothing
        Me.NodeNameList = Nothing
        Me.MemberSelected = False
        Me.MutationSelected = False
        Me.btnLoadAllBigYHg19.Enabled = False
        Me.SelectedItms = Nothing
        'Me.mnuCutFrom.Enabled = False
        'Me.mnuMoveToAbove.Enabled = False
        Me.MoveAboveItem = -1
        Me.lvwSNPs.LabelEdit = False
    End Sub

    Private Sub btnSelectMembers_Click(sender As Object, e As EventArgs) Handles btnSelectMembers.Click
        If Me.MemberSelected = False Then
            MemberSelected = SelectMembers()
            If MemberSelected = True Then
                btnSelectMembers.Text = "Discard Members"
                lblPassingPositions.Text = "0"
                Me.btnFindPosition.Enabled = False
                Me.txtFindPosition.Enabled = False
                Me.txtFindPosition.Text = ""
                If MutationSelected = True Then Me.FillTable()
                'btnLoadAllBigYHg19.Enabled = True 'to be activated if we solve the issue with this table!
            End If
        Else
            MemberSelected = False
            btnSelectMembers.Text = "Select Members"
            MembersList = Nothing
        End If

    End Sub

    Private Function SelectMembers() As Boolean

        Dim MembList As Integer()
        Dim PrgFrm As New frmProgress
        Dim i As Integer

        lvwSNPs.Clear()
        MembList = {1, 3, 2, 125}
        'MembList = {3, 7, 10, 21, 45, 55, 65, 76, 87, 98, 100, 110, 119, 125, 135, 145, 155, 165, 176} 'we need to find a way to select members

        MembersList = Nothing

        If IsNothing(MembList) Then
            MsgBox("No mutation available!")
            Return False
        End If
        ReDim MembersList(MembList.Count - 1)

        PrgFrm.InitiateMe()
        PrgFrm.Show()
        PrgFrm.UpdateMe("Loading members", 0)
        PrgFrm.Visible = True

        For i = 0 To MembList.Count - 1
            Dim Memb As New Member
            Memb.LoadWithID(MembList(i))
            MembersList(i) = Memb
            PrgFrm.UpdateMe("Loading members ...", 100 * ((i + 1) / MembList.Count))
        Next
        PrgFrm.Visible = False
        Return True

    End Function

    Private Sub FillTable()
        Call PopulateAllSNPForm()

        Me.btnFindPosition.Enabled = True
        'Me.btnFillTable.Enabled = False
        Me.txtFindPosition.Enabled = True
        Me.txtFindPosition.Text = ""
        'Me.mnuCutFrom.Enabled = True
    End Sub

    Private Sub txtFindPosition_TextChanged(sender As Object, e As EventArgs) Handles txtFindPosition.TextChanged
        If txtFindPosition.Text = "" Then
            Me.btnFindPosition.Enabled = False
        Else
            Me.btnFindPosition.Enabled = True
        End If
    End Sub

    Private Sub btnSelectMutations_Click(sender As Object, e As EventArgs) Handles btnSelectMutations.Click
        If Me.MutationSelected = False Then
            MutationSelected = SelectMutations()
            If MutationSelected = True Then
                btnSelectMutations.Text = "Discard Mutations"
                lblPassingPositions.Text = "0"
                Me.btnFindPosition.Enabled = False
                Me.txtFindPosition.Enabled = False
                Me.txtFindPosition.Text = ""
                If MemberSelected = True Then Me.FillTable()
            End If
        Else
            MutationSelected = False
            btnSelectMutations.Text = "Select Mutations"
            MutationList = Nothing
        End If
    End Sub

    Private Function SelectMutations() As Boolean
        Dim MutList As Integer()
        Dim PrgFrm As New frmProgress

        lvwSNPs.Clear()
        'MutList = GetAllMutationsIDs()

        MutList = {3, 30, 300, 900, 1200, 1500, 1800, 2100, 2400, 2700, 3000, 3300, 3600}

        If IsNothing(MutList) Then
            MsgBox("No mutation available!")
            Return False
        End If

        ReDim MutationList(MutList.Count - 1)
        ReDim PositionList(MutList.Count - 1)
        ReDim NodeNameList(MutList.Count - 1)


        PrgFrm.InitiateMe()
        PrgFrm.Show()
        PrgFrm.UpdateMe("Loading mutations", 0)
        PrgFrm.Visible = True

        For i = 0 To MutList.Count - 1
            Dim Mut As New Mutation
            Dim Pos As New Position

            Mut.Load(MutList(i))
            Pos.LoadWithID(Mut.PositionID)
            If Not Mut.CurrentParentNodeID = "" Then
                Dim Nod As New Node
                Nod.LoadWithID(Mut.CurrentParentNodeID)
                NodeNameList(i) = Nod.Name
            Else
                NodeNameList(i) = "No Node"
            End If
            MutationList(i) = Mut
            PositionList(i) = Pos
            PrgFrm.UpdateMe("Loading mutations ...", 100 * ((i + 1) / MutList.Count))
        Next
        PrgFrm.Visible = False
        Return True

    End Function

    Private Sub btnLoadAllBigYHg19_Click(sender As Object, e As EventArgs) Handles btnLoadAllBigYHg19.Click
        lvwSNPs.Clear()
        PopulateAllSNPFormWithBigYHg19()
        'Me.mnuCutFrom.Enabled = True
    End Sub

    Private Sub ListView_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvwSNPs.ItemDrag
        Dim myItem As ListViewItem
        Dim myItems(sender.SelectedItems.Count - 1) As ListViewItem
        Dim i As Integer = 0

        If sender Is Nothing OrElse Not TypeOf sender Is ListView Then Exit Sub
        ' Loop though the SelectedItems collection for the source.
        For Each myItem In sender.SelectedItems
            ' Add the ListViewItem to the array of ListViewItems.
            myItems(i) = myItem
            i = i + 1
        Next
        With CType(sender, ListView)
            .DoDragDrop(New DataObject("System.Windows.Forms.ListViewItem()", myItems), DragDropEffects.Move)
        End With
    End Sub

    Private Sub ListView_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvwSNPs.DragEnter
        If sender Is Nothing OrElse Not TypeOf sender Is ListView Then Exit Sub
        'If this is a listview item then allow the drag

        If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem()") Then
            e.Effect = DragDropEffects.Move
        End If

    End Sub

    Private Sub lvwSNPs_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvwSNPs.DragOver
        If sender Is Nothing OrElse Not TypeOf sender Is ListView Then Exit Sub
        'If this is a listview item then allow the drag

        If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem()") Then
            Dim p As Point = lvwSNPs.PointToClient(New Point(e.X, e.Y))
            If Not IsNothing(lvwSNPs.GetItemAt(p.X, p.Y)) Then
                indexOfItemUnderMouseToDrop = lvwSNPs.GetItemAt(p.X, p.Y).Index
            Else
                indexOfItemUnderMouseToDrop = -1
            End If
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub ListView_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvwSNPs.DragDrop
        If sender Is Nothing OrElse Not TypeOf sender Is ListView Then Exit Sub
        'Remove the item from the current listview and drop it in the new listview
        With CType(sender, ListView)
            If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem()") Then
                Dim drgItm As ListViewItem
                Dim draggedItem() As ListViewItem

                draggedItem = e.Data.GetData("System.Windows.Forms.ListViewItem()")
                For Each drgItm In draggedItem
                    drgItm.ListView.Items.Remove(drgItm)
                    If indexOfItemUnderMouseToDrop < 0 Then
                        .Items.Add(drgItm) 'should we test if exist already in the mutationlist and add it if not
                    Else
                        .Items.Insert(indexOfItemUnderMouseToDrop, drgItm) 'should we test if exist already in the mutationlist and add it if not
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub mnuCutFrom_Click(sender As Object, e As EventArgs)
        Dim i As Integer

        ReDim Me.SelectedItms(lvwSNPs.SelectedItems.Count - 1)
        For i = 0 To lvwSNPs.SelectedItems.Count - 1
            Me.SelectedItms(i) = lvwSNPs.SelectedItems.Item(i)
        Next
        'Me.mnuCutFrom.Enabled = False
        'Me.mnuMoveToAbove.Enabled = True
        Me.MoveAboveItem = -1
    End Sub

    Private Sub mnuMoveToAbove_Click(sender As Object, e As EventArgs)
        If lvwSNPs.SelectedItems.Count > 1 Then

        ElseIf lvwSNPs.SelectedItems.Count = 0 Then

        Else
            Me.MoveAboveItem = lvwSNPs.SelectedItems(0).Index
            MoveSelectedItemsAboveLine()
        End If
        Me.SelectedItms = Nothing
        'Me.mnuCutFrom.Enabled = True
        'Me.mnuMoveToAbove.Enabled = False
    End Sub

    Private Sub MoveSelectedItemsAboveLine() 'need to find a way to make it work also when we move below the originlly selected lines!

        Dim myItem As ListViewItem
        Dim i As Integer

        i = 0
        For Each myItem In Me.SelectedItms
            lvwSNPs.Items.Insert(MoveAboveItem + i, myItem.Clone)
            lvwSNPs.Items.Remove(myItem)
            i = i + 1
        Next
    End Sub

    Private Sub btnLoadTreeNode_Click(sender As Object, e As EventArgs) Handles btnLoadTreeNode.Click
        Dim frmHaploTree As New frmHaploTree
        lvwSNPs.Clear()
        frmHaploTree.ParentfrmAllMembersSNPs = Me
        frmHaploTree.SelectionMode = True
        frmHaploTree.Show()
    End Sub

    Public Sub LoadNode()
        Dim dsMutations As DataSet
        Dim dsMembers As DataSet
        Dim i As Integer

        i = 0
        Try
            If p_SelectedNodeID > 0 Then
                'Get the mutations from selectednode from the tblNode table
                dsMutations = cDataAccess.GetMutationIDsInNode(p_SelectedNodeID)
                If dsMutations.Tables(0).Rows.Count > 0 Then
                    If dsMutations.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                        Dim NodeIDLst(0) As Integer
                        NodeIDLst(0) = p_SelectedNodeID
                        LoadMutations(dsMutations, NodeIDLst)
                        MutationSelected = True
                    End If
                End If

                'Get the members from all the node below selectednode from the tblNode table
                dsMembers = GetMembersFromChrildrenNodes(p_SelectedNodeID) 'recursive function to get all members below the node
                LoadMembers(dsMembers)
                MemberSelected = True

                If Me.MemberSelected = True And Me.MutationSelected = True Then
                    'Me.btnFillTable.Enabled = True
                    Me.FillTable()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadAllNodesBelowOnly()
        Dim dsMutations As DataSet
        Dim dsMembers As DataSet

        Dim i As Integer = 0
        Try
            If p_SelectedNodeID > 0 Then
                'Get the mutations from all the node below selectednode from the tblNode table
                Dim NodeIDLst As Integer()
                NodeIDLst = Nothing
                dsMutations = GetMutationsFromChrildrenNodesOnly(p_SelectedNodeID, NodeIDLst)
                LoadMutations(dsMutations, NodeIDLst)
                MutationSelected = True

                'Get the members from all the node below selectednode from the tblNode table
                dsMembers = GetMembersFromChrildrenNodesOnly(p_SelectedNodeID) 'recursive function to get all members below the node
                LoadMembers(dsMembers)
                MemberSelected = True

                If Me.MemberSelected = True And Me.MutationSelected = True Then
                    'Me.btnFillTable.Enabled = True
                    Me.FillTable()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadAllNodesBelow()
        Dim dsMutations As DataSet
        Dim dsMembers As DataSet

        Dim i As Integer = 0
        Try
            If p_SelectedNodeID > 0 Then
                'Get the mutations from all the node below selectednode from the tblNode table
                Dim NodeIDLst(0) As Integer
                NodeIDLst(0) = p_SelectedNodeID
                dsMutations = GetMutationsFromChrildrenNodes(p_SelectedNodeID, NodeIDLst)
                LoadMutations(dsMutations, NodeIDLst)
                MutationSelected = True

                'Get the members from all the node below selectednode from the tblNode table
                dsMembers = GetMembersFromChrildrenNodes(p_SelectedNodeID) 'recursive function to get all members below the node
                LoadMembers(dsMembers)
                MemberSelected = True

                If Me.MemberSelected = True And Me.MutationSelected = True Then
                    'Me.btnFillTable.Enabled = True
                    Me.FillTable()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetMutationsFromChrildrenNodesOnly(NodeId As Integer, ByRef NdIDList As Integer()) As DataSet
        Dim dsMutations As DataSet
        Dim dsChildrenNode As DataSet
        Dim i As Integer
        Dim First As Boolean

        First = True
        dsMutations = Nothing
        Try
            dsChildrenNode = cDataAccess.GetNodeByParentNodeID(NodeId)
            If dsChildrenNode.Tables(0).Rows.Count > 0 Then
                If dsChildrenNode.Tables(0).Rows(0).IsNull("ID") = False Then
                    For i = 0 To dsChildrenNode.Tables(0).Rows.Count - 1
                        Dim Str As String
                        Dim StrArray As String()
                        Str = dsChildrenNode.Tables(0).Rows(i).Item("ID")
                        StrArray = Str.Split(",")
                        For Each MyStr In StrArray
                            If Not MyStr = "" Then
                                If First = True Then
                                    ReDim NdIDList(0)
                                    NdIDList(0) = MyStr
                                    dsMutations = GetMutationsFromChrildrenNodes(MyStr, NdIDList)
                                    First = False
                                Else
                                    Dim dsMoreMutations As DataSet
                                    Dim ListSize As Integer
                                    Dim NewListSize As Integer
                                    ListSize = NdIDList.Count
                                    NewListSize = ListSize + 1
                                    ReDim Preserve NdIDList(NewListSize - 1)
                                    NdIDList(NewListSize - 1) = MyStr
                                    dsMoreMutations = GetMutationsFromChrildrenNodes(MyStr, NdIDList)
                                    dsMutations.Merge(dsMoreMutations)
                                End If
                            End If
                        Next
                    Next
                End If
            End If
            Return dsMutations

        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function GetMutationsFromChrildrenNodes(NodeId As Integer, ByRef NdIDList As Integer()) As DataSet
        Dim dsMutations As DataSet
        Dim dsChildrenNode As DataSet
        Dim i As Integer

        Try
            dsMutations = cDataAccess.GetMutationIDsInNode(NodeId)
            dsChildrenNode = cDataAccess.GetNodeByParentNodeID(NodeId)
            If dsChildrenNode.Tables(0).Rows.Count > 0 Then
                If dsChildrenNode.Tables(0).Rows(0).IsNull("ID") = False Then
                    For i = 0 To dsChildrenNode.Tables(0).Rows.Count - 1
                        Dim Str As String
                        Dim StrArray As String()
                        Str = dsChildrenNode.Tables(0).Rows(i).Item("ID")
                        StrArray = Str.Split(",")
                        For Each MyStr In StrArray
                            If Not MyStr = "" Then
                                Dim dsMoreMutations As DataSet
                                Dim ListSize As Integer
                                Dim NewListSize As Integer
                                ListSize = NdIDList.Count
                                NewListSize = ListSize + 1
                                ReDim Preserve NdIDList(NewListSize - 1)
                                NdIDList(NewListSize - 1) = MyStr
                                dsMoreMutations = GetMutationsFromChrildrenNodes(MyStr, NdIDList)
                                dsMutations.Merge(dsMoreMutations)
                            End If
                        Next
                    Next
                End If
            End If
            Return dsMutations

        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try

    End Function

    Private Function GetMembersFromChrildrenNodesOnly(NodeId As Integer) As DataSet
        Dim dsMembers As DataSet
        Dim dsChildrenNode As DataSet
        Dim i As Integer
        Dim First As Boolean

        First = True
        dsMembers = Nothing
        Try
            dsChildrenNode = cDataAccess.GetNodeByParentNodeID(NodeId)
            If dsChildrenNode.Tables(0).Rows.Count > 0 Then
                If dsChildrenNode.Tables(0).Rows(0).IsNull("ID") = False Then
                    For i = 0 To dsChildrenNode.Tables(0).Rows.Count - 1
                        Dim Str As String
                        Dim StrArray As String()
                        Str = dsChildrenNode.Tables(0).Rows(i).Item("ID")
                        StrArray = Str.Split(",")
                        For Each MyStr In StrArray
                            If Not MyStr = "" Then
                                If First = True Then
                                    dsMembers = GetMembersFromChrildrenNodes(MyStr)
                                    First = False
                                Else
                                    Dim dsMoreMembers As DataSet
                                    dsMoreMembers = GetMembersFromChrildrenNodes(MyStr)
                                    dsMembers.Merge(dsMoreMembers)
                                End If
                            End If
                        Next
                    Next
                End If
            End If
            Return dsMembers

        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function GetMembersFromChrildrenNodes(NodeId As Integer) As DataSet ' recursive
        Dim dsMembers As DataSet
        Dim dsChildrenNode As DataSet
        Dim i As Integer

        Try
            'Get the members from the tblNode table
            dsMembers = cDataAccess.GetMemberIDsBelowNode(NodeId)

            dsChildrenNode = cDataAccess.GetNodeByParentNodeID(NodeId)
            If dsChildrenNode.Tables(0).Rows.Count > 0 Then
                If dsChildrenNode.Tables(0).Rows(0).IsNull("ID") = False Then
                    For i = 0 To dsChildrenNode.Tables(0).Rows.Count - 1
                        Dim Str As String
                        Dim StrArray As String()
                        Str = dsChildrenNode.Tables(0).Rows(i).Item("ID")
                        StrArray = Str.Split(",")
                        For Each MyStr In StrArray
                            Dim dsMoreMembers As DataSet
                            If Not MyStr = "" Then
                                dsMoreMembers = GetMembersFromChrildrenNodes(MyStr)
                                dsMembers.Merge(dsMoreMembers)
                            End If
                        Next
                    Next
                End If
            End If
            Return dsMembers

        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub LoadMutations(dsMut As DataSet, ByRef NdIDList As Integer())
        Dim i As Integer
        Dim j As Integer
        Dim Cntrt As Integer
        Dim PrgFrm As New frmProgress

        j = 0
        Cntrt = 0
        Try
            If dsMut.Tables(0).Rows.Count > 0 Then
                If dsMut.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then

                    PrgFrm.InitiateMe()
                    PrgFrm.Show()
                    PrgFrm.UpdateMe("Loading mutations", 0)
                    PrgFrm.Visible = True

                    For i = 0 To dsMut.Tables(0).Rows.Count - 1
                        Dim Str As String
                        Dim StrArray As String()

                        Str = dsMut.Tables(0).Rows(i).Item("MutationsIDs")
                        StrArray = Str.Split(",")
                        Cntrt = Cntrt + StrArray.Count
                        For Each MyStr In StrArray
                            If Not MyStr = "" Then
                                Dim Mut As New Mutation
                                Dim Pos As New Position
                                Mut.Load(MyStr)
                                Pos.LoadWithID(Mut.PositionID)
                                If j = 0 Then
                                    ReDim MutationList(j)
                                    ReDim PositionList(j)
                                    ReDim NodeNameList(j)
                                Else
                                    ReDim Preserve MutationList(j)
                                    ReDim Preserve PositionList(j)
                                    ReDim Preserve NodeNameList(j)
                                End If

                                If Not Mut.CurrentParentNodeID = "" Then
                                    Dim Nod As New Node
                                    Nod.LoadWithID(Mut.CurrentParentNodeID)
                                    NodeNameList(j) = Nod.Name
                                Else
                                    NodeNameList(j) = "No Node"
                                End If

                                'If IsNothing(NdIDList(i)) = True Then
                                'NodeNameList(i) = "No Node"
                                'Else
                                'Dim Nod As New Node
                                'Nod.LoadWithID(NdIDList(i))
                                'NodeNameList(j) = Nod.Name
                                'End If
                                MutationList(j) = Mut
                                PositionList(j) = Pos
                                j = j + 1
                            End If
                            PrgFrm.UpdateMe("Loading mutations ...", j, Cntrt)
                        Next
                        PrgFrm.UpdateMe("Loading mutations ...", (i + 1), dsMut.Tables(0).Rows.Count)
                    Next

                    PrgFrm.Visible = False
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadMembers(dsMemb As DataSet)
        Dim i As Integer
        Dim j As Integer
        Dim Cntrt As Integer
        Dim PrgFrm As New frmProgress

        j = 0
        Cntrt = 0
        Try
            If dsMemb.Tables(0).Rows.Count > 0 Then
                If dsMemb.Tables(0).Rows(0).IsNull("ChildrenMembersIDs") = False Then
                    PrgFrm.InitiateMe()
                    PrgFrm.Show()
                    PrgFrm.UpdateMe("Loading members", 0)
                    PrgFrm.Visible = True

                    ReDim MembersList(dsMemb.Tables(0).Rows.Count - 1)
                    For i = 0 To dsMemb.Tables(0).Rows.Count - 1
                        Dim Str As String
                        Dim StrArray As String()
                        Str = dsMemb.Tables(0).Rows(i).Item("ChildrenMembersIDs")
                        StrArray = Str.Split(",")
                        Cntrt = Cntrt + StrArray.Count
                        For Each MyStr In StrArray
                            If Not MyStr = "" Then
                                Dim Memb As New Member
                                Memb.LoadWithID(MyStr)
                                If j = 0 Then
                                    ReDim MembersList(j)
                                Else
                                    ReDim Preserve MembersList(j)
                                End If
                                MembersList(j) = Memb
                                j = j + 1
                            End If
                            PrgFrm.UpdateMe("Loading members ...", j, Cntrt)
                        Next
                        PrgFrm.UpdateMe("Loading members ...", (i + 1), dsMemb.Tables(0).Rows.Count)
                    Next

                    PrgFrm.Visible = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        Dim item As ListViewItem
        Dim NbMembers As Integer
        Dim Cntr As Integer
        Dim Ind As Integer

        If Not IsNothing(MembersList) Then
            NbMembers = MembersList.Count
        Else
            NbMembers = 0
        End If

        For Each item In lvwSNPs.Items
            Dim NdId As Integer
            For Cntr = 5 To NbMembers + 1 '+1 and not -1 to include the ref and the mutation alt call columns: 2 additional columns
                Dim Str As String

                Str = item.SubItems(Cntr).Text
                If Strings.Left(Str, 1) = "p" Then
                    'add that mutation to the member as putative mutation if not already there!
                    'get ID of the member - use Cntr as index of the column from MembersList
                    Ind = GetMutIndex(item.SubItems(1).Text)
                    If MembersList(Cntr - 5).HasPutativeMutation(MutationList(Ind).ID) = False Then
                        MembersList(Cntr - 5).AppendPutativeMutationsID(MutationList(Ind).ID)
                        MembersList(Cntr - 5).AppendMutationsID(MutationList(Ind).ID)
                    Else
                        'do nothing
                    End If
                ElseIf Str = "UndoP" Then
                    'remove that mutation from the mutation list and putativemutation list and get value back from variant
                    'get ID of the member - use Cntr as index of the column from MembersList
                    Ind = GetMutIndex(item.SubItems(1).Text)
                    If MembersList(Cntr - 5).HasPutativeMutation(MutationList(Ind).ID) = True Then
                        MembersList(Cntr - 5).AppendPutativeMutationsID(MutationList(Ind).ID)
                        MembersList(Cntr - 5).AppendMutationsID(MutationList(Ind).ID)
                    Else
                        'do nothing
                    End If
                End If
            Next

            If Not item.SubItems(0).Text = "No Node" Then
                Ind = GetMutIndex(item.SubItems(1).Text)
                If Not Ind = -1 Then
                    NdId = GetNodeIDfromName(item.SubItems(0).Text)
                    If Not NdId = -1 Then
                        If MutationList(Ind).CurrentParentNodeID = "" Then
                            Dim NewNd As New Node

                            NewNd.LoadWithID(NdId)

                            MutationList(Ind).CurrentParentNodeID = NdId
                            NewNd.AppendMutationsID(MutationList(Ind).ID)

                            NewNd.SavetoDB()
                            MutationList(Ind).SavetoDB()

                        ElseIf Not NdId = MutationList(Ind).CurrentParentNodeID Then
                            Dim NewNd As New Node
                            Dim OldNd As New Node

                            NewNd.LoadWithID(NdId)
                            OldNd.LoadWithID(MutationList(Ind).CurrentParentNodeID)

                            MutationList(Ind).CurrentParentNodeID = NdId
                            NewNd.AppendMutationsID(MutationList(Ind).ID)
                            OldNd.RemoveMutationID(MutationList(Ind).ID)

                            NewNd.SavetoDB()
                            OldNd.SavetoDB()
                            MutationList(Ind).SavetoDB()
                        End If
                    Else
                        MsgBox("No Node exists in the DB with Name: " & item.SubItems(0).Text & "!")
                    End If
                End If
            End If
        Next
    End Sub

    Private Function GetNodeIDfromName(NodName As String) As Integer
        Dim dsNode As DataSet

        dsNode = cDataAccess.GetNodeByName(NodName)
        If Not IsNothing(dsNode) Then
            If dsNode.Tables(0).Rows.Count > 0 Then
                If dsNode.Tables(0).Rows.Count = 1 Then
                    Return dsNode.Tables(0).Rows(0).Item("ID")
                Else 'multiple results!!
                    MsgBox("got more than 1 node id from nodename " & NodName & "!")
                    Return -1
                End If
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Private Function GetMutIndex(MutName As String) As Integer
        Dim i As Integer

        For i = 0 To MutationList.Count - 1
            If MutationList(i).AllNames = MutName Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub mnuReorder_Click(sender As Object, e As EventArgs) Handles mnuReorder.Click 'it doesn't work!
        Me.lvwSNPs.ListViewItemSorter = OriginalViewSorter
        Me.lvwSNPs.Sort()
    End Sub

    Private Sub btnAllowEdit_Click(sender As Object, e As EventArgs) Handles btnAllowEdit.Click
        If lvwSNPs.LabelEdit = True Then
            lvwSNPs.LabelEdit = False
            btnAllowEdit.Text = "Allow Node Name Edit"
        Else
            lvwSNPs.LabelEdit = True
            btnAllowEdit.Text = "Prevent Node Name Edit"
        End If
    End Sub

    Private Sub mnuSetToAltCallToolStrip_Click(sender As Object, e As EventArgs) Handles mnuSetToAltCallToolStrip.Click

        If lvwSNPs.Items.Count > 0 Then
            If Rowindex > -1 And Rowindex < lvwSNPs.Items.Count Then
                If Colindex > 4 And Colindex < lvwSNPs.Columns.Count Then
                    If lvwSNPs.Items(Rowindex).SubItems(Colindex).Text = "" Then
                        lvwSNPs.Items(Rowindex).SubItems(Colindex).Text = "p" & lvwSNPs.Items(Rowindex).SubItems(4).Text
                        btnSaveChanges.Enabled = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Function GetColumnAtMousePosition(ByVal pMouseX As Integer) As Integer
        Dim result As Integer = 0

        'Get column rights
        Dim colRights As New List(Of Integer)
        Dim colWidths As New List(Of Integer)
        For Each col As ColumnHeader In lvwSNPs.Columns
            colWidths.Add(col.Width)
            Dim colRight As Integer = 0 ' - pListView.Columns.Item(0).Width 'Subtract this if you were collecting lefts instead of rights
            For i As Integer = 0 To colWidths.Count - 1
                colRight += colWidths(i)
            Next
            colRights.Add(colRight)
        Next

        'Which column does the mouse X fall inside?
        Dim colIndex As Integer = 0
        For Each colRight As Integer In colRights
            If pMouseX <= colRight Then
                result = colIndex
                Exit For
            End If
            colIndex += 1
        Next

        Return result
    End Function

    Private Sub mnuRemoveAltCall_Click(sender As Object, e As EventArgs) Handles mnuRemoveAltCall.Click

        If lvwSNPs.Items.Count > 0 Then
            If Rowindex > -1 And Rowindex < lvwSNPs.Items.Count Then
                If Colindex > 4 And Colindex < lvwSNPs.Columns.Count Then
                    Dim Str As String
                    Str = lvwSNPs.Items(Rowindex).SubItems(Colindex).Text
                    If Strings.Left(Str, 1) = "p" Then
                        lvwSNPs.Items(Rowindex).SubItems(Colindex).Text = "UndoP"
                        btnSaveChanges.Enabled = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub mnuRenameMutation_Click(sender As Object, e As EventArgs) Handles mnuRenameMutation.Click

        If lvwSNPs.Items.Count > 0 Then
            If Rowindex > -1 And Rowindex < lvwSNPs.Items.Count Then
                If Colindex = 1 Then
                    Dim Str As String
                    Dim NewName As String
                    Str = lvwSNPs.Items(Rowindex).SubItems(Colindex).Text
                    NewName = InputBox("Enter new Mutation name", "New Mutation Name", Str)
                    If NewName = Str Then
                        MsgBox("no new name!")
                    ElseIf NewName = "" Then

                    Else
                        Dim Ind As Integer = GetMutIndex(Str)
                        MutationList(Ind).Name = {NewName}
                        MutationList(Ind).SavetoDB()
                        lvwSNPs.Items(Rowindex).SubItems(Colindex).Text = MutationList(Ind).AllNames
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub mnuRenameNode_Click(sender As Object, e As EventArgs) Handles mnuRenameNode.Click

        If lvwSNPs.Items.Count > 0 Then
            If Rowindex > -1 And Rowindex < lvwSNPs.Items.Count Then
                If Colindex = 0 Then
                    Dim NdName As String
                    Dim NewName As String
                    NdName = lvwSNPs.Items(Rowindex).SubItems(Colindex).Text
                    If Not NdName = "No Node" Then
                        NewName = InputBox("Enter new Node name", "New Node Name", NdName)
                        If NewName = NdName Then
                            MsgBox("no new name!")
                        ElseIf NewName = "" Then

                        Else
                            Dim Nd As New Node
                            Dim i As Integer
                            Nd.LoadWithName(NdName)
                            Nd.Name = NewName
                            Nd.SavetoDB()
                            lvwSNPs.Items(Rowindex).SubItems(Colindex).Text = NewName
                            For i = 0 To lvwSNPs.Items.Count - 1
                                If lvwSNPs.Items(i).SubItems(Colindex).Text = NdName Then lvwSNPs.Items(i).SubItems(Colindex).Text = NewName
                            Next
                        End If
                    End If
                End If
            End If
        End If




    End Sub
End Class