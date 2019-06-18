//=============================================================================
//
//D ReportItemCollection class
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
using System.Runtime.InteropServices;
using System.Collections;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //=============================================================================
  /// <summary>
  /// ReportItemCollection class - implements specialized collection of report items
  /// </summary>
  [
  GuidAttribute("5cee3a4b-6423-4c9b-865e-600ad440a276"), 
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual)
  ]
  public sealed class ReportItemCollection : System.Collections.CollectionBase
  {
    //=============================================================================
    /// <summary>
    /// ReportItemCollection class constructor
    /// </summary>
    public ReportItemCollection()
      : base()
    {
    }

    //=============================================================================
    /// <summary>
    /// Adds specified item to collection
    /// </summary>
    /// <param name="aReportItem">item to add</param>
    public int Add(IReportItem aReportItem)
    {
      return List.Add(aReportItem);
    }

    //=============================================================================
    /// <summary>
    /// Returns index of specified item in collection
    /// </summary>
    /// <param name="aReportItem">Item which index to get</param>
    /// <returns>index of an item</returns>
    public int IndexOf(IReportItem aReportItem)
    {
      for (int i = 0; i < List.Count; i++) {
        if ((List[i] as IReportItem) == aReportItem) {
          return i;
        }
      }
      return -1;
    }

    //=============================================================================
    /// <summary>
    /// Default index property
    /// </summary>    
    public IReportItem this [int index]
    {      
      get
      {        
        return List[index] as IReportItem;
      }
    }

  }
}

