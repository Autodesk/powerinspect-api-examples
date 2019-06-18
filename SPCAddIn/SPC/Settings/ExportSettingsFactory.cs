//=============================================================================
//D ExportSettingsFactory.cs
//
// Export settings factory
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
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Export settings factory
  /// </summary>
  internal class ExportSettingsFactory
  {
    
    //=========================================================================
    /// <summary>
    /// Create an instance of Export settings class
    /// </summary>
    /// <param name="a_export_type">Type of class to be created</param>
    /// <returns>Export settings class instance</returns>
    internal static ExportSettings create(SPCExportType a_export_type)
    {
      switch(a_export_type) {
        case SPCExportType.SPC_ET_CSV: return new ExportSettingsCSV();
        case SPCExportType.SPC_ET_SDD: return new ExportSettingsSDD();
        case SPCExportType.SPC_ET_QDAS: return new ExportSettingsQDAS();
        case SPCExportType.SPC_ET_SCRIPT: return new ExportSettingsScript();
        default: {
          Debug.Fail("should not happen");
          return null;
        }
      }
    }
  }
}

