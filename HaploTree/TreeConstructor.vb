Imports HaploTree

Module TreeConstructor
    Private p_TreeRoot As Node
    Dim cDataAccess As New clsDataAccess

    Public Property TreeRoot As Node
        Get
            Return p_TreeRoot
        End Get
        Set(value As Node)
            p_TreeRoot = value
        End Set
    End Property

    Public Sub AnalyseVariantData(KitID As Integer)
        Dim NewMember As New Member()
        Dim HasMut As Boolean

        NewMember.LoadWithID(KitID)
        If NewMember.IsPlacedInTheTree = False Then
            If NewMember.Variant19Loaded = True Or NewMember.Variant38Loaded = True Then
                HasMut = SetAllMutationsIDs(NewMember) 'set values to newmember.MutationsIDs and newmember.privatemutationsIDs if not already done
            Else
                MsgBox("Member " & NewMember.Name & " has no variant file loaded")
            End If
        Else
            MsgBox("Member " & NewMember.Name & " is already placed in the tree")
            'what if new Variant file!

        End If
    End Sub

    Public Sub InsertNewKitInTree(KitID As Integer, RootNodeName As String) 'run this AFTER saving KitID's variant file data to the DB 
        Dim ParentNode As Node
        Dim CheckNode As Node
        Dim SplitNode As Boolean
        Dim NewMember As New Member()
        Dim MutId As String
        Dim HasPrivateMutations As Boolean

        NewMember.LoadWithID(KitID)
        If NewMember.IsPlacedInTheTree = False Then
            If NewMember.Variant19Loaded = True Or NewMember.Variant38Loaded = True Then
                If Not IsNothing(NewMember.MutationsIDs) Then
                    If NewMember.MutationsIDs.Count > 0 Then
                        'RootNodeName = "Root" 'to be updated when we give user the choice of a starting node
                        p_TreeRoot = GetNode(RootNodeName)
                        If IsNothing(p_TreeRoot) Then 'the provided name is not found in the DB
                            If NBNodesInDB() > 0 Then
                                If MsgBox("Houston we have a problem: The node " & RootNodeName & " is not found in the DB.\n Pick an other node?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    'write code to give the possibilty to the user to directly chose an existing node in the DB?
                                    'while waiting:
                                    Exit Sub
                                Else
                                    Exit Sub
                                End If
                            Else 'we have no node yet and need to create the first one
                                'create node
                                Dim NewNd As New Node()
                                NewNd.Name = RootNodeName
                                NewNd.MutationsIDs = NewMember.MutationsIDs
                                NewNd.AppendChildMemberID(NewMember.ID)
                                NewNd.SavetoDB()
                                NewMember.CurrentParentNodeID = NewNd.ID 'need to save NewNd first so that it gets an ID appointed
                                NewMember.SavetoDB()
                                For Each MutId In NewMember.MutationsIDs
                                    If Not MutId = "" Then
                                        Dim Mut As New Mutation
                                        Mut.Load(MutId)
                                        Mut.CurrentParentNodeID = NewNd.ID
                                        Mut.SavetoDB()
                                    End If
                                Next
                                Exit Sub
                            End If
                        Else
                            If p_TreeRoot.ID = 0 Then 'node not found in the DB
                                If NBNodesInDB() > 0 Then
                                    If MsgBox("Houston we have a problem: The node " & RootNodeName & " is not found in the DB.\n Pick an other node?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                        'write code to give the possibilty to the user to directly chose an existing node in the DB?
                                        'while waiting:
                                        Exit Sub
                                    Else
                                        Exit Sub
                                    End If
                                Else 'we have no node yet and need to create the first one
                                    'create node
                                    Dim NewNd As New Node()
                                    NewNd.Name = RootNodeName
                                    NewNd.MutationsIDs = NewMember.MutationsIDs
                                    NewNd.AppendChildMemberID(NewMember.ID)
                                    NewNd.SavetoDB()
                                    NewMember.CurrentParentNodeID = NewNd.ID 'need to save NewNd first so that it gets an ID appointed
                                    NewMember.SavetoDB()
                                    For Each MutId In NewMember.MutationsIDs
                                        If Not MutId = "" Then
                                            'Dim Mut As New Mutation
                                            'Mut.Load(MutId)
                                            cDataAccess.SetMutationParentNode(MutId, NewNd.ID)
                                            'Mut.SavetoDB()
                                        End If
                                    Next
                                    Exit Sub
                                End If
                            End If
                        End If

                        ParentNode = FindClosestExistingNodeDownward(NewMember, p_TreeRoot) 'the user provides an apriori start node to speed up the process
                        CheckNode = CheckNodesBelowParent(NewMember, ParentNode) ' check that there are no common mutations in the nodes below (this should help discarding the missing putative mutations)
                        While CheckNode.ID <> ParentNode.ID
                            ParentNode = CheckNode
                            CheckNode = CheckNodesBelowParent(NewMember, ParentNode)
                        End While


                        If IsNothing(ParentNode) Then
                            'investigate higher in the tree?
                            If MsgBox("Houston we have a problem: this kit can not be hanged on any node below " & p_TreeRoot.Name & "in the DB.\n Investigate higher?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                ParentNode = FindClosestExistingNodeUpward(NewMember, p_TreeRoot, p_TreeRoot.ID)
                                If IsNothing(ParentNode) Then
                                    MsgBox("Houston we have a problem: this kit can not be hanged on any node! Abborting!!")
                                    Exit Sub
                                End If
                            Else
                                Exit Sub
                            End If
                        Else
                            If ParentNode.ID = 0 Then
                                If MsgBox("Houston we have a problem: this kit can not be hanged on any node below " & p_TreeRoot.Name & "in the DB.\n Investigate higher?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    ParentNode = FindClosestExistingNodeUpward(NewMember, p_TreeRoot, p_TreeRoot.ID)
                                    If IsNothing(ParentNode) Then
                                        MsgBox("Houston we have a problem: this kit can not be hanged on any node! Abborting!!")
                                        Exit Sub
                                    End If
                                Else
                                    Exit Sub
                                End If
                            End If
                        End If

                        SplitNode = False
                        For Each MutId In ParentNode.MutationsIDs
                            'test if the node needs to be splitted
                            If Not MutId = "" Then
                                If NewMember.HasMutation(MutId) = False Then
                                    SplitNode = True
                                    Exit For
                                End If
                            End If
                        Next

                        If SplitNode = True Then ' need to split node: the new member and the children below this node do not share all the mutations at that node.
                            InsertNodeBelowWithNonCommonMutations(ParentNode, NewMember) 'creates a new node below parentnode with the noncommon mutations of ParentNode and newmember and remove them from parentnode mutationsIDs
                        End If

                        'if new memb has private mutations then add a node below parentnode. Otherwise add the member directly.
                        'need to add also all other mutations the member has that are not in any of the nodes before
                        'looking only at private mutation of the member is only valid for a new member added to the DB 
                        'but it doesn't work for a rebuild of a DB where private mutations have been set in a different context.
                        ' we could forget some mutations that previous members used to build the tree didn't have.
                        'in the CheckMemberMutations function 
                        ' we will loop on each mutations from the new member: if the mutation has no parent (and is not ignored) we will add it
                        ' to the member's parent node (if the member is alone under it), or to a new node below (moving the member under it).
                        If Not IsNothing(NewMember.PrivateMutationsIDs) Then
                            HasPrivateMutations = False
                            For Each MutId In NewMember.PrivateMutationsIDs
                                If Not MutId = "" Then
                                    HasPrivateMutations = True
                                    Exit For
                                End If
                            Next
                            If HasPrivateMutations = True Then ' need to add a node with all private mutations of the new member
                                Dim NwNode As Node
                                NwNode = AddPrivateNodeBelow(ParentNode, NewMember) 'creates a new node below parentnode with the private mutations of newmember

                                ParentNode.AppendChildNodeID(NwNode.ID)
                                NewMember.CurrentParentNodeID = NwNode.ID
                            Else 'newmember has no private mutations
                                ParentNode.AppendChildMemberID(NewMember.ID)
                                NewMember.CurrentParentNodeID = ParentNode.ID
                            End If
                        Else 'newmember has no private mutations
                            ParentNode.AppendChildMemberID(NewMember.ID)
                            NewMember.CurrentParentNodeID = ParentNode.ID
                        End If
                        ParentNode.SavetoDB()
                        NewMember.SavetoDB()
                        CheckMemberMutations(NewMember)
                        CheckTreeConsistency(NewMember, p_TreeRoot)
                    Else
                        MsgBox("Member " & NewMember.Name & " has no mutations loaded!\n Analyse Variant first")
                    End If
                Else
                    MsgBox("Member " & NewMember.Name & " has no mutations loaded!\n Analyse Variant first")
                End If
            Else
                MsgBox("Member " & NewMember.Name & " has no variant file loaded")
            End If
        Else
            MsgBox("Member " & NewMember.Name & " is already placed in the tree")
        End If
    End Sub

    Public Sub CheckMemberMutations(ByRef Memb As Member)
        ' we will loop on each mutations from the new member: if the mutation has no parent (and is not ignored) we will add it
        ' to the member's parent node (if the member is alone under it), or to a new node below (moving the member under it).
        Dim HasMutOnHisOwn As Boolean
        Dim MutID As String

        HasMutOnHisOwn = False
        For Each MutID In Memb.MutationsIDs ' check if mutation is ignored and if it is already affected to a node
            If MutationIsIgnored(MutID) = False And MutationHasNodeParent(MutID) = False Then
                HasMutOnHisOwn = True
                Exit For
            End If
        Next

        If HasMutOnHisOwn = True Then
            Dim FrmPrgss As New frmProgress
            Dim MutIDsList As String
            Dim IsFirst As Boolean
            Dim Nd As New Node
            Dim i As Integer
            IsFirst = True
            MutIDsList = ""

            FrmPrgss.InitiateMe()
            FrmPrgss.UpdateMe("Checking member's mutations ...", 0)
            FrmPrgss.Show()
            FrmPrgss.Visible = True
            i = 0

            Nd.LoadWithID(Memb.CurrentParentNodeID)
            If Not IsNothing(Nd.ChildrenMembersIDs) Then
                If Nd.ChildrenMembersIDs.Count > 1 Then 'if has brothers need to create a new node under member's parent node to which to attach the member and his mutations
                    Nd.LoadWithID(Memb.CurrentParentNodeID)
                    InsertEmptyNodeBelow(Nd, Memb)
                End If
            End If

            For Each MutID In Memb.MutationsIDs
                    If Not MutID = "" Then
                        If MutationIsIgnored(MutID) = False And MutationHasNodeParent(MutID) = False Then
                            If IsFirst = True Then
                                MutIDsList = MutID
                                IsFirst = False
                            Else
                                MutIDsList = MutIDsList & "," & MutID
                            End If
                            SetParentNodeToMutation(MutID, Memb.CurrentParentNodeID)
                            i = i + 1
                            FrmPrgss.UpdateMe("Checking member's mutations ...", i, Memb.MutationsIDs.Count)
                        End If
                    End If
                Next
                FrmPrgss.Visible = False
                AddMutationToNode(Memb.CurrentParentNodeID, MutIDsList)
            End If
    End Sub

    Private Function MemberHasBrother(Memb As Member) As Boolean
        Dim Nd As New Node

        Nd.LoadWithID(Memb.CurrentParentNodeID)
        If Nd.ChildrenMembersIDs.Count > 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub SetParentNodeToMutation(MutID As String, ParentNodeID As String)
        cDataAccess.SetMutationParentNode(MutID, ParentNodeID)
    End Sub

    Private Function MutationIsIgnored(MutID As String) As Boolean
        Return cDataAccess.IsMutationIgnored(MutID)
    End Function

    Private Function MutationHasNodeParent(MutID As String) As Boolean
        Dim ParentNodeds As DataSet
        ParentNodeds = cDataAccess.GetMutationParentNode(MutID)
        If Not IsNothing(ParentNodeds) Then
            If ParentNodeds.Tables(0).Rows.Count > 0 Then
                If ParentNodeds.Tables(0).Rows(0).IsNull("CurrentParentNodeID") = False Then
                    Dim str As String
                    str = Strings.Replace(ParentNodeds.Tables(0).Rows(0).Item("CurrentParentNodeID"), ",", "")
                    If Not str = "" Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub AddMutationToNode(NodeID, MutationsIDsToAdd)
        Dim MutationsIDsds As DataSet
        Dim MutationsIDs As String
        MutationsIDsds = cDataAccess.GetMutationIDsInNode(NodeID)
        If Not IsNothing(MutationsIDsds) Then
            If MutationsIDsds.Tables(0).Rows.Count > 0 Then
                If MutationsIDsds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    MutationsIDs = MutationsIDsds.Tables(0).Rows(0).Item("MutationsIDs")
                    MutationsIDs = MutationsIDs & "," & MutationsIDsToAdd
                Else
                    MutationsIDs = MutationsIDsToAdd
                End If
            Else
                MutationsIDs = MutationsIDsToAdd
            End If
        Else
            MutationsIDs = MutationsIDsToAdd
        End If
        cDataAccess.SetMutationIDsInNode(NodeID, MutationsIDs)
    End Sub

    Private Function CheckNodesBelowParent(ByRef Memb As Member, ByRef NodeToCheckBelow As Node) As Node
        Dim NodeToReturn As Node
        NodeToReturn = Nothing
        For Each NdId In NodeToCheckBelow.ChildrenNodesIDs
            If Not NdId = "" Then
                Dim NewNd As New Node()
                NewNd.LoadWithID(NdId)
                NodeToReturn = FindClosestExistingNodeDownward(Memb, NewNd) 'recursive call of this function
                If Not IsNothing(NodeToReturn) Then 'a node came back from lower levels so we found the right place and stop - no need to investigate parallel levels
                    Exit For
                End If
            End If
        Next
        If IsNothing(NodeToReturn) Then
            Return NodeToCheckBelow 'no node came back from all lower levels because all were different below. Since there is no diff at this level, the current node is the right one
        Else
            Return NodeToReturn 'we return upward the node that came from the lower level
        End If
    End Function

    Private Function NBNodesInDB() As Integer

        Return cDataAccess.GetNBNodesInDB
    End Function

    Private Sub InsertNodeBelowWithNonCommonMutations(ByRef ParNode As Node, ByRef NewMemb As Member) 'As Node 'creates a new node below parentnode with the noncommon mutations of parentnode and newmember, looping on the mutation of ParentNode
        Dim NewNode As New Node()
        Dim MutId As String
        Dim MbId As String
        Dim NdId As String
        Dim Found1Mut As Boolean
        Dim EmptyStrArray(0) As String
        Dim FrmPrgss As New frmProgress
        Dim i As Integer

        NewNode.ParentNodeID = ParNode.ID
        NewNode.ChildrenNodesIDs = ParNode.ChildrenNodesIDs 'add all node children of parent node to new node
        NewNode.ChildrenMembersIDs = ParNode.ChildrenMembersIDs 'add all member children of parent node to new node
        NewNode.SavetoDB() 'need to save to get the ID of the new node

        FrmPrgss.InitiateMe()
        FrmPrgss.UpdateMe("Checking Parentnode's mutations ...", 0)
        FrmPrgss.Show()
        FrmPrgss.Visible = True
        i = 0

        Found1Mut = False
        If Not IsNothing(ParNode.MutationsIDs) Then
            For Each MutId In ParNode.MutationsIDs 'add mutations that the new memb doesn't have
                If Not MutId = "" Then
                    If NewMemb.HasMutation(MutId) = False Then
                        'Dim Mut As New Mutation
                        'Mut.Load(MutId)
                        'Mut.CurrentParentNodeID = NewNode.ID ' consider doing a single change directly in the DB rather than loading the mutation if not needed
                        'Mut.SavetoDB()
                        cDataAccess.SetMutationParentNode(MutId, NewNode.ID)
                        NewNode.AppendMutationsID(MutId)
                        ParNode.RemoveMutationID(MutId)
                        If Found1Mut = False Then
                            NewNode.Name = cDataAccess.GetMutationNames(MutId) '  Mut.Name(0) 'find a better way to give a default name to a node, or ensure that position 0 is not empty!
                            Found1Mut = True
                        End If
                    Else
                        'Dim Mut As New Mutation
                        'Mut.Load(MutId)
                        'Mut.IsPrivate = False
                        'Mut.SavetoDB()
                        'The mutation is not private anymore
                        cDataAccess.SetMutationToNOTPrivate(MutId)
                        'Remove from Private Mutations for the member below
                        If Not IsNothing(NewNode.ChildrenMembersIDs) Then
                            For Each MbId In NewNode.ChildrenMembersIDs 'move all member children under newnode
                                If Not MbId = "" Then
                                    RemovePrivateMutationFromMember(MutId, MbId)
                                End If
                            Next
                        End If
                    End If
                End If
                i = i + 1
                FrmPrgss.UpdateMe("Checking Parentnode's mutations ...", i, ParNode.MutationsIDs.Count)
            Next
        End If
        FrmPrgss.Visible = False
        If ParNode.Name = NewNode.Name Then 'this means the new node has got the mutation that gave its name to parent. The parent has not this muation anymore so need new name
            ParNode.Name = "Change Me: " & NewNode.Name
        End If

        If Not IsNothing(NewNode.ChildrenMembersIDs) Then
            For Each MbId In NewNode.ChildrenMembersIDs 'move all member children under newnode
                If Not MbId = "" Then
                    'Dim Mb As New Member()
                    'Mb.LoadWithID(MbId)
                    'Mb.CurrentParentNodeID = NewNode.ID ' consider doing a single change directly in the DB rather than loading the member if not needed
                    'Mb.SavetoDB()
                    cDataAccess.SetMemberParentNode(MbId, NewNode.ID)
                End If
            Next
        End If
        If Not IsNothing(NewNode.ChildrenNodesIDs) Then
            For Each NdId In NewNode.ChildrenNodesIDs 'move all node children under newnode
                If Not NdId = "" Then
                    'Dim Nd As New Node()
                    'Nd.LoadWithID(NdId)
                    'Nd.ParentNodeID = NewNode.ID ' consider doing a single change directly in the DB rather than loading the node if not needed
                    'Nd.SavetoDB()
                    cDataAccess.SetNodeParentNode(NdId, NewNode.ID)
                End If
            Next
        End If
        ParNode.ChildrenMembersIDs = EmptyStrArray
        ParNode.ChildrenNodesIDs = EmptyStrArray
        ParNode.AppendChildNodeID(NewNode.ID)

        ParNode.SavetoDB()
        NewNode.SavetoDB()
        'Return NewNode
    End Sub

    Private Sub RemovePrivateMutationFromMember(MutID As String, MembID As String)
        Dim PrivateMut As String
        Dim NewPrivateMut As String
        Dim ds As DataSet

        ds = cDataAccess.GetMemberByID(MembID)
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).IsNull("PrivateMutationsIDs") = False Then
                    PrivateMut = ds.Tables(0).Rows(0).Item("PrivateMutationsIDs")
                    NewPrivateMut = Strings.Replace(PrivateMut, "," & MutID, "")
                    cDataAccess.SetMemberPrivateMutations(MembID, NewPrivateMut)
                End If
            End If
        End If


    End Sub

    Private Sub InsertEmptyNodeBelow(ByRef ParNode As Node, ByRef NewMemb As Member) 'As Node 'creates an empty new node (without mutations) below parentnode, but rearanging child members
        Dim NewNode As New Node()
        Dim EmptyStrArray(0) As String

        NewNode.ParentNodeID = ParNode.ID
        NewNode.ChildrenNodesIDs = ParNode.ChildrenNodesIDs 'add all node children of parent node to new node
        NewNode.ChildrenMembersIDs = ParNode.ChildrenMembersIDs 'add all member children of parent node to new node
        NewNode.SavetoDB() 'need to save to get the ID of the new node

        NewNode.Name = "Node - Member ID: " & NewMemb.ID

        If Not IsNothing(NewNode.ChildrenMembersIDs) Then
            For Each MbId In NewNode.ChildrenMembersIDs 'move all member children under newnode
                If Not MbId = "" Then
                    'Dim Mb As New Member()
                    'Mb.LoadWithID(MbId)
                    'Mb.CurrentParentNodeID = NewNode.ID ' consider doing a single change directly in the DB rather than loading the member if not needed
                    'Mb.SavetoDB()
                    cDataAccess.SetMemberParentNode(MbId, NewNode.ID)
                End If
            Next
        End If
        If Not IsNothing(NewNode.ChildrenNodesIDs) Then
            For Each NdId In NewNode.ChildrenNodesIDs 'move all node children under newnode
                If Not NdId = "" Then
                    'Dim Nd As New Node()
                    'Nd.LoadWithID(NdId)
                    'Nd.ParentNodeID = NewNode.ID ' consider doing a single change directly in the DB rather than loading the node if not needed
                    'Nd.SavetoDB()
                    cDataAccess.SetNodeParentNode(NdId, NewNode.ID)
                End If
            Next
        End If
        ParNode.ChildrenMembersIDs = EmptyStrArray
        ParNode.ChildrenNodesIDs = EmptyStrArray
        ParNode.AppendChildNodeID(NewNode.ID)

        ParNode.SavetoDB()
        NewNode.SavetoDB()
        'Return NewNode
    End Sub

    Private Function AddPrivateNodeBelow(ByRef ParNode As Node, ByRef NewMemb As Member) As Node 'creates a New node below parentnode With the private mutations of newmember

        Dim NewNode As New Node()
        Dim MutId As String
        Dim Found1Mut As Boolean
        Dim EmptyStrArray(0) As String
        Dim i As Integer
        Dim FrmPrgss As New frmProgress

        NewNode.ParentNodeID = ParNode.ID
        NewNode.ChildrenNodesIDs = EmptyStrArray
        NewNode.AppendChildMemberID(NewMemb.ID)
        NewNode.SavetoDB() 'need to save to get the ID of the new node

        FrmPrgss.InitiateMe()
        FrmPrgss.UpdateMe("Checking member's Private mutations ...", 0)
        FrmPrgss.Show()
        FrmPrgss.Visible = True
        i = 0

        Found1Mut = False
        If Not IsNothing(NewMemb.PrivateMutationsIDs) Then
            For Each MutId In NewMemb.PrivateMutationsIDs 'add private mutations of the new memb
                If Not MutId = "" Then
                    'Dim Mut As New Mutation
                    'Mut.Load(MutId)
                    'Mut.CurrentParentNodeID = NewNode.ID ' consider doing a single change directly in the DB rather than loading the mutation if not needed
                    cDataAccess.SetMutationParentNode(MutId, NewNode.ID)
                    'Mut.IsPrivate = True
                    cDataAccess.SetMutationToPrivate(MutId)
                    'Mut.SavetoDB()
                    NewNode.AppendMutationsID(MutId)
                    If Found1Mut = False Then
                        NewNode.Name = cDataAccess.GetMutationNames(MutId) '  Mut.Name(0) 'find a better way to give a default name to a node, or ensure that position 0 is not empty!
                        Found1Mut = True
                    End If
                End If
                i = i + 1
                FrmPrgss.UpdateMe("Checking member's Private mutations ...", i, NewMemb.PrivateMutationsIDs.Count)
            Next
        End If
        FrmPrgss.Visible = False
        NewNode.SavetoDB()
        Return NewNode
    End Function

    Private Function GetNode(Nodename As String) As Node
        Dim newNode As New Node

        newNode.LoadWithName(Nodename)
        Return newNode
    End Function


    Private Function FindClosestExistingNodeDownward(ByRef Memb As Member, ByRef CurrentNode As Node) As Node 'find and return the closest Node in the current tree starting from a given node
        'recursive process starting from the CurrentNode to check if Memb has the wright mutation defining the nodes
        'we stop if he is missing at least one.
        'we return current node if he has at least 1 of the mutations (we will need to split that node)
        'we return the parentnode if he has none of the mutation for any children of that parent
        Dim MutId As String
        Dim HasStopped As Boolean
        Dim AtLeastOneSimilar As Boolean
        Dim NdId As String

        HasStopped = False
        AtLeastOneSimilar = False
        If Not IsNothing(CurrentNode.MutationsIDs) Then 'a proper node shall always have at least 1 mutation
            For Each MutId In CurrentNode.MutationsIDs
                If Memb.HasMutation(MutId) = False Then 'we found a difference
                    HasStopped = True 'this doesn't work for the highest node because there is always some missing mutations on the highest nodes
                Else
                    AtLeastOneSimilar = True
                End If
            Next
            If HasStopped = True Then ' we found at least 1 diff
                If AtLeastOneSimilar = True Then 'this doesn't work for the highest node because there is always some missing mutations on the highest nodes
                    Return CurrentNode 'we found at least 1 similar and 1 difference so we return this node ... will have to split it!
                Else
                    Return Nothing 'all mutations were different so we will have to return its parent or a parrallel node.
                End If
            Else 'we found no difference at that level and need to investigate the levels below
                Dim NodeToReturn As Node
                NodeToReturn = Nothing
                For Each NdId In CurrentNode.ChildrenNodesIDs
                    Dim NewNd As New Node()
                    NewNd.LoadWithID(NdId)
                    NodeToReturn = FindClosestExistingNodeDownward(Memb, NewNd) 'recursive call of this function
                    If Not IsNothing(NodeToReturn) Then 'a node came back from lower levels so we found the right place and stop - no need to investigate parallel levels
                        Exit For
                    End If
                Next
                If IsNothing(NodeToReturn) Then
                    Return CurrentNode 'no node came back from all lower levels because all were different below. Since there is no diff at this level, the current node is the right one
                Else
                    Return NodeToReturn 'we return upward the node that came from the lower level
                End If
            End If
        Else 'the node has not been initialised with any mutationIDs
            MsgBox("Node " & CurrentNode.Name & " has no mutations")
            Return Nothing
        End If

    End Function

    Private Function FindClosestExistingNodeUpward(ByRef Memb As Member, ByRef CurrentNode As Node, SendingChildID As integer) As Node 'find and return the closest Node in the current tree starting from a given node
        'recursive process starting from the CurrentNode's parent investigating parralel nodes to check if Memb has the wright mutation defining the nodes
        'check currentnode has no mutationIDs matching
        'check parent ... if non are matching check parent recursivelly, if all matching, check paralell nodes. stop if at least one matching but not all.

        Dim MutId As String
        Dim HasStopped As Boolean
        Dim AtLeastOneDiff As Boolean
        Dim NdId As String

        HasStopped = False
        AtLeastOneDiff = False
        If Not IsNothing(CurrentNode.MutationsIDs) Then 'a proper node shall always have at least 1 mutation
            For Each MutId In CurrentNode.MutationsIDs
                If Memb.HasMutation(MutId) = True Then 'we found one matching
                    HasStopped = True
                Else
                    AtLeastOneDiff = True
                End If
            Next
            If HasStopped = True Then ' we found at least 1 similar
                If AtLeastOneDiff = True Then
                    Return CurrentNode 'we found at least 1 similar and 1 difference so we return this node ... will have to split it!
                Else
                    'all mutations were similar so we will have to return this node or one of its child but not the child we are coming from since it was already investigated
                    Dim NodeToReturn As Node
                    NodeToReturn = Nothing
                    For Each NdId In CurrentNode.ChildrenNodesIDs
                        If Not NdId = SendingChildID Then ' we do not re-investigate the original sending child
                            Dim NewNd As New Node()
                            NewNd.LoadWithID(NdId)
                            NodeToReturn = FindClosestExistingNodeDownward(Memb, NewNd) 'recursive call of this function
                            If Not IsNothing(NodeToReturn) Then 'a node came back from lower levels so we found the right place and stop - no need to investigate parallel levels
                                Exit For
                            End If
                        End If
                    Next
                    If IsNothing(NodeToReturn) Then
                        Return CurrentNode 'no node came back from all lower levels because all were different below. Since there is no diff at this level, the current node is the right one
                    Else
                        Return NodeToReturn 'we return upward the node that came from the lower level
                    End If
                End If
            Else 'we found none similar and need to investigate updward but not current node again!
                If CurrentNode.ParentNodeID = "" Then
                    MsgBox("We arrived to the top of the tree without finding any node!")
                    Return Nothing
                Else
                    Dim ParNode As New Node()
                    ParNode.LoadWithID(CurrentNode.ParentNodeID)
                    Return FindClosestExistingNodeUpward(Memb, ParNode, CurrentNode.ID) 'recursive call of this function
                End If
            End If
        Else 'the node has not been initialised with any mutationIDs
            If CurrentNode.ParentNodeID = "" Then
                MsgBox("Node " & CurrentNode.Name & " has no mutations and no parent!")
                Return Nothing
            Else
                MsgBox("Node " & CurrentNode.Name & " has no mutations")
                Dim ParNode As New Node()
                ParNode.LoadWithID(CurrentNode.ParentNodeID)
                Return FindClosestExistingNodeUpward(Memb, ParNode, CurrentNode.ID) 'recursive call of this function
            End If
        End If
    End Function

    Private Sub CheckTreeConsistency(Memb As Member, RootNode As Node)
        'check that Memb has no other mutations that would contradict the position he has just been assigned both
        '- higher in the tree
        '- lower in the tree
        ' we have to do so because we start with an appriori tree already existing and potentially wrong
        ' will define Memb.putativemutationsIDs (has to be positive to mutations higher in the tree ... choice to be made case by case by user)
        ' will also review all mutations with isprivate = true and check if it still is the case 
        ' load also all positions for all members on the newly created private mutations
    End Sub

    Private Function SetAllMutationsIDs(ByRef NewMemb As Member) As Boolean 'set values to newmemb.MutationsIDs and newmemb.PrivatemutationsIDs if not already defined
        'putative mutations will be defined when placing the kit in the tree if needed - manual work though
        'iterate from the variant list
        'returns false if has no mutations
        Dim i, NbItems, NbMut, NbPrivate As Integer
        Dim NewMutationsIDsArray(1) As String
        Dim NewPrivateMutationsIDsArray(1) As String
        Dim Success As Boolean
        Dim UsingHg38 As Boolean
        Dim FrmPrgss As New frmProgress

        Success = True
        UsingHg38 = True

        If Not IsNothing(NewMemb.MutationsIDs) Then
            'ask if user wants to review list of mutations - in case a new variant file has been loaded
            'do it
            If MsgBox("This kit has already mutations defined.\n Do you want to to review its list of mutation?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return Success
            Else
                Dim EmptyStrArray(0) As String
                NewMemb.MutationsIDs = EmptyStrArray
                NewMemb.PrivateMutationsIDs = EmptyStrArray
                NewMemb.PutativeMutationsIDs = EmptyStrArray
                NewMemb.SavetoDB()
            End If
        End If

        If NewMemb.Variant38Loaded = True Then
            NbItems = NewMemb.NbVariant38 'return number of rows in the Hg38 variant loaded in the DB for this member

        ElseIf NewMemb.Variant19Loaded = True Then
            NbItems = NewMemb.NbVariant19 'return number of rows in the Hg19 variant loaded in the DB for this member
            UsingHg38 = False
        Else
            Success = False
            Return Success
        End If

        NbPrivate = 0
        NbMut = 0

        FrmPrgss.InitiateMe()
        FrmPrgss.UpdateMe("Analysing member's mutations ...", 0)
        FrmPrgss.Show()
        FrmPrgss.Visible = True


        For i = 0 To NbItems - 1
            Dim ID As Integer
            Dim Position38 As String
            Dim ReferenceCall As String
            Dim AlternateCall As String

            If UsingHg38 = True Then
                Position38 = NewMemb.GetPositionHg38AtRow(i) 'Pass here the posistion at row i 
                ReferenceCall = NewMemb.GetRefCallHg38AtRow(i) 'Pass here the refCall at row i
                AlternateCall = NewMemb.GetAltCallHg38AtRow(i) 'Pass here the altCall at row i
            Else
                Position38 = GetHg38FromHg19(NewMemb.GetPositionHg19AtRow(i)) 'Pass here the posistion at row i and transform from hg19 to hg38
                ReferenceCall = NewMemb.GetRefCallHg19AtRow(i) 'Pass here the refCall at row i
                AlternateCall = NewMemb.GetAltCallHg19AtRow(i) 'Pass here the altCall at row i
            End If
            If Not ReferenceCall = AlternateCall Then
                ID = GetMutationIDFromDB(Position38, ReferenceCall, AlternateCall) 'Check in the DB if mutation exists and returns its ID if so, 0 if not.
                If ID = 0 Then 'the mutation in this item's position didn't exist in the DB
                    ID = CreateNewMutationInDB(Position38, ReferenceCall, AlternateCall) 'Creates a new mutation in the mutation table and return its allocated ID if valid, 0 if not.
                    If Not ID = 0 Then 'this is a valid new mutation
                        NbPrivate = NbPrivate + 1
                        NewMemb.AppendPrivateMutationsID(ID)
                        NbMut = NbMut + 1
                        NewMemb.AppendMutationsID(ID)
                    Else
                        'it is not a valid mutation so no need to save it to the DB
                    End If
                Else 'the mutation in this item's position did exist in the DB
                    NbMut = NbMut + 1
                    NewMemb.AppendMutationsID(ID)
                End If
            Else
                'ref = alt no mutation
            End If
            FrmPrgss.UpdateMe("Analysing member's mutations ...", i + 1, NbItems)
        Next
        FrmPrgss.Visible = False
        NewMemb.SavetoDB()
        If NbMut = 0 Then Success = False
        Return Success
    End Function

    Private Function GetMutationIDFromDB(Pos38 As String, RefCall As String, altCall As String) As Integer 'Check in the DB if mutation exists and returns its ID if so, "" if not.
        Dim MutID As Integer
        Dim PosID As Integer

        PosID = PositionExistsInDB(Pos38, RefCall)
        If PosID = 0 Then 'position doesn't exist in the DB, then the mutation can not exist!
            MutID = 0
        Else ' the position exists
            MutID = MutationExistsInDB(PosID, altCall)
        End If
        Return MutID
    End Function

    Private Function PositionExistsInDB(Position38 As String, Reference38 As String) As Integer 'check if position exist in the DB - needs also to be same ref ... otherwise we have an issue!!
        '- return its ID if exists,  0 if Not
        Dim cDataAccess As New clsDataAccess
        Dim ds As DataSet

        ds = cDataAccess.GetPositionByPosHg38(Position38)
        If IsNothing(ds) = True Then
            Return 0
        Else
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).IsNull("AncestrallCall") = False Then
                    Dim ref As String
                    ref = ds.Tables(0).Rows(0).Item("AncestrallCall")
                    If Not ref = Reference38 Then
                        MsgBox("AncestrllCall Mismatch at position " & Position38 & "!")
                        Return 0
                    Else
                        Return ds.Tables(0).Rows(0).Item("ID")
                    End If
                Else
                    MsgBox("This Position has no Ancestral Call!")
                    Return 0
                End If
            Else
                Return 0
            End If
        End If

    End Function

    Private Function MutationExistsInDB(PositionID As Integer, alternCall As String) As Integer 'check if Mutation exists in the DB 
        '- return its ID if yes, 0 if not.
        ' needs to be same altCall otherwise it is a new mutation
        Dim cDataAccess As New clsDataAccess
        Dim ds As DataSet

        ds = cDataAccess.GetMutationByPosAndAltCall(PositionID, alternCall)
        If IsNothing(ds) = True Then 'the mutation doesn't exists
            Return 0
        Else
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).IsNull("AltCall") = False Then
                    Dim Alt As String
                    Alt = ds.Tables(0).Rows(0).Item("AltCall")
                    If Not Alt = alternCall Then
                        MsgBox("AltCall Mismatch at mutation ID" & ds.Tables(0).Rows(0).Item("ID") & "!") 'should not happen!
                        Return 0
                    Else
                        Return ds.Tables(0).Rows(0).Item("ID")
                    End If
                Else
                    MsgBox("This Mutation has no Alt Call!") 'impossible but but!
                    Return 0
                End If
            Else 'the mutation doesn't exists
                Return 0
            End If
        End If
    End Function

    Private Function CreateNewMutationInDB(Pos38 As String, RefCall As String, altCall As String) As Integer 'Creates a new mutation in the mutation table and return its allocated ID
        Dim MutID As Integer
        Dim PosID As Integer

        PosID = PositionExistsInDB(Pos38, RefCall)
        If PosID = 0 Then 'position doesn't exist in the DB, we need to create one
            If CheckCall(RefCall) = True Then
                Dim NewPos As New Position()
                NewPos.PosHg38 = Pos38
                NewPos.PosHg19 = GetHg19FromHg38(Pos38)
                NewPos.AncestrallCall = RefCall
                NewPos.SavetoDB()
                PosID = NewPos.ID
            Else ' not a proper reference, do not create a position and return ""
                Return ""
            End If
        End If
        MutID = MutationExistsInDB(PosID, altCall) 'checks if exists ... just in case!
        If MutID = 0 Then
            If CheckCall(altCall) = True Then
                Dim NewMut As New Mutation()
                NewMut.PositionID = PosID
                NewMut.AltCall = altCall
                NewMut.AppendName("temp_" & Pos38)
                NewMut.IsPrivate = True
                NewMut.SavetoDB()
                MutID = NewMut.ID
            Else ' not a proper altcall, do not create a position and return ""
                MsgBox("Not a proper call, mutation not created")
            End If
        Else
            MsgBox("mutation exists already, no need to create!")
        End If
        Return MutID
    End Function

    Private Function CheckCall(CallToCheck As String) As Boolean
        'check CallToCheck vs validcall table in DB
        Dim cDataAccess As New clsDataAccess
        If cDataAccess.CheckValidCallInDB(CallToCheck) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetHg19FromHg38(Position38 As String) As String
        Dim Pos19 As Long
        'Get it from Position38
        Pos19 = ConvertHG38ToHG19(CLng(Position38))
        Return Pos19
    End Function

    Public Function GetHg38FromHg19(Position19 As String) As String
        Dim Pos38 As Long
        'Get it from Position19
        Pos38 = ConvertHG19ToHG38(CLng(Position19))
        Return Pos38
    End Function


    Public Sub UpdatePrivateMutationsIDs(Memb As Member) 'Check all mutationsIDs of Memb vs. all other members in VariantDB
        'get list of membersIDs

        'loop on each Memb.MutationsIDs and check mutations of each other membersIDs, stops if found at least 1, if nonem set mutationID in PrivateMutationsIDs list.

    End Sub

    Public Sub LoadMutationsFromBigYHg19DB()
        Dim cDataAccess As New clsDataAccess
        Dim ds As New DataSet
        Dim i As Integer
        Dim NbMembers As Integer

        ds = cDataAccess.GetAllBigYHg19Mutations()
        If Not IsNothing(ds) Then 'the db is not empty
            If ds.Tables(0).Rows.Count > 0 Then
                NbMembers = ds.Tables(0).Columns.Count - 4
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).IsNull("ID") = False Then
                        Dim IsMut As Boolean
                        Dim PosHg19 As String
                        Dim PosHg38 As String
                        Dim SNPname As String
                        Dim ref As String
                        Dim rowID As Integer
                        Dim MutID As Integer
                        Dim MembKitnb As String
                        Dim read As String

                        IsMut = False

                        PosHg19 = ds.Tables(0).Rows(i).Item("PosHg19")
                        SNPname = ds.Tables(0).Rows(i).Item("SNPname")
                        ref = ds.Tables(0).Rows(i).Item("Reference")
                        rowID = ds.Tables(0).Rows(i).Item("ID")
                        read = ""

                        'test if a real mutation
                        If ref.Length = 1 Then 'we want only 1 base as reference
                            Dim j As Integer

                            For j = 0 To NbMembers - 1
                                'test if exists at least 1 read different from ref
                                MembKitnb = ds.Tables(0).Columns(j + 3).ColumnName
                                If Not IsDBNull(ds.Tables(0).Rows(i).Item(j + 3)) Then
                                    read = ds.Tables(0).Rows(i).Item(j + 3)
                                Else
                                    read = ""
                                End If

                                If read.Contains("PASS") = True Then
                                    If Not ref = Left(read, 1) Then
                                        IsMut = True
                                        Exit For
                                    End If
                                End If
                            Next
                        End If

                        'if a real mutation create and save mutation in the DB
                        If IsMut = True Then
                            PosHg38 = GetHg38FromHg19(PosHg19)
                            If PosHg38 = -999 Then
                                'MsgBox("position " & PosHg19 & " gives -999 in hg38")
                            Else
                                MutID = GetMutationIDFromDB(PosHg38, ref, Left(read, 1)) 'Check in the DB if mutation exists and returns its ID if so, 0 if not.
                                If MutID = 0 Then 'the mutation in this item's position didn't exist in the DB
                                    MutID = CreateNewMutationInDB(PosHg38, ref, Left(read, 1)) 'Creates a new mutation in the mutation table and return its allocated ID if valid, 0 if not.
                                    Dim newMut As New Mutation
                                    Dim Pos As New Position
                                    newMut.Load(MutID)
                                    Pos.LoadWithID(newMut.PositionID)

                                    If Not SNPname = "N/A" Then
                                        newMut.Name = SNPname.Split(",")
                                    Else
                                        newMut.AppendName("temp_" & Pos.PosHg38)
                                    End If

                                    If Not PosHg19 = Pos.PosHg19 Then
                                        MsgBox("position " & PosHg19 & " transfers int hg38 but gives back something different")
                                    End If
                                    newMut.SavetoDB()
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End If

    End Sub

    Public Function GetAllMutationsIDs() As Integer()
        Dim cDataAccess As New clsDataAccess
        Dim ds As New DataSet
        Dim i As Integer

        ds = cDataAccess.GetAllMutations
        If Not IsNothing(ds) Then 'the db is not empty
            If ds.Tables(0).Rows.Count > 0 Then
                Dim IntArray(ds.Tables(0).Rows.Count - 1) As Integer

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    IntArray(i) = ds.Tables(0).Rows(i).Item("ID")
                Next

                Return IntArray
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If

    End Function

    Public Sub LoadMembersFromBigYHg19DB()
        Dim cDataAccess As New clsDataAccess
        Dim ds As New DataSet
        Dim i As Integer
        Dim NbMembers As Integer

        ds = cDataAccess.GetAllBigYHg19Mutations()
        If Not IsNothing(ds) Then 'the db is not empty
            If ds.Tables(0).Rows.Count > 0 Then
                Dim PosHg19 As String
                Dim PosHg38 As String
                Dim ref As String
                Dim MutID As Integer
                Dim MembKitnb As String
                Dim read As String
                Dim j As Integer

                NbMembers = ds.Tables(0).Columns.Count - 4
                For j = 0 To NbMembers - 1
                    Dim NewMemb As New Member
                    Dim MembDs As New DataSet
                    'test if exists
                    MembKitnb = ds.Tables(0).Columns(j + 3).ColumnName
                    MembDs = cDataAccess.GetMemberByFTDNAID(MembKitnb)
                    If Not IsNothing(MembDs) Then
                        If MembDs.Tables(0).Rows.Count > 0 Then
                            'member exists already - do nothing

                        Else
                            'create new member.
                            cDataAccess.InsertMember("Name " & MembKitnb, MembKitnb, "")
                        End If
                    Else
                        'create new member.
                        cDataAccess.InsertMember("Name " & MembKitnb, MembKitnb, "")
                    End If

                    NewMemb.LoadWithFTDNAID(MembKitnb)

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        If ds.Tables(0).Rows(i).IsNull("ID") = False Then
                            ref = ds.Tables(0).Rows(i).Item("Reference")

                            'test if a real mutation
                            If ref.Length = 1 Then 'we want only 1 base as reference
                                PosHg19 = ds.Tables(0).Rows(i).Item("PosHg19")
                                read = ""
                                If Not IsDBNull(ds.Tables(0).Rows(i).Item(j + 3)) Then
                                    read = ds.Tables(0).Rows(i).Item(j + 3)
                                Else
                                    read = ""
                                End If

                                If read.Contains("PASS") = True Then
                                    If Not ref = Left(read, 1) Then 'the member has a mutation
                                        PosHg38 = GetHg38FromHg19(PosHg19)
                                        If PosHg38 = -999 Then
                                            'MsgBox("position " & PosHg19 & " gives -999 in hg38")
                                        Else
                                            MutID = GetMutationIDFromDB(PosHg38, ref, Left(read, 1)) 'Check in the DB if mutation exists and returns its ID if so, 0 if not.
                                            If Not MutID = 0 Then 'the mutation in this item's position didn't exist in the DB
                                                NewMemb.AppendMutationsID(MutID)
                                            End If
                                        End If

                                    End If
                                End If
                            End If

                        End If
                    Next
                    NewMemb.SavetoDB()
                Next
            End If
        End If
    End Sub

    Public Sub LoadMembersVariantFromBigYHg19DB()
        Dim cDataAccess As New clsDataAccess
        Dim ds As New DataSet
        Dim i As Integer
        Dim NbMembers As Integer

        ds = cDataAccess.GetAllBigYHg19Mutations()
        If Not IsNothing(ds) Then 'the db is not empty
            If ds.Tables(0).Rows.Count > 0 Then
                Dim PosHg19 As String
                Dim ref As String
                Dim MembKitnb As String
                Dim read As String
                Dim j As Integer

                NbMembers = ds.Tables(0).Columns.Count - 4
                For j = 0 To NbMembers - 1
                    Dim NewMembID As Integer
                    Dim MembDs As New DataSet
                    'test if exists
                    MembKitnb = ds.Tables(0).Columns(j + 3).ColumnName
                    MembDs = cDataAccess.GetMemberByFTDNAID(MembKitnb)
                    If Not IsNothing(MembDs) Then
                        If MembDs.Tables(0).Rows.Count > 0 Then
                            NewMembID = MembDs.Tables(0).Rows(0).Item("ID")
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).IsNull("ID") = False Then
                                    ref = ds.Tables(0).Rows(i).Item("Reference")

                                    If ref.Length = 1 Then 'we want only 1 base as reference
                                        PosHg19 = ds.Tables(0).Rows(i).Item("PosHg19")
                                        read = ""
                                        If Not IsDBNull(ds.Tables(0).Rows(i).Item(j + 3)) Then
                                            read = ds.Tables(0).Rows(i).Item(j + 3)
                                        Else
                                            read = ""
                                        End If

                                        If read.Contains("PASS") = True Then
                                            cDataAccess.InsertPositionByMemberID19(NewMembID, PosHg19, ref, read.Replace("PASS", ""), "PASS", read)
                                        ElseIf read.Contains("COVERED") = True Then
                                            cDataAccess.InsertPositionByMemberID19(NewMembID, PosHg19, ref, ref, "COVERED", "COVERED")
                                        ElseIf read.Contains("REJECTED") = True Then
                                            cDataAccess.InsertPositionByMemberID19(NewMembID, PosHg19, ref, read.Replace("REJECTED", ""), "REJECTED", read)
                                        Else

                                        End If
                                    End If

                                End If
                            Next
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub FindPrivateMutationsFromBigYHg19DB()
        Dim cDataAccess As New clsDataAccess
        Dim ds As New DataSet
        Dim i As Integer

        ds = cDataAccess.GetAllMutations()
        If Not IsNothing(ds) Then 'the db is not empty
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).IsNull("ID") = False Then
                        Dim MutID As String
                        Dim Mut As New Mutation
                        Dim Pos As New Position
                        Dim NbMembWithMut As Integer
                        Dim MembDs As New DataSet

                        MutID = ds.Tables(0).Rows(i).Item("ID")
                        Mut.Load(MutID)
                        Pos.LoadWithID(Mut.PositionID)

                        MembDs = cDataAccess.GetMembersWithHG19Variants(Pos.PosHg19, Pos.AncestrallCall, Mut.AltCall)
                        If Not IsNothing(MembDs) Then
                            NbMembWithMut = MembDs.Tables(0).Rows.Count
                            If NbMembWithMut > 1 Then
                                Mut.IsPrivate = False
                                Mut.SavetoDB()
                            ElseIf NbMembWithMut > 0 Then
                                'it is a private mutation
                                Mut.IsPrivate = True
                                Mut.SavetoDB()
                                Dim Memb As New Member
                                Memb.LoadWithID(MembDs.Tables(0).Rows(0).Item("FK_MemberID"))
                                Memb.AppendPrivateMutationsID(MutID)
                                Memb.SavetoDB()
                            Else
                                'that should not happen: it means no one has the mutation

                            End If
                        End If


                    End If
                Next
            End If
        End If
    End Sub

    Public Sub LoadMembersDetailsFromBigYHg19DB()
        Dim cDataAccess As New clsDataAccess
        Dim ds As New DataSet
        Dim i As Integer

        ds = cDataAccess.GetAllDetailsMemberBigYHg19Mutations()
        If Not IsNothing(ds) Then 'the db is not empty
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).IsNull("FTDNAKitID") = False Then
                        Dim FTDNAKitID As String
                        Dim YFullKitID As String
                        Dim MembName As String
                        Dim MembCountry As String

                        Dim NewMemb As New Member
                        Dim MembDs As New DataSet
                        'test if exists
                        FTDNAKitID = ds.Tables(0).Rows(i).Item("FTDNAKitID")
                        If ds.Tables(0).Rows(i).IsNull("YFullKitID") = False Then
                            YFullKitID = ds.Tables(0).Rows(i).Item("YFullKitID")
                        Else
                            YFullKitID = ""
                        End If
                        If ds.Tables(0).Rows(i).IsNull("Name") = False Then
                            MembName = ds.Tables(0).Rows(i).Item("Name")
                        Else
                            MembName = ""
                        End If

                        If ds.Tables(0).Rows(i).IsNull("Country") = False Then
                            MembCountry = ds.Tables(0).Rows(i).Item("Country")
                        Else
                            MembCountry = ""
                        End If
                        MembDs = cDataAccess.GetMemberByFTDNAID(FTDNAKitID)
                        If Not IsNothing(MembDs) Then
                            If MembDs.Tables(0).Rows.Count > 0 Then
                                'member exists already 

                            Else
                                'create new member.
                                cDataAccess.InsertMember(MembName, FTDNAKitID, YFullKitID)
                            End If
                        Else
                            'create new member.
                            cDataAccess.InsertMember(MembName, FTDNAKitID, YFullKitID)
                        End If

                        NewMemb.LoadWithFTDNAID(FTDNAKitID)
                        NewMemb.Name = MembName
                        NewMemb.YFullKit = YFullKitID
                        'NewMemb.Country = MembCountry

                        NewMemb.SavetoDB()
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub AddParentNodeIDtotblMutations()
        Dim cDataAccess As New clsDataAccess
        Dim dsNodes As DataSet
        Dim i As Integer
        Dim PrgFrmNd As New frmProgress

        dsNodes = cDataAccess.GetAllNodes()

        If Not IsNothing(dsNodes) Then
            If dsNodes.Tables(0).Rows.Count > 0 Then
                PrgFrmNd.InitiateMe()
                PrgFrmNd.Show()
                PrgFrmNd.UpdateMe("Loading nodes", 0)
                PrgFrmNd.Visible = True
                For i = 0 To dsNodes.Tables(0).Rows.Count - 1
                    If Not dsNodes.Tables(0).Rows(i).IsNull("ID") Then
                        Dim NdID As Integer
                        NdID = dsNodes.Tables(0).Rows(i).Item("ID")
                        If dsNodes.Tables(0).Rows(i).IsNull("MutationsIDs") = False Then
                            Dim PrgFrmMut As New frmProgress
                            Dim Str As String
                            Dim StrArray As String()
                            Dim j As Integer
                            PrgFrmMut.InitiateMe()
                            PrgFrmMut.Show()
                            PrgFrmMut.UpdateMe("Loading mutations", 0)
                            PrgFrmMut.Visible = True
                            Str = dsNodes.Tables(0).Rows(i).Item("MutationsIDs")
                            StrArray = Str.Split(",")
                            j = 0
                            For Each MyStr In StrArray
                                If Not MyStr = "" Then
                                    Dim Mut As New Mutation
                                    Mut.Load(MyStr)
                                    Mut.CurrentParentNodeID = NdID
                                    Mut.SavetoDB()
                                End If
                                PrgFrmMut.UpdateMe("Loading mutations ...", (j + 1), StrArray.Count)
                                j = j + 1
                            Next
                            PrgFrmMut.Visible = False
                        End If
                    End If
                    PrgFrmNd.UpdateMe("Loading nodes ...", (i + 1), dsNodes.Tables(0).Rows.Count)
                Next
                PrgFrmNd.Visible = False
            End If
        End If

    End Sub


    Sub RemoveAllParenNodes()
        cDataAccess.RemoveAllMutationParentsNodes()
    End Sub

    Sub SetAllMutationsToPrivate()
        cDataAccess.SetAllMutationsToPrivate()
    End Sub

    Sub SetAllMembersToHasVariant19()
        cDataAccess.SetTrueToHasVariantHg19_AllMembers()
    End Sub
End Module
