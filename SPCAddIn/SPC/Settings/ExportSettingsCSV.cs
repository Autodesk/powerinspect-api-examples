//=============================================================================
//D ExportSettingsCSV.cs
//
// CSV export settings 
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
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// CSV export settings
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("73965D9C-E27D-463B-8173-AE8560A07E71")
  ]
  public class ExportSettingsCSV : ExportSettings
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal ExportSettingsCSV()
      : base()
    {
    }

    //=========================================================================
    /// <summary>
    /// Load settings from dialog
    /// </summary>
    /// <returns>True, if succeeded</returns>
    public override bool load_from_dialog()
    {
      using (FormExportSettingsCSV fes = new FormExportSettingsCSV()) {
        // restore state we had before
        fes.textBoxOutputFile.Text = output_file_name;
        // show dialog to user
        if (fes.ShowDialog() != DialogResult.OK) return false; // FAILURE     
        // if user has pressed "Save" - save settings. Otherwise - return.
        output_file_name = fes.textBoxOutputFile.Text;
        return true; // SUCCEESS
      }
    }

    //=========================================================================
    /// <summary>
    /// Load from state keeper
    /// </summary>
    /// <param name="a_state_keeper">state keeper</param>
    /// <returns>True, if succeeded</returns>
    public override bool load_from_state_keeper(StateKeeper a_state_keeper)
    {
      // check if we have correct collection with settings
      if (a_state_keeper.settings_csv.Count < 1) return false; // FAILURE
      output_file_name = a_state_keeper.settings_csv[0];
      return true; // SUCCEESS
    }

    //=========================================================================
    /// <summary>
    /// Save using state keeper
    /// </summary>
    /// <param name="a_state_keeper">state keeper</param>
    /// <returns>True, if succeeded</returns>
    public override bool save_to_state_keeper(StateKeeper a_state_keeper)
    {
      a_state_keeper.settings_csv.Clear();
      a_state_keeper.settings_csv.Add(output_file_name);
      return true; // SUCCEESS
    }

    //=========================================================================
    /// <summary>
    /// Called each time export being done.
    /// </summary>
    public override void on_after_export()
    {
    }

    //=========================================================================
    /// <summary>
    /// Output file name
    /// </summary>
    public string output_file_name
    {
      get {
        return m_output_file_name;
      }
      set {
        m_output_file_name = value;
      }
    }

    //=========================================================================
    /// <summary>
    /// Called each time before export run
    /// </summary>
    /// <returns>Pre-import result</returns>
    internal override enumPreInputResult pre_export_input()
    {      
      if (!load_from_dialog()) return enumPreInputResult.PIR_ABORTED;
      if (!is_has_necessary_data()) return enumPreInputResult.PIR_WRONG_DATA;
      return enumPreInputResult.PIR_OK;
    }

    //=========================================================================
    /// <summary>
    /// Check if we have enough settings to begin export
    /// </summary>
    /// <returns>True, if we have enough settings to begin export</returns>
    public override bool is_has_necessary_data()
    {
      try {
        // check if path exists
        return Directory.Exists(Path.GetDirectoryName(output_file_name));
      } catch (Exception) {
        return false;
      }
    }

    /// <summary> Output file name </summary>
    private string m_output_file_name = "";

  }
}

