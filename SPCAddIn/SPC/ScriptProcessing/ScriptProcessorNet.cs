//=============================================================================
//D ScriptProcessorNet.cs
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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// implementation of .Net-based scripts processor
  /// </summary>
  internal abstract class ScriptProcessorNet : Autodesk.PowerInspect.AddIns.SPC.ScriptProcessor
  {

    #region Methods

    internal ScriptProcessorNet()
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
    public override bool InvokeMethod(string aMethodName, params object[] args)
    {

      Debug.Assert(m_spcOuputFormatter!=null,"Trying to invoke member of not existing class instance");

      // Call a method of instance of class
      m_spcOuputFormatter.InvokeMember(aMethodName, 
        BindingFlags.DeclaredOnly | 
        BindingFlags.Public | BindingFlags.NonPublic | 
        BindingFlags.Instance | BindingFlags.InvokeMethod, 
        null, 
        m_spcOuputFormatterObj, 
        args
        );

      return true;

    }


    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// Code DOM provider used by .NET script
    /// </summary>
    protected CodeDomProvider m_codeDomProvider;

    //=============================================================================
    /// <summary>
    /// Compilation results, created by compiler
    /// </summary>
    protected CompilerResults m_compilerResults;

    //=============================================================================
    /// <summary>
    /// Assembly created by compiler
    /// </summary>
    protected Assembly m_assembly;

    //=============================================================================
    /// <summary>
    /// Type of main class of script
    /// </summary>
    protected Type m_spcOuputFormatter;

    //=============================================================================
    /// <summary>
    /// object of class m_spcOuputFormatter
    /// </summary>
    protected Object m_spcOuputFormatterObj;

    //=============================================================================
    /// <summary>
    /// Parameters passed to compiler
    /// </summary>
    protected CompilerParameters m_compilerParametres;

    #endregion

  }
}

