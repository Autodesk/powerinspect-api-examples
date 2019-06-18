//=============================================================================
//D ExportSettings.cs
//
// Export settings (base class)
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
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Pre-export dialog result
  /// </summary>
  internal enum enumPreInputResult {
    PIR_OK,
    PIR_ABORTED,
    PIR_WRONG_DATA
  };

  //===========================================================================
  /// <summary>
  /// Base class for all export settings
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("82D13DCF-DFBF-43D2-9913-863D0D8B4994")
  ]
  public abstract class ExportSettings
  {
    //=========================================================================
    /// <summary>
    /// Private constructor
    /// </summary>
    protected ExportSettings()
    {

    }

    //=========================================================================
    /// <summary>
    /// Load settings from dialog
    /// </summary>
    /// <returns>True, is succeeded</returns>
    public abstract bool load_from_dialog();

    //=========================================================================
    /// <summary>
    /// Load settings from state keeper
    /// </summary>
    /// <param name="a_state_keeper">state keeper</param>
    /// <returns>True, if succeded</returns>
    public abstract bool load_from_state_keeper(StateKeeper a_state_keeper);

    //=========================================================================
    /// <summary>
    /// Save settings using state keeper
    /// </summary>
    /// <param name="a_state_keeper">state keeper</param>
    /// <returns>True, if succeded</returns>
    public abstract bool save_to_state_keeper(StateKeeper a_state_keeper);

    //=========================================================================
    /// <summary>
    /// Called after successful export has been done
    /// </summary>
    public abstract void on_after_export();

    //=========================================================================
    /// <summary>
    /// Check if we necessary settings data is present. Use GUI to grab data.
    /// </summary>
    /// <param name="a_can_use_gui">Can use GUI to grab settings</param>
    /// <returns>TRUE, if all necessary data is present</returns>
    public bool check_for_necessary_data(bool a_can_use_gui)
    {
      if (is_has_necessary_data()) return true;
      if (!a_can_use_gui) return false;
      if (!load_from_dialog()) return false;
      return is_has_necessary_data();
    }

    //=========================================================================
    /// <summary>
    /// Called before each export session
    /// </summary>
    /// <returns>Pre-export dialog result</returns>
    internal abstract enumPreInputResult pre_export_input();

    //=========================================================================
    /// <summary>
    /// Check if we necessary settings data is present
    /// </summary>
    /// <returns>TRUE, if all necessary data is present</returns>
    public abstract bool is_has_necessary_data();

    //=========================================================================
    /// <summary>
    /// Cut string at a_max_len characters
    /// </summary>
    /// <param name="a_str">Source string</param>
    /// <param name="a_max_len">Max allowed length</param>
    /// <returns>Cutted string</returns>
    protected string cut_str(string a_str, int a_max_len)
    {
      if (a_str.Length <= a_max_len) return a_str;
      return a_str.Substring(0, a_max_len);
    }

  }
}

