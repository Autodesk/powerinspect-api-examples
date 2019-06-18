//=============================================================================
//
//D StateKeeper class
//
// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using pi = PowerINSPECT;
using System.Text;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// Stores export properties
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("D60337FA-4BA2-403A-A52D-ABF6FE641EFF")
  ]
  public class StateKeeper
  {
    
    #region Constants

    //=============================================================================
    /// <summary>
    /// Current version of configuration file
    /// </summary>
    private const int CURRENT_CONFIG_VERSION = 6;
    #endregion

    #region Methods

    //=============================================================================
    /// <summary>
    /// StateKeeper class constructor
    /// </summary>
    internal StateKeeper()
    {
      
      m_reportVariables = new StringCollection();
      m_reportMeasures = new StringCollection();
      m_exportedMeasures = new StringCollection();
      m_exportOptions = new StringCollection();
      m_savedReportMeasures = new StringCollection();
      m_savedExportedMeasures = new StringCollection();
      m_settings_csv = new StringCollection();
      m_settings_sdd = new StringCollection();
      m_settings_qdas = new StringCollection();
      m_settings_script = new StringCollection();
      m_catalog_information = new StringCollection();

      m_storedCollections = new ArrayList();

      m_storedCollections.Add(m_reportVariables);
      m_storedCollections.Add(m_reportMeasures);
      m_storedCollections.Add(m_exportedMeasures);
      m_storedCollections.Add(m_exportOptions);
      m_storedCollections.Add(m_savedReportMeasures);
      m_storedCollections.Add(m_savedExportedMeasures);
      m_storedCollections.Add(m_settings_csv);
      m_storedCollections.Add(m_settings_sdd);
      m_storedCollections.Add(m_settings_qdas);
      m_storedCollections.Add(m_settings_script);
      m_storedCollections.Add(m_catalog_information);

    }

    //==============================================================================
    /// <summary>
    /// 
    /// </summary>
    internal void init(bool aEnabledGUI)
    {
      pi.Document doc = PIConnector.instance.active_document;
      Debug.Assert(doc!=null, "StateKeeper.Init() : ActiveDocument is null" );
      if (doc==null) return;
      m_fileName = doc.FullPathName+"_spc";
      m_enableGUI = aEnabledGUI;
    }

    //=========================================================================
    /// <summary>
    /// Init state keeper (useful for Automation)
    /// </summary>
    /// <param name="a_file_name">File name</param>
    internal void init(string a_file_name)
    {
      m_fileName = a_file_name;
      m_enableGUI = false;
    }

    //=============================================================================
    /// <summary>
    /// Saves current state of AddIn in file
    /// </summary>
    internal void save()
    {
      try {
        if (File.Exists(m_fileName)) {
          FileAttributes fa = File.GetAttributes(m_fileName);
          if ((fa & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
            if (
              (m_enableGUI) &&
              (MessageBox.Show(
                LS.Lc(String.Format("SPC state file {0} has a Read-only attribute.\nDo you want to overwrite it?", m_fileName)),
                LS.Lc("Attention"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
              ) == DialogResult.Yes)
            ) {
              // reset read-only attribute
              File.SetAttributes(m_fileName, fa & (~FileAttributes.ReadOnly));
            } else {
              // state will not be saved
              return;
            }
          }
        }

        FileStream fs = new FileStream(m_fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
        Debug.Assert(fs != null, "StateKeeper.Save() : can't create FileStream");
        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
        Debug.Assert(sw != null, "StateKeeper.Save() : can't create StreamWriter");

        // Current version of configuration file
        sw.WriteLine(CURRENT_CONFIG_VERSION);
        // Number of stored collections
        sw.WriteLine(m_storedCollections.Count);

        foreach (StringCollection sc in m_storedCollections) {
          sw.WriteLine(sc.Count);
          foreach (string s in sc) {
            sw.WriteLine("\"" + s + "\"");
          }
        }

        sw.Close();
        fs.Close();
      } catch (System.UnauthorizedAccessException) {
        MessageBox.Show(String.Format(
          LS.Lc("SPC Add-In couldn't create auxiliary file {0} next to PowerInspect document.\nThis file is used to remember what data has been recently exported.\n* Make sure you have enough rights\n* Move document file outside of Program Files folder if you have UAC enabled"),
          m_fileName
        ));
      } catch (Exception) {
        MessageBox.Show(String.Format(
          LS.Lc("SPC Add-In couldn't create auxiliary file {0}"),
          m_fileName
        ));
      }
    }

    //=============================================================================
    /// <summary>
    /// Loads current state of AddIn from file
    /// </summary>
    internal void load()
    {

      if (!File.Exists(m_fileName)) return;

      StreamReader sr = new StreamReader(m_fileName, System.Text.Encoding.UTF8, true);

      try {

        Debug.Assert(sr!=null,"StateKeeper.Load() : can't create StreamReader");

        m_version_of_spc_config = int.Parse(sr.ReadLine());

        if (m_version_of_spc_config < CURRENT_CONFIG_VERSION) {
          if (m_enableGUI) {
            if (
                MessageBox.Show(
                  LS.Lc("SPC settings file associated with this project was created by previous version of SPC add-in.\nWould you like to try to load information from this file?"),
                  LS.Lc("Warning"),
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Exclamation
                ) == DialogResult.No
            ) {
              sr.Close();
              return;
            }
          }
        } else if (m_version_of_spc_config > CURRENT_CONFIG_VERSION) {
          if (m_enableGUI) {
            MessageBox.Show(
              LS.Lc("SPC settings file associated with this project was created by newer version of SPC add-in\nIt's impossible to continue the work."),
              LS.Lc("Warning"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation
            );          
            sr.Close();
            return;          
          }
        }

        int collectionCount = int.Parse(sr.ReadLine());

        for (int i=0; i<collectionCount; i++)
        {
          StringCollection sc = m_storedCollections[i] as StringCollection;
          sc.Clear();
          int elemCount = int.Parse(sr.ReadLine());
          for (int j=0; j<elemCount; j++)
          {
            string sTmp = sr.ReadLine();
            sTmp = sTmp.TrimStart('"');
            sTmp = sTmp.TrimEnd('"');
            // i know this looks awful... 
            // this should be removed when configuration file
            // will be converted to XML format
            if ((i <= 2) || ((i >= 4) && (i <= 5))) {
              sTmp = sTmp.Replace(" ", "_");
            }
            sc.Add(sTmp);
          }
        }
      } finally {
        sr.Close();
      }
    }

    #endregion

    #region Fields
    
    /// <summary> file name of SPC AddIn configuration for current document </summary>
    private string m_fileName;

    /// <summary> holds Keys of ReportItems which selected for export </summary>
    private StringCollection m_reportVariables;

    /// <summary> holds measures IDs that was selected! </summary>
    private StringCollection m_reportMeasures;

    /// <summary> holds measures IDs that was exported before! (restored by StateKeeper) </summary>
    private StringCollection m_exportedMeasures;

    /// <summary> export options collection (e.g. file format, custom script name, `export to file` flag) </summary>
    private StringCollection m_exportOptions;

    /// <summary> holds an image of m_reportMeasures </summary>
    private StringCollection m_savedReportMeasures;

    /// <summary> holds an image of m_exportedMeasures </summary>
    private StringCollection m_savedExportedMeasures;

    /// <summary> CVS export settings  </summary>
    private StringCollection m_settings_csv;
    /// <summary> Lighthouse SPC export settings </summary>
    private StringCollection m_settings_sdd;
    /// <summary> Q-DAS export settings </summary>
    private StringCollection m_settings_qdas;
    /// <summary> Script export settings </summary>
    private StringCollection m_settings_script;
    /// <summary> Q-DAS catalog information </summary>
    private StringCollection m_catalog_information;

    /// <summary> collection of all stored collections </summary>
    private ArrayList m_storedCollections;

    /// <summary> flag indicates that GUI enabled </summary>
    private bool m_enableGUI;

    /// <summary> The version of SPC configuration file was loaded the last time </summary>
    private int m_version_of_spc_config = 0;

    #endregion

    #region Properties

    //=============================================================================
    /// <summary>
    /// Keys of ReportItems which selected for export
    /// </summary>
    public StringCollection ReportVariables
    {
      get
      {
        return m_reportVariables;
      }
    }

    //=============================================================================
    /// <summary>
    /// measures IDs that was selected!
    /// </summary>
    public StringCollection ReportMeasures
    {
      get
      {
        return m_reportMeasures;
      }
    }

    //=============================================================================
    /// <summary>
    /// measures IDs that was exported before! (restored by StateKeeper)
    /// </summary>
    public StringCollection ExportedMeasures
    {
      get {
        return m_exportedMeasures;
      }
    }

    //=============================================================================
    /// <summary>
    /// options collection (e.g. file format, custom script name, `export to file` flag)
    /// </summary>
    public StringCollection ExportOptions
    {
      get {
        return m_exportOptions;
      }
    }

    //=========================================================================
    /// <summary>
    /// CVS export settings
    /// </summary>
    public StringCollection settings_csv
    {
      get {
        return m_settings_csv;
      }
    }

    //=========================================================================
    /// <summary>
    /// Lighthouse SPC export settings
    /// </summary>
    public StringCollection settings_sdd
    {
      get {
        return m_settings_sdd;
      }
    }

    //=========================================================================
    /// <summary>
    /// Q-DAS export settings
    /// </summary>
    public StringCollection settings_qdas
    {
      get {
        return m_settings_qdas;
      }
    }

    //=========================================================================
    /// <summary>
    /// Script export settings
    /// </summary>
    public StringCollection settings_script
    {
      get {
        return m_settings_script;
      }
    }

    //=========================================================================
    /// <summary>
    /// Q-DAS catalogs information
    /// </summary>
    public StringCollection catalog_information
    {
      get {
        return m_catalog_information;
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns the version of the configuration file was loaded last time
    /// </summary>
    public int version_of_spc_config
    {
      get {
        return m_version_of_spc_config;
      }      
    }

    #endregion


  } // end class StateKeeper

} //end namespace Autodesk.PowerInspect.AddIns.SPC

