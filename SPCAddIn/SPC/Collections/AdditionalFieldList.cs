//=============================================================================
//D AdditionalFieldList.cs
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
using System.Runtime.InteropServices;
using System.Collections;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //===========================================================================
  /// <summary>
  /// Additional field list. Provides advanced field search methods.
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("BF94C80A-1617-4C6B-BE78-1FD327005D1D")
  ]
  public class AdditionalFieldList : CollectionBase
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_field_list">Initial list</param>
    internal AdditionalFieldList(AdditionalField[] a_field_list)
    {
      foreach (AdditionalField af in a_field_list) {
        Add(af);
      }
    }

    //=========================================================================
    /// <summary>
    /// Get reference to additional field by it's K-string
    /// </summary>
    /// <param name="a_k_str">K-string</param>
    /// <returns>Reference to additional field</returns>
    public AdditionalField get_by_k_str(string a_k_str)
    {
      foreach(AdditionalField af in List) {
        if (af.k_str == a_k_str) return af;
      }
      return null;
    }
 
    //=========================================================================
    /// <summary>
    /// Get reference to additional field by it's index
    /// </summary>
    /// <param name="a_index">Zero-based index</param>
    /// <returns>Reference to additional field</returns>
    public AdditionalField get_by_index(int a_index)
    {
      return List[a_index] as AdditionalField;
    }

    //=============================================================================
    /// <summary>
    /// Default index property
    /// </summary>    
    public AdditionalField this[int index]
    {
      get {
        return get_by_index(index);
      }
    }

    internal AdditionalField this[string a_k_str]
    {
      get{
        return get_by_k_str(a_k_str);
      }
    }

    //=========================================================================
    /// <summary>
    /// Add item to the list
    /// </summary>
    /// <param name="a_additional_field">Item to add</param>
    /// <returns>Item index</returns>
    private int Add(AdditionalField a_additional_field)
    {
      return List.Add(a_additional_field);
    }

  }
}

