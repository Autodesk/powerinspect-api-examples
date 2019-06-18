//=============================================================================
//D Catalog.cs
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
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// Known catalog types
  /// </summary>
  [ComVisible(true)]
  public enum enumCatalogType {
    CT_CUSTOMER = 0,
    CT_MANUFACTURER = 1,
    CT_SUPPLIER = 2,
    CT_CONTRACT_NUMBER = 3,
    CT_MATERIAL = 4,
    CT_DRAWING = 5,
    CT_MACHINE = 6,
    CT_GAGE = 7,
    CT_UNITS = 8,
    CT_OPERATOR = 9,
    CT_CONTRACTOR = 10,
    CT_PRODUCT = 11,
    CT_STUFF = 12,
    CT_EVENT = 22,
    CT_PROCESS_PARAMETER = 24,
    CT_CAVITY = 25
  }

  //===========================================================================
  /// <summary>
  /// Q-DAS Catalog
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("18710FE7-378E-46E1-8D25-828D942130DB")
  ]
  public class Catalog : ICloneable
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_catalog_type">Type of Q-DAS catalog</param>
    internal Catalog(enumCatalogType a_catalog_type)
    {
      m_catalog_type = a_catalog_type;
      switch (a_catalog_type) {
        case enumCatalogType.CT_CUSTOMER: {
          m_name = LS.Lc("Customer catalog");
          break;
        }
        case enumCatalogType.CT_MANUFACTURER: {
          m_name = LS.Lc("Manufacturer catalog");
          break;
        }
        case enumCatalogType.CT_SUPPLIER: {
          m_name = LS.Lc("Supplier catalog");
          break;
        }
        case enumCatalogType.CT_CONTRACT_NUMBER: {
          m_name = LS.Lc("Contract number");
          break;
        }
        case enumCatalogType.CT_MATERIAL: {
          m_name = LS.Lc("Material catalog");
          break;
        }
        case enumCatalogType.CT_DRAWING: {
          m_name = LS.Lc("Drawing catalog");
          break;
        }
        case enumCatalogType.CT_MACHINE: {
          m_name = LS.Lc("Machine catalog");
          break;
        }
        case enumCatalogType.CT_GAGE: {
          m_name = LS.Lc("Gage catalog");
          break;
        }
        case enumCatalogType.CT_UNITS: {
          m_name = LS.Lc("Units catalog");
          break;
        }
        case enumCatalogType.CT_OPERATOR: {
          m_name = LS.Lc("Operator catalog");
          break;
        }
        case enumCatalogType.CT_CONTRACTOR: {
          m_name = LS.Lc("Contractor catalog");
          break;
        }
        case enumCatalogType.CT_PRODUCT: {
          m_name = LS.Lc("Product catalog");
          break;
        }
        case enumCatalogType.CT_STUFF: {
          m_name = LS.Lc("Stuff catalog");
          break;
        }
        case enumCatalogType.CT_EVENT: {
          m_name = LS.Lc("Event catalog");
          break;
        }
        case enumCatalogType.CT_PROCESS_PARAMETER: {
          m_name = LS.Lc("Process parameter catalog");
          break;
        }
        case enumCatalogType.CT_CAVITY: {
          m_name = LS.Lc("Cavity catalog");
          break;
        }
        default: {
          m_name = LS.Lc("Unknown catalog");
          break;
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Conversion routine
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
      return name;
    }

    //=========================================================================
    /// <summary>
    /// Add catalog item into the catalog
    /// </summary>
    /// <param name="a_catalog_item">Catalog item to add</param>
    /// <returns>Index of just added item</returns>
    public int add(CatalogItem a_catalog_item)
    {
      Debug.Assert(null != a_catalog_item);
      return m_items.Add(a_catalog_item);
    }

    //=========================================================================
    /// <summary>
    /// Get catalog item by it's number
    /// </summary>
    /// <param name="a_number">Number (THIS IS NOT AN INDEX!!!) of catalog item</param>
    /// <returns>Catalog item</returns>
    public CatalogItem get_item(string a_number)
    {
      foreach (CatalogItem ca in m_items) {
        if (ca.number == a_number) return ca;
      }
      return null;
    }

    //=========================================================================
    /// <summary>
    /// Get catalog item by index
    /// </summary>
    /// <param name="a_index">Index [zero-based] of catalog item inside the catalog</param>
    /// <returns>Catalog item</returns>
    public CatalogItem get_item(int a_index)
    {
      foreach (CatalogItem ca in m_items) {
        if (ca.index == a_index) return ca;
      }
      return null;
    }

    //=========================================================================
    /// <summary>
    /// Remove catalog item by it's name
    /// </summary>
    /// <param name="a_catalog_item_name"></param>
    /// <returns></returns>
    public bool remove(string a_catalog_item_name)
    {
      CatalogItem ca = get_item(a_catalog_item_name);
      if (ca == null) return false;
      m_items.Remove(ca);
      return true;
    }

    //=========================================================================
    /// <summary>
    /// Clear catalog
    /// </summary>
    public void clear()
    {
      m_items.Clear();
    }

    //=========================================================================
    /// <summary>
    /// Get count of catalog items
    /// </summary>
    public int count
    {
      get { return m_items.Count; }
    }

    //=========================================================================
    /// <summary>
    /// Get catalog items
    /// </summary>
    public ArrayList items 
    {
      get { return m_items; }
    }

    //=========================================================================
    /// <summary>
    /// Get catalog type
    /// </summary>
    public enumCatalogType catalog_type
    {
      get { return m_catalog_type; }
    }

    //=========================================================================
    /// <summary>
    /// Get/Set catalog name
    /// </summary>
    public string name
    {
      get { return m_name; }
      set { m_name = value; }
    }

    /// <summary> Catalog items </summary>
    private ArrayList m_items = new ArrayList();
    /// <summary> Catalog type </summary>
    private enumCatalogType m_catalog_type;
    /// <summary> Catalog name </summary>
    private string m_name = "";

    #region ICloneable Members

    //=========================================================================
    /// <summary>
    /// Clone catalog
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
      Catalog c = new Catalog(catalog_type);
      c.name = name;
      foreach(CatalogItem ci in items) {
        c.add(ci.Clone() as CatalogItem);
      }
      return c;
    }

    #endregion
  }
}

