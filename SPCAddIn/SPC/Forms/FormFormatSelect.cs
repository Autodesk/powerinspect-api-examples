//=============================================================================
//
//D FormFormatSelect class
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// Summary description for FormFormatSelect.
  /// </summary>
  internal class FormFormatSelect : System.Windows.Forms.Form
  {
    private System.Windows.Forms.GroupBox groupBoxExportFormat;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private RadioButton radioButtonCSV;
    private RadioButton radioButtonSDD;
    private RadioButton radioButtonQDAS;
    private RadioButton radioButtonScript;
    private SPCExportType m_export_type;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal FormFormatSelect()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      localize();

    }

    //=========================================================================
    /// <summary>
    /// Get/set export type
    /// </summary>
    internal SPCExportType export_type
    {
      get {
        return m_export_type;
      }
      set {
        switch (value) {
          case SPCExportType.SPC_ET_CSV: {
            radioButtonCSV.Checked = true;
            m_export_type = value;
            break;
          }
          case SPCExportType.SPC_ET_SDD: {
            radioButtonSDD.Checked = true;
            m_export_type = value;
            break;
          }
          case SPCExportType.SPC_ET_QDAS: {            
            radioButtonQDAS.Checked = true;
            m_export_type = value;
            break;
          }
          case SPCExportType.SPC_ET_SCRIPT: {
            m_export_type = value;
            radioButtonScript.Checked = true;
            break;
          }
          default: {
            Debug.Fail("Wrong enumeration value passed");
            break;
          }
        }        
      }
    }

    //=========================================================================
    /// <summary>
    /// Setup form view to current local
    /// </summary>
    private void localize()
    {
      this.groupBoxExportFormat.Text = LS.Lc("Available export formats");
      this.radioButtonScript.Text = LS.Lc("Custom script (JScript, VBScript)");
      this.radioButtonQDAS.Text = LS.Lc("Q-DAS (*.dfd, *.dfx)");
      this.radioButtonSDD.Text = LS.Lc("Lighthouse Systems' SPC data file format (*.sdd)");
      this.radioButtonCSV.Text = LS.Lc("Comma separated values (*.csv)");
      this.buttonOK.Text = LS.Lc("OK");
      this.buttonCancel.Text = LS.Lc("Cancel");
      this.Text = LS.Lc("Select export format");
    }

    //=========================================================================
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if (disposing) {
        if(components != null) {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    //=========================================================================
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.groupBoxExportFormat = new System.Windows.Forms.GroupBox();
      this.radioButtonScript = new System.Windows.Forms.RadioButton();
      this.radioButtonQDAS = new System.Windows.Forms.RadioButton();
      this.radioButtonSDD = new System.Windows.Forms.RadioButton();
      this.radioButtonCSV = new System.Windows.Forms.RadioButton();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.groupBoxExportFormat.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBoxExportFormat
      // 
      this.groupBoxExportFormat.Controls.Add(this.radioButtonScript);
      this.groupBoxExportFormat.Controls.Add(this.radioButtonQDAS);
      this.groupBoxExportFormat.Controls.Add(this.radioButtonSDD);
      this.groupBoxExportFormat.Controls.Add(this.radioButtonCSV);
      this.groupBoxExportFormat.Location = new System.Drawing.Point(8, 8);
      this.groupBoxExportFormat.Name = "groupBoxExportFormat";
      this.groupBoxExportFormat.Size = new System.Drawing.Size(376, 157);
      this.groupBoxExportFormat.TabIndex = 0;
      this.groupBoxExportFormat.TabStop = false;
      this.groupBoxExportFormat.Text = "Available export formats";
      // 
      // radioButtonScript
      // 
      this.radioButtonScript.Location = new System.Drawing.Point(16, 120);
      this.radioButtonScript.Name = "radioButtonScript";
      this.radioButtonScript.Size = new System.Drawing.Size(352, 24);
      this.radioButtonScript.TabIndex = 3;
      this.radioButtonScript.Text = "Custom script (JScript, VBScript)";
      this.radioButtonScript.CheckedChanged += new System.EventHandler(this.radioButtonScript_CheckedChanged);
      // 
      // radioButtonQDAS
      // 
      this.radioButtonQDAS.Location = new System.Drawing.Point(16, 88);
      this.radioButtonQDAS.Name = "radioButtonQDAS";
      this.radioButtonQDAS.Size = new System.Drawing.Size(352, 24);
      this.radioButtonQDAS.TabIndex = 2;
      this.radioButtonQDAS.Text = "Q-DAS (*.dfd, *.dfx)";
      this.radioButtonQDAS.CheckedChanged += new System.EventHandler(this.radioButtonQDAS_CheckedChanged);
      // 
      // radioButtonSDD
      // 
      this.radioButtonSDD.Location = new System.Drawing.Point(16, 56);
      this.radioButtonSDD.Name = "radioButtonSDD";
      this.radioButtonSDD.Size = new System.Drawing.Size(352, 24);
      this.radioButtonSDD.TabIndex = 1;
      this.radioButtonSDD.Text = "Lighthouse Systems' SPC data file format (*.sdd)";
      this.radioButtonSDD.CheckedChanged += new System.EventHandler(this.radioButtonSDD_CheckedChanged);
      // 
      // radioButtonCSV
      // 
      this.radioButtonCSV.Location = new System.Drawing.Point(16, 24);
      this.radioButtonCSV.Name = "radioButtonCSV";
      this.radioButtonCSV.Size = new System.Drawing.Size(352, 24);
      this.radioButtonCSV.TabIndex = 0;
      this.radioButtonCSV.Text = "Comma separated values (*.csv)";
      this.radioButtonCSV.CheckedChanged += new System.EventHandler(this.radioButtonCSV_CheckedChanged);
      // 
      // buttonOK
      // 
      this.buttonOK.Enabled = false;
      this.buttonOK.Location = new System.Drawing.Point(104, 179);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(208, 179);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      // 
      // FormFormatSelect
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(392, 214);
      this.ControlBox = false;
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.groupBoxExportFormat);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormFormatSelect";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Select export format";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFormatSelect_FormClosing);
      this.groupBoxExportFormat.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    //=========================================================================
    /// <summary>
    /// radioButtonScript CheckedChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void radioButtonScript_CheckedChanged(object sender, System.EventArgs e)
    {      
      buttonOK.Enabled = true;
    }

    //=========================================================================
    /// <summary>
    /// radioButtonQDAS CheckedChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void radioButtonQDAS_CheckedChanged(object sender, System.EventArgs e)
    {
      buttonOK.Enabled = true;
    }

    //=========================================================================
    /// <summary>
    /// radioButtonSDD CheckedChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void radioButtonSDD_CheckedChanged(object sender, System.EventArgs e)
    {
      buttonOK.Enabled = true;
    }

    //=========================================================================
    /// <summary>
    /// radioButtonCSV CheckedChanged event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void radioButtonCSV_CheckedChanged(object sender, System.EventArgs e)
    {
      buttonOK.Enabled = true;
    }

    //=========================================================================
    /// <summary>
    /// FormFormatSelect FormClosing event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormFormatSelect_FormClosing(object sender, FormClosingEventArgs e)
    {
      // this event will happen if user press window close button
      if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
    }

    //=========================================================================
    /// <summary>
    /// buttonOK Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (radioButtonCSV.Checked) {
        m_export_type = SPCExportType.SPC_ET_CSV;
      } else if (radioButtonSDD.Checked) {
        m_export_type = SPCExportType.SPC_ET_SDD;
      } else if (radioButtonQDAS.Checked) {
        m_export_type = SPCExportType.SPC_ET_QDAS;
      } else if (radioButtonScript.Checked) {
        m_export_type = SPCExportType.SPC_ET_SCRIPT;
      } else {
        Debug.Fail("should not happen");
      }      
      DialogResult = DialogResult.OK;
    }

    //=========================================================================
    /// <summary>
    /// Load from state keeper
    /// </summary>
    /// <param name="a_state_keeper">state keeper</param>
    internal void load_from_stake_keeper(StateKeeper a_state_keeper)
    {
      if (a_state_keeper.ExportOptions.Count > 0) {
        export_type = (SPCExportType)Convert.ToInt32(a_state_keeper.ExportOptions[0]);
      }
    }

    //=========================================================================
    /// <summary>
    /// Save to state keeper
    /// </summary>
    /// <param name="a_state_keeper">state keeper</param>
    internal void save_to_state_keeper(StateKeeper a_state_keeper)
    {
      if (a_state_keeper.ExportOptions.Count > 0) {
        a_state_keeper.ExportOptions[0] = ((int)export_type).ToString();
      } else {
        a_state_keeper.ExportOptions.Add(((int)export_type).ToString());
      }
    }
  }
}

