//=============================================================================
//
//D ScriptManager class - Script-independent processor
//D main purpose of this class is supporing ANY versions of scripts
//D and providing COMMON interface for methods' invoking
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
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Autodesk.PowerInspect.AddIns.SPC.ScriptProcessing;


namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=========================================================================
  /// <summary>
  /// ScriptManager class - script abstraction layer class
  /// Provides COMMON interface for script execution
  /// Supports: 
  ///   VBScript, JScript - for backward compatibility
  ///   VBScript.NET, JScript.NET, CSharp.NET - as a primary scripting languages
  /// Note:
  ///   it is necessary (for .NET versions of scripts) to use SPCOutputFormatter
  ///   class as main class of each script.
  ///   Source code of old version of scripts (VBScript & JScript) should be
  ///   wrapped by <script language="..."></script> tag!
  /// </summary>
  internal class ScriptManager
  {
    
    #region Methods

    //=============================================================================
    /// <summary>
    /// ScriptManager class constructor
    /// </summary>
    internal ScriptManager()
    {
    }

    //=============================================================================
    /// <summary>
    /// Do `smart` load algorithm for a specified source file.
    /// </summary>
    /// <param name="aFileName">Name of source file</param>
    /// <returns></returns>
    public bool LoadSourceFromFile(string aFileName)
    {
      if (!File.Exists(aFileName)) {
        Debug.Assert(false);
        return false;
      }
      m_fileName = aFileName;
      StreamReader sr = new StreamReader(aFileName, System.Text.Encoding.Default, true);
      string srcStr = sr.ReadToEnd();
      sr.Close();
      m_good = ChoiseProcessor(srcStr);
      return m_good;
    }

    //=============================================================================
    /// <summary>
    /// Calls specified method withing the main class of script (or just function
    /// within script, in case of old script version)
    /// </summary>
    /// <param name="aMethodName">name of method/function to call</param>
    /// <param name="args">arguments, passed to calling method/function</param>
    /// <returns>true - if everything goes OK</returns>
    public bool InvokeMethod(string aMethodName, params object[] args)
    {
      if (m_scriptProcessor!=null) {
        return m_scriptProcessor.InvokeMethod(aMethodName,args);
      } else return false;
    }

    //=============================================================================
    /// <summary>
    /// Looks inside a source, and choice corresponding script processor
    /// </summary>
    /// <param name="aScriptSource">source code of script</param>
    /// <returns>true - if corresponding processor was found</returns>
    private bool ChoiseProcessor(string aScriptSource)
    {
      Regex r = new Regex(
        "<script\\s*?language\\s*?=\\\"(\\w+)\\\"\\s*?>(.*)</script>",
        RegexOptions.Singleline | RegexOptions.IgnoreCase
      );

      MatchCollection mc = r.Matches(aScriptSource);
      
      if (mc.Count > 0) {
        m_scriptProcessor = new ScriptProcessorPI(aScriptSource);
        return true;
      }

      return false; 
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// current script processor
    /// </summary>
    private ScriptProcessor m_scriptProcessor;

    //=============================================================================
    /// <summary>
    /// name of file from which source was grabbed 
    /// </summary>
    private string m_fileName;

    //=============================================================================
    /// <summary>
    /// this field holds TRUE, if no errors was happened while choosing processor
    /// </summary>
    private bool m_good;

    #endregion

    #region Properties

    
    //=============================================================================
    /// <summary>
    /// Returns name of file from which source was grabbed 
    /// </summary>
    public string FileName
    {
      get
      {
        return m_fileName;
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns TRUE, if no errors was happened while choosing processor
    /// </summary>
    public bool Good
    {
      get
      {
        return m_good;
      }
    }

    #endregion

  }

}

