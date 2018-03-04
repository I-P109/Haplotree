<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditMembers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditMembersDetails = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditMembersAnalyseVariants = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditMembersUploadToTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadMemberDbHg19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadVariantsFromDbHG19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadPrivateMutFromDbHG19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetAllToHasMutationhg19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MutationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadMutationDbHG19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddParentNodeID = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveAllParentNodes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetAllMutationsToPrivate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllSNPsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewMembersSNPs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewHaploTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsMembers = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuView, Me.mnuReports, Me.TestToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1024, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileImport})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(44, 24)
        Me.mnuFile.Text = "File"
        '
        'mnuFileImport
        '
        Me.mnuFileImport.Name = "mnuFileImport"
        Me.mnuFileImport.Size = New System.Drawing.Size(129, 26)
        Me.mnuFileImport.Text = "Import"
        '
        'mnuEdit
        '
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditMembers, Me.MutationsToolStripMenuItem})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(47, 24)
        Me.mnuEdit.Text = "Edit"
        '
        'mnuEditMembers
        '
        Me.mnuEditMembers.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditMembersDetails, Me.mnuEditMembersAnalyseVariants, Me.mnuEditMembersUploadToTree, Me.mnuLoadMemberDbHg19, Me.mnuLoadVariantsFromDbHG19, Me.mnuLoadPrivateMutFromDbHG19, Me.mnuSetAllToHasMutationhg19})
        Me.mnuEditMembers.Name = "mnuEditMembers"
        Me.mnuEditMembers.Size = New System.Drawing.Size(181, 26)
        Me.mnuEditMembers.Text = "Members"
        '
        'mnuEditMembersDetails
        '
        Me.mnuEditMembersDetails.Name = "mnuEditMembersDetails"
        Me.mnuEditMembersDetails.Size = New System.Drawing.Size(378, 26)
        Me.mnuEditMembersDetails.Text = "Details"
        '
        'mnuEditMembersAnalyseVariants
        '
        Me.mnuEditMembersAnalyseVariants.Name = "mnuEditMembersAnalyseVariants"
        Me.mnuEditMembersAnalyseVariants.Size = New System.Drawing.Size(378, 26)
        Me.mnuEditMembersAnalyseVariants.Text = "Analyse Variants"
        '
        'mnuEditMembersUploadToTree
        '
        Me.mnuEditMembersUploadToTree.Name = "mnuEditMembersUploadToTree"
        Me.mnuEditMembersUploadToTree.Size = New System.Drawing.Size(378, 26)
        Me.mnuEditMembersUploadToTree.Text = "Upload to Tree"
        '
        'mnuLoadMemberDbHg19
        '
        Me.mnuLoadMemberDbHg19.Name = "mnuLoadMemberDbHg19"
        Me.mnuLoadMemberDbHg19.Size = New System.Drawing.Size(378, 26)
        Me.mnuLoadMemberDbHg19.Text = "Load Members from Db HG19 "
        Me.mnuLoadMemberDbHg19.Visible = False
        '
        'mnuLoadVariantsFromDbHG19
        '
        Me.mnuLoadVariantsFromDbHG19.Name = "mnuLoadVariantsFromDbHG19"
        Me.mnuLoadVariantsFromDbHG19.Size = New System.Drawing.Size(378, 26)
        Me.mnuLoadVariantsFromDbHG19.Text = "Load Variants from Db HG19 "
        Me.mnuLoadVariantsFromDbHG19.Visible = False
        '
        'mnuLoadPrivateMutFromDbHG19
        '
        Me.mnuLoadPrivateMutFromDbHG19.Name = "mnuLoadPrivateMutFromDbHG19"
        Me.mnuLoadPrivateMutFromDbHG19.Size = New System.Drawing.Size(378, 26)
        Me.mnuLoadPrivateMutFromDbHG19.Text = "Load Private Mutations from Db HG19 "
        Me.mnuLoadPrivateMutFromDbHG19.Visible = False
        '
        'mnuSetAllToHasMutationhg19
        '
        Me.mnuSetAllToHasMutationhg19.Name = "mnuSetAllToHasMutationhg19"
        Me.mnuSetAllToHasMutationhg19.Size = New System.Drawing.Size(378, 26)
        Me.mnuSetAllToHasMutationhg19.Text = "Set True to HasMutationHg19 - All Members"
        Me.mnuSetAllToHasMutationhg19.Visible = False
        '
        'MutationsToolStripMenuItem
        '
        Me.MutationsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLoadMutationDbHG19, Me.mnuAddParentNodeID, Me.mnuRemoveAllParentNodes, Me.mnuSetAllMutationsToPrivate})
        Me.MutationsToolStripMenuItem.Name = "MutationsToolStripMenuItem"
        Me.MutationsToolStripMenuItem.Size = New System.Drawing.Size(181, 26)
        Me.MutationsToolStripMenuItem.Text = "Mutations"
        '
        'mnuLoadMutationDbHG19
        '
        Me.mnuLoadMutationDbHG19.Name = "mnuLoadMutationDbHG19"
        Me.mnuLoadMutationDbHG19.Size = New System.Drawing.Size(318, 26)
        Me.mnuLoadMutationDbHG19.Text = "Load Mutations from Db HG19"
        Me.mnuLoadMutationDbHG19.Visible = False
        '
        'mnuAddParentNodeID
        '
        Me.mnuAddParentNodeID.Name = "mnuAddParentNodeID"
        Me.mnuAddParentNodeID.Size = New System.Drawing.Size(318, 26)
        Me.mnuAddParentNodeID.Text = "Add ParentNodeID from HaploTree"
        '
        'mnuRemoveAllParentNodes
        '
        Me.mnuRemoveAllParentNodes.Name = "mnuRemoveAllParentNodes"
        Me.mnuRemoveAllParentNodes.Size = New System.Drawing.Size(318, 26)
        Me.mnuRemoveAllParentNodes.Text = "Remove All Parent Nodes"
        Me.mnuRemoveAllParentNodes.Visible = False
        '
        'mnuSetAllMutationsToPrivate
        '
        Me.mnuSetAllMutationsToPrivate.Name = "mnuSetAllMutationsToPrivate"
        Me.mnuSetAllMutationsToPrivate.Size = New System.Drawing.Size(318, 26)
        Me.mnuSetAllMutationsToPrivate.Text = "Set All Mutations to Private"
        Me.mnuSetAllMutationsToPrivate.Visible = False
        '
        'mnuView
        '
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllSNPsToolStripMenuItem, Me.mnuViewMembersSNPs, Me.mnuViewTree, Me.mnuViewHaploTree})
        Me.mnuView.Name = "mnuView"
        Me.mnuView.Size = New System.Drawing.Size(53, 24)
        Me.mnuView.Text = "View"
        '
        'AllSNPsToolStripMenuItem
        '
        Me.AllSNPsToolStripMenuItem.Name = "AllSNPsToolStripMenuItem"
        Me.AllSNPsToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.AllSNPsToolStripMenuItem.Text = "All SNPs"
        '
        'mnuViewMembersSNPs
        '
        Me.mnuViewMembersSNPs.Name = "mnuViewMembersSNPs"
        Me.mnuViewMembersSNPs.Size = New System.Drawing.Size(183, 26)
        Me.mnuViewMembersSNPs.Text = "Members SNPs"
        '
        'mnuViewTree
        '
        Me.mnuViewTree.Name = "mnuViewTree"
        Me.mnuViewTree.Size = New System.Drawing.Size(183, 26)
        Me.mnuViewTree.Text = "Tree"
        '
        'mnuViewHaploTree
        '
        Me.mnuViewHaploTree.Name = "mnuViewHaploTree"
        Me.mnuViewHaploTree.Size = New System.Drawing.Size(183, 26)
        Me.mnuViewHaploTree.Text = "Haplo Tree"
        '
        'mnuReports
        '
        Me.mnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsMembers})
        Me.mnuReports.Name = "mnuReports"
        Me.mnuReports.Size = New System.Drawing.Size(72, 24)
        Me.mnuReports.Text = "Reports"
        '
        'mnuReportsMembers
        '
        Me.mnuReportsMembers.Name = "mnuReportsMembers"
        Me.mnuReportsMembers.Size = New System.Drawing.Size(195, 26)
        Me.mnuReportsMembers.Text = "Members Report"
        '
        'TestToolStripMenuItem
        '
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Size = New System.Drawing.Size(47, 24)
        Me.TestToolStripMenuItem.Text = "Test"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 514)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMain"
        Me.Text = "Haplotree"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditMembers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewMembersSNPs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewTree As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsMembers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditMembersDetails As ToolStripMenuItem
    Friend WithEvents mnuEditMembersAnalyseVariants As ToolStripMenuItem
    Friend WithEvents TestToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuEditMembersUploadToTree As ToolStripMenuItem
    Friend WithEvents AllSNPsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MutationsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuLoadMutationDbHG19 As ToolStripMenuItem
    Friend WithEvents mnuLoadMemberDbHg19 As ToolStripMenuItem
    Friend WithEvents mnuViewHaploTree As ToolStripMenuItem
    Friend WithEvents mnuAddParentNodeID As ToolStripMenuItem
    Friend WithEvents mnuRemoveAllParentNodes As ToolStripMenuItem
    Friend WithEvents mnuSetAllMutationsToPrivate As ToolStripMenuItem
    Friend WithEvents mnuLoadVariantsFromDbHG19 As ToolStripMenuItem
    Friend WithEvents mnuLoadPrivateMutFromDbHG19 As ToolStripMenuItem
    Friend WithEvents mnuSetAllToHasMutationhg19 As ToolStripMenuItem
End Class
