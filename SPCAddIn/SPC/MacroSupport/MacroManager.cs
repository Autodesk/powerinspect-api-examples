//=============================================================================
//D MacroManager.cs
//
// Contains the list of macro commands and utility functions to handle them.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pi = PowerINSPECT;
using System.Text.RegularExpressions;
using System.IO;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  public static class MacroManager
  {
    /// <summary> 
    /// Macro that outputs active PI document name
    /// </summary>
    private static MacroItem macro_document_name = new MacroItem {
      title = LS.Lc("Document name"),
      macro = "Document.Name",
      command = (MacroCallContext context) => {
        if (context.pi_doc == null) return String.Empty;
        return context.pi_doc.Name;
      }
    };

    /// <summary> 
    /// Macro that outputs active PI document name using short format
    /// (no file extension)
    /// </summary>
    private static MacroItem macro_document_short_name = new MacroItem {
      title = LS.Lc("Document name (short)"),
      macro = "Document.ShortName",
      command = (MacroCallContext context) => {
        if (context.pi_doc == null) return String.Empty;        
        return Path.GetFileNameWithoutExtension(context.pi_doc.Name);
      }
    };

    /// <summary> 
    /// Macro that outputs measurement name
    /// </summary>
    private static MacroItem macro_measure_name = new MacroItem {
      title = LS.Lc("Measure name"),
      macro = "Measure.Name",
      command = (MacroCallContext context) => {
        if (context.pi_measure == null) return String.Empty;
        return context.pi_measure.Name;
      }
    };

    /// <summary> 
    /// All available macro are listed here
    /// </summary>
    public static List<MacroItem> available_macros = new List<MacroItem> {
      macro_document_name,
      macro_document_short_name,
      macro_measure_name
    };

    //=========================================================================
    /// <summary> 
    /// Looks up MacroItem by macro text
    /// </summary>
    /// <param name="macro">The macro text</param>
    public static MacroItem find_macro(string macro)
    {
      return available_macros.Find((MacroItem mi) => mi.macro == macro);
    }

    //=========================================================================
    /// <summary> 
    /// Executes and substitutes all macro within the src_text.
    /// Passes macro_context to each macro call.
    /// </summary>
    /// <param name="src_text">The source text</param>
    /// <param name="macro_context">The macro context</param>
    public static string subst_all(
      string src_text, 
      MacroCallContext macro_context
    )
    {
      string res = "";

      Regex re = new Regex(
        MacroItem.escape_tag + @"(\w[\w\d\.]+)" + MacroItem.escape_tag
      );
      MatchCollection matches = re.Matches(src_text);

      int src_pos = 0;

      foreach (Match m in matches) {
        if (src_pos < m.Index) { // If there are chars to copy          
          int copy_len = m.Index - src_pos;
          res += src_text.Substring(src_pos, copy_len);
          src_pos += copy_len;
        }
        MacroItem macro_item = find_macro(m.Groups[1].Value);
        if (macro_item != null) {
          // Calculate macro value and append to result string
          string macro_result = macro_item.command(macro_context);
          res += macro_result;          
        } else {
          // Not supported macro.
          // Copy macro text to the result string
          res += m.Value;          
        }
        src_pos += m.Length;
      }

      // copy remaining tail of source string if any
      if (src_pos < src_text.Length) {
        res += src_text.Substring(src_pos);
      }

      return res;
    }

    //=========================================================================
    /// <summary> 
    /// Returns true if text contains per measure macro (like "Measure.Name") 
    /// </summary>
    public static bool contains_per_measure_macros(string text)
    {
      List<MacroItem> measure_related_macros = new List<MacroItem> {
        macro_measure_name
      };

      return measure_related_macros.Exists(
        (MacroItem mi) => text.Contains(mi.escaped_macro)
      );
    }


  }
}
