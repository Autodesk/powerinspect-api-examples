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
        Me.pbModifying = New System.Windows.Forms.ProgressBar()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.ilTabs = New System.Windows.Forms.ImageList(Me.components)
        Me.tcMiscellaneous = New System.Windows.Forms.TabControl()
        Me.tpSphereContact = New System.Windows.Forms.TabPage()
        Me.gbCompensationMethod = New System.Windows.Forms.GroupBox()
        Me.cboCompensationVectors = New System.Windows.Forms.ComboBox()
        Me.chkSphereVectorInvert = New System.Windows.Forms.CheckBox()
        Me.rbDefineCompensationVector = New System.Windows.Forms.RadioButton()
        Me.rbShpereVector = New System.Windows.Forms.RadioButton()
        Me.tpCircleContact = New System.Windows.Forms.TabPage()
        Me.gbCompensationMethodCircle = New System.Windows.Forms.GroupBox()
        Me.lblCurcleVectorOffset = New System.Windows.Forms.Label()
        Me.txtCircleVectorOffset = New System.Windows.Forms.TextBox()
        Me.cboCompensationVectorsCircle = New System.Windows.Forms.ComboBox()
        Me.chkCircleVectorInvert = New System.Windows.Forms.CheckBox()
        Me.rbDefineCompensationVectorCircle = New System.Windows.Forms.RadioButton()
        Me.rbCircleVector = New System.Windows.Forms.RadioButton()
        Me.pnlBottom.SuspendLayout()
        Me.tcMiscellaneous.SuspendLayout()
        Me.tpSphereContact.SuspendLayout()
        Me.gbCompensationMethod.SuspendLayout()
        Me.tpCircleContact.SuspendLayout()
        Me.gbCompensationMethodCircle.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.pbModifying)
        Me.pnlBottom.Controls.Add(Me.btnAbout)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 271)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(387, 94)
        Me.pnlBottom.TabIndex = 2
        '
        'pbModifying
        '
        Me.pbModifying.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbModifying.Location = New System.Drawing.Point(0, 71)
        Me.pbModifying.Name = "pbModifying"
        Me.pbModifying.Size = New System.Drawing.Size(387, 23)
        Me.pbModifying.TabIndex = 12
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
        Me.btnAbout.Location = New System.Drawing.Point(4, 15)
        Me.btnAbout.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(85, 47)
        Me.btnAbout.TabIndex = 11
        Me.btnAbout.Text = "Über..."
        Me.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(329, 15)
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
        Me.ilTabs.Images.SetKeyName(0, "ShpereContact.png")
        Me.ilTabs.Images.SetKeyName(1, "CircleContact.png")
        '
        'tcMiscellaneous
        '
        Me.tcMiscellaneous.Controls.Add(Me.tpSphereContact)
        Me.tcMiscellaneous.Controls.Add(Me.tpCircleContact)
        Me.tcMiscellaneous.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMiscellaneous.HotTrack = True
        Me.tcMiscellaneous.ImageList = Me.ilTabs
        Me.tcMiscellaneous.Location = New System.Drawing.Point(0, 21)
        Me.tcMiscellaneous.Name = "tcMiscellaneous"
        Me.tcMiscellaneous.SelectedIndex = 0
        Me.tcMiscellaneous.Size = New System.Drawing.Size(387, 250)
        Me.tcMiscellaneous.TabIndex = 1
        '
        'tpSphereContact
        '
        Me.tpSphereContact.Controls.Add(Me.gbCompensationMethod)
        Me.tpSphereContact.ImageIndex = 0
        Me.tpSphereContact.Location = New System.Drawing.Point(4, 39)
        Me.tpSphereContact.Name = "tpSphereContact"
        Me.tpSphereContact.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSphereContact.Size = New System.Drawing.Size(379, 207)
        Me.tpSphereContact.TabIndex = 2
        Me.tpSphereContact.Text = "Kugel-Fläche"
        '
        'gbCompensationMethod
        '
        Me.gbCompensationMethod.Controls.Add(Me.cboCompensationVectors)
        Me.gbCompensationMethod.Controls.Add(Me.chkSphereVectorInvert)
        Me.gbCompensationMethod.Controls.Add(Me.rbDefineCompensationVector)
        Me.gbCompensationMethod.Controls.Add(Me.rbShpereVector)
        Me.gbCompensationMethod.Location = New System.Drawing.Point(8, 15)
        Me.gbCompensationMethod.Name = "gbCompensationMethod"
        Me.gbCompensationMethod.Size = New System.Drawing.Size(365, 137)
        Me.gbCompensationMethod.TabIndex = 0
        Me.gbCompensationMethod.TabStop = False
        Me.gbCompensationMethod.Text = "Kompensationsrichtung"
        '
        'cboCompensationVectors
        '
        Me.cboCompensationVectors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompensationVectors.Enabled = False
        Me.cboCompensationVectors.FormattingEnabled = True
        Me.cboCompensationVectors.Items.AddRange(New Object() {"X+", "X-", "Y+", "Y-", "Z+", "Z-"})
        Me.cboCompensationVectors.Location = New System.Drawing.Point(37, 104)
        Me.cboCompensationVectors.MaxDropDownItems = 6
        Me.cboCompensationVectors.Name = "cboCompensationVectors"
        Me.cboCompensationVectors.Size = New System.Drawing.Size(64, 21)
        Me.cboCompensationVectors.TabIndex = 3
        '
        'chkSphereVectorInvert
        '
        Me.chkSphereVectorInvert.AutoSize = True
        Me.chkSphereVectorInvert.Checked = True
        Me.chkSphereVectorInvert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSphereVectorInvert.Location = New System.Drawing.Point(37, 42)
        Me.chkSphereVectorInvert.Name = "chkSphereVectorInvert"
        Me.chkSphereVectorInvert.Size = New System.Drawing.Size(66, 17)
        Me.chkSphereVectorInvert.TabIndex = 2
        Me.chkSphereVectorInvert.Text = "invertiert"
        Me.chkSphereVectorInvert.UseVisualStyleBackColor = True
        '
        'rbDefineCompensationVector
        '
        Me.rbDefineCompensationVector.AutoSize = True
        Me.rbDefineCompensationVector.Location = New System.Drawing.Point(19, 81)
        Me.rbDefineCompensationVector.Name = "rbDefineCompensationVector"
        Me.rbDefineCompensationVector.Size = New System.Drawing.Size(61, 17)
        Me.rbDefineCompensationVector.TabIndex = 1
        Me.rbDefineCompensationVector.Text = "Entlang"
        Me.rbDefineCompensationVector.UseVisualStyleBackColor = True
        '
        'rbShpereVector
        '
        Me.rbShpereVector.AutoSize = True
        Me.rbShpereVector.Checked = True
        Me.rbShpereVector.Location = New System.Drawing.Point(19, 19)
        Me.rbShpereVector.Name = "rbShpereVector"
        Me.rbShpereVector.Size = New System.Drawing.Size(82, 17)
        Me.rbShpereVector.TabIndex = 0
        Me.rbShpereVector.TabStop = True
        Me.rbShpereVector.Text = "Kugelvektor"
        Me.rbShpereVector.UseVisualStyleBackColor = True
        '
        'tpCircleContact
        '
        Me.tpCircleContact.BackColor = System.Drawing.SystemColors.Control
        Me.tpCircleContact.Controls.Add(Me.gbCompensationMethodCircle)
        Me.tpCircleContact.ImageIndex = 1
        Me.tpCircleContact.Location = New System.Drawing.Point(4, 39)
        Me.tpCircleContact.Name = "tpCircleContact"
        Me.tpCircleContact.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCircleContact.Size = New System.Drawing.Size(379, 207)
        Me.tpCircleContact.TabIndex = 3
        Me.tpCircleContact.Text = "Kreis-Fläche"
        '
        'gbCompensationMethodCircle
        '
        Me.gbCompensationMethodCircle.Controls.Add(Me.lblCurcleVectorOffset)
        Me.gbCompensationMethodCircle.Controls.Add(Me.txtCircleVectorOffset)
        Me.gbCompensationMethodCircle.Controls.Add(Me.cboCompensationVectorsCircle)
        Me.gbCompensationMethodCircle.Controls.Add(Me.chkCircleVectorInvert)
        Me.gbCompensationMethodCircle.Controls.Add(Me.rbDefineCompensationVectorCircle)
        Me.gbCompensationMethodCircle.Controls.Add(Me.rbCircleVector)
        Me.gbCompensationMethodCircle.Location = New System.Drawing.Point(8, 15)
        Me.gbCompensationMethodCircle.Name = "gbCompensationMethodCircle"
        Me.gbCompensationMethodCircle.Size = New System.Drawing.Size(365, 137)
        Me.gbCompensationMethodCircle.TabIndex = 1
        Me.gbCompensationMethodCircle.TabStop = False
        Me.gbCompensationMethodCircle.Text = "Kompensationsrichtung"
        '
        'lblCurcleVectorOffset
        '
        Me.lblCurcleVectorOffset.AutoSize = True
        Me.lblCurcleVectorOffset.Location = New System.Drawing.Point(297, 23)
        Me.lblCurcleVectorOffset.Name = "lblCurcleVectorOffset"
        Me.lblCurcleVectorOffset.Size = New System.Drawing.Size(42, 13)
        Me.lblCurcleVectorOffset.TabIndex = 5
        Me.lblCurcleVectorOffset.Text = "Versatz"
        '
        'txtCircleVectorOffset
        '
        Me.txtCircleVectorOffset.Location = New System.Drawing.Point(218, 20)
        Me.txtCircleVectorOffset.Name = "txtCircleVectorOffset"
        Me.txtCircleVectorOffset.Size = New System.Drawing.Size(73, 20)
        Me.txtCircleVectorOffset.TabIndex = 2
        '
        'cboCompensationVectorsCircle
        '
        Me.cboCompensationVectorsCircle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompensationVectorsCircle.Enabled = False
        Me.cboCompensationVectorsCircle.FormattingEnabled = True
        Me.cboCompensationVectorsCircle.Items.AddRange(New Object() {"X+", "X-", "Y+", "Y-", "Z+", "Z-"})
        Me.cboCompensationVectorsCircle.Location = New System.Drawing.Point(37, 104)
        Me.cboCompensationVectorsCircle.MaxDropDownItems = 6
        Me.cboCompensationVectorsCircle.Name = "cboCompensationVectorsCircle"
        Me.cboCompensationVectorsCircle.Size = New System.Drawing.Size(64, 21)
        Me.cboCompensationVectorsCircle.TabIndex = 3
        '
        'chkCircleVectorInvert
        '
        Me.chkCircleVectorInvert.AutoSize = True
        Me.chkCircleVectorInvert.Location = New System.Drawing.Point(37, 42)
        Me.chkCircleVectorInvert.Name = "chkCircleVectorInvert"
        Me.chkCircleVectorInvert.Size = New System.Drawing.Size(66, 17)
        Me.chkCircleVectorInvert.TabIndex = 1
        Me.chkCircleVectorInvert.Text = "invertiert"
        Me.chkCircleVectorInvert.UseVisualStyleBackColor = True
        '
        'rbDefineCompensationVectorCircle
        '
        Me.rbDefineCompensationVectorCircle.AutoSize = True
        Me.rbDefineCompensationVectorCircle.Location = New System.Drawing.Point(19, 81)
        Me.rbDefineCompensationVectorCircle.Name = "rbDefineCompensationVectorCircle"
        Me.rbDefineCompensationVectorCircle.Size = New System.Drawing.Size(61, 17)
        Me.rbDefineCompensationVectorCircle.TabIndex = 1
        Me.rbDefineCompensationVectorCircle.Text = "Entlang"
        Me.rbDefineCompensationVectorCircle.UseVisualStyleBackColor = True
        '
        'rbCircleVector
        '
        Me.rbCircleVector.AutoSize = True
        Me.rbCircleVector.Checked = True
        Me.rbCircleVector.Location = New System.Drawing.Point(19, 19)
        Me.rbCircleVector.Name = "rbCircleVector"
        Me.rbCircleVector.Size = New System.Drawing.Size(132, 17)
        Me.rbCircleVector.TabIndex = 0
        Me.rbCircleVector.TabStop = True
        Me.rbCircleVector.Text = "Vektor Referenzebene"
        Me.rbCircleVector.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(387, 365)
        Me.Controls.Add(Me.tcMiscellaneous)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmMain"
        Me.Text = "Sonstiges"
        Me.TopMost = true
        Me.pnlBottom.ResumeLayout(false)
        Me.tcMiscellaneous.ResumeLayout(false)
        Me.tpSphereContact.ResumeLayout(false)
        Me.gbCompensationMethod.ResumeLayout(false)
        Me.gbCompensationMethod.PerformLayout
        Me.tpCircleContact.ResumeLayout(false)
        Me.gbCompensationMethodCircle.ResumeLayout(false)
        Me.gbCompensationMethodCircle.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pbModifying As System.Windows.Forms.ProgressBar
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents ilTabs As System.Windows.Forms.ImageList
    Friend WithEvents tcMiscellaneous As System.Windows.Forms.TabControl
    Friend WithEvents tpSphereContact As System.Windows.Forms.TabPage
    Friend WithEvents gbCompensationMethod As System.Windows.Forms.GroupBox
    Friend WithEvents cboCompensationVectors As System.Windows.Forms.ComboBox
    Friend WithEvents chkSphereVectorInvert As System.Windows.Forms.CheckBox
    Friend WithEvents rbDefineCompensationVector As System.Windows.Forms.RadioButton
    Friend WithEvents rbShpereVector As System.Windows.Forms.RadioButton
    Friend WithEvents tpCircleContact As System.Windows.Forms.TabPage
    Friend WithEvents gbCompensationMethodCircle As System.Windows.Forms.GroupBox
    Friend WithEvents lblCurcleVectorOffset As System.Windows.Forms.Label
    Friend WithEvents txtCircleVectorOffset As System.Windows.Forms.TextBox
    Friend WithEvents cboCompensationVectorsCircle As System.Windows.Forms.ComboBox
    Friend WithEvents chkCircleVectorInvert As System.Windows.Forms.CheckBox
    Friend WithEvents rbDefineCompensationVectorCircle As System.Windows.Forms.RadioButton
    Friend WithEvents rbCircleVector As System.Windows.Forms.RadioButton

End Class
