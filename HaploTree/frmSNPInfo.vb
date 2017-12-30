Public Class frmSNPInfo
    Dim mintID As Integer
    Dim cDataAccess As New clsDataAccess

    Public Property ID() As String
        Get
            Return mintID
        End Get
        Set(ByVal value As String)
            mintID = value
        End Set
    End Property

    Private Sub frmSNPInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class