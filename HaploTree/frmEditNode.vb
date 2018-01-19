Public Class frmEditNode
    Private mOldNodeText As String
    Private mID As Integer
    Dim cDataAccess As New clsDataAccess

    Public Property OldNodeText() As String
        Get
            Return mOldNodeText
        End Get
        Set(ByVal value As String)
            mOldNodeText = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property

    Private Sub frmEditNode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call PopulateForm()
    End Sub

    Public Function PopulateForm()
        Me.txtOldNodeName.Text = OldNodeText
        Me.txtOldNodeName.Enabled = False

    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim blnExists As Boolean
        Dim intReturn As Integer

        Try
            msg = "Change this SNP name from" & Me.txtOldNodeName.Text & " to " & Me.txtNewNodeText.Text & "?"   ' Define message.
            style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo
            title = "SAVE"   ' Define title.

            ' Display message.
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then   ' User chose Yes.
                Call cDataAccess.UpdateSNPName(ID, Me.txtNewNodeText.Text)
                Call cDataAccess.UpdateSNPParentBranch(Me.txtNewNodeText.Text, Me.txtOldNodeName.Text)
            End If
            MsgBox("SNP Name Changed", MsgBoxStyle.Information, "SAVED")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class