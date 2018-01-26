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
        Me.cmnuEditNodeName = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuRemoveNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuAddBackgroundColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuAddTextColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSNPInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.cmnuSelectNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvwTree
        '
        Me.tvwTree.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tvwTree.Location = New System.Drawing.Point(459, 0)
        Me.tvwTree.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tvwTree.Name = "tvwTree"
        Me.tvwTree.Size = New System.Drawing.Size(1105, 959)
        Me.tvwTree.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuAddNode, Me.cmnuEditNodeName, Me.cmnuRemoveNode, Me.ToolStripSeparator1, Me.cmnuAddBackgroundColor, Me.cmnuAddTextColor, Me.ToolStripSeparator2, Me.cmnuSNPInfo, Me.cmnuSelectNode})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(230, 212)
        '
        'cmnuAddNode
        '
        Me.cmnuAddNode.Name = "cmnuAddNode"
        Me.cmnuAddNode.Size = New System.Drawing.Size(229, 24)
        Me.cmnuAddNode.Text = "Add Node"
        '
        'cmnuEditNodeName
        '
        Me.cmnuEditNodeName.Name = "cmnuEditNodeName"
        Me.cmnuEditNodeName.Size = New System.Drawing.Size(229, 24)
        Me.cmnuEditNodeName.Text = "Edit Node Name"
        '
        'cmnuRemoveNode
        '
        Me.cmnuRemoveNode.Name = "cmnuRemoveNode"
        Me.cmnuRemoveNode.Size = New System.Drawing.Size(229, 24)
        Me.cmnuRemoveNode.Text = "Remove Node"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(226, 6)
        '
        'cmnuAddBackgroundColor
        '
        Me.cmnuAddBackgroundColor.Name = "cmnuAddBackgroundColor"
        Me.cmnuAddBackgroundColor.Size = New System.Drawing.Size(229, 24)
        Me.cmnuAddBackgroundColor.Text = "Add Background Color"
        '
        'cmnuAddTextColor
        '
        Me.cmnuAddTextColor.Name = "cmnuAddTextColor"
        Me.cmnuAddTextColor.Size = New System.Drawing.Size(229, 24)
        Me.cmnuAddTextColor.Text = "Add Text Color"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(226, 6)
        '
        'cmnuSNPInfo
        '
        Me.cmnuSNPInfo.Name = "cmnuSNPInfo"
        Me.cmnuSNPInfo.Size = New System.Drawing.Size(229, 24)
        Me.cmnuSNPInfo.Text = "SNP Info"
        '
        'cmnuSelectNode
        '
        Me.cmnuSelectNode.Name = "cmnuSelectNode"
        Me.cmnuSelectNode.Size = New System.Drawing.Size(229, 24)
        Me.cmnuSelectNode.Text = "Select Node"
        '
        'frmTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1581, 975)
        Me.Controls.Add(Me.tvwTree)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
    Friend WithEvents cmnuEditNodeName As ToolStripMenuItem
    Friend WithEvents cmnuSelectNode As ToolStripMenuItem
End Class
