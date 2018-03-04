<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMembersSearch
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMembers = New System.Windows.Forms.Label()
        Me.lvwMembers = New System.Windows.Forms.ListView()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(118, 695)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 17)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Total Members: "
        '
        'lblMembers
        '
        Me.lblMembers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMembers.AutoSize = True
        Me.lblMembers.Location = New System.Drawing.Point(236, 695)
        Me.lblMembers.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMembers.Name = "lblMembers"
        Me.lblMembers.Size = New System.Drawing.Size(80, 17)
        Me.lblMembers.TabIndex = 28
        Me.lblMembers.Text = "lblMembers"
        '
        'lvwMembers
        '
        Me.lvwMembers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwMembers.ForeColor = System.Drawing.Color.Blue
        Me.lvwMembers.FullRowSelect = True
        Me.lvwMembers.Location = New System.Drawing.Point(121, 13)
        Me.lvwMembers.Margin = New System.Windows.Forms.Padding(4)
        Me.lvwMembers.MultiSelect = False
        Me.lvwMembers.Name = "lvwMembers"
        Me.lvwMembers.Size = New System.Drawing.Size(902, 659)
        Me.lvwMembers.TabIndex = 27
        Me.lvwMembers.UseCompatibleStateImageBehavior = False
        Me.lvwMembers.View = System.Windows.Forms.View.Details
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(13, 13)
        Me.btnSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(100, 28)
        Me.btnSelect.TabIndex = 30
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(10, 684)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 28)
        Me.btnClose.TabIndex = 31
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(13, 98)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 28)
        Me.btnEdit.TabIndex = 32
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'frmMembersSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 721)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMembers)
        Me.Controls.Add(Me.lvwMembers)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMembersSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Members Search"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMembers As System.Windows.Forms.Label
    Friend WithEvents lvwMembers As System.Windows.Forms.ListView
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
End Class
