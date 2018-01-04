Imports HaploTree

Public Class Mutation
    Private p_ID As String
    Private p_PositionID As String
    Private p_AltCall As String
    Private p_Names As String() 'May have several names
    Private p_IsISOGGOfficial As Boolean 'if ISOGG gave a RefSNPID to it
    Private p_IsPrivate As Boolean 'if only 1 member has it
    Private p_RefSNPID As String ' From ISOGG
    Private p_IsSavedToDB As Boolean
    Private p_IsIgnored As Boolean 'In case we do not want to use it in the tree building process - not in use right now

    Public ReadOnly Property ID As String
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
            For i = 0 To UBound(value) - 1
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

    Public Sub New()
        p_ID = ""
        p_PositionID = ""
        p_AltCall = ""
        p_RefSNPID = ""
        ReDim p_Names(1)
        p_Names = {""}
        p_IsISOGGOfficial = False
        p_IsPrivate = False
        p_IsIgnored = False
        p_IsSavedToDB = False
    End Sub

    Public Sub New(ByVal MutID As String, ByVal PosID As String, ByVal AlternateCall As String, Optional ByVal ReferenceSNPID As String = "", Optional IsPrivateSNP As Boolean = False, Optional IsIgnoredInTree As Boolean = False)
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
        p_IsPrivate = IsPrivateSNP
        p_IsIgnored = IsIgnoredInTree
        p_IsSavedToDB = False
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
        If Not p_Names(0) = "" Then
            ReDim Preserve p_Names(UBound(p_Names) + 1)
        End If
        p_Names(UBound(p_Names) - 1) = NewName
        p_IsSavedToDB = False
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
                For i = 0 To UBound(p_Names) - 1
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

    Public Sub Load(ByVal MutID As String) 'load from the DB
        'do it
        MsgBox("we need to load a mutation with ID " & MutID & " from db")
        p_IsSavedToDB = True
    End Sub

    Public Sub SavetoDB()
        'do it
        MsgBox("we need to save changes to mutation " & p_Names(0) & " to the db")
        If p_ID = "" Then
            'Save as new mutation

        Else
            'Save updates

        End If
        p_IsSavedToDB = True
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
