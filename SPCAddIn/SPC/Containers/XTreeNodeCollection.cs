//=============================================================================
//
//D XTreeNodeCollection class
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
using System.Collections;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //=============================================================================
  /// <summary>
  /// XTreeNodeCollection - collection that can replace TreeNodeCollection in scripts
  /// it's needed because it's impossible to create an instance of TreeNodeCollection
  /// </summary>
  internal class XTreeNodeCollection : CollectionBase
  {
    //=============================================================================
    /// <summary>
    /// XTreeNodeCollection class constructor
    /// </summary>
    internal XTreeNodeCollection()
    {
      m_parent = null;
    }

    internal XTreeNodeCollection(XTreeNode aParent)
    {
      m_parent = aParent;
    }

    //=============================================================================
    /// <summary>
    /// Adds node to the collection
    /// </summary>
    /// <param name="aNode">a node to add</param>
    public void Add(XTreeNode aNode)
    {
      aNode.Parent = m_parent;
      List.Add(aNode);
    }

    //=============================================================================
    /// <summary>
    /// Gets specified item from collection
    /// </summary>
    /// <param name="aIndex">index of item in collection</param>
    /// <returns></returns>
    public XTreeNode ItemAt(int aIndex)
    {
      return List[aIndex] as XTreeNode;
    }

    #region Fields

    private XTreeNode m_parent;

    #endregion

    //=============================================================================
    /// <summary>
    /// Default index property for collection
    /// </summary>
    public XTreeNode this [int index]
    {
      get
      {
        return List[index] as XTreeNode;
      }
      set
      {
        List[index] = value;
      }
    }

  }
}
