Imports HaploTree

Public Class Mutation
    Private p_ID As Integer
    Private p_PositionID As String
    Private p_AltCall As String
    Private p_Names As String() 'May have several names
    Private p_IsISOGGOfficial As Boolean 'if ISOGG gave a RefSNPID to it
    Private p_IsPrivate As Boolean 'if only 1 member has it
    Private p_RefSNPID As String ' From ISOGG
    Private p_IsSavedToDB As Boolean
    Private p_IsIgnored As Boolean 'In case we do not want to use it in the tree building process - not in use right now
    Private p_ds As DataSet 'from Mutation table in HaploTreeDB
    Private p_CurrentParentNodeID As String

    Public ReadOnly Property ID As Integer
        Get
            Return p_ID
        End Get
    End Property

    Public Property PositionID As String
        Get
            Return p_PositionID
        End Get
        Set(value As String)
            p_PositionID = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property AltCall As String
        Get
            Return p_AltCall
        End Get
        Set(value As String)
            p_AltCall = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property Name As String()
        Get
            Return p_Names
        End Get
        Set(value As String())
            Dim i As Integer
            ReDim p_Names(UBound(value))
            For i = 0 To UBound(value)
                p_Names(i) = value(i)
            Next
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property IsISOGGOfficial As Boolean
        Get
            Return p_IsISOGGOfficial
        End Get
        Set(value As Boolean)
            p_IsISOGGOfficial = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property IsPrivate As Boolean
        Get
            Return p_IsPrivate
        End Get
        Set(value As Boolean)
            p_IsPrivate = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property RefSNPID As String
        Get
            Return p_RefSNPID
        End Get
        Set(value As String)
            p_RefSNPID = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public ReadOnly Property IsSavedToDB As Boolean
        Get
            Return p_IsSavedToDB
        End Get
    End Property

    Public Property IsIgnored As Boolean
        Get
            Return p_IsIgnored
        End Get
        Set(value As Boolean)
            p_IsIgnored = value
        End Set
    End Property

    Public Property CurrentParentNodeID As String
        Get
            Return p_CurrentParentNodeID
        End Get
        Set(value As String)
            p_CurrentParentNodeID = value
        End Set
    End Property

    Public Sub New()
        p_ID = 0
        p_PositionID = ""
        p_AltCall = ""
        p_RefSNPID = ""
        ReDim p_Names(1)
        p_Names = {""}
        p_ds = Nothing
        p_IsISOGGOfficial = False
        p_IsPrivate = False
        p_IsIgnored = False
        p_IsSavedToDB = False
        p_CurrentParentNodeID = ""
    End Sub

    Public Sub New(ByVal MutID As Integer, ByVal PosID As String, ByVal AlternateCall As String, Optional ByVal ReferenceSNPID As String = "", Optional IsPrivateSNP As Boolean = False, Optional IsIgnoredInTree As Boolean = False, Optional CurrParNodeID As String = "")
        p_ID = MutID
        p_PositionID = PosID
        p_AltCall = AlternateCall
        p_RefSNPID = ReferenceSNPID
        ReDim p_Names(1)
        p_Names = {""}
        If ReferenceSNPID = "" Then
            p_IsISOGGOfficial = False
        Else
            p_IsISOGGOfficial = True
        End If
        p_ds = Nothing
        p_IsPrivate = IsPrivateSNP
        p_IsIgnored = IsIgnoredInTree
        p_IsSavedToDB = False
        p_CurrentParentNodeID = CurrParNodeID
    End Sub

    Public Function HasName(Nam As String) As Boolean
        Dim Nm As String
        Dim HasNm As Boolean

        HasNm = False
        For Each Nm In p_Names
            If Nm = Nam Then
                HasNm = True
                Exit For
            End If
        Next

        Return HasNm
    End Function

    Public Sub AppendName(NewName As String)
        If Me.HasName(NewName) = False Then
            If Not p_Names(0) = "" Then
                ReDim Preserve p_Names(UBound(p_Names) + 1)
            End If
            p_Names(UBound(p_Names)) = NewName
            p_IsSavedToDB = False
        End If
    End Sub

    Public Sub RemoveName(NameToRemove As String)
        If IsNothing(p_Names) Then
            'not much to remove
        Else
            If UBound(p_Names) > 1 Then
                Dim NewStringArray(1) As String
                NewStringArray(0) = ""
                Dim i As Integer
                Dim count As Integer
                count = 0
                For i = 0 To UBound(p_Names)
                    If Not p_Names(i) = NameToRemove Then
                        If count = 0 Then
                            NewStringArray(count) = p_Names(i)
                        Else
                            ReDim Preserve NewStringArray(count + 1)
                            NewStringArray(count) = p_Names(i)
                        End If
                        count = count + 1
                    End If
                Next
                p_Names = NewStringArray
            Else
                If p_Names(0) = NameToRemove Then
                    p_Names = Nothing
                End If
            End If
        End If
        p_IsSavedToDB = False
    End Sub

    Private Function GetStringArrayCommaDelimited(MyStringArray As String) As String()

        Return MyStringArray.Split(",")

    End Function

    Public Sub Load(ByVal MutID As Integer) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetMutationByID(MutID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_ID = MutID
                If p_ds.Tables(0).Rows(0).IsNull("PositionID") = False Then
                    p_PositionID = p_ds.Tables(0).Rows(0).Item("PositionID")
                Else
                    MsgBox("This Mutation has no PositionID!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("AltCall") = False Then
                    p_AltCall = p_ds.Tables(0).Rows(0).Item("AltCall")
                Else
                    MsgBox("This Mutation has no AltCal!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("RefSNPID") = False Then
                    p_RefSNPID = p_ds.Tables(0).Rows(0).Item("RefSNPID")
                Else
                    MsgBox("This Mutation has no RefSNPID number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationNames") = False Then
                    p_Names = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("MutationNames"))
                Else
                    MsgBox("This Mutation has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsISOGGOfficial") = False Then
                    p_IsISOGGOfficial = p_ds.Tables(0).Rows(0).Item("IsISOGGOfficial")
                Else
                    MsgBox("This Mutation has IsISOGGOfficial Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPrivate") = False Then
                    p_IsPrivate = p_ds.Tables(0).Rows(0).Item("IsPrivate")
                Else
                    MsgBox("This Mutation has no IsPrivate Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsIgnored") = False Then
                    p_IsIgnored = p_ds.Tables(0).Rows(0).Item("IsIgnored")
                Else
                    MsgBox("This Mutation has no IsIgnored loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentNodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentNodeID")
                Else
                    MsgBox("This Mutation has no CurrentNodeID yet!")
                End If
            End If
        Else
            MsgBox("Could not load Mutation with ID " & MutID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithPositionIDAndAltCall(ByVal PosID As String, ByVal AlternCall As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetMutationByPosAndAltCall(PosID, AlternCall) 'from HaploTreeDB
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_PositionID = PosID
                p_AltCall = AlternCall
                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    MsgBox("This Mutation has no ID!") 'should not really happen
                End If

                If p_ds.Tables(0).Rows(0).IsNull("RefSNPID") = False Then
                    p_RefSNPID = p_ds.Tables(0).Rows(0).Item("RefSNPID")
                Else
                    MsgBox("This Mutation has no RefSNPID number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationNames") = False Then
                    p_Names = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("MutationNames"))
                Else
                    MsgBox("This Mutation has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsISOGGOfficial") = False Then
                    p_IsISOGGOfficial = p_ds.Tables(0).Rows(0).Item("IsISOGGOfficial")
                Else
                    MsgBox("This Mutation has IsISOGGOfficial Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPrivate") = False Then
                    p_IsPrivate = p_ds.Tables(0).Rows(0).Item("IsPrivate")
                Else
                    MsgBox("This Mutation has no IsPrivate Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsIgnored") = False Then
                    p_IsIgnored = p_ds.Tables(0).Rows(0).Item("IsIgnored")
                Else
                    MsgBox("This Mutation has no IsIgnored loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentNodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentNodeID")
                Else
                    MsgBox("This Mutation has no CurrentNodeID yet!")
                End If
            End If
        Else
            MsgBox("Could not load Mutation with PositionID " & PosID & " and Alt Call" & AlternCall & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Private Function AlreadyExistsInDB() As Integer 'returns the ID if exists, "" if not
        Dim ds As DataSet
        Dim cDataAccess As New clsDataAccess

        ds = cDataAccess.GetMutationByPosAndAltCall(p_PositionID, p_AltCall) 'from HaploTreeDB
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("ID")
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Public Sub SavetoDB() 'into HaploTreeDB
        Dim cDataAccess As New clsDataAccess
        Dim AllNames As String
        Dim i As Integer

        If p_IsSavedToDB = False Then
            If Not IsNothing(p_Names) Then
                AllNames = p_Names(0)
                For i = 1 To p_Names.Count - 1
                    AllNames = AllNames & "," & p_Names(i)
                Next
            Else
                AllNames = ""
            End If
            If p_ID = 0 Then 'insert 
                'Save as new Mutation, but check if exists in first
                p_ID = AlreadyExistsInDB()
                If p_ID = 0 Then 'This is an insert
                    cDataAccess.InsertMutation(p_PositionID, p_AltCall, p_RefSNPID, AllNames, p_IsISOGGOfficial, p_IsPrivate, p_IsIgnored, p_CurrentParentNodeID)
                    p_ID = AlreadyExistsInDB() 'now should have got a ID!
                Else 'This is an update
                    cDataAccess.UpdateMutation(p_ID, p_PositionID, p_AltCall, p_RefSNPID, AllNames, p_IsISOGGOfficial, p_IsPrivate, p_IsIgnored, p_CurrentParentNodeID)
                End If
            Else
                'Save updates
                cDataAccess.UpdateMutation(p_ID, p_PositionID, p_AltCall, p_RefSNPID, AllNames, p_IsISOGGOfficial, p_IsPrivate, p_IsIgnored, p_CurrentParentNodeID)
            End If
            p_IsSavedToDB = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        If p_IsSavedToDB = False Then
            If MsgBox("Mutation " & p_Names(0) & " has been modified! Do you want to save changes to the DB?") = MsgBoxResult.Ok Then
                'do it
                Me.SavetoDB()
            End If
        End If
    End Sub
End Class
