' **********************************************************************
' *         © COPYRIGHT 2019 Autodesk, Inc.All Rights Reserved         *
' *                                                                    *
' *  Use of this software is subject to the terms of the Autodesk      *
' *  license agreement provided at the time of installation            *
' *  or download, or which otherwise accompanies this software         *
' *  in either electronic or hard copy form.                           *
' **********************************************************************

Imports System.IO
Imports System.Collections
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms

''' <summary>
''' Class to handle report definition 
''' </summary>
Public Class ReportManager

    ''' <summary>
    ''' Delimiter symbol of tolernace string
    ''' </summary>
    Private Const CToleranceSeperator As String = " / "

    ''' <summary>
    ''' Version of supported by current program
    ''' </summary>
    Public Const CDefinitionVersion As Integer = 3

    ''' <summary>
    ''' Stores the report definition data
    ''' </summary>
    Public ReportData As ReportDefData

    ''' <summary>
    ''' List of (report) traget sheets
    ''' </summary>
    ''' <remarks>List contains name of the (target) sheet and the attribute index/nr range of the sheet</remarks>
    Private _attributeTargetSheets As New ObjectModel.Collection(Of AttributeDataTarget)


    ''' <summary>
    ''' Opens report definition file and stores definition data in memory
    ''' </summary>
    ''' <param name="fileName">Report definition file to open</param>
    ''' <returns>True if reading was successfull</returns>
    ''' <remarks>Report definition data contains also excel report template</remarks>
    Public Function readReportDefFromFile(ByVal fileName As String) As Boolean
        readReportDefFromFile = False
        If IO.File.Exists(fileName) Then
            Dim reportDef As New ReportDefData()

            'Deserialize text file to a new object.
            ' Open the file containing the data that you want to deserialize.
            Dim fs As New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Try
                Dim formatter As New BinaryFormatter

                ' Deserialize the hashtable from the file and 
                ' assign the reference to the local variable.
                reportDef = DirectCast(formatter.Deserialize(fs), ReportDefData)
                If reportDef.DefinitionVersion <> CDefinitionVersion Then
                    Throw New SerializationException
                End If
            Catch e As SerializationException
                MessageBox.Show("Definitionsversion der Datei: " & Path.GetFileName(fileName) & _
                        " entspricht evtl. nicht der vom Programm unterstützten Version: " & CDefinitionVersion.ToString & Chr(10) & Chr(13) & _
                        "Detail: " & e.Message, "Reportdefinition kann nicht geladen werden", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Finally
                fs.Close()
            End Try

            ReportData = reportDef
            readReportDefFromFile = True
        End If
    End Function

    ''' <summary>
    ''' Save current report definition in xml file
    ''' </summary>
    ''' <param name="fileName">xml file of report definition</param>
    ''' <returns>True if saving was successfull</returns>
    ''' <remarks>Report definition data contains also excel report template</remarks>
    Public Function saveReportDefToFile(ByVal fileName As String) As Boolean
        saveReportDefToFile = False

        'Serialize objects to a binary file.
        Dim fs As New FileStream(fileName, FileMode.Create)
        ' Construct a BinaryFormatter and use it to serialize the data to the stream.
        Dim formatter As New BinaryFormatter
        Try
            formatter.Serialize(fs, ReportData)
            saveReportDefToFile = True
        Catch e As SerializationException
            MessageBox.Show("Reason: " & e.Message, "Failed to serialize.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            fs.Close()
        End Try

    End Function

    ''' <summary>
    ''' Sart Excel, opens report template and copy attribute description and data to report
    ''' </summary>
    ''' <param name="dfdFileName">Name of the attribute description file</param>
    ''' <param name="dfxFileName">Name of the attribute data file</param>
    ''' <param name="xlsFileName">Name of the Excel report template</param>
    ''' <param name="pgbProcess">progressbar control of the form where the progress will be shown</param>
    ''' <returns>True if building report was successfull</returns>
    Public Function FillOutReport(ByVal dfdFileName As String, ByVal dfxFileName As String, _
                                  ByVal xlsFileName As String, ByRef pgbProcess As ToolStripProgressBar) As Boolean
        FillOutReport = False
        Dim oldCursor As Cursor = Cursor.Current
        Try
            System.Windows.Forms.Application.UseWaitCursor = True
            Cursor.Current = Cursors.WaitCursor

            ' create collection to store attribute descriptions
            Dim attribDescriptionCol As AttributeDescriptionCol = New AttributeDescriptionCol(ReportData.AttributeSperator)
            ' read attribute description out of dfd file and store data in list, function returns true if attributecount >0 only
            If attribDescriptionCol.ReadAttributeDescriptionsFromFile(dfdFileName) Then

                ' start excel and open report template
                Dim xlApp As Microsoft.Office.Interop.Excel.Application
                Try
                    xlApp = GetObject(, "Excel.Application")
                Catch ex As Exception
                    xlApp = CreateObject("Excel.Application")
                End Try
                Dim xlWb As Workbook = xlApp.Workbooks.Open(xlsFileName)
                Try
                    ' clear list of target worksheets and the containing attribute descriptions on every sheet
                    _attributeTargetSheets.Clear()
                    ' add 1st sheet to the list
                    Dim attributeDataTarget As AttributeDataTarget = New AttributeDataTarget()
                    attributeDataTarget.TargetSheetName = ReportData.XlsTargetWorkSheet
                    ' copy nr/index of first attribute to list
                    attributeDataTarget.AttributeNameFrom = attribDescriptionCol.Descriptions.Item(0).Index
                    ' last index of first sheet will be updated if it is well-known
                    attributeDataTarget.AttributeNameTo = -1
                    _attributeTargetSheets.Add(attributeDataTarget)

                    ' use first worksheet to store attribute description
                    FillOutReportWithAttributeDescriptions(xlWb, attribDescriptionCol)

                    ' read attribute data values of every part out of dfx file and store data in list
                    Dim partCol As PartCol = New PartCol()
                    If partCol.ReadAttributeDataFromFile(dfxFileName) Then
                        ' copy data (read of dfx-file) to xls target sheet, description is needed to check limits
                        FillOutReportWithAttributeData(xlWb, attribDescriptionCol, partCol, pgbProcess)
                    End If

                    FillOutReport = True
                Catch ex As Exception
                    MessageBox.Show("Reason: " & ex.Message, "Failed to access excel target worksheet.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (Not xlApp Is Nothing) AndAlso Not xlApp.Visible Then
                xlApp.Visible = True
            End If
            xlApp = Nothing
        End Try
            End If
        Catch ex As Exception
            MessageBox.Show("Reason: " & ex.Message, "Failed to create excel workbook.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            System.Windows.Forms.Application.UseWaitCursor = False
            Cursor.Current = oldCursor
        End Try

    End Function

    ''' <summary>
    ''' Copy previously read attribute data values to known list of Excel (target) worksheets (of the report template)
    ''' </summary>
    ''' <param name="xlWb">Target Excel workbook, the attribute data will be copied to</param>
    ''' <param name="attribDescriptionCol">Collection of attributes which will be copied to report</param>
    ''' <param name="partCol">Collection of parts and their data values which will be copied to report</param>
    ''' <param name="pgbProcess">progressbar control of the form where the progress will be shown</param>
    ''' <remarks>AttributeDescriptionCol is needed to check limits of data value</remarks>
    Private Sub FillOutReportWithAttributeData(ByRef xlWb As Workbook, _
                ByRef attribDescriptionCol As AttributeDescriptionCol, _
                ByRef partCol As PartCol, ByRef pgbProcess As ToolStripProgressBar)
        ' copy data from list to sheet
        Dim xlWs As Worksheet
        xlWs = Nothing
        Dim xlWsName As String = String.Empty

        Dim attribIndex As Integer
        attribIndex = 1
        Dim partIndex As Integer = 0
        Dim partCount As Integer = 1
        Dim targetRow As Integer
        Dim targetCol As Integer

        ' setup progrssbar
        pgbProcess.Maximum = partCol.Parts.Count * attribDescriptionCol.AttributeCount

        ' define firat target sheet to store data values
        If _attributeTargetSheets.Count > 0 Then
            If ReportData.Orientation = ReportDefData.eOrientation.portrait Then

                Dim xlWsRowFrom As Integer = 0
                Dim xlWsRowTo As Integer = -1
                For Each part As DataValueCol In partCol.Parts
                    ' start every new part with first attribute
                    For Each attributeDataValue As Double In part.DataValues

                        ' get the name of the target sheet dependent of current attribute to be processed
                        If attribIndex > xlWsRowTo Then
                            For Each targetSheet As AttributeDataTarget In _attributeTargetSheets
                                If (targetSheet.AttributeNameFrom <= attribIndex) And _
                                    (attribIndex <= targetSheet.AttributeNameTo) Then
                                    xlWsName = targetSheet.TargetSheetName
                                    xlWsRowFrom = targetSheet.AttributeNameFrom
                                    xlWsRowTo = targetSheet.AttributeNameTo

                                    ' copy title of current part to sheet
                                    xlWs = xlWb.Worksheets(xlWsName)
                                    If ReportData.RowTitle > 0 Then
                                        targetCol = xlWs.Columns(ReportData.ColDataStart).Column + partIndex
                                        xlWs.Cells(ReportData.RowTitle, targetCol).Value = ReportData.TitlePrefixP & " " & Convert.ToInt16(partCount)
                                    End If

                                    Exit For
                                End If
                            Next
                        End If

                        If Not (xlWsName = String.Empty) Then
                            ' calculate target row
                            targetRow = attribIndex - xlWsRowFrom + ReportData.RowStart
                            ' calculate target column 
                            targetCol = xlWs.Columns(ReportData.ColDataStart).Column + partIndex
                            xlWs.Cells(targetRow, targetCol).Value = attributeDataValue
                            pgbProcess.PerformStep()
                            ' use red color to display values out of tolerance
                            If ReportData.ColAttributeTolerance.ToUpper <> "KEINE" Then
                                If (attributeDataValue < attribDescriptionCol.Descriptions.Item(attribIndex - 1).AttributeLowerLimit) Or _
                                    (attributeDataValue > attribDescriptionCol.Descriptions.Item(attribIndex - 1).AttributeUpperLimit) Then
                                    xlWs.Cells(targetRow, targetCol).Font.Color = RGB(255, 0, 0)
                                End If
                            End If
                        End If
                        attribIndex = attribIndex + 1
                    Next

                    ' check if new sheet has to be added, because max count of parts per sheet is exceeded
                    If (xlWs.Columns(ReportData.ColDataStart).Column + partIndex + ReportData.ColDataOffset) > (xlWs.Columns(ReportData.ColDataStop).Column) Then
                        ' add a set of target sheets (to store all attributes of the next parts)
                        For Each targetSheet As AttributeDataTarget In _attributeTargetSheets
                            xlWs = Nothing
                            xlWs = xlWb.Worksheets(targetSheet.TargetSheetName)
                            xlWs.Copy(, xlWb.Worksheets(xlWb.Worksheets.Count))
                            xlWs = xlWb.ActiveSheet

                            ' update list with name of new sheet
                            targetSheet.TargetSheetName = xlWs.Name

                            ' clear data columns and remove red font color (used if previously values were out or tolerance) 
                            If ReportData.RowStop > 0 Then
                                xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowStart.ToString), _
                                           xlWs.Range(ReportData.ColDataStop & ReportData.RowStop.ToString)).Value = ""
                                xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowStart.ToString), _
                                           xlWs.Range(ReportData.ColDataStop & ReportData.RowStop.ToString)).Font.Color = RGB(0, 0, 0)
                            Else
                                xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowStart.ToString), _
                                           xlWs.Range(ReportData.ColDataStop & (ReportData.RowStart + attribDescriptionCol.AttributeCount).ToString)).Value = ""
                                xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowStart.ToString), _
                                           xlWs.Range(ReportData.ColDataStop & (ReportData.RowStart + attribDescriptionCol.AttributeCount).ToString)).Font.Color = RGB(0, 0, 0)
                            End If
                            ' clear title row 
                            If ReportData.RowTitle > 0 Then
                                xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowTitle.ToString), _
                                           xlWs.Range(ReportData.ColDataStop & ReportData.RowTitle.ToString)).Value = ""
                            End If
                        Next
                        partIndex = 0
                    Else
                        partIndex = partIndex + ReportData.ColDataOffset
                    End If
                    partCount = partCount + 1

                    ' reset data to get target worksheet of the first attribute again
                    attribIndex = 1
                    xlWsRowFrom = 0
                    xlWsRowTo = -1
                Next
            ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape Then

                Dim xlWsColFrom As Integer = 0
                Dim xlWsColTo As Integer = -1

                Dim xlsColumns() As String = New String() {}
                ReDim xlsColumns(255)
                frmMain.BuildListOfXlsColumns(xlsColumns)
                Dim idxColStart As Integer = Array.IndexOf(xlsColumns, ReportData.ColStart) + 1
                Dim idxColStop As Integer = Array.IndexOf(xlsColumns, ReportData.ColStop) + 1
                Dim idxColTitle As Integer = GetIdxColTitle(xlsColumns)

                For Each part As DataValueCol In partCol.Parts
                    ' start every new part with first attribute
                    For Each attributeDataValue As Double In part.DataValues

                        ' get the name of the target sheet dependent of current attribute to be processed
                        If attribIndex > xlWsColTo Then
                            For Each targetSheet As AttributeDataTarget In _attributeTargetSheets
                                If (targetSheet.AttributeNameFrom <= attribIndex) And _
                                    (attribIndex <= targetSheet.AttributeNameTo) Then
                                    xlWsName = targetSheet.TargetSheetName
                                    xlWsColFrom = targetSheet.AttributeNameFrom
                                    xlWsColTo = targetSheet.AttributeNameTo

                                    ' copy title of current part to sheet
                                    xlWs = xlWb.Worksheets(xlWsName)
                                    If idxColTitle > 0 Then
                                        targetRow = ReportData.RowDataStart + partIndex
                                        xlWs.Cells(targetRow, idxColTitle).Value = ReportData.TitlePrefixL & " " & Convert.ToInt16(partCount)
                                    End If

                                    Exit For
                                End If
                            Next
                        End If

                        If Not (xlWsName = String.Empty) Then
                            ' calculate target column
                            targetCol = attribIndex - xlWsColFrom + idxColStart
                            ' calculate target row 
                            targetRow = ReportData.RowDataStart + partIndex
                            xlWs.Cells(targetRow, targetCol).Value = attributeDataValue
                            pgbProcess.PerformStep()
                            ' use red color to display values out of tolerance
                            If ReportData.ColAttributeTolerance.ToUpper <> "KEINE" Then
                                If (attributeDataValue < attribDescriptionCol.Descriptions.Item(attribIndex - 1).AttributeLowerLimit) Or _
                                    (attributeDataValue > attribDescriptionCol.Descriptions.Item(attribIndex - 1).AttributeUpperLimit) Then
                                    xlWs.Cells(targetRow, targetCol).Font.Color = RGB(255, 0, 0)
                                End If
                            End If
                        End If
                        attribIndex = attribIndex + 1
                    Next

                    ' check if new sheet has to be added, because max count of parts per sheet is exceeded
                    If (ReportData.RowDataStart + partIndex + ReportData.RowDataOffset) > (ReportData.RowDataStop) Then
                        ' add a set of target sheets (to store all attributes of the next parts)
                        For Each targetSheet As AttributeDataTarget In _attributeTargetSheets
                            xlWs = Nothing
                            xlWs = xlWb.Worksheets(targetSheet.TargetSheetName)
                            xlWs.Copy(, xlWb.Worksheets(xlWb.Worksheets.Count))
                            xlWs = xlWb.ActiveSheet

                            ' update list with name of new sheet
                            targetSheet.TargetSheetName = xlWs.Name

                            ' clear data columns and remove red font color (used if previously values were out or tolerance) 
                            If idxColStop > 0 Then
                                ' clear data columns and remove red font color (used if previously values were out or tolernace) 
                                xlWs.Range(xlWs.Cells(ReportData.RowDataStart, idxColStart), xlWs.Cells(ReportData.RowDataStop, idxColStop)).Value = ""
                                xlWs.Range(xlWs.Cells(ReportData.RowDataStart, idxColStart), xlWs.Cells(ReportData.RowDataStop, idxColStop)).Font.Color = RGB(0, 0, 0)
                            Else
                                ' clear data columns and remove red font color (used if previously values were out or tolernace) 
                                xlWs.Range(xlWs.Cells(ReportData.RowDataStart, idxColStart), xlWs.Cells(ReportData.RowDataStop, idxColStart + attribDescriptionCol.AttributeCount)).Value = ""
                                xlWs.Range(xlWs.Cells(ReportData.RowDataStart, idxColStart), xlWs.Cells(ReportData.RowDataStop, idxColStart + attribDescriptionCol.AttributeCount)).Font.Color = RGB(0, 0, 0)
                            End If
                            ' clear title row 
                            If idxColTitle > 0 Then
                                xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowTitle.ToString), _
                                           xlWs.Range(ReportData.ColDataStop & ReportData.RowTitle.ToString)).Value = ""
                            End If
                        Next
                        partIndex = 0
                    Else
                        partIndex = partIndex + ReportData.RowDataOffset
                    End If
                    partCount = partCount + 1

                    ' reset data to get target worksheet of the first attribute again
                    attribIndex = 1
                    xlWsColFrom = 0
                    xlWsColTo = -1
                Next
            End If
        End If
        pgbProcess.Value = 0
    End Sub

    ''' <summary>
    ''' Copy previously read attribute descriptions to known list of Excel (target) worksheets (of the report template)
    ''' </summary>
    ''' <param name="xlWb">Target Excel workbook, the attribute descriptions will be copied to</param>
    ''' <param name="attribDescriptionCol">Collection of attributes which will be copied to report</param>
    Private Sub FillOutReportWithAttributeDescriptions(ByRef xlWb As Workbook, ByRef attribDescriptionCol As AttributeDescriptionCol)

        Dim xlsColumns() As String = New String() {}
        ReDim xlsColumns(255)
        frmMain.BuildListOfXlsColumns(xlsColumns)
        Dim idxColStart As Integer = Array.IndexOf(xlsColumns, ReportData.ColStart) + 1
        Dim idxColStop As Integer = Array.IndexOf(xlsColumns, ReportData.ColStop) + 1

        Dim xlWs As Worksheet = xlWb.Worksheets(ReportData.XlsTargetWorkSheet)

        ' copy attribute description to excel sheet
        Dim idxAttribute As Integer = 0
        For Each attribute As AttributeDef In attribDescriptionCol.Descriptions

            ' "Merkmal Bezeichnung"
            Dim attributeText As String = String.Empty
            ' concat attribute parts to one description
            If ReportData.AttributePartGroup Then
                attributeText = attributeText & " " & attribute.Group
            End If
            If ReportData.AttributePartAttributeName Then
                attributeText = attributeText & " " & attribute.AttributeName
            End If
            If ReportData.AttributePartAttribute Then
                attributeText = attributeText & " " & attribute.Attribute
            End If
            If ReportData.Orientation = ReportDefData.eOrientation.portrait And UCase(ReportData.ColAttributeText) <> "KEINE" Then
                xlWs.Range(ReportData.ColAttributeText & (idxAttribute + ReportData.RowStart).ToString).Value = attributeText
            ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape And ReportData.RowAttributeText > 0 And (idxColStart >= 0) Then
                ' Zeile: Zielzeile (fest) = RowAttributeText  ## Spalte: Startsplate (fest) = ColStart + index
                xlWs.Cells(ReportData.RowAttributeText, (idxColStart + idxAttribute)).Value = attributeText
            End If

            ' "Merkmal Nummer"
            If ReportData.Orientation = ReportDefData.eOrientation.portrait And UCase(ReportData.ColAttributeNr) <> "KEINE" Then
                xlWs.Range(ReportData.ColAttributeNr & (idxAttribute + ReportData.RowStart).ToString).Value = attribute.Index.ToString
            ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape And ReportData.RowAttributeNr > 0 And (idxColStart >= 0) Then
                ' Zeile: Zielzeile (fest) = RowAttributeNr  ## Spalte: Startsplate (fest) = ColStart + index
                xlWs.Cells(ReportData.RowAttributeNr, (idxColStart + idxAttribute)).Value = attribute.Index.ToString
            End If

                ' "Merkmal Einheit"
            If ReportData.Orientation = ReportDefData.eOrientation.portrait And UCase(ReportData.ColAttributeUnit) <> "KEINE" Then
                xlWs.Range(ReportData.ColAttributeUnit & (idxAttribute + ReportData.RowStart).ToString).Value = attribute.AttributeUnit
            ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape And ReportData.RowAttributeUnit > 0 And (idxColStart >= 0) Then
                ' Zeile: Zielzeile (fest) = RowAttributUnit  ## Spalte: Startsplate (fest) = ColStart + index
                xlWs.Cells(ReportData.RowAttributeUnit, (idxColStart + idxAttribute)).Value = attribute.AttributeUnit
            End If

            ' "Merkmal Toleranz"
            Dim attributeTolerance As String = String.Empty
            Dim attributeTolerance2 As String = String.Empty
            Dim attributeTolerance3 As String = String.Empty
            Dim doTol3 As Boolean
            If ReportData.Orientation = ReportDefData.eOrientation.portrait Then
                doTol3 = (UCase(ReportData.ColAttributeToleranceCol2) <> "KEINE")
            ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape Then
                doTol3 = (UCase(ReportData.RowAttributeToleranceRow2) > 0)
            End If


            If (ReportData.Orientation = ReportDefData.eOrientation.portrait And (UCase(ReportData.ColAttributeTolerance) <> "KEINE" Or doTol3)) Or _
                (ReportData.Orientation = ReportDefData.eOrientation.landscape And (ReportData.RowAttributeTolerance > 0 Or doTol3)) Then
                Dim lowerVal As Double
                Dim defaultVal As Double
                Dim upperVal As Double

                defaultVal = attribute.AttributeDefaultValue
                lowerVal = attribute.AttributeLowerLimit
                upperVal = attribute.AttributeUpperLimit

                ' format tolerance dependent on definition
                If (ReportData.Orientation = ReportDefData.eOrientation.portrait And ReportData.AttributeToleranceFormatP = ReportDefData.eToleranceDisplayFormat.plusMinus) Or _
                    (ReportData.Orientation = ReportDefData.eOrientation.landscape And ReportData.AttributeToleranceFormatL = ReportDefData.eToleranceDisplayFormat.plusMinus) Then
                    ' display tolerance in format +/-

                    If attribute.AttributeDefaultValueExist Then

                        If (attribute.AttributeLowerLimitExist AndAlso (defaultVal <> lowerVal)) Or doTol3 Then

                            If (attribute.AttributeUpperLimitExist AndAlso (defaultVal <> upperVal)) Or doTol3 Then

                                ' lower, default and upper values defined and they are different to the default value
                                ' we have to display a + and a - tolerance
                                If (Math.Abs(defaultVal - lowerVal) = Math.Abs(upperVal - defaultVal)) Or doTol3 Then
                                    ' display +- format
                                    If doTol3 Then
                                        attributeTolerance = (-Math.Abs(defaultVal - lowerVal)).ToString("N03")
                                        attributeTolerance2 = defaultVal.ToString("N03")
                                        attributeTolerance3 = Math.Abs(upperVal - defaultVal).ToString("N03")
                                    Else
                                        ' store nominal val & tolerances in one column
                                        attributeTolerance = defaultVal.ToString("N03") & _
                                                             CToleranceSeperator & "+-" & Math.Abs(upperVal - defaultVal).ToString("N03")
                                    End If
                                Else
                                    ' display + tolerance and - tolerance separately
                                    attributeTolerance = defaultVal.ToString("N03") & _
                                                         CToleranceSeperator & "+" & Math.Abs(upperVal - defaultVal).ToString("N03") & _
                                                         CToleranceSeperator & "-" & Math.Abs(defaultVal - lowerVal).ToString("N03")
                                End If
                            Else
                                ' no upper limit defined => show default value and lower limit
                                attributeTolerance = defaultVal.ToString("N03") & _
                                                     CToleranceSeperator & "-" & Math.Abs(defaultVal - lowerVal).ToString("N03")
                            End If

                        Else
                            ' no lower limit defined => check upper limit
                            If attribute.AttributeUpperLimitExist AndAlso (defaultVal <> upperVal) Then
                                ' upper limit defined => show default value and upper limit
                                attributeTolerance = defaultVal.ToString("N03") & _
                                                     CToleranceSeperator & "+" & Math.Abs(upperVal - defaultVal).ToString("N03")
                            Else
                                ' no upper limit defined => show default value only
                                attributeTolerance = defaultVal.ToString("N03")
                            End If
                        End If
                    Else
                        ' if no default value => no toleance should be displayed
                    End If

                ElseIf (ReportData.Orientation = ReportDefData.eOrientation.portrait And ReportData.AttributeToleranceFormatP = ReportDefData.eToleranceDisplayFormat.absolut) Or _
                    (ReportData.Orientation = ReportDefData.eOrientation.landscape And ReportData.AttributeToleranceFormatL = ReportDefData.eToleranceDisplayFormat.absolut) Then
                    ' display tolerance in format absolut values

                    If doTol3 Then
                        attributeTolerance = lowerVal.ToString("N03")
                        attributeTolerance2 = defaultVal.ToString("N03")
                        attributeTolerance3 = upperVal.ToString("N03")
                    Else
                        ' show lower value if unequal to default value
                        If attribute.AttributeLowerLimit <> attribute.AttributeDefaultValue Then
                            attributeTolerance = lowerVal.ToString & CToleranceSeperator
                        Else
                            attributeTolerance = CToleranceSeperator
                        End If
                        ' show default value always
                        attributeTolerance = attributeTolerance & defaultVal.ToString & CToleranceSeperator
                        ' show upper value if unequal to default value
                        If attribute.AttributeUpperLimit <> attribute.AttributeDefaultValue Then
                            attributeTolerance = attributeTolerance & upperVal.ToString
                        End If

                    End If

                Else
                    ' no format defined => don't display tolerance values
                End If

                If (ReportData.Orientation = ReportDefData.eOrientation.portrait) And (UCase(ReportData.ColAttributeTolerance) <> "KEINE") Then
                    xlWs.Range(ReportData.ColAttributeTolerance & (idxAttribute + ReportData.RowStart).ToString).Value = attributeTolerance
                ElseIf (ReportData.Orientation = ReportDefData.eOrientation.landscape) And (ReportData.RowAttributeTolerance > 0) Then
                    ' Zeile: Zielzeile (fest) = RowAttributeTolerance  ## Spalte: Startsplate (fest) = ColStart + index
                    xlWs.Cells(ReportData.RowAttributeTolerance, (idxColStart + idxAttribute)).Value = attributeTolerance
                End If
                If doTol3 Then
                    If ReportData.Orientation = ReportDefData.eOrientation.portrait Then
                        xlWs.Range(ReportData.ColAttributeToleranceCol2 & (idxAttribute + ReportData.RowStart).ToString).Value = attributeTolerance2
                    ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape Then
                        ' Zeile: Zielzeile (fest) = RowAttributeToleranceRow2  ## Spalte: Startsplate (fest) = ColStart + index
                        xlWs.Cells(ReportData.RowAttributeToleranceRow2, (idxColStart + idxAttribute)).Value = attributeTolerance2
                    End If
                End If
                If (ReportData.Orientation = ReportDefData.eOrientation.portrait) And (UCase(ReportData.ColAttributeToleranceCol3) <> "KEINE") Then
                    xlWs.Range(ReportData.ColAttributeToleranceCol3 & (idxAttribute + ReportData.RowStart).ToString).Value = attributeTolerance3
                ElseIf (ReportData.Orientation = ReportDefData.eOrientation.landscape) And (ReportData.RowAttributeToleranceRow3 > 0) Then
                    ' Zeile: Zielzeile (fest) = RowAttributeToleranceRow3  ## Spalte: Startsplate (fest) = ColStart + index
                    xlWs.Cells(ReportData.RowAttributeToleranceRow3, (idxColStart + idxAttribute)).Value = attributeTolerance3
                End If
            End If

            idxAttribute = idxAttribute + 1

            ' check if new page necessary 
            If ReportData.Orientation = ReportDefData.eOrientation.portrait Then
                If ((idxAttribute + ReportData.RowStart) > ReportData.RowStop) And (ReportData.RowStop > 0) And (idxAttribute < attribDescriptionCol.AttributeCount) Then
                    ' copy result sheet and remove previously added data
                    AddNewTargetSheet(xlWb, xlWs, idxAttribute)
                End If
            ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape Then
                If ((idxAttribute + idxColStart) > idxColStop) And (idxColStop > 0) And (idxAttribute < attribDescriptionCol.AttributeCount) Then
                    ' copy result sheet and remove previously added data
                    AddNewTargetSheet(xlWb, xlWs, idxAttribute)
                End If
            End If

        Next
        ' store the nr/index of attributes of last sheet in list
        _attributeTargetSheets.Item(_attributeTargetSheets.Count - 1).AttributeNameTo = _
            (_attributeTargetSheets.Item(_attributeTargetSheets.Count - 1).AttributeNameFrom) + idxAttribute - 1

    End Sub
    ''' <summary>
    ''' Add a new (target) sheet to store additional attribute descriptions/data
    ''' </summary>
    ''' <param name="xlWb">Target Excel workbook, one sheet of thie workbook will be copied</param>
    ''' <param name="xlWs">Worksheet which will be copied</param>
    ''' <param name="idxAttribute">Attribute index/nr of the last attribute of the sheet to be copied</param>
    ''' <remarks>THe copied (new) sheet starts with Attribute index/nr  +1</remarks>
    Private Sub AddNewTargetSheet(ByRef xlWb As Workbook, ByRef xlWs As Worksheet, ByRef idxAttribute As Integer)

        xlWs.Copy(, xlWs)
        xlWs = xlWb.ActiveSheet

        ' store the nr of attributes of current sheet in list (current/last item of list)
        Dim tmpIdx As Integer = (_attributeTargetSheets.Item(_attributeTargetSheets.Count - 1).AttributeNameFrom) + idxAttribute - 1
        _attributeTargetSheets.Item(_attributeTargetSheets.Count - 1).AttributeNameTo = tmpIdx

        ' add added sheet to the list
        Dim attributeDataTarget As AttributeDataTarget = New AttributeDataTarget()
        attributeDataTarget.TargetSheetName = xlWs.Name
        attributeDataTarget.AttributeNameFrom = tmpIdx + 1
        attributeDataTarget.AttributeNameTo = -1
        _attributeTargetSheets.Add(attributeDataTarget)

        If ReportData.Orientation = ReportDefData.eOrientation.portrait Then

            ' clear attribute nr cells
            If ReportData.ColAttributeNr.ToUpper <> "KEINE" Then
                xlWs.Range(xlWs.Range(ReportData.ColAttributeNr & ReportData.RowStart.ToString), _
                                   xlWs.Range(ReportData.ColAttributeNr & ReportData.RowStop.ToString)).Value = ""
            End If
            ' clear attribute text cells
            If ReportData.ColAttributeText.ToUpper <> "KEINE" Then
                xlWs.Range(xlWs.Range(ReportData.ColAttributeText & ReportData.RowStart.ToString), _
                                   xlWs.Range(ReportData.ColAttributeText & ReportData.RowStop.ToString)).Value = ""
            End If
            ' clear attribute unit cells
            If ReportData.ColAttributeUnit.ToUpper <> "KEINE" Then
                xlWs.Range(xlWs.Range(ReportData.ColAttributeUnit & ReportData.RowStart.ToString), _
                           xlWs.Range(ReportData.ColAttributeUnit & ReportData.RowStop.ToString)).Value = ""
            End If
            ' clear attribute tolerance cells
            If ReportData.ColAttributeTolerance.ToUpper <> "KEINE" Then
                xlWs.Range(xlWs.Range(ReportData.ColAttributeTolerance & ReportData.RowStart.ToString), _
                           xlWs.Range(ReportData.ColAttributeTolerance & ReportData.RowStop.ToString)).Value = ""
            End If
            ' clear attribute tolerance (nomnal value) cells (second column)
            If ReportData.ColAttributeToleranceCol2.ToUpper <> "KEINE" Then
                xlWs.Range(xlWs.Range(ReportData.ColAttributeToleranceCol2 & ReportData.RowStart.ToString), _
                           xlWs.Range(ReportData.ColAttributeToleranceCol2 & ReportData.RowStop.ToString)).Value = ""
            End If
            ' clear attribute upper tolerance cells (third column)
            If ReportData.ColAttributeToleranceCol3.ToUpper <> "KEINE" Then
                xlWs.Range(xlWs.Range(ReportData.ColAttributeToleranceCol3 & ReportData.RowStart.ToString), _
                           xlWs.Range(ReportData.ColAttributeToleranceCol3 & ReportData.RowStop.ToString)).Value = ""
            End If
            ' clear data columns and remove red font color (used if previously values were out or tolernace) 
            xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowStart.ToString), _
                       xlWs.Range(ReportData.ColDataStop & ReportData.RowStop.ToString)).Value = ""
            xlWs.Range(xlWs.Range(ReportData.ColDataStart & ReportData.RowStart.ToString), _
                       xlWs.Range(ReportData.ColDataStop & ReportData.RowStop.ToString)).Font.Color = RGB(0, 0, 0)

        ElseIf ReportData.Orientation = ReportDefData.eOrientation.landscape Then

            Dim xlsColumns() As String = New String() {}
            ReDim xlsColumns(255)
            frmMain.BuildListOfXlsColumns(xlsColumns)
            Dim idxColStart As Integer = Array.IndexOf(xlsColumns, ReportData.ColStart) + 1
            Dim idxColStop As Integer = Array.IndexOf(xlsColumns, ReportData.ColStop) + 1
            ' clear attribute nr cells
            If ReportData.RowAttributeNr > 0 Then
                xlWs.Range(xlWs.Cells(ReportData.RowAttributeNr, idxColStart), xlWs.Cells(ReportData.RowAttributeNr, idxColStop)).Value = ""
            End If
            ' clear attribute text cells
            If ReportData.RowAttributeText > 0 Then
                xlWs.Range(xlWs.Cells(ReportData.RowAttributeText, idxColStart), xlWs.Cells(ReportData.RowAttributeText, idxColStop)).Value = ""
            End If
            ' clear attribute unit cells
            If ReportData.RowAttributeUnit > 0 Then
                xlWs.Range(xlWs.Cells(ReportData.RowAttributeUnit, idxColStart), xlWs.Cells(ReportData.RowAttributeUnit, idxColStop)).Value = ""
            End If
            ' clear attribute tolerance cells
            If ReportData.RowAttributeTolerance > 0 Then
                xlWs.Range(xlWs.Cells(ReportData.RowAttributeTolerance, idxColStart), xlWs.Cells(ReportData.RowAttributeTolerance, idxColStop)).Value = ""
            End If
            ' clear attribute tolerance (nominal value) cells (second row)
            If ReportData.RowAttributeToleranceRow2 > 0 Then
                xlWs.Range(xlWs.Cells(ReportData.RowAttributeToleranceRow2, idxColStart), xlWs.Cells(ReportData.RowAttributeToleranceRow2, idxColStop)).Value = ""
            End If
            ' clear attribute upper tolerance cells (third row)
            If ReportData.RowAttributeToleranceRow3 > 0 Then
                xlWs.Range(xlWs.Cells(ReportData.RowAttributeToleranceRow3, idxColStart), xlWs.Cells(ReportData.RowAttributeToleranceRow3, idxColStop)).Value = ""
            End If
            ' clear data columns and remove red font color (used if previously values were out or tolernace) 
            xlWs.Range(xlWs.Cells(ReportData.RowDataStart, idxColStart), xlWs.Cells(ReportData.RowDataStop, idxColStop)).Value = ""
            xlWs.Range(xlWs.Cells(ReportData.RowDataStart, idxColStart), xlWs.Cells(ReportData.RowDataStop, idxColStop)).Font.Color = RGB(0, 0, 0)
        End If
        idxAttribute = 0
    End Sub

    ''' <summary>
    ''' Get index of column name of column title, rearding "KEINE"
    ''' </summary>
    ''' <param name="xlsColumns">List of available columns (without "KEINE")</param>
    ''' <returns>Listindex</returns>
    Private Function GetIdxColTitle(ByVal xlsColumns As String()) As Integer
        Dim idxColTitle As Integer
        If ReportData.ColTitle.ToUpper = "KEINE" Then
            idxColTitle = 0
        Else
            idxColTitle = Array.IndexOf(xlsColumns, ReportData.ColTitle) + 1
        End If
        Return idxColTitle
    End Function

End Class


''' <summary>
''' Class to store report definition data
''' </summary>
<Serializable()> Public Class ReportDefData

    Private _definitionVersion As Integer
    Private _xlsTargetPath As String
    Private _attributeSperator As String
    Private _attributePartGroup As Boolean
    Private _attributePartAttributeName As Boolean
    Private _attributePartAttribute As Boolean
    Private _xlsTargetWorkSheet As String
    Private _xlsTargetWorkbook As Byte() = Nothing
    Private _orientation As eOrientation
    ' PORTRAIT data definition
    Private _rowStart As Integer
    Private _rowStop As Integer
    Private _rowTitle As Integer
    Private _colAttributeText As String
    Private _TitlePrefixP As String
    Private _colAttributeNr As String
    Private _colAttributeUnit As String
    Private _colAttributeTolerance As String
    Private _colAttributeToleranceCol2 As String
    Private _colAttributeToleranceCol3 As String
    Private _attributeToleranceFormatP As eToleranceDisplayFormat
    Private _colDataStart As String
    Private _colDataStop As String
    Private _colDataOffset As Integer
    ' LANDSCAPE data definition
    Private _colStart As String
    Private _colStop As String
    Private _colTitle As String
    Private _rowAttributeText As Integer
    Private _TitlePrefixL As String
    Private _rowAttributeNr As Integer
    Private _rowAttributeUnit As Integer
    Private _rowAttributeTolerance As Integer
    Private _rowAttributeToleranceRow2 As Integer
    Private _rowAttributeToleranceRow3 As Integer
    Private _attributeToleranceFormatL As eToleranceDisplayFormat
    Private _rowDataStart As Integer
    Private _rowDataStop As Integer
    Private _rowDataOffset As Integer

    ''' <summary>
    ''' Enum of possible orientations
    ''' </summary>
    Public Enum eOrientation
        portrait
        landscape
    End Enum

    ''' <summary>
    ''' Enum of possible tolerance formats
    ''' </summary>
    Public Enum eToleranceDisplayFormat
        ''' <summary>
        ''' Shows tolerance in +- format
        ''' </summary>
        plusMinus
        ''' <summary>
        ''' Shows tolernace in absolut values seperated by a seperator (/ by default)
        ''' </summary>
        absolut
    End Enum

    ''' <summary>
    ''' Constructor, set supported version of definition file
    ''' </summary>
    Public Sub New()
        _definitionVersion = ReportManager.CDefinitionVersion
    End Sub

    ''' <summary>
    ''' Version of definition
    ''' </summary>
    Property DefinitionVersion() As Integer
        Get
            Return (_definitionVersion)
        End Get
        Set(ByVal value As Integer)
            _definitionVersion = value
        End Set
    End Property

    ''' <summary>
    ''' Path and filename of Excel report template
    ''' </summary>
    Property XlsTargetPath() As String
        Get
            Return (_xlsTargetPath)
        End Get
        Set(ByVal value As String)
            _xlsTargetPath = value
        End Set
    End Property

    ''' <summary>
    ''' Seperator to split read attribute description into group/attrubute name/attribute
    ''' </summary>
    Property AttributeSperator() As String
        Get
            Return (_attributeSperator)
        End Get
        Set(ByVal value As String)
            _attributeSperator = value
        End Set
    End Property

    ''' <summary>
    ''' Define if read (attribute) group will be displayed in report
    ''' </summary>
    Property AttributePartGroup() As Boolean
        Get
            Return (_attributePartGroup)
        End Get
        Set(ByVal value As Boolean)
            _attributePartGroup = value
        End Set
    End Property

    ''' <summary>
    ''' Define if read attribute name will be displayed in report
    ''' </summary>
    Property AttributePartAttributeName() As Boolean
        Get
            Return (_attributePartAttributeName)
        End Get
        Set(ByVal value As Boolean)
            _attributePartAttributeName = value
        End Set
    End Property

    ''' <summary>
    ''' Define if read attribute will be displayed in report
    ''' </summary>
    Property AttributePartAttribute() As Boolean
        Get
            Return (_attributePartAttribute)
        End Get
        Set(ByVal value As Boolean)
            _attributePartAttribute = value
        End Set
    End Property

    ''' <summary>
    ''' Excel report template
    ''' </summary>
    ''' <remarks>The content of the xls file will be stored as byte array in this property.
    ''' Its neccessary to serialize the whole definition data object
    ''' </remarks>
    Property XlsTargetWorkbook() As Byte()
        Get
            Return (_xlsTargetWorkbook)
        End Get
        Set(ByVal value As Byte())
            _xlsTargetWorkbook = value
        End Set
    End Property

    ''' <summary>
    ''' Name of target worksheet in Excel report template
    ''' </summary>
    Property XlsTargetWorkSheet() As String
        Get
            Return (_xlsTargetWorkSheet)
        End Get
        Set(ByVal value As String)
            _xlsTargetWorkSheet = value
        End Set
    End Property

    ''' <summary>
    ''' orientation of report
    ''' </summary>
    Property Orientation() As eOrientation
        Get
            Return (_orientation)
        End Get
        Set(ByVal value As eOrientation)
            _orientation = value
        End Set
    End Property


    ''' <summary>
    ''' Start row of range of data values in report
    ''' </summary>
    Property RowStart() As Integer
        Get
            Return (_rowStart)
        End Get
        Set(ByVal value As Integer)
            _rowStart = value
        End Set
    End Property

    ''' <summary>
    ''' Stop row of range of data values in report
    ''' </summary>
    Property RowStop() As Integer
        Get
            Return (_rowStop)
        End Get
        Set(ByVal value As Integer)
            _rowStop = value
        End Set
    End Property

    ''' <summary>
    ''' Row to show data headlines (partnumber) in report 
    ''' </summary>
    ''' <remarks>Columns are defined by ColDataStart and ColDataStop 
    ''' <seealso cref="ColDataStart"/> 
    ''' <seealso cref="ColDataStop"/>
    ''' </remarks>
    Property RowTitle() As Integer
        Get
            Return (_rowTitle)
        End Get
        Set(ByVal value As Integer)
            _rowTitle = value
        End Set
    End Property
    ''' <summary>
    ''' Prefix of data headlines (part numeration)
    ''' </summary>
    ''' <remarks>Range is defined by RowTitle, ColDataStart and ColDataStop 
    ''' <seealso cref="RowTitle"/> 
    ''' <seealso cref="ColDataStart"/> 
    ''' <seealso cref="ColDataStop"/>
    ''' </remarks>
    Property TitlePrefixP() As String
        Get
            Return (_TitlePrefixP)
        End Get
        Set(ByVal value As String)
            _TitlePrefixP = value
        End Set
    End Property

    ''' <summary>
    ''' Column in report to show attribute text (group/attribute name/attribute)
    ''' </summary>
    Property ColAttributeText() As String
        Get
            Return (_colAttributeText)
        End Get
        Set(ByVal value As String)
            _colAttributeText = value
        End Set
    End Property

    ''' <summary>
    ''' Column in report to show attribute index/nr
    ''' </summary>
    Property ColAttributeNr() As String
        Get
            Return (_colAttributeNr)
        End Get
        Set(ByVal value As String)
            _colAttributeNr = value
        End Set
    End Property

    ''' <summary>
    ''' Column in report to show attribute unit
    ''' </summary>
    Property ColAttributeUnit() As String
        Get
            Return (_colAttributeUnit)
        End Get
        Set(ByVal value As String)
            _colAttributeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Column in report to show attribute tolerance (range)
    ''' </summary>
    Property ColAttributeTolerance() As String
        Get
            Return (_colAttributeTolerance)
        End Get
        Set(ByVal value As String)
            _colAttributeTolerance = value
        End Set
    End Property

    ''' <summary>
    ''' Column in report to show attribute nominal value (used to define if tolerance is stored in three columns)
    ''' </summary>
    Property ColAttributeToleranceCol2() As String
        Get
            Return (_colAttributeToleranceCol2)
        End Get
        Set(ByVal value As String)
            _colAttributeToleranceCol2 = value
        End Set
    End Property

    ''' <summary>
    ''' Column in report to show upper tolerance value (used to define if tolerance is stored in three columns)
    ''' </summary>
    Property ColAttributeToleranceCol3() As String
        Get
            Return (_colAttributeToleranceCol3)
        End Get
        Set(ByVal value As String)
            _colAttributeToleranceCol3 = value
        End Set
    End Property

    ''' <summary>
    ''' Defines the displayformat of attributes tolerance
    ''' </summary>
    Property AttributeToleranceFormatP() As eToleranceDisplayFormat
        Get
            Return (_attributeToleranceFormatP)
        End Get
        Set(ByVal value As eToleranceDisplayFormat)
            _attributeToleranceFormatP = value
        End Set
    End Property

    ''' <summary>
    ''' Start column of range of data values in report
    ''' </summary>
    Property ColDataStart() As String
        Get
            Return (_colDataStart)
        End Get
        Set(ByVal value As String)
            _colDataStart = value
        End Set
    End Property

    ''' <summary>
    ''' Stop column of range of data values in report
    ''' </summary>
    Property ColDataStop() As String
        Get
            Return (_colDataStop)
        End Get
        Set(ByVal value As String)
            _colDataStop = value
        End Set
    End Property

    ''' <summary>
    ''' Column offset between two data columns
    ''' </summary>
    Property ColDataOffset() As Integer
        Get
            Return (_colDataOffset)
        End Get
        Set(ByVal value As Integer)
            _colDataOffset = value
        End Set
    End Property

    ''' <summary>
    ''' Start column of range of data values in report
    ''' </summary>
    Property ColStart() As String
        Get
            Return (_colStart)
        End Get
        Set(ByVal value As String)
            _colStart = value
        End Set
    End Property

    ''' <summary>
    ''' Stop column of range of data values in report
    ''' </summary>
    Property ColStop() As String
        Get
            Return (_colStop)
        End Get
        Set(ByVal value As String)
            _colStop = value
        End Set
    End Property

    ''' <summary>
    ''' column to show data headlines (partnumber) in report 
    ''' </summary>
    ''' <remarks>Rows are defined by RowDataStart and RowDataStop 
    ''' <seealso cref="RowDataStart"/> 
    ''' <seealso cref="RowDataStop"/>
    ''' </remarks>
    Property ColTitle() As String
        Get
            Return (_colTitle)
        End Get
        Set(ByVal value As String)
            _colTitle = value
        End Set
    End Property
    ''' <summary>
    ''' Prefix of data headlines (part numeration)
    ''' </summary>
    ''' <remarks>Range is defined by RowTitle, RowDataStart and RowDataStop 
    ''' <seealso cref="RowTitle"/> 
    ''' <seealso cref="RowDataStart"/> 
    ''' <seealso cref="RowDataStop"/>
    ''' </remarks>
    Property TitlePrefixL() As String
        Get
            Return (_TitlePrefixL)
        End Get
        Set(ByVal value As String)
            _TitlePrefixL = value
        End Set
    End Property

    ''' <summary>
    ''' Row in report to show attribute text (group/attribute name/attribute)
    ''' </summary>
    Property RowAttributeText() As Integer
        Get
            Return (_rowAttributeText)
        End Get
        Set(ByVal value As Integer)
            _rowAttributeText = value
        End Set
    End Property

    ''' <summary>
    ''' Row in report to show attribute index/nr
    ''' </summary>
    Property RowAttributeNr() As Integer
        Get
            Return (_rowAttributeNr)
        End Get
        Set(ByVal value As Integer)
            _rowAttributeNr = value
        End Set
    End Property

    ''' <summary>
    ''' Row in report to show attribute unit
    ''' </summary>
    Property RowAttributeUnit() As Integer
        Get
            Return (_rowAttributeUnit)
        End Get
        Set(ByVal value As Integer)
            _rowAttributeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Row in report to show attribute tolerance (range)
    ''' </summary>
    Property RowAttributeTolerance() As Integer
        Get
            Return (_rowAttributeTolerance)
        End Get
        Set(ByVal value As Integer)
            _rowAttributeTolerance = value
        End Set
    End Property

    ''' <summary>
    ''' Row in report to show attribute nominal value (used to define if tolerance is stored in three rows)
    ''' </summary>
    Property RowAttributeToleranceRow2() As Integer
        Get
            Return (_rowAttributeToleranceRow2)
        End Get
        Set(ByVal value As Integer)
            _rowAttributeToleranceRow2 = value
        End Set
    End Property

    ''' <summary>
    ''' Row in report to show upper tolerance value (used to define if tolerance is stored in three rows)
    ''' </summary>
    Property RowAttributeToleranceRow3() As Integer
        Get
            Return (_rowAttributeToleranceRow3)
        End Get
        Set(ByVal value As Integer)
            _rowAttributeToleranceRow3 = value
        End Set
    End Property

    ''' <summary>
    ''' Defines the displayformat of attributes tolerance
    ''' </summary>
    Property AttributeToleranceFormatL() As eToleranceDisplayFormat
        Get
            Return (_attributeToleranceFormatL)
        End Get
        Set(ByVal value As eToleranceDisplayFormat)
            _attributeToleranceFormatL = value
        End Set
    End Property

    ''' <summary>
    ''' Start row of range of data values in report
    ''' </summary>
    Property RowDataStart() As Integer
        Get
            Return (_rowDataStart)
        End Get
        Set(ByVal value As Integer)
            _rowDataStart = value
        End Set
    End Property

    ''' <summary>
    ''' Stop row of range of data values in report
    ''' </summary>
    Property RowDataStop() As Integer
        Get
            Return (_rowDataStop)
        End Get
        Set(ByVal value As Integer)
            _rowDataStop = value
        End Set
    End Property

    ''' <summary>
    ''' Row offset between two data columns
    ''' </summary>
    Property RowDataOffset() As Integer
        Get
            Return (_rowDataOffset)
        End Get
        Set(ByVal value As Integer)
            _rowDataOffset = value
        End Set
    End Property


End Class

''' <summary>
''' Class to store target (report) sheet names and the attribute index range per sheet
''' </summary>
Public Class AttributeDataTarget

    Private _targetSheetName As String
    Private _attributeNameFrom As Integer
    Private _attributeNameTo As Integer

    ''' <summary>
    ''' Constructor, currently not used
    ''' </summary>
    Public Sub New()
        ' ....

    End Sub

    ''' <summary>
    ''' Name of the traget sheet in report
    ''' </summary>
    Public Property TargetSheetName() As String
        Get
            Return _targetSheetName
        End Get
        Set(ByVal value As String)
            _targetSheetName = value
        End Set
    End Property

    ''' <summary>
    ''' attribute start index/nr on current sheet
    ''' </summary>
    Public Property AttributeNameFrom() As Integer
        Get
            Return _attributeNameFrom
        End Get
        Set(ByVal value As Integer)
            _attributeNameFrom = value
        End Set
    End Property

    ''' <summary>
    ''' attribute stop index/nr on current sheet
    ''' </summary>
    Public Property AttributeNameTo() As Integer
        Get
            Return _attributeNameTo
        End Get
        Set(ByVal value As Integer)
            _attributeNameTo = value
        End Set
    End Property

End Class
