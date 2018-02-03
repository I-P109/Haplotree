Imports HaploTree

Module TreeConstructor
    Private p_TreeRoot As Node

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
        End If
    End Sub

    Public Sub InsertNewKitInTree(KitID As Integer, RootNodeName As String) 'run this AFTER saving KitID's variant file data to the DB 
        Dim ParentNode As Node
        Dim SplitNode As Boolean
        Dim HasCommonMutations As Boolean
        Dim HasAddedBranch As Boolean
        Dim NewMember As New Member()
        Dim MutId As String
        Dim MbId As String
        Dim NdId As String


        NewMember.LoadWithID(KitID)
        If NewMember.IsPlacedInTheTree = False Then
            If NewMember.Variant19Loaded = True Or NewMember.Variant38Loaded = True Then
                If Not IsNothing(NewMember.MutationsIDs) Then
                    If NewMember.MutationsIDs.Count > 0 Then
                        'RootNodeName = "Root" 'to be updated when we give user the choice of a starting node
                        p_TreeRoot = GetNode(RootNodeName)
                        If IsNothing(p_TreeRoot) Then
                            If NBNodesInDB() > 0 Then 'the provided name is not found in the DB
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
                                NewMember.CurrentParentNodeID = NewNd.ID
                                NewNd.SavetoDB()
                                NewMember.SavetoDB()
                                Exit Sub
                            End If
                        End If

                        ParentNode = FindClosestExistingNodeDownward(NewMember, p_TreeRoot) 'the user provides an apriori start node to speed up the process

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
                        End If

                        SplitNode = False
                        For Each MutId In ParentNode.MutationsIDs
                            'test if the node needs to be splitted
                            If NewMember.HasMutation(MutId) = False Then
                                SplitNode = True
                                Exit For
                            End If
                        Next

                        If SplitNode = False Then 'no need to split the node
                            'check if has common mutations with existing members below parentnode
                            HasAddedBranch = False
                            For Each MbId In ParentNode.ChildrenMembersIDs
                                Dim Mb As New Member()
                                Mb.LoadWithID(MbId)
                                HasCommonMutations = False
                                If HasAddedBranch = False Then 'no other member should have common muttations with new kit
                                    For Each MutId In Mb.PrivateMutationsIDs 'it is only necessary to check private mutations to eventually add a branch
                                        If NewMember.HasMutation(MutId) = True Then
                                            HasCommonMutations = True
                                            Exit For
                                        End If
                                    Next
                                    If HasCommonMutations = True Then 'need to add a node/branch
                                        Dim NewNode As Node
                                        NewNode = AddNodeBelow(ParentNode, Mb, NewMember) 'creates a new node below parentnode with the common mutations of Mb and newmember

                                        ParentNode.AppendChildNodeID(NewNode.ID)
                                        ParentNode.RemoveChildMemberID(Mb.ID)
                                        For Each MutId In NewNode.MutationsIDs
                                            Dim Mut As New Mutation()
                                            Mb.RemovePrivateMutationID(MutId)
                                            Mut.Load(MutId)
                                            Mut.IsPrivate = False
                                            Mut.SavetoDB()
                                        Next

                                        Mb.CurrentParentNodeID = NewNode.ID
                                        NewMember.CurrentParentNodeID = NewNode.ID

                                        NewNode.SavetoDB()
                                        ParentNode.SavetoDB()
                                        Mb.SavetoDB()
                                        NewMember.SavetoDB()
                                        HasAddedBranch = True
                                    End If
                                Else
                                    MsgBox("Houston we have a problem: It seems we can make more than one new branch with different members")
                                    'here we need to find a way to address that issue if it happens ... may have to review/add some putativemutations in some members below this node - manual work
                                End If
                            Next
                            If HasAddedBranch = False Then 'there is no other members with common mutations
                                ParentNode.AppendChildMemberID(NewMember.ID)
                                NewMember.CurrentParentNodeID = ParentNode.ID
                                ParentNode.SavetoDB()
                                NewMember.SavetoDB()
                            End If
                        Else ' need to split node
                            Dim NewNode As Node

                            NewNode = AddNodeBelow(ParentNode, NewMember) 'creates a new node below parentnode with the noncommon mutations of ParentNode and newmember

                            For Each MutId In NewNode.MutationsIDs
                                ParentNode.RemoveMutationID(MutId)
                            Next
                            For Each MbId In NewNode.ChildrenMembersIDs
                                Dim Mb As New Member()
                                Mb.LoadWithID(MbId)
                                Mb.CurrentParentNodeID = NewNode.ID
                                Mb.SavetoDB()
                            Next
                            For Each NdId In NewNode.ChildrenNodesIDs
                                Dim Nd As New Node()
                                Nd.LoadWithID(NdId)
                                Nd.ParentNodeID = NewNode.ID
                                Nd.SavetoDB()
                            Next

                            ParentNode.ChildrenMembersIDs = Nothing
                            ParentNode.ChildrenNodesIDs = Nothing

                            ParentNode.AppendChildMemberID(NewMember.ID)
                            ParentNode.AppendChildNodeID(NewNode.ID)

                            NewMember.CurrentParentNodeID = ParentNode.ID

                            NewNode.SavetoDB()
                            ParentNode.SavetoDB()
                            NewMember.SavetoDB()
                            HasAddedBranch = True
                        End If
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

    Private Function NBNodesInDB() As Integer
        Dim cDataAccess As New clsDataAccess
        Return cDataAccess.GetNBNodesInDB
    End Function

    Private Function AddNodeBelow(ByRef ParNode As Node, ByRef ExistingMemb As Member, ByRef NewMemb As Member) As Node 'creates a new node below parentnode with the common mutations of Mb and newmember
        Dim NewNode As New Node()
        Dim MutId As String
        Dim Mut As New Mutation()

        NewNode.ParentNodeID = ParNode.ID
        NewNode.AppendChildMemberID(ExistingMemb.ID)
        NewNode.AppendChildMemberID(NewMemb.ID)

        For Each MutId In ExistingMemb.PrivateMutationsIDs 'it is only necessary to check private mutations to find common mutations
            If NewMemb.HasMutation(MutId) = True Then
                NewNode.AppendMutationsID(MutId)
            End If
        Next
        Mut.Load(NewNode.MutationsIDs(0))
        NewNode.Name = Mut.Name(0)

        NewNode.SavetoDB()
        Return NewNode
    End Function

    Private Function AddNodeBelow(ByRef ParNode As Node, ByRef NewMemb As Member) As Node 'creates a new node below parentnode with the noncommon mutations of parentnode and newmember
        Dim NewNode As New Node()
        Dim MutId As String
        Dim Mut As New Mutation()

        NewNode.ParentNodeID = ParNode.ID
        NewNode.ChildrenNodesIDs = ParNode.ChildrenNodesIDs 'add all node children of parent node to new node
        NewNode.ChildrenMembersIDs = ParNode.ChildrenMembersIDs 'add all member children of parent node to new node

        For Each MutId In ParNode.MutationsIDs 'add mutations that the new memb doesn't have
            If NewMemb.HasMutation(MutId) = False Then
                NewNode.AppendMutationsID(MutId)
            End If
        Next
        Mut.Load(NewNode.MutationsIDs(0))
        NewNode.Name = Mut.Name(0)

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
                    HasStopped = True
                Else
                    AtLeastOneSimilar = True
                End If
            Next
            If HasStopped = True Then ' we found at least 1 diff
                If AtLeastOneSimilar = True Then
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

        Success = True
        UsingHg38 = True

        If Not IsNothing(NewMemb.MutationsIDs) Then
            'ask if user wants to review list of mutations - in case a new variant file has been loaded
            'do it
            If MsgBox("This kit has already mutations defined.\n Do you want to to review its list of mutation?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return Success
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
        For i = 1 To NbItems - 1
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
        Next
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

    Private Function GetHg19FromHg38(Position38 As String) As String
        Dim Pos19 As Long
        'Get it from Position38
        Pos19 = ConvertHG38ToHG19(CLng(Position38))
        Return Pos19
    End Function

    Private Function GetHg38FromHg19(Position19 As String) As String
        Dim Pos38 As Long
        'Get it from Position19
        Pos38 = ConvertHG19ToHG38(CLng(Position19))
        Return Pos38
    End Function
End Module
