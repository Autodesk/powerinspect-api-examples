//=============================================================================
//D FormExportSettingsSDD.cs
//
// SDD export settings form
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

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// SDD export settings form
  /// </summary>
  internal class FormExportSettingsSDD : Form
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal FormExportSettingsSDD()
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
      this.m_label_output_file = new System.Windows.Forms.Label();
      this.m_textbox_output_file = new System.Windows.Forms.TextBox();
      this.m_button_ok = new System.Windows.Forms.Button();
      this.m_button_cancel = new System.Windows.Forms.Button();
      this.m_button_browse_output_folder = new System.Windows.Forms.Button();
      this.m_textbox_output_folder = new System.Windows.Forms.TextBox();
      this.m_label_output_folder = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // m_label_output_file
      // 
      this.m_label_output_file.AutoSize = true;
      this.m_label_output_file.Location = new System.Drawing.Point(9, 57);
      this.m_label_output_file.Name = "m_label_output_file";
      this.m_label_output_file.Size = new System.Drawing.Size(158, 13);
      this.m_label_output_file.TabIndex = 0;
      this.m_label_output_file.Text = "File name (without an extension)";
      // 
      // m_textbox_output_file
      // 
      this.m_textbox_output_file.Location = new System.Drawing.Point(12, 73);
      this.m_textbox_output_file.Name = "m_textbox_output_file";
      this.m_textbox_output_file.Size = new System.Drawing.Size(445, 20);
      this.m_textbox_output_file.TabIndex = 1;
      // 
      // m_button_ok
      // 
      this.m_button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_button_ok.Location = new System.Drawing.Point(99, 115);
      this.m_button_ok.Name = "m_button_ok";
      this.m_button_ok.Size = new System.Drawing.Size(120, 23);
      this.m_button_ok.TabIndex = 3;
      this.m_button_ok.Text = "OK";
      this.m_button_ok.UseVisualStyleBackColor = true;
      // 
      // m_button_cancel
      // 
      this.m_button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_button_cancel.Location = new System.Drawing.Point(247, 115);
      this.m_button_cancel.Name = "m_button_cancel";
      this.m_button_cancel.Size = new System.Drawing.Size(120, 23);
      this.m_button_cancel.TabIndex = 4;
      this.m_button_cancel.Text = "Cancel";
      this.m_button_cancel.UseVisualStyleBackColor = true;
      // 
      // m_button_browse_output_folder
      // 
      this.m_button_browse_output_folder.Location = new System.Drawing.Point(345, 26);
      this.m_button_browse_output_folder.Name = "m_button_browse_output_folder";
      this.m_button_browse_output_folder.Size = new System.Drawing.Size(112, 23);
      this.m_button_browse_output_folder.TabIndex = 7;
      this.m_button_browse_output_folder.Text = "Browse...";
      this.m_button_browse_output_folder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.m_button_browse_output_folder.UseVisualStyleBackColor = true;
      this.m_button_browse_output_folder.Click += new System.EventHandler(this.button_browse_output_folder_click);
      // 
      // m_textbox_output_folder
      // 
      this.m_textbox_output_folder.Location = new System.Drawing.Point(12, 28);
      this.m_textbox_output_folder.Name = "m_textbox_output_folder";
      this.m_textbox_output_folder.Size = new System.Drawing.Size(327, 20);
      this.m_textbox_output_folder.TabIndex = 6;
      // 
      // m_label_output_folder
      // 
      this.m_label_output_folder.AutoSize = true;
      this.m_label_output_folder.Location = new System.Drawing.Point(9, 12);
      this.m_label_output_folder.Name = "m_label_output_folder";
      this.m_label_output_folder.Size = new System.Drawing.Size(68, 13);
      this.m_label_output_folder.TabIndex = 5;
      this.m_label_output_folder.Text = "Output folder";
      // 
      // FormExportSettingsSDD
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(467, 152);
      this.ControlBox = false;
      this.Controls.Add(this.m_button_browse_output_folder);
      this.Controls.Add(this.m_textbox_output_folder);
      this.Controls.Add(this.m_label_output_folder);
      this.Controls.Add(this.m_button_cancel);
      this.Controls.Add(this.m_button_ok);
      this.Controls.Add(this.m_textbox_output_file);
      this.Controls.Add(this.m_label_output_file);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormExportSettingsSDD";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "SDD export settings";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportSettingsSDD_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    

    //=========================================================================
    /// <summary>
    /// Localisation routine
    /// </summary>
    private void localize()
    {
      this.m_label_output_folder.Text = LS.Lc("Output folder");
      this.m_label_output_file.Text = LS.Lc("File name (without an extension)");      
      this.m_button_browse_output_folder.Text = LS.Lc("Browse...");      
      this.m_button_ok.Text = LS.Lc("OK");
      this.m_button_cancel.Text = LS.Lc("Cancel");
      this.Text = LS.Lc("SDD export settings");
    }

    //=========================================================================
    /// <summary>
    /// FormExportSettingsSDD FormClosing event hanler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event argument</param>
    private void FormExportSettingsSDD_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Label m_label_output_file;
    private System.Windows.Forms.Button m_button_ok;
    private System.Windows.Forms.Button m_button_cancel;

    //=========================================================================
    /// <summary>
    /// "Browse for output folder" button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button_browse_output_folder_click(object sender, EventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.ShowNewFolderButton = true;
      fbd.Description = LS.Lc("Select a folder to export");
      fbd.RootFolder = Environment.SpecialFolder.Desktop;
      if (Directory.Exists(m_textbox_output_folder.Text)) {
        // if there's a folder selected by the user previously (or entered by the user)
        // then we should use it as initial folder for the browse dialog
        fbd.SelectedPath = m_textbox_output_folder.Text;
      }
      if (fbd.ShowDialog() == DialogResult.OK) {
        m_textbox_output_folder.Text = fbd.SelectedPath;
      }
    }

    /// <summary> Browse the output folder button </summary>
    private Button m_button_browse_output_folder;
    /// <summary> Text box "Output folder" </summary>
    internal TextBox m_textbox_output_folder;
    /// <summary> Label for output folder text box  </summary>
    private Label m_label_output_folder;
    /// <summary> Output file name text box </summary>
    internal System.Windows.Forms.TextBox m_textbox_output_file;

  }
}
