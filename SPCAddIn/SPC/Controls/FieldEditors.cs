//=============================================================================
//D FieldEditors.cs
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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Editor factory
  /// </summary>
  internal class AdditionalFieldEditorCreator
  {
    //=========================================================================
    /// <summary>
    /// Private constructor (static class emulation for 1.0 .NET Framework)
    /// </summary>
    private AdditionalFieldEditorCreator()
    {
    }

    //=========================================================================
    /// <summary>
    /// Creates a editor instance depending on type of field being edited.
    /// </summary>
    /// <param name="a_lvi">ListViewItem linked with edited field</param>
    /// <param name="a_es">Export settings</param>
    /// <returns>Instance of editor</returns>
    internal static AdditionalFieldEditor create(ListViewItem a_lvi, ExportSettings a_es)
    {
      Debug.Assert(a_lvi != null);
      Debug.Assert(a_lvi.Tag != null);
      AdditionalField af = null;

      af = a_lvi.Tag as AdditionalField;
      switch (af.field_type) {
        case enumQDASKFieldType.KFT_A: {
            return new AdditionalFieldEditorText(a_lvi.ListView);
          }
        case enumQDASKFieldType.KFT_D: {
            return new AdditionalFieldEditorDateTime(a_lvi.ListView);            
          }
        case enumQDASKFieldType.KFT_I: {
            if (af.catalog_used) {
              // catalog 
              return new AdditionalFieldEditorCatalog(a_lvi.ListView, a_es);
            } else {
              return new AdditionalFieldEditorText(a_lvi.ListView);
            }
          }
        default: {
            Debug.Fail("Wrong enumeration value");
            return null;
          }
      }
    }
  }

  //===========================================================================
  /// <summary>
  /// Base class for all custom editors
  /// </summary>
  internal abstract class AdditionalFieldEditor
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_owner">Owner of custom control</param>
    internal AdditionalFieldEditor(ListView a_owner)
    {
      owner = a_owner;
    }

    //=========================================================================
    /// <summary>
    /// Create & install control
    /// </summary>
    internal void install()
    {
      if (owner.SelectedItems.Count != 1) return;
      Control c = create_control();      

      c.Tag = selected_item;

      setup_control_values(c);

      owner.Controls.Add(c);
      c.Bounds = get_control_bounds();
      c.Focus();
    }

    //=========================================================================
    /// <summary>
    /// Abstract method that creates control instance. Should be overridden.
    /// </summary>
    /// <returns></returns>
    protected abstract Control create_control();

    //=========================================================================
    /// <summary>
    /// Sets up control internals after it was has been created.
    /// </summary>
    /// <param name="c">Control to setup</param>
    protected abstract void setup_control_values(Control c);

    //=========================================================================
    /// <summary>
    /// Retreives control's bounds
    /// </summary>
    /// <returns></returns>
    protected Rectangle get_control_bounds()
    {
      return selected_item.SubItems[c_col_index].Bounds;
    }

    //=========================================================================
    /// <summary>
    /// Retrieves selected list view item
    /// </summary>
    protected ListViewItem selected_item
    {
      get {
        Debug.Assert(owner.SelectedItems.Count == 1);
        return owner.SelectedItems[0];
      }
    }

    /// <summary> Owner control </summary>
    protected readonly ListView owner;

    /// <summary> Column occupied by overlapped control </summary>
    protected const int c_col_index = 1;
  }

  //===========================================================================
  /// <summary>
  /// Text box field editor
  /// </summary>
  internal class AdditionalFieldEditorText : AdditionalFieldEditor
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_owner">Owner control</param>
    internal AdditionalFieldEditorText(ListView a_owner)
      : base(a_owner)
    {      
    }

    //=========================================================================
    /// <summary>
    /// Creates textbox control
    /// </summary>
    /// <returns></returns>
    protected override Control create_control()
    {
      TextBox tbox = new TextBox();
      tbox.LostFocus += new EventHandler(tb_LostFocus);
      tbox.KeyDown += new KeyEventHandler(tb_KeyDown);
      tbox.ContextMenuStrip = create_context_menu_with_macro_support(tbox);
      tbox.ContextMenuStrip.Tag = tbox;
      return tbox;
    }

    //==========================================================================
    /// <summary>
    /// Creates context menu that replaces the standard editbox's context menu.
    /// This menu has "Insert Macro" command in addition to traditional 
    /// Cut, Copy, etc.
    /// </summary>
    /// <param name="tbox">The text box to create the context menu for</param>
    private ContextMenuStrip create_context_menu_with_macro_support(TextBox tbox)
    {
      ContextMenuStrip menu = new ContextMenuStrip();      
      // Undo
      ToolStripItem undo = new ToolStripMenuItem(
        LS.Lc("Undo"),
        null, 
        new EventHandler((object o, EventArgs ev) => {
          tbox.Undo(); 
        })
      );
      undo.Name = "Undo";

      // Cut
      ToolStripItem cut = new ToolStripMenuItem(
        LS.Lc("Cut"),
        null,
        new EventHandler((object o, EventArgs ev) => {
          tbox.Cut();
        })
      );
      cut.Name = "Cut";

      // Copy
      ToolStripItem copy = new ToolStripMenuItem(
        LS.Lc("Copy"),
        null,
        new EventHandler((object o, EventArgs ev) => {
          tbox.Copy();
        })
      );
      copy.Name = "Copy";

      // Paste
      ToolStripItem paste = new ToolStripMenuItem(
        LS.Lc("Paste"),
        null,
        new EventHandler((object o, EventArgs ev) => {
          tbox.Paste();
        })
      );
      paste.Name = "Paste";

      // Insert Macro
      ToolStripMenuItem insert_macro = new ToolStripMenuItem(
        LS.Lc("Insert Macro")
      );
      insert_macro.Name = "Insert Macro";

      MacroManager.available_macros.ForEach((MacroItem mi) => {
        insert_macro.DropDownItems.Add(
          mi.title,
          null,
          new EventHandler((object o, EventArgs ev) => {
            tbox.Paste(mi.escaped_macro);
          })
        );
      });

      menu.Items.AddRange(
        new ToolStripItem[] {
          undo,
          new ToolStripSeparator(),
          cut,
          copy,
          paste,
          new ToolStripSeparator(),
          insert_macro
        }
      );

      menu.Opening += textbox_menu_Opening;
      return menu;
    }

    //==========================================================================
    /// <summary>
    /// Custom context menu Opening event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void textbox_menu_Opening(
      object sender, 
      System.ComponentModel.CancelEventArgs e
    )
    //
    {
      ContextMenuStrip menu = sender as ContextMenuStrip;
      Debug.Assert(menu != null);

      TextBox text_box = menu.Tag as TextBox;
      Debug.Assert(text_box != null);

      menu.Items["Undo"].Enabled = text_box.CanUndo;
      menu.Items["Cut"].Enabled = text_box.SelectedText.Length > 0;
      menu.Items["Copy"].Enabled = text_box.SelectedText.Length > 0;
      menu.Items["Paste"].Enabled = Clipboard.ContainsText();
    }

    //=========================================================================
    /// <summary>
    /// KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void tb_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode) {
        case Keys.Return: {
          TextBox tb = sender as TextBox;

          ListViewItem lvi = tb.Tag as ListViewItem;
          Debug.Assert(null != lvi, "Tag should be an ListViewItem object");

          AdditionalField af = lvi.Tag as AdditionalField;
          Debug.Assert(af != null, "Tag should be an AdditionalFiend object");

          if (af.check_value_valid(tb.Text)) {
            // we have a valid value
            ListViewItem.ListViewSubItem lvsi = lvi.SubItems[c_col_index];
            Debug.Assert(null != lvsi);
            lvsi.Text = tb.Text;
            tb.Dispose();
          } else {
            m_lock_dispose = true;
            // something wrong with this value
            MessageBox.Show(
              LS.Lc("The value you have entered is incorrect"),
              LS.Lc("Attention"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation
            );
            tb.Focus();
            m_lock_dispose = false;
          }
          e.Handled = true;
          break;
        }
        case Keys.Escape: {
          e.Handled = true;
          (sender as Control).Dispose();
          break;
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// LostFocus event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void tb_LostFocus(object sender, EventArgs e)
    {
      if (!m_lock_dispose) {
        TextBox tb = sender as TextBox;
        Debug.Assert(tb != null, "Expecting TextBox control");
        ListViewItem lvi = tb.Tag as ListViewItem;
        Debug.Assert(null != lvi, "Tag should be an ListViewItem object");
        AdditionalField af = lvi.Tag as AdditionalField;
        Debug.Assert(af != null, "Tag should be an AdditionalFiend object");
        if (af.check_value_valid(tb.Text)) {
          ListViewItem.ListViewSubItem lvsi = lvi.SubItems[c_col_index];
          Debug.Assert(null != lvsi);
          lvsi.Text = tb.Text;
        }
        (sender as Control).Dispose();
      }
    }    

    //=========================================================================
    /// <summary>
    /// Setup control
    /// </summary>
    /// <param name="c">Control to setup</param>
    protected override void setup_control_values(Control c)
    {
      TextBox tb = c as TextBox;
      Debug.Assert(null != tb);
      AdditionalField af = selected_item.Tag as AdditionalField;
      Debug.Assert(null != af);
      tb.MaxLength = af.max_length;
      tb.Text = selected_item.SubItems[c_col_index].Text;
    }

    /// <summary> Control dispose </summary>
    private bool m_lock_dispose = false;

  }

  //===========================================================================
  /// <summary>
  /// Catalog-connected combobox control
  /// </summary>
  internal class AdditionalFieldEditorCatalog : AdditionalFieldEditor
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_owner">Owner control</param>
    /// <param name="a_es">Export settings</param>
    internal AdditionalFieldEditorCatalog(ListView a_owner, ExportSettings a_es)
      : base(a_owner)
    {
      Debug.Assert(a_es is ExportSettingsQDAS);
      export_settings = a_es as ExportSettingsQDAS;
    }

    //=========================================================================
    /// <summary>
    /// Create combobox control
    /// </summary>
    /// <returns>The created control</returns>
    protected override Control create_control()
    {
      ComboBox cb = new ComboBox();
      cb.SelectionChangeCommitted += new EventHandler(cb_SelectionChangeCommitted);
      cb.LostFocus += new EventHandler(cb_LostFocus);
      return cb;
    }

    //=========================================================================
    /// <summary>
    /// SelectionChangeCommitted event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    void cb_SelectionChangeCommitted(object sender, EventArgs e)
    {
      ComboBox cb = (sender as ComboBox);

      ListViewItem lvi = (cb.Tag as ListViewItem);
      Debug.Assert(null != lvi, "Tag should be an ListViewItem object");
      
      AdditionalField af = lvi.Tag as AdditionalField;
      Debug.Assert(af != null);

      int index = cb.SelectedIndex + 1;

      if (af.check_value_valid(index.ToString())) {
        af.combo_index = index;
        // we have a valid value
        ListViewItem.ListViewSubItem lvsi = lvi.SubItems[c_col_index];
        Debug.Assert(null != lvsi);
        lvsi.Text = cb.SelectedItem.ToString();
        cb.Dispose();
      } else {
        m_lock_dispose = true;
        // something wrong with this value
        MessageBox.Show(
          LS.Lc("The value you have entered is incorrect"),
          LS.Lc("Attention"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
        cb.Focus();
        m_lock_dispose = false;
      }            
    }    

    //=========================================================================
    /// <summary>
    /// LostFocus event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    void cb_LostFocus(object sender, EventArgs e)
    {
      if (!m_lock_dispose) (sender as Control).Dispose();
    }

    //=========================================================================
    /// <summary>
    /// Setup control
    /// </summary>
    /// <param name="c">Control to setup</param>
    protected override void setup_control_values(Control c)
    {
      AdditionalField af = selected_item.Tag as AdditionalField;
      ComboBox cb = c as ComboBox;
      Debug.Assert(null != cb);
      cb.DropDownStyle = ComboBoxStyle.DropDownList;
      cb.Items.Clear();
      Debug.Assert(af.catalog_used);

      Catalog cat = export_settings.catalog_manager.get_catalog(af.linked_catalog_type);

      foreach(CatalogItem ci in cat.items) {
        cb.Items.Add(ci);
      }

      if (
        (af.combo_index >= 1) && 
        (af.combo_index <= cb.Items.Count)
      ) {
        cb.SelectedIndex = af.combo_index - 1;
      }
    }

    /// <summary> Export settings used by control </summary>
    private readonly ExportSettingsQDAS export_settings;

    /// <summary> Dispose lock flag </summary>
    private bool m_lock_dispose = false;
  }

  //===========================================================================
  /// <summary>
  /// Date & Time edit control
  /// </summary>
  internal class AdditionalFieldEditorDateTime : AdditionalFieldEditor
  {
    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_list_view">owner control</param>
    internal AdditionalFieldEditorDateTime(ListView a_list_view)
      : base(a_list_view)
    {
    }

    //=========================================================================
    /// <summary>
    /// Create date&time control instance
    /// </summary>
    /// <returns></returns>
    protected override Control create_control()
    {
      DateTimePicker dtp = new DateTimePicker();
      dtp.DropDown += new EventHandler(dtp_DropDown);
      dtp.LostFocus += new EventHandler(dtp_LostFocus);
      dtp.CloseUp += new EventHandler(dtp_CloseUp);
      dtp.KeyDown += new KeyEventHandler(dtp_KeyDown);
      return dtp;
    }

    //=========================================================================
    /// <summary>
    /// CloseUp event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    void dtp_CloseUp(object sender, EventArgs e)
    {
      m_dispose_locked = false;
    }

    //=========================================================================
    /// <summary>
    /// DropDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    void dtp_DropDown(object sender, EventArgs e)
    {
      m_dispose_locked = true;
    }

    //=========================================================================
    /// <summary>
    /// KeyDown event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    void dtp_KeyDown(object sender, KeyEventArgs e)
    {
      DateTimePicker dtp = sender as DateTimePicker;
      switch (e.KeyCode) {
        case Keys.Return: {
          m_dispose_locked = false;          
          Debug.Assert(null != dtp);
          ListViewItem lvi = (dtp.Tag as ListViewItem);
          Debug.Assert(null != lvi, "Tag should be an ListViewItem object");
          AdditionalField af = lvi.Tag as AdditionalField;
          Debug.Assert(af != null, "Tag should be an AdditionalFiend object");


          // we have a valid value
          ListViewItem.ListViewSubItem lvsi = lvi.SubItems[c_col_index];
          Debug.Assert(null != lvsi);
          af.datetime = dtp.Value;
          lvsi.Text = dtp.Value.ToString(c_datetime_picker_format);
          dtp.Dispose();
          e.Handled = true;
          break;
        }
        case Keys.Escape: {
          dtp.Dispose();
          e.Handled = true;
          break;
        }
      }      
    }

    //=========================================================================
    /// <summary>
    /// LostFocus event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    void dtp_LostFocus(object sender, EventArgs e)
    {
      if (!m_dispose_locked) { 
        (sender as Control).Dispose();
      }
    }

    //=========================================================================
    /// <summary>
    /// Setup control
    /// </summary>
    /// <param name="c">Control to setup</param>
    protected override void setup_control_values(Control c)
    {
      AdditionalField af = selected_item.Tag as AdditionalField;
      DateTimePicker dtp = c as DateTimePicker;
      dtp.CustomFormat = c_datetime_picker_format;
      dtp.Format = DateTimePickerFormat.Custom;
      if (af.field_value == String.Empty) {
        dtp.Value = DateTime.Now;
      } else {
        try {
          dtp.Value = af.datetime;
        } catch (Exception) {
        }
      }
    }

    /// <summary> Date&time store format (used when saving to DFX) </summary>
    private string c_datetime_picker_format = "dd MMMM yyyy - HH:mm";
    /// <summary> Dispose lock flag </summary>
    private bool m_dispose_locked = false;
  }
}

