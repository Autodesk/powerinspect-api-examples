//=============================================================================
//D DFDCatalogParser.cs
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
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// DFD catalog parser
  /// </summary>
  internal class DFDCatalogParser
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_input_file_name">Name of input DFD file</param>
    /// <param name="a_catalog_manager">Catalog manager that will accept all extracted information</param>
    internal DFDCatalogParser(string a_input_file_name, CatalogManager a_catalog_manager)
    {
      m_input_file_name = a_input_file_name;
      m_catalog_manager = a_catalog_manager;
    }

    //=========================================================================
    /// <summary>
    /// Parse file
    /// </summary>
    /// <returns>TRUE, if succeeded</returns>
    internal bool parse()
    {
      FileInfo fi = new FileInfo(m_input_file_name);
      if (fi.Length == 0) return false;
      
      // pre-requirements
      m_catalog_manager.clear_catalogs();
      // predefines
      string catalogs_regex_str = get_known_catalogs_regex_str();
      // initials
      enumCatalogType current_catalog_type = enumCatalogType.CT_CUSTOMER;
      Catalog current_catalog = null;
      CatalogItem current_catalog_item = null;
      int catalog_item_index = 1;
      int prev_catalog_item_index = -1;
      // parsing...
      string[] lines = File.ReadAllLines(m_input_file_name, Encoding.Default);
      foreach(string line in lines) {
        Match m = null;

        // Catalog declaration header
        m = Regex.Match(line, "K4(" + catalogs_regex_str + ")0\\/0\\s+(.*)", RegexOptions.IgnoreCase);
        if (m.Success) {
          current_catalog_type = str_to_catalog_type(m.Groups[1].Value);
          current_catalog = get_or_create_catalog(current_catalog_type);
          current_catalog.name = (m.Groups[2].Value);
          continue;
        }

        // Catalog item number
        m = Regex.Match(line, "K4(" + catalogs_regex_str + ")2\\/(\\d+)\\s+(.*)", RegexOptions.IgnoreCase);
        if (m.Success) {
          current_catalog_type = str_to_catalog_type(m.Groups[1].Value);
          current_catalog = get_or_create_catalog(current_catalog_type);
          catalog_item_index = Convert.ToInt32(m.Groups[2].Value);          
          if (prev_catalog_item_index != catalog_item_index) {
            // must create new catalog item
            current_catalog_item = new CatalogItem(m.Groups[3].Value, "");
            current_catalog_item.index = catalog_item_index;
            current_catalog.add(current_catalog_item);
          }
          prev_catalog_item_index = catalog_item_index;          
          continue;
        }

        // Catalog item value
        m = Regex.Match(line, "K4(" + catalogs_regex_str + ")3\\/(\\d+)\\s+(.*)", RegexOptions.IgnoreCase);
        if (m.Success) {
          current_catalog_type = str_to_catalog_type(m.Groups[1].Value);
          current_catalog = get_or_create_catalog(current_catalog_type);

          Debug.Assert(current_catalog_item != null);
          current_catalog_item.value_str = m.Groups[3].Value;
          continue;
        }

        // if no one regex matched - just skip the line
      }
      return true;
    }

    //=========================================================================
    /// <summary>
    /// converts integer that written in form of string into catalog 
    /// type enumeration value
    /// </summary>
    /// <param name="a_str"></param>
    /// <returns></returns>
    private static enumCatalogType str_to_catalog_type(string a_str)
    {
      enumCatalogType ct = (enumCatalogType)Convert.ToInt32(a_str);
      Debug.Assert(Enum.IsDefined(typeof(enumCatalogType),ct), "Unknown catalog type");
      return ct;
    }

    //=========================================================================
    /// <summary>
    /// Builds regex that matches all known catalog types string index
    /// (see Q-DAS documentation)
    /// </summary>
    /// <returns></returns>
    private static string get_known_catalogs_regex_str()
    {
      bool first_one = true;
      string res = "";
      foreach (int i in Enum.GetValues(typeof(enumCatalogType))) {
        if (!first_one) res += '|';
        res += i.ToString("00");
        first_one = false;
      }
      return res;
    }

    //=========================================================================
    /// <summary>
    /// Get or create catalog by it's type
    /// </summary>
    /// <param name="a_catalog_type"></param>
    /// <returns></returns>
    private Catalog get_or_create_catalog(enumCatalogType a_catalog_type)
    {
      Catalog c = m_catalog_manager.get_catalog(a_catalog_type);
      if (null != c) return c;
      c = new Catalog(a_catalog_type);
      m_catalog_manager.catalogs.Add(c);
      return c;
    }

    /// <summary> Input file name </summary>
    private readonly string m_input_file_name;
    /// <summary> Catalog manager reference </summary>
    private readonly CatalogManager m_catalog_manager;
  }
}

