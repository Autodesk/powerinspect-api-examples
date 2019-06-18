//=============================================================================
//
//D ScriptProcessorNetVB class
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
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// VB.NET script processor class
  /// </summary>
  internal class ScriptProcessorNetVB : Autodesk.PowerInspect.AddIns.SPC.ScriptProcessorNet
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// ScriptProcessorNetVB class constructor
    /// </summary>
    /// <param name="aScriptSource">source code of script</param>
    internal ScriptProcessorNetVB(string aScriptSource)
    {
      m_codeDomProvider = new VBCodeProvider();

      string [] refList = GetReferenceList(aScriptSource);

      m_compilerParametres = new CompilerParameters(refList);
      m_compilerParametres.GenerateExecutable = false;
      m_compilerParametres.GenerateInMemory = true;

      m_compilerResults = m_codeDomProvider.CompileAssemblyFromSource(m_compilerParametres,aScriptSource);

      Debug.Assert(m_compilerResults.Errors.Count == 0, "Errors while compiling script");

      foreach (CompilerError err in m_compilerResults.Errors) {
        MessageBox.Show(
          String.Format(LS.Lc("Line: {0}, Col: {1}, Error: {2} - {3}"),err.Line,err.Column,err.ErrorNumber,err.ErrorText),
          LS.Lc("Error while compiling script")
          ); 
      }      

      if (m_compilerResults.Errors.Count > 0) return;

      // Load compiled assembly
      m_assembly = m_compilerResults.CompiledAssembly; 

      // Observe SPCOutputFormatter
      m_spcOuputFormatter = m_assembly.GetType(sp_MAIN_CLASS_NAME);

      if (m_spcOuputFormatter==null) {
        Debug.Assert(false,"Can't find class "+sp_MAIN_CLASS_NAME+" in script file");
        MessageBox.Show(
          String.Format(LS.Lc("Can't find class {0} in script file"),
          sp_MAIN_CLASS_NAME)
        );
        return;
      }

      // create an instance of class SPCOutputFormatter
      m_spcOuputFormatterObj = m_spcOuputFormatter.InvokeMember(
        null,
        BindingFlags.DeclaredOnly | 
        BindingFlags.Public | BindingFlags.NonPublic | 
        BindingFlags.Instance | BindingFlags.CreateInstance,
        null,
        null,
        null//args
      );

      Debug.Assert(m_spcOuputFormatter!=null,"ScriptProcessorNetVB.ScriptProcessorNetVB(): Can't create instance of class "+sp_MAIN_CLASS_NAME);

    }

    //=============================================================================
    /// <summary>
    /// Returns list of referenced assemblies
    /// </summary>
    /// <param name="aScriptSource">source code of script</param>
    /// <returns></returns>
    private string[] GetReferenceList(string aScriptSource)
    {
      StringCollection sc = new StringCollection();
      
      Regex r = new Regex("^'Ref\\s*:\\s*([\\w\\._]+)(?:\\s*,\\s*([\\w\\._]+))*\\s*?;", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                          

      MatchCollection mc = r.Matches(aScriptSource);

      if (mc.Count >  0) {
        foreach (Match m in mc) {
          sc.Add(m.Groups[1].Captures[0].Value + ".dll");
          if (m.Groups.Count > 2) {
            foreach (Capture c in m.Groups[2].Captures) {
              sc.Add(c.Value + ".dll");
            }
          }          
        }        
      }

      String[] sres = new String[sc.Count];
      sc.CopyTo(sres,0);

      return sres;
    }

    #endregion

    #region Properties

    
    //=============================================================================
    /// <summary>
    /// Returns script's language 'ID string'
    /// </summary>
    public override string Language
    {
      get
      {
        return "VBasic.NET";
      }
    }

    #endregion
    

  } // end class ScriptProcessorNetVB

} // end namespace Autodesk.PowerInspect.AddIns.SPC

