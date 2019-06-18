//=============================================================================
//D CatalogItem.cs
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
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Q-DAS catalog item
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("65F52468-9E31-4D15-9937-76CE7421A823")
  ]
  public class CatalogItem : ICloneable
  {
    //=========================================================================
    /// <summary>
    /// Default constructor (Automation cat used it to create catalog items)
    /// </summary>
    public CatalogItem()
    {
    }

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_number">Number of catalog item</param>
    /// <param name="a_value_str">Value of catalog item</param>
    public CatalogItem(string a_number, string a_value_str)
    {
      m_number = a_number;
      m_value_str = a_value_str;
    }

    //=========================================================================
    /// <summary>
    /// Convert routine
    /// </summary>
    /// <returns>String representation of catalog items</returns>
    public override string ToString()
    {      
      return value_str;
    }


    //=============================================================================
    ///<summary>
    /// Number of catalog item
    ///</summary>
    public string number
    {
      get { return m_number; }
      set { m_number=value; }
    }


    //=============================================================================
    ///<summary>
    /// Value of catalog item
    ///</summary>
    public string value_str
    {
      get { return m_value_str; }
      set { m_value_str=value; }
    }

    //=========================================================================
    /// <summary>
    /// Catalog item index (used by parser)
    /// </summary>
    public int index
    {
      get { return m_index; }
      set
      {
        Debug.Assert(value > 0);
        m_index = value;
      }
    }

    /// <summary> Number of catalog item </summary>
    private string m_number = "";
    /// <summary> Value of catalog item </summary>
    private string m_value_str = "";
    /// <summary> Index of catalog item (used by parser) </summary>
    private int m_index = 1;


    #region ICloneable Members

    //=========================================================================
    /// <summary>
    /// Clone catalog item
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
      CatalogItem ci = new CatalogItem(number, value_str);
      return ci;
    }

    #endregion
  }
}

