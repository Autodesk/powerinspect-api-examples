' **********************************************************************
' *         © COPYRIGHT 2019 Autodesk, Inc.All Rights Reserved         *
' *                                                                    *
' *  Use of this software is subject to the terms of the Autodesk      *
' *  license agreement provided at the time of installation            *
' *  or download, or which otherwise accompanies this software         *
' *  in either electronic or hard copy form.                           *
' **********************************************************************

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
    Private Const CpidiSupportedMajorVersion As Integer = 17
    Private Const CpidiSupportedMinorVersion As Integer = 1

    Private Const CHelpFileName As String = "PI CopyTransform.pdf"

    Private mPointSource As Feature


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

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Get our process name.
        Dim myName As String = Process.GetCurrentProcess.ProcessName
        ' Get information about processes with this name.
        Dim procs() As Process = Process.GetProcessesByName(myName)
        ' If there is only one, it's us.
        If procs.Length > 1 Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' HINT vorerst Funktionalität Spiegeln ausblenden
        tcTransformation.TabPages.Remove(tpMirror)

        PiStart()

        tcTransformation.SelectedTab = tpCopy

        nudCopyCount.Value = 1
        txtCopyNamePrefix.Text = String.Empty

        chkCopyUsePointSources.Checked = False

        txtTranslationX.Text = 0
        txtTranslationY.Text = 0
        txtTranslationZ.Text = 0

        cboRotationAxis.SelectedIndex = 0
        txtRotationAngle.Text = 0

        cboMirrorPlane.SelectedIndex = 0
        txtMirrorCoordinate.Text = 0

        pbModifying.Value = 0
        lblCopyInfo.Text = String.Empty
    End Sub

    Private Sub nudCopyCount_ValueChanged(sender As Object, e As EventArgs) Handles nudCopyCount.ValueChanged
        If nudCopyCount.Value > 1 Then
            lblCopyInfo.Text = "Bei mehr als einer Kopie wird PI während des Kopiervorganges ausgeblendet !"
        Else
            lblCopyInfo.Text = String.Empty
        End If
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Dim aAboutBox As FrmAboutBox = New FrmAboutBox
        aAboutBox.ShowDialog()
        aAboutBox.Dispose()
    End Sub

    Private Sub FrmMain_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Dim path As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        System.Diagnostics.Process.Start(path + System.IO.Path.DirectorySeparatorChar + CHelpFileName)
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        itemModify()
        ' if enter is pressed (no mouse click on OK), select complete text of textbox or value entered in numericupdown, 
        ' to be able to change text/value immediately (without selecting text with mouse to sellect the text to overwrite) 
        If Me.ActiveControl.GetType Is GetType(TextBox) Then
            CType(Me.ActiveControl, TextBox).SelectAll()
        ElseIf Me.ActiveControl.GetType Is GetType(NumericUpDown) Then
            CType(Me.ActiveControl, NumericUpDown).Select(0, 5)
        End If
    End Sub


    ''' <summary>
    ''' Modify (copy or transform) selected item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub itemModify()
        mPiDoc = mPi.ActiveDocument
        If Not (mPiDoc Is Nothing) AndAlso Not (mPiDoc.ActiveItem Is Nothing) Then
            Dim oldCursor As Cursor = Cursor.Current
            Try
                Windows.Forms.Application.UseWaitCursor = True
                Cursor.Current = Cursors.WaitCursor
                Dim resetGroup = False
                If (mPiDoc.ActiveGroup Is Nothing) AndAlso Not (mPiDoc.ActiveItem.Group Is Nothing) Then
                    ' activate group
                    mPiDoc.ActiveGroup = mPiDoc.ActiveItem.Group
                    ' remenber that we've to reset active group after copy/transform
                    resetGroup = True
                End If

                If (mPiDoc.ActiveGroup Is Nothing) Then
                    ' no group activated (no item embedded in a group)
                    MsgBox("Elmente auf Definitionsebene können nicht kopiert/transformiert werden." & vbCrLf & _
                           "Aktivieren sie ein Element auf der Merkmalsebene.", vbOKOnly + vbInformation, "Definitionsebene")
                Else
                    pbModifying.Value = 0
                    pbModifying.Maximum = nudCopyCount.Value

                    If nudCopyCount.Value > 1 Then
                        mPi.Visible = False
                    End If
                    Try
                        mPointSource = Nothing
                        For i As Integer = 0 To nudCopyCount.Value - 1
                            pbModifying.Value = i

                            If Not mPiDoc Is Nothing Then
                                If mPiDoc.ActiveItem Is Nothing Then
                                    ' no item selected => nothing to do
                                Else
                                    Dim seqItem As SequenceItem = mPiDoc.ActiveItem
                                    ' modify active item
                                    If seqItem Is Nothing Then
                                        ' nothing to do here !!




                                        '-----------------------------------------------------
                                        ' PLANES
                                        '-----------------------------------------------------
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Plane_Probed_ Then
                                        ' change items of probed plane
                                        modifyProbedPlane()
                                        ' HINT aktivieren? ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Plane_ParallelThroughRefPoint_ Then


                                        '-----------------------------------------------------
                                        ' LINES
                                        '-----------------------------------------------------
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Line_Probed_ Then
                                        ' change items of probed line
                                        modifyProbedLine()
                                        ' HINT aktivieren? ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Line_PerpendicularTroughPoint_ Then



                                        '-----------------------------------------------------
                                        ' POINTS
                                        '-----------------------------------------------------
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Point_RetracePoint_ Then
                                        ' change items of single point
                                        modifyRetracePoint()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_SinglePointCircle_ Then
                                        ' change items of single point circle
                                        modifySinglePointCircle()



                                        '-----------------------------------------------------
                                        ' FEATURES
                                        '-----------------------------------------------------
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_ProbedCircle_ Then
                                        ' change items of probed circle
                                        modifyCircle()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_ProbedEllipse_ Then
                                        ' change items of probed ellipse
                                        modifyEllipse()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Slot_ Then
                                        ' change items of slot
                                        modifySlot()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Rectangle_ Then
                                        ' change items of rectangle
                                        modifyRectangle()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Polygone_ Then
                                        ' change items of polygone
                                        modifyPolygone()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Cylinder_ Then
                                        ' change items of ?
                                        modifyCylinder()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Cone_ Then
                                        ' change items of ?
                                        modifyCone()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Sphere_ Then
                                        ' change items of ?
                                        modifySphere()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Torus_ Then
                                        ' change items of ?
                                        modifyTorus()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_CircleThroughNPoints_ Then
                                        ' change items of ?
                                        modifyCircleNpts()





                                        '-----------------------------------------------------
                                        ' COMPOUND
                                        '-----------------------------------------------------
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Compound_Circle_ Then
                                        ' change items of compound circle
                                        modifyCircleCompound()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Compound_Ellipse_ Then
                                        ' change items of compound ellipse
                                        modifyEllipseCompound()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Compound_Rectangle_ Then
                                        ' change items of compound rectangle
                                        modifyRectangleCompound()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Compound_RoundedSlot_ Then
                                        ' change items of compound slot
                                        modifySlotCompound()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Compound_Polygon_ Then
                                        ' change items of compound polygon
                                        modifyPolygonCompound()
                                    ElseIf seqItem.ItemType = PWI_EntityItemType.pwi_ent_Compound_Line_ Then
                                        ' change items of compound line
                                        modifyLineCompound()
                                        ' HINT weitere Entities aktivieren? ElseIf seqItem.ItemType = PWI_EntityItemType. 
                                    Else
                                        MsgBox("Nicht unterstützter Merkmalstyp: " & seqItem.ItemType.ToString(), vbOKOnly + vbExclamation, "Kopieren/Transformieren nicht möglich")
                                    End If
                                End If
                            End If
                        Next
                    Catch

                    Finally

                    End Try
                End If
                If resetGroup Then
                    mPiDoc.ActiveGroup = Nothing
                End If
            Finally
                If Not mPi Is Nothing Then
                    If Not mPi.Visible Then
                        mPi.Visible = True
                    End If
                End If
                Windows.Forms.Application.UseWaitCursor = False
                Cursor.Current = oldCursor
                pbModifying.Value = 0
            End Try
        End If
    End Sub

    ''' <summary>
    ''' tries to convert string into double
    ''' </summary>
    ''' <param name="strVal">string value to be converted in double</param>
    ''' <param name="strName">name of string value</param>
    ''' <returns>converted value</returns>
    Private Function getValOfString(ByVal strVal As String, ByVal strName As String) As Double
        Dim dblVal As Double = 0.0
        If Not strVal.Trim() = String.Empty Then
            ' get thickness value
            Dim curNF As NumberFormatInfo = System.Windows.Forms.Application.CurrentCulture.NumberFormat
            Try
                dblVal = Double.Parse(strVal.Trim(), curNF)
                Debug.Print(dblVal.ToString())
            Catch ex As Exception
                ' Let the user know what went wrong.
                MsgBox("Fehlerhafte Eingabe in Feld: " & strName, vbExclamation + vbOKOnly, "Fehlerhafte Eingabe")
            End Try
        End If
        Return dblVal
    End Function

    ''' <summary>
    ''' Get name of sequence item (if defined in dialog, add defined prefix to name)
    ''' </summary>
    ''' <param name="seqName">Name of sequence item (automatically created by PI)</param>
    ''' <returns>Name to be used in added item</returns>
    Private Function GetSeqItemName(ByVal seqName As String) As String
        If txtCopyNamePrefix.Text.Trim() <> String.Empty Then
            If nudCopyCount.Value > 1 Then
                ' if we want to copy more than one, we've to add seq nr to prefix
                ' extract seq nr from name 
                Dim parts As String() = seqName.Split(" ")
                If parts.Length > 1 Then
                    ' concat prefix and seq nr to new name of item
                    seqName = txtCopyNamePrefix.Text.Trim() & " " & parts(parts.Length - 1).Trim()
                End If
            Else
                ' if we want to copy the item just once, give them the name the user has entered in prefix textbox
                seqName = txtCopyNamePrefix.Text.Trim()
            End If
        End If
        Return seqName
    End Function


    ''' <summary>
    ''' Rotate point about the specified axis vector and angle
    ''' </summary>
    ''' <param name="aAngle">The angle in degree to rotate</param>
    ''' <param name="rotPoint">coordinates of point to be rotated</param>
    ''' <returns>Coordinate of rotated point</returns>
    Private Function RotateAboutAxis(ByVal aAngle As Double, ByVal rotPoint As Point3D) As Point3D
        Dim axis As Vector3D = New Vector3D()
        Select Case cboRotationAxis.SelectedIndex
            Case 0
                ' X axis
                axis.SetCoordinates(1, 0, 0)
            Case 1
                ' Y axis
                axis.SetCoordinates(0, 1, 0)
            Case 2
                ' Z axis
                axis.SetCoordinates(0, 0, 1)
        End Select

        Dim trans As Transform = New Transform()
        trans.CreateRotation(axis, aAngle)

        rotPoint.TransformFrom(trans)
        Debug.Print("Tfrom: " & rotPoint.X.ToString() & " / " & rotPoint.Y.ToString() & " / " & rotPoint.Z.ToString())
        Return rotPoint
    End Function

    ''' <summary>
    ''' modify item of type probed plane
    ''' </summary>
    Private Sub modifyProbedPlane()
        Try
            Dim aPp As Plane_Probed = mPiDoc.ActiveItem
            Dim newPp As Plane_Probed = aPp.Clone()
            newPp.UseNominals = aPp.UseNominals
            newPp.OutputToReport = aPp.OutputToReport
            newPp.Visible = aPp.Visible
            newPp.Comment = aPp.Comment
            newPp.Name = GetSeqItemName(newPp.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newPp.CNCProbePath Is Nothing) AndAlso (newPp.CNCProbePath.Items.Count > 0) Then
                            newPp.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aPp.FeaturePlane
                        End If
                        newPp.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newPp.ParameterPoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newPp.ParameterPoint.Point.X = newPp.ParameterPoint.Point.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newPp.ParameterPoint.Point.Y = newPp.ParameterPoint.Point.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newPp.ParameterPoint.Point.Z = newPp.ParameterPoint.Point.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newPp.ParameterPoint.Point.X, newPp.ParameterPoint.Point.Y, newPp.ParameterPoint.Point.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newPp.ParameterPoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newPp.ParameterPoint.Point.X = rotPoint.X
                    newPp.ParameterPoint.Point.Y = rotPoint.Y
                    newPp.ParameterPoint.Point.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newPp.Refresh()
            mPiDoc.ActiveItem = newPp
            newPp.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type probed line
    ''' </summary>
    Private Sub modifyProbedLine()
        Try
            Dim aPl As Line_Probed = mPiDoc.ActiveItem
            Dim newPl As Line_Probed = aPl.Clone()
            newPl.UseNominals = aPl.UseNominals
            newPl.OutputToReport = aPl.OutputToReport
            newPl.Visible = aPl.Visible
            newPl.Comment = aPl.Comment
            newPl.Name = GetSeqItemName(newPl.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newPl.CNCProbePath Is Nothing) AndAlso (newPl.CNCProbePath.Items.Count > 0) Then
                            newPl.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aPl.FeatureLine
                        End If
                        newPl.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newPl.ParameterCentroid.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newPl.ParameterCentroid.Point.X = newPl.ParameterCentroid.Point.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newPl.ParameterCentroid.Point.Y = newPl.ParameterCentroid.Point.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newPl.ParameterCentroid.Point.Z = newPl.ParameterCentroid.Point.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newPl.ParameterCentroid.Point.X, newPl.ParameterCentroid.Point.Y, newPl.ParameterCentroid.Point.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newPl.ParameterCentroid.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newPl.ParameterCentroid.Point.X = rotPoint.X
                    newPl.ParameterCentroid.Point.Y = rotPoint.Y
                    newPl.ParameterCentroid.Point.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newPl.Refresh()
            mPiDoc.ActiveItem = newPl
            newPl.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub


    ''' <summary>
    ''' modify item of type retrace point (single point)
    ''' </summary>
    Private Sub modifyRetracePoint()
        Try
            Dim aSp As Point_RetracePoint = mPiDoc.ActiveItem
            Dim newSp As Point_RetracePoint = aSp.Clone()
            newSp.UseNominals = aSp.UseNominals
            newSp.OutputToReport = aSp.OutputToReport
            newSp.Visible = aSp.Visible
            newSp.Comment = aSp.Comment
            newSp.Name = GetSeqItemName(newSp.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newSp.CNCProbePath Is Nothing) AndAlso (newSp.CNCProbePath.Items.Count > 0) Then
                            newSp.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aSp.FeaturePoint
                        End If
                        newSp.PointSources.Add(mPointSource)
                    End If
                Case 1
                        ' tabpage TRANSLATE
                        newSp.PropertyTargetPoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                        newSp.PropertyTargetPoint.NominalCoord.X = newSp.PropertyTargetPoint.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                        newSp.PropertyTargetPoint.NominalCoord.Y = newSp.PropertyTargetPoint.NominalCoord.y + getValOfString(txtTranslationY.Text, "Translation Y")
                        newSp.PropertyTargetPoint.NominalCoord.Z = newSp.PropertyTargetPoint.NominalCoord.z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                        ' tabpage ROTATE
                        Dim rotPoint As Point3D = New Point3D()
                        Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                        rotPoint.SetCoordinates(newSp.PropertyTargetPoint.NominalCoord.X, newSp.PropertyTargetPoint.NominalCoord.Y, newSp.PropertyTargetPoint.NominalCoord.Z)
                        rotPoint = RotateAboutAxis(aAngle, rotPoint)
                        newSp.PropertyTargetPoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                        newSp.PropertyTargetPoint.NominalCoord.X = rotPoint.X
                        newSp.PropertyTargetPoint.NominalCoord.Y = rotPoint.Y
                        newSp.PropertyTargetPoint.NominalCoord.Z = rotPoint.Z
                Case 3
                        ' tabpage MIRROR

            End Select
            newSp.Refresh()
            mPiDoc.ActiveItem = newSp
            newSp.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type single point circle
    ''' </summary>
    Private Sub modifySinglePointCircle()
        Try
            Dim aSpC As Feat_SinglePointCircle = mPiDoc.ActiveItem
            Dim newSpC As Feat_SinglePointCircle = aSpC.Clone()
            newSpC.UseNominals = aSpC.UseNominals
            newSpC.OutputToReport = aSpC.OutputToReport
            newSpC.Visible = aSpC.Visible
            newSpC.Comment = aSpC.Comment
            newSpC.Name = GetSeqItemName(newSpC.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newSpC.CNCProbePath Is Nothing) AndAlso (newSpC.CNCProbePath.Items.Count > 0) Then
                            newSpC.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aSpC.FeatureArc
                        End If
                        newSpC.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newSpC.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newSpC.PropertyCentre.NominalCoord.X = newSpC.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newSpC.PropertyCentre.NominalCoord.Y = newSpC.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newSpC.PropertyCentre.NominalCoord.Z = newSpC.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newSpC.PropertyCentre.NominalCoord.X, newSpC.PropertyCentre.NominalCoord.Y, newSpC.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newSpC.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newSpC.PropertyCentre.NominalCoord.X = rotPoint.X
                    newSpC.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newSpC.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newSpC.Refresh()
            mPiDoc.ActiveItem = newSpC
            newSpC.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type probed circle
    ''' </summary>
    Private Sub modifyCircle()
        Try
            Dim aCircle As Feat_ProbedCircle = mPiDoc.ActiveItem
            Dim newCircle As Feat_ProbedCircle = aCircle.Clone()
            newCircle.UseNominals = aCircle.UseNominals
            newCircle.OutputToReport = aCircle.OutputToReport
            newCircle.Visible = aCircle.Visible
            newCircle.Comment = aCircle.Comment
            newCircle.Name = GetSeqItemName(newCircle.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newCircle.CNCProbePath Is Nothing) AndAlso (newCircle.CNCProbePath.Items.Count > 0) Then
                            newCircle.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aCircle.FeatureArc
                        End If
                        newCircle.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newCircle.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCircle.PropertyCentre.NominalCoord.X = newCircle.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCircle.PropertyCentre.NominalCoord.Y = newCircle.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCircle.PropertyCentre.NominalCoord.Z = newCircle.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCircle.PropertyCentre.NominalCoord.X, newCircle.PropertyCentre.NominalCoord.Y, newCircle.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCircle.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCircle.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCircle.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCircle.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCircle.Refresh()
            mPiDoc.ActiveItem = newCircle
            newCircle.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type probed circle
    ''' </summary>
    Private Sub modifyEllipse()
        Try
            Dim aEllipse As Feat_ProbedEllipse = mPiDoc.ActiveItem
            Dim newEllipse As Feat_ProbedEllipse = aEllipse.Clone()
            newEllipse.UseNominals = aEllipse.UseNominals
            newEllipse.OutputToReport = aEllipse.OutputToReport
            newEllipse.Visible = aEllipse.Visible
            newEllipse.Comment = aEllipse.Comment
            newEllipse.Name = GetSeqItemName(newEllipse.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newEllipse.CNCProbePath Is Nothing) AndAlso (newEllipse.CNCProbePath.Items.Count > 0) Then
                            newEllipse.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aEllipse.FeatureEllipse
                        End If
                        newEllipse.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newEllipse.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newEllipse.PropertyCentre.NominalCoord.X = newEllipse.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newEllipse.PropertyCentre.NominalCoord.Y = newEllipse.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newEllipse.PropertyCentre.NominalCoord.Z = newEllipse.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newEllipse.PropertyCentre.NominalCoord.X, newEllipse.PropertyCentre.NominalCoord.Y, newEllipse.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newEllipse.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newEllipse.PropertyCentre.NominalCoord.X = rotPoint.X
                    newEllipse.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newEllipse.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newEllipse.Refresh()
            mPiDoc.ActiveItem = newEllipse
            newEllipse.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type slot
    ''' </summary>
    Private Sub modifySlot()
        Try
            Dim aSlot As Feat_Slot = mPiDoc.ActiveItem
            Dim newSlot As Feat_Slot = aSlot.Clone()
            newSlot.UseNominals = aSlot.UseNominals
            newSlot.OutputToReport = aSlot.OutputToReport
            newSlot.Visible = aSlot.Visible
            newSlot.Comment = aSlot.Comment
            newSlot.Name = GetSeqItemName(newSlot.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newSlot.CNCProbePath Is Nothing) AndAlso (newSlot.CNCProbePath.Items.Count > 0) Then
                            newSlot.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aSlot.FeatureRoundedSlot
                        End If
                        newSlot.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newSlot.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newSlot.PropertyCentre.NominalCoord.X = newSlot.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newSlot.PropertyCentre.NominalCoord.Y = newSlot.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newSlot.PropertyCentre.NominalCoord.Z = newSlot.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newSlot.PropertyCentre.NominalCoord.X, newSlot.PropertyCentre.NominalCoord.Y, newSlot.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newSlot.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newSlot.PropertyCentre.NominalCoord.X = rotPoint.X
                    newSlot.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newSlot.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newSlot.Refresh()
            mPiDoc.ActiveItem = newSlot
            newSlot.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type rectangle
    ''' </summary>
    Private Sub modifyRectangle()
        Try
            Dim aRectangle As Feat_Rectangle = mPiDoc.ActiveItem
            Dim newRectangle As Feat_Rectangle = aRectangle.Clone()
            newRectangle.UseNominals = aRectangle.UseNominals
            newRectangle.OutputToReport = aRectangle.OutputToReport
            newRectangle.Visible = aRectangle.Visible
            newRectangle.Comment = aRectangle.Comment
            newRectangle.Name = GetSeqItemName(newRectangle.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newRectangle.CNCProbePath Is Nothing) AndAlso (newRectangle.CNCProbePath.Items.Count > 0) Then
                            newRectangle.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aRectangle.FeatureRectangle
                        End If
                        newRectangle.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newRectangle.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newRectangle.PropertyCentre.NominalCoord.X = newRectangle.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newRectangle.PropertyCentre.NominalCoord.Y = newRectangle.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newRectangle.PropertyCentre.NominalCoord.Z = newRectangle.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newRectangle.PropertyCentre.NominalCoord.X, newRectangle.PropertyCentre.NominalCoord.Y, newRectangle.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newRectangle.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newRectangle.PropertyCentre.NominalCoord.X = rotPoint.X
                    newRectangle.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newRectangle.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newRectangle.Refresh()
            mPiDoc.ActiveItem = newRectangle
            newRectangle.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type polygone
    ''' </summary>
    Private Sub modifyPolygone()
        Try
            Dim aPolygone As Feat_Polygone = mPiDoc.ActiveItem
            Dim newPolygone As Feat_Polygone = aPolygone.Clone()
            newPolygone.UseNominals = aPolygone.UseNominals
            newPolygone.OutputToReport = aPolygone.OutputToReport
            newPolygone.Visible = aPolygone.Visible
            newPolygone.Comment = aPolygone.Comment
            newPolygone.Name = GetSeqItemName(newPolygone.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newPolygone.CNCProbePath Is Nothing) AndAlso (newPolygone.CNCProbePath.Items.Count > 0) Then
                            newPolygone.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aPolygone.FeaturePolygon
                        End If
                        newPolygone.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newPolygone.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newPolygone.PropertyCentre.NominalCoord.X = newPolygone.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newPolygone.PropertyCentre.NominalCoord.Y = newPolygone.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newPolygone.PropertyCentre.NominalCoord.Z = newPolygone.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newPolygone.PropertyCentre.NominalCoord.X, newPolygone.PropertyCentre.NominalCoord.Y, newPolygone.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newPolygone.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newPolygone.PropertyCentre.NominalCoord.X = rotPoint.X
                    newPolygone.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newPolygone.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newPolygone.Refresh()
            mPiDoc.ActiveItem = newPolygone
            newPolygone.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type cylinder
    ''' </summary>
    Private Sub modifyCylinder()
        Try
            Dim aCylinder As Feat_Cylinder = mPiDoc.ActiveItem
            Dim newCylinder As Feat_Cylinder = aCylinder.Clone()
            newCylinder.UseNominals = aCylinder.UseNominals
            newCylinder.OutputToReport = aCylinder.OutputToReport
            newCylinder.Visible = aCylinder.Visible
            newCylinder.Comment = aCylinder.Comment
            newCylinder.Name = GetSeqItemName(newCylinder.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newCylinder.CNCProbePath Is Nothing) AndAlso (newCylinder.CNCProbePath.Items.Count > 0) Then
                            newCylinder.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aCylinder.FeatureCylinder
                        End If
                        newCylinder.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newCylinder.ParameterBasePoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCylinder.ParameterBasePoint.Point.X = newCylinder.ParameterBasePoint.Point.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCylinder.ParameterBasePoint.Point.Y = newCylinder.ParameterBasePoint.Point.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCylinder.ParameterBasePoint.Point.Z = newCylinder.ParameterBasePoint.Point.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCylinder.ParameterBasePoint.Point.X, newCylinder.ParameterBasePoint.Point.Y, newCylinder.ParameterBasePoint.Point.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCylinder.ParameterBasePoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCylinder.ParameterBasePoint.Point.X = rotPoint.X
                    newCylinder.ParameterBasePoint.Point.Y = rotPoint.Y
                    newCylinder.ParameterBasePoint.Point.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCylinder.Refresh()
            mPiDoc.ActiveItem = newCylinder
            newCylinder.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type cone
    ''' </summary>
    Private Sub modifyCone()
        Try
            Dim aCone As Feat_Cone = mPiDoc.ActiveItem
            Dim newCone As Feat_Cone = aCone.Clone()
            newCone.UseNominals = aCone.UseNominals
            newCone.OutputToReport = aCone.OutputToReport
            newCone.Visible = aCone.Visible
            newCone.Comment = aCone.Comment
            newCone.Name = GetSeqItemName(newCone.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newCone.CNCProbePath Is Nothing) AndAlso (newCone.CNCProbePath.Items.Count > 0) Then
                            newCone.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aCone.FeatureCone
                        End If
                        newCone.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newCone.ParameterBasePoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCone.ParameterBasePoint.Point.X = newCone.ParameterBasePoint.Point.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCone.ParameterBasePoint.Point.Y = newCone.ParameterBasePoint.Point.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCone.ParameterBasePoint.Point.Z = newCone.ParameterBasePoint.Point.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCone.ParameterBasePoint.Point.X, newCone.ParameterBasePoint.Point.Y, newCone.ParameterBasePoint.Point.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCone.ParameterBasePoint.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCone.ParameterBasePoint.Point.X = rotPoint.X
                    newCone.ParameterBasePoint.Point.Y = rotPoint.Y
                    newCone.ParameterBasePoint.Point.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCone.Refresh()
            mPiDoc.ActiveItem = newCone
            newCone.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type sphere
    ''' </summary>
    Private Sub modifySphere()
        Try
            Dim aSphere As Feat_Sphere = mPiDoc.ActiveItem
            Dim newSphere As Feat_Sphere = aSphere.Clone()
            newSphere.UseNominals = aSphere.UseNominals
            newSphere.OutputToReport = aSphere.OutputToReport
            newSphere.Visible = aSphere.Visible
            newSphere.Comment = aSphere.Comment
            newSphere.Name = GetSeqItemName(newSphere.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newSphere.CNCProbePath Is Nothing) AndAlso (newSphere.CNCProbePath.Items.Count > 0) Then
                            newSphere.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aSphere.FeatureSphere
                        End If
                        newSphere.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newSphere.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newSphere.PropertyCentre.NominalCoord.X = newSphere.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newSphere.PropertyCentre.NominalCoord.Y = newSphere.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newSphere.PropertyCentre.NominalCoord.Z = newSphere.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newSphere.PropertyCentre.NominalCoord.X, newSphere.PropertyCentre.NominalCoord.Y, newSphere.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newSphere.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newSphere.PropertyCentre.NominalCoord.X = rotPoint.X
                    newSphere.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newSphere.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newSphere.Refresh()
            mPiDoc.ActiveItem = newSphere
            newSphere.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type torus
    ''' </summary>
    Private Sub modifyTorus()
        Try
            Dim aTorus As Feat_Torus = mPiDoc.ActiveItem
            Dim newTorus As Feat_Torus = aTorus.Clone()
            newTorus.UseNominals = aTorus.UseNominals
            newTorus.OutputToReport = aTorus.OutputToReport
            newTorus.Visible = aTorus.Visible
            newTorus.Comment = aTorus.Comment
            newTorus.Name = GetSeqItemName(newTorus.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too
                    If (chkCopyUsePointSources.Checked) Then
                        If (Not newTorus.CNCProbePath Is Nothing) AndAlso (newTorus.CNCProbePath.Items.Count > 0) Then
                            newTorus.CNCProbePath.Clear()
                        End If
                        If mPointSource Is Nothing Then
                            mPointSource = aTorus.FeatureTorus
                        End If
                        newTorus.PointSources.Add(mPointSource)
                    End If
                Case 1
                    ' tabpage TRANSLATE
                    newTorus.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newTorus.PropertyCentre.NominalCoord.X = newTorus.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newTorus.PropertyCentre.NominalCoord.Y = newTorus.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newTorus.PropertyCentre.NominalCoord.Z = newTorus.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newTorus.PropertyCentre.NominalCoord.X, newTorus.PropertyCentre.NominalCoord.Y, newTorus.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newTorus.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newTorus.PropertyCentre.NominalCoord.X = rotPoint.X
                    newTorus.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newTorus.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newTorus.Refresh()
            mPiDoc.ActiveItem = newTorus
            newTorus.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type circle by n points
    ''' </summary>
    Private Sub modifyCircleNpts()
        Try
            Dim aCircleNpts As Feat_CircleThroughNPoints = mPiDoc.ActiveItem
            Dim newCircleNpts As Feat_CircleThroughNPoints = aCircleNpts.Clone()
            newCircleNpts.UseNominals = aCircleNpts.UseNominals
            newCircleNpts.OutputToReport = aCircleNpts.OutputToReport
            newCircleNpts.Visible = aCircleNpts.Visible
            newCircleNpts.Comment = aCircleNpts.Comment
            newCircleNpts.Name = GetSeqItemName(newCircleNpts.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => copy point sources to source item too, not supported for CircleThroughNpoints
                Case 1
                    ' tabpage TRANSLATE
                    newCircleNpts.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCircleNpts.PropertyCentre.NominalCoord.X = newCircleNpts.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCircleNpts.PropertyCentre.NominalCoord.Y = newCircleNpts.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCircleNpts.PropertyCentre.NominalCoord.Z = newCircleNpts.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCircleNpts.PropertyCentre.NominalCoord.X, newCircleNpts.PropertyCentre.NominalCoord.Y, newCircleNpts.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCircleNpts.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCircleNpts.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCircleNpts.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCircleNpts.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCircleNpts.Refresh()
            mPiDoc.ActiveItem = newCircleNpts
            newCircleNpts.CNCProbePathParameters.RefreshCNCProbePath()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type circle compound
    ''' </summary>
    Private Sub modifyCircleCompound()
        Try
            Dim aCircleCompound As Compound_Circle = mPiDoc.ActiveItem
            Dim newCompoundCircle As Compound_Circle = aCircleCompound.Clone()
            newCompoundCircle.UseNominals = aCircleCompound.UseNominals
            newCompoundCircle.OutputToReport = aCircleCompound.OutputToReport
            newCompoundCircle.Visible = aCircleCompound.Visible
            newCompoundCircle.Comment = aCircleCompound.Comment
            newCompoundCircle.Name = GetSeqItemName(newCompoundCircle.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => no point sources to copy
                Case 1
                    ' tabpage TRANSLATE
                    newCompoundCircle.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundCircle.PropertyCentre.NominalCoord.X = newCompoundCircle.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCompoundCircle.PropertyCentre.NominalCoord.Y = newCompoundCircle.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCompoundCircle.PropertyCentre.NominalCoord.Z = newCompoundCircle.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCompoundCircle.PropertyCentre.NominalCoord.X, newCompoundCircle.PropertyCentre.NominalCoord.Y, newCompoundCircle.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCompoundCircle.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundCircle.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCompoundCircle.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCompoundCircle.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCompoundCircle.Refresh()
            mPiDoc.ActiveItem = newCompoundCircle
            newCompoundCircle.CNCProbePathParameters.RefreshCNCProbePath()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type ellipse compound
    ''' </summary>
    Private Sub modifyEllipseCompound()
        Try
            Dim aCompoundEllipse As Compound_Ellipse = mPiDoc.ActiveItem
            Dim newCompoundEllipse As Compound_Ellipse = aCompoundEllipse.Clone()
            newCompoundEllipse.UseNominals = aCompoundEllipse.UseNominals
            newCompoundEllipse.OutputToReport = aCompoundEllipse.OutputToReport
            newCompoundEllipse.Visible = aCompoundEllipse.Visible
            newCompoundEllipse.Comment = aCompoundEllipse.Comment
            newCompoundEllipse.Name = GetSeqItemName(newCompoundEllipse.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => no point sources to copy
                Case 1
                    ' tabpage TRANSLATE
                    newCompoundEllipse.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundEllipse.PropertyCentre.NominalCoord.X = newCompoundEllipse.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCompoundEllipse.PropertyCentre.NominalCoord.Y = newCompoundEllipse.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCompoundEllipse.PropertyCentre.NominalCoord.Z = newCompoundEllipse.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCompoundEllipse.PropertyCentre.NominalCoord.X, newCompoundEllipse.PropertyCentre.NominalCoord.Y, newCompoundEllipse.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCompoundEllipse.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundEllipse.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCompoundEllipse.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCompoundEllipse.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCompoundEllipse.Refresh()
            mPiDoc.ActiveItem = newCompoundEllipse
            newCompoundEllipse.CNCProbePathParameters.RefreshCNCProbePath()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type rectangle compound
    ''' </summary>
    Private Sub modifyRectangleCompound()
        Try
            Dim aCompoundRect As Compound_Rectangle = mPiDoc.ActiveItem
            Dim newCompoundRect As Compound_Rectangle = aCompoundRect.Clone()
            newCompoundRect.UseNominals = aCompoundRect.UseNominals
            newCompoundRect.OutputToReport = aCompoundRect.OutputToReport
            newCompoundRect.Visible = aCompoundRect.Visible
            newCompoundRect.Comment = aCompoundRect.Comment
            newCompoundRect.Name = GetSeqItemName(newCompoundRect.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => no point sources to copy
                Case 1
                    ' tabpage TRANSLATE
                    newCompoundRect.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundRect.PropertyCentre.NominalCoord.X = newCompoundRect.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCompoundRect.PropertyCentre.NominalCoord.Y = newCompoundRect.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCompoundRect.PropertyCentre.NominalCoord.Z = newCompoundRect.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCompoundRect.PropertyCentre.NominalCoord.X, newCompoundRect.PropertyCentre.NominalCoord.Y, newCompoundRect.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCompoundRect.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundRect.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCompoundRect.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCompoundRect.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCompoundRect.Refresh()
            mPiDoc.ActiveItem = newCompoundRect
            newCompoundRect.CNCProbePathParameters.RefreshCNCProbePath()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type slot compound
    ''' </summary>
    Private Sub modifySlotCompound()
        Try
            Dim aCompoundSlot As Compound_Slot = mPiDoc.ActiveItem
            Dim newCompoundSlot As Compound_Slot = aCompoundSlot.Clone()
            newCompoundSlot.UseNominals = aCompoundSlot.UseNominals
            newCompoundSlot.OutputToReport = aCompoundSlot.OutputToReport
            newCompoundSlot.Visible = aCompoundSlot.Visible
            newCompoundSlot.Comment = aCompoundSlot.Comment
            newCompoundSlot.Name = GetSeqItemName(newCompoundSlot.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => no point sources to copy
                Case 1
                    ' tabpage TRANSLATE
                    newCompoundSlot.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundSlot.PropertyCentre.NominalCoord.X = newCompoundSlot.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCompoundSlot.PropertyCentre.NominalCoord.Y = newCompoundSlot.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCompoundSlot.PropertyCentre.NominalCoord.Z = newCompoundSlot.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCompoundSlot.PropertyCentre.NominalCoord.X, newCompoundSlot.PropertyCentre.NominalCoord.Y, newCompoundSlot.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCompoundSlot.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundSlot.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCompoundSlot.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCompoundSlot.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCompoundSlot.Refresh()
            mPiDoc.ActiveItem = newCompoundSlot
            newCompoundSlot.CNCProbePathParameters.RefreshCNCProbePath()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type polygon compound
    ''' </summary>
    Private Sub modifyPolygonCompound()
        Try
            Dim aCompoundPolygon As Compound_Polygon = mPiDoc.ActiveItem
            Dim newCompoundPolygon As Compound_Polygon = aCompoundPolygon.Clone()
            newCompoundPolygon.UseNominals = aCompoundPolygon.UseNominals
            newCompoundPolygon.OutputToReport = aCompoundPolygon.OutputToReport
            newCompoundPolygon.Visible = aCompoundPolygon.Visible
            newCompoundPolygon.Comment = aCompoundPolygon.Comment
            newCompoundPolygon.Name = GetSeqItemName(newCompoundPolygon.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => no point sources to copy
                Case 1
                    ' tabpage TRANSLATE
                    newCompoundPolygon.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundPolygon.PropertyCentre.NominalCoord.X = newCompoundPolygon.PropertyCentre.NominalCoord.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCompoundPolygon.PropertyCentre.NominalCoord.Y = newCompoundPolygon.PropertyCentre.NominalCoord.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCompoundPolygon.PropertyCentre.NominalCoord.Z = newCompoundPolygon.PropertyCentre.NominalCoord.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCompoundPolygon.PropertyCentre.NominalCoord.X, newCompoundPolygon.PropertyCentre.NominalCoord.Y, newCompoundPolygon.PropertyCentre.NominalCoord.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCompoundPolygon.PropertyCentre.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundPolygon.PropertyCentre.NominalCoord.X = rotPoint.X
                    newCompoundPolygon.PropertyCentre.NominalCoord.Y = rotPoint.Y
                    newCompoundPolygon.PropertyCentre.NominalCoord.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCompoundPolygon.Refresh()
            mPiDoc.ActiveItem = newCompoundPolygon
            newCompoundPolygon.CNCProbePathParameters.RefreshCNCProbePath()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' modify item of type line compound
    ''' </summary>
    Private Sub modifyLineCompound()
        Try
            Dim aCompoundLine As Compound_Line = mPiDoc.ActiveItem
            Dim newCompoundLine As Compound_Line = aCompoundLine.Clone()
            newCompoundLine.UseNominals = aCompoundLine.UseNominals
            newCompoundLine.OutputToReport = aCompoundLine.OutputToReport
            newCompoundLine.Visible = aCompoundLine.Visible
            newCompoundLine.Comment = aCompoundLine.Comment
            newCompoundLine.Name = GetSeqItemName(newCompoundLine.Name)

            ' do copy/transform dependent on current page control
            Select Case tcTransformation.SelectedIndex
                Case 0
                    ' tabpage COPY => no point sources to copy
                Case 1
                    ' tabpage TRANSLATE
                    newCompoundLine.ParameterCentroid.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundLine.ParameterCentroid.Point.X = newCompoundLine.ParameterCentroid.Point.X + getValOfString(txtTranslationX.Text, "Translation X")
                    newCompoundLine.ParameterCentroid.Point.Y = newCompoundLine.ParameterCentroid.Point.Y + getValOfString(txtTranslationY.Text, "Translation Y")
                    newCompoundLine.ParameterCentroid.Point.Z = newCompoundLine.ParameterCentroid.Point.Z + getValOfString(txtTranslationZ.Text, "Translation Z")
                Case 2
                    ' tabpage ROTATE
                    Dim rotPoint As Point3D = New Point3D()
                    Dim aAngle As Double = getValOfString(txtRotationAngle.Text, "Winkel")
                    rotPoint.SetCoordinates(newCompoundLine.ParameterCentroid.Point.X, newCompoundLine.ParameterCentroid.Point.Y, newCompoundLine.ParameterCentroid.Point.Z)
                    rotPoint = RotateAboutAxis(aAngle, rotPoint)
                    newCompoundLine.ParameterCentroid.CoordinateType.CoordType = geomPoint3DCoordinateType.geomp3d_CARTESIAN
                    newCompoundLine.ParameterCentroid.Point.X = rotPoint.X
                    newCompoundLine.ParameterCentroid.Point.Y = rotPoint.Y
                    newCompoundLine.ParameterCentroid.Point.Z = rotPoint.Z
                Case 3
                    ' tabpage MIRROR

            End Select
            newCompoundLine.Refresh()
            mPiDoc.ActiveItem = newCompoundLine
            newCompoundLine.CNCProbePathParameters.RefreshCNCProbePath()
        Catch
        End Try
    End Sub

    Private Sub txt_Enter(sender As Object, e As EventArgs) Handles txtCopyNamePrefix.Enter, txtTranslationZ.Enter, txtTranslationY.Enter, txtTranslationX.Enter, txtRotationAngle.Enter, txtMirrorCoordinate.FontChanged
        sender.SelectAll()
    End Sub
End Class
