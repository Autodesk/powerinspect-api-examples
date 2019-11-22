' **********************************************************************
' *         © COPYRIGHT 2019 Autodesk, Inc.All Rights Reserved         *
' *                                                                    *
' *  Use of this software is subject to the terms of the Autodesk      *
' *  license agreement provided at the time of installation            *
' *  or download, or which otherwise accompanies this software         *
' *  in either electronic or hard copy form.                           *
' **********************************************************************

Imports System.IO
Imports System.Drawing

Public Class FrmAboutBox

    Private Sub frmAboutBox_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Legen Sie den Titel des Formulars fest.
        Dim applicationTitle As String
        If My.Application.Info.Title <> "" Then
            applicationTitle = My.Application.Info.Title
        Else
            applicationTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Text = String.Format("Info {0}", applicationTitle)
        ' Initialisieren Sie den gesamten Text, der im Infofeld angezeigt wird.
        '    Projekteigenschaften (im Menü "Projekt") an.
        LabelProductName.Text = My.Application.Info.ProductName
        LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        LabelCopyright.Text = My.Application.Info.Copyright
        LabelCompanyName.Text = My.Application.Info.CompanyName
        TextBoxDescription.Text = String.Empty
        If My.Application.Info.Version.Major = 0 And My.Application.Info.Version.Minor = 0 Then
            TextBoxDescription.ForeColor = Color.Red
            ' ReSharper disable LocalizableElement
            TextBoxDescription.Text = "ALPHA Version" & vbCrLf & "Nicht für den produktiven Einsatz" & vbCrLf & vbCrLf
            ' ReSharper restore LocalizableElement
        ElseIf My.Application.Info.Version.Major = 0 And My.Application.Info.Version.Minor <> 0 Then
            TextBoxDescription.ForeColor = Color.Red
            ' ReSharper disable LocalizableElement
            TextBoxDescription.Text = "BETA Version" & vbCrLf & "Nicht für den produktiven Einsatz" & vbCrLf & vbCrLf
            ' ReSharper restore LocalizableElement
        ElseIf My.Application.Info.Version.Major = 1 And My.Application.Info.Version.Minor = 0 And My.Application.Info.Version.Build = 0 Then
            TextBoxDescription.ForeColor = Color.Blue
            ' ReSharper disable LocalizableElement
            TextBoxDescription.Text = "Release Candidate" & vbCrLf & vbCrLf
            ' ReSharper restore LocalizableElement
        Else
        End If
        TextBoxDescription.Text = TextBoxDescription.Text & My.Application.Info.Description
    End Sub


End Class
