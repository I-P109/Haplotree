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
        Me.mnuView = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewMembersSNPs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsMembers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditMembersDetails = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditMembersUploadToTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuView, Me.mnuReports})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(896, 28)
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
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditMembers})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(47, 24)
        Me.mnuEdit.Text = "Edit"
        '
        'mnuEditMembers
        '
        Me.mnuEditMembers.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditMembersDetails, Me.mnuEditMembersUploadToTree})
        Me.mnuEditMembers.Name = "mnuEditMembers"
        Me.mnuEditMembers.Size = New System.Drawing.Size(181, 26)
        Me.mnuEditMembers.Text = "Members"
        '
        'mnuView
        '
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuViewMembersSNPs, Me.mnuViewTree})
        Me.mnuView.Name = "mnuView"
        Me.mnuView.Size = New System.Drawing.Size(53, 24)
        Me.mnuView.Text = "View"
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
        'mnuEditMembersDetails
        '
        Me.mnuEditMembersDetails.Name = "mnuEditMembersDetails"
        Me.mnuEditMembersDetails.Size = New System.Drawing.Size(183, 26)
        Me.mnuEditMembersDetails.Text = "Details"
        '
        'mnuEditMembersUploadToTree
        '
        Me.mnuEditMembersUploadToTree.Name = "mnuEditMembersUploadToTree"
        Me.mnuEditMembersUploadToTree.Size = New System.Drawing.Size(183, 26)
        Me.mnuEditMembersUploadToTree.Text = "Upload to Tree"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 476)
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
    Friend WithEvents mnuEditMembersUploadToTree As ToolStripMenuItem
End Class
