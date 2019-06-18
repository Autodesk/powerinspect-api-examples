//=============================================================================
//D LockStrategyQDAS.cs
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
using System.Text;
using System.IO;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Lock strategies
  /// </summary>
  internal enum enumLockStrategy
  {
    LM_SHARE_DENY,
    LM_ATTR_ARCHIVE,
    LM_ATTR_READONLY
  }

  //===========================================================================
  /// <summary>
  /// Lock strategy factory
  /// </summary>
  internal class FactoryLockStrategyQDAS
  {
    //=========================================================================
    /// <summary>
    /// Create instance strategy class of selected type
    /// </summary>
    /// <param name="a_strategy_type">Type of strategy to instantiate</param>
    /// <param name="a_file_name">Name of file to be locked</param>
    /// <returns>Instance of lock strategy class</returns>
    internal static LockStrategyQDAS create(enumLockStrategy a_strategy_type, string a_file_name)
    {
      switch(a_strategy_type) {
        case enumLockStrategy.LM_SHARE_DENY: return new LockStrategyQDASFileShare(a_file_name);
        case enumLockStrategy.LM_ATTR_ARCHIVE: return new LockStrategyQDASFileAttr(a_file_name, FileAttributes.Archive);
        case enumLockStrategy.LM_ATTR_READONLY: return new LockStrategyQDASFileAttr(a_file_name, FileAttributes.ReadOnly);
        default: return null;
      }
    }
  }

  //===========================================================================
  /// <summary>
  /// Base class for all lock strategies
  /// </summary>
  internal abstract class LockStrategyQDAS : IDisposable
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_file_name">Name of file to be locked</param>
    internal LockStrategyQDAS(string a_file_name)
    {
      m_file_name = a_file_name;
    }

    #region IDisposable Members
    //=========================================================================
    /// <summary>
    /// Instance dispose
    /// </summary>
    public virtual void Dispose()
    {
    }
    #endregion

    //=========================================================================
    /// <summary>
    /// Name of file strategy is applied to.
    /// </summary>
    protected string file_name
    {
      get { return m_file_name; }
    }

    /// <summary> Name of file strategy is applied to. </summary>
    private string m_file_name;
  }

  //===========================================================================
  /// <summary>
  /// File-share based lock strategy
  /// </summary>
  internal sealed class LockStrategyQDASFileShare : LockStrategyQDAS
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_file_name">Name of locked file</param>
    internal LockStrategyQDASFileShare(string a_file_name)
      : base(a_file_name)
    {
    }

    #region IDisposable Members
    //=========================================================================
    /// <summary>
    /// Instance dispose
    /// </summary>
    public override void Dispose()
    {
      base.Dispose();
    }
    #endregion
  }

  //===========================================================================
  /// <summary>
  /// Attribute-based file lock strategy
  /// </summary>
  internal sealed class LockStrategyQDASFileAttr : LockStrategyQDAS
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_file_name">Name of file to apply strategy to</param>
    /// <param name="a_lock_attribute">File attribute that will be used as LOCK attribute</param>
    internal LockStrategyQDASFileAttr(string a_file_name, FileAttributes a_lock_attribute)
      : base(a_file_name)
    {
      m_lock_attribute = a_lock_attribute;
      lock_file();
    }

    //=========================================================================
    /// <summary>
    /// Lock file
    /// </summary>
    private void lock_file()
    {
      // do nothing. we have already locked the file when open the stream      
      FileAttributes attributes = File.GetAttributes(file_name);
      // wait until Q-DAS releases the file
      do {
        attributes = File.GetAttributes(file_name);
      } while ((attributes & m_lock_attribute) != 0); // WARNING!

      attributes = attributes | m_lock_attribute; // set lock attribute
      File.SetAttributes(file_name, attributes);
    }

    //=========================================================================
    /// <summary>
    /// Unlock file
    /// </summary>
    private void unlock_file()
    {
      File.SetAttributes(file_name, File.GetAttributes(file_name) & (~m_lock_attribute));
    }

    //=========================================================================
    /// <summary>
    /// Get file share mode
    /// </summary>
    internal FileShare file_share_mode
    {
      get {
        // allow read sharing, because we are using attribute-based
        // file locking.
        return FileShare.Read; 
      }
    }

    /// <summary> Lock attribute </summary>
    private FileAttributes m_lock_attribute;

    #region IDisposable Members

    //=========================================================================
    /// <summary>
    /// Instance dispose
    /// </summary>
    public override void Dispose()
    {
      unlock_file();
      base.Dispose();      
    }

    #endregion
  }


}

