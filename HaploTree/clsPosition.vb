Imports HaploTree

Public Class Position
    Private p_ID As String
    Private p_PosHg19 As Long
    Private p_PosHg38 As Long
    Private p_AncestrallCall As String 'or reference call
    Private p_IsSavedToDB As Boolean
    Private p_ds As DataSet 'from Member table in HaploTreeDB

    Public ReadOnly Property ID As String
        Get
            Return p_ID
        End Get
    End Property

    Public Property PosHg19 As Long
        Get
            Return p_PosHg19
        End Get
        Set(value As Long)
            p_PosHg19 = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property PosHg38 As Long
        Get
            Return p_PosHg38
        End Get
        Set(value As Long)
            p_PosHg38 = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public Property AncestrallCall As String
        Get
            Return p_AncestrallCall
        End Get
        Set(value As String)
            p_AncestrallCall = value
            p_IsSavedToDB = False
        End Set
    End Property

    Public ReadOnly Property IsSavedToDB As Boolean
        Get
            Return p_IsSavedToDB
        End Get
    End Property

    Public Sub New()
        p_ID = ""
        p_PosHg19 = -999
        p_PosHg38 = -999
        p_AncestrallCall = ""
        p_IsSavedToDB = False
        p_ds = Nothing
    End Sub

    Public Sub New(ByVal PosID As String, ByVal Pos19 As String, ByVal Pos38 As String, ByVal AncestCall As String)
        p_ID = PosID
        p_PosHg19 = Pos19
        p_PosHg38 = Pos38
        p_AncestrallCall = AncestCall
        p_IsSavedToDB = False
        p_ds = Nothing
    End Sub

    Public Sub LoadWithID(ByVal PosID As String) 'load from the HaploTreeDB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetPositionByID(PosID)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_ID = PosID
                If p_ds.Tables(0).Rows(0).IsNull("PosHg19") = False Then
                    p_PosHg19 = p_ds.Tables(0).Rows(0).Item("PosHg19")
                Else
                    MsgBox("This Position has no PosHg19!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PosHg38") = False Then
                    p_PosHg38 = p_ds.Tables(0).Rows(0).Item("PosHg38")
                Else
                    MsgBox("This Position has no PosHg38!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("AncestrallCall") = False Then
                    p_AncestrallCall = p_ds.Tables(0).Rows(0).Item("AncestrallCall")
                Else
                    MsgBox("This Position has no AncestrallCall!")
                End If
            End If
        Else
            MsgBox("Could not load Position with ID " & PosID & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithPos19(ByVal Pos19 As Long) 'load from the HaploTreeDB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetPositionByPosHg19(Pos19)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_PosHg19 = Pos19
                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    MsgBox("This Position has no ID!") 'should be impossible
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PosHg38") = False Then
                    p_PosHg38 = p_ds.Tables(0).Rows(0).Item("PosHg38")
                Else
                    MsgBox("This Position has no PosHg38!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("AncestrallCall") = False Then
                    p_AncestrallCall = p_ds.Tables(0).Rows(0).Item("AncestrallCall")
                Else
                    MsgBox("This Position has no AncestrallCall!")
                End If
            End If
        Else
            MsgBox("Could not load Position with ID " & Pos19 & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithPos38(ByVal Pos38 As Long) 'load from the HaploTreeDB
        Dim cDataAccess As New clsDataAccess

        p_ds = cDataAccess.GetPositionByPosHg38(Pos38)
        If Not IsNothing(p_ds) Then
            If p_ds.Tables(0).Rows.Count > 0 Then
                p_PosHg38 = Pos38
                If p_ds.Tables(0).Rows(0).IsNull("ID") = False Then
                    p_ID = p_ds.Tables(0).Rows(0).Item("ID")
                Else
                    MsgBox("This Position has no ID!") 'should be impossible
                End If

                If p_ds.Tables(0).Rows(0).IsNull("PosHg19") = False Then
                    p_PosHg19 = p_ds.Tables(0).Rows(0).Item("PosHg19")
                Else
                    MsgBox("This Position has no PosHg19!")
                End If

                If p_ds.Tables(0).Rows(0).IsNull("AncestrallCall") = False Then
                    p_AncestrallCall = p_ds.Tables(0).Rows(0).Item("AncestrallCall")
                Else
                    MsgBox("This Position has no AncestrallCall!")
                End If
            End If
        Else
            MsgBox("Could not load Position with ID " & Pos38 & "!")
        End If
        p_IsSavedToDB = True
    End Sub

    Private Function AlreadyExistsInDB() As String 'returns the ID if exists, "" if not
        Dim ds As DataSet
        Dim cDataAccess As New clsDataAccess

        ds = cDataAccess.GetPositionByPosHg38(p_PosHg38)
        If Not IsNothing(ds) Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("ID")
            End If
        End If
        ds = Nothing
        ds = cDataAccess.GetPositionByPos19(p_PosHg19) 'from HaploTreeDB
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

    Public Sub SavetoDB() 'into HaploTreeDB
        Dim cDataAccess As New clsDataAccess

        If p_ID = "" Then 'insert 
            'Save as new Position, but check if exists in first
            p_ID = AlreadyExistsInDB()
            If p_ID = "" Then 'This is an insert
                cDataAccess.InsertPosition(p_PosHg19, p_PosHg38, p_AncestrallCall)
                p_ID = AlreadyExistsInDB() 'now should have got a ID!
            Else 'This is an update
                cDataAccess.UpdatePosition(p_PosHg19, p_PosHg38, p_AncestrallCall, p_ID)
            End If
        Else
            'Save updates
            cDataAccess.UpdatePosition(p_PosHg19, p_PosHg38, p_AncestrallCall, p_ID)
        End If
        p_IsSavedToDB = True
    End Sub

    Protected Overrides Sub Finalize()
        If p_IsSavedToDB = False Then
            If MsgBox("Position " & ID & " has been modified! Do you want to save changes to the DB?") = MsgBoxResult.Ok Then
                'do it
                Me.SavetoDB()
            End If
        End If
    End Sub
End Class
