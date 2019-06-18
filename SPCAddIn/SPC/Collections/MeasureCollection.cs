//=============================================================================
//
//D MeasureCollection class
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

using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// MeasureCollection class - specialized collection of measures
  /// </summary>
  [
  GuidAttribute("7173f9bd-4dc2-48cd-8477-91230d1721ad"), 
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual)
  ]
  public sealed class MeasureCollection : CollectionBase
  {
    
    #region Methods

    //=============================================================================
    /// <summary>
    /// MeasureCollection class constructor
    /// </summary>
    public MeasureCollection()
      : base()
    {

    }
    
    //=============================================================================
    /// <summary>
    /// Add specified measure to collection
    /// </summary>
    /// <param name="aMeasure">measure to add</param>
    public int Add(pi.IMeasure aMeasure)
    {
      return List.Add(aMeasure);
    }

    //=============================================================================
    /// <summary>
    /// default index property
    /// </summary>    
    public pi.IMeasure this [int index]
    {      
      get
      {
        return List[index] as pi.IMeasure;
      }
    }

    #endregion

  }
}

