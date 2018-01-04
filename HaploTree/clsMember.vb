Imports HaploTree

Public Class Member
    Private p_ID As String
    Private p_Name As String
    Private p_FTDNAKit As String
    Private p_YFullKit As String
    Private p_MutationsIDs As String() 'includes private and putative mutations
    Private p_PrivateMutationsIDs As String() 'no one else has those mutations
    Private p_CurrentParentNodeID As String
    Private p_PutativeMutationsIDs As String() ' mutations that have not been tested or results are too bad to be interpreted but we/admin have strong presumptions that this member has the mutation
    Private p_IsSavedToDB As Boolean
    Private p_IsPlacedInTheTree As Boolean

    Public Property PutativeMutationsIDs As String()
        Get
            Return p_PutativeMutationsIDs
        End Get
        Set(value As String())
            Dim i As Integer
            ReDim p_PutativeMutationsIDs(UBound(value))
            For i = 0 To UBound(value) - 1
                p_PutativeMutationsIDs(i) = value(i)
            Next
            p_IsSavedToDB = False
        End Set
    End Property

    Public Sub AppendPutativeMutationsID(NewPutativeMutationsID As String)
        If IsNothing(p_PutativeMutationsIDs) Then
            ReDim p_PutativeMutationsIDs(1)
        Else
            ReDim Preserve p_PutativeMutationsIDs(UBound(p_PutativeMutationsIDs) + 1)
        End If
        p_PutativeMutationsIDs(UBound(p_PutativeMutationsIDs) - 1) = NewPutativeMutationsID
        p_IsSavedToDB = False
    End Sub

    Public Sub RemovePutativeMutationID(MutationIDToRemove As String)
        If IsNothing(p_PutativeMutationsIDs) Then
            'not much to remove
        Else
            If UBound(p_PutativeMutationsIDs) > 1 Then
                Dim NewStringArray(1) As String
                NewStringArray(0) = ""
                Dim i As Integer
                Dim count As Integer
                count = 0
                For i = 0 To UBound(p_PutativeMutationsIDs) - 1
                    If Not p_PutativeMutationsIDs(i) = MutationIDToRemove Then
                        If count = 0 Then
                            NewStringArray(count) = p_PutativeMutationsIDs(i)
                        Else
                            ReDim Preserve NewStringArray(count + 1)
                            NewStringArray(count) = p_PutativeMutationsIDs(i)
                        End If
                        count = count + 1
                    End If
                Next
                p_PutativeMutationsIDs = NewStringArray
            Else
                If p_PutativeMutationsIDs(0) = MutationIDToRemove Then
                    p_PutativeMutationsIDs = Nothing
                End If
            End If
        End If
        p_IsSavedToDB = False
    End Sub

    Public Property CurrentParentNodeID As String
        Get
            Return p_CurrentParentNodeID
        End Get
        Set(value As String)
            p_CurrentParentNodeID = value
            p_IsPlacedInTheTree = True
            p_IsSavedToDB = False
        End Set
    End Property

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
            p_IsSavedToDB = False
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

    Public Property YFullKit As String
        Get
            Return p_YFullKit
        End Get
        Set(value As String)
            p_YFullKit = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property FTDNAKit As String
        Get
            Return p_FTDNAKit
        End Get
        Set(value As String)
            p_FTDNAKit = value
            p_IsSavedToDB = False
        End Set
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

    Public Property PrivateMutationsIDs As String()
        Get
            Return p_PrivateMutationsIDs
        End Get
        Set(value As String())
            Dim i As Integer
            ReDim p_PrivateMutationsIDs(UBound(value))
            For i = 0 To UBound(value) - 1
                p_PrivateMutationsIDs(i) = value(i)
            Next
            p_IsSavedToDB = False
        End Set
    End Property

    Public Sub AppendPrivateMutationsID(NewPrivateMutationsID As String)
        If IsNothing(p_PrivateMutationsIDs) Then
            ReDim p_PrivateMutationsIDs(1)
        Else
            ReDim Preserve p_PrivateMutationsIDs(UBound(p_PrivateMutationsIDs) + 1)
        End If
        p_PrivateMutationsIDs(UBound(p_PrivateMutationsIDs) - 1) = NewPrivateMutationsID
        p_IsSavedToDB = False
    End Sub

    Public Sub RemovePrivateMutationID(MutationIDToRemove As String)
        If IsNothing(p_PrivateMutationsIDs) Then
            'not much to remove
        Else
            If UBound(p_PrivateMutationsIDs) > 1 Then
                Dim NewStringArray(1) As String
                NewStringArray(0) = ""
                Dim i As Integer
                Dim count As Integer
                count = 0
                For i = 0 To UBound(p_PrivateMutationsIDs) - 1
                    If Not p_PrivateMutationsIDs(i) = MutationIDToRemove Then
                        If count = 0 Then
                            NewStringArray(count) = p_PrivateMutationsIDs(i)
                        Else
                            ReDim Preserve NewStringArray(count + 1)
                            NewStringArray(count) = p_PrivateMutationsIDs(i)
                        End If
                        count = count + 1
                    End If
                Next
                p_PrivateMutationsIDs = NewStringArray
            Else
                If p_PrivateMutationsIDs(0) = MutationIDToRemove Then
                    p_PrivateMutationsIDs = Nothing
                End If
            End If
        End If
        p_IsSavedToDB = False
    End Sub

    Public ReadOnly Property IsPlacedInTheTree As Boolean
        Get
            Return p_IsPlacedInTheTree
        End Get
    End Property

    Public Sub New()
        p_ID = ""
        p_Name = ""
        p_FTDNAKit = "-999"
        p_YFullKit = "-999"
        p_CurrentParentNodeID = ""
        p_MutationsIDs = Nothing
        p_PutativeMutationsIDs = Nothing
        p_PrivateMutationsIDs = Nothing
        p_IsSavedToDB = False
        p_IsPlacedInTheTree = False
    End Sub

    Public Sub New(ByVal memberID As String, ByVal memberName As String, Optional ByVal FTDNAkitval As String = "-999", Optional ByVal YFullKitval As String = "-999")
        p_ID = memberID
        p_Name = memberName
        p_FTDNAKit = FTDNAkitval
        p_YFullKit = YFullKitval
        p_CurrentParentNodeID = ""
        p_MutationsIDs = Nothing
        p_PutativeMutationsIDs = Nothing
        p_PrivateMutationsIDs = Nothing
        p_IsSavedToDB = False
        p_IsPlacedInTheTree = False
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

    Public Function HasPrivateMutation(MutationID As String) As Boolean
        Dim MutID As String
        Dim HasMut As Boolean

        HasMut = False
        For Each MutID In p_PrivateMutationsIDs
            If MutID = MutationID Then
                HasMut = True
                Exit For
            End If
        Next

        Return HasMut
    End Function

    Public Function HasPutativeMutation(MutationID As String) As Boolean
        Dim MutID As String
        Dim HasMut As Boolean

        HasMut = False
        For Each MutID In p_PutativeMutationsIDs
            If MutID = MutationID Then
                HasMut = True
                Exit For
            End If
        Next

        Return HasMut
    End Function

    Public Function GetPositionHg38AtRow(Item As Integer) As Long 'returns preferably from the HG38 variant data, from HG19 if not, position is transformed into hg38 in any case
        Return 0
    End Function

    Public Function GetRefCallAtRow(Item As Integer) As String 'returns preferably from the HG38 variant data, from HG19 if not
        Return ""
    End Function

    Public Function GetAltCallAtRow(Item As Integer) As String 'returns preferably from the HG38 variant data, from HG19 if not
        Return ""
    End Function

    Public Function GetRefCallHg19AtRow(Item As Integer) As String 'returns only from HG19 variant data, "" if HG19 does not exist
        Return ""
    End Function

    Public Function GetAltCallHg19AtRow(Item As Integer) As String 'returns only from HG19 variant data, "" if HG19 does not exist
        Return ""
    End Function

    Public Sub LoadWithName(ByVal memberName As String) 'load from the DB
        'do it
        MsgBox("we need to load member " & memberName & " from db using its name!")
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithID(ByVal memberID As String) 'load from the DB
        'do it
        MsgBox("we need to load a member with ID " & memberID & " from db using its ID!")
        p_IsSavedToDB = True
    End Sub

    Public Sub SavetoDB() 'note that a member should always exists already in the DB,
        'we do Not save New members in the DB from here, only changes.
        'do it
        MsgBox("we need to save changes to member " & p_Name & " to the db!")
        If p_ID = "" Then
            'Save as new Member - unsure if this should be possible

        Else
            'Save updates

        End If
        p_IsSavedToDB = True
    End Sub

    Protected Overrides Sub Finalize()
        If p_IsSavedToDB = False Then
            If MsgBox("Member " & p_Name & " has been modified! Do you want to save changes to the DB?") = MsgBoxResult.Ok Then
                'do it
                Me.SavetoDB()
            End If
        End If
    End Sub

End Class
