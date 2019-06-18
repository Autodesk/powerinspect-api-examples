//=============================================================================
//
//D GathererMeasureCollection class
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
using System.Diagnostics;

using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// GathererMeasureCollection class -- tool to gather measures collection from
  /// PIApp
  /// </summary>
  internal sealed class GathererMeasureCollection
  {
    //=============================================================================
    /// <summary>
    /// GathererMeasureCollection class constructor
    /// </summary>
    public GathererMeasureCollection()
    {
    }

    //=========================================================================
    /// <summary>
    /// Gets measures collection from PIApp
    /// </summary>
    /// <returns></returns>
    public static MeasureCollection GetMeasureCollection(StateKeeper aStateKeeper)
    {

      pi.IPIDocument doc = PIConnector.instance.active_document;

      Debug.Assert(doc!=null, "GathererMeasureCollection.GetMeasureCollection() : PIApp.ActiveDocument is null");

      MeasureCollection mc = new MeasureCollection();

      bool useStateKeeper = (null != aStateKeeper);

      pi.IMeasures measures = doc.Measures as pi.IMeasures;

      Debug.Assert(null!=measures, "GathererMeasureCollection.GetMeasureCollection() : PIApp.ActiveDocument.Measures is null");

      foreach (pi.IMeasure m in measures) 
      {
        if (useStateKeeper) {
          // If we are using SteteKeeper's settings then we need to get only
          // elements that in collection of StateKeeper
          foreach (string mID in aStateKeeper.ExportedMeasures) {
            if (mID == m.UniqueID.ToString() + "_0" ) {
              mc.Add(m);
              break;
            }
          }
        } else {
          if (!m.IsSimulated) mc.Add(m);
        }
      }

      return mc;
    }
  }
}

