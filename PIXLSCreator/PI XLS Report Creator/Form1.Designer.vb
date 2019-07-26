<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tctUserModes = New System.Windows.Forms.TabControl()
        Me.tpgUmUser = New System.Windows.Forms.TabPage()
        Me.pbxPiIcon = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tpgUmAdmin = New System.Windows.Forms.TabPage()
        Me.tcOrientation = New System.Windows.Forms.TabControl()
        Me.tpDataPortrait = New System.Windows.Forms.TabPage()
        Me.cboColAttributeToleranceCol3 = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboColAttributeToleranceCol2 = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtTitlePrefixP = New System.Windows.Forms.TextBox()
        Me.nudRowTitle = New System.Windows.Forms.NumericUpDown()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.nudColDataOffset = New System.Windows.Forms.NumericUpDown()
        Me.label17 = New System.Windows.Forms.Label()
        Me.cboColDataStop = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboColDataStart = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.grpToleranceTypeP = New System.Windows.Forms.GroupBox()
        Me.rdbTolFormatAbsValuesP = New System.Windows.Forms.RadioButton()
        Me.rdbTolFormatPlusMinusP = New System.Windows.Forms.RadioButton()
        Me.cboColAttributeTolerance = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cboColAttributeUnit = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboColAttributeNr = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboColAttributeText = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nudRowStop = New System.Windows.Forms.NumericUpDown()
        Me.nudRowStart = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tpDataLandscape = New System.Windows.Forms.TabPage()
        Me.nudRowDataOffset = New System.Windows.Forms.NumericUpDown()
        Me.nudRowDataStop = New System.Windows.Forms.NumericUpDown()
        Me.nudRowDataStart = New System.Windows.Forms.NumericUpDown()
        Me.nudRowAttributeToleranceRow3 = New System.Windows.Forms.NumericUpDown()
        Me.nudRowAttributeToleranceRow2 = New System.Windows.Forms.NumericUpDown()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTitlePrefixL = New System.Windows.Forms.TextBox()
        Me.nudRowAttributeUnit = New System.Windows.Forms.NumericUpDown()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.nudRowAttributeTolerance = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.grpToleranceTypeL = New System.Windows.Forms.GroupBox()
        Me.rdbTolFormatAbsValuesL = New System.Windows.Forms.RadioButton()
        Me.rdbTolFormatPlusMinusL = New System.Windows.Forms.RadioButton()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cboColTitle = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cboColStop = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.cboColStart = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.nudRowAttributeNr = New System.Windows.Forms.NumericUpDown()
        Me.nudRowAttributeText = New System.Windows.Forms.NumericUpDown()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.grpMerkmal = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkAttribute = New System.Windows.Forms.CheckBox()
        Me.chkGroup = New System.Windows.Forms.CheckBox()
        Me.txtAttributeSeperator = New System.Windows.Forms.TextBox()
        Me.chkAttributeName = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpDefLoadSave = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnDefinitionSave = New System.Windows.Forms.Button()
        Me.btnDefinitionLaden = New System.Windows.Forms.Button()
        Me.grpXlsTarget = New System.Windows.Forms.GroupBox()
        Me.txtXlsTargetWorkSheet = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtXlsTargetPath = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ofdXlsFile = New System.Windows.Forms.OpenFileDialog()
        Me.ofdDefFile = New System.Windows.Forms.OpenFileDialog()
        Me.sfdDefFile = New System.Windows.Forms.SaveFileDialog()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.statusBar = New System.Windows.Forms.StatusStrip()
        Me.pgbProcess = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnDefinitionRun = New System.Windows.Forms.Button()
        Me.ofdDfdFile = New System.Windows.Forms.OpenFileDialog()
        Me.tctUserModes.SuspendLayout
        Me.tpgUmUser.SuspendLayout
        CType(Me.pbxPiIcon,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpgUmAdmin.SuspendLayout
        Me.tcOrientation.SuspendLayout
        Me.tpDataPortrait.SuspendLayout
        CType(Me.nudRowTitle,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudColDataOffset,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpToleranceTypeP.SuspendLayout
        CType(Me.nudRowStop,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowStart,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpDataLandscape.SuspendLayout
        CType(Me.nudRowDataOffset,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowDataStop,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowDataStart,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowAttributeToleranceRow3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowAttributeToleranceRow2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowAttributeUnit,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowAttributeTolerance,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpToleranceTypeL.SuspendLayout
        CType(Me.nudRowAttributeNr,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRowAttributeText,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpMerkmal.SuspendLayout
        Me.grpDefLoadSave.SuspendLayout
        Me.grpXlsTarget.SuspendLayout
        Me.pnlBottom.SuspendLayout
        Me.statusBar.SuspendLayout
        Me.SuspendLayout
        '
        'tctUserModes
        '
        Me.tctUserModes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tctUserModes.Controls.Add(Me.tpgUmUser)
        Me.tctUserModes.Controls.Add(Me.tpgUmAdmin)
        Me.tctUserModes.Location = New System.Drawing.Point(0, 0)
        Me.tctUserModes.Margin = New System.Windows.Forms.Padding(2)
        Me.tctUserModes.Name = "tctUserModes"
        Me.tctUserModes.SelectedIndex = 0
        Me.tctUserModes.Size = New System.Drawing.Size(665, 664)
        Me.tctUserModes.TabIndex = 0
        '
        'tpgUmUser
        '
        Me.tpgUmUser.BackColor = System.Drawing.Color.Transparent
        Me.tpgUmUser.Controls.Add(Me.pbxPiIcon)
        Me.tpgUmUser.Controls.Add(Me.Label1)
        Me.tpgUmUser.Location = New System.Drawing.Point(4, 22)
        Me.tpgUmUser.Margin = New System.Windows.Forms.Padding(2)
        Me.tpgUmUser.Name = "tpgUmUser"
        Me.tpgUmUser.Padding = New System.Windows.Forms.Padding(2)
        Me.tpgUmUser.Size = New System.Drawing.Size(657, 638)
        Me.tpgUmUser.TabIndex = 0
        Me.tpgUmUser.Text = "Erstellen Bericht"
        '
        'pbxPiIcon
        '
        Me.pbxPiIcon.Image = CType(resources.GetObject("pbxPiIcon.Image"),System.Drawing.Image)
        Me.pbxPiIcon.Location = New System.Drawing.Point(32, 32)
        Me.pbxPiIcon.Margin = New System.Windows.Forms.Padding(2)
        Me.pbxPiIcon.Name = "pbxPiIcon"
        Me.pbxPiIcon.Size = New System.Drawing.Size(48, 48)
        Me.pbxPiIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbxPiIcon.TabIndex = 2
        Me.pbxPiIcon.TabStop = false
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(88, 39)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(483, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Generiere PowerInspect Report für Microsoft Excel"
        '
        'tpgUmAdmin
        '
        Me.tpgUmAdmin.BackColor = System.Drawing.SystemColors.Control
        Me.tpgUmAdmin.Controls.Add(Me.tcOrientation)
        Me.tpgUmAdmin.Controls.Add(Me.grpMerkmal)
        Me.tpgUmAdmin.Controls.Add(Me.grpDefLoadSave)
        Me.tpgUmAdmin.Controls.Add(Me.grpXlsTarget)
        Me.tpgUmAdmin.Location = New System.Drawing.Point(4, 22)
        Me.tpgUmAdmin.Margin = New System.Windows.Forms.Padding(2)
        Me.tpgUmAdmin.Name = "tpgUmAdmin"
        Me.tpgUmAdmin.Padding = New System.Windows.Forms.Padding(2)
        Me.tpgUmAdmin.Size = New System.Drawing.Size(657, 638)
        Me.tpgUmAdmin.TabIndex = 1
        Me.tpgUmAdmin.Text = "Definition Breichtslayout"
        '
        'tcOrientation
        '
        Me.tcOrientation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tcOrientation.Controls.Add(Me.tpDataPortrait)
        Me.tcOrientation.Controls.Add(Me.tpDataLandscape)
        Me.tcOrientation.Location = New System.Drawing.Point(15, 202)
        Me.tcOrientation.Name = "tcOrientation"
        Me.tcOrientation.SelectedIndex = 0
        Me.tcOrientation.Size = New System.Drawing.Size(634, 360)
        Me.tcOrientation.TabIndex = 9
        '
        'tpDataPortrait
        '
        Me.tpDataPortrait.BackColor = System.Drawing.SystemColors.Control
        Me.tpDataPortrait.Controls.Add(Me.cboColAttributeToleranceCol3)
        Me.tpDataPortrait.Controls.Add(Me.Label21)
        Me.tpDataPortrait.Controls.Add(Me.cboColAttributeToleranceCol2)
        Me.tpDataPortrait.Controls.Add(Me.Label20)
        Me.tpDataPortrait.Controls.Add(Me.Label19)
        Me.tpDataPortrait.Controls.Add(Me.txtTitlePrefixP)
        Me.tpDataPortrait.Controls.Add(Me.nudRowTitle)
        Me.tpDataPortrait.Controls.Add(Me.Label18)
        Me.tpDataPortrait.Controls.Add(Me.nudColDataOffset)
        Me.tpDataPortrait.Controls.Add(Me.label17)
        Me.tpDataPortrait.Controls.Add(Me.cboColDataStop)
        Me.tpDataPortrait.Controls.Add(Me.Label15)
        Me.tpDataPortrait.Controls.Add(Me.cboColDataStart)
        Me.tpDataPortrait.Controls.Add(Me.Label16)
        Me.tpDataPortrait.Controls.Add(Me.grpToleranceTypeP)
        Me.tpDataPortrait.Controls.Add(Me.cboColAttributeTolerance)
        Me.tpDataPortrait.Controls.Add(Me.Label14)
        Me.tpDataPortrait.Controls.Add(Me.cboColAttributeUnit)
        Me.tpDataPortrait.Controls.Add(Me.Label13)
        Me.tpDataPortrait.Controls.Add(Me.cboColAttributeNr)
        Me.tpDataPortrait.Controls.Add(Me.Label7)
        Me.tpDataPortrait.Controls.Add(Me.cboColAttributeText)
        Me.tpDataPortrait.Controls.Add(Me.Label6)
        Me.tpDataPortrait.Controls.Add(Me.nudRowStop)
        Me.tpDataPortrait.Controls.Add(Me.nudRowStart)
        Me.tpDataPortrait.Controls.Add(Me.Label5)
        Me.tpDataPortrait.Controls.Add(Me.Label4)
        Me.tpDataPortrait.Location = New System.Drawing.Point(4, 22)
        Me.tpDataPortrait.Name = "tpDataPortrait"
        Me.tpDataPortrait.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDataPortrait.Size = New System.Drawing.Size(626, 334)
        Me.tpDataPortrait.TabIndex = 0
        Me.tpDataPortrait.Text = "Definition Daten Hochformat"
        '
        'cboColAttributeToleranceCol3
        '
        Me.cboColAttributeToleranceCol3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColAttributeToleranceCol3.FormattingEnabled = true
        Me.cboColAttributeToleranceCol3.Location = New System.Drawing.Point(293, 207)
        Me.cboColAttributeToleranceCol3.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColAttributeToleranceCol3.Name = "cboColAttributeToleranceCol3"
        Me.cboColAttributeToleranceCol3.Size = New System.Drawing.Size(63, 21)
        Me.cboColAttributeToleranceCol3.TabIndex = 14
        Me.cboColAttributeToleranceCol3.Tag = "Zielspalte obere Merkmaltoleranz (dreispaltig)"
        Me.ToolTip1.SetToolTip(Me.cboColAttributeToleranceCol3, "Zielspalte obere Merkmaltoleranz, wenn Toleranz dreispaltig dargestellt werden so"& _ 
        "ll, dann hier dritte Spalte obere Merkmaltoleranz angeben")
        '
        'Label21
        '
        Me.Label21.AutoSize = true
        Me.Label21.Location = New System.Drawing.Point(18, 209)
        Me.Label21.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(257, 13)
        Me.Label21.TabIndex = 36
        Me.Label21.Text = "Zielspalte in Bericht für obere Merkmaltol. (dreispaltig)"
        '
        'cboColAttributeToleranceCol2
        '
        Me.cboColAttributeToleranceCol2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColAttributeToleranceCol2.FormattingEnabled = true
        Me.cboColAttributeToleranceCol2.Location = New System.Drawing.Point(293, 184)
        Me.cboColAttributeToleranceCol2.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColAttributeToleranceCol2.Name = "cboColAttributeToleranceCol2"
        Me.cboColAttributeToleranceCol2.Size = New System.Drawing.Size(63, 21)
        Me.cboColAttributeToleranceCol2.TabIndex = 13
        Me.cboColAttributeToleranceCol2.Tag = "Zielspalte Merkmalsollwert (dreispaltig)"
        Me.ToolTip1.SetToolTip(Me.cboColAttributeToleranceCol2, "Zielspalte Merkmalsollwert, wenn Toleranz dreispaltig dargestellt werden soll, da"& _ 
        "nn hier zweite Spalte für Sollwert angeben")
        '
        'Label20
        '
        Me.Label20.AutoSize = true
        Me.Label20.Location = New System.Drawing.Point(18, 186)
        Me.Label20.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(248, 13)
        Me.Label20.TabIndex = 34
        Me.Label20.Text = "Zielspalte in Bericht für Merkmalsollwert (dreispaltig)"
        '
        'Label19
        '
        Me.Label19.AutoSize = true
        Me.Label19.Location = New System.Drawing.Point(377, 61)
        Me.Label19.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(102, 13)
        Me.Label19.TabIndex = 32
        Me.Label19.Text = "Prefix für Überschrift"
        '
        'txtTitlePrefixP
        '
        Me.txtTitlePrefixP.Location = New System.Drawing.Point(485, 60)
        Me.txtTitlePrefixP.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTitlePrefixP.Name = "txtTitlePrefixP"
        Me.txtTitlePrefixP.Size = New System.Drawing.Size(63, 20)
        Me.txtTitlePrefixP.TabIndex = 7
        '
        'nudRowTitle
        '
        Me.nudRowTitle.Location = New System.Drawing.Point(293, 60)
        Me.nudRowTitle.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowTitle.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudRowTitle.Name = "nudRowTitle"
        Me.nudRowTitle.Size = New System.Drawing.Size(62, 20)
        Me.nudRowTitle.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.nudRowTitle, "Überschrift darf nicht im Datenbereich liegen. Ziffer 0 bedeutet keine Überschrif"& _ 
        "t")
        Me.nudRowTitle.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.Location = New System.Drawing.Point(18, 61)
        Me.Label18.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(219, 13)
        Me.Label18.TabIndex = 29
        Me.Label18.Text = "Zielzeile in Bericht für Daten-/Teileüberschrift"
        Me.ToolTip1.SetToolTip(Me.Label18, "Zielzeile darf nicht im Bereich zwischen Start- und Stoppzeile der Daten liegen")
        '
        'nudColDataOffset
        '
        Me.nudColDataOffset.Location = New System.Drawing.Point(293, 276)
        Me.nudColDataOffset.Margin = New System.Windows.Forms.Padding(2)
        Me.nudColDataOffset.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nudColDataOffset.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudColDataOffset.Name = "nudColDataOffset"
        Me.nudColDataOffset.Size = New System.Drawing.Size(62, 20)
        Me.nudColDataOffset.TabIndex = 17
        Me.nudColDataOffset.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'label17
        '
        Me.label17.AutoSize = true
        Me.label17.Location = New System.Drawing.Point(18, 277)
        Me.label17.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(148, 13)
        Me.label17.TabIndex = 27
        Me.label17.Text = "Spaltenoffset zwischen Daten"
        '
        'cboColDataStop
        '
        Me.cboColDataStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColDataStop.FormattingEnabled = true
        Me.cboColDataStop.Location = New System.Drawing.Point(293, 253)
        Me.cboColDataStop.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColDataStop.Name = "cboColDataStop"
        Me.cboColDataStop.Size = New System.Drawing.Size(63, 21)
        Me.cboColDataStop.TabIndex = 16
        Me.cboColDataStop.Tag = "Stoppspalte Daten"
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.Location = New System.Drawing.Point(18, 255)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(157, 13)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Stoppspalte in Bericht für Daten"
        '
        'cboColDataStart
        '
        Me.cboColDataStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColDataStart.FormattingEnabled = true
        Me.cboColDataStart.Location = New System.Drawing.Point(293, 230)
        Me.cboColDataStart.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColDataStart.Name = "cboColDataStart"
        Me.cboColDataStart.Size = New System.Drawing.Size(63, 21)
        Me.cboColDataStart.TabIndex = 15
        Me.cboColDataStart.Tag = "Startspalte Daten"
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.Location = New System.Drawing.Point(18, 233)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(151, 13)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "Startspalte in Bericht für Daten"
        '
        'grpToleranceTypeP
        '
        Me.grpToleranceTypeP.Controls.Add(Me.rdbTolFormatAbsValuesP)
        Me.grpToleranceTypeP.Controls.Add(Me.rdbTolFormatPlusMinusP)
        Me.grpToleranceTypeP.Location = New System.Drawing.Point(360, 136)
        Me.grpToleranceTypeP.Margin = New System.Windows.Forms.Padding(2)
        Me.grpToleranceTypeP.Name = "grpToleranceTypeP"
        Me.grpToleranceTypeP.Padding = New System.Windows.Forms.Padding(2)
        Me.grpToleranceTypeP.Size = New System.Drawing.Size(254, 39)
        Me.grpToleranceTypeP.TabIndex = 12
        Me.grpToleranceTypeP.TabStop = false
        Me.grpToleranceTypeP.Text = "DarstellungsformatToleranz (einspaltig)"
        '
        'rdbTolFormatAbsValuesP
        '
        Me.rdbTolFormatAbsValuesP.AutoSize = true
        Me.rdbTolFormatAbsValuesP.Location = New System.Drawing.Point(125, 15)
        Me.rdbTolFormatAbsValuesP.Margin = New System.Windows.Forms.Padding(2)
        Me.rdbTolFormatAbsValuesP.Name = "rdbTolFormatAbsValuesP"
        Me.rdbTolFormatAbsValuesP.Size = New System.Drawing.Size(97, 17)
        Me.rdbTolFormatAbsValuesP.TabIndex = 1
        Me.rdbTolFormatAbsValuesP.Text = "absolute Werte"
        Me.rdbTolFormatAbsValuesP.UseVisualStyleBackColor = true
        '
        'rdbTolFormatPlusMinusP
        '
        Me.rdbTolFormatPlusMinusP.AutoSize = true
        Me.rdbTolFormatPlusMinusP.Checked = true
        Me.rdbTolFormatPlusMinusP.Location = New System.Drawing.Point(12, 15)
        Me.rdbTolFormatPlusMinusP.Margin = New System.Windows.Forms.Padding(2)
        Me.rdbTolFormatPlusMinusP.Name = "rdbTolFormatPlusMinusP"
        Me.rdbTolFormatPlusMinusP.Size = New System.Drawing.Size(95, 17)
        Me.rdbTolFormatPlusMinusP.TabIndex = 0
        Me.rdbTolFormatPlusMinusP.TabStop = true
        Me.rdbTolFormatPlusMinusP.Text = "+/- Darstellung"
        Me.rdbTolFormatPlusMinusP.UseVisualStyleBackColor = true
        '
        'cboColAttributeTolerance
        '
        Me.cboColAttributeTolerance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColAttributeTolerance.FormattingEnabled = true
        Me.cboColAttributeTolerance.Location = New System.Drawing.Point(293, 149)
        Me.cboColAttributeTolerance.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColAttributeTolerance.Name = "cboColAttributeTolerance"
        Me.cboColAttributeTolerance.Size = New System.Drawing.Size(63, 21)
        Me.cboColAttributeTolerance.TabIndex = 11
        Me.cboColAttributeTolerance.Tag = "Zielspalte Merkmaltoleranz (einspaltig)"
        Me.ToolTip1.SetToolTip(Me.cboColAttributeTolerance, "Einspaltig: "&Global.Microsoft.VisualBasic.ChrW(9)&"Darstellung Sollwert & Toleranzen in einer Spalte.    "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Dreispaltig:"& _ 
        " "&Global.Microsoft.VisualBasic.ChrW(9)&"Darstellung Sollwert & Toleranzen in je einer Spalte."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"Hier wird die Zielspa"& _ 
        "lte der unteren Toleranz definiert.")
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(18, 152)
        Me.Label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(282, 34)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Zielspalte in Bericht für Merkmalsollwert+Toleranz (einsp.) oder Merkmaltoleranz "& _ 
    "(dreispaltig)"
        '
        'cboColAttributeUnit
        '
        Me.cboColAttributeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColAttributeUnit.FormattingEnabled = true
        Me.cboColAttributeUnit.Location = New System.Drawing.Point(293, 126)
        Me.cboColAttributeUnit.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColAttributeUnit.Name = "cboColAttributeUnit"
        Me.cboColAttributeUnit.Size = New System.Drawing.Size(63, 21)
        Me.cboColAttributeUnit.TabIndex = 10
        Me.cboColAttributeUnit.Tag = "Zielspalte Merkmaleinheit"
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.Location = New System.Drawing.Point(18, 129)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(188, 13)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Zielspalte in Bericht für Merkmaleinheit"
        '
        'cboColAttributeNr
        '
        Me.cboColAttributeNr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColAttributeNr.FormattingEnabled = true
        Me.cboColAttributeNr.Location = New System.Drawing.Point(293, 103)
        Me.cboColAttributeNr.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColAttributeNr.Name = "cboColAttributeNr"
        Me.cboColAttributeNr.Size = New System.Drawing.Size(63, 21)
        Me.cboColAttributeNr.TabIndex = 9
        Me.cboColAttributeNr.Tag = "Zielspalte Merkmalnummerierung"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(18, 105)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(223, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Zielspalte in Bericht für Merkmalnummerierung"
        '
        'cboColAttributeText
        '
        Me.cboColAttributeText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColAttributeText.FormattingEnabled = true
        Me.cboColAttributeText.Location = New System.Drawing.Point(293, 80)
        Me.cboColAttributeText.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColAttributeText.Name = "cboColAttributeText"
        Me.cboColAttributeText.Size = New System.Drawing.Size(63, 21)
        Me.cboColAttributeText.TabIndex = 8
        Me.cboColAttributeText.Tag = "Zielspalte Merkmalbezeichnung"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(18, 83)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(218, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Zielspalte in Bericht für Merkmalbezeichnung"
        '
        'nudRowStop
        '
        Me.nudRowStop.Location = New System.Drawing.Point(293, 39)
        Me.nudRowStop.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowStop.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudRowStop.Name = "nudRowStop"
        Me.nudRowStop.Size = New System.Drawing.Size(62, 20)
        Me.nudRowStop.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.nudRowStop, "Wert 0 bedeuted es wird kein neues Blatt nach n Zeilen begonnen.")
        Me.nudRowStop.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudRowStart
        '
        Me.nudRowStart.Location = New System.Drawing.Point(293, 19)
        Me.nudRowStart.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowStart.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudRowStart.Name = "nudRowStart"
        Me.nudRowStart.Size = New System.Drawing.Size(62, 20)
        Me.nudRowStart.TabIndex = 4
        Me.nudRowStart.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(18, 41)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(220, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Stoppzeile in Bericht für Merkmale und Daten"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(18, 21)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(214, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Startzeile in Bericht für Merkmale und Daten"
        '
        'tpDataLandscape
        '
        Me.tpDataLandscape.BackColor = System.Drawing.SystemColors.Control
        Me.tpDataLandscape.Controls.Add(Me.nudRowDataOffset)
        Me.tpDataLandscape.Controls.Add(Me.nudRowDataStop)
        Me.tpDataLandscape.Controls.Add(Me.nudRowDataStart)
        Me.tpDataLandscape.Controls.Add(Me.nudRowAttributeToleranceRow3)
        Me.tpDataLandscape.Controls.Add(Me.nudRowAttributeToleranceRow2)
        Me.tpDataLandscape.Controls.Add(Me.Label22)
        Me.tpDataLandscape.Controls.Add(Me.Label23)
        Me.tpDataLandscape.Controls.Add(Me.Label24)
        Me.tpDataLandscape.Controls.Add(Me.txtTitlePrefixL)
        Me.tpDataLandscape.Controls.Add(Me.nudRowAttributeUnit)
        Me.tpDataLandscape.Controls.Add(Me.Label25)
        Me.tpDataLandscape.Controls.Add(Me.nudRowAttributeTolerance)
        Me.tpDataLandscape.Controls.Add(Me.Label26)
        Me.tpDataLandscape.Controls.Add(Me.Label27)
        Me.tpDataLandscape.Controls.Add(Me.Label28)
        Me.tpDataLandscape.Controls.Add(Me.grpToleranceTypeL)
        Me.tpDataLandscape.Controls.Add(Me.Label29)
        Me.tpDataLandscape.Controls.Add(Me.cboColTitle)
        Me.tpDataLandscape.Controls.Add(Me.Label30)
        Me.tpDataLandscape.Controls.Add(Me.cboColStop)
        Me.tpDataLandscape.Controls.Add(Me.Label31)
        Me.tpDataLandscape.Controls.Add(Me.cboColStart)
        Me.tpDataLandscape.Controls.Add(Me.Label32)
        Me.tpDataLandscape.Controls.Add(Me.nudRowAttributeNr)
        Me.tpDataLandscape.Controls.Add(Me.nudRowAttributeText)
        Me.tpDataLandscape.Controls.Add(Me.Label33)
        Me.tpDataLandscape.Controls.Add(Me.Label34)
        Me.tpDataLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpDataLandscape.Name = "tpDataLandscape"
        Me.tpDataLandscape.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDataLandscape.Size = New System.Drawing.Size(626, 334)
        Me.tpDataLandscape.TabIndex = 1
        Me.tpDataLandscape.Text = "Definition Daten Querformat"
        '
        'nudRowDataOffset
        '
        Me.nudRowDataOffset.Location = New System.Drawing.Point(293, 275)
        Me.nudRowDataOffset.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowDataOffset.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowDataOffset.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudRowDataOffset.Name = "nudRowDataOffset"
        Me.nudRowDataOffset.Size = New System.Drawing.Size(62, 20)
        Me.nudRowDataOffset.TabIndex = 68
        Me.nudRowDataOffset.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudRowDataStop
        '
        Me.nudRowDataStop.Location = New System.Drawing.Point(293, 253)
        Me.nudRowDataStop.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowDataStop.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowDataStop.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudRowDataStop.Name = "nudRowDataStop"
        Me.nudRowDataStop.Size = New System.Drawing.Size(62, 20)
        Me.nudRowDataStop.TabIndex = 67
        Me.nudRowDataStop.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudRowDataStart
        '
        Me.nudRowDataStart.Location = New System.Drawing.Point(293, 231)
        Me.nudRowDataStart.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowDataStart.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowDataStart.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudRowDataStart.Name = "nudRowDataStart"
        Me.nudRowDataStart.Size = New System.Drawing.Size(62, 20)
        Me.nudRowDataStart.TabIndex = 66
        Me.nudRowDataStart.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudRowAttributeToleranceRow3
        '
        Me.nudRowAttributeToleranceRow3.Location = New System.Drawing.Point(293, 207)
        Me.nudRowAttributeToleranceRow3.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowAttributeToleranceRow3.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowAttributeToleranceRow3.Name = "nudRowAttributeToleranceRow3"
        Me.nudRowAttributeToleranceRow3.Size = New System.Drawing.Size(62, 20)
        Me.nudRowAttributeToleranceRow3.TabIndex = 65
        Me.nudRowAttributeToleranceRow3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudRowAttributeToleranceRow2
        '
        Me.nudRowAttributeToleranceRow2.Location = New System.Drawing.Point(293, 184)
        Me.nudRowAttributeToleranceRow2.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowAttributeToleranceRow2.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowAttributeToleranceRow2.Name = "nudRowAttributeToleranceRow2"
        Me.nudRowAttributeToleranceRow2.Size = New System.Drawing.Size(62, 20)
        Me.nudRowAttributeToleranceRow2.TabIndex = 64
        Me.nudRowAttributeToleranceRow2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label22
        '
        Me.Label22.AutoSize = true
        Me.Label22.Location = New System.Drawing.Point(18, 209)
        Me.Label22.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(243, 13)
        Me.Label22.TabIndex = 63
        Me.Label22.Text = "Zielzeile in Bericht für obere Merkmaltol. (dreizeilig)"
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.Location = New System.Drawing.Point(18, 186)
        Me.Label23.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(234, 13)
        Me.Label23.TabIndex = 62
        Me.Label23.Text = "Zielzeile in Bericht für Merkmalsollwert (dreizeilig)"
        '
        'Label24
        '
        Me.Label24.AutoSize = true
        Me.Label24.Location = New System.Drawing.Point(377, 61)
        Me.Label24.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(102, 13)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Prefix für Überschrift"
        '
        'txtTitlePrefixL
        '
        Me.txtTitlePrefixL.Location = New System.Drawing.Point(485, 60)
        Me.txtTitlePrefixL.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTitlePrefixL.Name = "txtTitlePrefixL"
        Me.txtTitlePrefixL.Size = New System.Drawing.Size(63, 20)
        Me.txtTitlePrefixL.TabIndex = 40
        '
        'nudRowAttributeUnit
        '
        Me.nudRowAttributeUnit.Location = New System.Drawing.Point(293, 127)
        Me.nudRowAttributeUnit.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowAttributeUnit.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowAttributeUnit.Name = "nudRowAttributeUnit"
        Me.nudRowAttributeUnit.Size = New System.Drawing.Size(62, 20)
        Me.nudRowAttributeUnit.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.nudRowAttributeUnit, "Überschrift darf nicht im Datenbereich liegen. Ziffer 0 bedeutet keine Überschrif"& _ 
        "t")
        Me.nudRowAttributeUnit.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label25
        '
        Me.Label25.AutoSize = true
        Me.Label25.Location = New System.Drawing.Point(18, 61)
        Me.Label25.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(226, 13)
        Me.Label25.TabIndex = 60
        Me.Label25.Text = "Zielspalte in Bericht für Daten-/Teileüberschrift"
        Me.ToolTip1.SetToolTip(Me.Label25, "Zielzeile darf nicht im Bereich zwischen Start- und Stoppzeile der Daten liegen")
        '
        'nudRowAttributeTolerance
        '
        Me.nudRowAttributeTolerance.Location = New System.Drawing.Point(293, 150)
        Me.nudRowAttributeTolerance.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowAttributeTolerance.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowAttributeTolerance.Name = "nudRowAttributeTolerance"
        Me.nudRowAttributeTolerance.Size = New System.Drawing.Size(62, 20)
        Me.nudRowAttributeTolerance.TabIndex = 54
        Me.nudRowAttributeTolerance.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.Location = New System.Drawing.Point(18, 277)
        Me.Label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(141, 13)
        Me.Label26.TabIndex = 59
        Me.Label26.Text = "Zeilenoffset zwischen Daten"
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.Location = New System.Drawing.Point(18, 255)
        Me.Label27.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(150, 13)
        Me.Label27.TabIndex = 58
        Me.Label27.Text = "Stoppzeile in Bericht für Daten"
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.Location = New System.Drawing.Point(18, 233)
        Me.Label28.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(144, 13)
        Me.Label28.TabIndex = 57
        Me.Label28.Text = "Startzeile in Bericht für Daten"
        '
        'grpToleranceTypeL
        '
        Me.grpToleranceTypeL.Controls.Add(Me.rdbTolFormatAbsValuesL)
        Me.grpToleranceTypeL.Controls.Add(Me.rdbTolFormatPlusMinusL)
        Me.grpToleranceTypeL.Location = New System.Drawing.Point(360, 136)
        Me.grpToleranceTypeL.Margin = New System.Windows.Forms.Padding(2)
        Me.grpToleranceTypeL.Name = "grpToleranceTypeL"
        Me.grpToleranceTypeL.Padding = New System.Windows.Forms.Padding(2)
        Me.grpToleranceTypeL.Size = New System.Drawing.Size(254, 39)
        Me.grpToleranceTypeL.TabIndex = 47
        Me.grpToleranceTypeL.TabStop = false
        Me.grpToleranceTypeL.Text = "DarstellungsformatToleranz (einzeilig)"
        '
        'rdbTolFormatAbsValuesL
        '
        Me.rdbTolFormatAbsValuesL.AutoSize = true
        Me.rdbTolFormatAbsValuesL.Location = New System.Drawing.Point(125, 15)
        Me.rdbTolFormatAbsValuesL.Margin = New System.Windows.Forms.Padding(2)
        Me.rdbTolFormatAbsValuesL.Name = "rdbTolFormatAbsValuesL"
        Me.rdbTolFormatAbsValuesL.Size = New System.Drawing.Size(97, 17)
        Me.rdbTolFormatAbsValuesL.TabIndex = 1
        Me.rdbTolFormatAbsValuesL.Text = "absolute Werte"
        Me.rdbTolFormatAbsValuesL.UseVisualStyleBackColor = true
        '
        'rdbTolFormatPlusMinusL
        '
        Me.rdbTolFormatPlusMinusL.AutoSize = true
        Me.rdbTolFormatPlusMinusL.Checked = true
        Me.rdbTolFormatPlusMinusL.Location = New System.Drawing.Point(12, 15)
        Me.rdbTolFormatPlusMinusL.Margin = New System.Windows.Forms.Padding(2)
        Me.rdbTolFormatPlusMinusL.Name = "rdbTolFormatPlusMinusL"
        Me.rdbTolFormatPlusMinusL.Size = New System.Drawing.Size(95, 17)
        Me.rdbTolFormatPlusMinusL.TabIndex = 0
        Me.rdbTolFormatPlusMinusL.TabStop = true
        Me.rdbTolFormatPlusMinusL.Text = "+/- Darstellung"
        Me.rdbTolFormatPlusMinusL.UseVisualStyleBackColor = true
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(18, 152)
        Me.Label29.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(281, 37)
        Me.Label29.TabIndex = 56
        Me.Label29.Text = "Zielzeile in Bericht für Merkmalsollwert+Toleranz (einzeilig) oder Merkmaltoleran"& _ 
    "z (dreizeilig)"
        '
        'cboColTitle
        '
        Me.cboColTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColTitle.FormattingEnabled = true
        Me.cboColTitle.Location = New System.Drawing.Point(293, 59)
        Me.cboColTitle.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColTitle.Name = "cboColTitle"
        Me.cboColTitle.Size = New System.Drawing.Size(63, 21)
        Me.cboColTitle.TabIndex = 44
        Me.cboColTitle.Tag = "Zielspalte Merkmaleinheit"
        '
        'Label30
        '
        Me.Label30.AutoSize = true
        Me.Label30.Location = New System.Drawing.Point(18, 129)
        Me.Label30.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(181, 13)
        Me.Label30.TabIndex = 55
        Me.Label30.Text = "Zielzeile in Bericht für Merkmaleinheit"
        '
        'cboColStop
        '
        Me.cboColStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColStop.FormattingEnabled = true
        Me.cboColStop.Location = New System.Drawing.Point(293, 38)
        Me.cboColStop.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColStop.Name = "cboColStop"
        Me.cboColStop.Size = New System.Drawing.Size(63, 21)
        Me.cboColStop.TabIndex = 43
        Me.cboColStop.Tag = "Zielspalte Merkmalnummerierung"
        '
        'Label31
        '
        Me.Label31.AutoSize = true
        Me.Label31.Location = New System.Drawing.Point(18, 105)
        Me.Label31.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(216, 13)
        Me.Label31.TabIndex = 53
        Me.Label31.Text = "Zielzeile in Bericht für Merkmalnummerierung"
        '
        'cboColStart
        '
        Me.cboColStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColStart.FormattingEnabled = true
        Me.cboColStart.Location = New System.Drawing.Point(293, 17)
        Me.cboColStart.Margin = New System.Windows.Forms.Padding(2)
        Me.cboColStart.Name = "cboColStart"
        Me.cboColStart.Size = New System.Drawing.Size(63, 21)
        Me.cboColStart.TabIndex = 41
        Me.cboColStart.Tag = "Zielspalte Merkmalbezeichnung"
        '
        'Label32
        '
        Me.Label32.AutoSize = true
        Me.Label32.Location = New System.Drawing.Point(18, 83)
        Me.Label32.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(211, 13)
        Me.Label32.TabIndex = 49
        Me.Label32.Text = "Zielzeile in Bericht für Merkmalbezeichnung"
        '
        'nudRowAttributeNr
        '
        Me.nudRowAttributeNr.Location = New System.Drawing.Point(293, 103)
        Me.nudRowAttributeNr.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowAttributeNr.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowAttributeNr.Name = "nudRowAttributeNr"
        Me.nudRowAttributeNr.Size = New System.Drawing.Size(62, 20)
        Me.nudRowAttributeNr.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.nudRowAttributeNr, "Wert 0 bedeuted es wird kein neues Blatt nach n Zeilen begonnen.")
        Me.nudRowAttributeNr.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudRowAttributeText
        '
        Me.nudRowAttributeText.Location = New System.Drawing.Point(293, 81)
        Me.nudRowAttributeText.Margin = New System.Windows.Forms.Padding(2)
        Me.nudRowAttributeText.Maximum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.nudRowAttributeText.Name = "nudRowAttributeText"
        Me.nudRowAttributeText.Size = New System.Drawing.Size(62, 20)
        Me.nudRowAttributeText.TabIndex = 37
        Me.nudRowAttributeText.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label33
        '
        Me.Label33.AutoSize = true
        Me.Label33.Location = New System.Drawing.Point(18, 41)
        Me.Label33.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(227, 13)
        Me.Label33.TabIndex = 46
        Me.Label33.Text = "Stoppspalte in Bericht für Merkmale und Daten"
        '
        'Label34
        '
        Me.Label34.AutoSize = true
        Me.Label34.Location = New System.Drawing.Point(18, 21)
        Me.Label34.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(221, 13)
        Me.Label34.TabIndex = 42
        Me.Label34.Text = "Startspalte in Bericht für Merkmale und Daten"
        '
        'grpMerkmal
        '
        Me.grpMerkmal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpMerkmal.Controls.Add(Me.Label3)
        Me.grpMerkmal.Controls.Add(Me.chkAttribute)
        Me.grpMerkmal.Controls.Add(Me.chkGroup)
        Me.grpMerkmal.Controls.Add(Me.txtAttributeSeperator)
        Me.grpMerkmal.Controls.Add(Me.chkAttributeName)
        Me.grpMerkmal.Controls.Add(Me.Label2)
        Me.grpMerkmal.Location = New System.Drawing.Point(15, 92)
        Me.grpMerkmal.Name = "grpMerkmal"
        Me.grpMerkmal.Size = New System.Drawing.Size(629, 104)
        Me.grpMerkmal.TabIndex = 8
        Me.grpMerkmal.TabStop = false
        Me.grpMerkmal.Text = "Definition Merkmal"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(23, 41)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(266, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Zeige folgende Bestandteile der Merkmalsbezeichnung"
        '
        'chkAttribute
        '
        Me.chkAttribute.AutoSize = true
        Me.chkAttribute.Location = New System.Drawing.Point(298, 81)
        Me.chkAttribute.Margin = New System.Windows.Forms.Padding(2)
        Me.chkAttribute.Name = "chkAttribute"
        Me.chkAttribute.Size = New System.Drawing.Size(66, 17)
        Me.chkAttribute.TabIndex = 3
        Me.chkAttribute.Text = "Merkmal"
        Me.chkAttribute.UseVisualStyleBackColor = true
        '
        'chkGroup
        '
        Me.chkGroup.AutoSize = true
        Me.chkGroup.Location = New System.Drawing.Point(298, 37)
        Me.chkGroup.Margin = New System.Windows.Forms.Padding(2)
        Me.chkGroup.Name = "chkGroup"
        Me.chkGroup.Size = New System.Drawing.Size(61, 17)
        Me.chkGroup.TabIndex = 1
        Me.chkGroup.Text = "Gruppe"
        Me.chkGroup.UseVisualStyleBackColor = true
        '
        'txtAttributeSeperator
        '
        Me.txtAttributeSeperator.Location = New System.Drawing.Point(298, 15)
        Me.txtAttributeSeperator.Margin = New System.Windows.Forms.Padding(2)
        Me.txtAttributeSeperator.Name = "txtAttributeSeperator"
        Me.txtAttributeSeperator.Size = New System.Drawing.Size(63, 20)
        Me.txtAttributeSeperator.TabIndex = 0
        '
        'chkAttributeName
        '
        Me.chkAttributeName.AutoSize = true
        Me.chkAttributeName.Location = New System.Drawing.Point(298, 59)
        Me.chkAttributeName.Margin = New System.Windows.Forms.Padding(2)
        Me.chkAttributeName.Name = "chkAttributeName"
        Me.chkAttributeName.Size = New System.Drawing.Size(92, 17)
        Me.chkAttributeName.TabIndex = 2
        Me.chkAttributeName.Text = "Merkmalname"
        Me.chkAttributeName.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(23, 19)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(166, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Trennzeichen Merkmalbestandteil"
        '
        'grpDefLoadSave
        '
        Me.grpDefLoadSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpDefLoadSave.Controls.Add(Me.Label10)
        Me.grpDefLoadSave.Controls.Add(Me.Label9)
        Me.grpDefLoadSave.Controls.Add(Me.btnDefinitionSave)
        Me.grpDefLoadSave.Controls.Add(Me.btnDefinitionLaden)
        Me.grpDefLoadSave.Location = New System.Drawing.Point(15, 567)
        Me.grpDefLoadSave.Margin = New System.Windows.Forms.Padding(2)
        Me.grpDefLoadSave.Name = "grpDefLoadSave"
        Me.grpDefLoadSave.Padding = New System.Windows.Forms.Padding(2)
        Me.grpDefLoadSave.Size = New System.Drawing.Size(629, 69)
        Me.grpDefLoadSave.TabIndex = 7
        Me.grpDefLoadSave.TabStop = false
        Me.grpDefLoadSave.Text = "Definition laden/speichern"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(23, 44)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Definitionen in Datei"
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(23, 20)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(111, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Definitionen aus Datei"
        '
        'btnDefinitionSave
        '
        Me.btnDefinitionSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnDefinitionSave.Location = New System.Drawing.Point(298, 44)
        Me.btnDefinitionSave.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDefinitionSave.Name = "btnDefinitionSave"
        Me.btnDefinitionSave.Size = New System.Drawing.Size(62, 20)
        Me.btnDefinitionSave.TabIndex = 7
        Me.btnDefinitionSave.Text = "speichern"
        Me.btnDefinitionSave.UseVisualStyleBackColor = true
        '
        'btnDefinitionLaden
        '
        Me.btnDefinitionLaden.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnDefinitionLaden.Location = New System.Drawing.Point(298, 20)
        Me.btnDefinitionLaden.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDefinitionLaden.Name = "btnDefinitionLaden"
        Me.btnDefinitionLaden.Size = New System.Drawing.Size(62, 20)
        Me.btnDefinitionLaden.TabIndex = 6
        Me.btnDefinitionLaden.Text = "laden"
        Me.btnDefinitionLaden.UseVisualStyleBackColor = true
        '
        'grpXlsTarget
        '
        Me.grpXlsTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpXlsTarget.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.grpXlsTarget.Controls.Add(Me.txtXlsTargetWorkSheet)
        Me.grpXlsTarget.Controls.Add(Me.Label11)
        Me.grpXlsTarget.Controls.Add(Me.btnBrowse)
        Me.grpXlsTarget.Controls.Add(Me.Label8)
        Me.grpXlsTarget.Controls.Add(Me.txtXlsTargetPath)
        Me.grpXlsTarget.Location = New System.Drawing.Point(15, 11)
        Me.grpXlsTarget.Margin = New System.Windows.Forms.Padding(2)
        Me.grpXlsTarget.Name = "grpXlsTarget"
        Me.grpXlsTarget.Padding = New System.Windows.Forms.Padding(2)
        Me.grpXlsTarget.Size = New System.Drawing.Size(629, 75)
        Me.grpXlsTarget.TabIndex = 4
        Me.grpXlsTarget.TabStop = false
        Me.grpXlsTarget.Text = "Definition Berichtsvorlage"
        '
        'txtXlsTargetWorkSheet
        '
        Me.txtXlsTargetWorkSheet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtXlsTargetWorkSheet.Location = New System.Drawing.Point(198, 46)
        Me.txtXlsTargetWorkSheet.Margin = New System.Windows.Forms.Padding(2)
        Me.txtXlsTargetWorkSheet.Name = "txtXlsTargetWorkSheet"
        Me.txtXlsTargetWorkSheet.Size = New System.Drawing.Size(387, 20)
        Me.txtXlsTargetWorkSheet.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Location = New System.Drawing.Point(23, 46)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(169, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Name des Zielblattes in Zielmappe"
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(598, 19)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(21, 23)
        Me.btnBrowse.TabIndex = 5
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = true
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(23, 24)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Zu verwendende Vorlagedatei"
        '
        'txtXlsTargetPath
        '
        Me.txtXlsTargetPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtXlsTargetPath.Location = New System.Drawing.Point(198, 21)
        Me.txtXlsTargetPath.Margin = New System.Windows.Forms.Padding(2)
        Me.txtXlsTargetPath.Name = "txtXlsTargetPath"
        Me.txtXlsTargetPath.ReadOnly = true
        Me.txtXlsTargetPath.Size = New System.Drawing.Size(387, 20)
        Me.txtXlsTargetPath.TabIndex = 0
        '
        'ofdXlsFile
        '
        Me.ofdXlsFile.DefaultExt = "xls"
        Me.ofdXlsFile.Filter = "Alle Dateien (*.*)|*.*|Excel Dateien (*.xlsx)|*.xlsx"
        Me.ofdXlsFile.FilterIndex = 2
        Me.ofdXlsFile.Title = "Selektiere Berichtsvorlagedatei"
        '
        'ofdDefFile
        '
        Me.ofdDefFile.DefaultExt = "def"
        Me.ofdDefFile.Filter = "Alle Dateien (*.*)|*.*|Reportdefinitionsdateien (*.def)|*.def"
        Me.ofdDefFile.FilterIndex = 2
        Me.ofdDefFile.Title = "Selektiere die zu öffnende Reportdefinitionsdatei"
        '
        'sfdDefFile
        '
        Me.sfdDefFile.DefaultExt = "def"
        Me.sfdDefFile.FileName = "Report.def"
        Me.sfdDefFile.Filter = "Alle Dateien (*.*)|*.*|Reportdefinitionsdateien (*.def)|*.def"
        Me.sfdDefFile.FilterIndex = 2
        Me.sfdDefFile.Title = "Speichere Reportdefinitionsdatei"
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.statusBar)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Controls.Add(Me.Label12)
        Me.pnlBottom.Controls.Add(Me.btnDefinitionRun)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 663)
        Me.pnlBottom.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(665, 57)
        Me.pnlBottom.TabIndex = 1
        '
        'statusBar
        '
        Me.statusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pgbProcess})
        Me.statusBar.Location = New System.Drawing.Point(0, 35)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.statusBar.Size = New System.Drawing.Size(665, 22)
        Me.statusBar.TabIndex = 25
        '
        'pgbProcess
        '
        Me.pgbProcess.Name = "pgbProcess"
        Me.pgbProcess.Size = New System.Drawing.Size(600, 16)
        Me.pgbProcess.Step = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(580, 11)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(56, 20)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = true
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = true
        Me.Label12.Location = New System.Drawing.Point(41, 14)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 13)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Reportdefinition"
        '
        'btnDefinitionRun
        '
        Me.btnDefinitionRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnDefinitionRun.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnDefinitionRun.Location = New System.Drawing.Point(316, 11)
        Me.btnDefinitionRun.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDefinitionRun.Name = "btnDefinitionRun"
        Me.btnDefinitionRun.Size = New System.Drawing.Size(62, 20)
        Me.btnDefinitionRun.TabIndex = 23
        Me.btnDefinitionRun.Text = "ausführen"
        Me.btnDefinitionRun.UseVisualStyleBackColor = true
        '
        'ofdDfdFile
        '
        Me.ofdDfdFile.DefaultExt = "dfd"
        Me.ofdDfdFile.Filter = "Alle Dateien (*.*)|*.*|Datendateien (*.dfd)|*.dfd"
        Me.ofdDfdFile.FilterIndex = 2
        Me.ofdDfdFile.Title = "Selektiere die zu öffnende Datendatei"
        '
        'frmMain
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 720)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.tctUserModes)
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmMain"
        Me.Text = "PowerINSPECT Prüfbericht"
        Me.tctUserModes.ResumeLayout(false)
        Me.tpgUmUser.ResumeLayout(false)
        Me.tpgUmUser.PerformLayout
        CType(Me.pbxPiIcon,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpgUmAdmin.ResumeLayout(false)
        Me.tcOrientation.ResumeLayout(false)
        Me.tpDataPortrait.ResumeLayout(false)
        Me.tpDataPortrait.PerformLayout
        CType(Me.nudRowTitle,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudColDataOffset,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpToleranceTypeP.ResumeLayout(false)
        Me.grpToleranceTypeP.PerformLayout
        CType(Me.nudRowStop,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowStart,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpDataLandscape.ResumeLayout(false)
        Me.tpDataLandscape.PerformLayout
        CType(Me.nudRowDataOffset,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowDataStop,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowDataStart,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowAttributeToleranceRow3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowAttributeToleranceRow2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowAttributeUnit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowAttributeTolerance,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpToleranceTypeL.ResumeLayout(false)
        Me.grpToleranceTypeL.PerformLayout
        CType(Me.nudRowAttributeNr,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRowAttributeText,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpMerkmal.ResumeLayout(false)
        Me.grpMerkmal.PerformLayout
        Me.grpDefLoadSave.ResumeLayout(false)
        Me.grpDefLoadSave.PerformLayout
        Me.grpXlsTarget.ResumeLayout(false)
        Me.grpXlsTarget.PerformLayout
        Me.pnlBottom.ResumeLayout(false)
        Me.pnlBottom.PerformLayout
        Me.statusBar.ResumeLayout(false)
        Me.statusBar.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents tctUserModes As System.Windows.Forms.TabControl
    Friend WithEvents tpgUmUser As System.Windows.Forms.TabPage
    Friend WithEvents tpgUmAdmin As System.Windows.Forms.TabPage
    Friend WithEvents txtAttributeSeperator As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkAttribute As System.Windows.Forms.CheckBox
    Friend WithEvents chkAttributeName As System.Windows.Forms.CheckBox
    Friend WithEvents chkGroup As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nudRowStop As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRowStart As System.Windows.Forms.NumericUpDown
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboColAttributeText As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboColAttributeNr As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grpXlsTarget As System.Windows.Forms.GroupBox
    Friend WithEvents ofdXlsFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtXlsTargetPath As System.Windows.Forms.TextBox
    Friend WithEvents grpDefLoadSave As System.Windows.Forms.GroupBox
    Friend WithEvents btnDefinitionLaden As System.Windows.Forms.Button
    Friend WithEvents btnDefinitionSave As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ofdDefFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sfdDefFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnDefinitionRun As System.Windows.Forms.Button
    Friend WithEvents ofdDfdFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtXlsTargetWorkSheet As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboColAttributeUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboColAttributeTolerance As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents grpToleranceTypeP As System.Windows.Forms.GroupBox
    Friend WithEvents rdbTolFormatAbsValuesP As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTolFormatPlusMinusP As System.Windows.Forms.RadioButton
    Friend WithEvents nudColDataOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents cboColDataStop As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboColDataStart As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTitlePrefixP As System.Windows.Forms.TextBox
    Friend WithEvents nudRowTitle As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents statusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents pgbProcess As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pbxPiIcon As System.Windows.Forms.PictureBox
    Friend WithEvents cboColAttributeToleranceCol2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboColAttributeToleranceCol3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grpMerkmal As System.Windows.Forms.GroupBox
    Friend WithEvents tcOrientation As System.Windows.Forms.TabControl
    Friend WithEvents tpDataPortrait As System.Windows.Forms.TabPage
    Friend WithEvents tpDataLandscape As System.Windows.Forms.TabPage
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtTitlePrefixL As System.Windows.Forms.TextBox
    Friend WithEvents nudRowAttributeUnit As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents nudRowAttributeTolerance As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents grpToleranceTypeL As System.Windows.Forms.GroupBox
    Friend WithEvents rdbTolFormatAbsValuesL As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTolFormatPlusMinusL As System.Windows.Forms.RadioButton
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cboColTitle As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cboColStop As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cboColStart As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents nudRowAttributeNr As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRowAttributeText As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents nudRowDataOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRowDataStop As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRowDataStart As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRowAttributeToleranceRow3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRowAttributeToleranceRow2 As System.Windows.Forms.NumericUpDown

End Class
