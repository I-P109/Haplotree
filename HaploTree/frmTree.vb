Public Class frmTree
    Dim cDataAccess As New clsDataAccess
    Private p_SelectedNodeName As String
    Private p_SelectOnly As Boolean 'to handle menus so that only the Select one is visible
    Private p_SelectedMemberID As Integer

    Public Property SelectOnly As Boolean
        Get
            Return p_SelectOnly
        End Get
        Set(value As Boolean)
            p_SelectOnly = value
            If value = False Then
                cmnuAddNode.Enabled = True
                cmnuRemoveNode.Enabled = True
                cmnuAddTextColor.Enabled = True
                cmnuAddBackgroundColor.Enabled = True
                cmnuSNPInfo.Enabled = True
                cmnuEditNodeName.Enabled = True
                cmnuSelectNode.Enabled = False
                p_SelectedNodeName = ""
            Else
                cmnuAddNode.Enabled = False
                cmnuRemoveNode.Enabled = False
                cmnuAddTextColor.Enabled = False
                cmnuAddBackgroundColor.Enabled = False
                cmnuSNPInfo.Enabled = False
                cmnuEditNodeName.Enabled = False
                cmnuSelectNode.Enabled = True
            End If
        End Set
    End Property

    Public ReadOnly Property SelectedNodeName As String
        Get
            Return p_SelectedNodeName
        End Get
    End Property

    Public Property SelectedMemberID As Integer
        Get
            Return p_SelectedMemberID
        End Get
        Set(value As Integer)
            p_SelectedMemberID = value
        End Set
    End Property

    Private Sub frmTree_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = 0
        Me.Top = 0
        'Me.Width = Me.Parent.Width
        'Me.Height = Me.Parent.Height

        Me.tvwTree.Height = Me.Height - 150

        Call PopulateTreeView()
    End Sub

    Private Sub PopulateTreeView()
        Dim ds As New DataSet
        'Dim dsNodes As New DataSet
        'Dim strMainNodeText As String = ""
        'Dim strMainNodeName As String = ""
        Try
            ds = cDataAccess.GetBranchByParentBranch(0)
            If ds.Tables(0).Rows.Count > 0 Then
                'first lets set the main node
                'strMainNodeText = ds.Tables(0).Rows(0).Item("BranchName")

                ' start off by adding a base treeview node
                'Dim mainNode As New TreeNode()
                'mainNode.Name = strMainNodeText
                'mainNode.Text = strMainNodeText
                'Me.tvwTree.Nodes.Add(mainNode)

                'dsNodes = cDataAccess.GetAllTree

                'tvwTree.Update()
                'tvwTree.Nodes.Clear()
                Dim parent As TreeNode
                parent = New TreeNode("Root")
                tvwTree.Nodes.Add(parent)
                AddMoreChildren(parent)

                tvwTree.ExpandAll()
                tvwTree.EndUpdate()
            End If

        Catch ex As Exception
            MsgBox("ERROR:" & ex.Message)
        End Try

    End Sub

    Private Sub AddMoreChildren(ByVal parent As TreeNode)
        Dim ds As New DataSet
        Dim dsExists As DataSet
        Dim adptr As New DataSet
        Try

            'Do an quick select to see if the ParentBranch exists
            dsExists = cDataAccess.ExistsBranchByParentBranch(parent.Text)
            If dsExists.Tables(0).Rows.Count > 0 Then
                ds = cDataAccess.GetBranchByParentBranch(parent.Text)
                '  ds.Clear()
                '  adptr.Fill(ds)
                If ds Is Nothing = False Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("ParentBranch").ToString = parent.Text Then
                                Dim child As TreeNode = New TreeNode(ds.Tables(0).Rows(i).Item("BranchName").ToString)

                                If ds.Tables(0).Rows(i).IsNull("BackgroundColor") = False Then
                                    child.BackColor = Color.FromArgb(CInt(ds.Tables(0).Rows(i).Item("BackgroundColor")))
                                End If

                                If ds.Tables(0).Rows(i).IsNull("TextColor") = False Then
                                    child.ForeColor = Color.FromArgb(CInt(ds.Tables(0).Rows(i).Item("TextColor")))
                                End If

                                'Set the country
                                If ds.Tables(0).Rows(i).IsNull("Country") = False Then
                                    child.ImageIndex = CInt(ds.Tables(0).Rows(i).Item("Country"))
                                Else
                                    child.ImageIndex = 0
                                End If

                                'If ds.Tables(0).Rows(i).IsNull("Tip") = False Then
                                '    child.ToolTipText = ds.Tables(0).Rows(i).Item("Tip")

                                'End If
                                child.Tag = ds.Tables(0).Rows(i).Item("ID").ToString
                                parent.Nodes.Add(child)
                                AddMoreChildren(child)
                            End If
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox("ERROR:" & ex.Message)
        End Try

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Me.tvwTree.SelectedNode Is Nothing Then
            e.Cancel = True
        End If

    End Sub

    Private Sub cmnuAddNode_Click(sender As Object, e As EventArgs) Handles cmnuAddNode.Click
        Dim intReturn As Integer

        Dim n As New frmAddNode()
        n.ShowDialog()
        Dim nod As New TreeNode()
        ' nod.Name = n.NewNodeName.ToString()
        If nod.Text.Length > 0 Then
            nod.Text = n.NewNodeText.ToString()
            'nod.Tag = n.NewNodeTag.ToString()
            n.Close()

            Me.tvwTree.SelectedNode.Nodes.Add(nod)
            tvwTree.SelectedNode.ExpandAll()

            'Insert into the database
            intReturn = cDataAccess.InsertNode(nod.Text, tvwTree.SelectedNode.Text)
            If intReturn > 0 Then
                MsgBox("Node Added")
                ' Call PopulateTreeView()
            End If
        End If
    End Sub

    Private Sub cmnuRemoveNode_Click(sender As Object, e As EventArgs) Handles cmnuRemoveNode.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        msg = "Deleting a node will delete ALL child nodes as well. Continue?"   ' Define message.
        style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        title = "DELETE NODE"   ' Define title.
        ' Display message.
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then   ' User chose Yes.
            Call cDataAccess.DeleteNode(tvwTree.SelectedNode.Text)
            tvwTree.SelectedNode.Remove()
            Call DeleteNodes()
        End If
    End Sub

    Public Sub DeleteNodes()
        Dim ds As DataSet
        Dim strBranchName As String = ""
        Dim i As Integer = 0
        Dim blnExists As Boolean = True

        'Get a list of distint parent branches.  If they do not exist as a branchname then delete them
        ds = cDataAccess.GetDistinctParentBranch()
        If ds.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds.Tables(0).Rows.Count - 1
                strBranchName = ds.Tables(0).Rows(i).Item("ParentBranch")
                blnExists = cDataAccess.CheckExistingBranch(strBranchName)
                If blnExists = True Then
                    'Do nothing as they exist.

                ElseIf blnExists = False Then
                    'They do not exist so delete.
                    Call cDataAccess.DeleteBranchesByParentBranch(strBranchName)
                End If
            Next
        End If
    End Sub

    Private Sub cmnuSNPInfo_Click(sender As Object, e As EventArgs) Handles cmnuSNPInfo.Click
        Dim f As New frmSNPInfo
        f.ID = tvwTree.SelectedNode.Tag
        f.ShowDialog()
    End Sub

    Private Sub cmnuAddBackgroundColor_Click(sender As Object, e As EventArgs) Handles cmnuAddBackgroundColor.Click
        Try


            ' Keeps the user from selecting a custom color.
            Me.ColorDialog1.AllowFullOpen = False

            ' Allows the user to get help. (The default is false.)
            ColorDialog1.ShowHelp = True
            ' ColorDialog1.ShowDialog()

            If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then

                'WORKS
                ' Sets the initial color select to the current text color,
                '  MyDialog.Color = TextBox1.ForeColor
                tvwTree.SelectedNode.BackColor = ColorDialog1.Color

                Dim mycolor As Color = ColorDialog1.Color
                Dim intcolor As Integer

                intcolor = mycolor.ToArgb

                'Write int to DB
                Call cDataAccess.UpdateBackgroundColorByID(tvwTree.SelectedNode.Tag, intcolor)
                '     Call PopulateTreeView()
            End If
        Catch ex As Exception
            MsgBox("ERROR:" & ex.Message)
        End Try
    End Sub

    Private Sub cmnuAddTextColor_Click(sender As Object, e As EventArgs) Handles cmnuAddTextColor.Click
        Try
            ' Keeps the user from selecting a custom color.
            Me.ColorDialog1.AllowFullOpen = False

            ' Allows the user to get help. (The default is false.)
            ColorDialog1.ShowHelp = True
            ' ColorDialog1.ShowDialog()

            If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                If tvwTree.SelectedNode.Tag <> "" Then
                    'WORKS
                    ' Sets the initial color select to the current text color,
                    '  MyDialog.Color = TextBox1.ForeColor
                    '      tvwTree.SelectedNode.BackColor = ColorDialog1.Color
                    tvwTree.SelectedNode.ForeColor = ColorDialog1.Color
                    Dim mycolor As Color = ColorDialog1.Color
                    Dim intcolor As Integer

                    intcolor = mycolor.ToArgb

                    'Write int to DB
                    Call cDataAccess.UpdateTextColorByID(tvwTree.SelectedNode.Tag, intcolor)

                End If
            End If

            '   Call PopulateTreeView()
        Catch ex As Exception
            MsgBox("ERROR:" & ex.Message)
        End Try
    End Sub

    Private Sub cmnuEditNodeName_Click(sender As Object, e As EventArgs) Handles cmnuEditNodeName.Click

        Dim n As New frmEditNode

        n.OldNodeText = tvwTree.SelectedNode.Text
        n.ID = tvwTree.SelectedNode.Tag
        n.ShowDialog()

    End Sub

    Private Sub cmnuSelectNode_Click(sender As Object, e As EventArgs) Handles cmnuSelectNode.Click
        p_SelectedNodeName = tvwTree.SelectedNode.Text

        If p_SelectedMemberID > 0 And Not p_SelectedNodeName = "" Then InsertNewKitInTree(p_SelectedMemberID, p_SelectedNodeName)

        Me.Close()
    End Sub
End Class