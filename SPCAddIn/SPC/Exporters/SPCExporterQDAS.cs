//=============================================================================
//
//D SPCExporterQDAS class
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
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using pi = PowerINSPECT;
using System.Collections.Generic;
using System.Globalization;


namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// SuperTag class - holds additional information about tree node + 
  /// original Tag property
  /// </summary>
  internal class SuperTag
  {
    public enum enumNodeType
    {
      NT_GROUP,
      NT_CHARACTERISTIC
    }

    /// <summary> Tag </summary>
    public object Tag;
    /// <summary> Node number </summary>
    public int NodeNum;
    /// <summary> Characteristic number </summary>
    public int CharacteristicNum;
    /// <summary> Node type </summary>
    public enumNodeType NodeType;


    //=============================================================================
    /// <summary>
    /// SuperTag class constructor
    /// </summary>
    /// <param name="aTag">Original tag of the node</param>
    /// <param name="aNodeNum">number of node in QDAS description file</param>
    /// <param name="aNodeType">type of node: group or characteristic (see QDAS docs)</param>
    /// <param name="aCharacteristicNum">Ordinal number of characteristic</param>
    internal SuperTag(object aTag, int aNodeNum, enumNodeType aNodeType, int aCharacteristicNum)
    {
      Tag = aTag;
      NodeNum = aNodeNum;
      NodeType = aNodeType;
      CharacteristicNum = aCharacteristicNum;
    }

    
  }
  

  //=============================================================================
  /// <summary>
  /// SPCExporterQDAS class - implements exporter to QDAS format
  /// </summary>
  [
    Guid("547ACEBA-729B-4734-BD35-B5FB5A6DE340"),
    ClassInterface(ClassInterfaceType.None),
    ComVisible(true)
  ]
  public sealed class SPCExporterQDAS : SPCExporter, ISPCExporter
  {
    #region Constants
    /// <summary> Values divider (using in DFX files) </summary>
    private const char c_value_divider = (char)0x0f;
    /// <summary> Values subdivider (used in DFX files) </summary>
    private const char c_value_subdidiver = (char)0x14;
    #endregion

    #region Methods

    //=============================================================================
    /// <summary>
    /// SPCExporterQDAS class constructor
    /// </summary>
    /// <param name="aDirectoryName">Directory of QDAS monitoring</param>
    /// <param name="aMeasures">Measures to export</param></param>
    /// <param name="aReportItems">Report items (variables / characteristics)</param>
    internal SPCExporterQDAS(
      ExportSettingsQDAS a_export_settings,
      MeasureCollection a_measures,
      ReportItemCollection a_report_items,
      StringCollection a_prev_report_items_keys
      //OutputStrategyQDAS a_output_strategy
    ) : base(a_measures, a_report_items)
    {
      export_settings = a_export_settings;
      m_prev_report_items_keys = a_prev_report_items_keys;
    }

    //=============================================================================
    /// <summary>
    /// Do the export to QDAS SPC
    /// </summary>
    /// <returns>TRUE, if data has been successfully exported</returns>
    public override bool Export()
    {
      // the strategy is fixed yet. but in the future it can be moved out of 
      // this method and be initialized within constructor.      
      OutputStrategyQDAS output_strategy = new OutputStrategyQDASIncrementalFileNames(
        enumLockStrategy.LM_SHARE_DENY,
        export_settings
      );

      // Do we need to create DFD file?
      // if DFD-critical data has been changed since last export
      //  - then we do.
      bool is_update_description = 
        !export_settings.descriptive_part_is_up_to_date || 
        !check_report_items_are_same();

      try {
        output_strategy.begin_update(is_update_description);
        if (is_update_description) {
          export_description(output_strategy.sw_desc);
        }
        // Export raw values
        export_values(output_strategy.sw_val);
      } finally {
        output_strategy.end_update();
      }
      return true;
    }

    //==========================================================================
    /// <summary>
    /// Formats double as string with limited fractional part digits count
    /// </summary>
    /// <param name="aValue">A double value</param>
    /// <param name="aNumDigits">
    ///   A quantity of fractional digits to output.
    /// </param>
    /// <returns></returns>
    protected override string format_double(double val, long num_digits)
    {
      return format_double_custom(CultureInfo.InvariantCulture, val, num_digits);
    }

    //=========================================================================
    /// <summary>
    /// Check if report items are the same as were at previous export session
    /// </summary>
    /// <returns></returns>
    private bool check_report_items_are_same()
    {
      if (m_prev_report_items_keys == null) return false;
      if (m_reportItems.Count != m_prev_report_items_keys.Count) return false;
      int idx = 0;
      foreach(IReportItem ri in m_reportItems) {
        string s1 = ri.Key;
        string s2 = m_prev_report_items_keys[idx++];
        if (s1 != s2) return false;
      }
      return true;
    }

    //=========================================================================
    /// <summary>
    /// Export description-related information
    /// </summary>
    /// <param name="a_sw"></param>
    private void export_description(StreamWriter a_sw)
    {      
      // Export all descriptive information
      export_parts(a_sw);
      export_characreristics(a_sw);
      if (export_settings.export_structure_information) {
        export_structure_information(a_sw);
      }      
    }

    //=============================================================================
    /// <summary>
    /// Routine exports information about parts (K01xx section).
    /// Actually it's one part, which name is equals to document's name
    /// </summary>
    /// <returns>true - if everything OK</returns>
    private bool export_parts(StreamWriter a_sw)
    {
      // Mandatory fields
      a_sw.WriteLine("K0100 {0}", m_reportItems.Count);

      foreach(AdditionalField af in export_settings.dfd_fields) {
        Debug.Assert((af.required && (af.field_value!=String.Empty)) || (!af.required));
        if (af.field_value != String.Empty) {
          a_sw.WriteLine(af.k_str + " " + af.field_value);
        }
      }

      return true;
    }

    //=============================================================================
    /// <summary>
    /// Routine exports information about characteristics (in terms of Q-DAS).
    /// (Properties names in term of PowerINSPECT)
    /// </summary>
    /// <returns>true - if everything OK</returns>
    private bool export_characreristics(StreamWriter sw)
    {
      for (int i=0; i<m_reportItems.Count; i++) {
        IReportItem ri = m_reportItems[i];
        // Characteristic description
        sw.WriteLine("K2002/{0} {1}", i + 1, ri.ReportVariableName);
        // Characteristic abbreviation
        sw.WriteLine("K2003/{0} {1}", i + 1, ri.ReportVariableNameShort);
        // Characteristics type - variable (0)
        sw.WriteLine("K2004/{0} 0", i + 1);
        // Number of decimal places for characteristic
        sw.WriteLine("K2022/{0} {1}", i + 1, ri.NumberOfDigits);
        // Nominal value
        sw.WriteLine(
          "K2101/{0} {1}", 
          i + 1, 
          format_double(ri.Nominal, ri.NumberOfDigits)
        );
        // Lower tolerance value
        sw.WriteLine(
          "K2110/{0} {1}", 
          i + 1, 
          format_double(ri.Nominal + ri.LowerTolerance, ri.NumberOfDigits)
        );
        // Upper tolerance value
        sw.WriteLine(
          "K2111/{0} {1}", 
          i + 1, 
          format_double(ri.Nominal + ri.UpperTolerance, ri.NumberOfDigits)
        );
        // Output units information only if it's exists
        if (ri.UnitsStr.Length > 0) {
          sw.WriteLine("K2142/{0} {1}", i + 1, ri.UnitsStr);
        }
      }
      return true;
    }

    #region Structure information export

    //=============================================================================
    /// <summary>
    /// Routine exports information about structure of document
    /// (particularly, it's information about grouping of elements)
    /// </summary>
    /// <returns>true - if everything OK</returns>
    private bool export_structure_information(StreamWriter a_sw)
    {
      // precondition
      Debug.Assert(export_settings.export_structure_information);

      // We have only one part
      // so node #1 is part #1
      a_sw.WriteLine("K5111/1 1");

      if (export_settings.use_flat_hierarchy) {
        // just use flat hierarchy
        export_flat_hierarchy(a_sw);
      } else {
        // build & export complex hierarchy with grouping
        export_complex_hierarchy(a_sw);
      }
      return true;
    }

    //=========================================================================
    /// <summary>
    /// Export flat hierarchy (no grouping)
    /// </summary>
    /// <param name="a_sw"></param>
    private void export_flat_hierarchy(StreamWriter a_sw)
    {
      int node_index = 2; // this is because 1-st node is a "part node"
      int char_index = 1;
      foreach (IReportItem ri in m_reportItems) {
        a_sw.WriteLine("K5112/{0} {1}", node_index++, char_index++);
      }
      // hierarchy linkage
      for(int i = 1; i <= char_index-1; i++) {
        a_sw.WriteLine("K5102/1 {0}", i);
      }
    }

    //=========================================================================
    /// <summary>
    /// Build and export complex hierarchy (with grouping)
    /// </summary>
    /// <param name="a_sw"></param>
    private void export_complex_hierarchy(StreamWriter a_sw)
    {
      // Build such tree of elements (groups & variables) which includes
      // only elements that listed in `ReportItems` collection.
      // We need to use this routine because ReportItems collection does'nt contain
      // information about hierarchy in an explicit form.
      XTreeNode tnRoot = build_report_tree();
      // Swap Tag property with SuperTag, which includes some additional information 
      // about sequential index of node in the tree, it's type and characteristic
      // index (in case if it's not group node)
      tnRoot.Tag = new SuperTag(null, 1, SuperTag.enumNodeType.NT_GROUP, 0);

      // Startup values
      int nodeIndex = 2;
      int groupIndex = 1;
      int characteristicIndex = 1;

      // List all nodes in tree. Assign them index and type
      collect_groups_and_characteristics(a_sw, tnRoot, ref nodeIndex, ref groupIndex, ref characteristicIndex);

      // Write hierarchy information
      foreach (XTreeNode tn in tnRoot.Nodes) {
        link_groups(a_sw, tn);
      }
    }

    //=============================================================================
    /// <summary>
    /// Routine builds such tree of elements (groups & variables) which includes
    /// only elements that listed in `ReportItems` collection.
    /// </summary>
    /// <returns>Root node of created tree</returns>
    private XTreeNode build_report_tree()
    {
      XTreeNode tnRoot = new XTreeNode("root");
      recursive_build_report_tree(ref tnRoot,2,0);
      return tnRoot;
    }

    //=============================================================================
    /// <summary>
    /// This is a sub-routine for BuildReportTree method
    /// </summary>
    /// <param name="aParent">Parent's node</param>
    /// <param name="aLevelToScan">level of element in tree\
    /// ( or the same - element index counted from the right side of
    /// ReportItem.ReportVariableName property)</param>
    /// <param name="startIndex">start index in ReportItemCollection to process</param>
    private void recursive_build_report_tree(ref XTreeNode aParent, int aLevelToScan, int startIndex)
    {
      if (m_reportItems.Count == 0) return;
      string prevElemName = "";
      for (int i = startIndex; i < m_reportItems.Count; i++) {
        ReportItem ri = m_reportItems[i] as ReportItem;
        string parentName = get_elem_name_from_report_item(ri,aLevelToScan-1);
        if (parentName != aParent.Text) break;
        string elemName = get_elem_name_from_report_item(ri,aLevelToScan);
        if (prevElemName == elemName) continue;
        XTreeNode tn = new XTreeNode(elemName);
        bool hasChlids = get_elem_name_from_report_item(ri,aLevelToScan+1)!="";
        tn.Tag = new SuperTag(
          hasChlids?null:ri,
          0,
          hasChlids?SuperTag.enumNodeType.NT_GROUP:SuperTag.enumNodeType.NT_CHARACTERISTIC,
          1
        );
        aParent.Nodes.Add(tn);
        if (hasChlids) {
          recursive_build_report_tree(ref tn,aLevelToScan+1,i);
        }
        prevElemName = elemName;
      }
    }

    //=============================================================================
    /// <summary>
    /// Parse ReportVariableName property of ReportItem and returns aLevel's element name
    /// </summary>
    /// <param name="aReportItem">ReportItem object to process</param>
    /// <param name="aLevel">a level (from right side of name)</param>
    /// <returns>Name of the element</returns>
    private string get_elem_name_from_report_item(IReportItem aReportItem, int aLevel)
    {
      string res = "";
      string delim = ReportItem.spc_VAR_NAME_DELIMITER;
      string s = delim + aReportItem.ReportVariableName + delim + "root";
      int delimLen = delim.Length;    
      if (s.Length < delimLen + 2) return res;

      int i = 0;
      int matchCnt = 0;
      int reslen = 0;
      
      for ( i = s.Length - delimLen; i>=0; i-- ) {
        if (s.Substring(i,delimLen) == delim) {
          // match found
          matchCnt++;
          if (matchCnt==aLevel) {
            res = s.Substring(i+delimLen,reslen);
            break;
          }
          i-=delimLen;
          reslen = 0;
        }
        reslen++;
      }      

      return res;
    }

    //=============================================================================
    /// <summary>
    /// Routine checks, if node contains at least one useful node (i.e. node that
    /// also exists in ReportItems collection)
    /// </summary>
    /// <param name="aNodeToExplore">A node to go through</param>
    /// <returns>True - if node must be included in tree</returns>
    private bool check_if_should_be_reported(TreeNode aNodeToExplore)
    {
      // Does node has Tag property?
      if (aNodeToExplore.Tag != null) {
        if (m_reportItems.IndexOf(aNodeToExplore.Tag as ReportItem) != -1) {
          return true;
        } else {
          return false;
        }
      }

      // Theres no Tag property set, but it can be in childes nodes
      if (aNodeToExplore.Nodes != null && aNodeToExplore.Nodes.Count > 0) {
        foreach (TreeNode tn in aNodeToExplore.Nodes) {
          if (check_if_should_be_reported(tn)) return true;
        }
      }

      return false;
    }

    
    //=============================================================================
    /// <summary>
    /// Routine lists all nodes in tree. Writes information about these nodes
    /// </summary>
    /// <param name="aNode">Node that has some sub-nodes</param>
    /// <param name="aNodeIndex">Current node index</param>
    /// <param name="aGroupIndex">Current group index</param>
    /// <param name="aCharacteristicIndex">Current characteristic index</param>
    private void collect_groups_and_characteristics(StreamWriter a_sw, XTreeNode aNode, ref int aNodeIndex, ref int aGroupIndex, ref int aCharacteristicIndex)
    {
      foreach (XTreeNode tn in aNode.Nodes) {
        if ((tn.Tag as SuperTag).Tag == null) {
          // it's a group
          (tn.Tag as SuperTag).NodeNum = aNodeIndex;
          (tn.Tag as SuperTag).NodeType = SuperTag.enumNodeType.NT_GROUP;
          a_sw.WriteLine("K5113/{0} {1}", aNodeIndex, aGroupIndex);
          a_sw.WriteLine("K5001/{0} {1}", aGroupIndex, aGroupIndex);
          a_sw.WriteLine("K5002/{0} {1}", aGroupIndex, tn.Text);
          aNodeIndex++;
          aGroupIndex++;
        } else {
          // it's a characteristic
          (tn.Tag as SuperTag).NodeNum = aNodeIndex;
          (tn.Tag as SuperTag).NodeType = SuperTag.enumNodeType.NT_CHARACTERISTIC;
          (tn.Tag as SuperTag).CharacteristicNum = aCharacteristicIndex;
          a_sw.WriteLine("K5112/{0} {1}", aNodeIndex, aCharacteristicIndex);
          aNodeIndex++;
          aCharacteristicIndex++;
        }
        if (tn.Nodes.Count > 0) {
          collect_groups_and_characteristics(a_sw, tn, ref aNodeIndex, ref aGroupIndex, ref aCharacteristicIndex);
        }
      }
    }

    //=============================================================================
    /// <summary>
    /// Routing builds information about node hierarchy in format acceptable by QDAS
    /// </summary>
    /// <param name="aNode"></param>
    private void link_groups(StreamWriter a_sw, XTreeNode aNode)
    {

      switch((aNode.Tag as SuperTag).NodeType) {
        case SuperTag.enumNodeType.NT_GROUP:
          a_sw.WriteLine("K5103/{0} {1}", (aNode.Parent.Tag as SuperTag).NodeNum, (aNode.Tag as SuperTag).NodeNum);
          break;
        case SuperTag.enumNodeType.NT_CHARACTERISTIC:
          a_sw.WriteLine("K5102/{0} {1}", (aNode.Parent.Tag as SuperTag).NodeNum, (aNode.Tag as SuperTag).CharacteristicNum);
          break;
      }

      if (aNode.Nodes.Count == 0) return;

      foreach (XTreeNode tn in aNode.Nodes) {
        link_groups(a_sw, tn);
      }
    }

    #endregion

    //=========================================================================
    /// <summary>
    /// Q-DAS date format string -> .NET date format string
    /// </summary>
    /// <param name="a_format">Q-DAS format string</param>
    /// <returns>.NET format string</returns>
    private string format_date_qdas_to_dot_net(string a_format)
    {
      switch (a_format) {
        case "DD.MM.YY": return "dd.MM.yy";
        case "DD.MM.YYYY": return "dd.MM.yyyy";
        case "MM/DD/YY": return @"MM/dd/yy";
        case "MM/DD/YYYY": return @"MM/dd/yyyy";
        case "MM-DD-YY": return "MM-dd-yyyy";
        case "MM-DD-YYYY": return "MM-dd-yyyy";
        default: {
          Debug.Fail("wrong enumeration value");
          return "";
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Q-DAS time format string -> .NET time format string
    /// </summary>
    /// <param name="a_format">Q-DAS format string</param>
    /// <returns>.NET format string</returns>
    private string format_time_qdas_to_dot_net(string a_format)
    {
      switch(a_format) {
        case "HH:MM:SS": return "HH:mm:ss";
        case "HH:MM": return "HH:mm";
        default: {
          Debug.Fail("Wrong enumeration value");
          return "";
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Convert DateTime to Q-DAS acceptable string representation
    /// </summary>
    /// <param name="a_datetime"></param>
    /// <returns></returns>
    private string datetime_to_str(DateTime a_datetime)
    {
      return a_datetime.ToString(
        format_date_qdas_to_dot_net(export_settings.date_format) + "/" +
        format_time_qdas_to_dot_net(export_settings.time_format),
        CultureInfo.InvariantCulture
      );
    }

    //==========================================================================
    /// <summary>
    /// Routine exports actual measure's data
    /// </summary>
    /// <returns></returns>
    private bool export_values(StreamWriter sw)
    {
      bool is_output_datetime = 
        export_settings.export_date_time && 
        (export_settings.date_format.Length > 0) && 
        (export_settings.time_format.Length > 0);

      var dfx_fields_to_export = 
      export_settings.dfx_fields.Cast<AdditionalField>().Where(
        (AdditionalField af) => {
          if (!af.selected_for_export || af.field_value.Length < 1) return false;
          if (af.catalog_used && !af.contains_valid_catalog_value) return false;
          return true;
        }
      );

      var per_measure_fields = dfx_fields_to_export.Where(
        (AdditionalField af) => 
          MacroManager.contains_per_measure_macros(af.field_value)        
      );            

      var per_session_fields = dfx_fields_to_export.Except(
        per_measure_fields, 
        new LambdaComparer<AdditionalField>((a,b) => a.k_str == b.k_str)
      );

      foreach (pi.IMeasure m in m_measures) {
        string val_str = ""; // values string
        string datetime_str = ""; // date&time string
        foreach (IReportItem ri in m_reportItems) {
          ri.Measure = m;
          val_str += format_double(ri.Value,ri.NumberOfDigits) + c_value_divider;
          if (is_output_datetime) {
            datetime_str += datetime_to_str(ri.MeasureDate) + c_value_divider;
          }
        }
        
        //-- Mandatory
        // values
        val_str = val_str.Remove(val_str.Length - 1, 1); // cut last character
        sw.WriteLine("K0001 " + val_str);

        //-- Optional
        // date&time
        if (is_output_datetime) {
          // Cut last character
          datetime_str = datetime_str.Remove(datetime_str.Length - 1, 1);
          sw.WriteLine("K0004 " + datetime_str);
        }

        foreach (AdditionalField af in per_measure_fields) {
          sw.WriteLine(
            "{0}/0 {1}",
            af.k_str,
            MacroManager.subst_all(
              af.field_value, new MacroCallContext(m.Document, m)
            )
          );            
        }
      } // foreach (pi.measure m in m_measures)

      foreach (AdditionalField af in per_session_fields) {
        sw.WriteLine(
          "{0}/0 {1}",
          af.k_str,
          MacroManager.subst_all(
            af.field_value,
            new MacroCallContext(PIConnector.instance.active_document, null)
          )
        );
      }
      return true;
    }

    #endregion

    #region Fields

    /// <summary> Export settings </summary>
    private ExportSettingsQDAS m_export_settings;

    /// <summary> 
    /// Previous report item key values 
    /// (needed to detect when we should create new description file (DFD))
    /// </summary>
    private StringCollection m_prev_report_items_keys;

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Export settings
    /// </summary>
    public ExportSettingsQDAS export_settings
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

  } // end class SuperTag

} // end namespace Autodesk.PowerInspect.AddIns.SPC

