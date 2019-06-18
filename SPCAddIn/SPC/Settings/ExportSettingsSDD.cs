//=========================================================================
//D ExportSettingsSDD.cs
//
// SDD export settings
//
// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
//-------------------------------------------------------------------------


using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=======================================================================
  /// <summary>
  /// SDD export settings
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("EF8C2229-0276-44FC-98DA-46FD210CEA94")
  ]
  public class ExportSettingsSDD : ExportSettings
  {

    //=====================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal ExportSettingsSDD()
      : base()
    {
    }

    //=====================================================================
    /// <summary>
    /// Load settings via dialog
    /// </summary>
    /// <returns></returns>
    public override bool load_from_dialog()
    {
      using (FormExportSettingsSDD fes = new FormExportSettingsSDD()) {
        // restore state we had before
        try {
          fes.m_textbox_output_file.Text = output_file_name_base;
          fes.m_textbox_output_folder.Text = output_folder;
          if (fes.ShowDialog() == DialogResult.OK) {
            output_folder = fes.m_textbox_output_folder.Text;
            output_file_name_base = fes.m_textbox_output_file.Text;
            return true;
          }
        } catch (Exception) {
          // Ignore the exceptions of prop sets, because they are designed 
          // for automation users primarily.
        }
        return false;        
      }
    }

    //=====================================================================
    /// <summary>
    /// Load settings from state keeper
    /// </summary>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>True, if succeeded</returns>
    public override bool load_from_state_keeper(StateKeeper a_state_keeper)
    {
      // check if we have correct collection with settings
      if (a_state_keeper.settings_sdd.Count == 0) return false;
      if (a_state_keeper.version_of_spc_config > 6) {
        Debug.Fail("Please take a look at load_from_state_keeper. It's out of date now.");
        return false;
      } else if (a_state_keeper.version_of_spc_config == 6) {
        output_folder = a_state_keeper.settings_sdd[0];
        output_file_name_base = a_state_keeper.settings_sdd[1];
      } else {
        // the oldest version
        // new version because of pinspect#9398        
        string full_fn = a_state_keeper.settings_sdd[0];
        string folder = Path.GetDirectoryName(full_fn);
        if (Directory.Exists(folder)) {
          string file_name = Path.GetFileName(full_fn);
          if (file_name.Length > 0) {
            output_folder = folder;
            output_file_name_base = file_name;
          }          
        }
      }

      //-- Sanity checks

      // Check that directory exists
      if (!Directory.Exists(output_folder)) {
        output_folder = ""; // reset directory, to force user to pick a new one
      }
      
      return true;
    }

    //=====================================================================
    /// <summary>
    /// Save settings using state keeper
    /// </summary>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>True, if succeeded</returns>
    public override bool save_to_state_keeper(StateKeeper a_state_keeper)
    {
      a_state_keeper.settings_sdd.Clear();
      a_state_keeper.settings_sdd.Add(output_folder);
      a_state_keeper.settings_sdd.Add(output_file_name_base);
      return true;
    }

    //=====================================================================
    /// <summary>
    /// Called each time after export is done
    /// </summary>
    public override void on_after_export()
    {
    }

    //=====================================================================
    ///<summary>
    /// The base name for created files.
    /// Say, if you put "myfile" here, the Lighthouse software will create 
    /// following files:
    /// myfile.000, myfile.001
    ///</summary>
    public string output_file_name_base
    {
      get { return m_output_file_name_base; }
      set {
        if (is_correct_file_name(value)) {
          m_output_file_name_base = value; 
        } else {
          throw new Exception("'output_file_name_base' property cannot be set to incorrect file name or to empty string");
        }
      }
    }

    //=====================================================================
    /// <summary>
    /// The output folder where Lighthouse will create all the files
    /// </summary>
    public string output_folder
    {
      get { return m_output_folder; }
      set { m_output_folder = value; }
    }

    //=====================================================================
    /// <summary>
    /// Pre export input
    /// </summary>
    /// <returns>Pre export input result</returns>
    internal override enumPreInputResult pre_export_input()
    {
      if (!is_has_necessary_data()) return enumPreInputResult.PIR_WRONG_DATA;
      return enumPreInputResult.PIR_OK;
    }

    //=====================================================================
    /// <summary>
    /// Check if we have enough settings to start export
    /// </summary>
    /// <returns></returns>
    public override bool is_has_necessary_data()
    {      
      try {
        
        return is_correct_file_name(output_file_name_base) &&
          Directory.Exists(output_folder);
      } catch(Exception) {
        return false;
      }
    }

    //=========================================================================
    /// <summary>
    /// Checks if the provided file name is ok for the current operating system
    /// </summary>
    /// <param name="a_file_name">A proposed name of the file</param>
    /// <returns>True - fiel name is correct. False - otherwise</returns>
    private bool is_correct_file_name(string a_file_name)
    {
      // first, we must ignore the empty file names
      if (0 == a_file_name.Length) return false;
      // now, get the list of system's restricted characters for file names
      // and check whether those characters are in a_file_name
      char[] invalid_chars = Path.GetInvalidFileNameChars();
      foreach (char c in invalid_chars) {
        if (a_file_name.Contains(c.ToString())) {
          // yes, we matched the "bad" character
          return false;
        }
      }
      // all characters in the file name are match the locale requirements
      return true;
    }

    /// <summary> the name of output file (w/o the extension) </summary>
    private string m_output_file_name_base = "";
    /// <summary> the name of output folder </summary>
    private string m_output_folder = "";

  }  
}

