Imports HaploTree

Public Class Member
    Private p_ID As Integer
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
    Private p_dsVariant38 As DataSet 'from Position38 table in VariantDB
    Private p_dsVariant19 As DataSet 'from Position19 table in VariantDB
    Private p_Variant38Loaded As Boolean
    Private p_Variant19Loaded As Boolean
    Private p_NbVariant38 As Integer
    Private p_NbVariant19 As Integer
    Private p_HasVariant38 As Boolean
    Private p_HasVariant19 As Boolean

    Public Property PutativeMutationsIDs As String()
        Get
            Return p_PutativeMutationsIDs
        End Get
        Set(value As String())
            Dim i As Integer
            If Not IsNothing(value) Then
                ReDim p_PutativeMutationsIDs(UBound(value))
                For i = 0 To UBound(value)
                    p_PutativeMutationsIDs(i) = value(i)
                Next
            Else
                Dim EmptyStrArray(0) As String
                p_PutativeMutationsIDs = EmptyStrArray
            End If
            p_IsSavedToDB = False
        End Set
    End Property

    Public Sub AppendPutativeMutationsID(NewPutativeMutationsID As String) 'we may need to check that the mutation ID is not already in the list??
        If Not NewPutativeMutationsID = "" Then
            If Me.HasPutativeMutation(NewPutativeMutationsID) = False Then
                If IsNothing(p_PutativeMutationsIDs) Then
                    ReDim p_PutativeMutationsIDs(1)
                Else

                    ReDim Preserve p_PutativeMutationsIDs(UBound(p_PutativeMutationsIDs) + 1)
                End If
                p_PutativeMutationsIDs(UBound(p_PutativeMutationsIDs)) = NewPutativeMutationsID
                p_IsSavedToDB = False
            End If
        End If
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
                For i = 0 To UBound(p_PutativeMutationsIDs)
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
            If Not IsNothing(value) Then
                ReDim p_MutationsIDs(UBound(value))
                For i = 0 To UBound(value)
                    p_MutationsIDs(i) = value(i)
                Next
            Else
                Dim EmptyStrArray(0) As String
                p_MutationsIDs = EmptyStrArray
            End If
            p_IsSavedToDB = False
        End Set
    End Property

    Public ReadOnly Property AllPrivateMutationsIDs As String
        Get
            Return GetStringCommaDelimitedFromArray(p_PrivateMutationsIDs)
        End Get
    End Property

    Public ReadOnly Property AllPutativeMutationsIDs As String
        Get
            Return GetStringCommaDelimitedFromArray(p_PutativeMutationsIDs)
        End Get
    End Property

    Public ReadOnly Property AllMutationsIDs As String
        Get
            Return GetStringCommaDelimitedFromArray(p_MutationsIDs)
        End Get
    End Property

    Private Function GetStringCommaDelimitedFromArray(StringArray() As String) As String
        Dim str As String
        Dim i As Integer
        Dim IsFirst As Boolean = True
        str = ""
        If Not IsNothing(StringArray) Then
            For i = 0 To StringArray.Count - 1
                If Not StringArray(i) = "" Then
                    If IsFirst = True Then
                        str = StringArray(i)
                        IsFirst = False
                    Else
                        str = str & "," & StringArray(i)
                    End If
                End If
            Next
        End If
        Return str
    End Function

    Public Sub AppendMutationsID(NewMutationsID As String) 'we may need to check that the mutation ID is not already in the list??
        If Not NewMutationsID = "" Then
            If Me.HasMutation(NewMutationsID) = False Then
                If IsNothing(p_MutationsIDs) Then
                    ReDim p_MutationsIDs(1)
                Else
                    ReDim Preserve p_MutationsIDs(UBound(p_MutationsIDs) + 1)
                End If
                p_MutationsIDs(UBound(p_MutationsIDs)) = NewMutationsID
                p_IsSavedToDB = False
            End If
        End If
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
                For i = 0 To UBound(p_MutationsIDs)
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

    Public ReadOnly Property ID As Integer
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
            If Not IsNothing(value) Then
                ReDim p_PrivateMutationsIDs(UBound(value))
                For i = 0 To UBound(value)
                    p_PrivateMutationsIDs(i) = value(i)
                Next
            Else
                Dim EmptyStrArray(0) As String
            p_MutationsIDs = EmptyStrArray
            End If
            p_IsSavedToDB = False
        End Set
    End Property

    Public Sub AppendPrivateMutationsID(NewPrivateMutationsID As String)
        If Not NewPrivateMutationsID = "" Then
            If Me.HasPrivateMutation(NewPrivateMutationsID) = False Then
                If IsNothing(p_PrivateMutationsIDs) Then
                    ReDim p_PrivateMutationsIDs(1)
                Else
                    ReDim Preserve p_PrivateMutationsIDs(UBound(p_PrivateMutationsIDs) + 1)
                End If
                p_PrivateMutationsIDs(UBound(p_PrivateMutationsIDs)) = NewPrivateMutationsID
                p_IsSavedToDB = False
            End If
        End If
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
                For i = 0 To UBound(p_PrivateMutationsIDs)
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

    Public ReadOnly Property Variant38Loaded As Boolean
        Get
            Return p_Variant38Loaded
        End Get
    End Property

    Public ReadOnly Property Variant19Loaded As Boolean
        Get
            Return p_Variant19Loaded
        End Get
    End Property

    Public ReadOnly Property NbVariant38 As Integer
        Get
            Return p_NbVariant38
        End Get

    End Property

    Public ReadOnly Property NbVariant19 As Integer
        Get
            Return p_NbVariant19
        End Get

    End Property

    Public ReadOnly Property HasVariant38 As Boolean
        Get
            Return p_HasVariant38
        End Get
    End Property

    Public ReadOnly Property HasVariant19 As Boolean
        Get
            Return p_HasVariant19
        End Get
    End Property

    Public Sub New()
        p_ID = 0
        p_Name = ""
        p_FTDNAKit = "-999"
        p_YFullKit = "-999"
        p_CurrentParentNodeID = ""
        p_MutationsIDs = Nothing
        p_PutativeMutationsIDs = Nothing
        p_PrivateMutationsIDs = Nothing
        p_IsSavedToDB = False
        p_IsPlacedInTheTree = False
        p_Variant38Loaded = False
        p_Variant19Loaded = False
        p_ds = Nothing
        p_dsVariant38 = Nothing
        p_dsVariant19 = Nothing
        p_NbVariant38 = 0
        p_NbVariant19 = 0
        p_HasVariant38 = False
        p_HasVariant19 = False
    End Sub

    Public Sub New(ByVal memberID As Integer, ByVal memberName As String, Optional ByVal FTDNAkitval As String = "-999", Optional ByVal YFullKitval As String = "-999")
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
        p_Variant38Loaded = False
        p_Variant19Loaded = False
        p_ds = Nothing
        p_dsVariant38 = Nothing
        p_dsVariant19 = Nothing
        p_NbVariant38 = 0
        p_NbVariant19 = 0
        p_HasVariant38 = False
        p_HasVariant19 = False
    End Sub

    Public Function HasMutation(MutationID As String) As Boolean
        Dim MutID As String
        Dim HasMut As Boolean

        HasMut = False
        If Not IsNothing(p_MutationsIDs) Then
            For Each MutID In p_MutationsIDs
                If MutID = MutationID Then
                    HasMut = True
                    Exit For
                End If
            Next
        End If
        Return HasMut
    End Function

    Public Function HasPrivateMutation(MutationID As String) As Boolean
        Dim MutID As String
        Dim HasMut As Boolean

        HasMut = False
        If Not IsNothing(p_PrivateMutationsIDs) Then
            For Each MutID In p_PrivateMutationsIDs
                If MutID = MutationID Then
                    HasMut = True
                    Exit For
                End If
            Next
        End If
        Return HasMut
    End Function

    Public Function HasPutativeMutation(MutationID As String) As Boolean
        Dim MutID As String
        Dim HasMut As Boolean

        HasMut = False
        If Not IsNothing(p_PutativeMutationsIDs) Then
            For Each MutID In p_PutativeMutationsIDs
                If MutID = MutationID Then
                    HasMut = True
                    Exit For
                End If
            Next
        End If
        Return HasMut
    End Function

    Public Sub LoadVariantsHG38only(MemberID As Integer) ' only when a member is already loaded
        Dim cDataAccess As New clsDataAccess

        p_Variant38Loaded = False
        p_ds = cDataAccess.GetMemberByID(MemberID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg38") = False Then
                    If p_ds.Tables(0).Rows(0).Item("HasVariantHg38") = True Then
                        If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                            If Not p_Name = p_ds.Tables(0).Rows(0).Item("MemberName") Then
                                MsgBox("Member Name Mismatch!")
                                Exit Sub
                            End If
                        Else
                            'MsgBox("This Member has no name!")
                        End If

                        If p_ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                            If Not p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAID") Then
                                MsgBox("Member FTDNA kit number Mismatch!")
                                Exit Sub
                            End If
                        Else
                            'MsgBox("This Member has no FTDNA kit number!")
                        End If

                        If p_ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                            If Not p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullID") Then
                                MsgBox("Member YFull kit number Mismatch!")
                                Exit Sub
                            End If
                        Else
                            'MsgBox("This Member has no YFull kit number!")
                        End If

                        'Now see if this person has records stored
                        p_dsVariant38 = cDataAccess.GetHg38VariantsByMemberID(MemberID)
                        If Not IsNothing(p_dsVariant38) Then
                            If Not p_dsVariant38.Tables(0).Rows.Count > 0 Then
                                MsgBox("Member has no variant38 data loaded!")
                                Exit Sub
                            End If
                            p_HasVariant38 = True
                            p_Variant38Loaded = True
                            p_NbVariant38 = p_dsVariant38.Tables(0).Rows.Count
                        Else
                            MsgBox("Member with ID " & MemberID & " has no variant38 loaded!")
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Could not load member with ID " & MemberID & "!")
        End If
    End Sub

    Public Sub LoadVariantsHG19only(MemberID As Integer) ' only when a member is already loaded
        Dim cDataAccess As New clsDataAccess

        p_Variant19Loaded = False
        p_ds = cDataAccess.GetMemberByID(MemberID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg19") = False Then
                    If p_ds.Tables(0).Rows(0).Item("HasVariantHg19") = True Then
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
                        p_dsVariant19 = cDataAccess.GetHg19VariantsByMemberID(MemberID)
                        If Not IsNothing(p_dsVariant19) Then
                            If Not p_dsVariant19.Tables(0).Rows.Count > 0 Then
                                MsgBox("Member has no variant19 data loaded!")
                                Exit Sub
                            End If
                            p_HasVariant19 = True
                            p_Variant19Loaded = True
                            p_NbVariant19 = p_dsVariant19.Tables(0).Rows.Count
                        Else
                            MsgBox("Member with ID " & MemberID & " has no variant19 loaded!")
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Could not load member with ID " & MemberID & "!")
        End If
    End Sub

    Public Function GetPositionHg38AtRow(Itemi As Integer) As String 'from Variant38
        If Not IsNothing(p_dsVariant38) Then
            Return p_dsVariant38.Tables(0).Rows(Itemi).Item("Pos")
        Else
            Return ""
        End If
    End Function

    Public Function GetFullAltCallHg38AtPositionHg38(PosHg38 As String) As String 'from Variant38
        If Not IsNothing(p_dsVariant38) Then
            Dim i As Integer
            Dim Itemi As Integer
            Itemi = -1
            For i = 0 To p_dsVariant38.Tables(0).Rows.Count - 1
                If p_dsVariant38.Tables(0).Rows(i).Item("Pos") = PosHg38 Then
                    Itemi = i
                    Exit For
                End If
            Next
            If Itemi = -1 Then 'not found
                Return ""
            Else
                Return p_dsVariant38.Tables(0).Rows(Itemi).Item("Alt")
            End If
        Else
            Return ""
        End If
    End Function

    Public Function GetFullAltCallAtPositionHg38(PosHg38 As String) As String 'prefarably from Variant38, returns from Variant19 if no Hh38 loaded
        If p_Variant38Loaded = True Then
            Dim i As Integer
            Dim Itemi As Integer
            Itemi = -1
            For i = 0 To p_dsVariant38.Tables(0).Rows.Count - 1
                If p_dsVariant38.Tables(0).Rows(i).Item("Pos") = PosHg38 Then
                    Itemi = i
                    Exit For
                End If
            Next
            If Itemi = -1 Then 'not found
                Return ""
            Else
                Return p_dsVariant38.Tables(0).Rows(Itemi).Item("Alt")
            End If
        Else
            If p_Variant19Loaded = True Then
                Dim i As Integer
                Dim Itemi As Integer
                Itemi = -1
                For i = 0 To p_dsVariant19.Tables(0).Rows.Count - 1
                    If p_dsVariant19.Tables(0).Rows(i).Item("Pos") = ConvertHG38ToHG19(CLng(PosHg38)) Then
                        Itemi = i
                        Exit For
                    End If
                Next
                If Itemi = -1 Then 'not found
                    Return ""
                Else
                    Return p_dsVariant19.Tables(0).Rows(Itemi).Item("Alt")
                End If
            Else
                Return ""
            End If
        End If
    End Function

    Public Function GetFullAltCallHg19AtPositionHg38(PosHg38 As String) As String 'from Variant19
        If Not IsNothing(p_dsVariant19) Then
            Dim i As Integer
            Dim Itemi As Integer
            Itemi = -1
            For i = 0 To p_dsVariant19.Tables(0).Rows.Count - 1
                If p_dsVariant19.Tables(0).Rows(i).Item("Pos") = PosHg38 Then
                    Itemi = i
                    Exit For
                End If
            Next
            If Itemi = -1 Then 'not found
                Return ""
            Else
                Return p_dsVariant19.Tables(0).Rows(Itemi).Item("Alt")
            End If
        Else
            Return ""
        End If
    End Function

    Public Function GetRefCallHg38AtRow(Itemi As Integer) As String 'from Variant38
        If Not IsNothing(p_dsVariant38) Then
            Dim str As String
            Dim strArray As String()

            strArray = GetStringArrayCommaDelimited(p_dsVariant38.Tables(0).Rows(Itemi).Item("Ref"))
            str = strArray(0)
            Return Left(str, 1)
        Else
            Return ""
        End If
    End Function

    Public Function GetAltCallHg38AtRow(Itemi As Integer) As String 'from Variant38
        If Not IsNothing(p_dsVariant38) Then
            Dim str As String
            Dim strArray As String()

            strArray = GetStringArrayCommaDelimited(p_dsVariant38.Tables(0).Rows(Itemi).Item("Alt"))
            str = strArray(0)
            Return Left(str, 1)
        Else
            Return ""
        End If
    End Function

    Public Function GetPositionHg19AtRow(Itemi As Integer) As String 'from Variant38
        If Not IsNothing(p_dsVariant19) Then
            Return p_dsVariant19.Tables(0).Rows(Itemi).Item("Pos")
        Else
            Return ""
        End If
    End Function

    Public Function GetRefCallHg19AtRow(Itemi As Integer) As String 'from Variant38
        If Not IsNothing(p_dsVariant19) Then
            Return p_dsVariant19.Tables(0).Rows(Itemi).Item("Ref")
        Else
            Return ""
        End If
    End Function

    Public Function GetAltCallHg19AtRow(Itemi As Integer) As String 'from Variant38
        If Not IsNothing(p_dsVariant19) Then
            Return p_dsVariant19.Tables(0).Rows(Itemi).Item("Alt")
        Else
            Return ""
        End If
    End Function

    Public Function GetAltCallDepthHg38AtRow(Itemi As Integer) As String 'only for Hg38, and only main Altcall
        If Not IsNothing(p_dsVariant38) Then
            Dim Str As String
            Dim Str2 As String
            Dim StrArray As String()
            Dim StrArray2 As String()

            Str = p_dsVariant38.Tables(0).Rows(Itemi).Item("Mutation")
            StrArray = Str.Split(";")
            Str2 = StrArray(1)
            StrArray2 = Str2.Split(",")
            Return StrArray2(1)
        Else
            Return ""
        End If
    End Function

    Public Function GetRefCallDepthHg38AtRow(Itemi As Integer) As String 'only for Hg38
        If Not IsNothing(p_dsVariant38) Then
            Dim Str As String
            Dim Str2 As String
            Dim StrArray As String()
            Dim StrArray2 As String()

            Str = p_dsVariant38.Tables(0).Rows(Itemi).Item("Mutation")
            StrArray = Str.Split(";")
            Str2 = StrArray(1)
            StrArray2 = Str2.Split(",")
            Return StrArray2(0)
        Else
            Return ""
        End If
    End Function

    Public Function GetTotalDepthHg38AtRow(Itemi As Integer) As String
        If Not IsNothing(p_dsVariant38) Then
            Dim Str As String
            Dim StrArray As String()

            Str = p_dsVariant38.Tables(0).Rows(Itemi).Item("Mutation")
            StrArray = Str.Split(";")
            Return StrArray(2)
        Else
            Return ""
        End If
    End Function

    Public Sub LoadWithName(ByVal memberName As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_Variant38Loaded = False
        p_Variant19Loaded = False
        p_ds = cDataAccess.GetMemberByName(memberName)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_Name = memberName
                If p_ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                    p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAID")
                Else
                    'MsgBox("This Member has no FTDNA kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                    p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullID")
                Else
                    'MsgBox("This Member has no YFull kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    'MsgBox("This Member has no ID!") 'should not realy happen
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    p_MutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("MutationsIDs"))
                Else
                    'MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsIDs") = False Then
                    p_PrivateMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PrivateMutationsIDs"))
                Else
                    'MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsIDs") = False Then
                    p_PutativeMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PutativeMutationsIDs"))
                Else
                    'MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    'MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg38") = False Then
                    p_HasVariant38 = p_ds.Tables(0).Rows(0).Item("HasVariantHg38")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant38 = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg19") = False Then
                    p_HasVariant19 = p_ds.Tables(0).Rows(0).Item("HasVariantHg19")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant19 = False
                End If

                'Now see if this person has variant38 records stored
                p_dsVariant38 = cDataAccess.GetHg38VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant38) Then
                    If Not p_dsVariant38.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant38 data loaded")
                        p_HasVariant38 = False
                    Else
                        p_HasVariant38 = True
                        p_Variant38Loaded = True
                        p_NbVariant38 = p_dsVariant38.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & memberName & " has no variant38 loaded!")
                    p_HasVariant38 = False
                End If

                'Now see if this person has variant19 records stored
                p_dsVariant19 = cDataAccess.GetHg19VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant19) Then
                    If Not p_dsVariant19.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant19 data loaded")
                        p_HasVariant19 = False
                    Else
                        p_HasVariant19 = True
                        p_Variant19Loaded = True
                        p_NbVariant19 = p_dsVariant19.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & memberName & " has no variant19 loaded!")
                    p_HasVariant19 = False
                End If
            End If
        Else
            MsgBox("Could not load member " & memberName & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Private Function GetStringArrayCommaDelimited(MyStringArray As String) As String()

        Return MyStringArray.Split(",")

    End Function

    Public Sub LoadWithFTDNAID(ByVal FTDNAID As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_Variant38Loaded = False
        p_Variant19Loaded = False
        p_ds = cDataAccess.GetMemberByFTDNAID(FTDNAID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_FTDNAKit = FTDNAID
                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("MemberName")
                Else
                    'MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                    p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullID")
                Else
                    'MsgBox("This Member has no YFull kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    'MsgBox("This Member has no ID!") 'should not realy happen
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    p_MutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("MutationsIDs"))
                Else
                    'MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsIDs") = False Then
                    p_PrivateMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PrivateMutationsIDs"))
                Else
                    'MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsIDs") = False Then
                    p_PutativeMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PutativeMutationsIDs"))
                Else
                    'MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    'MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg38") = False Then
                    p_HasVariant38 = p_ds.Tables(0).Rows(0).Item("HasVariantHg38")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant38 = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg19") = False Then
                    p_HasVariant19 = p_ds.Tables(0).Rows(0).Item("HasVariantHg19")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant19 = False
                End If

                'Now see if this person has variant38 records stored
                p_dsVariant38 = cDataAccess.GetHg38VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant38) Then
                    If Not p_dsVariant38.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant38 data loaded")
                        p_HasVariant38 = False
                    Else
                        p_Variant38Loaded = True
                        p_HasVariant38 = True
                        p_NbVariant38 = p_dsVariant38.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & p_Name & " has no variant38 loaded!")
                    p_HasVariant38 = False
                End If

                'Now see if this person has variant19 records stored
                p_dsVariant19 = cDataAccess.GetHg19VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant19) Then
                    If Not p_dsVariant19.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant19 data loaded")
                        p_HasVariant19 = False
                    Else
                        p_Variant19Loaded = True
                        p_HasVariant19 = True
                        p_NbVariant19 = p_dsVariant19.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & p_Name & " has no variant19 loaded!")
                    p_HasVariant19 = False
                End If
            End If
        Else
            MsgBox("Could not load member with FTDNA Kit number " & FTDNAID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithYFullID(ByVal YFullID As String) 'load from the DB
        Dim cDataAccess As New clsDataAccess

        p_Variant38Loaded = False
        p_Variant19Loaded = False
        p_ds = cDataAccess.GetMemberByYFullID(YFullKit)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_YFullKit = YFullID
                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("MemberName")
                Else
                    'MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                    p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAID")
                Else
                    'MsgBox("This Member has no FTDNA kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    'MsgBox("This Member has no ID!") 'should not realy happen
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    p_MutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("MutationsIDs"))
                Else
                    'MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsIDs") = False Then
                    p_PrivateMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PrivateMutationsIDs"))
                Else
                    'MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsIDs") = False Then
                    p_PutativeMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PutativeMutationsIDs"))
                Else
                    'MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    'MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg38") = False Then
                    p_HasVariant38 = p_ds.Tables(0).Rows(0).Item("HasVariantHg38")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant38 = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg19") = False Then
                    p_HasVariant19 = p_ds.Tables(0).Rows(0).Item("HasVariantHg19")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant19 = False
                End If

                'Now see if this person has variant38 records stored
                p_dsVariant38 = cDataAccess.GetHg38VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant38) Then
                    If Not p_dsVariant38.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant38 data loaded")
                        p_HasVariant38 = False
                    Else
                        p_Variant38Loaded = True
                        p_HasVariant38 = True
                        p_NbVariant38 = p_dsVariant38.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & p_Name & " has no variant38 loaded!")
                    p_HasVariant38 = False
                End If

                'Now see if this person has variant19 records stored
                p_dsVariant19 = cDataAccess.GetHg19VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant19) Then
                    If Not p_dsVariant19.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant19 data loaded")
                        p_HasVariant19 = False
                    Else
                        p_Variant19Loaded = True
                        p_HasVariant19 = True
                        p_NbVariant19 = p_dsVariant19.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & p_Name & " has no variant19 loaded!")
                    p_HasVariant19 = False
                End If
            End If
        Else
            MsgBox("Could not load member with YFull Kit number " & YFullID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithID(ByVal memberID As Integer) 'load from the VariantDB
        Dim cDataAccess As New clsDataAccess

        p_Variant38Loaded = False
        p_Variant19Loaded = False
        p_ds = cDataAccess.GetMemberByID(memberID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_ID = memberID
                If p_ds.Tables(0).Rows(0).IsNull("MemberName") = False Then
                    p_Name = p_ds.Tables(0).Rows(0).Item("MemberName")
                Else
                    'MsgBox("This Member has no name!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("FTDNAID") = False Then
                    p_FTDNAKit = p_ds.Tables(0).Rows(0).Item("FTDNAID")
                Else
                    'MsgBox("This Member has no FTDNA kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("YFullID") = False Then
                    p_YFullKit = p_ds.Tables(0).Rows(0).Item("YFullID")
                Else
                    'MsgBox("This Member has no YFull kit number!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("MutationsIDs") = False Then
                    p_MutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("MutationsIDs"))
                Else
                    'MsgBox("This Member has no Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PrivateMutationsIDs") = False Then
                    p_PrivateMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PrivateMutationsIDs"))
                Else
                    'MsgBox("This Member has no Private Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PutativeMutationsIDs") = False Then
                    p_PutativeMutationsIDs = GetStringArrayCommaDelimited(p_ds.Tables(0).Rows(0).Item("PutativeMutationsIDs"))
                Else
                    'MsgBox("This Member has no Putative Mutations Loaded!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("CurrentParentnodeID") = False Then
                    p_CurrentParentNodeID = p_ds.Tables(0).Rows(0).Item("CurrentParentnodeID")
                Else
                    'MsgBox("This Member has no CurrentParentnodeID yet!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("IsPlacedInTheTree") = False Then
                    p_IsPlacedInTheTree = p_ds.Tables(0).Rows(0).Item("IsPlacedInTheTree")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_IsPlacedInTheTree = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg38") = False Then
                    p_HasVariant38 = p_ds.Tables(0).Rows(0).Item("HasVariantHg38")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant38 = False
                End If

                If p_ds.Tables(0).Rows(0).IsNull("HasVariantHg19") = False Then
                    p_HasVariant19 = p_ds.Tables(0).Rows(0).Item("HasVariantHg19")
                Else
                    'MsgBox("This Member is not placed in the tree yet!")
                    p_HasVariant19 = False
                End If

                'Now see if this person has variant38 records stored
                p_dsVariant38 = cDataAccess.GetHg38VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant38) Then
                    If Not p_dsVariant38.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant38 data loaded")
                        p_HasVariant38 = False
                    Else
                        p_Variant38Loaded = True
                        p_HasVariant38 = True
                        p_NbVariant38 = p_dsVariant38.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & p_Name & " has no variant38 loaded!")
                    p_HasVariant38 = False
                End If

                'Now see if this person has variant19 records stored
                p_dsVariant19 = cDataAccess.GetHg19VariantsByMemberID(p_ID)
                If Not IsNothing(p_dsVariant19) Then
                    If Not p_dsVariant19.Tables(0).Rows.Count > 0 Then
                        'MsgBox("Member has no variant19 data loaded")
                        p_HasVariant19 = False
                    Else
                        p_Variant19Loaded = True
                        p_HasVariant19 = True
                        p_NbVariant19 = p_dsVariant19.Tables(0).Rows.Count
                    End If
                Else
                    'MsgBox("Member " & p_Name & " has no variant19 loaded!")
                    p_HasVariant19 = False
                End If
            End If
        Else
            MsgBox("Could not load member with ID " & memberID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Private Function AlreadyExistsInDB() As Integer 'returns the ID if exists, "" if not
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
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Public Sub SavetoDB()  'we save the member and eventual changes in the Member table of the VariantDB, not the variant positions and reads
        Dim cDataAccess As New clsDataAccess

        If p_IsSavedToDB = False Then
            If p_ID = 0 Then 'insert - this should not realy happen here!
                'Save as new Member, but check if exists in first
                p_ID = AlreadyExistsInDB()
                If p_ID = 0 Then 'This is an insert, but should not happen!
                    cDataAccess.InsertMember(p_Name, p_FTDNAKit, p_YFullKit, AllMutationsIDs, AllPrivateMutationsIDs, AllPutativeMutationsIDs, p_CurrentParentNodeID, p_IsPlacedInTheTree, p_HasVariant38, p_HasVariant19)
                    p_ID = AlreadyExistsInDB() 'now should have got a ID!
                Else 'This is an update
                    cDataAccess.UpdateMember(p_ID, p_Name, p_FTDNAKit, p_YFullKit, AllMutationsIDs, AllPrivateMutationsIDs, AllPutativeMutationsIDs, p_CurrentParentNodeID, p_IsPlacedInTheTree, p_HasVariant38, p_HasVariant19)
                End If
            Else
                'Save updates
                cDataAccess.UpdateMember(p_ID, p_Name, p_FTDNAKit, p_YFullKit, AllMutationsIDs, AllPrivateMutationsIDs, AllPutativeMutationsIDs, p_CurrentParentNodeID, p_IsPlacedInTheTree, p_HasVariant38, p_HasVariant19)
            End If
            p_IsSavedToDB = True
        End If
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