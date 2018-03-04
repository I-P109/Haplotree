<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblFTDNAKitNumber = New System.Windows.Forms.Label()
        Me.lblYFullIDCaption = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.tabImport = New System.Windows.Forms.TabPage()
        Me.tabCtlImport = New System.Windows.Forms.TabControl()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblMemberName = New System.Windows.Forms.Label()
        Me.lblYFullID = New System.Windows.Forms.Label()
        Me.lblFTDNAID = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblGenomeVersion = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPassingPositions = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPathAndFileName = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lvwImport = New System.Windows.Forms.ListView()
        Me.lvwHide = New System.Windows.Forms.ListView()
        Me.tabCtlImport.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(276, 18)
        Me.lblName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(45, 17)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Name"
        '
        'lblFTDNAKitNumber
        '
        Me.lblFTDNAKitNumber.AutoSize = True
        Me.lblFTDNAKitNumber.Location = New System.Drawing.Point(255, 59)
        Me.lblFTDNAKitNumber.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFTDNAKitNumber.Name = "lblFTDNAKitNumber"
        Me.lblFTDNAKitNumber.Size = New System.Drawing.Size(86, 17)
        Me.lblFTDNAKitNumber.TabIndex = 4
        Me.lblFTDNAKitNumber.Text = "FTDNA Kit #"
        '
        'lblYFullIDCaption
        '
        Me.lblYFullIDCaption.AutoSize = True
        Me.lblYFullIDCaption.Location = New System.Drawing.Point(269, 101)
        Me.lblYFullIDCaption.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblYFullIDCaption.Name = "lblYFullIDCaption"
        Me.lblYFullIDCaption.Size = New System.Drawing.Size(56, 17)
        Me.lblYFullIDCaption.TabIndex = 5
        Me.lblYFullIDCaption.Text = "YFull ID"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'tabImport
        '
        Me.tabImport.Location = New System.Drawing.Point(4, 25)
        Me.tabImport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabImport.Name = "tabImport"
        Me.tabImport.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabImport.Size = New System.Drawing.Size(113, 50)
        Me.tabImport.TabIndex = 0
        Me.tabImport.Text = "Import"
        Me.tabImport.UseVisualStyleBackColor = True
        '
        'tabCtlImport
        '
        Me.tabCtlImport.Controls.Add(Me.tabImport)
        Me.tabCtlImport.Location = New System.Drawing.Point(1929, 1025)
        Me.tabCtlImport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabCtlImport.Name = "tabCtlImport"
        Me.tabCtlImport.SelectedIndex = 0
        Me.tabCtlImport.Size = New System.Drawing.Size(121, 79)
        Me.tabCtlImport.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(193, 18)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(21, 17)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "ID"
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(225, 18)
        Me.lblID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(0, 17)
        Me.lblID.TabIndex = 15
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(13, 7)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 28)
        Me.btnSearch.TabIndex = 16
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblMemberName
        '
        Me.lblMemberName.BackColor = System.Drawing.Color.White
        Me.lblMemberName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMemberName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMemberName.ForeColor = System.Drawing.Color.Blue
        Me.lblMemberName.Location = New System.Drawing.Point(365, 18)
        Me.lblMemberName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMemberName.Name = "lblMemberName"
        Me.lblMemberName.Size = New System.Drawing.Size(279, 25)
        Me.lblMemberName.TabIndex = 17
        Me.lblMemberName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblYFullID
        '
        Me.lblYFullID.BackColor = System.Drawing.Color.White
        Me.lblYFullID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYFullID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYFullID.ForeColor = System.Drawing.Color.Blue
        Me.lblYFullID.Location = New System.Drawing.Point(365, 92)
        Me.lblYFullID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblYFullID.Name = "lblYFullID"
        Me.lblYFullID.Size = New System.Drawing.Size(279, 25)
        Me.lblYFullID.TabIndex = 18
        Me.lblYFullID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFTDNAID
        '
        Me.lblFTDNAID.BackColor = System.Drawing.Color.White
        Me.lblFTDNAID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFTDNAID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFTDNAID.ForeColor = System.Drawing.Color.Blue
        Me.lblFTDNAID.Location = New System.Drawing.Point(365, 54)
        Me.lblFTDNAID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFTDNAID.Name = "lblFTDNAID"
        Me.lblFTDNAID.Size = New System.Drawing.Size(279, 25)
        Me.lblFTDNAID.TabIndex = 19
        Me.lblFTDNAID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(1040, 530)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 28)
        Me.btnSave.TabIndex = 55
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblGenomeVersion
        '
        Me.lblGenomeVersion.AutoSize = True
        Me.lblGenomeVersion.ForeColor = System.Drawing.Color.Red
        Me.lblGenomeVersion.Location = New System.Drawing.Point(599, 190)
        Me.lblGenomeVersion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGenomeVersion.Name = "lblGenomeVersion"
        Me.lblGenomeVersion.Size = New System.Drawing.Size(0, 17)
        Me.lblGenomeVersion.TabIndex = 54
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(459, 188)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(128, 17)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Genome Version"
        '
        'lblPassingPositions
        '
        Me.lblPassingPositions.AutoSize = True
        Me.lblPassingPositions.ForeColor = System.Drawing.Color.Red
        Me.lblPassingPositions.Location = New System.Drawing.Point(359, 190)
        Me.lblPassingPositions.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassingPositions.Name = "lblPassingPositions"
        Me.lblPassingPositions.Size = New System.Drawing.Size(0, 17)
        Me.lblPassingPositions.TabIndex = 36
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(125, 190)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 17)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Total Positions that passed: "
        '
        'lblPathAndFileName
        '
        Me.lblPathAndFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPathAndFileName.BackColor = System.Drawing.Color.White
        Me.lblPathAndFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPathAndFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPathAndFileName.ForeColor = System.Drawing.Color.Blue
        Me.lblPathAndFileName.Location = New System.Drawing.Point(237, 133)
        Me.lblPathAndFileName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPathAndFileName.Name = "lblPathAndFileName"
        Me.lblPathAndFileName.Size = New System.Drawing.Size(903, 28)
        Me.lblPathAndFileName.TabIndex = 32
        '
        'btnBrowse
        '
        Me.btnBrowse.Enabled = False
        Me.btnBrowse.Location = New System.Drawing.Point(129, 130)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(100, 28)
        Me.btnBrowse.TabIndex = 31
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lvwImport
        '
        Me.lvwImport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwImport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwImport.ForeColor = System.Drawing.Color.Blue
        Me.lvwImport.FullRowSelect = True
        Me.lvwImport.GridLines = True
        Me.lvwImport.Location = New System.Drawing.Point(13, 209)
        Me.lvwImport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lvwImport.Name = "lvwImport"
        Me.lvwImport.Size = New System.Drawing.Size(1127, 313)
        Me.lvwImport.TabIndex = 30
        Me.lvwImport.UseCompatibleStateImageBehavior = False
        Me.lvwImport.View = System.Windows.Forms.View.Details
        Me.lvwImport.Visible = False
        '
        'lvwHide
        '
        Me.lvwHide.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwHide.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwHide.ForeColor = System.Drawing.Color.Blue
        Me.lvwHide.FullRowSelect = True
        Me.lvwHide.GridLines = True
        Me.lvwHide.Location = New System.Drawing.Point(13, 209)
        Me.lvwHide.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lvwHide.Name = "lvwHide"
        Me.lvwHide.Size = New System.Drawing.Size(1127, 313)
        Me.lvwHide.TabIndex = 56
        Me.lvwHide.UseCompatibleStateImageBehavior = False
        Me.lvwHide.View = System.Windows.Forms.View.Details
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1153, 571)
        Me.Controls.Add(Me.lvwHide)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblGenomeVersion)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblPassingPositions)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblPathAndFileName)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.lvwImport)
        Me.Controls.Add(Me.lblFTDNAID)
        Me.Controls.Add(Me.lblYFullID)
        Me.Controls.Add(Me.lblMemberName)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tabCtlImport)
        Me.Controls.Add(Me.lblYFullIDCaption)
        Me.Controls.Add(Me.lblFTDNAKitNumber)
        Me.Controls.Add(Me.lblName)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmImport"
        Me.Text = "Import"
        Me.tabCtlImport.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblFTDNAKitNumber As System.Windows.Forms.Label
    Friend WithEvents lblYFullIDCaption As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tabImport As System.Windows.Forms.TabPage
    Friend WithEvents tabCtlImport As System.Windows.Forms.TabControl
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblMemberName As System.Windows.Forms.Label
    Friend WithEvents lblYFullID As System.Windows.Forms.Label
    Friend WithEvents lblFTDNAID As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblGenomeVersion As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblPassingPositions As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPathAndFileName As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lvwImport As System.Windows.Forms.ListView
    Friend WithEvents lvwHide As ListView
End Class
