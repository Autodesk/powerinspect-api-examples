' **********************************************************************
' *         © COPYRIGHT 2019 Autodesk, Inc.All Rights Reserved         *
' *                                                                    *
' *  Use of this software is subject to the terms of the Autodesk      *
' *  license agreement provided at the time of installation            *
' *  or download, or which otherwise accompanies this software         *
' *  in either electronic or hard copy form.                           *
' **********************************************************************

Imports System.Windows.Forms
Imports System.Globalization
Imports PowerINSPECT
Imports pwiCOMGeometry
Imports PWIMATHBOXLib

Public Class FrmMain
    Private WithEvents mPi As PowerINSPECT.Application
    Private WithEvents mPiDoc As Document

    ''' <summary>
    ''' supported PI version
    ''' </summary>
    Public Const CpidiSupportedMajorVersion As Integer = 17
    Public Const CpidiSupportedMinorVersion As Integer = 1

    Public Const CHelpFileName As String = "PI DocModifier.pdf"


#Region "PI Manager"
    ''' <summary>
    ''' Check if PI is already running, if yes use this PI, if not start PI and use started PI to do setup document, check version too
    ''' </summary>
    ''' <returns>True if PI is running and BLS app has a reference to PI</returns>
    Public Function PiStart() As Boolean
        ' Connect to PowerINSPECT
        Try

            mPi = New PowerINSPECT.Application()
            If Not mPi.MajorVersion = CpidiSupportedMajorVersion Or Not mPi.MinorVersion = CpidiSupportedMinorVersion Then
                MessageBox.Show(String.Format("Die Applikation unterstützt die PI-Version {0}.{1} nicht." & vbNewLine & _
                                              "Die Applikation benötigt die Version {2}.{3}", _
                                              mPi.MajorVersion.ToString, mPi.MinorVersion.ToString, CpidiSupportedMajorVersion.ToString, CpidiSupportedMinorVersion.ToString), _
                                              "Fehler:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PiEnd()
                Return False
            End If
            mPi.Visible = True

        Catch ex As Exception
            PiEnd()
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' open given PI document (CP), with measured values
    ''' </summary>
    ''' <param name="piDocName">Name of PWI file to be opened</param>
    ''' <returns>True if successful</returns>
    Public Function PiDocumentOpen(ByVal piDocName As String) As Boolean
        Dim retVal As Boolean = False

        Try
            mPi.Documents.Open(piDocName, True, True, False)
            retVal = True
        Catch
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' close all open documents in PI
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PiDocumentsClose()
        If mPi.Documents.Count > 0 Then
            Try
                mPi.Documents.Close()
            Catch
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Reset varible the active document is stored in
    ''' </summary>
    Private Sub piDocumentClose(ByVal pdoc As Document) Handles mPi.DocumentClose
        Try
            mPiDoc = Nothing
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' set variable to store current active document to currently loaded doc
    ''' </summary>
    ''' <param name="doc">Name of the currently opened document</param>
    ''' <remarks></remarks>
    Private Sub piDocumentOpen(ByVal doc As Document) Handles mPi.DocumentOpen
        Try
            mPiDoc = Nothing
            mPiDoc = doc
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' close PowerINSPECT 
    ''' </summary>
    Public Sub PiEnd()
        Try
            mPi.Documents.Close()
            mPi.Quit()
        Catch
        End Try
        If Not mPiDoc Is Nothing Then mPiDoc = Nothing
        If Not mPi Is Nothing Then mPi = Nothing
    End Sub

    ''' <summary>
    ''' Save PI document as PWI document and closes it after saving
    ''' </summary>
    ''' <param name="aFilename">Name of the PWI document</param>
    Public Sub PiDocumentSaveAndClose(ByVal aFilename As String)
        If (mPi.Documents.Count >= 1) And ((Not mPi.ActiveDocument Is Nothing)) Then
            mPi.ActiveDocument.SaveAs(aFilename, False)
            mPi.ActiveDocument.Close()
        Else
            MsgBox("Kein Messdokument. Daten können nicht gespeichert werden.", vbOKOnly + vbExclamation, "Speichern nicht möglich")
        End If
    End Sub

    Private Sub piNewDocument(ByVal doc As Document) Handles mPi.NewDocument
        mPiDoc = Nothing
        mPiDoc = doc
    End Sub

#End Region

    Private Sub frmMain_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        PiStart()

        ' page single point
        chkChangeNameSinglePoint.Checked = False
        txtChangeNameSinglePoint.Enabled = False
        txtChangeNameSinglePoint.Text = String.Empty

        chkTargetPoint.Checked = False
        chkTargetPointX.Enabled = chkTargetPoint.Checked
        chkTargetPointY.Enabled = chkTargetPoint.Checked
        chkTargetPointZ.Enabled = chkTargetPoint.Checked
        If Not chkTargetPoint.Checked Then
            chkTargetPointX.Checked = False
            chkTargetPointY.Checked = False
            chkTargetPointZ.Checked = False
        End If
        chkVectorInvert.Checked = False
        txtThickness.Text = String.Empty

        ' page circle
        chkChangeNameCircle.Checked = False
        txtChangeNameCircle.Enabled = False
        txtChangeNameCircle.Text = String.Empty

        chkCenter.Checked = False
        chkCenterX.Enabled = chkCenter.Checked
        chkCenterY.Enabled = chkCenter.Checked
        chkCenterZ.Enabled = chkCenter.Checked
        If Not chkCenter.Checked Then
            chkCenterX.Checked = False
            chkCenterY.Checked = False
            chkCenterZ.Checked = False
        End If

        chkFilltingAlgo.Enabled = True
        chkDiameter.Enabled = True
        chkOffset.Enabled = True
        chkCircularity.Enabled = True
        rbDiameter.Checked = True

        ' page circle compound
        chkChangeNameCircleCompound.Checked = False
        txtChangeNameCircleCompound.Enabled = False
        txtChangeNameCircleCompound.Text = String.Empty

        chkCenterCircleCompound.Checked = False
        chkCenterXCircleCompound.Enabled = chkCenterCircleCompound.Checked
        chkCenterYCircleCompound.Enabled = chkCenterCircleCompound.Checked
        chkCenterZCircleCompound.Enabled = chkCenterCircleCompound.Checked
        If Not chkCenterCircleCompound.Checked Then
            chkCenterXCircleCompound.Checked = False
            chkCenterYCircleCompound.Checked = False
            chkCenterZCircleCompound.Checked = False
        End If
        chkFilltingAlgoCircleCpompound.Enabled = True
        chkDiameterCircleCompound.Enabled = True
        chkCircularityCircleCompound.Enabled = True
        rbDiameterCircleCompound.Checked = True
        chkNominalVectorCircleCompound.Checked = False
        chkNominalVectorICircleCompound.Enabled = chkNominalVectorCircleCompound.Checked
        chkNominalVectorJCircleCompound.Enabled = chkNominalVectorCircleCompound.Checked
        chkNominalVectorKCircleCompound.Enabled = chkNominalVectorCircleCompound.Checked
        If Not chkNominalVectorCircleCompound.Checked Then
            chkNominalVectorICircleCompound.Checked = False
            chkNominalVectorJCircleCompound.Checked = False
            chkNominalVectorKCircleCompound.Checked = False
        End If

        pbModifying.Value = 0
    End Sub

    Private Sub chkChangeNameCircle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChangeNameCircle.CheckedChanged
        If chkChangeNameCircle.Checked Then
            txtChangeNameCircle.Enabled = True
        Else
            txtChangeNameCircle.Enabled = False
            txtChangeNameCircle.Text = String.Empty
        End If
    End Sub

    Private Sub chkCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCenter.CheckedChanged
        chkCenterX.Enabled = chkCenter.Checked
        chkCenterY.Enabled = chkCenter.Checked
        chkCenterZ.Enabled = chkCenter.Checked

        chkCenterX.Checked = chkCenter.Checked
        chkCenterY.Checked = chkCenter.Checked
        chkCenterZ.Checked = chkCenter.Checked
    End Sub

    Private Sub chkChangeNameSinglePoint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChangeNameSinglePoint.CheckedChanged
        If chkChangeNameSinglePoint.Checked Then
            txtChangeNameSinglePoint.Enabled = True
        Else
            txtChangeNameSinglePoint.Enabled = False
            txtChangeNameSinglePoint.Text = String.Empty
        End If
    End Sub

    Private Sub chkTargetPoint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetPoint.CheckedChanged
        chkTargetPointX.Enabled = chkTargetPoint.Checked
        chkTargetPointY.Enabled = chkTargetPoint.Checked
        chkTargetPointZ.Enabled = chkTargetPoint.Checked

        chkTargetPointX.Checked = chkTargetPoint.Checked
        chkTargetPointY.Checked = chkTargetPoint.Checked
        chkTargetPointZ.Checked = chkTargetPoint.Checked
    End Sub


    ''' <summary>
    ''' Update parameters of single/point circle dependent on current settings
    ''' </summary>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim aPiGrp As GeometricGroup
        mPiDoc = mPi.ActiveDocument
        Try
            If Not mPiDoc Is Nothing Then
                If mPiDoc.ActiveItem Is Nothing Then
                    ' no item selected => nothing to do
                ElseIf (mPiDoc.ActiveGroup Is Nothing) And (mPiDoc.ActiveItem.ItemType = PWI_EntityItemType.pwi_grp_GeometricGroup_) Then
                    ' loop through all items of active group and modify know item types
                    Dim oldCursor As Cursor = Cursor.Current
                    Try
                        Windows.Forms.Application.UseWaitCursor = True
                        Cursor.Current = Cursors.WaitCursor
                        Dim indexSp As Integer = 1
                        Dim indexCircle As Integer = 1
                        Dim indexCircleCompound As Integer = 1
                        mPiDoc.ActiveGroup = mPiDoc.ActiveItem
                        aPiGrp = mPiDoc.ActiveGroup

                        pbModifying.Maximum = aPiGrp.SequenceItems.Count
                        For indexItem As Integer = 1 To aPiGrp.SequenceItems.Count

                            If tcChangeItems.SelectedTab Is tpSinglePoint Then
                                ' change items of single point
                                If Not modRetracePoint(aPiGrp, indexItem, indexSp) Then
                                    Exit For
                                End If
                            ElseIf tcChangeItems.SelectedTab Is tpCircle Then
                                ' change items of circle
                                modCircle(aPiGrp, indexItem, indexCircle)
                            ElseIf tcChangeItems.SelectedTab Is tpCircleCompound Then
                                ' change items of compund circle
                                modCircleCompound(aPiGrp, indexItem, indexCircleCompound)
                            End If

                            pbModifying.Value = indexItem
                            pbModifying.Invalidate()
                        Next
                        mPiDoc.ActiveGroup = Nothing
                    Finally
                        Windows.Forms.Application.UseWaitCursor = False
                        Cursor.Current = oldCursor
                    End Try
                Else
                    ' create dummy counter, not used if one active item selected
                    Dim dmyIndex As Integer = -1
                    ' modify active item
                    If tcChangeItems.SelectedTab Is tpSinglePoint Then
                        ' change items of single point
                        modRetracePoint(Nothing, dmyIndex, dmyIndex)
                    ElseIf tcChangeItems.SelectedTab Is tpCircle Then
                        ' change items of circle
                        modCircle(Nothing, dmyIndex, dmyIndex)
                    ElseIf tcChangeItems.SelectedTab Is tpCircleCompound Then
                        ' change items of compund circle
                        modCircleCompound(Nothing, dmyIndex, dmyIndex)
                    End If
                End If
            End If
        Catch

        Finally
            pbModifying.Value = 0
        End Try
        ' if enter is pressed (no mouse click on OK), select complete text of textbox or value entered in numericupdown, 
        ' to be able to change text/value immediately (without selecting text with mouse to sellect the text to overwrite) 
        If Me.ActiveControl.GetType Is GetType(TextBox) Then
            CType(Me.ActiveControl, TextBox).SelectAll()
        End If
    End Sub

    ''' <summary>
    ''' Modifiy items of type retrace point (single point)
    ''' </summary>
    ''' <param name="aPiGrp">Group which contains the items</param>
    ''' <param name="indexItem">Index of current item</param>
    ''' <param name="indexSp">Index of current single/retrace point</param>
    ''' <returns>True if successful</returns>
    Private Function modRetracePoint(ByRef aPiGrp As GeometricGroup, ByVal indexItem As Integer, ByRef indexSp As Integer) As Boolean
        Try
            Dim aSp As Point_RetracePoint
            If (aPiGrp Is Nothing) Or (mPiDoc.ActiveItem.ItemType <> PWI_EntityItemType.pwi_grp_GeometricGroup_) Then
                ' get single point of ActiveItem
                aSp = mPiDoc.ActiveItem
            Else
                ' get single point of current group
                aSp = aPiGrp.SequenceItems(indexItem)
            End If

            If Not txtThickness.Text.Trim() = String.Empty Then
                ' get thickness value
                Dim curNF As NumberFormatInfo = System.Windows.Forms.Application.CurrentCulture.NumberFormat
                Dim aThickness As Double
                Dim aThicknessStr As String = txtThickness.Text.Trim()
                Try
                    aThickness = Double.Parse(aThicknessStr, curNF)
                Catch ex As Exception
                    ' Let the user know what went wrong.
                    MsgBox("Fehlerhafte Eingabe in Feld Materialstärke.", vbExclamation + vbOKOnly, "Fehlerhafte Eingabe")
                    Return False
                End Try

                Try
                    ' save thickness as nominal value
                    aSp.PropertyDistanceToTargetPlane.Nominal = aThickness
                Catch ex As Exception
                    ' Let the user know what went wrong.
                    MsgBox("Fehler bei Verrechnung Materialstärke.", vbExclamation + vbOKOnly, "Fehler")
                    Return False
                End Try
            End If

            ' invert vector
            If chkVectorInvert.Checked Then
                If aSp.ParameterCompensationMethod.CompensationMethod = PWI_ProbeCompenstionMethod.pcm_ALONG_A_EXPLICIT_DIRECTION Then
                    If aSp.ParameterCompensationMethod.CompensationDir.CoordinateType.CoordType = geomVector3DCoordinateType.geomv3d_IJK Then
                        ' invert compensation vector
                        Dim aNominalVec As Vector3D = New Vector3D()
                        aNominalVec.I = aSp.ParameterCompensationMethod.CompensationDir.Vector.I
                        aNominalVec.J = aSp.ParameterCompensationMethod.CompensationDir.Vector.J
                        aNominalVec.K = aSp.ParameterCompensationMethod.CompensationDir.Vector.K
                        aNominalVec.Unify()
                        aNominalVec.Scale(-1)
                        aSp.ParameterCompensationMethod.CompensationDir.Vector = aNominalVec

                        aSp.CNCProbePathParameters.ReverseDirectionVector()
                        aSp.CNCProbePathParameters.RefreshCNCProbePath()
                    End If
                End If
            End If

            ' report settings
            If chkTargetPoint.Checked Then
                aSp.PropertyTargetPoint.OutputCoordToReport(0) = chkTargetPointX.Checked
                aSp.PropertyTargetPoint.OutputCoordToReport(1) = chkTargetPointY.Checked
                aSp.PropertyTargetPoint.OutputCoordToReport(2) = chkTargetPointZ.Checked
            Else
                aSp.PropertyTargetPoint.OutputCoordToReport(0) = False
                aSp.PropertyTargetPoint.OutputCoordToReport(1) = False
                aSp.PropertyTargetPoint.OutputCoordToReport(2) = False
            End If
            aSp.PropertyTargetPoint.OutputToReport = chkTargetPoint.Checked

            aSp.PropertyDistanceToTargetPlane.OutputToReport = chkDistanceTargetplane.Checked
            aSp.PropertyDistanceToTarget.OutputToReport = chkDistanceTargetPoint.Checked
            aSp.PropertyDistanceOnPlane.OutputToReport = chkDistancePlane.Checked

            ' rename item
            If chkChangeNameSinglePoint.Checked Then
                If indexSp > 0 Then
                    ' rename in a loop (of current sequence item group) => use index as suffix to name
                    aSp.Name = txtChangeNameSinglePoint.Text & indexSp.ToString()
                    indexSp += 1
                Else
                    ' just one item selected => rename without index as suffix
                    aSp.Name = txtChangeNameSinglePoint.Text
                End If
            End If
            aSp.Refresh()
        Catch ex As Exception
        End Try
        Return True
    End Function


    ''' <summary>
    ''' Modifiy items of type circle
    ''' </summary>
    ''' <param name="aPiGrp">Group which contains the items</param>
    ''' <param name="indexItem">Index of current item</param>
    ''' <param name="indexCircle">Index of current circle</param>
    Private Sub modCircle(ByRef aPiGrp As GeometricGroup, ByVal indexItem As Integer, ByRef indexCircle As Integer)

        Try
            Dim aCircle As Feat_ProbedCircle
            If (aPiGrp Is Nothing) Or (mPiDoc.ActiveItem.ItemType <> PWI_EntityItemType.pwi_grp_GeometricGroup_) Then
                ' get circle of ActiveItem
                aCircle = mPiDoc.ActiveItem
            Else
                ' get circle of current group
                aCircle = aPiGrp.SequenceItems(indexItem)
            End If

            ' report settings
            If chkCenter.Checked Then
                aCircle.PropertyCentre.OutputCoordToReport(0) = chkCenterX.Checked
                aCircle.PropertyCentre.OutputCoordToReport(1) = chkCenterY.Checked
                aCircle.PropertyCentre.OutputCoordToReport(2) = chkCenterZ.Checked
            Else
                aCircle.PropertyCentre.OutputCoordToReport(0) = False
                aCircle.PropertyCentre.OutputCoordToReport(1) = False
                aCircle.PropertyCentre.OutputCoordToReport(2) = False
            End If
            aCircle.PropertyCentre.OutputToReport = chkCenter.Checked

            aCircle.ParameterFittingAlgorithm.OutputToReport = chkFilltingAlgo.Checked
            aCircle.PropertyOffset.OutputToReport = chkOffset.Checked
            aCircle.PropertyCircularity.OutputToReport = chkCircularity.Checked
            aCircle.PropertyRadius.OutputToReport = chkDiameter.Checked
            If rbDiameter.Checked Then
                aCircle.PropertyRadius.RadialDimensionType = PWI_RadialDimensionType.pwi_Diameter
            Else
                aCircle.PropertyRadius.RadialDimensionType = PWI_RadialDimensionType.pwi_Radius
            End If

            ' rename item
            If chkChangeNameCircle.Checked Then
                If indexCircle > 0 Then
                    ' rename in a loop (of current sequence item group) => use index as suffix to name
                    aCircle.Name = txtChangeNameCircle.Text & indexCircle.ToString()
                    indexCircle += 1
                Else
                    ' just one item selected => rename without index as suffix
                    aCircle.Name = txtChangeNameCircle.Text
                End If
            End If
            aCircle.Refresh()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Modifiy items of type circle compound
    ''' </summary>
    ''' <param name="aPiGrp">Group which contains the items</param>
    ''' <param name="indexItem">Index of current item</param>
    ''' <param name="indexCircleCompound">Index of current circle</param>
    Private Sub modCircleCompound(ByRef aPiGrp As GeometricGroup, ByVal indexItem As Integer, ByRef indexCircleCompound As Integer)

        Try
            Dim aCircleCompound As Compound_Circle
            If (aPiGrp Is Nothing) Or (mPiDoc.ActiveItem.ItemType <> PWI_EntityItemType.pwi_grp_GeometricGroup_) Then
                ' get circle of ActiveItem
                aCircleCompound = mPiDoc.ActiveItem
            Else
                ' get compund circle of current group
                aCircleCompound = aPiGrp.SequenceItems(indexItem)
            End If

            ' report settings
            If chkCenterCircleCompound.Checked Then
                aCircleCompound.PropertyCentre.OutputCoordToReport(0) = chkCenterXCircleCompound.Checked
                aCircleCompound.PropertyCentre.OutputCoordToReport(1) = chkCenterYCircleCompound.Checked
                aCircleCompound.PropertyCentre.OutputCoordToReport(2) = chkCenterZCircleCompound.Checked
            Else
                aCircleCompound.PropertyCentre.OutputCoordToReport(0) = False
                aCircleCompound.PropertyCentre.OutputCoordToReport(1) = False
                aCircleCompound.PropertyCentre.OutputCoordToReport(2) = False
            End If
            aCircleCompound.PropertyCentre.OutputToReport = chkCenterCircleCompound.Checked

            aCircleCompound.ParameterFittingAlgorithm.OutputToReport = chkFilltingAlgoCircleCpompound.Checked
            aCircleCompound.PropertyCircularity.OutputToReport = chkCircularityCircleCompound.Checked
            aCircleCompound.PropertyRadius.OutputToReport = chkDiameterCircleCompound.Checked
            If rbDiameterCircleCompound.Checked Then
                aCircleCompound.PropertyRadius.RadialDimensionType = PWI_RadialDimensionType.pwi_Diameter
            Else
                aCircleCompound.PropertyRadius.RadialDimensionType = PWI_RadialDimensionType.pwi_Radius
            End If
            If chkNominalVectorCircleCompound.Checked Then
                aCircleCompound.PropertyNormalVector.OutputCoordToReport(0) = chkNominalVectorICircleCompound.Checked
                aCircleCompound.PropertyNormalVector.OutputCoordToReport(1) = chkNominalVectorJCircleCompound.Checked
                aCircleCompound.PropertyNormalVector.OutputCoordToReport(2) = chkNominalVectorKCircleCompound.Checked
            Else
                aCircleCompound.PropertyNormalVector.OutputCoordToReport(0) = False
                aCircleCompound.PropertyNormalVector.OutputCoordToReport(1) = False
                aCircleCompound.PropertyNormalVector.OutputCoordToReport(2) = False
            End If
            aCircleCompound.PropertyNormalVector.OutputToReport = chkNominalVectorCircleCompound.Checked

            ' rename item
            If chkChangeNameCircleCompound.Checked Then
                If indexCircleCompound > 0 Then
                    ' rename in a loop (of current sequence item group) => use index as suffix to name
                    aCircleCompound.Name = txtChangeNameCircleCompound.Text & indexCircleCompound.ToString()
                    indexCircleCompound += 1
                Else
                    ' just one item selected => rename without index as suffix
                    aCircleCompound.Name = txtChangeNameCircleCompound.Text
                End If
            End If
            aCircleCompound.Refresh()
        Catch
        End Try
    End Sub
    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        Dim aAboutBox As FrmAboutBox = New FrmAboutBox
        aAboutBox.ShowDialog()
        aAboutBox.Dispose()
    End Sub

    Private Sub chkChangeNameCircleCompound_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChangeNameCircleCompound.CheckedChanged
        If chkChangeNameCircleCompound.Checked Then
            txtChangeNameCircleCompound.Enabled = True
        Else
            txtChangeNameCircleCompound.Enabled = False
            txtChangeNameCircleCompound.Text = String.Empty
        End If
    End Sub

    Private Sub chkCenterCircleCompound_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCenterCircleCompound.CheckedChanged
        chkCenterXCircleCompound.Enabled = chkCenterCircleCompound.Checked
        chkCenterYCircleCompound.Enabled = chkCenterCircleCompound.Checked
        chkCenterZCircleCompound.Enabled = chkCenterCircleCompound.Checked

        chkCenterXCircleCompound.Checked = chkCenterCircleCompound.Checked
        chkCenterYCircleCompound.Checked = chkCenterCircleCompound.Checked
        chkCenterZCircleCompound.Checked = chkCenterCircleCompound.Checked
    End Sub

    Private Sub chkNominalVectorCircleCompound_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNominalVectorCircleCompound.CheckedChanged
        chkNominalVectorICircleCompound.Enabled = chkNominalVectorCircleCompound.Checked
        chkNominalVectorJCircleCompound.Enabled = chkNominalVectorCircleCompound.Checked
        chkNominalVectorKCircleCompound.Enabled = chkNominalVectorCircleCompound.Checked

        chkNominalVectorICircleCompound.Checked = chkNominalVectorCircleCompound.Checked
        chkNominalVectorJCircleCompound.Checked = chkNominalVectorCircleCompound.Checked
        chkNominalVectorKCircleCompound.Checked = chkNominalVectorCircleCompound.Checked
    End Sub

    Private Sub FrmMain_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Dim path As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        System.Diagnostics.Process.Start(path + System.IO.Path.DirectorySeparatorChar + CHelpFileName)
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get our process name.
        Dim myName As String = Process.GetCurrentProcess.ProcessName
        ' Get information about processes with this name.
        Dim procs() As Process = Process.GetProcessesByName(myName)
        ' If there is only one, it's us.
        If procs.Length > 1 Then
            Me.Close()
        End If
    End Sub

    Private Sub txt_Enter(sender As Object, e As EventArgs) Handles txtChangeNameCircleCompound.Enter, txtThickness.Enter, txtChangeNameSinglePoint.Enter, txtChangeNameCircle.Enter
        sender.SelectAll()
    End Sub

End Class