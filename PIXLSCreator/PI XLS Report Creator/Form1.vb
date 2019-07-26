' **********************************************************************
' *         © COPYRIGHT 2019 Autodesk, Inc.All Rights Reserved         *
' *                                                                    *
' *  Use of this software is subject to the terms of the Autodesk      *
' *  license agreement provided at the time of installation            *
' *  or download, or which otherwise accompanies this software         *
' *  in either electronic or hard copy form.                           *
' **********************************************************************

Imports System.IO
Imports Microsoft.Office.Interop.Excel

Public Class frmMain
    ''' <summary>
    ''' list of possible pages to be displayed in current application
    ''' </summary>
    Private _tabPages As New List(Of TabPage)()

    ''' <summary>
    ''' object to handle report (read/save report definition)
    ''' </summary>
    Private reportManager As ReportManager

    ''' <summary>
    ''' Text shown as application title
    ''' </summary>
    Private Const CAppTitle As String = "PowerINSPECT Prüfbericht"

    Public Const CHelpFileName As String = "Autodesk PowerINSPECT-XLS Report Creator V1.1.6.pdf"

    ''' <summary>
    ''' Height of the form if mode USER is activ only
    ''' </summary>
    Private Const CFrmHeightModeUser As Integer = 250
    ''' <summary>
    ''' Width of the form if mode USER is activ only
    ''' </summary>
    Private Const CFrmWidthModeUser As Integer = 650
    ''' <summary>
    ''' Height of the form if mode ADMIN is activ
    ''' </summary>
    ''' <remarks>Mode ADMIN has higher priority. 
    ''' If Mode USER is activ too the form will be configured for Mode ADMIN.</remarks>
    Private Const CFrmHeightModeAdmin As Integer = 790
    ''' <summary>
    ''' Width of the form if mode ADMIN is activ only
    ''' </summary>
    ''' <remarks>Mode ADMIN has higher priority. 
    ''' If Mode USER is activ too the form will be configured for Mode ADMIN.</remarks>
    Private Const CFrmWidthModeAdmin As Integer = 895


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitializeForm()
    End Sub

    ''' <summary>
    ''' Initialize controls of loaded form and displays tabpages dependent on command line arguments
    ''' </summary>
    ''' <remarks>Among other things the "Column" comboboxes will be filled with names of possible excel columns.
    ''' Command line argument ADMIN shows page to manage and/or run report definition  
    ''' Command line argument USER shows page to run report definition  
    ''' </remarks>
    Private Sub InitializeForm()

        Dim xlsColumns() As String = New String() {}
        ReDim xlsColumns(255)

        Me.Text = CAppTitle
        pgbProcess.AutoSize = False
        resizeProgressbar()

        ' store displayable tabs in list
        _tabPages.Add(tpgUmAdmin)
        _tabPages.Add(tpgUmUser)

        ' first remove the tabs
        tctUserModes.Controls.Remove(tpgUmAdmin)
        tctUserModes.Controls.Remove(tpgUmUser)

        If My.Application.CommandLineArgs.Count > 0 Then
            ' check mode to be displayed
            For Each argument As String In My.Application.CommandLineArgs
                ' display page dependent on command line argument
                If UCase(argument).Contains("USER") Then
                    tctUserModes.Controls.Add(tpgUmUser)
                    Me.Height = CFrmHeightModeUser
                    Me.Width = CFrmWidthModeUser
                End If
                If UCase(argument).Contains("ADMIN") Then
                    tctUserModes.Controls.Add(tpgUmAdmin)
                    Me.Height = CFrmHeightModeAdmin
                    Me.Width = CFrmWidthModeAdmin
                End If
            Next
        Else
            ' if no argument in command line use mode USER
            tctUserModes.Controls.Add(tpgUmUser)
            Me.Height = CFrmHeightModeUser
            Me.Width = CFrmWidthModeUser
        End If

        BuildListOfXlsColumns(xlsColumns)

        ' setup tab page PORTRAIT
        ' define possible columns to store "Merkmal"
        cboColAttributeText.Items.Clear()
        cboColAttributeText.Items.Add("Keine")
        cboColAttributeText.Items.AddRange(xlsColumns)
        cboColAttributeText.SelectedIndex = 2

        ' define possible columns to store "Merkmal Nr"
        cboColAttributeNr.Items.Clear()
        cboColAttributeNr.Items.Add("Keine")
        cboColAttributeNr.Items.AddRange(xlsColumns)
        cboColAttributeNr.SelectedIndex = 1

        ' define possible columns to store "Merkmal Einheit"
        cboColAttributeUnit.Items.Clear()
        cboColAttributeUnit.Items.Add("Keine")
        cboColAttributeUnit.Items.AddRange(xlsColumns)
        cboColAttributeUnit.SelectedIndex = 14

        ' define possible columns to store "Merkmal Toleranz "
        cboColAttributeTolerance.Items.Clear()
        cboColAttributeTolerance.Items.Add("Keine")
        cboColAttributeTolerance.Items.AddRange(xlsColumns)
        cboColAttributeTolerance.SelectedIndex = 8

        ' define possible columns to store "Merkmal Sollwert (dreispaltig)"
        cboColAttributeToleranceCol2.Items.Clear()
        cboColAttributeToleranceCol2.Items.Add("Keine")
        cboColAttributeToleranceCol2.Items.AddRange(xlsColumns)
        cboColAttributeToleranceCol2.SelectedIndex = 0

        ' define possible columns to store "Merkmal obere Toleranz (dreispaltig)"
        cboColAttributeToleranceCol3.Items.Clear()
        cboColAttributeToleranceCol3.Items.Add("Keine")
        cboColAttributeToleranceCol3.Items.AddRange(xlsColumns)
        cboColAttributeToleranceCol3.SelectedIndex = 0

        ' define start column to store data
        cboColDataStart.Items.Clear()
        cboColDataStart.Items.AddRange(xlsColumns)
        cboColDataStart.SelectedIndex = 15

        ' define stop column to store data
        cboColDataStop.Items.Clear()
        cboColDataStop.Items.AddRange(xlsColumns)
        cboColDataStop.SelectedIndex = 25

        ' other default settings
        nudColDataOffset.Value = 2
        nudRowStart.Value = 35
        nudRowStop.Value = 57
        nudRowTitle.Value = 34

        txtTitlePrefixP.Text = "Teil"
        rdbTolFormatPlusMinusP.Checked = True

        ' setup tab page LANDSCAPE
        ' define row to store "Merkmal"
        nudRowAttributeText.Value = 10

        ' define row to store "Merkmal Nr"
        nudRowAttributeNr.Value = 0

        ' define row to store "Merkmal Einheit"
        nudRowAttributeUnit.Value = 0

        ' define row to store "Merkmal Toleranz "
        nudRowAttributeTolerance.Value = 0

        ' define row to store "Merkmal Sollwert (dreispaltig)"
        nudRowAttributeToleranceRow2.Value = 0

        ' define row to store "Merkmal obere Toleranz (dreispaltig)"
        nudRowAttributeToleranceRow3.Value = 0

        ' define start row to store data
        nudRowDataStart.Value = 12

        ' define stop column to store data
        nudRowDataStop.Value = 36

        ' define row offset of data rows
        nudRowDataOffset.Value = 1

        ' define start column to store attributes and data
        cboColStart.Items.Clear()
        cboColStart.Items.AddRange(xlsColumns)
        cboColStart.SelectedIndex = 1

        ' define stop column to store attributes and data
        cboColStop.Items.Clear()
        cboColStop.Items.AddRange(xlsColumns)
        cboColStop.SelectedIndex = 6

        ' define column to store title
        cboColTitle.Items.Clear()
        cboColTitle.Items.Add("Keine")
        cboColTitle.Items.AddRange(xlsColumns)
        cboColTitle.SelectedIndex = 1

        txtTitlePrefixL.Text = "Teil"
        rdbTolFormatPlusMinusL.Checked = True

        ' define common settings
        txtXlsTargetWorkSheet.Text = "Ergebnis"
        txtAttributeSeperator.Text = "::"
        chkGroup.Checked = False
        chkAttributeName.Checked = True
        chkAttribute.Checked = True

        ' create report manager
        reportManager = New ReportManager()
    End Sub

    ''' <summary>
    ''' Build list of possible column names of excel worksheets
    ''' </summary>
    ''' <param name="xlsColumns">Stringlist of columnnames</param>
    Public Sub BuildListOfXlsColumns(ByRef xlsColumns As String())
        Dim strCol As String

        ' Build list of names of excel columns ( 256 columns: "A" .. "IV") 
        Dim arrayIndex As Integer = 0
        For index2 As Integer = 65 To 90
            ' loop from a to z
            strCol = Chr(index2)
            xlsColumns(arrayIndex) = strCol
            arrayIndex = arrayIndex + 1
        Next
        For index1 As Integer = 65 To 73
            ' loop from a to i
            For index2 = 65 To 90
                ' loop from a to z
                strCol = Chr(index1) + Chr(index2)
                xlsColumns(arrayIndex) = strCol
                If arrayIndex >= xlsColumns.Length - 1 Then
                    Exit For
                End If
                arrayIndex = arrayIndex + 1
            Next
        Next
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        BrowseToXlsTemplate()
    End Sub

    ''' <summary>
    ''' Show dialog to define excel report template (used to generate report)
    ''' </summary>
    Private Sub BrowseToXlsTemplate()
        If ofdXlsFile.ShowDialog = DialogResult.OK Then
            txtXlsTargetPath.Text = ofdXlsFile.FileName
        End If
    End Sub

    ''' <summary>
    ''' Reads content of given file and store the data in one byte array
    ''' </summary>
    ''' <param name="filename">File whose data will be read to byte array</param>
    ''' <returns>Read data as byte array</returns>
    ''' <remarks>Reading filecontent into a byte array is neccessary to serialize it later 
    ''' (store read and additional data in a xml file.
    ''' </remarks>
    Private Function ObjectToByteArray(ByVal filename As String) As Byte()
        If IO.File.Exists(filename) Then
            ' load defined xls workbook into buffer (byte array)
            Dim f = New System.IO.FileStream(filename, IO.FileMode.Open, IO.FileAccess.Read)
            Try
                ' read workbook to byte array
                Dim mybuffer(CInt(f.Length - 1)) As Byte
                f.Read(mybuffer, 0, mybuffer.Length)
                Return mybuffer
            Catch e As Exception
                ' Error 
                MessageBox.Show("Reason: " & e.Message, "Failed to read excel report template.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                f.Close()
            End Try
        End If
        ' Error occured, return null 
        Return Nothing
    End Function

    ''' <summary>
    ''' Update form controls with current data stored in definition data object (_reportDefData)
    ''' </summary>
    Private Sub updateForm()

        ' refresh form with current data
        ' common settings
        txtXlsTargetPath.Text = reportManager.ReportData.XlsTargetPath
        txtXlsTargetWorkSheet.Text = reportManager.ReportData.XlsTargetWorkSheet
        txtAttributeSeperator.Text = reportManager.ReportData.AttributeSperator
        chkGroup.Checked = reportManager.ReportData.AttributePartGroup
        chkAttributeName.Checked = reportManager.ReportData.AttributePartAttributeName
        chkAttribute.Checked = reportManager.ReportData.AttributePartAttribute
        If reportManager.ReportData.Orientation = ReportDefData.eOrientation.portrait Then
            tcOrientation.SelectTab(tpDataPortrait)
        Else
            tcOrientation.SelectTab(tpDataLandscape)
        End If
        ' PORTRAIT
        nudRowStart.Value = reportManager.ReportData.RowStart
        nudRowStop.Value = reportManager.ReportData.RowStop
        nudRowTitle.Value = reportManager.ReportData.RowTitle
        txtTitlePrefixP.Text = reportManager.ReportData.TitlePrefixP
        cboColAttributeText.Text = reportManager.ReportData.ColAttributeText
        cboColAttributeNr.Text = reportManager.ReportData.ColAttributeNr
        cboColAttributeUnit.Text = reportManager.ReportData.ColAttributeUnit
        cboColAttributeTolerance.Text = reportManager.ReportData.ColAttributeTolerance
        cboColAttributeToleranceCol2.Text = reportManager.ReportData.ColAttributeToleranceCol2
        cboColAttributeToleranceCol3.Text = reportManager.ReportData.ColAttributeToleranceCol3
        If reportManager.ReportData.AttributeToleranceFormatP = ReportDefData.eToleranceDisplayFormat.plusMinus Then
            rdbTolFormatPlusMinusP.Checked = True
        Else
            rdbTolFormatAbsValuesP.Checked = True
        End If
        cboColDataStart.Text = reportManager.ReportData.ColDataStart
        cboColDataStop.Text = reportManager.ReportData.ColDataStop
        nudColDataOffset.Value = reportManager.ReportData.ColDataOffset
        ' LANDSCAPE
        cboColStart.Text = reportManager.ReportData.ColStart
        cboColStop.Text = reportManager.ReportData.ColStop
        cboColTitle.Text = reportManager.ReportData.ColTitle
        txtTitlePrefixL.Text = reportManager.ReportData.TitlePrefixL
        nudRowAttributeText.Value = reportManager.ReportData.RowAttributeText
        nudRowAttributeNr.Value = reportManager.ReportData.RowAttributeNr
        nudRowAttributeUnit.Value = reportManager.ReportData.RowAttributeUnit
        nudRowAttributeTolerance.Value = reportManager.ReportData.RowAttributeTolerance
        nudRowAttributeToleranceRow2.Value = reportManager.ReportData.RowAttributeToleranceRow2
        nudRowAttributeToleranceRow3.Value = reportManager.ReportData.RowAttributeToleranceRow3
        If reportManager.ReportData.AttributeToleranceFormatL = ReportDefData.eToleranceDisplayFormat.plusMinus Then
            rdbTolFormatPlusMinusL.Checked = True
        Else
            rdbTolFormatAbsValuesL.Checked = True
        End If
        nudRowDataStart.Text = reportManager.ReportData.RowDataStart
        nudRowDataStop.Text = reportManager.ReportData.RowDataStop
        nudRowDataOffset.Value = reportManager.ReportData.RowDataOffset
    End Sub

    ''' <summary>
    ''' Copy data of form controls to definition data object (_reportDefData)
    ''' </summary>
    ''' <remarks>Checks if data entered by user are plausible</remarks>
    Private Function saveForm() As Boolean

        ' Page PORTRAIT
        ' outer loop through every control, the loop looks only at comboboxes
        For Each outerControl As Control In Me.tpDataPortrait.Controls
            Dim oCtrlType As String = UCase(outerControl.GetType().ToString)
            If oCtrlType.Contains("COMBOBOX") AndAlso (UCase(outerControl.Text) <> "KEINE") Then
                ' inner loop through every control, the loop looks only at comboboxes
                For Each innerControl As Control In Me.tpDataPortrait.Controls
                    Dim iCtrlType As String = UCase(innerControl.GetType().ToString)
                    If iCtrlType.Contains("COMBOBOX") AndAlso _
                        Not (outerControl Is innerControl) AndAlso _
                        (UCase(innerControl.Text) <> "KEINE") AndAlso _
                        (UCase(innerControl.Text) = UCase(outerControl.Text)) Then
                        MessageBox.Show(outerControl.Tag.ToString & " darf nicht identisch sein mit " & innerControl.Tag.ToString, _
                                        "Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                Next
            End If
        Next

        ' Page LANDSCAPE
        ' outer loop through every control, the loop looks only at comboboxes
        For Each outerControl As Control In Me.tpDataLandscape.Controls
            Dim oCtrlType As String = UCase(outerControl.GetType().ToString)
            If oCtrlType.Contains("COMBOBOX") AndAlso (UCase(outerControl.Text) <> "KEINE") Then
                ' inner loop through every control, the loop looks only at comboboxes
                For Each innerControl As Control In Me.tpDataLandscape.Controls
                    Dim iCtrlType As String = UCase(innerControl.GetType().ToString)
                    If iCtrlType.Contains("COMBOBOX") AndAlso _
                        Not (outerControl Is innerControl) AndAlso _
                        (UCase(innerControl.Text) <> "KEINE") AndAlso _
                        (UCase(innerControl.Text) = UCase(outerControl.Text)) Then
                        MessageBox.Show(outerControl.Tag.ToString & " darf nicht identisch sein mit " & innerControl.Tag.ToString, _
                                        "Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                Next
            End If
        Next


        ' store from data in corresponding data object
        Dim reportDef As New ReportDefData()

        ' common settings
        reportDef.XlsTargetPath = Trim(txtXlsTargetPath.Text)
        reportDef.XlsTargetWorkSheet = Trim(txtXlsTargetWorkSheet.Text)
        reportDef.AttributeSperator = Trim(txtAttributeSeperator.Text)
        reportDef.AttributePartGroup = chkGroup.Checked
        reportDef.AttributePartAttributeName = chkAttributeName.Checked
        reportDef.AttributePartAttribute = chkAttribute.Checked
        If tcOrientation.SelectedTab Is tpDataPortrait Then
            reportDef.Orientation = ReportDefData.eOrientation.portrait
        Else
            reportDef.Orientation = ReportDefData.eOrientation.landscape
        End If

        ' save settings of PORTRAIT
        saveForm = setupOrientationPortrait(reportDef)

        ' save settings of LANDSCAPE
        saveForm = saveForm And setupOrientationLandscape(reportDef)

        If IO.File.Exists(reportDef.XlsTargetPath) Then
            ' read workbook to byte array
            reportDef.XlsTargetWorkbook = ObjectToByteArray(reportDef.XlsTargetPath)
            ' check if target workbook contains defined worksheet
            Dim xlApp As Application
            'create an excel app always because it will be closed always
            xlApp = CreateObject("Excel.Application")
            If xlApp.Visible Then
                xlApp.Visible = False
            End If

            Dim xlWb As Workbook = xlApp.Workbooks.Open(reportDef.XlsTargetPath)
            Try
                Dim xlWs As Worksheet = xlApp.ActiveWorkbook.Worksheets(reportDef.XlsTargetWorkSheet)
            Catch ex As Exception
                MessageBox.Show("In der angegebenen Vorlagedatei ist das definierte Zielblatt nicht enthalten.", "Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
                saveForm = False
            Finally
                xlWb.Close()
                xlWb = Nothing
                xlApp.Quit()
                xlApp = Nothing
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        Else
            reportDef.XlsTargetWorkbook = Nothing
        End If

        If saveForm Then
            reportManager.ReportData = reportDef
        End If
    End Function

    ''' <summary>
    ''' Save settings of orientation PORTRAIT
    ''' </summary>
    ''' <param name="reportDef">data object the reports settings are stored in</param>
    Private Function setupOrientationPortrait(ByRef reportDef As ReportDefData) As Boolean
        Dim saveForm As Boolean = True

        ' PORTRAIT settings
        ' copy current form data to data object
        reportDef.ColAttributeText = cboColAttributeText.Text
        reportDef.ColAttributeNr = cboColAttributeNr.Text
        reportDef.ColAttributeUnit = cboColAttributeUnit.Text
        reportDef.ColAttributeTolerance = cboColAttributeTolerance.Text
        reportDef.ColAttributeToleranceCol2 = cboColAttributeToleranceCol2.Text
        reportDef.ColAttributeToleranceCol3 = cboColAttributeToleranceCol3.Text
        reportDef.ColDataStart = cboColDataStart.Text
        reportDef.ColDataStop = cboColDataStop.Text

        If (nudRowTitle.Value >= nudRowStart.Value) And _
           (nudRowTitle.Value <= nudRowStop.Value) Then
            MessageBox.Show("Zeile für Teileüberschrift darf nicht im Bereich der Datenzeilen liegen.", _
                            "Hochformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        Else
            reportDef.RowTitle = nudRowTitle.Value
        End If
        reportDef.TitlePrefixP = Trim(txtTitlePrefixP.Text)

        If ((nudRowStop.Value > Convert.ToDecimal(0)) And (nudRowStart.Value < nudRowStop.Value)) Or _
           (nudRowStop.Value = Convert.ToDecimal(0)) Then
            reportDef.RowStart = nudRowStart.Value
            reportDef.RowStop = nudRowStop.Value
        Else
            MessageBox.Show("Stoppzeile für Merkmale und Daten darf nicht kleiner als die Startzeile sein.", _
                            "Hochformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        End If


        If (cboColDataStop.SelectedIndex <= cboColDataStart.SelectedIndex) Then
            MessageBox.Show("Startspalte für Daten darf nicht größer als die Stopspalte sein.", _
                            "Hochformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        End If

        If rdbTolFormatPlusMinusP.Checked Then
            reportDef.AttributeToleranceFormatP = ReportDefData.eToleranceDisplayFormat.plusMinus
        Else
            reportDef.AttributeToleranceFormatP = ReportDefData.eToleranceDisplayFormat.absolut
        End If

        If nudColDataOffset.Value < Convert.ToDecimal((cboColDataStop.SelectedIndex - cboColDataStart.SelectedIndex)) Then
            reportDef.ColDataOffset = nudColDataOffset.Value
        Else
            MessageBox.Show("Spaltenoffset darf nicht größer als Abstand zwischen Start- und Stoppspalte sein.", _
                            "Hochformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        End If
        Return saveForm
    End Function

    ''' <summary>
    ''' Save settings of orientation LANDSCAPE
    ''' </summary>
    ''' <param name="reportDef">data object the reports settings are stored in</param>
    Private Function setupOrientationLandscape(ByRef reportDef As ReportDefData) As Boolean
        Dim saveForm As Boolean = True

        ' LANDSCAPE settings
        ' copy current form data to data object
        If (cboColStart.SelectedIndex < cboColStop.SelectedIndex) Then
            reportDef.ColStart = cboColStart.Text
            reportDef.ColStop = cboColStop.Text
        Else
            MessageBox.Show("Stoppspalte für Merkmale und Daten darf nicht kleiner als die Startspalte sein.", _
                            "Querformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        End If
        If (cboColTitle.SelectedIndex > 0) And _
            ((cboColTitle.SelectedIndex - 1) >= cboColStart.SelectedIndex) And _
            ((cboColTitle.SelectedIndex - 1) <= cboColStop.SelectedIndex) Then
            MessageBox.Show("Spalte für Teileüberschrift darf nicht im Bereich der Datenspalten liegen.", _
                            "Querformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        Else
            reportDef.ColTitle = cboColTitle.Text
        End If
        reportDef.TitlePrefixL = Trim(txtTitlePrefixL.Text)
        reportDef.RowAttributeText = nudRowAttributeText.Value
        reportDef.RowAttributeNr = nudRowAttributeNr.Value
        reportDef.RowAttributeUnit = nudRowAttributeUnit.Value
        reportDef.RowAttributeTolerance = nudRowAttributeTolerance.Value
        reportDef.RowAttributeToleranceRow2 = nudRowAttributeToleranceRow2.Value
        reportDef.RowAttributeToleranceRow3 = nudRowAttributeToleranceRow3.Value
        If rdbTolFormatPlusMinusL.Checked Then
            reportDef.AttributeToleranceFormatL = ReportDefData.eToleranceDisplayFormat.plusMinus
        Else
            reportDef.AttributeToleranceFormatL = ReportDefData.eToleranceDisplayFormat.absolut
        End If
        reportDef.RowDataStart = nudRowDataStart.Value
        reportDef.RowDataStop = nudRowDataStop.Value

        If (nudRowDataStop.Value <= nudRowDataStart.Value) Then
            MessageBox.Show("Startzeile für Daten darf nicht größer als die Stopzeile sein.", _
                            "Querformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        End If

        If nudRowDataOffset.Value < (nudRowDataStop.Value - nudRowDataStart.Value) Then
            reportDef.RowDataOffset = nudRowDataOffset.Value
        Else
            MessageBox.Show("Zeilenoffset darf nicht größer als Abstand zwischen Start- und Stoppzeile sein.", _
                            "Querformat: Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveForm = False
        End If

        Return saveForm
    End Function

    Private Sub btnLaden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefinitionLaden.Click
        ReadReportDefinition()
    End Sub

    ''' <summary>
    ''' Shows dialog to open and show content of report definition file
    ''' </summary>
    Private Sub ReadReportDefinition()
        ofdDefFile.Title = "Selektiere die zu öffnende Definitionsdatei"
        If ofdDefFile.ShowDialog = DialogResult.OK Then
            ' read data into object
            If reportManager.readReportDefFromFile(ofdDefFile.FileName) Then
                ' update form with loaded data
                updateForm()
                Me.Text = CAppTitle & " : " & Path.GetFileName(ofdDefFile.FileName)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Closes form/application
    ''' </summary>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefinitionSave.Click
        SaveReportDefinition()
    End Sub

    ''' <summary>
    ''' Shows dialog to save current report definition in a file (defined by SaveAs dialog)
    ''' </summary>
    Private Sub SaveReportDefinition()
        If IO.File.Exists(txtXlsTargetPath.Text) Then
            If Not (Trim(txtXlsTargetWorkSheet.Text) = String.Empty) Then
                ' copy data into object
                If saveForm() Then
                    If sfdDefFile.ShowDialog = DialogResult.OK Then
                        ' save data object to file
                        Try
                            reportManager.saveReportDefToFile(sfdDefFile.FileName)
                            Me.Text = CAppTitle & " : " & Path.GetFileName(sfdDefFile.FileName)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Try
                    End If
                End If
            Else
                MessageBox.Show("Es wurde kein Zielblatt der Excel-Reportvorlage definiert in das Daten kopiert werden sollen.", "Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Es wurde keine Excel Reportvorlage definiert.", "Speichern nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnDefinitionRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefinitionRun.Click
        CreateReport()
    End Sub

    ''' <summary>
    ''' Show dialog to open report definition file and process it to get a new Excel report
    ''' </summary>
    Private Sub CreateReport()
        ' select report definition file to be opened
        ofdDefFile.Title = "Selektiere die auszuführende Definitionsdatei"
        If ofdDefFile.ShowDialog = DialogResult.OK Then
            ' select report datafile file to be opened
            If ofdDfdFile.ShowDialog = DialogResult.OK Then
                Dim dfdFileName As String = ofdDfdFile.FileName
                Dim dfxFileName As String = UCase(dfdFileName).Replace(".DFD", ".DFX")
                If IO.File.Exists(dfxFileName) Then
                    ' read report definition data into object
                    If reportManager.readReportDefFromFile(ofdDefFile.FileName) Then

                        ' create XLS file using byte array contained in _reportDefData
                        Dim xlsTempFile As String = System.IO.Path.GetTempFileName()
                        xlsTempFile = UCase(xlsTempFile).Replace(".TMP", ".XLS")
                        ' write data to file and open file as excel workbook
                        IO.File.WriteAllBytes(xlsTempFile, reportManager.ReportData.XlsTargetWorkbook)

                        ' create xls report and copy data to report
                        If reportManager.FillOutReport(dfdFileName, dfxFileName, xlsTempFile, pgbProcess) Then
                            ' successful report genration
                        Else
                            ' report genration not successful 
                        End If
                    End If
                Else
                    MessageBox.Show("Datei " & dfxFileName & " nicht vorhanden.", "Report kann nicht erstellt werden.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
        Me.Text = CAppTitle
    End Sub

    ''' <summary>
    ''' Adjust progressbar to width of form
    ''' </summary>
    Private Sub resizeProgressbar()
        pgbProcess.Width = Me.Width - 30
    End Sub

    Private Sub frmMain_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        resizeProgressbar()
    End Sub

Private Sub frmMain_HelpButtonClicked( sender As Object,  e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Dim path As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        path = path + System.IO.Path.DirectorySeparatorChar + "Documentation" + System.IO.Path.DirectorySeparatorChar + CHelpFileName
        System.Diagnostics.Process.Start(path)
End Sub
End Class

