//=============================================================================
//D XListView class
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Autodesk.PowerInspect.AddIns.SPC.Controls
{
  //=============================================================================
  /// <summary>
  /// XListView - flicker free list view
  /// </summary>
  [ComVisible(false)]
  public class XListView : System.Windows.Forms.ListView
  {

    #region Fields
    //=============================================================================
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;
    #endregion

    //=============================================================================
    /// <summary>
    /// XListView class constructor
    /// </summary>
    public XListView()
    {
      // This call is required by the Windows.Forms Form Designer.
      InitializeComponent();

      // Activate double buffering
      SetStyle( ControlStyles.AllPaintingInWmPaint
        /*| ControlStyles.UserPaint*/
        | ControlStyles.DoubleBuffer
        | ControlStyles.ResizeRedraw, true );

      // Allows catching the WM_ERASEBKGND message
      SetStyle( ControlStyles.EnableNotifyMessage, true);
    }

    //=============================================================================
    /// <summary>
    /// Message filtering routine
    /// </summary>
    /// <param name="aMsg">message</param>
    protected override void OnNotifyMessage (Message aMsg)
    {
      // filter WM_ERASEBKGND
      if(aMsg.Msg != 0x14) {
        base.OnNotifyMessage(aMsg);
      }
    }

    //=============================================================================
    /// <summary>
    /// Dummy for Paint background event
    /// </summary>
    /// <param name="aPaintEventArgs">Event arguments</param>
    protected override void OnPaintBackground(PaintEventArgs aPaintEventArgs)
    {
      // do nothing here since this event is now handled by OnPaint
    }

    //=============================================================================
    /// <summary>
    /// OnPaint event handler
    /// </summary>
    /// <param name="aPaintEventArgs">Event arguments</param>
    protected override void OnPaint(PaintEventArgs aPaintEventArgs)
    {
      base.OnPaint(aPaintEventArgs);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing ) {
        if( components != null ) components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
    }
    #endregion
  }
}

