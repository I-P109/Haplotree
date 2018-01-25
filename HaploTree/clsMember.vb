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
    Private p_ds As DataSet 'from Member table in VariantDB
    Private p_dsPositions As DataSet 'from Position table in VariantDB
    Private p_VariantLoaded As Boolean

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

    Public ReadOnly Property VariantLoaded As Boolean
        Get
            Return p_VariantLoaded
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
        p_VariantLoaded = False
        p_ds = Nothing
        p_dsPositions = Nothing
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
        p_VariantLoaded = False
        p_ds = Nothing
        p_dsPositions = Nothing
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

    Public Sub LoadVariantsHG38(MemberID As Integer) ' only when a member is already loaded
        Dim cDataAccess As New clsDataAccess

        p_VariantLoaded = False
        p_ds = cDataAccess.GetMemberByID(MemberID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then

                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    If Not p_Name = p_ds.Tables(0).Rows(0).Item("MemberName") Then
                        MsgBox("Member Name Mismatch!")
                        Exit Sub
                    End If
                Else
                    MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                    If Not p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAID") Then
                        MsgBox("Member FTDNA kit number Mismatch!")
                        Exit Sub
                    End If
                Else
                    MsgBox("This Member has no FTDNA kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                    If Not p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullID") Then
                        MsgBox("Member YFull kit number Mismatch!")
                        Exit Sub
                    End If
                Else
                    MsgBox("This Member has no YFull kit number!")
                End If

                'Now see if this person has records stored
                p_dsPositions = cDataAccess.GetPositionsByMemberID38(MemberID)
                If Not IsNothing(p_dsPositions) Then
                    If Not p_dsPositions.Tables(0).Rows.Count > 0 Then
                        MsgBox("Member has no variant data loaded!")
                        Exit Sub
                    End If
                    p_VariantLoaded = True
                Else
                    MsgBox("Member with ID " & MemberID & " has no variant loaded!")
                End If
            End If
        Else
            MsgBox("Could not load member with ID " & MemberID & "!")
        End If
    End Sub

    Public Function GetPositionHg38AtRow(Itemi As Integer) As Long 'returns preferably from the HG38 variant data, from HG19 if not, position is transformed into hg38 in any case
        If Not IsNothing(p_dsPositions) Then
            Return p_dsPositions.Tables(0).Rows(Itemi).Item("Pos")
        Else
            Return 0
        End If
    End Function

    Public Function GetRefCallAtRow(Itemi As Integer) As String 'returns preferably from the HG38 variant data, from HG19 if not
        If Not IsNothing(p_dsPositions) Then
            Return p_dsPositions.Tables(0).Rows(Itemi).Item("Ref")
        Else
            Return ""
        End If
    End Function

    Public Function GetAltCallAtRow(Itemi As Integer) As String 'returns preferably from the HG38 variant data, from HG19 if not
        If Not IsNothing(p_dsPositions) Then
            Return p_dsPositions.Tables(0).Rows(Itemi).Item("Alt")
        Else
            Return ""
        End If
    End Function

    Public Function GetAltCallDepthAtRow(Itemi As Integer) As String 'returns preferably from the HG38 variant data, from HG19 if not
        If Not IsNothing(p_dsPositions) Then
            'use p_dsPositions.Tables(0).Rows(Itemi).Item("Mutation") and extract depth
            Return ""
        Else
            Return ""
        End If
    End Function

    Public Function GetTotalDepthAtRow(Itemi As Integer) As String 'returns preferably from the HG38 variant data, from HG19 if not
        If Not IsNothing(p_dsPositions) Then
            'use p_dsPositions.Tables(0).Rows(Itemi).Item("Mutation") and extract depth
            Return ""
        Else
            Return ""
        End If
    End Function

    Public Function GetRefCallHg19AtRow(Itemi As Integer) As String 'returns only from HG19 variant data, "" if HG19 does not exist
        If Not IsNothing(p_dsPositions) Then
            Return ""
        Else
            Return ""
        End If
    End Function

    Public Function GetAltCallHg19AtRow(Itemi As Integer) As String 'returns only from HG19 variant data, "" if HG19 does not exist
        If Not IsNothing(p_dsPositions) Then
            Return ""
        Else
            Return ""
        End If
    End Function

    Public Sub LoadWithName(ByVal memberName As String) 'load from the DB
        'do it
        MsgBox("we need to load member " & memberName & " from db using its name!")
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithFTDNAID(ByVal FTDNAID As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_VariantLoaded = False
        p_ds = cDataAccess.GetMemberByFTDNAID(FTDNAID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_FTDNAKit = FTDNAID
                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("MemberName")
                Else
                    MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("YFullKit") = False Then
                    p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullKit")
                Else
                    MsgBox("This Member has no YFull kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    MsgBox("This Member has no ID!") 'should not realy happen
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsID") = False Then
                    'p_MutationsIDs = p_ds.Tables(0).Rows(0).Item("MutationsID") 'find a proper way to get all the mutationsIDs into an array
                Else
                    MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsID") = False Then
                    'p_PrivateMutationsIDs = p_ds.Tables(0).Rows(0).Item("PrivateMutationsID") 'find a proper way to get all the private mutationsIDs into an array
                Else
                    MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsID") = False Then
                    'p_PutativeMutationsIDs = p_ds.Tables(0).Rows(0).Item("PutativeMutationsID") 'find a proper way to get all the putative mutationsIDs into an array
                Else
                    MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                'Now see if this person has variant records stored
                p_dsPositions = cDataAccess.GetPositionsByMemberID38(p_ID)
                If Not IsNothing(p_dsPositions) Then
                    If Not p_dsPositions.Tables(0).Rows.Count > 0 Then
                        MsgBox("Member has no variant data loaded")
                    Else
                        p_VariantLoaded = True
                    End If
                Else
                    MsgBox("Member with FTDNA Kit number " & FTDNAID & " has no variant loaded!")
                End If
            End If
        Else
            MsgBox("Could not load member with FTDNA Kit number " & FTDNAID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithYFullID(ByVal YFullID As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_VariantLoaded = False
        p_ds = cDataAccess.GetMemberByYFullID(YFullKit)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_YFullKit = YFullID
                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("MemberName")
                Else
                    MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("FTDNAKit") = False Then
                    p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAKit")
                Else
                    MsgBox("This Member has no FTDNA kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    MsgBox("This Member has no ID!") 'should not realy happen
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsID") = False Then
                    'p_MutationsIDs = p_ds.Tables(0).Rows(0).Item("MutationsID") 'find a proper way to get all the mutationsIDs into an array
                Else
                    MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsID") = False Then
                    'p_PrivateMutationsIDs = p_ds.Tables(0).Rows(0).Item("PrivateMutationsID") 'find a proper way to get all the private mutationsIDs into an array
                Else
                    MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsID") = False Then
                    'p_PutativeMutationsIDs = p_ds.Tables(0).Rows(0).Item("PutativeMutationsID") 'find a proper way to get all the putative mutationsIDs into an array
                Else
                    MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                'Now see if this person has variant records stored
                p_dsPositions = cDataAccess.GetPositionsByMemberID38(p_ID)
                If Not IsNothing(p_dsPositions) Then
                    If Not p_dsPositions.Tables(0).Rows.Count > 0 Then
                        MsgBox("Member has no variant data loaded")
                    Else
                        p_VariantLoaded = True
                    End If
                Else
                    MsgBox("Member with YFull Kit number " & YFullID & " has no variant loaded!")
                End If
            End If
        Else
            MsgBox("Could not load member with YFull Kit number " & YFullID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithID(ByVal memberID As String) 'load from the VariantDB
        Dim cDataAccess As New clsDataAccess

        p_VariantLoaded = False
        p_ds = cDataAccess.GetMemberByID(memberID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_ID = memberID
                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("MemberName")
                Else
                    MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                    p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAID")
                Else
                    MsgBox("This Member has no FTDNA kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                    p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullID")
                Else
                    MsgBox("This Member has no YFull kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsID") = False Then
                    'p_MutationsIDs = p_ds.Tables(0).Rows(0).Item("MutationsID") 'find a proper way to get all the mutationsIDs into an array
                Else
                    MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsID") = False Then
                    'p_PrivateMutationsIDs = p_ds.Tables(0).Rows(0).Item("PrivateMutationsID") 'find a proper way to get all the private mutationsIDs into an array
                Else
                    MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsID") = False Then
                    'p_PutativeMutationsIDs = p_ds.Tables(0).Rows(0).Item("PutativeMutationsID") 'find a proper way to get all the putative mutationsIDs into an array
                Else
                    MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsSavedToDB") = False Then ' a bit useless!
                    p_IsSavedToDB = p_ds.Tables(0).Rows(0).Item("IsSavedToDB")
                Else

                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                'Now see if this person has variant records stored
                p_dsPositions = cDataAccess.GetPositionsByMemberID38(memberID)
                If Not IsNothing(p_dsPositions) Then
                    If Not p_dsPositions.Tables(0).Rows.Count > 0 Then
                        MsgBox("Member has no variant data loaded")
                    Else
                        p_VariantLoaded = True
                    End If
                Else
                    MsgBox("Member with ID " & memberID & " has no variant loaded!")
                End If
            End If
        Else
            MsgBox("Could not load member with ID " & memberID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Private Function AlreadyExistsInDB() As String 'returns the ID if exists, "" if not
        Dim ds As DataSet
        Dim cDataAccess As New clsDataAccess

        ds = cDataAccess.GetMemberByName(p_Name)
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("ID")
            End If
        End If
        ds = Nothing
        ds = cDataAccess.GetMemberByFTDNAID(p_FTDNAKit)
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("ID")
            End If
        End If
        ds = Nothing
        ds = cDataAccess.GetMemberByYFullID(p_YFullKit)
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("ID")
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Public Sub SavetoDB()  'we save the member and eventual changes in the Member table of the VariantDB, not the variant positions
        Dim cDataAccess As New clsDataAccess

        If p_ID = "" Then 'insert - this should not realy happen here!
            'Save as new Member, but check if exists in first
            p_ID = AlreadyExistsInDB()
            If p_ID = "" Then 'This is an insert, but should not happen!
                cDataAccess.InsertMember(p_Name, p_FTDNAKit, p_YFullKit, p_MutationsIDs, p_PrivateMutationsIDs, p_PutativeMutationsIDs, p_CurrentParentNodeID, p_IsPlacedInTheTree)
                p_ID = AlreadyExistsInDB() 'now should have got a ID!
            Else 'This is an update
                cDataAccess.UpdateMember(p_Name, p_FTDNAKit, p_YFullKit, p_MutationsIDs, p_PrivateMutationsIDs, p_PutativeMutationsIDs, p_CurrentParentNodeID, p_IsPlacedInTheTree, p_ID)
            End If
        Else
            'Save updates
            cDataAccess.UpdateMember(p_Name, p_FTDNAKit, p_YFullKit, p_MutationsIDs, p_PrivateMutationsIDs, p_PutativeMutationsIDs, p_CurrentParentNodeID, p_IsPlacedInTheTree, p_ID)
        End If
        p_IsSavedToDB = True
    End Sub

    Protected Overrides Sub Finalize()
        If p_IsSavedToDB = False Then
            If MsgBox("Member " & p_Name & " has been modified! Do you want to save changes to the DB?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'do it
                Me.SavetoDB()
            End If
        End If
    End Sub

End Class
