//=============================================================================
//D LocalisationSupport.cs
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
using System.Reflection;
using System.Runtime.InteropServices;
using PILocaleManagerLib;
using System.Text.RegularExpressions;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// Class for localization of strings 
  /// </summary>
  [ComVisible(false)]
  public class LS
  {
    ///<summary></summary>
    public static string error_c
    {
      get {
        return LS.Lc("Error");
      }
    }

    ///<summary>Returns localized string</summary>
    ///<param name="a_str">a given string</param>
    public static string Lc(string a_str)
    {
      if (null == m_locale_manager) {
        return a_str;
      }
      object[] o = new object[0];
      return m_locale_manager.LocalizeString(m_translation_file_cookie, a_str, ref o);
    }

    ///<summary>Initializes translation for a project with a given name</summary>
    ///<param name="a_project_name">a given name of a project</param>
    public static void initialise_translation(Assembly a_main_assembly)
    {
      if (null == m_locale_manager) {
        try {
          m_locale_manager = new PILocaleManager();

          string addin_path = a_main_assembly.Location;
          string addin_directory = Path.GetDirectoryName(addin_path);
          m_locale_manager.AddSearchPath(addin_directory);

          long hinst = System.Diagnostics.Process.GetCurrentProcess().Handle.ToInt64();
          m_translation_file_cookie = m_locale_manager.GetLangFileCookie(
            hinst,
            a_main_assembly.GetName().Name
          );
        } catch (Exception) {
          m_locale_manager = null;
        }
      }
    }


    /// <summary>
    /// Object for localization of strings
    /// </summary>
    private static PILocaleManager m_locale_manager = null;

    /// <summary>
    /// Indicates translation file
    /// </summary>
    private static int m_translation_file_cookie = 0;
  }
}
