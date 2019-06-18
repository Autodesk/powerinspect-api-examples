//=============================================================================
//D FormExportSettingsCSV.cs
//
// CSV export settings form
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

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// CSV export settings form
  /// </summary>
  internal class FormExportSettingsCSV : Form
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal FormExportSettingsCSV()
    {
      InitializeComponent();
      // perform localization
      localize();
    }

    internal System.Windows.Forms.TextBox textBoxOutputFile;

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

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;


    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.labelOutputFile = new System.Windows.Forms.Label();
      this.textBoxOutputFile = new System.Windows.Forms.TextBox();
      this.buttonBrowseOutputFile = new System.Windows.Forms.Button();
      this.buttonSave = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // labelOutputFile
      // 
      this.labelOutputFile.AutoSize = true;
      this.labelOutputFile.Location = new System.Drawing.Point(12, 9);
      this.labelOutputFile.Name = "labelOutputFile";
      this.labelOutputFile.Size = new System.Drawing.Size(55, 13);
      this.labelOutputFile.TabIndex = 0;
      this.labelOutputFile.Text = "Output file";
      // 
      // textBoxOutputFile
      // 
      this.textBoxOutputFile.Location = new System.Drawing.Point(15, 25);
      this.textBoxOutputFile.Name = "textBoxOutputFile";
      this.textBoxOutputFile.Size = new System.Drawing.Size(419, 20);
      this.textBoxOutputFile.TabIndex = 1;
      // 
      // buttonBrowseOutputFile
      // 
      this.buttonBrowseOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonBrowseOutputFile.Location = new System.Drawing.Point(445, 23);
      this.buttonBrowseOutputFile.Name = "buttonBrowseOutputFile";
      this.buttonBrowseOutputFile.Size = new System.Drawing.Size(94, 23);
      this.buttonBrowseOutputFile.TabIndex = 2;
      this.buttonBrowseOutputFile.Text = "Browse...";
      this.buttonBrowseOutputFile.UseVisualStyleBackColor = true;
      this.buttonBrowseOutputFile.Click += new System.EventHandler(this.buttonBrowseOutputFile_Click);
      // 
      // buttonSave
      // 
      this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonSave.Location = new System.Drawing.Point(140, 63);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(120, 23);
      this.buttonSave.TabIndex = 3;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(284, 63);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(120, 23);
      this.buttonCancel.TabIndex = 4;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // FormExportSettingsCSV
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(551, 98);
      this.ControlBox = false;
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.buttonBrowseOutputFile);
      this.Controls.Add(this.textBoxOutputFile);
      this.Controls.Add(this.labelOutputFile);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormExportSettingsCSV";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "CSV export settings";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    

    //=========================================================================
    /// <summary>
    /// Form localization routine
    /// </summary>
    private void localize()
    {
      this.labelOutputFile.Text = LS.Lc("Output file");
      this.buttonBrowseOutputFile.Text = LS.Lc("Browse...");
      this.buttonSave.Text = LS.Lc("Save");
      this.buttonCancel.Text = LS.Lc("Cancel");
      this.Text = LS.Lc("CSV export settings");
    }

    private void buttonBrowseOutputFile_Click(object sender, EventArgs e)
    {
      SaveFileDialog fd = new SaveFileDialog();
      fd.Title = LS.Lc("Select name and location of .CSV file");
      fd.Filter = LS.Lc("Comma Separated Values file (*.csv)|*.csv|All files (*.*)|*.*");
      fd.FilterIndex = 1;
      if (fd.ShowDialog() == DialogResult.OK) {
        textBoxOutputFile.Text = fd.FileName;
      }        
    }

    private System.Windows.Forms.Label labelOutputFile;
    private System.Windows.Forms.Button buttonBrowseOutputFile;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonCancel;

  }
}
