//=============================================================================
//D FormCatalogItemEditor.cs
//
// CatalogItem edit form
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Catalog item edit form
  /// </summary>
  public class FormCatalogItemEditor : Form
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    public FormCatalogItemEditor()
    {
      InitializeComponent();
      // perform localization
      localize();
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

    //=========================================================================
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.buttonOK = new System.Windows.Forms.Button();
      this.textBoxNumber = new System.Windows.Forms.TextBox();
      this.labelNumber = new System.Windows.Forms.Label();
      this.labelValue = new System.Windows.Forms.Label();
      this.textBoxValue = new System.Windows.Forms.TextBox();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Enabled = false;
      this.buttonOK.Location = new System.Drawing.Point(50, 103);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(100, 23);
      this.buttonOK.TabIndex = 2;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      // 
      // textBoxNumber
      // 
      this.textBoxNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxNumber.Location = new System.Drawing.Point(12, 25);
      this.textBoxNumber.MaxLength = 20;
      this.textBoxNumber.Name = "textBoxNumber";
      this.textBoxNumber.Size = new System.Drawing.Size(296, 20);
      this.textBoxNumber.TabIndex = 0;
      this.textBoxNumber.TextChanged += new System.EventHandler(this.textBoxNumber_TextChanged);
      this.textBoxNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNumber_KeyDown);
      // 
      // labelNumber
      // 
      this.labelNumber.AutoSize = true;
      this.labelNumber.Location = new System.Drawing.Point(9, 9);
      this.labelNumber.Name = "labelNumber";
      this.labelNumber.Size = new System.Drawing.Size(44, 13);
      this.labelNumber.TabIndex = 4;
      this.labelNumber.Text = "Number";
      // 
      // labelValue
      // 
      this.labelValue.AutoSize = true;
      this.labelValue.Location = new System.Drawing.Point(12, 51);
      this.labelValue.Name = "labelValue";
      this.labelValue.Size = new System.Drawing.Size(34, 13);
      this.labelValue.TabIndex = 5;
      this.labelValue.Text = "Value";
      // 
      // textBoxValue
      // 
      this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxValue.Location = new System.Drawing.Point(12, 67);
      this.textBoxValue.MaxLength = 100;
      this.textBoxValue.Name = "textBoxValue";
      this.textBoxValue.Size = new System.Drawing.Size(293, 20);
      this.textBoxValue.TabIndex = 1;
      this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
      this.textBoxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(166, 103);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(100, 23);
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // FormCatalogItemEditor
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(317, 136);
      this.ControlBox = false;
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.textBoxValue);
      this.Controls.Add(this.labelValue);
      this.Controls.Add(this.labelNumber);
      this.Controls.Add(this.textBoxNumber);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormCatalogItemEditor";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Catalog item edit";
      this.Load += new System.EventHandler(this.FormCatalogItemEditor_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion


    internal System.Windows.Forms.TextBox textBoxNumber;
    internal System.Windows.Forms.TextBox textBoxValue;

    //=========================================================================
    /// <summary>
    /// Localisation routine
    /// </summary>
    private void localize()
    {
      this.buttonOK.Text = LS.Lc("OK");
      this.labelNumber.Text = LS.Lc("Number");
      this.labelValue.Text = LS.Lc("Value");
      this.buttonCancel.Text = LS.Lc("Cancel");
      this.Text = LS.Lc("Catalog item edit");
    }

    //=========================================================================
    /// <summary>
    /// Form Load event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormCatalogItemEditor_Load(object sender, EventArgs e)
    {
      textBoxNumber.Focus();
    }

    //=========================================================================
    /// <summary>
    /// TextBoxNumber TextChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void textBoxNumber_TextChanged(object sender, EventArgs e)
    {
      update_ok_button();
    }

    //=========================================================================
    /// <summary>
    /// textBoxValue TextChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void textBoxValue_TextChanged(object sender, EventArgs e)
    {
      update_ok_button();
    }

    //=========================================================================
    /// <summary>
    /// Check if form can be closed via OK
    /// </summary>
    /// <returns></returns>
    private bool can_be_closed_via_ok()
    {
      return (textBoxNumber.Text.Length > 0) && (textBoxValue.Text.Length > 0);
    }

    //=========================================================================
    /// <summary>
    /// Update OK button state
    /// </summary>
    private void update_ok_button()
    {
      buttonOK.Enabled = can_be_closed_via_ok();
    }

    //=========================================================================
    /// <summary>
    /// textBoxNumber KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void textBoxNumber_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return) {
        if (can_be_closed_via_ok()) {
          DialogResult = DialogResult.OK;
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// textBoxValue KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return) {
        if (can_be_closed_via_ok()) {
          DialogResult = DialogResult.OK;
        }
      }
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label labelNumber;
    private System.Windows.Forms.Label labelValue;
    private System.Windows.Forms.Button buttonCancel;

  }
}
