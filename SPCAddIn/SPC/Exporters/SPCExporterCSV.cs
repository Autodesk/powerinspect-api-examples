//=============================================================================
//
//D SPCExporterCSV class
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
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;

using pi = PowerINSPECT;
using System.Diagnostics;
using System.Globalization;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //=============================================================================
  /// <summary>
  /// SPCExporterCSV class - implements exporter to `Comma Separated Values` format
  /// </summary>
  [
    Guid("5A0E7CBD-4089-4F3B-8D4A-1905E8C94BAF"),
    ClassInterface(ClassInterfaceType.None),
    ComVisible(true)
  ]
  public sealed class SPCExporterCSV : SPCExporter, ISPCExporter
  {

    #region Constants

    readonly string cvs_REPORT_VARIABLES_SEPARATOR;

    #endregion

    #region Methods

    //=============================================================================
    /// <summary>
    /// SPCExporterCSV class constructor
    /// </summary>    
    /// <param name="a_export_settings">Export settings</param>
    /// <param name="a_measures">Measures to export</param></param>
    /// <param name="a_report_items">Report items (variables / characteristics)</param>
    internal SPCExporterCSV(
      ExportSettingsCSV a_export_settings,
      MeasureCollection a_measures,
      ReportItemCollection a_report_items
    ) : base(a_measures, a_report_items)
    {
      export_settings = a_export_settings;
      cvs_REPORT_VARIABLES_SEPARATOR = CultureInfo.CurrentCulture.TextInfo.ListSeparator;      
    }

    //=============================================================================
    /// <summary>
    /// Do the export to CSV format file
    /// </summary>
    /// <returns>True - if data was successfully exported</returns>
    public override bool Export()
    {
      if (m_measures==null) {
        MessageBox.Show(
          LS.Lc("There are no measures to export"),
          LS.Lc("Attention"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
        return false;
      }

      if (m_reportItems==null) {
        MessageBox.Show(
          LS.Lc("There are no variables to export"),
          LS.Lc("Attention"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
        return false;
      }

      if ((null == export_settings) || (export_settings.output_file_name.Length == 0)) {
        MessageBox.Show(
          LS.Lc("Destination file name not specified"),
          LS.Lc("Attention"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
        return false;
      }

      StringCollection sc = new StringCollection();

      foreach (pi.IMeasure m in m_measures) {
        string s = "";
        foreach (IReportItem ri in m_reportItems) {
          ri.Measure = m;
          if (!ri.HasValidValue) {
            MessageBox.Show(
              String.Format(
                LS.Lc("The item {0} is not measured for {1}.\nNo data will be written to the file until you measure required item,\n or remove it from export list"), 
                ri.SequenceItem.Name, 
                m.Name
              ) ,
              LS.Lc("Attention"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation
            );
            return false;
          }
          double dVal = ri.Value;
          s += format_double(dVal,ri.NumberOfDigits) + cvs_REPORT_VARIABLES_SEPARATOR;
        }

        // remove last occurrence of separator
        s = s.Remove(
          s.LastIndexOf(cvs_REPORT_VARIABLES_SEPARATOR),
          cvs_REPORT_VARIABLES_SEPARATOR.Length
        );

        // add string to collection
        sc.Add(s);
      }

      // output strings to CSV file
      StreamWriter sw = null;
      try {
        sw = File.CreateText(export_settings.output_file_name);
      } catch (Exception) {
        MessageBox.Show(
          String.Format(
            LS.Lc("Error occurred while opening file {0}. Probably file is locked by other application."),
            export_settings.output_file_name
          ),
          LS.Lc("Error"),
          MessageBoxButtons.OK
        );
        return false;
      }
      
      try {
        foreach (string s in sc) {
          sw.WriteLine(s);
        }
      } finally {
        // flush & close
        sw.Close();
      }

      return true;

    }

    #endregion

    #region Fields

    private ExportSettingsCSV m_export_settings;

    #endregion

    #region Properties

    public ExportSettingsCSV export_settings
    {
      get {
        return m_export_settings;
      }
      set {
        Debug.Assert(null != value, "Export settings can't be set to null");
        // set only if value is not null
        if (null != value) {
          m_export_settings = value;
        }
      }
    }
    #endregion

  } // end class SPCExporterCSV

} // end namespace Autodesk.PowerInspect.AddIns.SPC

