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

''' <summary>
''' Class to store collection of AttributeDescription
''' </summary>
Public Class AttributeDescriptionCol

    ''' <summary>
    ''' culture which is used to read data out of dfd/dfx-files
    ''' </summary>
    Private _attributeSeparator As String = String.Empty

    ''' <summary>
    ''' list of attributes and their descriptions read out of dfd-file
    ''' </summary>
    Public Descriptions As ObjectModel.Collection(Of AttributeDef)

    ''' <summary>
    ''' create collection of attribute desriptions
    ''' </summary>
    Public Sub New(ByVal attribSep As String)
        ' create collection
        _attributeSeparator = attribSep
        ' create real collection
        Descriptions = New ObjectModel.Collection(Of AttributeDef)
    End Sub

    ''' <summary>
    ''' Number of attributes read out of dfd/dfx-file
    ''' </summary>
    Public Property AttributeCount() As Integer = -1

    ''' <summary>
    ''' Seperator to split read attribute description into group/attrubute name/attribute
    ''' </summary>
    Public Property AttributeSeperator() As Integer
        Get
            Return _attributeSeparator
        End Get
        Set(ByVal value As Integer)
            _attributeSeparator = value
        End Set
    End Property

    ''' <summary>
    ''' Get attribute index/nr of current line/string
    ''' </summary>
    ''' <param name="sLine">Current line (string) read out of dfd-file (attribute description) </param>
    ''' <returns>Attribute index/nr the current line contains</returns>
    Private Function GetAttributeIndex(ByVal sLine As String) As Integer
        Dim idxSpace As Integer

        ' seperate attribute nr  e. g. K2002/3 I::Normal.... => 3
        GetAttributeIndex = -1
        If Microsoft.VisualBasic.Mid(sLine, 6, 1) = "/" Then
            ' seperate text between first / (at pos 6) and first space
            idxSpace = sLine.IndexOf(" ", 6)
            If (idxSpace > 6) Then
                Dim attribNr As String = Trim(Mid(sLine, 6 + 1, idxSpace - 6))
                If IsNumeric(attribNr) Then
                    GetAttributeIndex = Convert.ToInt32(attribNr)
                End If
            End If
        End If
    End Function


    ''' <summary>
    ''' Open attribute description file, read data and copy data to data object (Descriptions)
    ''' </summary>
    ''' <param name="dfdFileName">Name of file that contains attribute descriptions (dfd-file)</param>
    ''' <returns>True if reading was successfull</returns>
    Public Function ReadAttributeDescriptionsFromFile(ByVal dfdFileName As String) As Boolean
        ReadAttributeDescriptionsFromFile = False
        Descriptions.Clear()
        If IO.File.Exists(dfdFileName) Then

            Dim lCurrentCulture As Globalization.CultureInfo = Threading.Thread.CurrentThread.CurrentCulture
            Try
                'Ensure always reading in English
                Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

                Dim objReader As New StreamReader(dfdFileName)
                Dim sLine As String = String.Empty
                Dim idLine As String
                Dim idxSpace As Integer
                Dim readErr As Boolean = False
                Try
                    Do
                        sLine = objReader.ReadLine()
                        If Not sLine Is Nothing Then
                            sLine = Trim(sLine)
                            idLine = UCase(Microsoft.VisualBasic.Left(sLine, 5))
                            Select Case idLine
                                Case "K0100"
                                    ' count of attributes
                                    If (IsNumeric(Trim(Mid(sLine, 6)))) Then
                                        AttributeCount = Trim(Mid(sLine, 6))

                                        If AttributeCount > 65000 Then
                                            ' about excel more than 65535 rows are not possible
                                            MessageBox.Show("Es können nicht mehr als 65.000 Merkmale importiert werden.", _
                                                            "Einlesen nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Return False
                                        End If
                                    Else
                                        readErr = True
                                    End If

                                Case "K2002"
                                    ' "Merkmal Bezeichnung"
                                    Dim attribute As New AttributeDef()
                                    attribute.Attribute = String.Empty
                                    attribute.AttributeName = String.Empty
                                    attribute.Group = String.Empty
                                    attribute.AttributeUnit = String.Empty
                                    attribute.AttributeLowerLimit = Nothing
                                    attribute.AttributeLowerLimitExist = False
                                    attribute.AttributeDefaultValue = Nothing
                                    attribute.AttributeDefaultValueExist = False
                                    attribute.AttributeUpperLimit = Nothing
                                    attribute.AttributeUpperLimitExist = False

                                    ' parse current line and store data in object

                                    ' seperate attribute nr  e. g. K2002/3 I::Normal.... => 3
                                    attribute.Index = GetAttributeIndex(sLine)

                                    ' seperate parts ob attribute description
                                    idxSpace = sLine.IndexOf(" ") + 1
                                    If (idxSpace > 0) And (idxSpace < sLine.Length - 1) Then
                                        Dim txtLine As String = Trim(Mid(sLine, idxSpace))
                                        Dim attribPart() As String = Split(txtLine, _attributeSeparator)

                                        ' last part = group
                                        If attribPart.Length > 0 Then
                                            attribute.Group = attribPart(attribPart.Length - 1)
                                            ' next to last part = attribute name    
                                            If attribPart.Length > 1 Then
                                                attribute.AttributeName = attribPart(attribPart.Length - 1 - 1)
                                                ' other parts = attribute 
                                                If attribPart.Length > 2 Then
                                                    Dim tmpStr As String = String.Empty
                                                    For index As Integer = attribPart.Length - 3 To 0 Step -1
                                                        tmpStr = tmpStr & attribPart(index) & " "
                                                    Next
                                                    attribute.Attribute = Trim(tmpStr)
                                                End If
                                            End If
                                        End If
                                    Else
                                        readErr = True
                                    End If
                                    If Descriptions.Count > 65000 Then
                                        ' about excel more than 65535 rows are not possible
                                        MessageBox.Show("Es können nicht mehr als 65.000 Merkmale importiert werden.", _
                                                        "Einlesen nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Return False
                                    Else
                                        Descriptions.Add(attribute)
                                    End If
                                Case "K2142"
                                    ' "Einheit Bezeichnung"
                                    ' current unit will be stored in lastly added attribute
                                    ' but check if attributeNr is the same
                                    Dim attribIndex As Integer = GetAttributeIndex(sLine)
                                    If attribIndex = Descriptions.Item(Descriptions.Count - 1).Index Then
                                        Dim attribUnit() As String = Split(sLine, " ")
                                        ' last part of current line contains key and unit => use last part (the unit)
                                        If attribUnit.Length = 2 Then
                                            Descriptions.Item(Descriptions.Count - 1).AttributeUnit = attribUnit(1)
                                        Else
                                            readErr = True
                                        End If
                                    End If
                                Case "K2101"
                                    ' "Nennmaß"
                                    ' current default value will be stored in lastly added attribute
                                    ' but check if attributeNr is the same
                                    Dim attribIndex As Integer = GetAttributeIndex(sLine)
                                    If attribIndex = Descriptions.Item(Descriptions.Count - 1).Index Then
                                        Dim attribValue() As String = Split(sLine, " ")
                                        ' last part of current line contains key and value => use last part (the value)
                                        If (attribValue.Length = 2) AndAlso IsNumeric(attribValue(1)) Then
                                            Descriptions.Item(Descriptions.Count - 1).AttributeDefaultValue = Convert.ToDouble(attribValue(1))
                                            Descriptions.Item(Descriptions.Count - 1).AttributeDefaultValueExist = True
                                        Else
                                            readErr = True
                                        End If
                                    End If
                                Case "K2110"
                                    ' "Unterer Grenzwert"
                                    ' current lower Limit will be stored in lastly added attribute
                                    ' but check if attributeNr is the same
                                    Dim attribIndex As Integer = GetAttributeIndex(sLine)
                                    If attribIndex = Descriptions.Item(Descriptions.Count - 1).Index Then
                                        Dim attribLowerLimit() As String = Split(sLine, " ")
                                        ' last part of current line contains key and value => use last part (the lower Limit)
                                        If attribLowerLimit.Length = 2 AndAlso IsNumeric(attribLowerLimit(1)) Then
                                            Descriptions.Item(Descriptions.Count - 1).AttributeLowerLimit = Convert.ToDouble(attribLowerLimit(1))
                                            Descriptions.Item(Descriptions.Count - 1).AttributeLowerLimitExist = True
                                        Else
                                            readErr = True
                                        End If
                                    End If
                                Case "K2111"
                                    ' "Oberer Grenzwert"
                                    ' current upper Limit will be stored in lastly added attribute
                                    ' but check if attributeNr is the same
                                    Dim attribIndex As Integer = GetAttributeIndex(sLine)
                                    If attribIndex = Descriptions.Item(Descriptions.Count - 1).Index Then
                                        Dim attribUpperLimit() As String = Split(sLine, " ")
                                        ' last part of current line contains key and value => use last part (the upper Limit)
                                        If attribUpperLimit.Length = 2 AndAlso IsNumeric(attribUpperLimit(1)) Then
                                            Descriptions.Item(Descriptions.Count - 1).AttributeUpperLimit = Convert.ToDouble(attribUpperLimit(1))
                                            Descriptions.Item(Descriptions.Count - 1).AttributeUpperLimitExist = True
                                        Else
                                            readErr = True
                                        End If
                                    End If
                            End Select

                            If readErr Then
                                MessageBox.Show("Beim Lesen der Zeile >" & sLine & _
                                    "< aus der DFD-Datei ist ein Fehler aufgetreten.", "Einlesen nicht möglich", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If
                        End If
                    Loop Until sLine Is Nothing
                    If AttributeCount > 0 Then
                        ReadAttributeDescriptionsFromFile = True
                    Else
                        MessageBox.Show("In DFD-Datei ist Anzahl der Merkmale nicht enthalten.", "Einlesen nicht möglich", _
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Finally
                    objReader.Close()
                End Try
            Finally
                Threading.Thread.CurrentThread.CurrentCulture = lCurrentCulture
            End Try
        End If
    End Function

End Class


''' <summary>
''' Class to store the attribute description read out of dfd-file
''' </summary>
Public Class AttributeDef

    Private _index As Integer
    Private _group As String
    Private _attributeName As String
    Private _attribute As String
    Private _attributeUnit As String
    Private _attributeDefaultValue As Double
    Private _attributeDefaultValueExist As Boolean
    Private _attributeUpperLimit As Double
    Private _attributeUpperLimitExist As Boolean
    Private _attributeLowerLimit As Double
    Private _attributeLowerLimitExist As Boolean

    Public Sub New()
        ' ....

    End Sub

    ''' <summary>
    ''' Attribute index/nr read out of dfd-file (K2002)
    ''' </summary>
    ''' <remarks>Sequential number of the attributes</remarks>
    Public Property Index() As Integer
        Get
            Return _index
        End Get
        Set(ByVal value As Integer)
            _index = value
        End Set
    End Property

    ''' <summary>
    ''' Group part of the attribute text read out of dfd-file (K2002)
    ''' </summary>
    Public Property Group() As String
        Get
            Return _group
        End Get
        Set(ByVal value As String)
            _group = value
        End Set
    End Property

    ''' <summary>
    ''' Attribute name part of the attribute text read out of dfd-file (K2002)
    ''' </summary>
    Public Property AttributeName() As String
        Get
            Return _attributeName
        End Get
        Set(ByVal value As String)
            _attributeName = value
        End Set
    End Property

    ''' <summary>
    ''' Attribute part of the attribute text read out of dfd-file (K2002)
    ''' </summary>
    Public Property Attribute() As String
        Get
            Return _attribute
        End Get
        Set(ByVal value As String)
            _attribute = value
        End Set
    End Property

    ''' <summary>
    ''' Attribute unit read out of dfd-file (K2142)
    ''' </summary>
    Public Property AttributeUnit() As String
        Get
            Return _attributeUnit
        End Get
        Set(ByVal value As String)
            _attributeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Attributes default value read out of dfd-file (K2101)
    ''' </summary>
    Public Property AttributeDefaultValue() As Double
        Get
            Return _attributeDefaultValue
        End Get
        Set(ByVal value As Double)
            _attributeDefaultValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates if a default values exists for the current attribute
    ''' </summary>
    Public Property AttributeDefaultValueExist() As Boolean
        Get
            Return _attributeDefaultValueExist
        End Get
        Set(ByVal value As Boolean)
            _attributeDefaultValueExist = value
        End Set
    End Property

    ''' <summary>
    ''' Attributes upper limit read out of dfd-file (K2111)
    ''' </summary>
    Public Property AttributeUpperLimit() As Double
        Get
            Return _attributeUpperLimit
        End Get
        Set(ByVal value As Double)
            _attributeUpperLimit = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates if a upper limit exists for the current attribute
    ''' </summary>
    Public Property AttributeUpperLimitExist() As Boolean
        Get
            Return _attributeUpperLimitExist
        End Get
        Set(ByVal value As Boolean)
            _attributeUpperLimitExist = value
        End Set
    End Property

    ''' <summary>
    ''' Attributes lower limit read out of dfd-file (K2110)
    ''' </summary>
    Public Property AttributeLowerLimit() As Double
        Get
            Return _attributeLowerLimit
        End Get
        Set(ByVal value As Double)
            _attributeLowerLimit = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates if a lower limit exists for the current attribute
    ''' </summary>
    Public Property AttributeLowerLimitExist() As Boolean
        Get
            Return _attributeLowerLimitExist
        End Get
        Set(ByVal value As Boolean)
            _attributeLowerLimitExist = value
        End Set
    End Property

End Class

''' <summary>
''' Class to store collection of attribute data values 
''' </summary>
Public Class DataValueCol

    ''' <summary>
    ''' list of data values read out of dfx-file
    ''' </summary>
    Public DataValues As ObjectModel.Collection(Of Double)

    ''' <summary>
    ''' create collection of data values
    ''' </summary>
    Public Sub New()
        ' create real collection
        DataValues = New ObjectModel.Collection(Of Double)
    End Sub
End Class

''' <summary>
''' Class to store collection of parts and their attribute data values 
''' </summary>
''' <remarks>Each part in collection has a collection of data values</remarks>
Public Class PartCol

    ''' <summary>
    ''' Default attribute data seperator 0F hex, to split read data values
    ''' </summary>
    Private Const CAttributesDataValueSeperator As Char = Chr(15)

    ''' <summary>
    ''' list of parts read out of dfx-file
    ''' </summary>
    Public Parts As ObjectModel.Collection(Of DataValueCol)

    ''' <summary>
    ''' create collection of parts
    ''' </summary>
    Public Sub New()
        ' create real collection
        Parts = New System.Collections.ObjectModel.Collection(Of DataValueCol)
    End Sub

    ''' <summary>
    ''' Open attribute data file, read data and copy data to data object (_attributeData)
    ''' </summary>
    ''' <param name="dfxFileName">Name of file that contains attribute data values (dfx-file)</param>
    ''' <returns>True if reading was successful</returns>
    Public Function ReadAttributeDataFromFile(ByVal dfxFileName As String) As Boolean
        ReadAttributeDataFromFile = False

        If IO.File.Exists(dfxFileName) Then

            Dim lCurrentCulture As Globalization.CultureInfo = Threading.Thread.CurrentThread.CurrentCulture
            Try
                'Ensure always reading in English
                Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

                Dim objReader As New StreamReader(dfxFileName)
                Dim sLine As String = String.Empty
                Dim readErr As Boolean = False
                Try
                    Do
                        sLine = objReader.ReadLine()
                        If Not sLine Is Nothing Then
                            sLine = Trim(sLine)
                            sLine = sLine.Remove(0, 6)

                            Dim readDataValues() As String = Split(Trim(sLine), CAttributesDataValueSeperator)
                            If readDataValues.Length > 0 Then
                                ' copy data (read to array) into collection of data values of current attribute (row)
                                Dim attributeData As New DataValueCol()
                                attributeData.DataValues.Clear()
                                For valIndex As Integer = 0 To readDataValues.Length - 1
                                    If IsNumeric(readDataValues(valIndex)) Then
                                        Dim tmpVal As Double = Convert.ToDouble(readDataValues(valIndex))
                                        attributeData.DataValues.Add(tmpVal)
                                    Else
                                        readErr = True
                                    End If
                                Next
                                If Not readErr Then
                                    ' add list of values (of current attribute) to list of attributes
                                    Parts.Add(attributeData)
                                End If
                            End If

                            If readErr Then
                                MessageBox.Show("Beim Lesen der Zeile >" & sLine & _
                                    "< aus der DFX-Datei ist ein Fehler aufgetreten.", "Einlesen nicht möglich", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If
                        End If
                    Loop Until sLine Is Nothing
                    If Parts.Count > 0 Then
                        ReadAttributeDataFromFile = True
                    Else
                        MessageBox.Show("In DFX-Datei sind keine Daten enthalten.", "Einlesen nicht möglich", _
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Finally
                    objReader.Close()
                End Try
            Finally
                Threading.Thread.CurrentThread.CurrentCulture = lCurrentCulture
            End Try
        End If
    End Function
End Class