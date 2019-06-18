//=============================================================================
//
//D SPCExporterCustomScript class
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
using System.Runtime.InteropServices;

using pi = PowerINSPECT;
using System.Diagnostics;


namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// SPCExporterCSV class - implements exporter that use script engine to format
  /// output
  /// </summary>
  [
    Guid("D1A05173-D2F4-4B14-A021-30E5ED1197E5"),
    ClassInterface(ClassInterfaceType.None),
    ComVisible(true)
  ]
  public sealed class SPCExporterCustomScript : SPCExporter, ISPCExporter
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// SPCExporterCustomScript class constructor
    /// </summary>
    /// <param name="aScriptFileName">Name of script file</param>
    /// <param name="aOutputFileName">Name of output file, produced by script</param>
    /// <param name="aMeasures">Measures to export</param></param>
    /// <param name="aReportItems">Report items (variables / characteristics)</param>
    internal SPCExporterCustomScript(
      ExportSettingsScript a_export_settings,
      MeasureCollection aMeasures,
      ReportItemCollection aReportItems
    ) : base(aMeasures, aReportItems)
    {
      export_settings = a_export_settings;      
    }

    //=============================================================================
    /// <summary>
    /// Do the export to QDAS SPC
    /// </summary>
    /// <returns>True - if data was successfully exported</returns>
    public override bool Export()
    {
      // cerate an instance of script manager
      ScriptManager sm = new ScriptManager();
      // load source code from file. in case of failure - return false
      if (!sm.LoadSourceFromFile(export_settings.script_file_name)) return false;
      return sm.InvokeMethod("FormatOutput",export_settings.output_file_name,m_measures,m_reportItems);
    }

    #endregion

    #region Fields

    private ExportSettingsScript m_export_settings;

    #endregion

    #region Properties

    public ExportSettingsScript export_settings
    {
      get
      {
        return m_export_settings;
      }
      set
      {
        Debug.Assert(null != value, "Export settings can't be set to null");
        // set only if value is not null
        if (null != value) {
          m_export_settings = value;
        }
      }
    }
    #endregion


  }
}

