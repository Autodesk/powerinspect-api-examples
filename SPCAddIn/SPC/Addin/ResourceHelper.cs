//=============================================================================
//D ResourceHelper.cs
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// Helper class to retrieve information from .NET resources
  /// </summary>
  [ComVisible(false)]
  public class ResourceHelper
  {
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);
    //Used to delete HBITMAP object created in CreateHbitmapFromResource
    //Deletion is essential

    /// <summary>
    /// Extracts an embedded resource as memory chunk
    /// </summary>
    /// <param name="path">Fully quilified resource path</param>
    /// <param name="assembly">Assembly that holds the embeded resource</param>
    /// <returns>Memory chunk or null if no resource found</returns>
    public static Byte[] ExtractResource(string path, Assembly assembly)
    {
      try {
        Stream res_stream = assembly.GetManifestResourceStream(path);
        if (null == res_stream) {
          return null;
        }
        BufferedStream reader = new BufferedStream(res_stream);
        int buf_length = (int)reader.Length;
        Byte[] image = new Byte[buf_length];
        reader.Read(image, 0, buf_length);
        reader.Close();
        return image;
      } catch (System.Exception e) {
        Debug.Fail("Error occurred while extracting icons. " + e.Message);
        return null;
      }
    }

    ///<summary>Loads a bitmap from .NET resource, creates and returns the handle of 
    ///the bitmap, which must be destroyed by DeleteObject</summary>
    ///<param name="assembly">An assembly with a resource</param>
    ///<param name="resourceName">A full name of a resource</param>
    public static IntPtr CreateHbitmapFromResource(string resourceName, Assembly assembly)
    {
      //Create HBITMAP from resources of .NET assembly
      //It will be freed by IAddInManager.SetAddInInfo
      Byte[] image = ExtractResource(resourceName, assembly);
      if (image == null) {
        return IntPtr.Zero;
      }
      MemoryStream stream = new MemoryStream(image);
      Bitmap bmp = new Bitmap(stream);
      return bmp.GetHbitmap();
    }
  }
}
