//=============================================================================
//D ExportSettingsScript.cs
//
// Export settings for script-based export
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
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Script-based export settings
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("89771BA4-62D9-4A3C-A380-D659448CD85D")
  ]
  public class ExportSettingsScript : ExportSettings
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal ExportSettingsScript()
      : base()
    {      
    }

    //=========================================================================
    /// <summary>
    /// Load settings from dialog
    /// </summary>
    /// <returns></returns>
    public override bool load_from_dialog()
    {
      using (FormExportSettingsScript fes = new FormExportSettingsScript()) {
        // restore state we had before
        fes.textBoxScriptFile.Text = script_file_name;
        fes.textBoxOutputFile.Text = output_file_name;
        // show dialog to user
        if (fes.ShowDialog() != DialogResult.OK) return false; // FAILURE     
        // if user has pressed "Save" - save settings. Otherwise - return.
        script_file_name = fes.textBoxScriptFile.Text;
        output_file_name = fes.textBoxOutputFile.Text;
        return true; // SUCCEESS
      }
    }

    //=========================================================================
    /// <summary>
    /// Load settings from state keeper
    /// </summary>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>True, if succeeded</returns>
    public override bool load_from_state_keeper(StateKeeper a_state_keeper)
    {
      // check if we have correct collection with settings
      if (a_state_keeper.settings_script.Count < 2) return false; // FAILURE
      script_file_name = a_state_keeper.settings_script[0];
      output_file_name = a_state_keeper.settings_script[1];
      return true; // SUCCEESS
    }

    //=========================================================================
    /// <summary>
    /// Save settings useing state keeper
    /// </summary>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>True, if succeeded</returns>
    public override bool save_to_state_keeper(StateKeeper a_state_keeper)
    {
      a_state_keeper.settings_script.Clear();
      a_state_keeper.settings_script.Add(script_file_name);
      a_state_keeper.settings_script.Add(output_file_name);
      return true; // SUCCEESS
    }

    //=========================================================================
    /// <summary>
    /// Called each time after export is done
    /// </summary>
    public override void on_after_export()
    {      
    }

    //=========================================================================
    ///<summary>
    /// Name of script file to be ran for processing
    ///</summary>
    public string script_file_name
    {
      get { return m_script_file_name; }
      set { m_script_file_name=value; }
    }

    //=========================================================================
    ///<summary>
    /// Output file name script should write data to
    ///</summary>
    public string output_file_name
    {
      get { return m_output_file_name; }
      set { m_output_file_name=value; }
    }

    //=========================================================================
    /// <summary>
    /// Pre export input
    /// </summary>
    /// <returns>Preexport input result</returns>
    internal override enumPreInputResult pre_export_input()
    {
      if (!load_from_dialog()) return enumPreInputResult.PIR_ABORTED;
      if (!is_has_necessary_data()) return enumPreInputResult.PIR_WRONG_DATA;
      return enumPreInputResult.PIR_OK;
    }

    //=========================================================================
    /// <summary>
    /// Check is we have enough settings to start export
    /// </summary>
    /// <returns></returns>
    public override bool is_has_necessary_data()
    {
      try {
        return File.Exists(script_file_name) &&
          Directory.Exists(Path.GetDirectoryName(output_file_name));
      } catch (Exception) {
        return false;
      }
    }

    /// <summary> Name of script file to be ran for processing </summary>
    private string m_script_file_name = "";
    /// <summary> Output file name script should write data to </summary>
    private string m_output_file_name = "";

  }
}

