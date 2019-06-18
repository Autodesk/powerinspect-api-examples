//=============================================================================
//D AdditionalField.cs
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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Field type
  /// </summary>
  [ComVisible(true)]
  public enum enumQDASKFieldType {
    KFT_A = 1,
    KFT_I = 2,
    KFT_D = 3
  };

  //===========================================================================
  /// <summary>
  /// Additional field
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("4B14D93F-E20E-4B69-9F65-3F5C2B792575")
  ]
  public class AdditionalField
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_k_str">K-string</param>
    /// <param name="a_description">Field description</param>
    /// <param name="a_field_type">Field type</param>
    /// <param name="a_max_length">Max length (in characters)</param>
    /// <param name="a_validation_regex">Validation regex</param>
    /// <param name="a_required">Required flag</param>
    internal AdditionalField(
      string a_k_str, 
      string a_description, 
      enumQDASKFieldType a_field_type, 
      int a_max_length, 
      string a_validation_regex, 
      bool a_required
    )
    {
      // preconditions      
      Debug.Assert(a_k_str != String.Empty);
      Debug.Assert(a_description != String.Empty);
      Debug.Assert(a_max_length > 0);
      Debug.Assert(a_validation_regex != null);

      k_str = a_k_str;
      description = a_description;
      field_type = a_field_type;
      max_length = a_max_length;
      validation_regex = a_validation_regex;
      required = a_required;
      catalog_used = false;
      m_selected_for_export = a_required; // by default all required values are selected for export
    }


    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_k_str">K-string</param>
    /// <param name="a_description">Field description</param>
    /// <param name="a_field_type">Field type</param>
    /// <param name="a_max_length">Max length (in characters)</param>
    /// <param name="a_validation_regex">Validation regex</param>
    /// <param name="a_required">Required flag</param>
    /// <param name="a_linked_catalog_type">Linked catalog type</param>
    internal AdditionalField(
      string a_k_str, 
      string a_description, 
      enumQDASKFieldType a_field_type, 
      int a_max_length, 
      string a_validation_regex, 
      bool a_required, 
      enumCatalogType a_linked_catalog_type
    )
    {
      // preconditions
      Debug.Assert(a_k_str != String.Empty);
      Debug.Assert(a_description != String.Empty);
      Debug.Assert(a_max_length > 0);
      Debug.Assert(a_validation_regex != null);

      k_str = a_k_str;
      description = a_description;
      field_type = a_field_type;
      max_length = a_max_length;
      validation_regex = a_validation_regex;
      required = a_required;
      linked_catalog_type = a_linked_catalog_type;
      catalog_used = true;
      m_selected_for_export = a_required; // by default all required values are selected for export
    }

    //=========================================================================
    /// <summary>
    /// Conversion routine ( --> string)
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return String.Format("[{0}] {1}", k_str, description);
    }

    //=========================================================================
    /// <summary>
    /// Check if value is valid for this field
    /// </summary>
    /// <param name="a_str">Value to check</param>
    /// <returns>TRUE, if value is valid</returns>
    public bool check_value_valid(string a_str)
    {
      return
        (a_str != null) &&
        (a_str.Length <= max_length) &&
        (
          (a_str == String.Empty) ||
          (
            (validation_regex == String.Empty) ||
            (Regex.IsMatch(a_str, validation_regex, RegexOptions.IgnoreCase))
          )
        );
    }

    //=========================================================================
    /// <summary>
    /// Field value
    /// </summary>
    public string field_value
    {
      get { return m_value; }
      set {
        if (check_value_valid(value)) {
          m_value = value;
        } else {
          Debug.Fail("Invalid value");
        }        
      }
    }

    //=========================================================================
    /// <summary>
    /// Check this object value
    /// </summary>
    public bool is_value_valid
    {
      get { return check_value_valid(field_value); }
    }

    //=========================================================================
    /// <summary>
    /// Check if value is selected for export
    /// </summary>
    public bool selected_for_export
    {
      get {
        return m_selected_for_export;
      }
      set {        
        Debug.Assert(value || (!value && !required), "Required field cannot be marked as non-selected for export");        
        m_selected_for_export = value;
      }
    }

    //=========================================================================
    /// <summary>
    /// Get/set Combo box index (this is a helper property, used by form)
    /// </summary>
    internal int combo_index
    {
      get { return m_combo_index; }
      set { m_combo_index = value; }
    }

    //=========================================================================
    /// <summary>
    /// Get/Set datetime (if the field is of type D)
    /// </summary>
    internal DateTime datetime
    {
      get { return m_datetime; }
      set { m_datetime = value; }
    }

    /// <summary> K-string </summary>
    public readonly string k_str;
    /// <summary> Description </summary>
    public readonly string description;
    /// <summary> Max length (in characters) </summary>
    public readonly int max_length;
    /// <summary> Validation regex </summary>
    public readonly string validation_regex;
    /// <summary> Type of field </summary>
    public readonly enumQDASKFieldType field_type;
    /// <summary> Required field flag </summary>
    public readonly bool required;
    /// <summary> Linked catalog type </summary>
    public readonly enumCatalogType linked_catalog_type;
    /// <summary> Catalog used flag </summary>
    public readonly bool catalog_used;
    /// <summary> Selected for export flag </summary>
    private bool m_selected_for_export;

    //=========================================================================
    /// <summary>
    /// Returns true if field supports macro substitutions
    /// </summary>
    public bool supports_macro
    {
      get {
        // text fields support macro
        return field_type == enumQDASKFieldType.KFT_A;
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns true if field contains valid catalog value
    /// </summary>
    public bool contains_valid_catalog_value
    {
      get {
        if (catalog_used) {
          return Convert.ToInt32(field_value, 10) > 0;
        }
        return false;
      }
    }

    /// <summary> Current field value </summary>
    private string m_value = "";
    /// <summary> Combo box index </summary>
    private int m_combo_index = -1;
    /// <summary> Date&time value of field (in case if type of field is D) </summary>
    private DateTime m_datetime;
  }
}

