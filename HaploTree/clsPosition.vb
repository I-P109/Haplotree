Imports HaploTree

Public Class Position
    Private p_ID As String
    Private p_PosHg19 As Long
    Private p_PosHg38 As Long
    Private p_AncestrallCall As String 'or reference call
    Private p_IsSavedToDB As Boolean

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
    End Sub

    Public Sub New(ByVal PosID As String, ByVal Pos19 As String, ByVal Pos38 As String, ByVal AncestCall As String)
        p_ID = PosID
        p_PosHg19 = Pos19
        p_PosHg38 = Pos38
        p_AncestrallCall = AncestCall
        p_IsSavedToDB = False
    End Sub

    Public Sub LoadWithID(ByVal PosID As String) 'load from the DB
        'do it
        MsgBox("we need to load a position with ID " & PosID & " from db")
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithPos19(ByVal Pos19 As Long) 'load from the DB
        'do it
        MsgBox("we need to load a position with Pos19 " & Pos19 & " from db")
        p_IsSavedToDB = True
    End Sub

    Public Sub LoadWithPos38(ByVal Pos38 As Long) 'load from the DB
        'do it
        MsgBox("we need to load a position with Pos38 " & Pos38 & " from db")
        p_IsSavedToDB = True
    End Sub

    Public Sub SavetoDB()
        'do it
        MsgBox("we need to save changes to position " & p_ID & " to the db")
        If p_ID = "" Then
            'Save as new position

        Else
            'Save updates

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
