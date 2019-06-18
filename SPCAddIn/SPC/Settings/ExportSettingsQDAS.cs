//=============================================================================
//D ExportSettingsQDAS.cs
//
// Export settings for Q-DAS export.
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
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Q-DAS export settings 
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("373A78D7-DBFC-4DD6-AD9C-3F787BCB9B2D")
  ]
  public class ExportSettingsQDAS : ExportSettings
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal ExportSettingsQDAS()
      : base()
    {
    }

    public override bool load_from_dialog()
    {
      using (FormExportSettingsQDAS frm = new FormExportSettingsQDAS()) {
        // restore state we had before

        // output directory
        frm.textBoxOutputDirectory.Text = output_directory;

        // export details
        frm.checkBoxExportDateTime.Checked = export_date_time;
        frm.checkBoxExportStructureInformation.Checked = export_structure_information;
        frm.checkBoxUseFlatHierarchy.Checked = use_flat_hierarchy;

        // Items available for export (empty-valued items)
        frm.listViewAvailible.Items.Clear();
        foreach (AdditionalField af in dfd_fields) {
          if (!af.required && !af.selected_for_export) {
            ListViewItem lvi = new ListViewItem(
              new string[]{
                af.k_str,
                af.description
              }
            );
            lvi.Tag = af;
            frm.listViewAvailible.Items.Add(lvi);
          }
        }

        // Exported items (value is not empty)
        frm.listViewExported.Items.Clear();
        foreach (AdditionalField af in dfd_fields) {
          if (af.required || af.selected_for_export) {
            ListViewItem lvi = new ListViewItem(
              new string[]{
                af.description,
                extract_field_value(af)
              }
            );
            lvi.Tag = af;
            frm.listViewExported.Items.Add(lvi);
          }
        }

        // Date&Time combo boxes
        frm.comboBoxDateFormat.SelectedIndex = frm.comboBoxDateFormat.FindStringExact(date_format);
        frm.comboBoxTimeFormat.SelectedIndex = frm.comboBoxTimeFormat.FindStringExact(time_format);

        // DFX fields list
        frm.checkedListBoxDFXFields.Items.Clear();
        foreach(AdditionalField af in dfx_fields) {
          frm.checkedListBoxDFXFields.Items.Add(af, af.selected_for_export || af.required);
        }

        // CATALOG MANAGEMENT TAB
        frm.listBoxCatalogs.Items.Clear();
        foreach(Catalog c in catalog_manager.catalogs) {
          frm.listBoxCatalogs.Items.Add(c);
        }

        frm.catalog_manager = catalog_manager.Clone() as CatalogManager;


        //////////////////////////////////////////////////////////////////////////        
        // show dialog to user
        // if user has pressed "Save" - save settings. Otherwise - return.
        //////////////////////////////////////////////////////////////////////////        
        if (frm.ShowDialog() != DialogResult.OK) return false; // FAILURE?

        output_directory = frm.textBoxOutputDirectory.Text;

        export_date_time = frm.checkBoxExportDateTime.Checked;
        export_structure_information = frm.checkBoxExportStructureInformation.Checked;
        use_flat_hierarchy = frm.checkBoxUseFlatHierarchy.Checked;

        foreach (ListViewItem lvi in frm.listViewAvailible.Items) {
          AdditionalField af = lvi.Tag as AdditionalField;
          Debug.Assert(af != null);
          af.selected_for_export = false;
        }

        foreach(ListViewItem lvi in frm.listViewExported.Items) {
          AdditionalField af = lvi.Tag as AdditionalField;
          Debug.Assert(af != null);
          af.selected_for_export = true;          
          if (af.catalog_used) {
            af.field_value = af.combo_index.ToString();
          } else if (af.field_type == enumQDASKFieldType.KFT_D) {
            if (af.datetime != null) {
              af.field_value = af.datetime.ToString(c_datetime_store_format);
            }
          } else {
            string val = lvi.SubItems[1].Text;
            if (af.check_value_valid(val)) {
              af.field_value = val;
            } else {
              // break on first failure
              Debug.Fail("Should not happen");              
            }
          }
        }

        date_format = (frm.comboBoxDateFormat.SelectedItem != null)?frm.comboBoxDateFormat.SelectedItem.ToString():"";
        time_format = (frm.comboBoxTimeFormat.SelectedItem != null)?frm.comboBoxTimeFormat.SelectedItem.ToString():"";

        // DFX fields list
        foreach (AdditionalField af in frm.checkedListBoxDFXFields.CheckedItems) {
          af.selected_for_export = true;
        }

        m_catalog_manager = frm.catalog_manager;

        return true; // SUCCEESS
      }
    }

    public override bool load_from_state_keeper(StateKeeper a_state_keeper)
    {
      // check if we have correct collection with settings
      // this list should include:
      // * output directory element
      // * all the elements from additional_fields_complete_list
      // * md5 hash of previously exported descriptive date
      //   this hash is required to determine if a new DFD file should be created or we can
      //   keep using the old one (if the old and the new hashed are match)
      // * date format
      // * time format
      if (a_state_keeper.settings_qdas.Count < 4 + dfd_fields.Count) return false; // FAILURE

      int idx = 0;

      output_directory = a_state_keeper.settings_qdas[idx++];
      export_date_time = str_to_flag(a_state_keeper.settings_qdas[idx++]);
      export_structure_information = str_to_flag(a_state_keeper.settings_qdas[idx++]);
      use_flat_hierarchy = str_to_flag(a_state_keeper.settings_qdas[idx++]);

      // DFD fields
      foreach(AdditionalField af in dfd_fields) {
        string s = a_state_keeper.settings_qdas[idx++];
        if (s.Length < 1) continue;
        // check if additional field is selected for export        
        af.selected_for_export = str_to_flag(s);
        if (s.Length < 2) continue;
        s = s.Substring(1);
        af.field_value = s;        
      }

      // DFX fields
      foreach (AdditionalField af in dfx_fields) {
        string s = a_state_keeper.settings_qdas[idx++];
        if (s.Length < 1) continue;
        // check if additional field is selected for export        
        af.selected_for_export = str_to_flag(s);
        if (s.Length < 2) continue;
        s = s.Substring(1);
        af.field_value = s;  
      }

      date_format = a_state_keeper.settings_qdas[idx++];
      time_format = a_state_keeper.settings_qdas[idx++];

      prev_md5 = a_state_keeper.settings_qdas[idx++];

      catalog_manager.load_from_state_keeper(a_state_keeper);

      return true; // SUCCEESS
    }

    public override bool save_to_state_keeper(StateKeeper a_state_keeper)
    {
      a_state_keeper.settings_qdas.Clear();
      
      a_state_keeper.settings_qdas.Add(output_directory);

      a_state_keeper.settings_qdas.Add(flag_to_str(export_date_time));
      a_state_keeper.settings_qdas.Add(flag_to_str(export_structure_information));
      a_state_keeper.settings_qdas.Add(flag_to_str(use_flat_hierarchy));


      foreach (AdditionalField af in dfd_fields) {
        a_state_keeper.settings_qdas.Add(flag_to_str(af.selected_for_export) + af.field_value);
      }

      // DFX fields
      foreach (AdditionalField af in dfx_fields) {
        a_state_keeper.settings_qdas.Add(flag_to_str(af.selected_for_export) + af.field_value);
      }

      a_state_keeper.settings_qdas.Add(date_format);
      a_state_keeper.settings_qdas.Add(time_format);

      //-- md5 hash
      a_state_keeper.settings_qdas.Add(prev_md5);

      catalog_manager.save_to_state_keeper(a_state_keeper);

      return true; // SUCCEESS
    }

    public override void on_after_export()
    {
      set_uptodate_descriptive_part();
    }

    //=============================================================================
    ///<summary>
    /// Output directory (Q-DAS monitoring directory), e.g. "C:\Out\Measures"
    ///</summary>
    public string output_directory
    {
      get { return m_output_directory; }
      set { m_output_directory=value; }
    }

    #region Q-DAS DFD fields

    public string part_number
    {
      get { return get_desc_field_value(0); }
      set { set_desc_field_value(0,value); }
    }

    public string part_description
    {
      get { return get_desc_field_value(1); }
      set { set_desc_field_value(1, value); }
    }    


    #endregion

    //=============================================================================
    ///<summary>
    /// Export date&time of a measure?
    ///</summary>
    public bool export_date_time
    {
      get { return m_export_date_time; }
      set { m_export_date_time=value; }
    }

    //=============================================================================
    ///<summary>
    /// Export structure information? (K5xxx fields)
    ///</summary>
    public bool export_structure_information
    {
      get { return m_export_structure_information; }
      set { m_export_structure_information=value; }
    }

    //=========================================================================
    /// <summary>
    /// Use flat hierarchy when export (no groups)
    /// </summary>
    public bool use_flat_hierarchy
    {
      get { return m_use_flat_hierarchy; }
      set { m_use_flat_hierarchy = value; }
    }

    public string date_format
    {
      get { return m_date_format; }
      set { 
        foreach(string df in m_available_date_formats) {
          if (df == value) {
            m_date_format = value;
            break;
          }
        }
      }
    }

    //=============================================================================
    ///<summary>
    /// Time format used for output
    ///</summary>
    public string time_format
    {
      get { return m_time_format; }
      set {
        foreach(string tf in m_available_time_format) {
          if (tf == value) {
            m_time_format = value;
            break;
          }
        }        
      }
    }

    //=========================================================================
    /// <summary>
    /// Retrieve catalog manager
    /// </summary>
    public CatalogManager catalog_manager
    {
      get { return m_catalog_manager; }
    }


    //=============================================================================
    ///<summary>
    /// Number of digits used in file names when using IncrementalFile naming rule
    ///</summary>
    public int incremental_file_name_digits
    {
      get { return m_incremental_file_name_digits; }
      set {
        if ((value >= 4) && (value <= 10)) {
          m_incremental_file_name_digits = value;
        }
      }
    }


    //=========================================================================
    /// <summary>
    /// Update MD5 hash
    /// </summary>
    internal void set_uptodate_descriptive_part()
    {
      prev_md5 = calculate_md5();
    }

    //=========================================================================
    /// <summary>
    /// Check if descriptive part is up-to-date
    /// </summary>
    internal bool descriptive_part_is_up_to_date
    {
      get {
        string md5 = calculate_md5();
        return md5 == prev_md5; // compare two version of md5 hash
      }
    }

    #region Additional fields declaration (see Q-DAS documentation)

    public readonly AdditionalFieldList dfd_fields = new AdditionalFieldList(
      new AdditionalField[]{
      new AdditionalField(
        "K1001",
        LS.Lc("Part number"),
        enumQDASKFieldType.KFT_A, 30, "", true 
      ),
      new AdditionalField(
        "K1002",
        LS.Lc("Part description"),
        enumQDASKFieldType.KFT_A, 80, "", true
      ),
      new AdditionalField(
        "K1003",
        LS.Lc("Part abbreviation"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1004",
        LS.Lc("Part Amendment status"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1005",
        LS.Lc("Product"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1007",
        LS.Lc("Abbreviation Part no."),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1008",
        LS.Lc("Part type"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1009",
        LS.Lc("Part code"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1010",
        LS.Lc("Control item"),
        enumQDASKFieldType.KFT_I, 1, "\\d+", false
      ),
      new AdditionalField(
        "K1011",
        LS.Lc("Variant"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1012",
        LS.Lc("ID number annex"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1013",
        LS.Lc("ID number index"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1014",
        LS.Lc("Parts ID"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1016",
        LS.Lc("Assembly part"),
        enumQDASKFieldType.KFT_A, 30, "", false
      ),
      new AdditionalField(
        "K1020",
        LS.Lc("Manufacturer Catalog"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1021",
        LS.Lc("Manufacturer No."),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1022",
        LS.Lc("Manufacturer name"),
        enumQDASKFieldType.KFT_A, 80, "", false
      ),
      new AdditionalField(
        "K1023",
        LS.Lc("Manufacturer number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1030",
        LS.Lc("Materials Catalog"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1031",
        LS.Lc("Material number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1032",
        LS.Lc("Material Description"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1033",
        LS.Lc("Material number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1040",
        LS.Lc("Drawing Catalog"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1041",
        LS.Lc("Drawing number"),
        enumQDASKFieldType.KFT_A, 30, "", false
      ),
      new AdditionalField(
        "K1042",
        LS.Lc("Drawing Amendment"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1043",
        LS.Lc("Drawing Index"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1044",
        LS.Lc("Drawing Number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1045",
        LS.Lc("Drawing validation date"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1046",
        LS.Lc("Drawing name"),
        enumQDASKFieldType.KFT_A, 60, "", false
      ),
      new AdditionalField(
        "K1047",
        LS.Lc("Basic drawing number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1051",
        LS.Lc("Contractor Number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1052",
        LS.Lc("Contractor Name"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1053",
        LS.Lc("Contract"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1054",
        LS.Lc("Contractor Number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1061",
        LS.Lc("Customer Number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1062",
        LS.Lc("Customer Name"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1063",
        LS.Lc("Customer Number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1071",
        LS.Lc("Supplier Number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1072",
        LS.Lc("Supplier Name"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1073",
        LS.Lc("Supplier Number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1081",
        LS.Lc("Machine Number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1082",
        LS.Lc("Machine Description"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1083",
        LS.Lc("Machine Number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1085",
        LS.Lc("Machine Location"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1086",
        LS.Lc("Work Cycle"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1100",
        LS.Lc("Plant Sector"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1101",
        LS.Lc("Department"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1102",
        LS.Lc("Workshop/sector"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1103",
        LS.Lc("Cost centre"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1104",
        LS.Lc("Shift"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1110",
        LS.Lc("Order number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1111",
        LS.Lc("Goods received number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1201",
        LS.Lc("Test Facility Number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1202",
        LS.Lc("Test Facility Description"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1203",
        LS.Lc("Reason for Test"),
        enumQDASKFieldType.KFT_A, 80, "", false
      ),
      new AdditionalField(
        "K1204",
        LS.Lc("Test Begin"),
        enumQDASKFieldType.KFT_D, 20, "", false
      ),
      new AdditionalField(
        "K1205",
        LS.Lc("Test End"),
        enumQDASKFieldType.KFT_D, 20, "", false
      ),
      new AdditionalField(
        "K1206",
        LS.Lc("Test Location"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1208",
        LS.Lc("Test Facility No."),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1209",
        LS.Lc("Inspection type"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1210",
        LS.Lc("measurement type"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1211",
        LS.Lc("Standard Number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1212",
        LS.Lc("Standard description"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1215",
        LS.Lc("Standard number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1221",
        LS.Lc("Inspector number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1222",
        LS.Lc("Inspector name"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1223",
        LS.Lc("Inspector number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1230",
        LS.Lc("Gage room"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1231",
        LS.Lc("Measuring program number"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1232",
        LS.Lc("Measuring program version"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1301",
        LS.Lc("Client"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K1302",
        LS.Lc("Test batch"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1303",
        LS.Lc("plant"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1311",
        LS.Lc("Production order"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1341",
        LS.Lc("Test Plan Number Text"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ),
      new AdditionalField(
        "K1342",
        LS.Lc("Test Plan Name"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1343",
        LS.Lc("Test Plan Development Date"),
        enumQDASKFieldType.KFT_D, 20, "", false
      ),
      new AdditionalField(
        "K1344",
        LS.Lc("Test Plan Developer"),
        enumQDASKFieldType.KFT_A, 40, "", false
      ),
      new AdditionalField(
        "K1900",
        LS.Lc("Remark"),
        enumQDASKFieldType.KFT_A, 255, "", false
      )
      }
    );

    public readonly AdditionalFieldList dfx_fields = new AdditionalFieldList (
      new AdditionalField[]{
      new AdditionalField(
        "K0002",
        LS.Lc("attribute"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0003",
        LS.Lc("Flag"),
        enumQDASKFieldType.KFT_I, 1, "[01]", false
      ),
      new AdditionalField(
        "K0005",
        LS.Lc("Event"),
        enumQDASKFieldType.KFT_I, 10, "\\d+", false, enumCatalogType.CT_EVENT
      ),
      new AdditionalField(
        "K0006",
        LS.Lc("Batch number"),
        enumQDASKFieldType.KFT_A, 14, "", false
      ),
      new AdditionalField(
        "K0007",
        LS.Lc("Cavity number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false, enumCatalogType.CT_CAVITY
      ),
      new AdditionalField(
        "K0008",
        LS.Lc("Operator name"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false, enumCatalogType.CT_OPERATOR
      ),
      new AdditionalField(
        "K0009",
        LS.Lc("Text"),
        enumQDASKFieldType.KFT_A, 255, "", false
      ),
      new AdditionalField(
        "K0010",
        LS.Lc("Machine number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false, enumCatalogType.CT_MACHINE
      ),
      new AdditionalField(
        "K0011",
        LS.Lc("Process parameter"),
        enumQDASKFieldType.KFT_I, 10, "\\d+", false, enumCatalogType.CT_PROCESS_PARAMETER
      ),
      new AdditionalField(
        "K0012",
        LS.Lc("Gage number"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false, enumCatalogType.CT_GAGE
      ),
      new AdditionalField(
        "K0013",
        LS.Lc("Process parameter value"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0020",
        LS.Lc("Subgroup size"),
        enumQDASKFieldType.KFT_I, 8, "\\d+", false
      ),
      new AdditionalField(
        "K0021",
        LS.Lc("No. of errors"),
        enumQDASKFieldType.KFT_I, 8, "\\d+", false
      ),
      new AdditionalField(
        "K0022",
        LS.Lc("Number < LSL"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0023",
        LS.Lc("Number > USL"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0024",
        LS.Lc("Rejects"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0025",
        LS.Lc("Re-work"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0026",
        LS.Lc("Error class"),
        enumQDASKFieldType.KFT_I, 5, "\\d+", false
      ),
      new AdditionalField(
        "K0053",
        LS.Lc("Order"),
        enumQDASKFieldType.KFT_A, 20, "", false
      ) 
      }
    );

    #endregion

    //=========================================================================
    /// <summary>
    /// Pre export input
    /// </summary>
    /// <returns></returns>
    internal override enumPreInputResult pre_export_input()
    {
      // the implementation of this method for Q-DAS exporter is differ from
      // other exporters, because Q-DAS has much more options as well.
      // the dialog this method shows is for operator. it has minimum amount of 
      // fields (the exact list of fields is programmed by inspector via Settings 
      // dialog).
      using (FormOperatorInput frm = new FormOperatorInput(this)) {

        // load fields marked as "selected_for_export" into list view
        frm.listViewDFXFields.Items.Clear();
        foreach (AdditionalField af in dfx_fields) {
          if (!af.selected_for_export) continue;          
          ListViewItem lvi = new ListViewItem(
            new string[] {
              af.ToString(),
              extract_field_value(af)
            }
          );
          lvi.Tag = af;
          frm.listViewDFXFields.Items.Add(lvi);
        }

        if (frm.ShowDialog() != DialogResult.OK) {
          return enumPreInputResult.PIR_ABORTED; // user has pressed CANCEL
        }
       
        // Check if we have bad values
        foreach(ListViewItem lvi in frm.listViewDFXFields.Items) {
          //AdditionalFieldComboTag op_tag = lvi.Tag as AdditionalFieldComboTag;
          //AdditionalField af = op_tag.additional_field;          
          AdditionalField af = lvi.Tag as AdditionalField;
          if (af.catalog_used) {
            af.field_value = af.combo_index.ToString();
          } else if (af.field_type == enumQDASKFieldType.KFT_D) {
            if (af.datetime != null) {
              af.field_value = af.datetime.ToString(c_datetime_store_format);
            }
          } else {
            string val = lvi.SubItems[1].Text;
            if (af.check_value_valid(val)) {
              af.field_value = val;
            } else {
              // break on first failure
              Debug.Fail("Should not happen");
              return enumPreInputResult.PIR_WRONG_DATA;
            }
          }
        }

        return enumPreInputResult.PIR_OK; // SUCCEESS
      }
    }

    //=========================================================================
    /// <summary>
    /// Check is we have enough settings to start export
    /// </summary>
    /// <returns></returns>
    public override bool is_has_necessary_data()
    {
      try {

        bool res = Directory.Exists(output_directory);

        foreach (AdditionalField af in dfd_fields) {
          if (af.required) {
            res = res && (af.field_value.Length > 0) && (af.is_value_valid);
          }
        }

        if (export_date_time) {
          res = res && (date_format != String.Empty) && (time_format != String.Empty);
        }

        return res;

      } catch (Exception) {
        Debug.Fail("Exception?");
        return false;
      }
    }

    //=========================================================================
    /// <summary>
    /// Extract value from additional field
    /// </summary>
    /// <param name="a_af">Additional field reference</param>
    /// <returns>string value</returns>
    private string extract_field_value(AdditionalField a_af)
    {
      string field_val = "";
      if (a_af.catalog_used) {
        // grab from catalog
        if (a_af.field_value != String.Empty) {
          try {
            int idx = Convert.ToInt32(a_af.field_value) - 1;
            a_af.combo_index = idx;
            if (idx >= 0) {
              field_val = (catalog_manager.get_catalog(a_af.linked_catalog_type).items[idx] as CatalogItem).ToString();
            }
          } catch (Exception) {
          }
        }
      } else if (a_af.field_type == enumQDASKFieldType.KFT_D) {
        field_val = a_af.field_value;
        if (field_val != String.Empty) {
          try {
            a_af.datetime = DateTime.ParseExact(field_val, c_datetime_store_format, null);
          } catch (Exception) {
          }
        }
      } else {
        field_val = a_af.field_value;
      }
      return field_val;
    }

    //=========================================================================
    /// <summary>
    /// Get descriptive field value
    /// </summary>
    /// <param name="a_index">Field index inside description_part_fields_complete_list</param>
    /// <returns>Field value</returns>
    private string get_desc_field_value(int a_index)
    {
      return dfd_fields[a_index].field_value;
    }

    //=========================================================================
    /// <summary>
    /// Set descriptive field value
    /// </summary>
    /// <param name="a_index">Field index inside description_part_fields_complete_list</param>
    /// <param name="a_value">Value to set the field to</param>
    private void set_desc_field_value(int a_index, string a_value)
    {
      dfd_fields[a_index].field_value = cut_str(a_value, dfd_fields[a_index].max_length);
    }

    //=============================================================================
    ///<summary>
    /// MD5 hash of the previous instance used when the latests successful export has
    /// happened (only DFD-critical part of this class)
    ///</summary>
    private string prev_md5
    {
      get { return m_prev_md5; }
      set { m_prev_md5=value; }
    }    

    //=========================================================================
    /// <summary>
    /// Calculate DFD-dependent md5 hash
    /// </summary>
    /// <returns></returns>
    private string calculate_md5()
    {

      MemoryStream ms = new MemoryStream();
      StreamWriter ms_sw = new StreamWriter(ms, Encoding.UTF8);      

      ms_sw.Write(output_directory);
      ms_sw.Write(export_structure_information);
      ms_sw.Write(use_flat_hierarchy);

      foreach(AdditionalField af in dfd_fields) {
        // take into account all values (even empty & optional)
        // this string consists of two parts:
        // * selected_for_export flag
        // * field value itself
        ms_sw.Write(af.field_value);
      }

      ms_sw.Flush();

      ms.Seek(0, SeekOrigin.Begin);

      MD5 md = MD5.Create();
      return md5_to_str(md.ComputeHash(ms));
    }


    //=========================================================================
    /// <summary>
    /// MD5 RAW --> string
    /// </summary>
    /// <param name="a_md5">raw MD5 bytes</param>
    /// <returns>string representation of md5 hash value</returns>
    private static string md5_to_str(byte[] a_md5)
    {
      string md5_str = "";
      foreach (byte b in a_md5) {
        md5_str += b.ToString("X2");
      }
      return md5_str;
    }

    //=========================================================================
    /// <summary>
    /// String to boolean flag conversion routine
    /// </summary>
    /// <param name="a_str">String</param>
    /// <returns>Boolean flag</returns>
    private static bool str_to_flag(string a_str)
    {
      return a_str[0] != '0';
    }

    //=========================================================================
    /// <summary>
    /// Boolean flag to string conversion routine
    /// </summary>
    /// <param name="a_flag">Boolean flag</param>
    /// <returns>String</returns>
    private static string flag_to_str(bool a_flag)
    {
      return a_flag ? "1" : "0";
    }

    /// <summary> Output directory (Q-DAS monitoring directory), e.g. "C:\Out\Measures" </summary>
    private string m_output_directory = "";
    /// <summary> Export date&time of a measure? </summary>
    private bool m_export_date_time = true;
    /// <summary> Export structure information? (K5xxx fields) </summary>
    private bool m_export_structure_information = true;
    /// <summary> 
    /// Groups will be flatted out during grouping of elements,
    /// i.e. only part-characteristic relation remains.
    /// </summary>
    private bool m_use_flat_hierarchy = true;

    /// <summary> Date format used for output </summary>
    private string m_date_format = m_available_date_formats[1]; // this is default format
    /// <summary> Time format used for output </summary>
    private string m_time_format = m_available_time_format[1]; // this is default format

    private CatalogManager m_catalog_manager = new CatalogManager();

    /// <summary> Number of digits used in file names when using IncrementalFile naming rule </summary>
    private int m_incremental_file_name_digits = 8;

    /// <summary> 
    /// MD5 hash of the previous instance used for successful export 
    /// (only DFD-critical part)
    /// </summary>
    private string m_prev_md5 = "";

    /// <summary> Available date formats </summary>
    private static string[] m_available_date_formats = {
      "DD.MM.YY",
      "DD.MM.YYYY",
      "MM/DD/YY",
      "MM/DD/YYYY",
      "MM-DD-YY",
      "MM-DD-YYYY"
    };

    /// <summary> Available time formats </summary>
    private static string[] m_available_time_format = {
      "HH:MM",
      "HH:MM:SS"
    };

    /// <summary> Date&time store format used when saving with statekeeper </summary>
    private const string c_datetime_store_format = "dd.MM.yyyy/HH:mm";
  }
}

