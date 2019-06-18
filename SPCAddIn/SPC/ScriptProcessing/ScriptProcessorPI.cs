//=============================================================================
//
//D Wrapper class for PI's own script engine.
//
// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using scripting;
using System.Windows.Forms;

namespace Autodesk.PowerInspect.AddIns.SPC.ScriptProcessing
{
  class ScriptProcessorPI : Autodesk.PowerInspect.AddIns.SPC.ScriptProcessor
  {
    /// <summary> The script engine this class is a wrapper for. </summary>
    scrScriptHostCOM m_scripthost;


    //=============================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    internal ScriptProcessorPI(string aScriptSource)
    {
      m_scripthost = new scrScriptHostCOM();
      m_scripthost.LoadCodeFromString(aScriptSource);
    }


    //=============================================================================
    /// <summary>
    /// Calls specified method within script.
    /// </summary>
    /// <param name="aMethodName">name of method/function to call</param>
    /// <param name="args">arguments, passed to calling method/function</param>
    /// <returns>true - if everything goes OK</returns>
    public override bool InvokeMethod(string aMethodName, params object[] args)
    {
      bool success = false;
      m_scripthost.Run();

      try {
        object value = m_scripthost.InvokeMethod(aMethodName, args);
        success = true;
      }
      catch (Exception) {
        MessageBox.Show("An error occurred during script execution.");
      }
      finally
      {
        m_scripthost.Stop();
      }

      return success;
    }


    //=============================================================================
    /// <summary>
    /// Returns script's language 'ID string'
    /// </summary>
    public override string Language
    {
      get
      {
        return ""; // Not applicable since this class supports several languages.
      }
    }

  }
}
