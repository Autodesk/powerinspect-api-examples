//=============================================================================
//
//D GathererReportItemCollection class
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

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// GathererReportItemCollection class -- tool to gather report items collection 
  /// from PIApp
  /// </summary>
  internal sealed class GathererReportItemCollection
  {
    //=============================================================================
    /// <summary>
    /// GathererReportItemCollection class constructor
    /// </summary>
    internal GathererReportItemCollection()
    {
    }

    //=============================================================================
    /// <summary>
    /// Gets report items collection from tree
    /// </summary>
    /// <param name="aSequenceTreeRootNodes">source tree</param>
    /// <param name="aStateKeeper">the state keeper</param>
    /// <returns>collection of report items</returns>
    static public ReportItemCollection GetReportItems(XTreeNodeCollection aSequenceTreeRootNodes, StateKeeper aStateKeeper)
    {
      ReportItemCollection ric = new ReportItemCollection();
      CollectReportItemsFromTree(aSequenceTreeRootNodes,ref ric, aStateKeeper);
      return ric;
    }

    //=============================================================================
    /// <summary>
    /// Sub-routine used by GetReportItems to walk through the tree recursively
    /// </summary>
    /// <param name="aTNC">Tree node collection to go through</param>
    /// <param name="aRIC">Reference to instance of ReportItemCollection</param>
    /// <param name="aStateKeeper">the state keeper</param>
    static private void CollectReportItemsFromTree(XTreeNodeCollection aTNC, ref ReportItemCollection aRIC, StateKeeper aSteteKeeper)
    {

      bool useStateKeeper = (null != aSteteKeeper);

      foreach (XTreeNode tn in aTNC) 
      {
        if (tn.Tag != null) 
        {
          if (useStateKeeper) {
            // if we are using settings from StateKeeper then we should add
            // to collection only elements having appropriate Key
            foreach (string riKey in aSteteKeeper.ReportVariables) {
              String originalKey = (tn.Tag as ReportItem).Key;
              if ( originalKey == riKey) {
                aRIC.Add(tn.Tag as ReportItem);
                break;
              }              
            }
          } else {
            // otherwise - we add every report item
            aRIC.Add(tn.Tag as ReportItem);
          }          
        }
        if ((tn.Nodes != null) && (tn.Nodes.Count > 0)) 
        {
          CollectReportItemsFromTree(tn.Nodes,ref aRIC, aSteteKeeper);
        }
      }      
    }
  }
}

