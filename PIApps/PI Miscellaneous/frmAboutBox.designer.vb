<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAboutBox
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelProductName As System.Windows.Forms.Label
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents LabelCompanyName As System.Windows.Forms.Label
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents LabelCopyright As System.Windows.Forms.Label

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAboutBox))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.LabelProductName = New System.Windows.Forms.Label()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.LabelCopyright = New System.Windows.Forms.Label()
        Me.LabelCompanyName = New System.Windows.Forms.Label()
        Me.TextBoxDescription = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.TableLayoutPanel.SuspendLayout
        CType(Me.LogoPictureBox,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 1
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel.Controls.Add(Me.LogoPictureBox, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelProductName, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelVersion, 0, 2)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCopyright, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCompanyName, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxDescription, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.btnOK, 0, 6)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(9, 9)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 7
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(493, 358)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"),System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(3, 3)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(487, 137)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 1
        Me.LogoPictureBox.TabStop = false
        '
        'LabelProductName
        '
        Me.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelProductName.Location = New System.Drawing.Point(6, 143)
        Me.LabelProductName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelProductName.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelProductName.Name = "LabelProductName"
        Me.LabelProductName.Size = New System.Drawing.Size(484, 17)
        Me.LabelProductName.TabIndex = 0
        Me.LabelProductName.Text = "Produktname"
        Me.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelVersion
        '
        Me.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelVersion.Location = New System.Drawing.Point(6, 160)
        Me.LabelVersion.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelVersion.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(484, 17)
        Me.LabelVersion.TabIndex = 0
        Me.LabelVersion.Text = "Version"
        Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelCopyright
        '
        Me.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCopyright.Location = New System.Drawing.Point(6, 177)
        Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelCopyright.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelCopyright.Name = "LabelCopyright"
        Me.LabelCopyright.Size = New System.Drawing.Size(484, 17)
        Me.LabelCopyright.TabIndex = 0
        Me.LabelCopyright.Text = "Copyright"
        Me.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelCompanyName
        '
        Me.LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCompanyName.Location = New System.Drawing.Point(6, 194)
        Me.LabelCompanyName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelCompanyName.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelCompanyName.Name = "LabelCompanyName"
        Me.LabelCompanyName.Size = New System.Drawing.Size(484, 17)
        Me.LabelCompanyName.TabIndex = 0
        Me.LabelCompanyName.Text = "Firmenname"
        Me.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TextBoxDescription.Location = New System.Drawing.Point(6, 214)
        Me.TextBoxDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.TextBoxDescription.Multiline = true
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.ReadOnly = true
        Me.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxDescription.Size = New System.Drawing.Size(484, 94)
        Me.TextBoxDescription.TabIndex = 0
        Me.TextBoxDescription.TabStop = false
        Me.TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text")
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(96,Byte),Integer), CType(CType(175,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(194,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"),System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(442, 311)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(51, 47)
        Me.btnOK.TabIndex = 4
        Me.btnOK.UseVisualStyleBackColor = true
        '
        'FrmAboutBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 376)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmAboutBox"
        Me.Padding = New System.Windows.Forms.Padding(9)
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PowerINSPECT Sonsitges"
        Me.TableLayoutPanel.ResumeLayout(false)
        Me.TableLayoutPanel.PerformLayout
        CType(Me.LogoPictureBox,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents btnOK As System.Windows.Forms.Button

End Class
