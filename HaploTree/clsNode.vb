Imports HaploTree

Public Class Node
    Private p_ID As String
    Private p_Name As String
    Private p_HasParent As Boolean 'May be useless?
    Private p_ParentNodeID As String
    Private p_ChildrenNodesIDs As String() ' Nodes are holding other nodes/members below
    Private p_ChildrenMembersIDs As String() 'Members are particular nodes = terminations/leaves in the tree
    Private p_MutationsIDs As String() 'All mutations that define that node ... ultimately we want only 1 mutation per node
    Private p_IsSavedToDB As Boolean
    Private p_ds As DataSet 'from Member table in HaploTreeDB

    Public Property ChildrenNodesIDs As String()
        Get
            Return p_ChildrenNodesIDs
        End Get
        Set(value As String())
            Dim i As Integer
            ReDim p_ChildrenNodesIDs(UBound(value))
            For i = 0 To UBound(value) - 1
                p_ChildrenNodesIDs(i) = value(i)
            Next
            p_IsSavedToDB = False
        End Set
    End Property

    Public Sub AppendChildNodeID(NewChildNodeID As String)
        If IsNothing(p_ChildrenNodesIDs) Then
            ReDim p_ChildrenNodesIDs(1)
        Else
            ReDim Preserve p_ChildrenNodesIDs(UBound(p_ChildrenNodesIDs) + 1)
        End If
        p_ChildrenNodesIDs(UBound(p_ChildrenNodesIDs) - 1) = NewChildNodeID
        p_IsSavedToDB = False
    End Sub

    Public Sub RemoveChildNodeID(ChildNodeIDToRemove As String)
        If IsNothing(p_ChildrenNodesIDs) Then
            'not much to remove
        Else
            If UBound(p_ChildrenNodesIDs) > 1 Then
                Dim NewStringArray(1) As String
                NewStringArray(0) = ""
                Dim i As Integer
                Dim count As Integer
                count = 0
                For i = 0 To UBound(p_ChildrenNodesIDs) - 1
                    If Not p_ChildrenNodesIDs(i) = ChildNodeIDToRemove Then
                        If count = 0 Then
                            NewStringArray(count) = p_ChildrenNodesIDs(i)
                        Else
                            ReDim Preserve NewStringArray(count + 1)
                            NewStringArray(count) = p_ChildrenNodesIDs(i)
                        End If
                        count = count + 1
                    End If
                Next
                p_ChildrenNodesIDs = NewStringArray
            Else
                If p_ChildrenNodesIDs(0) = ChildNodeIDToRemove Then
                    p_ChildrenNodesIDs = Nothing
                End If
            End If
        End If
        p_IsSavedToDB = False
    End Sub

    Public Property ParentNodeID As String
        Get
            Return p_ParentNodeID
        End Get
        Set(value As String)
            p_ParentNodeID = value
            p_HasParent = True
            p_IsSavedToDB = False
        End Set
    End Property

    Public ReadOnly Property HasParent As Boolean
        Get
            Return p_HasParent
        End Get
    End Property

    Public Property Name As String
        Get
            Return p_Name
        End Get
        Set(value As String)
            p_Name = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public ReadOnly Property ID As String
        Get
            Return p_ID
        End Get
    End Property

    Public ReadOnly Property IsSavedToDB As Boolean
        Get
            Return p_IsSavedToDB
        End Get
    End Property

    Public Property ChildrenMembersIDs As String()
        Get
            Return p_ChildrenMembersIDs
        End Get
        Set(value As String())
            Dim i As Integer
            ReDim p_ChildrenMembersIDs(UBound(value))
            For i = 0 To UBound(value) - 1
                p_ChildrenMembersIDs(i) = value(i)
            Next
        End Set
    End Property

    Public Sub AppendChildMemberID(NewMemberChildID As String)
        If IsNothing(p_ChildrenMembersIDs) Then
            ReDim p_ChildrenMembersIDs(1)
        Else
            ReDim Preserve p_ChildrenMembersIDs(UBound(p_ChildrenMembersIDs) + 1)
        End If
        p_ChildrenMembersIDs(UBound(p_ChildrenMembersIDs) - 1) = NewMemberChildID
        p_IsSavedToDB = False
    End Sub

    Public Sub RemoveChildMemberID(MbChildIDToRemove As String)
        If IsNothing(p_ChildrenMembersIDs) Then
            'not much to remove
        Else
            If UBound(p_ChildrenMembersIDs) > 1 Then
                Dim NewStringArray(1) As String
                NewStringArray(0) = ""
                Dim i As Integer
                Dim count As Integer
                count = 0
                For i = 0 To UBound(p_ChildrenMembersIDs) - 1
                    If Not p_ChildrenMembersIDs(i) = MbChildIDToRemove Then
                        If count = 0 Then
                            NewStringArray(count) = p_ChildrenMembersIDs(i)
                        Else
                            ReDim Preserve NewStringArray(count + 1)
                            NewStringArray(count) = p_ChildrenMembersIDs(i)
                        End If
                        count = count + 1
                    End If
                Next
                p_ChildrenMembersIDs = NewStringArray
            Else
                If p_ChildrenMembersIDs(0) = MbChildIDToRemove Then
                    p_ChildrenMembersIDs = Nothing
                End If
            End If
        End If
        p_IsSavedToDB = False
    End Sub

    Public Property MutationsIDs As String()
        Get
            Return p_MutationsIDs
        End Get
        Set(value As String())
            Dim i As Integer
            ReDim p_MutationsIDs(UBound(value))
            For i = 0 To UBound(value) - 1
                p_MutationsIDs(i) = value(i)
            Next
        End Set
    End Property

    Public Sub AppendMutationsID(NewMutationsID As String)
        If IsNothing(p_MutationsIDs) Then
            ReDim p_MutationsIDs(1)
        Else
            ReDim Preserve p_MutationsIDs(UBound(p_MutationsIDs) + 1)
        End If
        p_MutationsIDs(UBound(p_MutationsIDs) - 1) = NewMutationsID
        p_IsSavedToDB = False
    End Sub

    Public Sub RemoveMutationID(MutationIDToRemove As String)
        If IsNothing(p_MutationsIDs) Then
            'not much to remove
        Else
            If UBound(p_MutationsIDs) > 1 Then
                Dim NewStringArray(1) As String
                NewStringArray(0) = ""
                Dim i As Integer
                Dim count As Integer
                count = 0
                For i = 0 To UBound(p_MutationsIDs) - 1
                    If Not p_MutationsIDs(i) = MutationIDToRemove Then
                        If count = 0 Then
                            NewStringArray(count) = p_MutationsIDs(i)
                        Else
                            ReDim Preserve NewStringArray(count + 1)
                            NewStringArray(count) = p_MutationsIDs(i)
                        End If
                        count = count + 1
                    End If
                Next
                p_MutationsIDs = NewStringArray
            Else
                If p_MutationsIDs(0) = MutationIDToRemove Then
                    p_MutationsIDs = Nothing
                End If
            End If
        End If
        p_IsSavedToDB = False
    End Sub

    Public Sub New()
        p_ID = ""
        p_Name = ""
        p_ParentNodeID = ""
        p_ChildrenNodesIDs = Nothing
        p_ChildrenMembersIDs = Nothing
        p_MutationsIDs = Nothing
        p_HasParent = False
        p_IsSavedToDB = False
        p_ds = Nothing
    End Sub

    Public Sub New(ByVal NodeID As String, ByVal NodeName As String, Optional ByVal ParentID As String = "")
        p_ID = NodeID
        p_Name = NodeName
        p_ParentNodeID = ParentID
        p_ChildrenNodesIDs = Nothing
        p_ChildrenMembersIDs = Nothing
        p_MutationsIDs = Nothing
        If ParentID = "" Then
            p_HasParent = False
        Else
            p_HasParent = True
        End If
        p_IsSavedToDB = False
        p_ds = Nothing
    End Sub

    Public Function HasMutation(MutationID As String) As Boolean
        Dim MutID As String
        Dim HasMut As Boolean

        HasMut = False
        For Each MutID In p_MutationsIDs
            If MutID = MutationID Then
                HasMut = True
                Exit For
            End If
        Next

        Return HasMut
    End Function

    Public Sub LoadWithName(ByVal NodeName As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetNodeByName(NodeName)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_Name = NodeName
                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    MsgBox("This Node has no ID!") 'should not be possible
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ChildrenNodesIDs") = False Then
                    ReDim p_ChildrenNodesIDs(1)
                    p_ChildrenNodesIDs = {""}
                    'p_ChildrenNodesIDs = p_ds.Tables(0).Rows(0).Item("ChildrenNodesIDs") 'find a proper way to get all the ChildrenNodesIDs into an array
                Else
                    MsgBox("This Node has no ChildrenNodesID!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ChildrenMembersIDs") = False Then
                    ReDim p_ChildrenMembersIDs(1)
                    p_ChildrenMembersIDs = {""}
                    'p_ChildrenMembersIDs = p_ds.Tables(0).Rows(0).Item("ChildrenMembersIDs") 'find a proper way to get all the ChildrenMembersIDs into an array
                Else
                    MsgBox("This Node has no ChildrenNodesID!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    ReDim p_MutationsIDs(1)
                    p_MutationsIDs = {""}
                    'p_MutationsIDs = p_ds.Tables(0).Rows(0).Item("MutationsIDs") 'find a proper way to get all the MutationsIDs into an array
                Else
                    MsgBox("This Node has no MutationsIDs!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasParent") = False Then
                    p_HasParent = p_ds.Tables(0).Rows(0).Item("HasParent")
                Else
                    MsgBox("This Node has no IsISOGGOfficial Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ParentNodeID") = False Then
                    p_ParentNodeID = p_ds.Tables(0).Rows(0).Item("ParentNodeID")
                Else
                    MsgBox("This Node has no ParentNodeID Loaded!")
                End If
            End If
        Else
            MsgBox("Could not load Node with ID " & NodeName & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithID(ByVal NodeID As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetMutationByID(NodeID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_ID = NodeID
                If p_ds.Tables(0).Rows(0).IsNull("Name") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("Name")
                Else
                    MsgBox("This Node has no Name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ChildrenNodesIDs") = False Then
                    ReDim p_ChildrenNodesIDs(1)
                    p_ChildrenNodesIDs = {""}
                    'p_ChildrenNodesIDs = p_ds.Tables(0).Rows(0).Item("ChildrenNodesIDs") 'find a proper way to get all the ChildrenNodesIDs into an array
                Else
                    MsgBox("This Node has no ChildrenNodesID!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ChildrenMembersIDs") = False Then
                    ReDim p_ChildrenMembersIDs(1)
                    p_ChildrenMembersIDs = {""}
                    'p_ChildrenMembersIDs = p_ds.Tables(0).Rows(0).Item("ChildrenMembersIDs") 'find a proper way to get all the ChildrenMembersIDs into an array
                Else
                    MsgBox("This Node has no ChildrenNodesID!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    ReDim p_MutationsIDs(1)
                    p_MutationsIDs = {""}
                    'p_MutationsIDs = p_ds.Tables(0).Rows(0).Item("MutationsIDs") 'find a proper way to get all the MutationsIDs into an array
                Else
                    MsgBox("This Node has no MutationsIDs!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasParent") = False Then
                    p_HasParent = p_ds.Tables(0).Rows(0).Item("HasParent")
                Else
                    MsgBox("This Node has no HasParent Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ParentNodeID") = False Then
                    p_ParentNodeID = p_ds.Tables(0).Rows(0).Item("ParentNodeID")
                Else
                    MsgBox("This Node has no ParentNodeID Loaded!")
                End If
            End If
        Else
            MsgBox("Could not load Node with ID " & NodeID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Private Function AlreadyExistsInDB() As String 'returns the ID if exists, "" if not
        Dim ds As DataSet
        Dim cDataAccess As New clsDataAccess

        ds = cDataAccess.GetNodeByName(p_Name) 'from HaploTreeDB
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).IsNull("ParentNodeID") = False Then
                    If p_ParentNodeID = ds.Tables(0).Rows(0).Item("ParentNodeID") Then 'same name and same parent = same node
                        Return ds.Tables(0).Rows(0).Item("ID")
                    Else 'same name but different parent = different node (and we have an issue!)
                        Return ""
                    End If
                Else
                    If p_ParentNodeID = "" Then 'same name and no parent in both cases = same node
                        Return ds.Tables(0).Rows(0).Item("ID")
                    Else 'same name but different parent = different node (and we have an issue!)
                        Return ""
                    End If
                End If
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Public Sub SavetoDB() 'into HaploTreeDB
        Dim cDataAccess As New clsDataAccess

        If p_ID = "" Then 'insert 
            'Save as new node, but check if exists in first
            p_ID = AlreadyExistsInDB()
            If p_ID = "" Then 'This is an insert
                cDataAccess.InsertNodeInTree(p_Name, p_ParentNodeID, p_ChildrenNodesIDs, p_ChildrenMembersIDs, p_MutationsIDs, p_HasParent)
                p_ID = AlreadyExistsInDB() 'now should have got a ID!
            Else 'This is an update - be carefull we may have encountered an node with same name
                cDataAccess.UpdateNodeInTree(p_Name, p_ParentNodeID, p_ChildrenNodesIDs, p_ChildrenMembersIDs, p_MutationsIDs, p_HasParent, p_ID)
            End If
        Else
            'Save updates
            cDataAccess.UpdateNodeinTree(p_Name, p_ParentNodeID, p_ChildrenNodesIDs, p_ChildrenMembersIDs, p_MutationsIDs, p_HasParent, p_ID)
        End If
        p_IsSavedToDB = True
    End Sub

    Protected Overrides Sub Finalize()
        If p_IsSavedToDB = False Then
            If MsgBox("Node " & p_Name & " has been modified! Do you want to save changes to the DB?") = MsgBoxResult.Ok Then
                'do it
                Me.SavetoDB()
            End If
        End If
    End Sub
End Class
