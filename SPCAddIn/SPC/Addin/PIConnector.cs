//=============================================================================
//
//D SPCAddin class. Implements PSAddinManager.IPowerSolutionAddIn interface
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using pi = PowerINSPECT;
using piAM = PSAddinManager;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  
  [
  ComVisible(true)
  ]
  public enum SPCExportType {
    SPC_ET_CSV = 0,
    SPC_ET_SDD = 1,
    SPC_ET_QDAS = 2,
    SPC_ET_SCRIPT = 3
  }


  //=============================================================================
  /// <summary>
  /// IPowerInspectSPCAddIn interface for SPC Add-In. 
  /// Used to do export through automation.
  /// </summary>
  [
    Guid("B4D849D9-6CA0-4437-B029-E2D107EF292F"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)  
  ]
  public interface IPowerInspectSPCAddIn{
    void Show();
    object GetReportItems([In] object a_state_keeper);
    object GetMeasures([In] object a_state_keeper);

    object GetAllReportItems();
    object GetAllMeasures();

    bool Export(
      [In] int a_export_type,
      [In] object a_export_settings,
      [In] object a_report_items,
      [In] object a_measures,
      [In] object a_state_keeper
    );

    bool ExportWOState(
      [In] int a_export_type,
      [In] object a_export_settings,
      [In] object a_report_items,
      [In] object a_measures
    );

    object GetStateKeeper();
    object GetStateKeeper([In] string a_pwi_spc_file_name);
    object CreateExportSettings([In] int a_export_type);

  }

  
  
  //=========================================================================
  /// <summary>
  /// SPC AddIn. IPowerSolutionAddIn implementation class.
  /// </summary>
  [
    Guid("05A5DBE5-287B-403A-B05B-1AFB39E67532"),
    ClassInterface(ClassInterfaceType.None),
    ComVisible(true)    
  ]
  public sealed class PIConnector : PIConnectorBase, IPowerInspectSPCAddIn
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    public PIConnector()
      : base()
    {
      m_instance = this;
    }

    //=============================================================================
    /// <summary>
    /// Destructor
    /// </summary>
    ~PIConnector()
    {
      m_instance = null;
    }

    //=========================================================================
    /// <summary>
    /// Shows dialog
    /// </summary>
    public void Show()
    {
      if (null == active_document) {
        MessageBox.Show(
          LS.Lc("Please open at least one document"),
          LS.Lc("Warning"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
        return;
      }
      
      if(!active_document.Saved) {
        if (
          MessageBox.Show(
            LS.Lc("It is impossible to export from unsaved document.\nDo you want to save it now and continue to export?"),
            LS.Lc("Warning"),
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation
          ) == DialogResult.Yes
        ) {
          active_document.Save();
        }
        if (!active_document.Saved) return;
      }

      // create main AddIn dialog
      using (FormSPCAddIn frmAddIn = new FormSPCAddIn()) {
        // show em as modal. we don't care about the results.. but
        // if ShowDialog() returns OK then, probably, all done fine.
        frmAddIn.ShowDialog();
      }

    } // end Show()


    
    //=============================================================================
    /// <summary>
    /// Gets sequence tree as XTreeNodeCollection type object
    /// </summary>
    /// <returns></returns>
    private XTreeNodeCollection GetSequenceTree()
    {
      return GathererElementsTree.GatherTree();
    }

    //=============================================================================
    /// <summary>
    /// Gets all available report items from sequence tree
    /// </summary>
    /// <returns></returns>
    public ReportItemCollection GetAllReportItems()
    {
      return GathererReportItemCollection.GetReportItems(GetSequenceTree(), null);
    }

    //=============================================================================
    /// <summary>
    /// Gets all available report items from sequence tree
    /// </summary>
    /// <returns></returns>
    public MeasureCollection GetAllMeasures()
    {
      return GathererMeasureCollection.GetMeasureCollection(null);
    }
    

    
    //=============================================================================
    /// <summary>
    /// Gets report items from sequence tree
    /// </summary>
    /// <returns></returns>
    public ReportItemCollection GetReportItems(StateKeeper a_state_keeper)
    {      
      return GathererReportItemCollection.GetReportItems(GetSequenceTree(),a_state_keeper);
    }

    //=============================================================================
    /// <summary>
    /// gets all measures except simulated
    /// </summary>
    /// <returns></returns>
    public MeasureCollection GetMeasures(StateKeeper a_state_keeper)
    {      
      return GathererMeasureCollection.GetMeasureCollection(a_state_keeper);
    }


    //=============================================================================
    /// <summary>
    /// Gets StateKeeper and loads settings
    /// </summary>
    /// <returns>Initialized and loaded StateKeeper object</returns>
    public StateKeeper GetStateKeeper()
    {
      StateKeeper stateKeeper = new StateKeeper();
      // Init & disable GUI
      stateKeeper.init(false);
      // Load state
      stateKeeper.load();

      return stateKeeper;
    }

    //=============================================================================
    /// <summary>
    /// Gets StateKeeper and loads settings
    /// </summary>
    /// <returns>Initialized and loaded StateKeeper object</returns>
    public StateKeeper GetStateKeeper(string a_pwi_spc_file_name)
    {
      StateKeeper stateKeeper = new StateKeeper();
      // Init & disable GUI
      stateKeeper.init(a_pwi_spc_file_name);
      // Load state
      stateKeeper.load();

      return stateKeeper;
    }

    //==========================================================================
    /// <summary>
    /// Creates export settings object instance using the factory template
    /// </summary>
    /// <param name="a_export_type"></param>
    /// <returns></returns>
    public ExportSettings CreateExportSettings(SPCExportType a_export_type)
    {
      return ExportSettingsFactory.create(a_export_type);
    }

    //==========================================================================
    /// <summary>
    /// Does the export using the state got from StateKeeper
    /// </summary>
    /// <param name="a_export_type">Type of export</param>
    /// <param name="a_export_settings">Setting for exporter</param>
    /// <param name="a_report_items">Report items to export</param>
    /// <param name="a_measures">Measures to export</param>
    /// <param name="a_state_keeper">State keeper instance</param>
    /// <returns>TRUE if succeeded</returns>
    public bool Export(
      SPCExportType a_export_type, 
      ExportSettings a_export_settings, 
      ReportItemCollection a_report_items,
      MeasureCollection a_measures,
      StateKeeper a_state_keeper
    )
    {
      // check if we have enough settings
      if (!a_export_settings.check_for_necessary_data(false)) return false;
      // create exporter instance
      SPCExporter exporter = ExporterFactory.create(
        a_export_type,
        a_export_settings,
        a_measures,
        a_report_items,
        a_state_keeper
      );      
      if (null == exporter) return false;
      // do export
      return exporter.Export();
    }

    //==========================================================================
    /// <summary>
    /// Export w/o using the state from StateKeeper
    /// </summary>
    /// <param name="a_export_type">Type of export</param>
    /// <param name="a_export_settings">Settings for exporter</param>
    /// <param name="a_report_items">Report items to export</param>
    /// <param name="a_measures">Measures to export</param>
    /// <returns>TRUE if succeeded</returns>
    public bool ExportWOState(
      SPCExportType a_export_type,
      ExportSettings a_export_settings,
      ReportItemCollection a_report_items,
      MeasureCollection a_measures
    )
    {
      // check if we have enough settings
      if (!a_export_settings.check_for_necessary_data(false)) return false;
      // create exporter instance
      SPCExporter exporter = ExporterFactory.create(
        a_export_type,
        a_export_settings,
        a_measures,
        a_report_items,
        null
      );
      if (null == exporter) return false;
      // do export
      return exporter.Export();
    }    

    #endregion
    #region Properties

    //==========================================================================
    /// <summary>
    /// Returns PIConnector instance
    /// </summary>
    internal static PIConnector instance
    {
      get { return m_instance; }
    }
    #endregion


    #region IPowerInspectSPCAddIn Members

    //==========================================================================
    /// <summary>
    /// Get report items collection (list is provided by State Keeper)
    /// </summary>
    /// <param name="a_state_keeper">State keeper instance</param>
    /// <returns>Report items</returns>
    object IPowerInspectSPCAddIn.GetReportItems(object a_state_keeper)
    {
      return this.GetReportItems(a_state_keeper as StateKeeper);
    }

    //==========================================================================
    /// <summary>
    /// Get measures collection (list is provided by State Keeper)
    /// </summary>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>Measures collection</returns>
    object IPowerInspectSPCAddIn.GetMeasures(object a_state_keeper)
    {
      return this.GetMeasures(a_state_keeper as StateKeeper);
    }

    //==========================================================================
    /// <summary>
    /// Get all report items
    /// </summary>
    /// <returns></returns>
    object IPowerInspectSPCAddIn.GetAllReportItems()
    {
      return this.GetAllReportItems();
    }

    //==========================================================================
    /// <summary>
    /// Get all measures
    /// </summary>
    /// <returns></returns>
    object IPowerInspectSPCAddIn.GetAllMeasures()
    {
      return this.GetAllMeasures();
    }

    //==========================================================================
    /// <summary>
    /// Export using the state from State Keeper
    /// </summary>
    /// <param name="a_export_type">Type of export</param>
    /// <param name="a_export_settings">Exporter settings</param>
    /// <param name="a_report_items">Report items</param>
    /// <param name="a_measures">Measures</param>
    /// <param name="a_state_keeper">State Keeper</param>
    /// <returns>TRUE, if succeeded</returns>
    bool IPowerInspectSPCAddIn.Export(int a_export_type, object a_export_settings, object a_report_items, object a_measures, object a_state_keeper)
    {
      return this.Export(
        (SPCExportType)a_export_type, 
        a_export_settings as ExportSettings, 
        a_report_items as ReportItemCollection, 
        a_measures as MeasureCollection, 
        a_state_keeper as StateKeeper
     );
    }

    //==========================================================================
    /// <summary>
    /// Export using the state from State Keeper
    /// </summary>
    /// <param name="a_export_type">Type of export</param>
    /// <param name="a_export_settings">Exporter settings</param>
    /// <param name="a_report_items">Report items</param>
    /// <param name="a_measures">Measures</param>
    /// <returns>TRUE, if succeeded</returns>
    bool IPowerInspectSPCAddIn.ExportWOState(int a_export_type, object a_export_settings, object a_report_items, object a_measures)
    {
      return this.ExportWOState(
        (SPCExportType)a_export_type,
        a_export_settings as ExportSettings,
        a_report_items as ReportItemCollection,
        a_measures as MeasureCollection      
      );
    }

    //==========================================================================
    /// <summary>
    /// Show addin main dialog
    /// </summary>
    void IPowerInspectSPCAddIn.Show()
    {
      this.Show();
    }

    //==========================================================================
    /// <summary>
    /// Returns StateKeeper object
    /// </summary>
    /// <returns>State</returns>
    object IPowerInspectSPCAddIn.GetStateKeeper()
    {
      return this.GetStateKeeper();
    }

    //==========================================================================
    /// <summary>
    /// Get state keeper object for PWI session
    /// </summary>
    /// <param name="a_pwi_spc_file_name"></param>
    /// <returns></returns>
    object IPowerInspectSPCAddIn.GetStateKeeper(string a_pwi_spc_file_name)
    {
      return this.GetStateKeeper(a_pwi_spc_file_name);
    }

    //==========================================================================
    /// <summary>
    /// Create export 
    /// </summary>
    /// <param name="a_export_type">Export settings object type</param>
    /// <returns>Export settings object</returns>
    object IPowerInspectSPCAddIn.CreateExportSettings(int a_export_type)
    {
      return this.CreateExportSettings((SPCExportType)a_export_type);
    }
    #endregion

    //==========================================================================
    /// <summary>
    /// Create command buttons
    /// </summary>
    protected override void CreateCommandBarButtons()
    {
      AddCommandBarButton(
        "SPC",
        Show,
        LS.Lc("SPC Export\nOpen SPC export dialog\nOpen SPC export dialog"),
        0
     );
    }

    /// <summary>
    /// PIConnector instance
    /// </summary>
    private static PIConnector m_instance;

  } // end class PIConnector

} // end namespace Autodesk.PowerInspect.AddIns.SPC

