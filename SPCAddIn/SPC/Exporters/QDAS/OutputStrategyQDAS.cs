//=============================================================================
//D OutputStrategyQDAS.cs
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
using pi = PowerINSPECT;
using System.Diagnostics;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Base class for all output strategies
  /// </summary>
  internal abstract class OutputStrategyQDAS
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_lock_method">Lock method</param>
    /// <param name="a_export_settings">Export setting</param>
    internal OutputStrategyQDAS(enumLockStrategy a_lock_method, ExportSettingsQDAS a_export_settings)
    {
      m_lock_method = a_lock_method;
      m_export_settings = a_export_settings;
    }

    //=========================================================================
    /// <summary>
    /// Begin update
    /// </summary>
    /// <param name="a_update_description">If true, the description will be updated</param>
    internal void begin_update(bool a_update_description)
    {      
      m_update_description = a_update_description;
      update_file_name();
      if (m_update_description) {        
        begin_update_desc();
      }      
      begin_update_val();
    }

    //=========================================================================
    /// <summary>
    /// End update
    /// </summary>
    internal void end_update()
    {      
      if (m_update_description) end_update_desc();
      end_update_val();
    }

    //=========================================================================
    /// <summary>
    /// Write line to description stream
    /// </summary>
    /// <param name="a_line"></param>
    internal void write_line_desc(string a_line)
    {
      // Preconditions
      Debug.Assert(m_lock_desc != null); // ensure we have a locked file
      Debug.Assert(m_fs_desc != null); // ensure file stream exists
      Debug.Assert(m_fs_desc != null); // ensure stream writer exists
      // Output
      m_sw_desc.WriteLine(a_line);
    }

    //=========================================================================
    /// <summary>
    /// Write line to values stream
    /// </summary>
    /// <param name="a_line"></param>
    internal void write_line_val(string a_line)
    {
      // Preconditions
      Debug.Assert(m_lock_val != null); // ensure we have a locked file
      Debug.Assert(m_fs_val != null); // ensure file stream exists
      Debug.Assert(m_fs_val != null); // ensure stream writer exists
      // Output
      m_sw_val.WriteLine(a_line);

    }

    //=========================================================================
    /// <summary>
    /// Description stream writer
    /// </summary>
    internal StreamWriter sw_desc
    {
      get {
        Debug.Assert(m_sw_desc != null);
        return m_sw_desc; 
      }
    }

    internal StreamWriter sw_val
    {
      get {
        Debug.Assert(m_sw_val != null);
        return m_sw_val; 
      }
    }

    //=========================================================================
    /// <summary>
    /// Called before description is being updated
    /// </summary>
    private void begin_update_desc()
    {
      // open file
      open_file(file_name_desc, out m_fs_desc, out m_sw_desc, FileMode.Create);
      // lock file
      m_lock_desc = FactoryLockStrategyQDAS.create(lock_method, file_name_desc);
    }

    //=========================================================================
    /// <summary>
    /// Called after description is updated
    /// </summary>
    private void end_update_desc()
    {
      // close file
      close_file(ref m_fs_desc, ref m_sw_desc);
      // unlock file
      m_lock_desc.Dispose();
      m_lock_desc = null;
    }

    //=========================================================================
    /// <summary>
    /// Called before values being updated
    /// </summary>
    private void begin_update_val()
    {
      // open file
      open_file(file_name_val, out m_fs_val, out m_sw_val, FileMode.Create);
      // lock file (this actually done when opening for this lock strategy)
      m_lock_val = FactoryLockStrategyQDAS.create(lock_method, file_name_val);
    }

    //=========================================================================
    /// <summary>
    /// Called after values updated
    /// </summary>
    private void end_update_val()
    {
      // close file
      close_file(ref m_fs_val, ref m_sw_val);
      // unlock file
      m_lock_val.Dispose();
      m_lock_val = null;
    }

    //=========================================================================
    /// <summary>
    /// Open file
    /// </summary>
    /// <param name="a_file_name">File name</param>
    /// <param name="a_file_stream">File stream to initialize</param>
    /// <param name="a_stream_writer">Stream writer to initialize</param>
    /// <param name="a_mode">Open mode</param>
    protected void open_file(string a_file_name, out FileStream a_file_stream, out StreamWriter a_stream_writer, FileMode a_mode)
    {
      a_file_stream = new FileStream(
       a_file_name,
       a_mode,
       FileAccess.Write,
       (lock_method == enumLockStrategy.LM_SHARE_DENY) ? FileShare.None : FileShare.Read // this is for share access - based lock strategy
     );
     a_stream_writer = new StreamWriter(a_file_stream, Encoding.Default);
    }

    //=========================================================================
    /// <summary>
    /// Close file
    /// </summary>
    /// <param name="a_file_stream">File stream</param>
    /// <param name="a_stream_writer">Stream writer</param>
    protected void close_file(ref FileStream a_file_stream, ref StreamWriter a_stream_writer)
    {
      a_stream_writer.Close();
      a_file_stream.Close();
      a_stream_writer = null;
      a_file_stream = null;
    }

    //=========================================================================
    /// <summary>
    /// Update output file name. Overridden by decedents.
    /// </summary>
    protected abstract void update_file_name();

    //=========================================================================
    /// <summary>
    /// Get lock method
    /// </summary>
    protected enumLockStrategy lock_method
    {
      get {
        return m_lock_method;
      }
    }

    //=========================================================================
    /// <summary>
    /// Get export settings
    /// </summary>
    protected ExportSettingsQDAS export_settings
    {
      get { return m_export_settings; }
    }

    //=========================================================================
    /// <summary>
    /// Get description file name (DFD)
    /// </summary>
    protected abstract string file_name_desc
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Get value file name(DFX)
    /// </summary>
    protected abstract string file_name_val
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Get base name (prefix) of DFD/DFX files
    /// </summary>
    protected string base_name
    {
      get { 
        // Use part number (as this is a mandatory field) as prefix
        return export_settings.part_number; 
      }
    }

    /// <summary> Description file extension </summary>
    protected const string c_desc_ext = ".DFD";
    /// <summary> Values file extension </summary>
    protected const string c_val_ext = ".DFX";

    /// <summary> DFD file stream </summary>
    private FileStream m_fs_desc = null;
    /// <summary> DFD stream writer </summary>
    private StreamWriter m_sw_desc = null;
    /// <summary> DFX file stream </summary>
    private FileStream m_fs_val = null;
    /// <summary> DFX stream writer </summary>
    private StreamWriter m_sw_val = null;
    /// <summary> Lock method </summary>
    private enumLockStrategy m_lock_method;
    /// <summary> Lock strategy for DFD files </summary>
    private LockStrategyQDAS m_lock_desc = null;
    /// <summary> Lock strategy for DFX files </summary>
    private LockStrategyQDAS m_lock_val = null;
    /// <summary> Export settings </summary>
    private ExportSettingsQDAS m_export_settings = null;
    /// <summary> Update description flag </summary>
    private bool m_update_description;
  }

  //===========================================================================
  /// <summary>
  /// Output strategy based on fixed file names concept
  /// </summary>
  internal sealed class OutputStrategyQDASFixedFileName : OutputStrategyQDAS
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_lock_method">Lock method</param>
    /// <param name="a_export_settings">Export settings</param>
    internal OutputStrategyQDASFixedFileName(
      enumLockStrategy a_lock_method, 
      ExportSettingsQDAS a_export_settings
    )
      : base(a_lock_method, a_export_settings)
    {
    }

    //=========================================================================
    /// <summary>
    /// Description file name
    /// </summary>
    protected override string file_name_desc
    {
      get {
        return Path.Combine(export_settings.output_directory, base_name + c_desc_ext);
      }
    }

    //=========================================================================
    /// <summary>
    /// Values file name
    /// </summary>
    protected override string file_name_val
    {
      get {
        return Path.Combine(export_settings.output_directory, base_name + c_val_ext);
      }
    }

    //=========================================================================
    /// <summary>
    /// Update file name
    /// </summary>
    protected override void update_file_name()
    {
      // do nothing
    }
  }

  //===========================================================================
  /// <summary>
  /// Output strategy based on incremental file names concept
  /// </summary>
  internal sealed class OutputStrategyQDASIncrementalFileNames : OutputStrategyQDAS
  {

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_lock_method">Lock method</param>
    /// <param name="a_export_settings">Export settings</param>
    /// <param name="a_num_digits">Number of digits being used to complete file names</param>
    internal OutputStrategyQDASIncrementalFileNames(
      enumLockStrategy a_lock_method, 
      ExportSettingsQDAS a_export_settings      
    )
      : base(a_lock_method, a_export_settings)
    {
      m_num_digits = a_export_settings.incremental_file_name_digits;
      // 0 is a start point, but after first call 
      // to want_* functions this will be at least 1      
    }

    //=========================================================================
    /// <summary>
    /// Update file name
    /// </summary>
    protected override void update_file_name()
    {
      m_index = get_last_free_file_index();
    }

    //=========================================================================
    /// <summary>
    /// Description file name
    /// </summary>
    protected override string file_name_desc
    {
      get {
        return Path.Combine(export_settings.output_directory, base_name + index_str(m_index) + c_desc_ext);
      }
    }

    //=========================================================================
    /// <summary>
    /// Values file name
    /// </summary>
    protected override string file_name_val
    {
      get {
        return Path.Combine(export_settings.output_directory, base_name + index_str(m_index) + c_val_ext);
      }
    }

    //=============================================================================
    /// <summary>
    /// Get unallocated acceptable number of file in QDAS's monitoring directory
    /// </summary>
    /// <returns>Number of file we can create</returns>
    private int get_last_free_file_index()
    {
      int new_index = c_start_index;
      // search through all *.DF? files (*.DFD & *.DFX)
      foreach (string fn in Directory.GetFiles(export_settings.output_directory, base_name + "*.DF?")) {
        string short_fn = Path.GetFileName(fn);
        string cut = short_fn.Substring(0, short_fn.LastIndexOf('.'));
        cut = cut.Substring(base_name.Length);        
        try {
          int idx = Convert.ToInt32(cut, 10);
          if (idx >= new_index) {
            new_index = idx + 1;
          }
        } catch {
          // watch out for incorrect values
          // if we've got one. it will be just.. ignored in this catch.
        }        
      }      
      return new_index; // return next index
    }

    //=========================================================================
    /// <summary>
    /// Build index string using num_digits property value
    /// </summary>
    /// <param name="a_index">Index value</param>
    /// <returns>Index string</returns>
    private string index_str(int a_index)
    {
      return a_index.ToString("D" + m_num_digits.ToString());
    }

    /// <summary> Number of digits of index </summary>
    private int m_num_digits;

    /// <summary> DFD & DFX files index </summary>
    private int m_index = c_start_index;

    /// <summary> Start index </summary>
    private const int c_start_index = 1;
  }

}

