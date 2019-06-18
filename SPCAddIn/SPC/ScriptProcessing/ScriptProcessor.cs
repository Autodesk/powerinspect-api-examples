//=============================================================================
//
//D ScriptProcessor class
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
  /// Base class for all script processors
  /// </summary>
  internal abstract class ScriptProcessor
  {    
    
    #region Constants

    //=============================================================================
    /// <summary>
    /// This constant determines name of the main class of every .NET script
    /// </summary>
    public const string sp_MAIN_CLASS_NAME = "SPCOutputFormatter";

    #endregion


    #region Methods

    //=============================================================================
    /// <summary>
    /// ScriptProcessor class constructor
    /// </summary>
    internal protected ScriptProcessor()
    {

    }

    //=============================================================================
    /// <summary>
    /// Calls specified method withing the main class of script (or just function
    /// within script, in case of old script version)
    /// </summary>
    /// <param name="aMethodName">name of method/function to call</param>
    /// <param name="args">arguments, passed to calling method/function</param>
    /// <returns>true - if everything goes OK</returns>
    public abstract bool InvokeMethod(string aMethodName, params object[] args);

    #endregion

    #region Properties

    //=============================================================================
    /// <summary>
    /// Returns language ID string of currently loaded script
    /// </summary>
    public abstract string Language {get;}

    #endregion

  } // end class ScriptProcessor

} // end namespace Autodesk.PowerInspect.AddIns.SPC

