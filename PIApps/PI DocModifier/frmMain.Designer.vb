<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.pbModifying = New System.Windows.Forms.ProgressBar()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.tcChangeItems = New System.Windows.Forms.TabControl()
        Me.tpSinglePoint = New System.Windows.Forms.TabPage()
        Me.lblThickness = New System.Windows.Forms.Label()
        Me.txtThickness = New System.Windows.Forms.TextBox()
        Me.gpOutputtoReportSinglePoint = New System.Windows.Forms.GroupBox()
        Me.chkDistancePlane = New System.Windows.Forms.CheckBox()
        Me.chkDistanceTargetplane = New System.Windows.Forms.CheckBox()
        Me.chkDistanceTargetPoint = New System.Windows.Forms.CheckBox()
        Me.chkTargetPointZ = New System.Windows.Forms.CheckBox()
        Me.chkTargetPointY = New System.Windows.Forms.CheckBox()
        Me.chkTargetPointX = New System.Windows.Forms.CheckBox()
        Me.chkTargetPoint = New System.Windows.Forms.CheckBox()
        Me.chkVectorInvert = New System.Windows.Forms.CheckBox()
        Me.txtChangeNameSinglePoint = New System.Windows.Forms.TextBox()
        Me.chkChangeNameSinglePoint = New System.Windows.Forms.CheckBox()
        Me.tpCircle = New System.Windows.Forms.TabPage()
        Me.gbDiameter = New System.Windows.Forms.GroupBox()
        Me.rbDiameter = New System.Windows.Forms.RadioButton()
        Me.rbRadius = New System.Windows.Forms.RadioButton()
        Me.gpOutputtoReportCircle = New System.Windows.Forms.GroupBox()
        Me.chkDiameter = New System.Windows.Forms.CheckBox()
        Me.chkFilltingAlgo = New System.Windows.Forms.CheckBox()
        Me.chkCircularity = New System.Windows.Forms.CheckBox()
        Me.chkOffset = New System.Windows.Forms.CheckBox()
        Me.chkCenterZ = New System.Windows.Forms.CheckBox()
        Me.chkCenterY = New System.Windows.Forms.CheckBox()
        Me.chkCenterX = New System.Windows.Forms.CheckBox()
        Me.chkCenter = New System.Windows.Forms.CheckBox()
        Me.txtChangeNameCircle = New System.Windows.Forms.TextBox()
        Me.chkChangeNameCircle = New System.Windows.Forms.CheckBox()
        Me.tpCircleCompound = New System.Windows.Forms.TabPage()
        Me.gbDiameterCircleCompound = New System.Windows.Forms.GroupBox()
        Me.rbDiameterCircleCompound = New System.Windows.Forms.RadioButton()
        Me.rbRadiusCircleCompound = New System.Windows.Forms.RadioButton()
        Me.gpOutputtoReportCircleCompound = New System.Windows.Forms.GroupBox()
        Me.chkNominalVectorKCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkNominalVectorJCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkNominalVectorICircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkNominalVectorCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkDiameterCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkFilltingAlgoCircleCpompound = New System.Windows.Forms.CheckBox()
        Me.chkCircularityCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkCenterZCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkCenterYCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkCenterXCircleCompound = New System.Windows.Forms.CheckBox()
        Me.chkCenterCircleCompound = New System.Windows.Forms.CheckBox()
        Me.txtChangeNameCircleCompound = New System.Windows.Forms.TextBox()
        Me.chkChangeNameCircleCompound = New System.Windows.Forms.CheckBox()
        Me.pnlBottom.SuspendLayout()
        Me.tcChangeItems.SuspendLayout()
        Me.tpSinglePoint.SuspendLayout()
        Me.gpOutputtoReportSinglePoint.SuspendLayout()
        Me.tpCircle.SuspendLayout()
        Me.gbDiameter.SuspendLayout()
        Me.gpOutputtoReportCircle.SuspendLayout()
        Me.tpCircleCompound.SuspendLayout()
        Me.gbDiameterCircleCompound.SuspendLayout()
        Me.gpOutputtoReportCircleCompound.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnAbout)
        Me.pnlBottom.Controls.Add(Me.pbModifying)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 402)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(325, 86)
        Me.pnlBottom.TabIndex = 1
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
        Me.btnAbout.Location = New System.Drawing.Point(4, 9)
        Me.btnAbout.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(85, 47)
        Me.btnAbout.TabIndex = 13
        Me.btnAbout.Text = "Über..."
        Me.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'pbModifying
        '
        Me.pbModifying.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbModifying.Location = New System.Drawing.Point(0, 63)
        Me.pbModifying.Name = "pbModifying"
        Me.pbModifying.Size = New System.Drawing.Size(325, 23)
        Me.pbModifying.TabIndex = 12
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(267, 9)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(51, 47)
        Me.btnOK.TabIndex = 0
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'tcChangeItems
        '
        Me.tcChangeItems.Controls.Add(Me.tpSinglePoint)
        Me.tcChangeItems.Controls.Add(Me.tpCircle)
        Me.tcChangeItems.Controls.Add(Me.tpCircleCompound)
        Me.tcChangeItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcChangeItems.Location = New System.Drawing.Point(0, 0)
        Me.tcChangeItems.Name = "tcChangeItems"
        Me.tcChangeItems.SelectedIndex = 0
        Me.tcChangeItems.Size = New System.Drawing.Size(325, 402)
        Me.tcChangeItems.TabIndex = 0
        '
        'tpSinglePoint
        '
        Me.tpSinglePoint.Controls.Add(Me.lblThickness)
        Me.tpSinglePoint.Controls.Add(Me.txtThickness)
        Me.tpSinglePoint.Controls.Add(Me.gpOutputtoReportSinglePoint)
        Me.tpSinglePoint.Controls.Add(Me.chkVectorInvert)
        Me.tpSinglePoint.Controls.Add(Me.txtChangeNameSinglePoint)
        Me.tpSinglePoint.Controls.Add(Me.chkChangeNameSinglePoint)
        Me.tpSinglePoint.Location = New System.Drawing.Point(4, 22)
        Me.tpSinglePoint.Name = "tpSinglePoint"
        Me.tpSinglePoint.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSinglePoint.Size = New System.Drawing.Size(317, 376)
        Me.tpSinglePoint.TabIndex = 0
        Me.tpSinglePoint.Text = "Geführte Einzelpunkte"
        Me.tpSinglePoint.UseVisualStyleBackColor = True
        '
        'lblThickness
        '
        Me.lblThickness.AutoSize = True
        Me.lblThickness.Location = New System.Drawing.Point(7, 310)
        Me.lblThickness.Name = "lblThickness"
        Me.lblThickness.Size = New System.Drawing.Size(168, 13)
        Me.lblThickness.TabIndex = 4
        Me.lblThickness.Text = "Abstand Zielebene, Materialstärke"
        '
        'txtThickness
        '
        Me.txtThickness.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtThickness.Location = New System.Drawing.Point(181, 307)
        Me.txtThickness.Name = "txtThickness"
        Me.txtThickness.Size = New System.Drawing.Size(128, 20)
        Me.txtThickness.TabIndex = 5
        '
        'gpOutputtoReportSinglePoint
        '
        Me.gpOutputtoReportSinglePoint.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkDistancePlane)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkDistanceTargetplane)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkDistanceTargetPoint)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkTargetPointZ)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkTargetPointY)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkTargetPointX)
        Me.gpOutputtoReportSinglePoint.Controls.Add(Me.chkTargetPoint)
        Me.gpOutputtoReportSinglePoint.Location = New System.Drawing.Point(8, 55)
        Me.gpOutputtoReportSinglePoint.Name = "gpOutputtoReportSinglePoint"
        Me.gpOutputtoReportSinglePoint.Size = New System.Drawing.Size(300, 212)
        Me.gpOutputtoReportSinglePoint.TabIndex = 2
        Me.gpOutputtoReportSinglePoint.TabStop = False
        Me.gpOutputtoReportSinglePoint.Text = "Ausgabe in Bericht"
        '
        'chkDistancePlane
        '
        Me.chkDistancePlane.AutoSize = True
        Me.chkDistancePlane.Location = New System.Drawing.Point(28, 135)
        Me.chkDistancePlane.Name = "chkDistancePlane"
        Me.chkDistancePlane.Size = New System.Drawing.Size(116, 17)
        Me.chkDistancePlane.TabIndex = 6
        Me.chkDistancePlane.Text = "Abstand zur Ebene"
        Me.chkDistancePlane.UseVisualStyleBackColor = True
        '
        'chkDistanceTargetplane
        '
        Me.chkDistanceTargetplane.AutoSize = True
        Me.chkDistanceTargetplane.Location = New System.Drawing.Point(28, 116)
        Me.chkDistanceTargetplane.Name = "chkDistanceTargetplane"
        Me.chkDistanceTargetplane.Size = New System.Drawing.Size(132, 17)
        Me.chkDistanceTargetplane.TabIndex = 5
        Me.chkDistanceTargetplane.Text = "Abstand zur Zielebene"
        Me.chkDistanceTargetplane.UseVisualStyleBackColor = True
        '
        'chkDistanceTargetPoint
        '
        Me.chkDistanceTargetPoint.AutoSize = True
        Me.chkDistanceTargetPoint.Location = New System.Drawing.Point(28, 97)
        Me.chkDistanceTargetPoint.Name = "chkDistanceTargetPoint"
        Me.chkDistanceTargetPoint.Size = New System.Drawing.Size(134, 17)
        Me.chkDistanceTargetPoint.TabIndex = 4
        Me.chkDistanceTargetPoint.Text = "Abstand zum Zielpunkt"
        Me.chkDistanceTargetPoint.UseVisualStyleBackColor = True
        '
        'chkTargetPointZ
        '
        Me.chkTargetPointZ.AutoSize = True
        Me.chkTargetPointZ.Location = New System.Drawing.Point(71, 80)
        Me.chkTargetPointZ.Name = "chkTargetPointZ"
        Me.chkTargetPointZ.Size = New System.Drawing.Size(33, 17)
        Me.chkTargetPointZ.TabIndex = 3
        Me.chkTargetPointZ.Text = "Z"
        Me.chkTargetPointZ.UseVisualStyleBackColor = True
        '
        'chkTargetPointY
        '
        Me.chkTargetPointY.AutoSize = True
        Me.chkTargetPointY.Location = New System.Drawing.Point(71, 61)
        Me.chkTargetPointY.Name = "chkTargetPointY"
        Me.chkTargetPointY.Size = New System.Drawing.Size(33, 17)
        Me.chkTargetPointY.TabIndex = 2
        Me.chkTargetPointY.Text = "Y"
        Me.chkTargetPointY.UseVisualStyleBackColor = True
        '
        'chkTargetPointX
        '
        Me.chkTargetPointX.AutoSize = True
        Me.chkTargetPointX.Location = New System.Drawing.Point(71, 42)
        Me.chkTargetPointX.Name = "chkTargetPointX"
        Me.chkTargetPointX.Size = New System.Drawing.Size(33, 17)
        Me.chkTargetPointX.TabIndex = 1
        Me.chkTargetPointX.Text = "X"
        Me.chkTargetPointX.UseVisualStyleBackColor = True
        '
        'chkTargetPoint
        '
        Me.chkTargetPoint.AutoSize = True
        Me.chkTargetPoint.Location = New System.Drawing.Point(28, 19)
        Me.chkTargetPoint.Name = "chkTargetPoint"
        Me.chkTargetPoint.Size = New System.Drawing.Size(70, 17)
        Me.chkTargetPoint.TabIndex = 0
        Me.chkTargetPoint.Text = "Zielpunkt"
        Me.chkTargetPoint.UseVisualStyleBackColor = True
        '
        'chkVectorInvert
        '
        Me.chkVectorInvert.AutoSize = True
        Me.chkVectorInvert.Location = New System.Drawing.Point(8, 280)
        Me.chkVectorInvert.Name = "chkVectorInvert"
        Me.chkVectorInvert.Size = New System.Drawing.Size(183, 17)
        Me.chkVectorInvert.TabIndex = 3
        Me.chkVectorInvert.Text = "Antastrichtung, Vektor invertieren"
        Me.chkVectorInvert.UseVisualStyleBackColor = True
        '
        'txtChangeNameSinglePoint
        '
        Me.txtChangeNameSinglePoint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChangeNameSinglePoint.Location = New System.Drawing.Point(141, 21)
        Me.txtChangeNameSinglePoint.Name = "txtChangeNameSinglePoint"
        Me.txtChangeNameSinglePoint.Size = New System.Drawing.Size(168, 20)
        Me.txtChangeNameSinglePoint.TabIndex = 1
        '
        'chkChangeNameSinglePoint
        '
        Me.chkChangeNameSinglePoint.AutoSize = True
        Me.chkChangeNameSinglePoint.Location = New System.Drawing.Point(8, 23)
        Me.chkChangeNameSinglePoint.Name = "chkChangeNameSinglePoint"
        Me.chkChangeNameSinglePoint.Size = New System.Drawing.Size(127, 17)
        Me.chkChangeNameSinglePoint.TabIndex = 0
        Me.chkChangeNameSinglePoint.Text = "Ändere Elementname"
        Me.chkChangeNameSinglePoint.UseVisualStyleBackColor = True
        '
        'tpCircle
        '
        Me.tpCircle.Controls.Add(Me.gbDiameter)
        Me.tpCircle.Controls.Add(Me.gpOutputtoReportCircle)
        Me.tpCircle.Controls.Add(Me.txtChangeNameCircle)
        Me.tpCircle.Controls.Add(Me.chkChangeNameCircle)
        Me.tpCircle.Location = New System.Drawing.Point(4, 22)
        Me.tpCircle.Name = "tpCircle"
        Me.tpCircle.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCircle.Size = New System.Drawing.Size(317, 376)
        Me.tpCircle.TabIndex = 1
        Me.tpCircle.Text = "Gemessener Kreis"
        Me.tpCircle.UseVisualStyleBackColor = True
        '
        'gbDiameter
        '
        Me.gbDiameter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDiameter.Controls.Add(Me.rbDiameter)
        Me.gbDiameter.Controls.Add(Me.rbRadius)
        Me.gbDiameter.Location = New System.Drawing.Point(6, 272)
        Me.gbDiameter.Name = "gbDiameter"
        Me.gbDiameter.Size = New System.Drawing.Size(303, 65)
        Me.gbDiameter.TabIndex = 2
        Me.gbDiameter.TabStop = False
        Me.gbDiameter.Text = "Durchmesser"
        '
        'rbDiameter
        '
        Me.rbDiameter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbDiameter.AutoSize = True
        Me.rbDiameter.Location = New System.Drawing.Point(210, 31)
        Me.rbDiameter.Name = "rbDiameter"
        Me.rbDiameter.Size = New System.Drawing.Size(87, 17)
        Me.rbDiameter.TabIndex = 1
        Me.rbDiameter.TabStop = True
        Me.rbDiameter.Text = "Durchmesser"
        Me.rbDiameter.UseVisualStyleBackColor = True
        '
        'rbRadius
        '
        Me.rbRadius.AutoSize = True
        Me.rbRadius.Location = New System.Drawing.Point(30, 31)
        Me.rbRadius.Name = "rbRadius"
        Me.rbRadius.Size = New System.Drawing.Size(58, 17)
        Me.rbRadius.TabIndex = 0
        Me.rbRadius.TabStop = True
        Me.rbRadius.Text = "Radius"
        Me.rbRadius.UseVisualStyleBackColor = True
        '
        'gpOutputtoReportCircle
        '
        Me.gpOutputtoReportCircle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkDiameter)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkFilltingAlgo)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkCircularity)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkOffset)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkCenterZ)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkCenterY)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkCenterX)
        Me.gpOutputtoReportCircle.Controls.Add(Me.chkCenter)
        Me.gpOutputtoReportCircle.Location = New System.Drawing.Point(8, 55)
        Me.gpOutputtoReportCircle.Name = "gpOutputtoReportCircle"
        Me.gpOutputtoReportCircle.Size = New System.Drawing.Size(300, 210)
        Me.gpOutputtoReportCircle.TabIndex = 6
        Me.gpOutputtoReportCircle.TabStop = False
        Me.gpOutputtoReportCircle.Text = "Ausgabe in Bericht"
        '
        'chkDiameter
        '
        Me.chkDiameter.AutoSize = True
        Me.chkDiameter.Location = New System.Drawing.Point(28, 120)
        Me.chkDiameter.Name = "chkDiameter"
        Me.chkDiameter.Size = New System.Drawing.Size(88, 17)
        Me.chkDiameter.TabIndex = 5
        Me.chkDiameter.Text = "Durchmesser"
        Me.chkDiameter.UseVisualStyleBackColor = True
        '
        'chkFilltingAlgo
        '
        Me.chkFilltingAlgo.AutoSize = True
        Me.chkFilltingAlgo.Location = New System.Drawing.Point(28, 19)
        Me.chkFilltingAlgo.Name = "chkFilltingAlgo"
        Me.chkFilltingAlgo.Size = New System.Drawing.Size(141, 17)
        Me.chkFilltingAlgo.TabIndex = 0
        Me.chkFilltingAlgo.Text = "Anpassungs-Algorithmus"
        Me.chkFilltingAlgo.UseVisualStyleBackColor = True
        '
        'chkCircularity
        '
        Me.chkCircularity.AutoSize = True
        Me.chkCircularity.Location = New System.Drawing.Point(28, 158)
        Me.chkCircularity.Name = "chkCircularity"
        Me.chkCircularity.Size = New System.Drawing.Size(94, 17)
        Me.chkCircularity.TabIndex = 7
        Me.chkCircularity.Text = "Kreisförmigkeit"
        Me.chkCircularity.UseVisualStyleBackColor = True
        '
        'chkOffset
        '
        Me.chkOffset.AutoSize = True
        Me.chkOffset.Location = New System.Drawing.Point(28, 139)
        Me.chkOffset.Name = "chkOffset"
        Me.chkOffset.Size = New System.Drawing.Size(97, 17)
        Me.chkOffset.TabIndex = 6
        Me.chkOffset.Text = "Versatz/Stärke"
        Me.chkOffset.UseVisualStyleBackColor = True
        '
        'chkCenterZ
        '
        Me.chkCenterZ.AutoSize = True
        Me.chkCenterZ.Location = New System.Drawing.Point(71, 99)
        Me.chkCenterZ.Name = "chkCenterZ"
        Me.chkCenterZ.Size = New System.Drawing.Size(33, 17)
        Me.chkCenterZ.TabIndex = 4
        Me.chkCenterZ.Text = "Z"
        Me.chkCenterZ.UseVisualStyleBackColor = True
        '
        'chkCenterY
        '
        Me.chkCenterY.AutoSize = True
        Me.chkCenterY.Location = New System.Drawing.Point(71, 80)
        Me.chkCenterY.Name = "chkCenterY"
        Me.chkCenterY.Size = New System.Drawing.Size(33, 17)
        Me.chkCenterY.TabIndex = 3
        Me.chkCenterY.Text = "Y"
        Me.chkCenterY.UseVisualStyleBackColor = True
        '
        'chkCenterX
        '
        Me.chkCenterX.AutoSize = True
        Me.chkCenterX.Location = New System.Drawing.Point(71, 61)
        Me.chkCenterX.Name = "chkCenterX"
        Me.chkCenterX.Size = New System.Drawing.Size(33, 17)
        Me.chkCenterX.TabIndex = 2
        Me.chkCenterX.Text = "X"
        Me.chkCenterX.UseVisualStyleBackColor = True
        '
        'chkCenter
        '
        Me.chkCenter.AutoSize = True
        Me.chkCenter.Location = New System.Drawing.Point(28, 38)
        Me.chkCenter.Name = "chkCenter"
        Me.chkCenter.Size = New System.Drawing.Size(49, 17)
        Me.chkCenter.TabIndex = 1
        Me.chkCenter.Text = "Mitte"
        Me.chkCenter.UseVisualStyleBackColor = True
        '
        'txtChangeNameCircle
        '
        Me.txtChangeNameCircle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChangeNameCircle.Enabled = False
        Me.txtChangeNameCircle.Location = New System.Drawing.Point(141, 21)
        Me.txtChangeNameCircle.Name = "txtChangeNameCircle"
        Me.txtChangeNameCircle.Size = New System.Drawing.Size(168, 20)
        Me.txtChangeNameCircle.TabIndex = 1
        '
        'chkChangeNameCircle
        '
        Me.chkChangeNameCircle.AutoSize = True
        Me.chkChangeNameCircle.Location = New System.Drawing.Point(8, 23)
        Me.chkChangeNameCircle.Name = "chkChangeNameCircle"
        Me.chkChangeNameCircle.Size = New System.Drawing.Size(127, 17)
        Me.chkChangeNameCircle.TabIndex = 0
        Me.chkChangeNameCircle.Text = "Ändere Elementname"
        Me.chkChangeNameCircle.UseVisualStyleBackColor = True
        '
        'tpCircleCompound
        '
        Me.tpCircleCompound.Controls.Add(Me.gbDiameterCircleCompound)
        Me.tpCircleCompound.Controls.Add(Me.gpOutputtoReportCircleCompound)
        Me.tpCircleCompound.Controls.Add(Me.txtChangeNameCircleCompound)
        Me.tpCircleCompound.Controls.Add(Me.chkChangeNameCircleCompound)
        Me.tpCircleCompound.Location = New System.Drawing.Point(4, 22)
        Me.tpCircleCompound.Name = "tpCircleCompound"
        Me.tpCircleCompound.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCircleCompound.Size = New System.Drawing.Size(317, 376)
        Me.tpCircleCompound.TabIndex = 2
        Me.tpCircleCompound.Text = "Verbund-Kreis"
        Me.tpCircleCompound.UseVisualStyleBackColor = True
        '
        'gbDiameterCircleCompound
        '
        Me.gbDiameterCircleCompound.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDiameterCircleCompound.Controls.Add(Me.rbDiameterCircleCompound)
        Me.gbDiameterCircleCompound.Controls.Add(Me.rbRadiusCircleCompound)
        Me.gbDiameterCircleCompound.Location = New System.Drawing.Point(6, 303)
        Me.gbDiameterCircleCompound.Name = "gbDiameterCircleCompound"
        Me.gbDiameterCircleCompound.Size = New System.Drawing.Size(303, 65)
        Me.gbDiameterCircleCompound.TabIndex = 3
        Me.gbDiameterCircleCompound.TabStop = False
        Me.gbDiameterCircleCompound.Text = "Durchmesser"
        '
        'rbDiameterCircleCompound
        '
        Me.rbDiameterCircleCompound.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbDiameterCircleCompound.AutoSize = True
        Me.rbDiameterCircleCompound.Location = New System.Drawing.Point(210, 31)
        Me.rbDiameterCircleCompound.Name = "rbDiameterCircleCompound"
        Me.rbDiameterCircleCompound.Size = New System.Drawing.Size(87, 17)
        Me.rbDiameterCircleCompound.TabIndex = 1
        Me.rbDiameterCircleCompound.TabStop = True
        Me.rbDiameterCircleCompound.Text = "Durchmesser"
        Me.rbDiameterCircleCompound.UseVisualStyleBackColor = True
        '
        'rbRadiusCircleCompound
        '
        Me.rbRadiusCircleCompound.AutoSize = True
        Me.rbRadiusCircleCompound.Location = New System.Drawing.Point(30, 31)
        Me.rbRadiusCircleCompound.Name = "rbRadiusCircleCompound"
        Me.rbRadiusCircleCompound.Size = New System.Drawing.Size(58, 17)
        Me.rbRadiusCircleCompound.TabIndex = 0
        Me.rbRadiusCircleCompound.TabStop = True
        Me.rbRadiusCircleCompound.Text = "Radius"
        Me.rbRadiusCircleCompound.UseVisualStyleBackColor = True
        '
        'gpOutputtoReportCircleCompound
        '
        Me.gpOutputtoReportCircleCompound.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkNominalVectorKCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkNominalVectorJCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkNominalVectorICircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkNominalVectorCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkDiameterCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkFilltingAlgoCircleCpompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkCircularityCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkCenterZCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkCenterYCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkCenterXCircleCompound)
        Me.gpOutputtoReportCircleCompound.Controls.Add(Me.chkCenterCircleCompound)
        Me.gpOutputtoReportCircleCompound.Location = New System.Drawing.Point(8, 55)
        Me.gpOutputtoReportCircleCompound.Name = "gpOutputtoReportCircleCompound"
        Me.gpOutputtoReportCircleCompound.Size = New System.Drawing.Size(300, 241)
        Me.gpOutputtoReportCircleCompound.TabIndex = 2
        Me.gpOutputtoReportCircleCompound.TabStop = False
        Me.gpOutputtoReportCircleCompound.Text = "Ausgabe in Bericht"
        '
        'chkNominalVectorKCircleCompound
        '
        Me.chkNominalVectorKCircleCompound.AutoSize = True
        Me.chkNominalVectorKCircleCompound.Location = New System.Drawing.Point(71, 219)
        Me.chkNominalVectorKCircleCompound.Name = "chkNominalVectorKCircleCompound"
        Me.chkNominalVectorKCircleCompound.Size = New System.Drawing.Size(33, 17)
        Me.chkNominalVectorKCircleCompound.TabIndex = 10
        Me.chkNominalVectorKCircleCompound.Text = "K"
        Me.chkNominalVectorKCircleCompound.UseVisualStyleBackColor = True
        '
        'chkNominalVectorJCircleCompound
        '
        Me.chkNominalVectorJCircleCompound.AutoSize = True
        Me.chkNominalVectorJCircleCompound.Location = New System.Drawing.Point(71, 200)
        Me.chkNominalVectorJCircleCompound.Name = "chkNominalVectorJCircleCompound"
        Me.chkNominalVectorJCircleCompound.Size = New System.Drawing.Size(31, 17)
        Me.chkNominalVectorJCircleCompound.TabIndex = 9
        Me.chkNominalVectorJCircleCompound.Text = "J"
        Me.chkNominalVectorJCircleCompound.UseVisualStyleBackColor = True
        '
        'chkNominalVectorICircleCompound
        '
        Me.chkNominalVectorICircleCompound.AutoSize = True
        Me.chkNominalVectorICircleCompound.Location = New System.Drawing.Point(71, 181)
        Me.chkNominalVectorICircleCompound.Name = "chkNominalVectorICircleCompound"
        Me.chkNominalVectorICircleCompound.Size = New System.Drawing.Size(29, 17)
        Me.chkNominalVectorICircleCompound.TabIndex = 8
        Me.chkNominalVectorICircleCompound.Text = "I"
        Me.chkNominalVectorICircleCompound.UseVisualStyleBackColor = True
        '
        'chkNominalVectorCircleCompound
        '
        Me.chkNominalVectorCircleCompound.AutoSize = True
        Me.chkNominalVectorCircleCompound.Location = New System.Drawing.Point(28, 158)
        Me.chkNominalVectorCircleCompound.Name = "chkNominalVectorCircleCompound"
        Me.chkNominalVectorCircleCompound.Size = New System.Drawing.Size(101, 17)
        Me.chkNominalVectorCircleCompound.TabIndex = 7
        Me.chkNominalVectorCircleCompound.Text = "Normalenvektor"
        Me.chkNominalVectorCircleCompound.UseVisualStyleBackColor = True
        '
        'chkDiameterCircleCompound
        '
        Me.chkDiameterCircleCompound.AutoSize = True
        Me.chkDiameterCircleCompound.Location = New System.Drawing.Point(28, 120)
        Me.chkDiameterCircleCompound.Name = "chkDiameterCircleCompound"
        Me.chkDiameterCircleCompound.Size = New System.Drawing.Size(88, 17)
        Me.chkDiameterCircleCompound.TabIndex = 5
        Me.chkDiameterCircleCompound.Text = "Durchmesser"
        Me.chkDiameterCircleCompound.UseVisualStyleBackColor = True
        '
        'chkFilltingAlgoCircleCpompound
        '
        Me.chkFilltingAlgoCircleCpompound.AutoSize = True
        Me.chkFilltingAlgoCircleCpompound.Location = New System.Drawing.Point(28, 19)
        Me.chkFilltingAlgoCircleCpompound.Name = "chkFilltingAlgoCircleCpompound"
        Me.chkFilltingAlgoCircleCpompound.Size = New System.Drawing.Size(141, 17)
        Me.chkFilltingAlgoCircleCpompound.TabIndex = 0
        Me.chkFilltingAlgoCircleCpompound.Text = "Anpassungs-Algorithmus"
        Me.chkFilltingAlgoCircleCpompound.UseVisualStyleBackColor = True
        '
        'chkCircularityCircleCompound
        '
        Me.chkCircularityCircleCompound.AutoSize = True
        Me.chkCircularityCircleCompound.Location = New System.Drawing.Point(28, 139)
        Me.chkCircularityCircleCompound.Name = "chkCircularityCircleCompound"
        Me.chkCircularityCircleCompound.Size = New System.Drawing.Size(94, 17)
        Me.chkCircularityCircleCompound.TabIndex = 6
        Me.chkCircularityCircleCompound.Text = "Kreisförmigkeit"
        Me.chkCircularityCircleCompound.UseVisualStyleBackColor = True
        '
        'chkCenterZCircleCompound
        '
        Me.chkCenterZCircleCompound.AutoSize = True
        Me.chkCenterZCircleCompound.Location = New System.Drawing.Point(71, 99)
        Me.chkCenterZCircleCompound.Name = "chkCenterZCircleCompound"
        Me.chkCenterZCircleCompound.Size = New System.Drawing.Size(33, 17)
        Me.chkCenterZCircleCompound.TabIndex = 4
        Me.chkCenterZCircleCompound.Text = "Z"
        Me.chkCenterZCircleCompound.UseVisualStyleBackColor = True
        '
        'chkCenterYCircleCompound
        '
        Me.chkCenterYCircleCompound.AutoSize = True
        Me.chkCenterYCircleCompound.Location = New System.Drawing.Point(71, 80)
        Me.chkCenterYCircleCompound.Name = "chkCenterYCircleCompound"
        Me.chkCenterYCircleCompound.Size = New System.Drawing.Size(33, 17)
        Me.chkCenterYCircleCompound.TabIndex = 3
        Me.chkCenterYCircleCompound.Text = "Y"
        Me.chkCenterYCircleCompound.UseVisualStyleBackColor = True
        '
        'chkCenterXCircleCompound
        '
        Me.chkCenterXCircleCompound.AutoSize = True
        Me.chkCenterXCircleCompound.Location = New System.Drawing.Point(71, 61)
        Me.chkCenterXCircleCompound.Name = "chkCenterXCircleCompound"
        Me.chkCenterXCircleCompound.Size = New System.Drawing.Size(33, 17)
        Me.chkCenterXCircleCompound.TabIndex = 2
        Me.chkCenterXCircleCompound.Text = "X"
        Me.chkCenterXCircleCompound.UseVisualStyleBackColor = True
        '
        'chkCenterCircleCompound
        '
        Me.chkCenterCircleCompound.AutoSize = True
        Me.chkCenterCircleCompound.Location = New System.Drawing.Point(28, 38)
        Me.chkCenterCircleCompound.Name = "chkCenterCircleCompound"
        Me.chkCenterCircleCompound.Size = New System.Drawing.Size(49, 17)
        Me.chkCenterCircleCompound.TabIndex = 1
        Me.chkCenterCircleCompound.Text = "Mitte"
        Me.chkCenterCircleCompound.UseVisualStyleBackColor = True
        '
        'txtChangeNameCircleCompound
        '
        Me.txtChangeNameCircleCompound.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtChangeNameCircleCompound.Enabled = False
        Me.txtChangeNameCircleCompound.Location = New System.Drawing.Point(141, 21)
        Me.txtChangeNameCircleCompound.Name = "txtChangeNameCircleCompound"
        Me.txtChangeNameCircleCompound.Size = New System.Drawing.Size(168, 20)
        Me.txtChangeNameCircleCompound.TabIndex = 1
        '
        'chkChangeNameCircleCompound
        '
        Me.chkChangeNameCircleCompound.AutoSize = True
        Me.chkChangeNameCircleCompound.Location = New System.Drawing.Point(8, 23)
        Me.chkChangeNameCircleCompound.Name = "chkChangeNameCircleCompound"
        Me.chkChangeNameCircleCompound.Size = New System.Drawing.Size(127, 17)
        Me.chkChangeNameCircleCompound.TabIndex = 0
        Me.chkChangeNameCircleCompound.Text = "Ändere Elementname"
        Me.chkChangeNameCircleCompound.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 488)
        Me.Controls.Add(Me.tcChangeItems)
        Me.Controls.Add(Me.pnlBottom)
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmMain"
        Me.Text = "Eigenschaften ändern..."
        Me.TopMost = true
        Me.pnlBottom.ResumeLayout(false)
        Me.tcChangeItems.ResumeLayout(false)
        Me.tpSinglePoint.ResumeLayout(false)
        Me.tpSinglePoint.PerformLayout
        Me.gpOutputtoReportSinglePoint.ResumeLayout(false)
        Me.gpOutputtoReportSinglePoint.PerformLayout
        Me.tpCircle.ResumeLayout(false)
        Me.tpCircle.PerformLayout
        Me.gbDiameter.ResumeLayout(false)
        Me.gbDiameter.PerformLayout
        Me.gpOutputtoReportCircle.ResumeLayout(false)
        Me.gpOutputtoReportCircle.PerformLayout
        Me.tpCircleCompound.ResumeLayout(false)
        Me.tpCircleCompound.PerformLayout
        Me.gbDiameterCircleCompound.ResumeLayout(false)
        Me.gbDiameterCircleCompound.PerformLayout
        Me.gpOutputtoReportCircleCompound.ResumeLayout(false)
        Me.gpOutputtoReportCircleCompound.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents tcChangeItems As System.Windows.Forms.TabControl
    Friend WithEvents tpSinglePoint As System.Windows.Forms.TabPage
    Friend WithEvents tpCircle As System.Windows.Forms.TabPage
    Friend WithEvents gpOutputtoReportSinglePoint As System.Windows.Forms.GroupBox
    Friend WithEvents chkDistancePlane As System.Windows.Forms.CheckBox
    Friend WithEvents chkDistanceTargetplane As System.Windows.Forms.CheckBox
    Friend WithEvents chkDistanceTargetPoint As System.Windows.Forms.CheckBox
    Friend WithEvents chkTargetPointZ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTargetPointY As System.Windows.Forms.CheckBox
    Friend WithEvents chkTargetPointX As System.Windows.Forms.CheckBox
    Friend WithEvents chkTargetPoint As System.Windows.Forms.CheckBox
    Friend WithEvents txtChangeNameSinglePoint As System.Windows.Forms.TextBox
    Friend WithEvents chkChangeNameSinglePoint As System.Windows.Forms.CheckBox
    Friend WithEvents gpOutputtoReportCircle As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilltingAlgo As System.Windows.Forms.CheckBox
    Friend WithEvents chkCircularity As System.Windows.Forms.CheckBox
    Friend WithEvents chkOffset As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterZ As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterY As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterX As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenter As System.Windows.Forms.CheckBox
    Friend WithEvents txtChangeNameCircle As System.Windows.Forms.TextBox
    Friend WithEvents chkChangeNameCircle As System.Windows.Forms.CheckBox
    Friend WithEvents chkDiameter As System.Windows.Forms.CheckBox
    Friend WithEvents gbDiameter As System.Windows.Forms.GroupBox
    Friend WithEvents rbDiameter As System.Windows.Forms.RadioButton
    Friend WithEvents rbRadius As System.Windows.Forms.RadioButton
    Friend WithEvents pbModifying As System.Windows.Forms.ProgressBar
    Friend WithEvents lblThickness As System.Windows.Forms.Label
    Friend WithEvents txtThickness As System.Windows.Forms.TextBox
    Friend WithEvents chkVectorInvert As System.Windows.Forms.CheckBox
    Friend WithEvents tpCircleCompound As System.Windows.Forms.TabPage
    Friend WithEvents gbDiameterCircleCompound As System.Windows.Forms.GroupBox
    Friend WithEvents rbDiameterCircleCompound As System.Windows.Forms.RadioButton
    Friend WithEvents rbRadiusCircleCompound As System.Windows.Forms.RadioButton
    Friend WithEvents gpOutputtoReportCircleCompound As System.Windows.Forms.GroupBox
    Friend WithEvents chkDiameterCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilltingAlgoCircleCpompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkCircularityCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterZCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterYCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterXCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents txtChangeNameCircleCompound As System.Windows.Forms.TextBox
    Friend WithEvents chkChangeNameCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkNominalVectorKCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkNominalVectorJCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkNominalVectorICircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents chkNominalVectorCircleCompound As System.Windows.Forms.CheckBox
    Friend WithEvents btnAbout As System.Windows.Forms.Button
End Class
