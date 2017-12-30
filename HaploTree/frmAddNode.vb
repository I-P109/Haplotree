Public Class frmAddNode

    Private mNewNodeName As String
    Private mNewNodeText As String
    Private mNewNodeTag As String



    Public Property NewNodeText() As String
        Get
            Return mNewNodeText
        End Get
        Set(ByVal value As String)
            mNewNodeText = value
        End Set
    End Property
    Private Sub frmAddNode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If txtNewNodeText.Text <> String.Empty Then
            NewNodeText = txtNewNodeText.Text
        Else
            MessageBox.Show("Provide the new node's text")
            Return
        End If

        'If txtTag.Text <> String.Empty Then
        '    NewNodeTag = txtTag.Text
        'Else
        '    MessageBox.Show("Provide the new node's text")
        '    Return
        'End If

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class