//=============================================================================
//D CatalogManager.cs
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
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Catalog manager
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("8DFDC085-AB37-4266-A59A-691B94C6C662")
  ]
  public class CatalogManager : ICloneable
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal CatalogManager()
    {
      foreach (enumCatalogType ct in Enum.GetValues(typeof(enumCatalogType))) {
        m_catalogs.Add(new Catalog(ct));
      }
    }

    //=========================================================================
    /// <summary>
    /// Clear all catalogs
    /// </summary>
    public void clear_catalogs()
    {
      foreach(Catalog c in catalogs) {
        c.clear();
      }
    }

    //=========================================================================
    /// <summary>
    /// Get catalog list
    /// </summary>
    public ArrayList catalogs
    {
      get { return m_catalogs; }
    }

    //=========================================================================
    /// <summary>
    /// Get catalog by it's type
    /// </summary>
    /// <param name="a_catalog_type"></param>
    /// <returns></returns>
    public Catalog get_catalog(enumCatalogType a_catalog_type)
    {
      foreach(Catalog c in m_catalogs) {
        if (c.catalog_type == a_catalog_type) return c;
      }
      return null;
    }

    //=========================================================================
    /// <summary>
    /// Load all catalogs from DFD file
    /// </summary>
    /// <param name="a_file_name"></param>
    /// <returns></returns>
    public bool load_from_dfd(string a_file_name)
    {
      DFDCatalogParser dfdcp = new DFDCatalogParser(a_file_name, this);
      return dfdcp.parse();
    }

    //=========================================================================
    /// <summary>
    /// Load all catalogs from state keeper
    /// </summary>
    /// <param name="a_state_keeper">State keeper</param>
    /// <returns>TRUE, if succeeded</returns>
    public bool load_from_state_keeper(StateKeeper a_state_keeper)
    {
      if (a_state_keeper.catalog_information.Count == 0) return false;
      int idx = 0;
      // clear all catalogs we have at the moment
      clear_catalogs();
      // total catalogs number
      int catalogs_count = Convert.ToInt32(a_state_keeper.catalog_information[idx++]);

      for (int i = 0; i < catalogs_count; i++ ) {                
        // catalog type
        enumCatalogType ct = (enumCatalogType)Convert.ToInt32(a_state_keeper.catalog_information[idx++]);
        // catalog name
        Catalog c = get_catalog(ct);
        c.name = a_state_keeper.catalog_information[idx++];
        // number of catalog items of the catalog
        int catalog_items_count = Convert.ToInt32(a_state_keeper.catalog_information[idx++]);
        for (int j = 0; j < catalog_items_count; j++) {
          // catalog item number
          int ci_index = Convert.ToInt32(a_state_keeper.catalog_information[idx++]);
          // catalog item number
          string ci_number = a_state_keeper.catalog_information[idx++];
          // catalog item value
          string ci_value = a_state_keeper.catalog_information[idx++];
          CatalogItem ci = new CatalogItem(ci_number, ci_value);
          ci.index = ci_index;
          c.add(ci);
        }        
      }
      return true;
    }

    //=========================================================================
    /// <summary>
    /// Save using state keeper
    /// </summary>
    /// <param name="a_state_keeper">Stake keeper</param>
    internal void save_to_state_keeper(StateKeeper a_state_keeper)
    {
      // clear collection as the first thing
      a_state_keeper.catalog_information.Clear();
      // total catalogs number
      a_state_keeper.catalog_information.Add(catalogs.Count.ToString());
      foreach(Catalog c in catalogs) {
        // catalog type
        a_state_keeper.catalog_information.Add(((int)c.catalog_type).ToString());
        // catalog name
        a_state_keeper.catalog_information.Add(c.name);        
        // number of catalog items of the catalog
        a_state_keeper.catalog_information.Add(c.count.ToString());        
        foreach(CatalogItem ci in c.items) {
          // catalog item index
          a_state_keeper.catalog_information.Add(ci.index.ToString());
          // catalog item number
          a_state_keeper.catalog_information.Add(ci.number);
          // catalog item value
          a_state_keeper.catalog_information.Add(ci.value_str);
        }
      }
    }

    /// <summary> Catalog list </summary>
    private ArrayList m_catalogs = new ArrayList();

    #region ICloneable Members

    //=========================================================================
    /// <summary>
    /// Clone catalog manager
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
      CatalogManager cm = new CatalogManager();

      for (int i = 0; i < catalogs.Count; i++ ) {
        cm.catalogs[i] = (catalogs[i] as Catalog).Clone();
      }

      return cm;
    }

    #endregion
  }
}

