<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTree
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
        Me.components = New System.ComponentModel.Container()
        Me.tvwTree = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuAddNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuRemoveNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuAddBackgroundColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuAddTextColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSNPInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvwTree
        '
        Me.tvwTree.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tvwTree.Location = New System.Drawing.Point(344, 0)
        Me.tvwTree.Name = "tvwTree"
        Me.tvwTree.Size = New System.Drawing.Size(830, 780)
        Me.tvwTree.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuAddNode, Me.cmnuRemoveNode, Me.ToolStripSeparator1, Me.cmnuAddBackgroundColor, Me.cmnuAddTextColor, Me.ToolStripSeparator2, Me.cmnuSNPInfo})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(196, 148)
        '
        'cmnuAddNode
        '
        Me.cmnuAddNode.Name = "cmnuAddNode"
        Me.cmnuAddNode.Size = New System.Drawing.Size(195, 22)
        Me.cmnuAddNode.Text = "Add Node"
        '
        'cmnuRemoveNode
        '
        Me.cmnuRemoveNode.Name = "cmnuRemoveNode"
        Me.cmnuRemoveNode.Size = New System.Drawing.Size(195, 22)
        Me.cmnuRemoveNode.Text = "Remove Node"
        '
        'cmnuAddBackgroundColor
        '
        Me.cmnuAddBackgroundColor.Name = "cmnuAddBackgroundColor"
        Me.cmnuAddBackgroundColor.Size = New System.Drawing.Size(195, 22)
        Me.cmnuAddBackgroundColor.Text = "Add Background Color"
        '
        'cmnuAddTextColor
        '
        Me.cmnuAddTextColor.Name = "cmnuAddTextColor"
        Me.cmnuAddTextColor.Size = New System.Drawing.Size(195, 22)
        Me.cmnuAddTextColor.Text = "Add Text Color"
        '
        'cmnuSNPInfo
        '
        Me.cmnuSNPInfo.Name = "cmnuSNPInfo"
        Me.cmnuSNPInfo.Size = New System.Drawing.Size(195, 22)
        Me.cmnuSNPInfo.Text = "SNP Info"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(192, 6)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(192, 6)
        '
        'frmTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1186, 792)
        Me.Controls.Add(Me.tvwTree)
        Me.Name = "frmTree"
        Me.Text = "Tree"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tvwTree As TreeView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents cmnuAddNode As ToolStripMenuItem
    Friend WithEvents cmnuRemoveNode As ToolStripMenuItem
    Friend WithEvents cmnuAddBackgroundColor As ToolStripMenuItem
    Friend WithEvents cmnuAddTextColor As ToolStripMenuItem
    Friend WithEvents cmnuSNPInfo As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ColorDialog1 As ColorDialog
End Class
