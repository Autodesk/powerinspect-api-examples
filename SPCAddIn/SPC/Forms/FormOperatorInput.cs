//=============================================================================
//D FormOperatorInput.cs
//
// Operator input form
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
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Operator input form
  /// </summary>
  public class FormOperatorInput : Form
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_export_settings">Export settings</param>
    public FormOperatorInput(ExportSettings a_export_settings)
    {
      InitializeComponent();
      m_export_settings = a_export_settings as ExportSettingsQDAS;
      Debug.Assert(null != m_export_settings, "Should be of type ExportSettingsQDAS");
      localize();
    }

    internal System.Windows.Forms.ListView listViewDFXFields;

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
      this.listViewDFXFields = new System.Windows.Forms.ListView();
      this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderValue = new System.Windows.Forms.ColumnHeader();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // listViewDFXFields
      // 
      this.listViewDFXFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewDFXFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderValue});
      this.listViewDFXFields.FullRowSelect = true;
      this.listViewDFXFields.GridLines = true;
      this.listViewDFXFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listViewDFXFields.Location = new System.Drawing.Point(0, 0);
      this.listViewDFXFields.MultiSelect = false;
      this.listViewDFXFields.Name = "listViewDFXFields";
      this.listViewDFXFields.Size = new System.Drawing.Size(450, 247);
      this.listViewDFXFields.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.listViewDFXFields.TabIndex = 0;
      this.listViewDFXFields.UseCompatibleStateImageBehavior = false;
      this.listViewDFXFields.View = System.Windows.Forms.View.Details;
      this.listViewDFXFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewDFXFields_KeyDown);
      this.listViewDFXFields.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewDFXFields_MouseDown);
      // 
      // columnHeaderName
      // 
      this.columnHeaderName.Text = "Name";
      this.columnHeaderName.Width = 170;
      // 
      // columnHeaderValue
      // 
      this.columnHeaderValue.Text = "Value";
      this.columnHeaderValue.Width = 250;
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Location = new System.Drawing.Point(91, 260);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(120, 23);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(232, 260);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(120, 23);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // FormOperatorInput
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(450, 295);
      this.ControlBox = false;
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.listViewDFXFields);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(800, 600);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 300);
      this.Name = "FormOperatorInput";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Operator input";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOperatorInput_FormClosing);
      this.ResumeLayout(false);

    }

    #endregion

    //=========================================================================
    /// <summary>
    /// Localisation routine
    /// </summary>
    private void localize()
    {
      this.columnHeaderName.Text = LS.Lc("Name");
      this.columnHeaderValue.Text = LS.Lc("Value");
      this.buttonOK.Text = LS.Lc("OK");
      this.buttonCancel.Text = LS.Lc("Cancel");
      this.Text = LS.Lc("Operator input");
    }

    //=========================================================================
    /// <summary>
    /// listViewDFXFields MouseDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void listViewDFXFields_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Clicks != 2) return;
      ListViewItem lvi = (sender as ListView).GetItemAt(e.X, e.Y);
      if (lvi == null) return;
      AdditionalFieldEditor afe = AdditionalFieldEditorCreator.create(lvi,m_export_settings);
      afe.install();
    }

    //=========================================================================
    /// <summary>
    /// listViewDFXFields KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void listViewDFXFields_KeyDown(object sender, KeyEventArgs e)
    {
      ListView lv = sender as ListView;
      Debug.Assert(null != lv);
      if (
        (lv.SelectedItems.Count != 1)
      ) {
        return;
      }
      ListViewItem lvi = lv.SelectedItems[0];
      AdditionalFieldEditor afe = AdditionalFieldEditorCreator.create(lvi, m_export_settings);
      afe.install();
    }

    //=========================================================================
    /// <summary>
    /// FormOperatorInput FormClosing event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormOperatorInput_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (DialogResult == DialogResult.None) {
        DialogResult = DialogResult.Cancel;
      }
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.ColumnHeader columnHeaderName;
    private System.Windows.Forms.ColumnHeader columnHeaderValue;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;    
    private ExportSettingsQDAS m_export_settings = null;

  }
}

