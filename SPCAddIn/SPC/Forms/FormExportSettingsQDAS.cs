//=============================================================================
//D FormExportSettingsQDAS.cs
//
// Q-DAS export settings form
//
// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Q-DAS export settings form
  /// </summary>
  internal class FormExportSettingsQDAS : Form
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal FormExportSettingsQDAS()
    {
      InitializeComponent();      
      // perform localization
      localize();
    }

    //=========================================================================
    /// <summary>
    /// Get/Set catalog manager
    /// </summary>
    internal CatalogManager catalog_manager
    {
      get { return m_catalog_manager; }
      set { m_catalog_manager = value; }
    }

    //=========================================================================
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.buttonSave = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPageExportDetails = new System.Windows.Forms.TabPage();
      this.groupBoxDFXData = new System.Windows.Forms.GroupBox();
      this.checkedListBoxDFXFields = new System.Windows.Forms.CheckedListBox();
      this.comboBoxTimeFormat = new System.Windows.Forms.ComboBox();
      this.comboBoxDateFormat = new System.Windows.Forms.ComboBox();
      this.labelTimeFormat = new System.Windows.Forms.Label();
      this.labelDateFormat = new System.Windows.Forms.Label();
      this.checkBoxExportDateTime = new System.Windows.Forms.CheckBox();
      this.groupBoxOutputDirectory = new System.Windows.Forms.GroupBox();
      this.buttonBrowseOutputDirectory = new System.Windows.Forms.Button();
      this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
      this.groupBoxDFDData = new System.Windows.Forms.GroupBox();
      this.panelAdditionalFields = new System.Windows.Forms.Panel();
      this.panelAdditionalFieldsExported = new System.Windows.Forms.Panel();
      this.labelExported = new System.Windows.Forms.Label();
      this.listViewExported = new System.Windows.Forms.ListView();
      this.columnHeaderExportedDescription = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderExportedValue = new System.Windows.Forms.ColumnHeader();
      this.splitterAdditionalFields = new System.Windows.Forms.Splitter();
      this.panelAdditionalFieldsAvailable = new System.Windows.Forms.Panel();
      this.labelAvailable = new System.Windows.Forms.Label();
      this.buttonRemove = new System.Windows.Forms.Button();
      this.buttonAdd = new System.Windows.Forms.Button();
      this.listViewAvailible = new System.Windows.Forms.ListView();
      this.columnHeaderAvailibleKValue = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderAvailibleDescription = new System.Windows.Forms.ColumnHeader();
      this.checkBoxUseFlatHierarchy = new System.Windows.Forms.CheckBox();
      this.checkBoxExportStructureInformation = new System.Windows.Forms.CheckBox();
      this.buttonToggleAvailibleFields = new System.Windows.Forms.Button();
      this.tabPageCatalogManagement = new System.Windows.Forms.TabPage();
      this.panelCatalogManagement = new System.Windows.Forms.Panel();
      this.panelCatalogContent = new System.Windows.Forms.Panel();
      this.groupBoxCatalogContent = new System.Windows.Forms.GroupBox();
      this.buttonDeleteCatalogItem = new System.Windows.Forms.Button();
      this.buttonInsertCatalogItem = new System.Windows.Forms.Button();
      this.buttonEditCatalogItem = new System.Windows.Forms.Button();
      this.listViewCatalogContent = new System.Windows.Forms.ListView();
      this.columnHeaderNumber = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderValue = new System.Windows.Forms.ColumnHeader();
      this.splitterCatalogManagement = new System.Windows.Forms.Splitter();
      this.panelCatalogList = new System.Windows.Forms.Panel();
      this.groupBoxCatalogList = new System.Windows.Forms.GroupBox();
      this.buttonImportFromQDAS = new System.Windows.Forms.Button();
      this.listBoxCatalogs = new System.Windows.Forms.ListBox();
      this.tabControl1.SuspendLayout();
      this.tabPageExportDetails.SuspendLayout();
      this.groupBoxDFXData.SuspendLayout();
      this.groupBoxOutputDirectory.SuspendLayout();
      this.groupBoxDFDData.SuspendLayout();
      this.panelAdditionalFields.SuspendLayout();
      this.panelAdditionalFieldsExported.SuspendLayout();
      this.panelAdditionalFieldsAvailable.SuspendLayout();
      this.tabPageCatalogManagement.SuspendLayout();
      this.panelCatalogManagement.SuspendLayout();
      this.panelCatalogContent.SuspendLayout();
      this.groupBoxCatalogContent.SuspendLayout();
      this.panelCatalogList.SuspendLayout();
      this.groupBoxCatalogList.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonSave
      // 
      this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonSave.Location = new System.Drawing.Point(433, 534);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(120, 23);
      this.buttonSave.TabIndex = 0;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(559, 534);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(120, 23);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPageExportDetails);
      this.tabControl1.Controls.Add(this.tabPageCatalogManagement);
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(694, 516);
      this.tabControl1.TabIndex = 30;
      // 
      // tabPageExportDetails
      // 
      this.tabPageExportDetails.Controls.Add(this.groupBoxDFXData);
      this.tabPageExportDetails.Controls.Add(this.groupBoxOutputDirectory);
      this.tabPageExportDetails.Controls.Add(this.groupBoxDFDData);
      this.tabPageExportDetails.Location = new System.Drawing.Point(4, 22);
      this.tabPageExportDetails.Name = "tabPageExportDetails";
      this.tabPageExportDetails.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageExportDetails.Size = new System.Drawing.Size(686, 490);
      this.tabPageExportDetails.TabIndex = 0;
      this.tabPageExportDetails.Text = "Export details";
      this.tabPageExportDetails.UseVisualStyleBackColor = true;
      // 
      // groupBoxDFXData
      // 
      this.groupBoxDFXData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxDFXData.Controls.Add(this.checkedListBoxDFXFields);
      this.groupBoxDFXData.Controls.Add(this.comboBoxTimeFormat);
      this.groupBoxDFXData.Controls.Add(this.comboBoxDateFormat);
      this.groupBoxDFXData.Controls.Add(this.labelTimeFormat);
      this.groupBoxDFXData.Controls.Add(this.labelDateFormat);
      this.groupBoxDFXData.Controls.Add(this.checkBoxExportDateTime);
      this.groupBoxDFXData.Location = new System.Drawing.Point(3, 346);
      this.groupBoxDFXData.Name = "groupBoxDFXData";
      this.groupBoxDFXData.Size = new System.Drawing.Size(680, 141);
      this.groupBoxDFXData.TabIndex = 32;
      this.groupBoxDFXData.TabStop = false;
      this.groupBoxDFXData.Text = "DFX data";
      // 
      // checkedListBoxDFXFields
      // 
      this.checkedListBoxDFXFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.checkedListBoxDFXFields.FormattingEnabled = true;
      this.checkedListBoxDFXFields.Location = new System.Drawing.Point(211, 9);
      this.checkedListBoxDFXFields.Name = "checkedListBoxDFXFields";
      this.checkedListBoxDFXFields.Size = new System.Drawing.Size(463, 124);
      this.checkedListBoxDFXFields.TabIndex = 12;
      // 
      // comboBoxTimeFormat
      // 
      this.comboBoxTimeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxTimeFormat.FormattingEnabled = true;
      this.comboBoxTimeFormat.Items.AddRange(new object[] {
            "HH:MM:SS",
            "HH:MM"});
      this.comboBoxTimeFormat.Location = new System.Drawing.Point(9, 96);
      this.comboBoxTimeFormat.Name = "comboBoxTimeFormat";
      this.comboBoxTimeFormat.Size = new System.Drawing.Size(196, 21);
      this.comboBoxTimeFormat.TabIndex = 11;
      // 
      // comboBoxDateFormat
      // 
      this.comboBoxDateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxDateFormat.FormattingEnabled = true;
      this.comboBoxDateFormat.Items.AddRange(new object[] {
            "DD.MM.YY",
            "DD.MM.YYYY",
            "MM/DD/YY",
            "MM/DD/YYYY",
            "MM-DD-YY",
            "MM-DD-YYYY"});
      this.comboBoxDateFormat.Location = new System.Drawing.Point(9, 56);
      this.comboBoxDateFormat.Name = "comboBoxDateFormat";
      this.comboBoxDateFormat.Size = new System.Drawing.Size(196, 21);
      this.comboBoxDateFormat.TabIndex = 10;
      // 
      // labelTimeFormat
      // 
      this.labelTimeFormat.AutoSize = true;
      this.labelTimeFormat.Location = new System.Drawing.Point(6, 80);
      this.labelTimeFormat.Name = "labelTimeFormat";
      this.labelTimeFormat.Size = new System.Drawing.Size(62, 13);
      this.labelTimeFormat.TabIndex = 30;
      this.labelTimeFormat.Text = "Time format";
      // 
      // labelDateFormat
      // 
      this.labelDateFormat.AutoSize = true;
      this.labelDateFormat.Location = new System.Drawing.Point(6, 40);
      this.labelDateFormat.Name = "labelDateFormat";
      this.labelDateFormat.Size = new System.Drawing.Size(62, 13);
      this.labelDateFormat.TabIndex = 29;
      this.labelDateFormat.Text = "Date format";
      // 
      // checkBoxExportDateTime
      // 
      this.checkBoxExportDateTime.AutoSize = true;
      this.checkBoxExportDateTime.Location = new System.Drawing.Point(9, 19);
      this.checkBoxExportDateTime.Name = "checkBoxExportDateTime";
      this.checkBoxExportDateTime.Size = new System.Drawing.Size(169, 17);
      this.checkBoxExportDateTime.TabIndex = 9;
      this.checkBoxExportDateTime.Text = "Export date&&time of a measure";
      this.checkBoxExportDateTime.UseVisualStyleBackColor = true;
      // 
      // groupBoxOutputDirectory
      // 
      this.groupBoxOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxOutputDirectory.Controls.Add(this.buttonBrowseOutputDirectory);
      this.groupBoxOutputDirectory.Controls.Add(this.textBoxOutputDirectory);
      this.groupBoxOutputDirectory.Location = new System.Drawing.Point(3, 3);
      this.groupBoxOutputDirectory.Name = "groupBoxOutputDirectory";
      this.groupBoxOutputDirectory.Size = new System.Drawing.Size(680, 49);
      this.groupBoxOutputDirectory.TabIndex = 31;
      this.groupBoxOutputDirectory.TabStop = false;
      this.groupBoxOutputDirectory.Text = "Output directory";
      // 
      // buttonBrowseOutputDirectory
      // 
      this.buttonBrowseOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonBrowseOutputDirectory.Location = new System.Drawing.Point(574, 17);
      this.buttonBrowseOutputDirectory.Name = "buttonBrowseOutputDirectory";
      this.buttonBrowseOutputDirectory.Size = new System.Drawing.Size(100, 23);
      this.buttonBrowseOutputDirectory.TabIndex = 1;
      this.buttonBrowseOutputDirectory.Text = "Browse...";
      this.buttonBrowseOutputDirectory.UseVisualStyleBackColor = true;
      this.buttonBrowseOutputDirectory.Click += new System.EventHandler(this.buttonBrowseOutputDirectory_Click);
      // 
      // textBoxOutputDirectory
      // 
      this.textBoxOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutputDirectory.BackColor = System.Drawing.SystemColors.Window;
      this.textBoxOutputDirectory.Location = new System.Drawing.Point(6, 19);
      this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
      this.textBoxOutputDirectory.Size = new System.Drawing.Size(562, 20);
      this.textBoxOutputDirectory.TabIndex = 0;
      // 
      // groupBoxDFDData
      // 
      this.groupBoxDFDData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxDFDData.Controls.Add(this.panelAdditionalFields);
      this.groupBoxDFDData.Controls.Add(this.checkBoxUseFlatHierarchy);
      this.groupBoxDFDData.Controls.Add(this.checkBoxExportStructureInformation);
      this.groupBoxDFDData.Controls.Add(this.buttonToggleAvailibleFields);
      this.groupBoxDFDData.Location = new System.Drawing.Point(3, 57);
      this.groupBoxDFDData.Name = "groupBoxDFDData";
      this.groupBoxDFDData.Size = new System.Drawing.Size(680, 283);
      this.groupBoxDFDData.TabIndex = 30;
      this.groupBoxDFDData.TabStop = false;
      this.groupBoxDFDData.Text = "DFD data";
      // 
      // panelAdditionalFields
      // 
      this.panelAdditionalFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panelAdditionalFields.Controls.Add(this.panelAdditionalFieldsExported);
      this.panelAdditionalFields.Controls.Add(this.splitterAdditionalFields);
      this.panelAdditionalFields.Controls.Add(this.panelAdditionalFieldsAvailable);
      this.panelAdditionalFields.Location = new System.Drawing.Point(3, 19);
      this.panelAdditionalFields.Name = "panelAdditionalFields";
      this.panelAdditionalFields.Size = new System.Drawing.Size(674, 224);
      this.panelAdditionalFields.TabIndex = 9;
      // 
      // panelAdditionalFieldsExported
      // 
      this.panelAdditionalFieldsExported.Controls.Add(this.labelExported);
      this.panelAdditionalFieldsExported.Controls.Add(this.listViewExported);
      this.panelAdditionalFieldsExported.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelAdditionalFieldsExported.Location = new System.Drawing.Point(357, 0);
      this.panelAdditionalFieldsExported.Name = "panelAdditionalFieldsExported";
      this.panelAdditionalFieldsExported.Size = new System.Drawing.Size(317, 224);
      this.panelAdditionalFieldsExported.TabIndex = 2;
      // 
      // labelExported
      // 
      this.labelExported.AutoSize = true;
      this.labelExported.Location = new System.Drawing.Point(3, 5);
      this.labelExported.Name = "labelExported";
      this.labelExported.Size = new System.Drawing.Size(49, 13);
      this.labelExported.TabIndex = 6;
      this.labelExported.Text = "Exported";
      // 
      // listViewExported
      // 
      this.listViewExported.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewExported.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderExportedDescription,
            this.columnHeaderExportedValue});
      this.listViewExported.FullRowSelect = true;
      this.listViewExported.GridLines = true;
      this.listViewExported.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listViewExported.Location = new System.Drawing.Point(3, 21);
      this.listViewExported.Name = "listViewExported";
      this.listViewExported.Size = new System.Drawing.Size(311, 200);
      this.listViewExported.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.listViewExported.TabIndex = 7;
      this.listViewExported.UseCompatibleStateImageBehavior = false;
      this.listViewExported.View = System.Windows.Forms.View.Details;
      this.listViewExported.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewExported_MouseDown);
      this.listViewExported.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewExported_KeyDown);
      // 
      // columnHeaderExportedDescription
      // 
      this.columnHeaderExportedDescription.Text = "Description";
      this.columnHeaderExportedDescription.Width = 130;
      // 
      // columnHeaderExportedValue
      // 
      this.columnHeaderExportedValue.Text = "Value";
      this.columnHeaderExportedValue.Width = 200;
      // 
      // splitterAdditionalFields
      // 
      this.splitterAdditionalFields.Location = new System.Drawing.Point(354, 0);
      this.splitterAdditionalFields.Name = "splitterAdditionalFields";
      this.splitterAdditionalFields.Size = new System.Drawing.Size(3, 224);
      this.splitterAdditionalFields.TabIndex = 1;
      this.splitterAdditionalFields.TabStop = false;
      // 
      // panelAdditionalFieldsAvailable
      // 
      this.panelAdditionalFieldsAvailable.Controls.Add(this.labelAvailable);
      this.panelAdditionalFieldsAvailable.Controls.Add(this.buttonRemove);
      this.panelAdditionalFieldsAvailable.Controls.Add(this.buttonAdd);
      this.panelAdditionalFieldsAvailable.Controls.Add(this.listViewAvailible);
      this.panelAdditionalFieldsAvailable.Dock = System.Windows.Forms.DockStyle.Left;
      this.panelAdditionalFieldsAvailable.Location = new System.Drawing.Point(0, 0);
      this.panelAdditionalFieldsAvailable.Name = "panelAdditionalFieldsAvailable";
      this.panelAdditionalFieldsAvailable.Size = new System.Drawing.Size(354, 224);
      this.panelAdditionalFieldsAvailable.TabIndex = 0;
      this.panelAdditionalFieldsAvailable.Visible = false;
      // 
      // labelAvailable
      // 
      this.labelAvailable.AutoSize = true;
      this.labelAvailable.Location = new System.Drawing.Point(3, 3);
      this.labelAvailable.Name = "labelAvailable";
      this.labelAvailable.Size = new System.Drawing.Size(50, 13);
      this.labelAvailable.TabIndex = 6;
      this.labelAvailable.Text = "Available";
      // 
      // buttonRemove
      // 
      this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonRemove.Location = new System.Drawing.Point(255, 121);
      this.buttonRemove.Name = "buttonRemove";
      this.buttonRemove.Size = new System.Drawing.Size(96, 23);
      this.buttonRemove.TabIndex = 8;
      this.buttonRemove.Text = "<-- Remove";
      this.buttonRemove.UseVisualStyleBackColor = true;
      this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
      // 
      // buttonAdd
      // 
      this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonAdd.Location = new System.Drawing.Point(255, 92);
      this.buttonAdd.Name = "buttonAdd";
      this.buttonAdd.Size = new System.Drawing.Size(96, 23);
      this.buttonAdd.TabIndex = 7;
      this.buttonAdd.Text = "Add -->";
      this.buttonAdd.UseVisualStyleBackColor = true;
      this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
      // 
      // listViewAvailible
      // 
      this.listViewAvailible.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewAvailible.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAvailibleKValue,
            this.columnHeaderAvailibleDescription});
      this.listViewAvailible.FullRowSelect = true;
      this.listViewAvailible.GridLines = true;
      this.listViewAvailible.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listViewAvailible.Location = new System.Drawing.Point(3, 21);
      this.listViewAvailible.Name = "listViewAvailible";
      this.listViewAvailible.Size = new System.Drawing.Size(249, 200);
      this.listViewAvailible.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.listViewAvailible.TabIndex = 5;
      this.listViewAvailible.UseCompatibleStateImageBehavior = false;
      this.listViewAvailible.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderAvailibleKValue
      // 
      this.columnHeaderAvailibleKValue.Text = "KXXXX";
      this.columnHeaderAvailibleKValue.Width = 54;
      // 
      // columnHeaderAvailibleDescription
      // 
      this.columnHeaderAvailibleDescription.Text = "Description";
      this.columnHeaderAvailibleDescription.Width = 180;
      // 
      // checkBoxUseFlatHierarchy
      // 
      this.checkBoxUseFlatHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBoxUseFlatHierarchy.AutoSize = true;
      this.checkBoxUseFlatHierarchy.Location = new System.Drawing.Point(460, 253);
      this.checkBoxUseFlatHierarchy.Name = "checkBoxUseFlatHierarchy";
      this.checkBoxUseFlatHierarchy.Size = new System.Drawing.Size(108, 17);
      this.checkBoxUseFlatHierarchy.TabIndex = 8;
      this.checkBoxUseFlatHierarchy.Text = "Use flat hierarchy";
      this.checkBoxUseFlatHierarchy.UseVisualStyleBackColor = true;
      // 
      // checkBoxExportStructureInformation
      // 
      this.checkBoxExportStructureInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBoxExportStructureInformation.AutoSize = true;
      this.checkBoxExportStructureInformation.Location = new System.Drawing.Point(288, 253);
      this.checkBoxExportStructureInformation.Name = "checkBoxExportStructureInformation";
      this.checkBoxExportStructureInformation.Size = new System.Drawing.Size(154, 17);
      this.checkBoxExportStructureInformation.TabIndex = 7;
      this.checkBoxExportStructureInformation.Text = "Export structure information";
      this.checkBoxExportStructureInformation.UseVisualStyleBackColor = true;
      // 
      // buttonToggleAvailibleFields
      // 
      this.buttonToggleAvailibleFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonToggleAvailibleFields.Location = new System.Drawing.Point(9, 249);
      this.buttonToggleAvailibleFields.Name = "buttonToggleAvailibleFields";
      this.buttonToggleAvailibleFields.Size = new System.Drawing.Size(273, 23);
      this.buttonToggleAvailibleFields.TabIndex = 6;
      this.buttonToggleAvailibleFields.Text = "Show available fields list";
      this.buttonToggleAvailibleFields.UseVisualStyleBackColor = true;
      this.buttonToggleAvailibleFields.Click += new System.EventHandler(this.buttonToggleAvailibleFields_Click);
      // 
      // tabPageCatalogManagement
      // 
      this.tabPageCatalogManagement.Controls.Add(this.panelCatalogManagement);
      this.tabPageCatalogManagement.Location = new System.Drawing.Point(4, 22);
      this.tabPageCatalogManagement.Name = "tabPageCatalogManagement";
      this.tabPageCatalogManagement.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageCatalogManagement.Size = new System.Drawing.Size(686, 490);
      this.tabPageCatalogManagement.TabIndex = 1;
      this.tabPageCatalogManagement.Text = "Catalog management";
      this.tabPageCatalogManagement.UseVisualStyleBackColor = true;
      // 
      // panelCatalogManagement
      // 
      this.panelCatalogManagement.Controls.Add(this.panelCatalogContent);
      this.panelCatalogManagement.Controls.Add(this.splitterCatalogManagement);
      this.panelCatalogManagement.Controls.Add(this.panelCatalogList);
      this.panelCatalogManagement.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelCatalogManagement.Location = new System.Drawing.Point(3, 3);
      this.panelCatalogManagement.Name = "panelCatalogManagement";
      this.panelCatalogManagement.Size = new System.Drawing.Size(680, 484);
      this.panelCatalogManagement.TabIndex = 1;
      // 
      // panelCatalogContent
      // 
      this.panelCatalogContent.Controls.Add(this.groupBoxCatalogContent);
      this.panelCatalogContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelCatalogContent.Location = new System.Drawing.Point(229, 0);
      this.panelCatalogContent.Name = "panelCatalogContent";
      this.panelCatalogContent.Size = new System.Drawing.Size(451, 484);
      this.panelCatalogContent.TabIndex = 2;
      // 
      // groupBoxCatalogContent
      // 
      this.groupBoxCatalogContent.Controls.Add(this.buttonDeleteCatalogItem);
      this.groupBoxCatalogContent.Controls.Add(this.buttonInsertCatalogItem);
      this.groupBoxCatalogContent.Controls.Add(this.buttonEditCatalogItem);
      this.groupBoxCatalogContent.Controls.Add(this.listViewCatalogContent);
      this.groupBoxCatalogContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBoxCatalogContent.Location = new System.Drawing.Point(0, 0);
      this.groupBoxCatalogContent.Name = "groupBoxCatalogContent";
      this.groupBoxCatalogContent.Size = new System.Drawing.Size(451, 484);
      this.groupBoxCatalogContent.TabIndex = 1;
      this.groupBoxCatalogContent.TabStop = false;
      this.groupBoxCatalogContent.Text = "Catalog content";
      // 
      // buttonDeleteCatalogItem
      // 
      this.buttonDeleteCatalogItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonDeleteCatalogItem.Enabled = false;
      this.buttonDeleteCatalogItem.Location = new System.Drawing.Point(258, 450);
      this.buttonDeleteCatalogItem.Name = "buttonDeleteCatalogItem";
      this.buttonDeleteCatalogItem.Size = new System.Drawing.Size(120, 23);
      this.buttonDeleteCatalogItem.TabIndex = 3;
      this.buttonDeleteCatalogItem.Text = "Delete";
      this.buttonDeleteCatalogItem.UseVisualStyleBackColor = true;
      this.buttonDeleteCatalogItem.Click += new System.EventHandler(this.buttonDeleteCatalogItem_Click);
      // 
      // buttonInsertCatalogItem
      // 
      this.buttonInsertCatalogItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonInsertCatalogItem.Enabled = false;
      this.buttonInsertCatalogItem.Location = new System.Drawing.Point(132, 450);
      this.buttonInsertCatalogItem.Name = "buttonInsertCatalogItem";
      this.buttonInsertCatalogItem.Size = new System.Drawing.Size(120, 23);
      this.buttonInsertCatalogItem.TabIndex = 2;
      this.buttonInsertCatalogItem.Text = "Insert...";
      this.buttonInsertCatalogItem.UseVisualStyleBackColor = true;
      this.buttonInsertCatalogItem.Click += new System.EventHandler(this.buttonInsertCatalogItem_Click);
      // 
      // buttonEditCatalogItem
      // 
      this.buttonEditCatalogItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonEditCatalogItem.Enabled = false;
      this.buttonEditCatalogItem.Location = new System.Drawing.Point(6, 450);
      this.buttonEditCatalogItem.Name = "buttonEditCatalogItem";
      this.buttonEditCatalogItem.Size = new System.Drawing.Size(120, 23);
      this.buttonEditCatalogItem.TabIndex = 1;
      this.buttonEditCatalogItem.Text = "Edit...";
      this.buttonEditCatalogItem.UseVisualStyleBackColor = true;
      this.buttonEditCatalogItem.Click += new System.EventHandler(this.buttonEditCatalogItem_Click);
      // 
      // listViewCatalogContent
      // 
      this.listViewCatalogContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewCatalogContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderNumber,
            this.columnHeaderValue});
      this.listViewCatalogContent.FullRowSelect = true;
      this.listViewCatalogContent.GridLines = true;
      this.listViewCatalogContent.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listViewCatalogContent.Location = new System.Drawing.Point(3, 19);
      this.listViewCatalogContent.Name = "listViewCatalogContent";
      this.listViewCatalogContent.Size = new System.Drawing.Size(445, 420);
      this.listViewCatalogContent.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.listViewCatalogContent.TabIndex = 0;
      this.listViewCatalogContent.UseCompatibleStateImageBehavior = false;
      this.listViewCatalogContent.View = System.Windows.Forms.View.Details;
      this.listViewCatalogContent.DoubleClick += new System.EventHandler(this.listViewCatalogContent_DoubleClick);
      this.listViewCatalogContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewCatalogContent_KeyDown);
      // 
      // columnHeaderNumber
      // 
      this.columnHeaderNumber.Text = "Number";
      this.columnHeaderNumber.Width = 100;
      // 
      // columnHeaderValue
      // 
      this.columnHeaderValue.Text = "Value";
      this.columnHeaderValue.Width = 340;
      // 
      // splitterCatalogManagement
      // 
      this.splitterCatalogManagement.Location = new System.Drawing.Point(226, 0);
      this.splitterCatalogManagement.Name = "splitterCatalogManagement";
      this.splitterCatalogManagement.Size = new System.Drawing.Size(3, 484);
      this.splitterCatalogManagement.TabIndex = 1;
      this.splitterCatalogManagement.TabStop = false;
      // 
      // panelCatalogList
      // 
      this.panelCatalogList.Controls.Add(this.groupBoxCatalogList);
      this.panelCatalogList.Dock = System.Windows.Forms.DockStyle.Left;
      this.panelCatalogList.Location = new System.Drawing.Point(0, 0);
      this.panelCatalogList.Name = "panelCatalogList";
      this.panelCatalogList.Size = new System.Drawing.Size(226, 484);
      this.panelCatalogList.TabIndex = 0;
      // 
      // groupBoxCatalogList
      // 
      this.groupBoxCatalogList.Controls.Add(this.buttonImportFromQDAS);
      this.groupBoxCatalogList.Controls.Add(this.listBoxCatalogs);
      this.groupBoxCatalogList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBoxCatalogList.Location = new System.Drawing.Point(0, 0);
      this.groupBoxCatalogList.Name = "groupBoxCatalogList";
      this.groupBoxCatalogList.Size = new System.Drawing.Size(226, 484);
      this.groupBoxCatalogList.TabIndex = 1;
      this.groupBoxCatalogList.TabStop = false;
      this.groupBoxCatalogList.Text = "Catalogs";
      // 
      // buttonImportFromQDAS
      // 
      this.buttonImportFromQDAS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonImportFromQDAS.Location = new System.Drawing.Point(6, 450);
      this.buttonImportFromQDAS.Name = "buttonImportFromQDAS";
      this.buttonImportFromQDAS.Size = new System.Drawing.Size(214, 23);
      this.buttonImportFromQDAS.TabIndex = 1;
      this.buttonImportFromQDAS.Text = "Import from Q-DAS...";
      this.buttonImportFromQDAS.UseVisualStyleBackColor = true;
      this.buttonImportFromQDAS.Click += new System.EventHandler(this.buttonImportFromQDAS_Click);
      // 
      // listBoxCatalogs
      // 
      this.listBoxCatalogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listBoxCatalogs.FormattingEnabled = true;
      this.listBoxCatalogs.Location = new System.Drawing.Point(3, 19);
      this.listBoxCatalogs.Name = "listBoxCatalogs";
      this.listBoxCatalogs.Size = new System.Drawing.Size(220, 420);
      this.listBoxCatalogs.TabIndex = 0;
      this.listBoxCatalogs.SelectedIndexChanged += new System.EventHandler(this.listBoxCatalogs_SelectedIndexChanged);
      // 
      // FormExportSettingsQDAS
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(694, 569);
      this.ControlBox = false;
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonSave);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(640, 500);
      this.Name = "FormExportSettingsQDAS";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Q-DAS export settings";
      this.Load += new System.EventHandler(this.FormExportSettingsQDAS_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportSettingsQDAS_FormClosing);
      this.tabControl1.ResumeLayout(false);
      this.tabPageExportDetails.ResumeLayout(false);
      this.groupBoxDFXData.ResumeLayout(false);
      this.groupBoxDFXData.PerformLayout();
      this.groupBoxOutputDirectory.ResumeLayout(false);
      this.groupBoxOutputDirectory.PerformLayout();
      this.groupBoxDFDData.ResumeLayout(false);
      this.groupBoxDFDData.PerformLayout();
      this.panelAdditionalFields.ResumeLayout(false);
      this.panelAdditionalFieldsExported.ResumeLayout(false);
      this.panelAdditionalFieldsExported.PerformLayout();
      this.panelAdditionalFieldsAvailable.ResumeLayout(false);
      this.panelAdditionalFieldsAvailable.PerformLayout();
      this.tabPageCatalogManagement.ResumeLayout(false);
      this.panelCatalogManagement.ResumeLayout(false);
      this.panelCatalogContent.ResumeLayout(false);
      this.groupBoxCatalogContent.ResumeLayout(false);
      this.panelCatalogList.ResumeLayout(false);
      this.groupBoxCatalogList.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    //=========================================================================
    /// <summary>
    /// Localisation routine
    /// </summary>
    private void localize()
    {
      this.buttonSave.Text = LS.Lc("Save");
      this.buttonCancel.Text = LS.Lc("Cancel");
      this.tabPageExportDetails.Text = LS.Lc("Export details");
      this.groupBoxDFXData.Text = LS.Lc("DFX data");
      this.labelTimeFormat.Text = LS.Lc("Time format");
      this.labelDateFormat.Text = LS.Lc("Date format");
      this.checkBoxExportDateTime.Text = LS.Lc("Export date&&time of a measure");
      this.groupBoxOutputDirectory.Text = LS.Lc("Output directory");
      this.buttonBrowseOutputDirectory.Text = LS.Lc("Browse...");
      this.groupBoxDFDData.Text = LS.Lc("DFD data");
      this.checkBoxExportStructureInformation.Text = LS.Lc("Export structure information");
      this.buttonToggleAvailibleFields.Text = LS.Lc("Show available fields list");
      this.labelAvailable.Text = LS.Lc("Available");
      this.buttonRemove.Text = LS.Lc("<-- Remove");
      this.buttonAdd.Text = LS.Lc("Add -->");
      this.columnHeaderAvailibleKValue.Text = LS.Lc("KXXXX");
      this.columnHeaderAvailibleDescription.Text = LS.Lc("Description");
      this.labelExported.Text = LS.Lc("Exported");
      this.columnHeaderExportedDescription.Text = LS.Lc("Description");
      this.columnHeaderExportedValue.Text = LS.Lc("Value");
      this.tabPageCatalogManagement.Text = LS.Lc("Catalog management");
      this.groupBoxCatalogList.Text = LS.Lc("Catalogs");
      this.buttonImportFromQDAS.Text = LS.Lc("Import from Q-DAS...");
      this.groupBoxCatalogContent.Text = LS.Lc("Catalog content");
      this.buttonDeleteCatalogItem.Text = LS.Lc("Delete");
      this.buttonInsertCatalogItem.Text = LS.Lc("Insert...");
      this.buttonEditCatalogItem.Text = LS.Lc("Edit...");
      this.columnHeaderNumber.Text = LS.Lc("Number");
      this.columnHeaderValue.Text = LS.Lc("Value");
      this.checkBoxUseFlatHierarchy.Text = LS.Lc("Use flat hierarchy");
      this.Text = LS.Lc("Q-DAS export settings");
    }

    //=========================================================================
    /// <summary>
    /// buttonBrowseOutputDirectory Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonBrowseOutputDirectory_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog dd = new FolderBrowserDialog()) {
        if (
          (textBoxOutputDirectory.Text.Length > 0) &&
          (Directory.Exists(textBoxOutputDirectory.Text))
        ) {
          // navigate to predefined directory, if last one exists
          dd.SelectedPath = textBoxOutputDirectory.Text;
        }

        dd.ShowNewFolderButton = true;
        dd.Description = LS.Lc("Select directory where exported DFD & DFX files should locate");

        if (dd.ShowDialog() == DialogResult.OK) {
          textBoxOutputDirectory.Text = dd.SelectedPath;
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// FormExportSettingsQDAS FormClosing event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormExportSettingsQDAS_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private Button buttonSave;
    private TabControl tabControl1;
    private TabPage tabPageExportDetails;
    private GroupBox groupBoxDFXData;
    internal CheckedListBox checkedListBoxDFXFields;
    internal ComboBox comboBoxTimeFormat;
    internal ComboBox comboBoxDateFormat;
    private Label labelTimeFormat;
    private Label labelDateFormat;
    internal CheckBox checkBoxExportDateTime;
    private GroupBox groupBoxOutputDirectory;
    private Button buttonBrowseOutputDirectory;
    internal TextBox textBoxOutputDirectory;
    private GroupBox groupBoxDFDData;
    internal CheckBox checkBoxExportStructureInformation;
    private Button buttonToggleAvailibleFields;
    private TabPage tabPageCatalogManagement;
    internal CheckBox checkBoxUseFlatHierarchy;
    private Panel panelAdditionalFields;
    private Panel panelAdditionalFieldsExported;
    private Splitter splitterAdditionalFields;
    private Panel panelAdditionalFieldsAvailable;
    private Label labelExported;
    internal ListView listViewExported;
    private ColumnHeader columnHeaderExportedDescription;
    private ColumnHeader columnHeaderExportedValue;
    private Label labelAvailable;
    private Button buttonRemove;
    private Button buttonAdd;
    internal ListView listViewAvailible;
    private ColumnHeader columnHeaderAvailibleKValue;
    private ColumnHeader columnHeaderAvailibleDescription;
    private Panel panelCatalogManagement;
    private Panel panelCatalogContent;
    private Splitter splitterCatalogManagement;
    private Panel panelCatalogList;
    private GroupBox groupBoxCatalogList;
    private Button buttonImportFromQDAS;
    internal ListBox listBoxCatalogs;
    private GroupBox groupBoxCatalogContent;
    private Button buttonDeleteCatalogItem;
    private Button buttonInsertCatalogItem;
    private Button buttonEditCatalogItem;
    internal ListView listViewCatalogContent;
    private ColumnHeader columnHeaderNumber;
    private ColumnHeader columnHeaderValue;
    private Button buttonCancel;

    //=========================================================================
    /// <summary>
    /// buttonToggleAvailibleFields Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonToggleAvailibleFields_Click(object sender, EventArgs e)
    {
      bool b = !panelAdditionalFieldsAvailable.Visible; //splitContainerAdditionalFields.Panel1Collapsed;
      if (b) {
        buttonToggleAvailibleFields.Text = LS.Lc("Hide available fields list");
      } else {
        buttonToggleAvailibleFields.Text = LS.Lc("Show available fields list");
      }
      //splitContainerAdditionalFields.Panel1Collapsed = !b;
      panelAdditionalFieldsAvailable.Visible = b;
      splitterAdditionalFields.Visible = b;
    }

    //=========================================================================
    /// <summary>
    /// buttonAdd Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonAdd_Click(object sender, EventArgs e)
    {
      foreach(ListViewItem lvi in listViewAvailible.SelectedItems) {
        AdditionalField af = lvi.Tag as AdditionalField;
        Debug.Assert(af != null);
        ListViewItem lvi_exp = new ListViewItem(
          new string[]{
            af.description,
            String.Empty
          }
        );
        lvi_exp.Tag = af;
        listViewExported.Items.Add(lvi_exp);
        listViewAvailible.Items.Remove(lvi);
      }
    }

    //=========================================================================
    /// <summary>
    /// buttonRemove Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonRemove_Click(object sender, EventArgs e)
    {
      foreach(ListViewItem lvi in listViewExported.SelectedItems) {
        AdditionalField af = lvi.Tag as AdditionalField;
        if (af.required) continue; // do not remove required fields
        af.selected_for_export = false; // otherwise mark field as not selected for export
        Debug.Assert(af != null);
        ListViewItem lvi_avail = new ListViewItem(
          new string[]{
            af.k_str,
            af.description
          }
        );
        lvi_avail.Tag = af;
        listViewAvailible.Items.Add(lvi_avail);
        listViewExported.Items.Remove(lvi);
      }
    }

    //=========================================================================
    /// <summary>
    /// listViewExported MouseDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void listViewExported_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Clicks != 2) return;
      ListViewItem lvi = (sender as ListView).GetItemAt(e.X, e.Y);
      if (lvi == null) return;
      AdditionalFieldEditor afe = AdditionalFieldEditorCreator.create(lvi, null);
      afe.install();      
    }

    //=========================================================================
    /// <summary>
    /// FormExportSettingsQDAS Load event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormExportSettingsQDAS_Load(object sender, EventArgs e)
    {
    }

    //=========================================================================
    /// <summary>
    /// listViewExported KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event argument</param>
    private void listViewExported_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Return || e.KeyCode == Keys.F2) {
        ListView lv = sender as ListView;
        if (lv.SelectedItems.Count != 1) return;
        ListViewItem lvi = lv.SelectedItems[0];
        if (lvi == null) return;
        //create_text_box(sender, lvi);
        AdditionalFieldEditor afe = AdditionalFieldEditorCreator.create(lvi, null);
        afe.install();
        e.Handled = true;
      }
    }

    //=========================================================================
    /// <summary>
    /// buttonImportFromQDAS Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonImportFromQDAS_Click(object sender, EventArgs e)
    {
      OpenFileDialog fd = new OpenFileDialog();
      fd.Title = LS.Lc("Select name and location of .DFD file");
      fd.Filter = LS.Lc("DFD file (*.DFD)|*.DFD|All files (*.*)|*.*");
      fd.FilterIndex = 1;
      fd.CheckFileExists = true;
      if (fd.ShowDialog() == DialogResult.OK) {
        if (!catalog_manager.load_from_dfd(fd.FileName)) {
          MessageBox.Show(
            LS.Lc("Failed to load catalog information.\nPlease make sure you have selected correct file."),
            LS.Lc("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
          );
        } else {
          load_content_from_catalog(get_selected_catalog());
        }
      }         
    }    

    //=========================================================================
    /// <summary>
    /// listViewCatalogContent KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void listViewCatalogContent_KeyDown(object sender, KeyEventArgs e)
    {
      if (null == get_selected_catalog()) {
        // do nothing as no catalog is selected at the moment
        e.Handled = true;
        return;
      }
      ListView lv = sender as ListView;
      switch(e.KeyCode) {
        case Keys.Delete: {
          delete_catalog_item();
          e.Handled = true;
          break;
        }
        case Keys.Insert: {
          insert_catalog_item();
          e.Handled = true;
          break;
        }
        case Keys.Return: {
          edit_catalog_item();
          e.Handled = true;
          break;
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Edit catalog item
    /// </summary>
    private void edit_catalog_item()
    {
      if (listViewCatalogContent.SelectedItems.Count == 1) {
        using (FormCatalogItemEditor frm = new FormCatalogItemEditor()) {
          CatalogItem ci = listViewCatalogContent.SelectedItems[0].Tag as CatalogItem;
          frm.Text = LS.Lc("Edit catalog item");          
          frm.textBoxNumber.Text = ci.number;          
          frm.textBoxValue.Text = ci.value_str;
          if (frm.ShowDialog(this) == DialogResult.OK) {
            ci.number = frm.textBoxNumber.Text;
            ci.value_str = frm.textBoxValue.Text;
            load_content_from_catalog(get_selected_catalog());
          }
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Delete catalog item
    /// </summary>
    private void delete_catalog_item()
    {
      if (listViewCatalogContent.SelectedItems.Count > 1) {
        if (MessageBox.Show(
          String.Format(
            LS.Lc("Delete {0} items?"),
            listViewCatalogContent.SelectedItems.Count
         ),
         LS.Lc("Confirmation"),
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Question
        ) == DialogResult.No) return;
      }
      foreach (ListViewItem lvi in listViewCatalogContent.SelectedItems) {
        get_selected_catalog().items.Remove(lvi.Tag);
        listViewCatalogContent.Items.Remove(lvi);
      }
    }

    //=========================================================================
    /// <summary>
    /// Insert catalog item
    /// </summary>
    private void insert_catalog_item()
    {
      using (FormCatalogItemEditor frm =  new FormCatalogItemEditor()) {
        frm.Text = LS.Lc("Create catalog item");
        if (frm.ShowDialog(this) == DialogResult.OK) {
          int index_by_number = get_catalog_item_index_by_number(frm.textBoxNumber.Text);
          if (
            (-1 != index_by_number) &&
            (index_by_number == get_catalog_item_index_by_value(frm.textBoxValue.Text))
          ) {
            MessageBox.Show(
              LS.Lc("Item with same number and value already exists"),
              LS.Lc("Attention"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation
            );
          } else {
            Catalog c = get_selected_catalog();
            c.add(new CatalogItem(frm.textBoxNumber.Text, frm.textBoxValue.Text));
            load_content_from_catalog(c);
          }              
        }            
      }
    }

    //=========================================================================
    /// <summary>
    /// Get catalog item index by item's number
    /// </summary>
    /// <param name="a_number">Number of item</param>
    /// <returns>index of item, or -1 if item not found</returns>
    private int get_catalog_item_index_by_number(string a_number)
    {
      foreach(ListViewItem lvi in listViewCatalogContent.Items) {
        if (lvi.Text == a_number) return lvi.Index;
      }
      return -1;
    }

    //=========================================================================
    /// <summary>
    /// Get catalog item index by item's value
    /// </summary>
    /// <param name="a_value">Value of item</param>
    /// <returns>index of item, or -1 if item not found</returns>
    private int get_catalog_item_index_by_value(string a_value)
    {
      foreach (ListViewItem lvi in listViewCatalogContent.Items) {
        if (lvi.SubItems[1].Text == a_value) return lvi.Index;
      }
      return -1;
    }

    //=========================================================================
    /// <summary>
    /// listBoxCatalogs SelectedIndexChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event argument</param>
    private void listBoxCatalogs_SelectedIndexChanged(object sender, EventArgs e)
    {
      // show catalog in the right panel
      Catalog c = get_selected_catalog();
      if (null != c) {
        // enable catalog management buttons
        buttonInsertCatalogItem.Enabled = true;
        buttonEditCatalogItem.Enabled = true;
        buttonDeleteCatalogItem.Enabled = true;
        load_content_from_catalog(c);
      }
    }

    //=========================================================================
    /// <summary>
    /// Load content of listViewCatalogContent from catalog
    /// </summary>
    /// <param name="a_catalog"></param>
    private void load_content_from_catalog(Catalog a_catalog)
    {      
      listViewCatalogContent.Items.Clear();
      if (a_catalog == null) return;
      foreach (CatalogItem ci in a_catalog.items) {
        ListViewItem lvi = new ListViewItem(
          new string[] {
            ci.number,
            ci.value_str
          }
        );
        lvi.Tag = ci;
        listViewCatalogContent.Items.Add(lvi);
      }
    }

    //=========================================================================
    /// <summary>
    /// Get selected catalog
    /// </summary>
    /// <returns>Catalog</returns>
    private Catalog get_selected_catalog()
    {      
      if (listBoxCatalogs.SelectedIndex >= 0) 
        return catalog_manager.catalogs[listBoxCatalogs.SelectedIndex] as Catalog;
      return null;
    }

    //=========================================================================
    /// <summary>
    /// buttonEditCatalogItem Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonEditCatalogItem_Click(object sender, EventArgs e)
    {
      edit_catalog_item();
    }

    //=========================================================================
    /// <summary>
    /// buttonInsertCatalogItem Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonInsertCatalogItem_Click(object sender, EventArgs e)
    {
      insert_catalog_item();
    }

    //=========================================================================
    /// <summary>
    /// buttonDeleteCatalogItem Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonDeleteCatalogItem_Click(object sender, EventArgs e)
    {
      delete_catalog_item();
    }

    private CatalogManager m_catalog_manager = new CatalogManager();

    private void listViewCatalogContent_DoubleClick(object sender, EventArgs e)
    {
      edit_catalog_item();
    }

  }
}

