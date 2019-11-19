<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lblCopyInfo = New System.Windows.Forms.Label()
        Me.nudCopyCount = New System.Windows.Forms.NumericUpDown()
        Me.txtCopyNamePrefix = New System.Windows.Forms.TextBox()
        Me.lblCopyNamePrefix = New System.Windows.Forms.Label()
        Me.lblCopyCount = New System.Windows.Forms.Label()
        Me.pbModifying = New System.Windows.Forms.ProgressBar()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.ilTabs = New System.Windows.Forms.ImageList(Me.components)
        Me.tcTransformation = New System.Windows.Forms.TabControl()
        Me.tpCopy = New System.Windows.Forms.TabPage()
        Me.chkCopyUsePointSources = New System.Windows.Forms.CheckBox()
        Me.tpTranslation = New System.Windows.Forms.TabPage()
        Me.txtTranslationZ = New System.Windows.Forms.TextBox()
        Me.lblTranslationZ = New System.Windows.Forms.Label()
        Me.txtTranslationY = New System.Windows.Forms.TextBox()
        Me.lblTranslationY = New System.Windows.Forms.Label()
        Me.txtTranslationX = New System.Windows.Forms.TextBox()
        Me.lblTranslationX = New System.Windows.Forms.Label()
        Me.tpRotate = New System.Windows.Forms.TabPage()
        Me.cboRotationAxis = New System.Windows.Forms.ComboBox()
        Me.txtRotationAngle = New System.Windows.Forms.TextBox()
        Me.lblRotationAngle = New System.Windows.Forms.Label()
        Me.lblRotationAxis = New System.Windows.Forms.Label()
        Me.tpMirror = New System.Windows.Forms.TabPage()
        Me.cboMirrorPlane = New System.Windows.Forms.ComboBox()
        Me.txtMirrorCoordinate = New System.Windows.Forms.TextBox()
        Me.lblMirrorCoordinate = New System.Windows.Forms.Label()
        Me.lblMirrorPlane = New System.Windows.Forms.Label()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.pnlBottom.SuspendLayout()
        CType(Me.nudCopyCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcTransformation.SuspendLayout()
        Me.tpCopy.SuspendLayout()
        Me.tpTranslation.SuspendLayout()
        Me.tpRotate.SuspendLayout()
        Me.tpMirror.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnAbout)
        Me.pnlBottom.Controls.Add(Me.lblCopyInfo)
        Me.pnlBottom.Controls.Add(Me.nudCopyCount)
        Me.pnlBottom.Controls.Add(Me.txtCopyNamePrefix)
        Me.pnlBottom.Controls.Add(Me.lblCopyNamePrefix)
        Me.pnlBottom.Controls.Add(Me.lblCopyCount)
        Me.pnlBottom.Controls.Add(Me.pbModifying)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 182)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(387, 183)
        Me.pnlBottom.TabIndex = 2
        '
        'lblCopyInfo
        '
        Me.lblCopyInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCopyInfo.Location = New System.Drawing.Point(129, 39)
        Me.lblCopyInfo.Name = "lblCopyInfo"
        Me.lblCopyInfo.Size = New System.Drawing.Size(246, 31)
        Me.lblCopyInfo.TabIndex = 16
        Me.lblCopyInfo.Text = "___"
        '
        'nudCopyCount
        '
        Me.nudCopyCount.Location = New System.Drawing.Point(128, 15)
        Me.nudCopyCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCopyCount.Name = "nudCopyCount"
        Me.nudCopyCount.Size = New System.Drawing.Size(120, 20)
        Me.nudCopyCount.TabIndex = 0
        Me.nudCopyCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtCopyNamePrefix
        '
        Me.txtCopyNamePrefix.Location = New System.Drawing.Point(128, 73)
        Me.txtCopyNamePrefix.Name = "txtCopyNamePrefix"
        Me.txtCopyNamePrefix.Size = New System.Drawing.Size(120, 20)
        Me.txtCopyNamePrefix.TabIndex = 1
        Me.txtCopyNamePrefix.WordWrap = False
        '
        'lblCopyNamePrefix
        '
        Me.lblCopyNamePrefix.AutoSize = True
        Me.lblCopyNamePrefix.Location = New System.Drawing.Point(14, 76)
        Me.lblCopyNamePrefix.Name = "lblCopyNamePrefix"
        Me.lblCopyNamePrefix.Size = New System.Drawing.Size(74, 13)
        Me.lblCopyNamePrefix.TabIndex = 15
        Me.lblCopyNamePrefix.Text = "Namenspräfix:"
        '
        'lblCopyCount
        '
        Me.lblCopyCount.AutoSize = True
        Me.lblCopyCount.Location = New System.Drawing.Point(14, 17)
        Me.lblCopyCount.Name = "lblCopyCount"
        Me.lblCopyCount.Size = New System.Drawing.Size(78, 13)
        Me.lblCopyCount.TabIndex = 13
        Me.lblCopyCount.Text = "Anzahl Kopien:"
        '
        'pbModifying
        '
        Me.pbModifying.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbModifying.Location = New System.Drawing.Point(0, 160)
        Me.pbModifying.Name = "pbModifying"
        Me.pbModifying.Size = New System.Drawing.Size(387, 23)
        Me.pbModifying.TabIndex = 12
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(329, 104)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(51, 47)
        Me.btnOK.TabIndex = 2
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'pnlTop
        '
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(387, 21)
        Me.pnlTop.TabIndex = 0
        '
        'ilTabs
        '
        Me.ilTabs.ImageStream = CType(resources.GetObject("ilTabs.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilTabs.TransparentColor = System.Drawing.Color.Transparent
        Me.ilTabs.Images.SetKeyName(0, "ItemCopy.png")
        Me.ilTabs.Images.SetKeyName(1, "ItemTranslation.png")
        Me.ilTabs.Images.SetKeyName(2, "ItemRotate.png")
        Me.ilTabs.Images.SetKeyName(3, "ItemMirror.png")
        '
        'tcTransformation
        '
        Me.tcTransformation.Controls.Add(Me.tpCopy)
        Me.tcTransformation.Controls.Add(Me.tpTranslation)
        Me.tcTransformation.Controls.Add(Me.tpRotate)
        Me.tcTransformation.Controls.Add(Me.tpMirror)
        Me.tcTransformation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcTransformation.HotTrack = True
        Me.tcTransformation.ImageList = Me.ilTabs
        Me.tcTransformation.Location = New System.Drawing.Point(0, 21)
        Me.tcTransformation.Name = "tcTransformation"
        Me.tcTransformation.SelectedIndex = 0
        Me.tcTransformation.Size = New System.Drawing.Size(387, 161)
        Me.tcTransformation.TabIndex = 1
        '
        'tpCopy
        '
        Me.tpCopy.Controls.Add(Me.chkCopyUsePointSources)
        Me.tpCopy.ImageIndex = 0
        Me.tpCopy.Location = New System.Drawing.Point(4, 39)
        Me.tpCopy.Name = "tpCopy"
        Me.tpCopy.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCopy.Size = New System.Drawing.Size(379, 118)
        Me.tpCopy.TabIndex = 0
        Me.tpCopy.Text = "Kopieren"
        '
        'chkCopyUsePointSources
        '
        Me.chkCopyUsePointSources.AutoSize = True
        Me.chkCopyUsePointSources.Location = New System.Drawing.Point(10, 30)
        Me.chkCopyUsePointSources.Name = "chkCopyUsePointSources"
        Me.chkCopyUsePointSources.Size = New System.Drawing.Size(145, 17)
        Me.chkCopyUsePointSources.TabIndex = 0
        Me.chkCopyUsePointSources.Text = "Verwende Punktequellen"
        Me.chkCopyUsePointSources.UseVisualStyleBackColor = True
        '
        'tpTranslation
        '
        Me.tpTranslation.Controls.Add(Me.txtTranslationZ)
        Me.tpTranslation.Controls.Add(Me.lblTranslationZ)
        Me.tpTranslation.Controls.Add(Me.txtTranslationY)
        Me.tpTranslation.Controls.Add(Me.lblTranslationY)
        Me.tpTranslation.Controls.Add(Me.txtTranslationX)
        Me.tpTranslation.Controls.Add(Me.lblTranslationX)
        Me.tpTranslation.ImageIndex = 1
        Me.tpTranslation.Location = New System.Drawing.Point(4, 39)
        Me.tpTranslation.Name = "tpTranslation"
        Me.tpTranslation.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTranslation.Size = New System.Drawing.Size(379, 118)
        Me.tpTranslation.TabIndex = 1
        Me.tpTranslation.Text = "Translation"
        '
        'txtTranslationZ
        '
        Me.txtTranslationZ.Location = New System.Drawing.Point(124, 79)
        Me.txtTranslationZ.Name = "txtTranslationZ"
        Me.txtTranslationZ.Size = New System.Drawing.Size(120, 20)
        Me.txtTranslationZ.TabIndex = 2
        Me.txtTranslationZ.Text = "0"
        Me.txtTranslationZ.WordWrap = False
        '
        'lblTranslationZ
        '
        Me.lblTranslationZ.AutoSize = True
        Me.lblTranslationZ.Location = New System.Drawing.Point(10, 82)
        Me.lblTranslationZ.Name = "lblTranslationZ"
        Me.lblTranslationZ.Size = New System.Drawing.Size(17, 13)
        Me.lblTranslationZ.TabIndex = 19
        Me.lblTranslationZ.Text = "Z:"
        '
        'txtTranslationY
        '
        Me.txtTranslationY.Location = New System.Drawing.Point(124, 53)
        Me.txtTranslationY.Name = "txtTranslationY"
        Me.txtTranslationY.Size = New System.Drawing.Size(120, 20)
        Me.txtTranslationY.TabIndex = 1
        Me.txtTranslationY.Text = "0"
        Me.txtTranslationY.WordWrap = False
        '
        'lblTranslationY
        '
        Me.lblTranslationY.AutoSize = True
        Me.lblTranslationY.Location = New System.Drawing.Point(10, 56)
        Me.lblTranslationY.Name = "lblTranslationY"
        Me.lblTranslationY.Size = New System.Drawing.Size(17, 13)
        Me.lblTranslationY.TabIndex = 17
        Me.lblTranslationY.Text = "Y:"
        '
        'txtTranslationX
        '
        Me.txtTranslationX.Location = New System.Drawing.Point(124, 27)
        Me.txtTranslationX.Name = "txtTranslationX"
        Me.txtTranslationX.Size = New System.Drawing.Size(120, 20)
        Me.txtTranslationX.TabIndex = 0
        Me.txtTranslationX.Text = "0"
        Me.txtTranslationX.WordWrap = False
        '
        'lblTranslationX
        '
        Me.lblTranslationX.AutoSize = True
        Me.lblTranslationX.Location = New System.Drawing.Point(10, 30)
        Me.lblTranslationX.Name = "lblTranslationX"
        Me.lblTranslationX.Size = New System.Drawing.Size(17, 13)
        Me.lblTranslationX.TabIndex = 15
        Me.lblTranslationX.Text = "X:"
        '
        'tpRotate
        '
        Me.tpRotate.Controls.Add(Me.cboRotationAxis)
        Me.tpRotate.Controls.Add(Me.txtRotationAngle)
        Me.tpRotate.Controls.Add(Me.lblRotationAngle)
        Me.tpRotate.Controls.Add(Me.lblRotationAxis)
        Me.tpRotate.ImageIndex = 2
        Me.tpRotate.Location = New System.Drawing.Point(4, 39)
        Me.tpRotate.Name = "tpRotate"
        Me.tpRotate.Padding = New System.Windows.Forms.Padding(3)
        Me.tpRotate.Size = New System.Drawing.Size(379, 118)
        Me.tpRotate.TabIndex = 2
        Me.tpRotate.Text = "Rotieren"
        '
        'cboRotationAxis
        '
        Me.cboRotationAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRotationAxis.Items.AddRange(New Object() {"X", "Y", "Z"})
        Me.cboRotationAxis.Location = New System.Drawing.Point(124, 27)
        Me.cboRotationAxis.Name = "cboRotationAxis"
        Me.cboRotationAxis.Size = New System.Drawing.Size(120, 21)
        Me.cboRotationAxis.TabIndex = 0
        '
        'txtRotationAngle
        '
        Me.txtRotationAngle.Location = New System.Drawing.Point(124, 53)
        Me.txtRotationAngle.Name = "txtRotationAngle"
        Me.txtRotationAngle.Size = New System.Drawing.Size(120, 20)
        Me.txtRotationAngle.TabIndex = 1
        Me.txtRotationAngle.Text = "0"
        Me.txtRotationAngle.WordWrap = False
        '
        'lblRotationAngle
        '
        Me.lblRotationAngle.AutoSize = True
        Me.lblRotationAngle.Location = New System.Drawing.Point(10, 56)
        Me.lblRotationAngle.Name = "lblRotationAngle"
        Me.lblRotationAngle.Size = New System.Drawing.Size(43, 13)
        Me.lblRotationAngle.TabIndex = 21
        Me.lblRotationAngle.Text = "Winkel:"
        '
        'lblRotationAxis
        '
        Me.lblRotationAxis.AutoSize = True
        Me.lblRotationAxis.Location = New System.Drawing.Point(10, 30)
        Me.lblRotationAxis.Name = "lblRotationAxis"
        Me.lblRotationAxis.Size = New System.Drawing.Size(40, 13)
        Me.lblRotationAxis.TabIndex = 19
        Me.lblRotationAxis.Text = "Achse:"
        '
        'tpMirror
        '
        Me.tpMirror.Controls.Add(Me.cboMirrorPlane)
        Me.tpMirror.Controls.Add(Me.txtMirrorCoordinate)
        Me.tpMirror.Controls.Add(Me.lblMirrorCoordinate)
        Me.tpMirror.Controls.Add(Me.lblMirrorPlane)
        Me.tpMirror.ImageIndex = 3
        Me.tpMirror.Location = New System.Drawing.Point(4, 39)
        Me.tpMirror.Name = "tpMirror"
        Me.tpMirror.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMirror.Size = New System.Drawing.Size(379, 118)
        Me.tpMirror.TabIndex = 3
        Me.tpMirror.Text = "Spiegeln"
        '
        'cboMirrorPlane
        '
        Me.cboMirrorPlane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMirrorPlane.Items.AddRange(New Object() {"X", "Y", "Z"})
        Me.cboMirrorPlane.Location = New System.Drawing.Point(124, 27)
        Me.cboMirrorPlane.Name = "cboMirrorPlane"
        Me.cboMirrorPlane.Size = New System.Drawing.Size(120, 21)
        Me.cboMirrorPlane.TabIndex = 0
        '
        'txtMirrorCoordinate
        '
        Me.txtMirrorCoordinate.Location = New System.Drawing.Point(124, 53)
        Me.txtMirrorCoordinate.Name = "txtMirrorCoordinate"
        Me.txtMirrorCoordinate.Size = New System.Drawing.Size(120, 20)
        Me.txtMirrorCoordinate.TabIndex = 1
        Me.txtMirrorCoordinate.Text = "0"
        Me.txtMirrorCoordinate.WordWrap = False
        '
        'lblMirrorCoordinate
        '
        Me.lblMirrorCoordinate.AutoSize = True
        Me.lblMirrorCoordinate.Location = New System.Drawing.Point(10, 56)
        Me.lblMirrorCoordinate.Name = "lblMirrorCoordinate"
        Me.lblMirrorCoordinate.Size = New System.Drawing.Size(61, 13)
        Me.lblMirrorCoordinate.TabIndex = 25
        Me.lblMirrorCoordinate.Text = "Koordinate:"
        '
        'lblMirrorPlane
        '
        Me.lblMirrorPlane.AutoSize = True
        Me.lblMirrorPlane.Location = New System.Drawing.Point(10, 30)
        Me.lblMirrorPlane.Name = "lblMirrorPlane"
        Me.lblMirrorPlane.Size = New System.Drawing.Size(41, 13)
        Me.lblMirrorPlane.TabIndex = 24
        Me.lblMirrorPlane.Text = "Ebene:"
        '
        'btnAbout
        '
        Me.btnAbout.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAbout.FlatAppearance.BorderSize = 0
        Me.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbout.Image = CType(resources.GetObject("btnAbout.Image"), System.Drawing.Image)
        Me.btnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAbout.Location = New System.Drawing.Point(9, 104)
        Me.btnAbout.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(85, 47)
        Me.btnAbout.TabIndex = 17
        Me.btnAbout.Text = "Über..."
        Me.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(387, 365)
        Me.Controls.Add(Me.tcTransformation)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmMain"
        Me.Text = "Kopieren/Transformieren"
        Me.TopMost = true
        Me.pnlBottom.ResumeLayout(false)
        Me.pnlBottom.PerformLayout
        CType(Me.nudCopyCount,System.ComponentModel.ISupportInitialize).EndInit
        Me.tcTransformation.ResumeLayout(false)
        Me.tpCopy.ResumeLayout(false)
        Me.tpCopy.PerformLayout
        Me.tpTranslation.ResumeLayout(false)
        Me.tpTranslation.PerformLayout
        Me.tpRotate.ResumeLayout(false)
        Me.tpRotate.PerformLayout
        Me.tpMirror.ResumeLayout(false)
        Me.tpMirror.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pbModifying As System.Windows.Forms.ProgressBar
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents ilTabs As System.Windows.Forms.ImageList
    Friend WithEvents tcTransformation As System.Windows.Forms.TabControl
    Friend WithEvents tpCopy As System.Windows.Forms.TabPage
    Friend WithEvents tpTranslation As System.Windows.Forms.TabPage
    Friend WithEvents tpRotate As System.Windows.Forms.TabPage
    Friend WithEvents tpMirror As System.Windows.Forms.TabPage
    Friend WithEvents txtCopyNamePrefix As System.Windows.Forms.TextBox
    Friend WithEvents lblCopyNamePrefix As System.Windows.Forms.Label
    Friend WithEvents lblCopyCount As System.Windows.Forms.Label
    Friend WithEvents nudCopyCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkCopyUsePointSources As System.Windows.Forms.CheckBox
    Friend WithEvents txtTranslationZ As System.Windows.Forms.TextBox
    Friend WithEvents lblTranslationZ As System.Windows.Forms.Label
    Friend WithEvents txtTranslationY As System.Windows.Forms.TextBox
    Friend WithEvents lblTranslationY As System.Windows.Forms.Label
    Friend WithEvents txtTranslationX As System.Windows.Forms.TextBox
    Friend WithEvents lblTranslationX As System.Windows.Forms.Label
    Friend WithEvents txtRotationAngle As System.Windows.Forms.TextBox
    Friend WithEvents lblRotationAngle As System.Windows.Forms.Label
    Friend WithEvents lblRotationAxis As System.Windows.Forms.Label
    Friend WithEvents cboRotationAxis As System.Windows.Forms.ComboBox
    Friend WithEvents cboMirrorPlane As System.Windows.Forms.ComboBox
    Friend WithEvents txtMirrorCoordinate As System.Windows.Forms.TextBox
    Friend WithEvents lblMirrorCoordinate As System.Windows.Forms.Label
    Friend WithEvents lblMirrorPlane As System.Windows.Forms.Label
    Friend WithEvents lblCopyInfo As System.Windows.Forms.Label
    Friend WithEvents btnAbout As System.Windows.Forms.Button

End Class
