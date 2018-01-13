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
        'Find the information and load them here.
        Call PopulateForm
    End Sub


    Public Function PopulateForm()
        Dim ds As DataSet

        ds = cDataAccess.GetBranchesByID(ID)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).IsNull("Pos") = False Then
                Me.txtPosition.Text = ds.Tables(0).Rows(0).Item("Pos")
            End If

            If ds.Tables(0).Rows(0).IsNull("Alt") = False Then
                Me.txtAlt.Text = ds.Tables(0).Rows(0).Item("Alt")
            End If

            If ds.Tables(0).Rows(0).IsNull("Ref") = False Then
                Me.txtRef.Text = ds.Tables(0).Rows(0).Item("Ref")
            End If
        End If


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

        msg = "Save this record?"   ' Define message.
        style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo
        title = "SAVE"   ' Define title.

        ' Display message.
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then   ' User chose Yes.
            Me.Cursor = Cursors.WaitCursor
            'check to see if the required fields are there
            If txtPosition.Text.Trim.Length > 0 Then
                If txtAlt.Text.Trim.Length > 0 Then
                    If txtRef.Text.Trim.Length > 0 Then
                        'Save the record
                        Call SaveRecord

                    Else
                        MsgBox("You need to add a Ref!", MsgBoxStyle.Critical, "ERROR")
                        Me.txtRef.Select()
                    End If
                Else
                    MsgBox("You need to add a Alt!", MsgBoxStyle.Critical, "ERROR")
                    Me.txtAlt.Select()
                End If
            Else
                MsgBox("You need to add a Position!", MsgBoxStyle.Critical, "ERROR")
                Me.txtPosition.Select()
            End If
        End If

        Me.Cursor = Cursors.Default
    End Sub


    Public Function SaveRecord()
        Dim intReturn As Integer
        Try
            intReturn = cDataAccess.UpdateSNPInfo(ID, Me.txtPosition.Text.Trim, Me.txtAlt.Text.Trim, Me.txtRef.Text.Trim)
            If intReturn > 0 Then
                MsgBox("Record Has been Saved", MsgBoxStyle.Critical, "SUCCESS")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
End Class