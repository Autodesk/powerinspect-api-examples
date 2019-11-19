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
Imports PWIMATHBOXLib

Public Class FrmMain

    Private WithEvents mPi As PowerINSPECT.Application
    Private WithEvents mPiDoc As Document
    Private mMeasure As measure

    ''' <summary>
    ''' supported PI version
    ''' </summary>
    Private Const CpidiSupportedMajorVersion As Integer = 17
    Private Const CpidiSupportedMinorVersion As Integer = 1

    Private Const CHelpFileName As String = "PI Miscellaneous.pdf"


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
        cboCompensationVectors.SelectedIndex = 5
        cboCompensationVectorsCircle.SelectedIndex = 5
    End Sub

    Private Sub FrmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        PiStart()

        tcMiscellaneous.SelectedTab = tpSphereContact

        pbModifying.Value = 0
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

    ''' <summary>
    ''' add surface point inspection group and name it with given name
    ''' </summary>
    ''' <param name="aGrpTxt">Name of the group</param>
    ''' <param name="aTolLower">Lower tolerance of surface inspection group</param>
    ''' <param name="aTolUpper">Upper tolerance of surface inspection group</param>
    Private Function addGrpOfSrfPoints(ByVal aGrpTxt As String, Optional ByVal aTolUpper As Double = Double.MinValue, Optional ByVal aTolLower As Double = Double.MinValue) As SurfaceGroup
        Dim aSrfGrp As SurfaceGroup = Nothing
        If Not mPiDoc Is Nothing Then
            Try
                aSrfGrp = mPiDoc.SequenceItems(aGrpTxt)
                aSrfGrp.Delete()
                aSrfGrp = Nothing
            Catch
            End Try
            If aSrfGrp Is Nothing Then
                aSrfGrp = mPiDoc.SequenceItems.AddGroup(PWI_GroupType.pwi_grp_SurfPointsGuided)
                aSrfGrp.Name = aGrpTxt
                aSrfGrp.MinimumNumberOfPoints = 1
                aSrfGrp.SurfaceOffset = 0.0
                If aTolLower > Double.MinValue Then
                    aSrfGrp.LowerTolerance = aTolLower
                End If
                If aTolUpper > Double.MinValue Then
                    aSrfGrp.UpperTolerance = aTolUpper
                End If
            End If
        End If
        Return aSrfGrp
    End Function

    ''' <summary>
    ''' Add surface point and give them measured coord of sphere as CMM points (measured), considering compensation vector setting
    ''' Assuming surface point group is active
    ''' </summary>
    ''' <param name="aSphere">Sphere with measure data => used to create surface point </param>
    Private Sub addSrfPtSphere(ByVal aSphere As Feat_Sphere)
        If Not mPiDoc Is Nothing Then

            Dim aSrfPt As ISurfacePointItem
            aSrfPt = mPiDoc.ActiveGroup.SequenceItems.AddItem(PWI_EntityItemType.pwi_srf_SurfacePointGuided_)
            aSrfPt.Name = aSphere.Name
            'aSrfPt.TargetPoint.Point.SetCoordinates(aCircle.PropertyCentre.NominalCoord.X, aCircle.PropertyCentre.NominalCoord.Y, aCircle.PropertyCentre.NominalCoord.Z)
            'aSrfPt.TargetPoint.Normal.SetCoordinates(aCircle.PropertyCentre.Nominal(1), aCircle.PropertyCentre.Nominal(1), aCircle.PropertyCentre.Nominal(1))

            Dim oFLAGS
            oFLAGS = 0
            ' Name                          Value 
            'rpf_CMM_COORDINATES_POINTS     1
            'rpf_USE_CURRENTPROBEDIAMETER   2
            'rpf_APPLY_CURRENTPROBEOFFSET   4
            'rpf_UNIT_MILLIMETERS           8
            'rpf_UNIT_INCHES                16
            'rpf_USE_PER_POINT_DIAMETER     32
            'rpf_SURFACE_REFERENCE          64

            'oFLAGS = REPLAYPOINTSFLAGS.rpf_CMM_COORDINATES_POINTS
            'oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_USE_CURRENTPROBEDIAMETER
            'oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_APPLY_CURRENTPROBEOFFSET

            If mPiDoc.Units = PWI_Units.units_INCHES Then
                oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_UNIT_INCHES
            Else
                oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_UNIT_MILLIMETERS
            End If

            Dim aDiameter As Double
            aDiameter = aSphere.PropertyRadius.Measured(mMeasure)

            Dim aPoint As New Point3D
            aPoint = aSphere.PropertyCentre.MeasuredCoord(mMeasure)

            Dim aVector As New Vector3D
            If rbShpereVector.Checked Then
                aVector.I = aSphere.ParameterOrientation.Vector.I
                aVector.J = aSphere.ParameterOrientation.Vector.J
                aVector.K = aSphere.ParameterOrientation.Vector.K
                If chkSphereVectorInvert.Checked Then
                    aVector.Scale(-1)
                End If
            Else
                Select Case cboCompensationVectors.Text
                    Case "X+"
                        aVector.I = 1.0
                        aVector.J = 0.0
                        aVector.K = 0.0
                    Case "X-"
                        aVector.I = -1.0
                        aVector.J = 0.0
                        aVector.K = 0.0
                    Case "Y+"
                        aVector.I = 0.0
                        aVector.J = 1.0
                        aVector.K = 0.0
                    Case "Y-"
                        aVector.I = 0.0
                        aVector.J = -1.0
                        aVector.K = 0.0
                    Case "Z+"
                        aVector.I = 0.0
                        aVector.J = 0.0
                        aVector.K = 1.0
                    Case "Z+"
                        aVector.I = 0.0
                        aVector.J = 0.0
                        aVector.K = -1.0
                End Select
            End If

            Dim oCMMPoints As CMMPoints
            oCMMPoints = CreateObject("PWIMathBox.CMMPoints")
            oCMMPoints.AddXYZIJK(aPoint.X, aPoint.Y, aPoint.Z, _
                                 aVector.I, aVector.J, aVector.K)

            aSrfPt.ReplayWithPoints(mMeasure, aDiameter, oCMMPoints, oFLAGS)
            aSrfPt.Refresh()
        End If
    End Sub


    ''' <summary>
    ''' Add surface point and give them measured coord of circle as CMM points (measured), considering compensation vector setting
    ''' Assuming surface point group is active
    ''' </summary>
    ''' <param name="aCircle">Circle with measure data => used to create surface point </param>
    ''' <param name="aOffset">Offset, defined by user, to be assigned to surface point</param>
    Private Sub addSrfPtCircle(ByVal aCircle As Feat_ProbedCircle, ByVal aOffset As Double)
        If Not mPiDoc Is Nothing Then

            Dim aSrfPt As ISurfacePointItem
            aSrfPt = mPiDoc.ActiveGroup.SequenceItems.AddItem(PWI_EntityItemType.pwi_srf_SurfacePointGuided_)
            aSrfPt.Name = aCircle.Name
            aSrfPt.SurfaceOffset = aOffset
            'aSrfPt.TargetPoint.Point.SetCoordinates(aCircle.PropertyCentre.NominalCoord.X, aCircle.PropertyCentre.NominalCoord.Y, aCircle.PropertyCentre.NominalCoord.Z)
            'aSrfPt.TargetPoint.Normal.SetCoordinates(aCircle.PropertyCentre.Nominal(1), aCircle.PropertyCentre.Nominal(1), aCircle.PropertyCentre.Nominal(1))

            Dim oFLAGS
            oFLAGS = 0
            ' Name                          Value 
            'rpf_CMM_COORDINATES_POINTS     1
            'rpf_USE_CURRENTPROBEDIAMETER   2
            'rpf_APPLY_CURRENTPROBEOFFSET   4
            'rpf_UNIT_MILLIMETERS           8
            'rpf_UNIT_INCHES                16
            'rpf_USE_PER_POINT_DIAMETER     32
            'rpf_SURFACE_REFERENCE          64

            'oFLAGS = REPLAYPOINTSFLAGS.rpf_CMM_COORDINATES_POINTS
            'oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_USE_CURRENTPROBEDIAMETER
            'oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_APPLY_CURRENTPROBEOFFSET

            If mPiDoc.Units = PWI_Units.units_INCHES Then
                oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_UNIT_INCHES
            Else
                oFLAGS = oFLAGS + REPLAYPOINTSFLAGS.rpf_UNIT_MILLIMETERS
            End If

            Dim aPoint As New Point3D
            aPoint = aCircle.PropertyCentre.MeasuredCoord(mMeasure)

            If aCircle.ReferencePlane.FeatureMask = PWI_FeatureMask.pwi_feature_Planes Then
                Dim aFeature As Feature = aCircle.ReferencePlane.Feature
                Dim aRefPlane As Plane_Probed = CType(mPiDoc.SequenceItems(aFeature.Name), Plane_Probed)

                Dim aVector As New Vector3D
                If rbCircleVector.Checked Then
                    aVector.I = aRefPlane.PropertyNormalVector.NominalCoord.I
                    aVector.J = aRefPlane.PropertyNormalVector.NominalCoord.J
                    aVector.K = aRefPlane.PropertyNormalVector.NominalCoord.K
                    If chkCircleVectorInvert.Checked Then
                        aVector.Scale(-1)
                    End If
                Else
                    Select Case cboCompensationVectorsCircle.Text
                        Case "X+"
                            aVector.I = 1.0
                            aVector.J = 0.0
                            aVector.K = 0.0
                        Case "X-"
                            aVector.I = -1.0
                            aVector.J = 0.0
                            aVector.K = 0.0
                        Case "Y+"
                            aVector.I = 0.0
                            aVector.J = 1.0
                            aVector.K = 0.0
                        Case "Y-"
                            aVector.I = 0.0
                            aVector.J = -1.0
                            aVector.K = 0.0
                        Case "Z+"
                            aVector.I = 0.0
                            aVector.J = 0.0
                            aVector.K = 1.0
                        Case "Z+"
                            aVector.I = 0.0
                            aVector.J = 0.0
                            aVector.K = -1.0
                    End Select
                End If

                Dim oCMMPoints As CMMPoints
                oCMMPoints = CreateObject("PWIMathBox.CMMPoints")
                oCMMPoints.AddXYZIJK(aPoint.X, aPoint.Y, aPoint.Z, _
                                     aVector.I, aVector.J, aVector.K)

                aSrfPt.ReplayWithPoints(mMeasure, 0, oCMMPoints, oFLAGS)
                aSrfPt.Refresh()
            End If
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim aOffset As Double = 0.0
        If tcMiscellaneous.SelectedTab Is tpCircleContact Then
            If Not txtCircleVectorOffset.Text.Trim() = String.Empty Then
                ' get offset value
                Dim curNF As NumberFormatInfo = System.Windows.Forms.Application.CurrentCulture.NumberFormat
                Dim aOffsetStr As String = txtCircleVectorOffset.Text.Trim()
                Try
                    aOffset = Double.Parse(aOffsetStr, curNF)
                Catch ex As Exception
                    ' Let the user know what went wrong.
                    MsgBox("Fehlerhafte Eingabe in Feld Versatz.", vbExclamation + vbOKOnly, "Fehlerhafte Eingabe")
                    Return
                End Try
            End If
        End If
        Dim aPiGrp As GeometricGroup
        Dim aSrfGrp As SurfaceGroup = Nothing
        Dim aSrfGrpName As String = String.Empty
        mPiDoc = mPi.ActiveDocument
        mMeasure = mPiDoc.ActiveMeasure
        Try
            If Not mPiDoc Is Nothing Then
                If mPiDoc.ActiveItem Is Nothing Then
                    ' no item selected => nothing to do
                    MsgBox("Kein Element selektiert.", MsgBoxStyle.Information, "Ausführung nicht möglich")
                ElseIf (mPiDoc.ActiveGroup Is Nothing) And (mPiDoc.ActiveItem.ItemType = PWI_EntityItemType.pwi_grp_GeometricGroup_) Then
                    ' loop through all items of active group and modify known item types
                    Dim oldCursor As Cursor = Cursor.Current
                    Try
                        Windows.Forms.Application.UseWaitCursor = True
                        Cursor.Current = Cursors.WaitCursor
                        Dim indexItem As Integer = 1
                        mPiDoc.ActiveGroup = mPiDoc.ActiveItem
                        aPiGrp = mPiDoc.ActiveGroup
                        pbModifying.Maximum = aPiGrp.SequenceItems.Count

                        aSrfGrpName = aPiGrp.Name
                        aSrfGrp = addGrpOfSrfPoints(aSrfGrpName)

                        For Each sequenceItem As SequenceItem In aPiGrp.SequenceItems
                            Select Case tcMiscellaneous.SelectedTab.Name
                                Case tpSphereContact.Name
                                    If sequenceItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Sphere_ Then
                                        Dim aSphere As Feat_Sphere
                                        aSphere = sequenceItem
                                        If aSphere.ResultValid(mMeasure) Then

                                            mPiDoc.ActiveGroup = aSrfGrp
                                            addSrfPtSphere(aSphere)
                                            mPiDoc.ActiveGroup = aPiGrp

                                        End If
                                    End If
                                Case tpCircleContact.Name
                                    If sequenceItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_ProbedCircle_ Then
                                        Dim aCircle As Feat_ProbedCircle
                                        aCircle = sequenceItem
                                        If aCircle.ResultValid(mMeasure) Then

                                            mPiDoc.ActiveGroup = aSrfGrp
                                            addSrfPtCircle(aCircle, aOffset)
                                            mPiDoc.ActiveGroup = aPiGrp

                                        End If
                                    End If
                            End Select
                            pbModifying.Value = indexItem
                            pbModifying.Invalidate()
                            indexItem += 1
                        Next

                        mPiDoc.ActiveGroup = Nothing
                    Finally
                        Windows.Forms.Application.UseWaitCursor = False
                        Cursor.Current = oldCursor
                    End Try
                Else
                    Select Case tcMiscellaneous.SelectedTab.Name
                        Case tpSphereContact.Name
                            ' create one surface point only, corresponding to currently activated sphere
                            If mPiDoc.ActiveItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_Sphere_ Then
                                Dim aSphere As Feat_Sphere
                                aSphere = mPiDoc.ActiveItem

                                aPiGrp = mPiDoc.ActiveGroup

                                aSrfGrpName = String.Empty
                                For Each sequenceItem As SequenceItem In mPiDoc.SequenceItems
                                    ' check if group of surface points already exists (belonging to current geometric group -containign current sphere-)
                                    If sequenceItem.ItemType = PWI_GroupType.pwi_grp_SurfPointsGuided AndAlso sequenceItem.Name = aSphere.Group.Name Then
                                        aSrfGrpName = aSphere.Group.Name
                                        aSrfGrp = sequenceItem
                                        Exit For
                                    End If
                                Next
                                If aSrfGrpName = String.Empty Then
                                    aSrfGrpName = aSphere.Group.Name
                                    aSrfGrp = addGrpOfSrfPoints(aSrfGrpName)
                                End If
                                If aSphere.ResultValid(mMeasure) Then

                                    mPiDoc.ActiveGroup = aSrfGrp
                                    addSrfPtSphere(aSphere)
                                    mPiDoc.ActiveGroup = aPiGrp

                                End If
                            Else
                                MsgBox("Kein Element des Typs 'gemessene Kugel' selektiert.", MsgBoxStyle.Information, "Ausführung nicht möglich")
                            End If
                        Case tpCircleContact.Name
                            ' create one surface point only, corresponding to currently activated circle
                            If mPiDoc.ActiveItem.ItemType = PWI_EntityItemType.pwi_ent_Feat_ProbedCircle_ Then
                                Dim aCircle As Feat_ProbedCircle
                                aCircle = mPiDoc.ActiveItem

                                aPiGrp = mPiDoc.ActiveGroup

                                aSrfGrpName = String.Empty
                                For Each sequenceItem As SequenceItem In mPiDoc.SequenceItems
                                    ' check if group of surface points already exists (belonging to current geometric group -containing current circle-)
                                    If sequenceItem.ItemType = PWI_GroupType.pwi_grp_SurfPointsGuided AndAlso sequenceItem.Name = aCircle.Group.Name Then
                                        aSrfGrpName = aCircle.Group.Name
                                        aSrfGrp = sequenceItem
                                        Exit For
                                    End If
                                Next
                                If aSrfGrpName = String.Empty Then
                                    aSrfGrpName = aCircle.Group.Name
                                    aSrfGrp = addGrpOfSrfPoints(aSrfGrpName)
                                End If
                                If aCircle.ResultValid(mMeasure) Then

                                    mPiDoc.ActiveGroup = aSrfGrp
                                    addSrfPtCircle(aCircle, aOffset)
                                    mPiDoc.ActiveGroup = aPiGrp

                                End If
                            Else
                                MsgBox("Kein Element des Typs 'gemessener Kreis' selektiert.", MsgBoxStyle.Information, "Ausführung nicht möglich")
                            End If
                    End Select
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            pbModifying.Value = 0
        End Try
    End Sub


    Private Sub rb_CheckedChanged(sender As Object, e As EventArgs) Handles rbDefineCompensationVector.CheckedChanged
        If rbDefineCompensationVector.Checked Then
            chkSphereVectorInvert.Enabled = False
            cboCompensationVectors.Enabled = True
        Else
            chkSphereVectorInvert.Enabled = True
            cboCompensationVectors.Enabled = False
        End If

    End Sub

    Private Sub rbDefineCompensationVectorCircle_CheckedChanged(sender As Object, e As EventArgs) Handles rbDefineCompensationVectorCircle.CheckedChanged
        If rbDefineCompensationVectorCircle.Checked Then
            chkCircleVectorInvert.Enabled = False
            cboCompensationVectorsCircle.Enabled = True
            txtCircleVectorOffset.Enabled = False
        Else
            chkCircleVectorInvert.Enabled = True
            cboCompensationVectorsCircle.Enabled = False
            txtCircleVectorOffset.Enabled = True
        End If

    End Sub
End Class
