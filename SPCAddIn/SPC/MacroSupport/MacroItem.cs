//=============================================================================
//D MacroItem.cs
//
// Represents the macro (title, macro text and action command)
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

namespace Autodesk.PowerInspect.AddIns.SPC
{
  public class MacroItem
  {
    //=============================================================================
    /// <summary> 
    /// The exact text of macro escaped with special characters
    /// i.e.: ##Measure.Name##
    /// </summary>
    public string escaped_macro
    //-----------------------------------------------------------------------------
    {
      get {
        return escape_tag + macro + escape_tag;
      }
    }

    /// <summary> Macro title (user readable) </summary>
    public string title;
    /// <summary> The exact macro text (what shown in textbox) </summary>
    public string macro;
    /// <summary> Macro command action </summary>
    public Func<MacroCallContext, string> command;
    /// <summary> Escape tag sequence </summary>
    public static string escape_tag = "##";
  }
}
