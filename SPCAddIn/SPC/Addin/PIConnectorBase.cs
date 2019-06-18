//=============================================================================
//D PIConnectorBase.cs
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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;


using pi = PowerINSPECT;
using PSAddinManager;
using PWICOMMANDBARSLib;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>enable the connection with powerINSPECT</summary>
  public abstract class PIConnectorBase : IPowerSolutionAddIn, IDisposable
  {
    /// <summary>The Default constructor is required for COM interop</summary>
    public PIConnectorBase()
    {
      Assembly asm = this.GetType().Assembly;
      LS.initialise_translation(asm);
      power_inspect = null;
      addin_manager = null;
      m_command_bar = null;
      m_cookie = 0;
    }

    /// <summary>The Finalizer will free all the unmanaged resources</summary>
    ~PIConnectorBase()
    {
      Dispose(false);
    }

    /// <summary>
    /// Get PowerInspect
    /// </summary>
    public pi.Application power_inspect
    {
      get;
      private set;
    }

    /// <summary>
    /// retreives active document
    /// </summary>
    public pi.Document active_document
    {
      get {
        return power_inspect.ActiveDocument;
      }
    }

    /// <summary>Get AddinManager</summary>
    public AddInManager addin_manager
    {
      get;
      private set;
    }

    /// <summary>
    /// Bitmap resource for button
    /// </summary>
    private IntPtr bitmap_resource
    {
      get {
        // If bitmap resource handle is not exists yet we must create one
        if (IntPtr.Zero == m_bitmap_resource) {
          // Extract resource out of assembly
          m_bitmap_resource = ResourceHelper.CreateHbitmapFromResource(
            @"Autodesk.PowerInspect.AddIns.SPC.res.AddInButton.png",
            this.GetType().Assembly
          );

          Debug.Assert(
            IntPtr.Zero != m_bitmap_resource,
            "Cannot load AddInButton.png. Make sure it's embedded into SPC."
          );
        }
        return m_bitmap_resource;
      }
    }

    /// <summary>Reset the stat of your user Interface</summary>
    public void OnResetUserInterface()
    {
      RemoveAllCommandBarButtons();
      CreateCommandBarButtons();
    }

    /// <summary>Actions to performs on connect with PowerINSPECT</summary>
    /// <param name="pAddInManager">The AddInManager</param>
    /// <param name="bFirstTime">boolean to specify wether it is the first time 
    /// connected or not</param>
    /// <param name="cookie">Integer required to add commands</param>
    /// <returns>True if succeed false if not</returns>
    public bool OnConnect(
      AddInManager pAddInManager,
      bool bFirstTime,
      int cookie
    )
    {


      if (!Init(pAddInManager, cookie)) return false;
      try {
        CreateCommandBarButtons();
      } catch (Exception e) {
        Console.WriteLine(LS.Lc("{0} Exception caught."), e.Message);
        MessageBox.Show(LS.error_c + e.ToString(),
          LS.Lc("Name Entry Error ") + e.ToString(),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation);
        return false;
      }
      return true;
    }

    /// <summary>Actions to be perfomed on disconection</summary>
    /// <param name="bLastTime"></param>
    public void OnDisconnect(bool bLastTime)
    {
      RemoveAllCommandBarButtons();

      //we release all the resourses we get
      Dispose();
    }

    /// <summary>Call this method when you want to do the clean up</summary>
    public void Dispose()
    {
      PIAppEventsDetach();
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Do the clean Up this is the only methods to override to performe the 
    /// full clean up
    /// </summary>
    /// <param name="disposing">
    /// If we need to clean the manged code disposing is true
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      ResourceHelper.DeleteObject(m_bitmap_resource);
      addin_manager = null;
      power_inspect = null;
      m_command_ids = null;
      m_command_bar = null;
    }

    /// <summary>
    /// Wrap the reference to the application and the addin manager so that
    /// we will be able to release it in time properly</summary>
    /// <param name="a_addInManager">The reference to the addin manager</param>
    /// <returns>True is succed</returns>
    private bool Init(AddInManager a_addInManager, int cookie)
    {
      try {

        addin_manager = a_addInManager;
        if (addin_manager == null) {
          return false;
        }

        power_inspect = addin_manager.Application as pi.Application;
        if (power_inspect == null) {
          return false;
        }

        m_cookie = cookie;

        // tell addin manager about our new addin
        addin_manager.SetAddInInfo(0, this, bitmap_resource, cookie);

        PIAppEventsAttach();

        m_timer = new System.Timers.Timer(500);
        m_timer.AutoReset = false;
        m_timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed);



      } catch (Exception e) {
        Console.WriteLine(e.ToString());

        throw e;
      }
      return true;
    }

    protected abstract void CreateCommandBarButtons();

    public delegate void AddinButtonClickHandler();

    protected virtual void PIAppEventsAttach()
    {
      power_inspect.DocumentClose +=
        new global::PowerINSPECT._IApplicationEvents_DocumentCloseEventHandler(
          power_inspect_DocumentClose
        );
    }

    protected virtual void PIAppEventsDetach()
    {
      power_inspect.DocumentClose -=
        new global::PowerINSPECT._IApplicationEvents_DocumentCloseEventHandler(
          power_inspect_DocumentClose
        );
    }

    protected virtual void power_inspect_DocumentClose(pi.Document a_doc)
    {
      // Release document object from .NET
      PIAppEventsDetach();
      while (Marshal.ReleaseComObject(a_doc) > 0) { }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      m_timer.Enabled = true;
    }

    private void timer_elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      PIAppEventsAttach();
    }

    protected void AddCommandBarButton(
      string a_button_caption,
      AddinButtonClickHandler a_handler_method,
      string a_command_desc,
      int a_bitmap_offset
    )
    {
      string method_name = a_handler_method.Method.Name;
      int command_id = addin_manager.AddCommand(
        a_command_desc,
        method_name,
        a_bitmap_offset,
        m_cookie
      );
      m_command_ids.Add(command_id);

      CommandBarButton btn = null;

      try {
        btn = command_bar.FindControl(
          PWICommandControlType.pwictrlBUTTON,
          command_id,
          null,
          true,
          true
        ) as CommandBarButton;
      } catch (Exception) {
        // FindControl can throw an exception in case when there's no such 
        // button on the bar.
        // we must handle this correctly
      }

      if (null == btn) {
        // Button not exists. We need to create it
        btn = command_bar.Controls.Add(
          PWICommandControlType.pwictrlBUTTON,
          command_id,
          -1 // means append button to toolbar
        ) as CommandBarButton;
        // force button to be visible
        btn.Visible = true;

        // setup button's style
        //btn.Style = PWICommandBarButtonStyle.pwiButtonIcon;
        // Set up button caption
        btn.Caption = a_button_caption;
      }
    }


    protected void RemoveAllCommandBarButtons()
    {
      foreach (int command_id in m_command_ids) {
        CommandBarButton btn = null;
        try {
          btn = command_bar.FindControl(
            PWICommandControlType.pwictrlBUTTON,
            command_id,
            null,
            true,
            true
          ) as CommandBarButton;
        } catch (Exception) {
          // FindControl can throw an exception in case when there's no such 
          // button on the bar.
          // we must handle this correctly
        }
        if (null != btn) {
          // Button exists and should be deleted
          // delete button
          btn.Delete();
        }
      }

      // As we're dealing with custom  bar we may want to delete it if it 
      // became empty
      if (0 == command_bar.Controls.Count) {
        // hide the bar if there are no more controls on it
        command_bar.Visible = false;
        command_bar.Delete();
      }
    }

    protected CommandBar command_bar
    {
      get {
        // Implement singleton template to access the object
        if (null == m_command_bar) {
          // Access power inspect command bars collection
          CommandBars bars = power_inspect.CommandBars;
          Debug.Assert(
                  null != bars,
                  "Cannot access PowerINPSECT's command bars collection"
                );

          // This constant is taken from PowerINSPECT resources
          const int IDR_PWI_TOOLBAR_ADDIN = 59406;

          string bar_name = "";

          // Try to extract exact name from resources
          if (power_inspect != null) {
            string tooltip =
              power_inspect.ResourceStringById(IDR_PWI_TOOLBAR_ADDIN);
            // Get the first part of the tooltip, which will be the 
            // name of the bar
            string[] delimiter = new string[] { "\n" };
            bar_name = tooltip.Split(
              delimiter,
              2,
              StringSplitOptions.RemoveEmptyEntries
            )[0];
          }

          // If couldn't extract use the last chance: English default name
          if (bar_name == String.Empty) {
            bar_name = "Addin Panel";
          }
          // Get bar by name.
          CommandBar bar_addin = bars[bar_name];
          // do we have such bar?
          if (null == bar_addin) {
            // if not, we should create one

            bar_addin = bars.Add(
              bar_name,
              PWICommandBarPosition.pwiBarTop,
              PWICommandBarType.pwictrlNORMALBAR);
          }
          // show bar anyway
          bar_addin.Visible = true;
          m_command_bar = bar_addin;
        }
        return m_command_bar;
      }
    }

    /// <summary>
    /// Command ID list. Stored here to be able to delete each 
    /// command/button separately from other commands/buttons
    /// This allows to keep bar alive when it shared with other addin.
    /// </summary>
    private List<long> m_command_ids = new List<long>();

    /// <summary>Command bar of addin</summary>
    protected CommandBar m_command_bar;

    /// <summary>
    /// Timer that used to attach/detach events from other events
    /// Thus it allows an execution point to get out from event handler
    /// Before actualy detach an event.
    /// </summary>
    private System.Timers.Timer m_timer;

    /// <summary>Button's bitmap resource</summary>
    private IntPtr m_bitmap_resource;

    /// <summary>The cookie for this connection unique identifier of the session</summary>
    private int m_cookie;
  }
}
