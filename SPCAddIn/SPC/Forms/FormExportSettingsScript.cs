//=============================================================================
//D FormExportSettingsScript.cs
//
// Script export settings form
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
  /// Script export settings form
  /// </summary>
  internal class FormExportSettingsScript : Form
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal FormExportSettingsScript()
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

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.labelScriptFile = new System.Windows.Forms.Label();
      this.textBoxScriptFile = new System.Windows.Forms.TextBox();
      this.buttonBrowseScriptFile = new System.Windows.Forms.Button();
      this.buttonBrowseOutputFile = new System.Windows.Forms.Button();
      this.textBoxOutputFile = new System.Windows.Forms.TextBox();
      this.labelOutputFile = new System.Windows.Forms.Label();
      this.buttonSave = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // labelScriptFile
      // 
      this.labelScriptFile.AutoSize = true;
      this.labelScriptFile.Location = new System.Drawing.Point(12, 9);
      this.labelScriptFile.Name = "labelScriptFile";
      this.labelScriptFile.Size = new System.Drawing.Size(50, 13);
      this.labelScriptFile.TabIndex = 0;
      this.labelScriptFile.Text = "Script file";
      // 
      // textBoxScriptFile
      // 
      this.textBoxScriptFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxScriptFile.Location = new System.Drawing.Point(12, 25);
      this.textBoxScriptFile.Name = "textBoxScriptFile";
      this.textBoxScriptFile.Size = new System.Drawing.Size(410, 20);
      this.textBoxScriptFile.TabIndex = 1;
      // 
      // buttonBrowseScriptFile
      // 
      this.buttonBrowseScriptFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonBrowseScriptFile.Location = new System.Drawing.Point(428, 23);
      this.buttonBrowseScriptFile.Name = "buttonBrowseScriptFile";
      this.buttonBrowseScriptFile.Size = new System.Drawing.Size(100, 23);
      this.buttonBrowseScriptFile.TabIndex = 2;
      this.buttonBrowseScriptFile.Text = "Browse...";
      this.buttonBrowseScriptFile.UseVisualStyleBackColor = true;
      this.buttonBrowseScriptFile.Click += new System.EventHandler(this.buttonBrowseScriptFile_Click);
      // 
      // buttonBrowseOutputFile
      // 
      this.buttonBrowseOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonBrowseOutputFile.Location = new System.Drawing.Point(428, 62);
      this.buttonBrowseOutputFile.Name = "buttonBrowseOutputFile";
      this.buttonBrowseOutputFile.Size = new System.Drawing.Size(100, 23);
      this.buttonBrowseOutputFile.TabIndex = 5;
      this.buttonBrowseOutputFile.Text = "Browse...";
      this.buttonBrowseOutputFile.UseVisualStyleBackColor = true;
      this.buttonBrowseOutputFile.Click += new System.EventHandler(this.buttonBrowseOutputFile_Click);
      // 
      // textBoxOutputFile
      // 
      this.textBoxOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutputFile.Location = new System.Drawing.Point(12, 64);
      this.textBoxOutputFile.Name = "textBoxOutputFile";
      this.textBoxOutputFile.Size = new System.Drawing.Size(410, 20);
      this.textBoxOutputFile.TabIndex = 4;
      // 
      // labelOutputFile
      // 
      this.labelOutputFile.AutoSize = true;
      this.labelOutputFile.Location = new System.Drawing.Point(12, 48);
      this.labelOutputFile.Name = "labelOutputFile";
      this.labelOutputFile.Size = new System.Drawing.Size(55, 13);
      this.labelOutputFile.TabIndex = 3;
      this.labelOutputFile.Text = "Output file";
      // 
      // buttonSave
      // 
      this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonSave.Location = new System.Drawing.Point(139, 100);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(120, 23);
      this.buttonSave.TabIndex = 6;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(280, 100);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(120, 23);
      this.buttonCancel.TabIndex = 7;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // FormExportSettingsScript
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(540, 135);
      this.ControlBox = false;
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.buttonBrowseOutputFile);
      this.Controls.Add(this.textBoxOutputFile);
      this.Controls.Add(this.labelOutputFile);
      this.Controls.Add(this.buttonBrowseScriptFile);
      this.Controls.Add(this.textBoxScriptFile);
      this.Controls.Add(this.labelScriptFile);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormExportSettingsScript";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Script export settings";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportSettingsScript_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.TextBox textBoxScriptFile;
    internal System.Windows.Forms.TextBox textBoxOutputFile;


    //=========================================================================
    /// <summary>
    /// Localization routine
    /// </summary>
    private void localize()
    {
      this.labelScriptFile.Text = LS.Lc("Script file");
      this.buttonBrowseScriptFile.Text = LS.Lc("Browse...");
      this.buttonBrowseOutputFile.Text = LS.Lc("Browse...");
      this.labelOutputFile.Text = LS.Lc("Output file");
      this.buttonSave.Text = LS.Lc("Save");
      this.buttonCancel.Text = LS.Lc("Cancel");
      this.Text = LS.Lc("Script export settings");
    }

    //=========================================================================
    /// <summary>
    /// buttonBrowseScriptFile Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonBrowseScriptFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog fd = new OpenFileDialog();
      fd.Title = LS.Lc("Select script file to process");
      fd.Filter = LS.Lc("VBScript files (*.vbs)|*.vbs|JScript files (*.js)|*.js|All files (*.*)|*.*");
      fd.FilterIndex = 1;
      if (fd.ShowDialog() == DialogResult.OK) {
        textBoxScriptFile.Text = fd.FileName;
      }
    }

    //=========================================================================
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonBrowseOutputFile_Click(object sender, EventArgs e)
    {
      SaveFileDialog fd = new SaveFileDialog();
      fd.Title = LS.Lc("Select output file");
      fd.Filter = LS.Lc("Text files (*.txt)|*.txt|All files (*.*)|*.*");
      fd.FilterIndex = 1;

      if (fd.ShowDialog() == DialogResult.OK) {
        textBoxOutputFile.Text = fd.FileName;
      }
    }

    //=========================================================================
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormExportSettingsScript_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
    }


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;


    private System.Windows.Forms.Label labelScriptFile;
    private System.Windows.Forms.Button buttonBrowseScriptFile;
    private System.Windows.Forms.Button buttonBrowseOutputFile;
    private System.Windows.Forms.Label labelOutputFile;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonCancel;


  }
}
