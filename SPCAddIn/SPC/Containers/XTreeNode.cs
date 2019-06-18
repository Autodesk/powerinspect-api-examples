//=============================================================================
//D XTreeNode.cs
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

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// XTreeNode class -- represents node in XTreeCollection
  /// </summary>
  internal class XTreeNode //: IXTreeNode
  {
    //=============================================================================
    /// <summary>
    /// XTreeNode class constructor
    /// </summary>
    internal XTreeNode()
    {
      m_text = "";
      m_nodes = new XTreeNodeCollection(this);
      m_parent = null;
    }

    //=============================================================================
    /// <summary>
    /// XTreeNode class constructor
    /// </summary>
    /// <param name="aText">node's text property value</param>
    internal XTreeNode(string aText)
    {
      m_parent = null;
      m_text = aText;
      m_nodes = new XTreeNodeCollection(this);      
    }

    //=============================================================================
    /// <summary>
    /// XTreeNode class destructor
    /// </summary>
    ~XTreeNode()
    {
    }

    #region Fields

    //=============================================================================
    /// <summary>
    /// Child nodes collection
    /// </summary>
    private XTreeNodeCollection m_nodes;

    private XTreeNode m_parent;

    //=============================================================================
    /// <summary>
    /// Tag can contain any associated object
    /// </summary>
    private Object m_tag;

    //=============================================================================
    /// <summary>
    /// Text that assigned with this node
    /// </summary>
    private string m_text;

    #endregion

    #region Properties


    //=============================================================================
    /// <summary>
    /// Text that assigned with this node
    /// </summary>
    public string Text
    {
      get
      {
        return m_text;
      }
      set
      {
        m_text = value;
      }
    }

    //=============================================================================
    /// <summary>
    /// Child nodes of this node
    /// </summary>
    public XTreeNodeCollection Nodes
    {
      get
      {
        return m_nodes as XTreeNodeCollection;
      }
    }

    //=============================================================================
    /// <summary>
    /// Tag property can contain reference to any object associated with this node
    /// </summary>
    public Object Tag
    {
      get
      {
        return m_tag;
      }
      set
      {
        m_tag = value;
      }
    }

    public XTreeNode Parent
    {
      get
      {
        return m_parent;
      }
      set
      {
        m_parent = value;
      }
    }

    #endregion

  }
}

