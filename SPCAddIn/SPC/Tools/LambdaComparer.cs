//=============================================================================
//D LambdaComparer - implements IEqualityComparer and allows to inject lambda 
//  callbacks via constructor.
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
using System.Diagnostics;


namespace Autodesk.PowerInspect.AddIns.SPC
{
  public class LambdaComparer<T> : IEqualityComparer<T>
  {
    //=============================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="comparer">Compare function</param>
    public LambdaComparer(Func<T, T, bool> comparer)
      : this(comparer, hash => 0)
    {
    }

    //=============================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="comparer">Compare function</param>
    /// <param name="hash">Hash calculatio function</param>
    public LambdaComparer(Func<T, T, bool> comparer, Func<T, int> hash)
    {
      Debug.Assert(comparer != null);
      Debug.Assert(hash != null);
      m_comparer = comparer;
      m_hash = hash;
    }

    
    #region IEqualityComparer<T>

    //=============================================================================
    /// <summary>
    /// Returns true if entities are equal. Uses injected function to compare.
    /// </summary>
    /// <param name="x">First entity</param>
    /// <param name="y">Second entity</param>
    /// <returns>True if equal</returns>
    public bool Equals(T x, T y)
    {
      return m_comparer(x, y);
    }

    
    //=============================================================================
    /// <summary>
    /// Calculates hash code for an entity via callback function
    /// </summary>
    /// <param name="obj">Entity</param>
    /// <returns>Hash code</returns>
    public int GetHashCode(T obj)
    {
      return m_hash(obj);
    }
    #endregion

    /// <summary> Actual compare function </summary>
    private readonly Func<T, T, bool> m_comparer;
    /// <summary> Hash calculation function </summary>
    private readonly Func<T, int> m_hash;

  }
}
