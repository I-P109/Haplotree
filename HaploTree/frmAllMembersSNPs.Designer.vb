<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAllMembersSNPs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnSelectMembers = New System.Windows.Forms.Button()
        Me.lblID = New System.Windows.Forms.Label()
        Me.tabMembersSNPs = New System.Windows.Forms.TabControl()
        Me.tabSNPs = New System.Windows.Forms.TabPage()
        Me.lblPassingPositions = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnFindPosition = New System.Windows.Forms.Button()
        Me.txtFindPosition = New System.Windows.Forms.TextBox()
        Me.lvwSNPs = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuReorder = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabMembersWithSNP = New System.Windows.Forms.TabPage()
        Me.lvwMembersWithSNP = New System.Windows.Forms.ListView()
        Me.btnSelectMutations = New System.Windows.Forms.Button()
        Me.btnLoadAllBigYHg19 = New System.Windows.Forms.Button()
        Me.btnLoadTreeNode = New System.Windows.Forms.Button()
        Me.ckbxMutations = New System.Windows.Forms.CheckBox()
        Me.ckbxMembers = New System.Windows.Forms.CheckBox()
        Me.btnSaveChanges = New System.Windows.Forms.Button()
        Me.btnAllowEdit = New System.Windows.Forms.Button()
        Me.mnuSetToAltCallToolStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveAltCall = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRenameMutation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRenameNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabMembersSNPs.SuspendLayout()
        Me.tabSNPs.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.tabMembersWithSNP.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSelectMembers
        '
        Me.btnSelectMembers.Location = New System.Drawing.Point(219, 39)
        Me.btnSelectMembers.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSelectMembers.Name = "btnSelectMembers"
        Me.btnSelectMembers.Size = New System.Drawing.Size(198, 28)
        Me.btnSelectMembers.TabIndex = 25
        Me.btnSelectMembers.Text = "Select Members"
        Me.btnSelectMembers.UseVisualStyleBackColor = True
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(211, 31)
        Me.lblID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(0, 17)
        Me.lblID.TabIndex = 24
        '
        'tabMembersSNPs
        '
        Me.tabMembersSNPs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabMembersSNPs.Controls.Add(Me.tabSNPs)
        Me.tabMembersSNPs.Controls.Add(Me.tabMembersWithSNP)
        Me.tabMembersSNPs.Location = New System.Drawing.Point(13, 74)
        Me.tabMembersSNPs.Margin = New System.Windows.Forms.Padding(4)
        Me.tabMembersSNPs.Name = "tabMembersSNPs"
        Me.tabMembersSNPs.SelectedIndex = 0
        Me.tabMembersSNPs.Size = New System.Drawing.Size(1134, 503)
        Me.tabMembersSNPs.TabIndex = 29
        '
        'tabSNPs
        '
        Me.tabSNPs.AutoScroll = True
        Me.tabSNPs.Controls.Add(Me.lblPassingPositions)
        Me.tabSNPs.Controls.Add(Me.Label1)
        Me.tabSNPs.Controls.Add(Me.btnFindPosition)
        Me.tabSNPs.Controls.Add(Me.txtFindPosition)
        Me.tabSNPs.Controls.Add(Me.lvwSNPs)
        Me.tabSNPs.Location = New System.Drawing.Point(4, 25)
        Me.tabSNPs.Margin = New System.Windows.Forms.Padding(4)
        Me.tabSNPs.Name = "tabSNPs"
        Me.tabSNPs.Padding = New System.Windows.Forms.Padding(4)
        Me.tabSNPs.Size = New System.Drawing.Size(1126, 474)
        Me.tabSNPs.TabIndex = 0
        Me.tabSNPs.Text = "SNPs"
        Me.tabSNPs.UseVisualStyleBackColor = True
        '
        'lblPassingPositions
        '
        Me.lblPassingPositions.AutoSize = True
        Me.lblPassingPositions.ForeColor = System.Drawing.Color.Red
        Me.lblPassingPositions.Location = New System.Drawing.Point(152, 14)
        Me.lblPassingPositions.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassingPositions.Name = "lblPassingPositions"
        Me.lblPassingPositions.Size = New System.Drawing.Size(0, 17)
        Me.lblPassingPositions.TabIndex = 59
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 17)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Total Mutations:"
        '
        'btnFindPosition
        '
        Me.btnFindPosition.Enabled = False
        Me.btnFindPosition.Location = New System.Drawing.Point(155, 44)
        Me.btnFindPosition.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFindPosition.Name = "btnFindPosition"
        Me.btnFindPosition.Size = New System.Drawing.Size(100, 23)
        Me.btnFindPosition.TabIndex = 57
        Me.btnFindPosition.Text = "Find"
        Me.btnFindPosition.UseVisualStyleBackColor = True
        '
        'txtFindPosition
        '
        Me.txtFindPosition.Enabled = False
        Me.txtFindPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindPosition.ForeColor = System.Drawing.Color.Blue
        Me.txtFindPosition.Location = New System.Drawing.Point(15, 44)
        Me.txtFindPosition.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFindPosition.Name = "txtFindPosition"
        Me.txtFindPosition.Size = New System.Drawing.Size(132, 23)
        Me.txtFindPosition.TabIndex = 56
        '
        'lvwSNPs
        '
        Me.lvwSNPs.AllowDrop = True
        Me.lvwSNPs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwSNPs.ContextMenuStrip = Me.ContextMenuStrip1
        Me.lvwSNPs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwSNPs.ForeColor = System.Drawing.Color.Blue
        Me.lvwSNPs.FullRowSelect = True
        Me.lvwSNPs.GridLines = True
        Me.lvwSNPs.Location = New System.Drawing.Point(8, 75)
        Me.lvwSNPs.Margin = New System.Windows.Forms.Padding(4)
        Me.lvwSNPs.Name = "lvwSNPs"
        Me.lvwSNPs.Size = New System.Drawing.Size(1110, 391)
        Me.lvwSNPs.TabIndex = 55
        Me.lvwSNPs.UseCompatibleStateImageBehavior = False
        Me.lvwSNPs.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReorder, Me.mnuSetToAltCallToolStrip, Me.mnuRemoveAltCall, Me.mnuRenameMutation, Me.mnuRenameNode})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(211, 152)
        '
        'mnuReorder
        '
        Me.mnuReorder.Name = "mnuReorder"
        Me.mnuReorder.Size = New System.Drawing.Size(210, 24)
        Me.mnuReorder.Text = "Re-order as original"
        Me.mnuReorder.Visible = False
        '
        'tabMembersWithSNP
        '
        Me.tabMembersWithSNP.Controls.Add(Me.lvwMembersWithSNP)
        Me.tabMembersWithSNP.Location = New System.Drawing.Point(4, 25)
        Me.tabMembersWithSNP.Margin = New System.Windows.Forms.Padding(4)
        Me.tabMembersWithSNP.Name = "tabMembersWithSNP"
        Me.tabMembersWithSNP.Padding = New System.Windows.Forms.Padding(4)
        Me.tabMembersWithSNP.Size = New System.Drawing.Size(1126, 474)
        Me.tabMembersWithSNP.TabIndex = 1
        Me.tabMembersWithSNP.Text = "Members With SNP"
        Me.tabMembersWithSNP.UseVisualStyleBackColor = True
        '
        'lvwMembersWithSNP
        '
        Me.lvwMembersWithSNP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwMembersWithSNP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwMembersWithSNP.ForeColor = System.Drawing.Color.Blue
        Me.lvwMembersWithSNP.FullRowSelect = True
        Me.lvwMembersWithSNP.Location = New System.Drawing.Point(4, 4)
        Me.lvwMembersWithSNP.Margin = New System.Windows.Forms.Padding(4)
        Me.lvwMembersWithSNP.MultiSelect = False
        Me.lvwMembersWithSNP.Name = "lvwMembersWithSNP"
        Me.lvwMembersWithSNP.Size = New System.Drawing.Size(1118, 466)
        Me.lvwMembersWithSNP.TabIndex = 0
        Me.lvwMembersWithSNP.UseCompatibleStateImageBehavior = False
        Me.lvwMembersWithSNP.View = System.Windows.Forms.View.Details
        '
        'btnSelectMutations
        '
        Me.btnSelectMutations.Location = New System.Drawing.Point(219, 8)
        Me.btnSelectMutations.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSelectMutations.Name = "btnSelectMutations"
        Me.btnSelectMutations.Size = New System.Drawing.Size(198, 28)
        Me.btnSelectMutations.TabIndex = 32
        Me.btnSelectMutations.Text = "Select Mutations"
        Me.btnSelectMutations.UseVisualStyleBackColor = True
        '
        'btnLoadAllBigYHg19
        '
        Me.btnLoadAllBigYHg19.Location = New System.Drawing.Point(931, 7)
        Me.btnLoadAllBigYHg19.Name = "btnLoadAllBigYHg19"
        Me.btnLoadAllBigYHg19.Size = New System.Drawing.Size(212, 59)
        Me.btnLoadAllBigYHg19.TabIndex = 33
        Me.btnLoadAllBigYHg19.Text = "Load All BigY Hg19"
        Me.btnLoadAllBigYHg19.UseVisualStyleBackColor = True
        Me.btnLoadAllBigYHg19.Visible = False
        '
        'btnLoadTreeNode
        '
        Me.btnLoadTreeNode.Location = New System.Drawing.Point(13, 8)
        Me.btnLoadTreeNode.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLoadTreeNode.Name = "btnLoadTreeNode"
        Me.btnLoadTreeNode.Size = New System.Drawing.Size(198, 58)
        Me.btnLoadTreeNode.TabIndex = 34
        Me.btnLoadTreeNode.Text = "Select Mutations and Members from HaploTree"
        Me.btnLoadTreeNode.UseVisualStyleBackColor = True
        '
        'ckbxMutations
        '
        Me.ckbxMutations.AutoSize = True
        Me.ckbxMutations.Location = New System.Drawing.Point(425, 13)
        Me.ckbxMutations.Name = "ckbxMutations"
        Me.ckbxMutations.Size = New System.Drawing.Size(18, 17)
        Me.ckbxMutations.TabIndex = 35
        Me.ckbxMutations.UseVisualStyleBackColor = True
        '
        'ckbxMembers
        '
        Me.ckbxMembers.AutoSize = True
        Me.ckbxMembers.Location = New System.Drawing.Point(425, 44)
        Me.ckbxMembers.Name = "ckbxMembers"
        Me.ckbxMembers.Size = New System.Drawing.Size(18, 17)
        Me.ckbxMembers.TabIndex = 36
        Me.ckbxMembers.UseVisualStyleBackColor = True
        '
        'btnSaveChanges
        '
        Me.btnSaveChanges.Enabled = False
        Me.btnSaveChanges.Location = New System.Drawing.Point(627, 9)
        Me.btnSaveChanges.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveChanges.Name = "btnSaveChanges"
        Me.btnSaveChanges.Size = New System.Drawing.Size(123, 58)
        Me.btnSaveChanges.TabIndex = 37
        Me.btnSaveChanges.Text = "Save Changes to DB"
        Me.btnSaveChanges.UseVisualStyleBackColor = True
        '
        'btnAllowEdit
        '
        Me.btnAllowEdit.Location = New System.Drawing.Point(496, 9)
        Me.btnAllowEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAllowEdit.Name = "btnAllowEdit"
        Me.btnAllowEdit.Size = New System.Drawing.Size(123, 58)
        Me.btnAllowEdit.TabIndex = 39
        Me.btnAllowEdit.Text = "Allow Node Name Edit"
        Me.btnAllowEdit.UseVisualStyleBackColor = True
        '
        'mnuSetToAltCallToolStrip
        '
        Me.mnuSetToAltCallToolStrip.Name = "mnuSetToAltCallToolStrip"
        Me.mnuSetToAltCallToolStrip.Size = New System.Drawing.Size(210, 24)
        Me.mnuSetToAltCallToolStrip.Text = "Set To Alt Call"
        '
        'mnuRemoveAltCall
        '
        Me.mnuRemoveAltCall.Name = "mnuRemoveAltCall"
        Me.mnuRemoveAltCall.Size = New System.Drawing.Size(210, 24)
        Me.mnuRemoveAltCall.Text = "Remove Alt Call"
        '
        'mnuRenameMutation
        '
        Me.mnuRenameMutation.Name = "mnuRenameMutation"
        Me.mnuRenameMutation.Size = New System.Drawing.Size(210, 24)
        Me.mnuRenameMutation.Text = "Rename Mutation"
        '
        'mnuRenameNode
        '
        Me.mnuRenameNode.Name = "mnuRenameNode"
        Me.mnuRenameNode.Size = New System.Drawing.Size(210, 24)
        Me.mnuRenameNode.Text = "Rename Node"
        '
        'frmAllMembersSNPs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1160, 590)
        Me.Controls.Add(Me.btnAllowEdit)
        Me.Controls.Add(Me.btnSaveChanges)
        Me.Controls.Add(Me.ckbxMembers)
        Me.Controls.Add(Me.ckbxMutations)
        Me.Controls.Add(Me.btnLoadTreeNode)
        Me.Controls.Add(Me.btnLoadAllBigYHg19)
        Me.Controls.Add(Me.btnSelectMutations)
        Me.Controls.Add(Me.tabMembersSNPs)
        Me.Controls.Add(Me.btnSelectMembers)
        Me.Controls.Add(Me.lblID)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAllMembersSNPs"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compare Variants"
        Me.tabMembersSNPs.ResumeLayout(False)
        Me.tabSNPs.ResumeLayout(False)
        Me.tabSNPs.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.tabMembersWithSNP.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSelectMembers As System.Windows.Forms.Button
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents tabMembersSNPs As System.Windows.Forms.TabControl
    Friend WithEvents tabSNPs As System.Windows.Forms.TabPage
    Friend WithEvents tabMembersWithSNP As System.Windows.Forms.TabPage
    Friend WithEvents lblPassingPositions As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnFindPosition As System.Windows.Forms.Button
    Friend WithEvents txtFindPosition As System.Windows.Forms.TextBox
    Friend WithEvents lvwSNPs As System.Windows.Forms.ListView
    Friend WithEvents lvwMembersWithSNP As System.Windows.Forms.ListView
    Friend WithEvents btnSelectMutations As Button
    Friend WithEvents btnLoadAllBigYHg19 As Button
    Friend WithEvents btnLoadTreeNode As Button
    Friend WithEvents ckbxMutations As CheckBox
    Friend WithEvents ckbxMembers As CheckBox
    Friend WithEvents btnSaveChanges As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents mnuReorder As ToolStripMenuItem
    Friend WithEvents btnAllowEdit As Button
    Friend WithEvents mnuSetToAltCallToolStrip As ToolStripMenuItem
    Friend WithEvents mnuRemoveAltCall As ToolStripMenuItem
    Friend WithEvents mnuRenameMutation As ToolStripMenuItem
    Friend WithEvents mnuRenameNode As ToolStripMenuItem
End Class
