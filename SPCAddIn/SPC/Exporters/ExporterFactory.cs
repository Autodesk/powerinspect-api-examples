//=============================================================================
//D ExporterFactory.cs
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
  /// Exporter factory
  /// </summary>
  internal class ExporterFactory
  {
    //=========================================================================
    /// <summary>
    /// Creates exporter class instance of specific type
    /// </summary>
    /// <param name="a_exporter_type">Type of exporter</param>
    /// <param name="a_export_settings">Export settings</param>
    /// <param name="a_measures">Measures to export</param>
    /// <param name="a_report_items">Report items to export</param>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>Exporter instance</returns>
    internal static SPCExporter create(
      SPCExportType a_exporter_type, 
      ExportSettings a_export_settings,
      MeasureCollection a_measures,
      ReportItemCollection a_report_items,
      StateKeeper a_state_keeper
    )
    {
      switch (a_exporter_type) {
        case SPCExportType.SPC_ET_CSV: 
          return new SPCExporterCSV(a_export_settings as ExportSettingsCSV,a_measures,a_report_items);
        case SPCExportType.SPC_ET_SDD:
          return new SPCExporterSDD(a_export_settings as ExportSettingsSDD, a_measures, a_report_items);
        case SPCExportType.SPC_ET_QDAS: {
            if (null != a_state_keeper) {
              return new SPCExporterQDAS(a_export_settings as ExportSettingsQDAS, a_measures, a_report_items, a_state_keeper.ReportVariables);
            } else {
              return new SPCExporterQDAS(a_export_settings as ExportSettingsQDAS, a_measures, a_report_items, null);
            }            
          }
        case SPCExportType.SPC_ET_SCRIPT:
          return new SPCExporterCustomScript(a_export_settings as ExportSettingsScript, a_measures, a_report_items);
        default: {
            Debug.Fail("wrong enumeration value");
            return null;
          }
      }
    }
  }
}

