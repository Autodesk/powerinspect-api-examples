//=============================================================================
//
// FormSPCAddIn class interface & implementation
// Main form of SPC add-in
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
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Autodesk.PowerInspect.AddIns.SPC.Controls;

// Aliases
using pi = PowerINSPECT;
using System.Reflection;
using System.Globalization;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //===========================================================================
  /// <summary>
  /// Summary description for FormSPCAddIn.
  /// </summary>
  internal class FormSPCAddIn : System.Windows.Forms.Form
  {

    #region Constants
    
    // Image indexes for ImageListTreeIcons
    const int IMG_IDX_ICON_GROUP_GEOMETRIC = 0;
    const int IMG_IDX_ICON_GROUP_SURFACE = 1;
    const int IMG_IDX_ICON_SUBGROUP = 2;
    const int IMG_IDX_ICON_FEATURE = 3;

    #endregion

    #region Typedefs

    //=============================================================================
    /// <summary>
    /// States of tri-state check-box
    /// </summary>
    enum TriState {Unchecked=0, PartiallyChecked=1, Checked=2};

    #endregion

    #region Fields

    #region Visual components

    private System.Windows.Forms.Button buttonExport;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.ImageList imageListTreeIcons;
    private System.Windows.Forms.GroupBox groupBoxElements;
    private System.Windows.Forms.Button buttonClearAllElem;
    private System.Windows.Forms.Button buttonSelAllElem;
    private System.Windows.Forms.TreeView treeViewElements;
    private System.Windows.Forms.Panel panelElemHolder;
    private System.Windows.Forms.ColumnHeader columnHeaderMeasureName;
    private System.Windows.Forms.ListView listViewMeasures;
    private System.Windows.Forms.Button buttonClearAllMeasures;
    private System.Windows.Forms.Button buttonSelectAllMeasures;
    private System.Windows.Forms.Button buttonSelectAllReported;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.ImageList imageListlistViewIcons;
    private System.Windows.Forms.ImageList imageListCheckBoxes;
    private System.ComponentModel.IContainer components;

    #endregion
    /// <summary> State keeper reference </summary>
    private StateKeeper m_stateKeeper = new StateKeeper();

    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ImageList imageListButtons;
    private System.Windows.Forms.ContextMenu contextMenuTreeView;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem treeViewPopup_Expand;
    private System.Windows.Forms.MenuItem treeViewPopup_Collapse;
    private System.Windows.Forms.MenuItem treeViewPopup_ExpandAll;
    private Button buttonSelectLatest;
    private Button buttonSelectUnexported;
    private Button buttonExportSettings;

    /// <summary> Image list used by tree control </summary>
    private ImageList m_completeTreeImageList = new ImageList();

    /// <summary> Settings dictionary </summary>
    Hashtable m_export_settings = new Hashtable();

    #endregion

    #region Methods

    #region Constructor & Destructor

    //=============================================================================
    /// <summary>
    /// Form constructor
    /// </summary>
    /// <param name="aSPCAddin">A reference to SPCAddin object</param>
    internal FormSPCAddIn()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      //
      // TODO: Add any constructor code after InitializeComponent call
      //
      localize();
    }

    #endregion

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing ) {
        if(components != null) {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSPCAddIn));
      this.imageListTreeIcons = new System.Windows.Forms.ImageList(this.components);
      this.buttonExport = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.groupBoxElements = new System.Windows.Forms.GroupBox();
      this.buttonSelectUnexported = new System.Windows.Forms.Button();
      this.buttonSelectLatest = new System.Windows.Forms.Button();
      this.buttonClearAllMeasures = new System.Windows.Forms.Button();
      this.buttonSelectAllReported = new System.Windows.Forms.Button();
      this.panelElemHolder = new System.Windows.Forms.Panel();
      this.listViewMeasures = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.imageListlistViewIcons = new System.Windows.Forms.ImageList(this.components);
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.treeViewElements = new System.Windows.Forms.TreeView();
      this.contextMenuTreeView = new System.Windows.Forms.ContextMenu();
      this.treeViewPopup_Expand = new System.Windows.Forms.MenuItem();
      this.treeViewPopup_Collapse = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.treeViewPopup_ExpandAll = new System.Windows.Forms.MenuItem();
      this.buttonClearAllElem = new System.Windows.Forms.Button();
      this.buttonSelAllElem = new System.Windows.Forms.Button();
      this.buttonSelectAllMeasures = new System.Windows.Forms.Button();
      this.columnHeaderMeasureName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.imageListCheckBoxes = new System.Windows.Forms.ImageList(this.components);
      this.imageListButtons = new System.Windows.Forms.ImageList(this.components);
      this.buttonExportSettings = new System.Windows.Forms.Button();
      this.groupBoxElements.SuspendLayout();
      this.panelElemHolder.SuspendLayout();
      this.SuspendLayout();
      // 
      // imageListTreeIcons
      // 
      this.imageListTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeIcons.ImageStream")));
      this.imageListTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListTreeIcons.Images.SetKeyName(0, "TreeIconGroupGeometric.png");
      this.imageListTreeIcons.Images.SetKeyName(1, "TreeIconSurfacePoint.png");
      this.imageListTreeIcons.Images.SetKeyName(2, "green_dot.png");
      this.imageListTreeIcons.Images.SetKeyName(3, "blue_group.png");
      // 
      // buttonExport
      // 
      this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonExport.Location = new System.Drawing.Point(394, 418);
      this.buttonExport.Name = "buttonExport";
      this.buttonExport.Size = new System.Drawing.Size(140, 46);
      this.buttonExport.TabIndex = 0;
      this.buttonExport.Text = "Export...";
      this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonClose.Location = new System.Drawing.Point(540, 418);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(140, 46);
      this.buttonClose.TabIndex = 1;
      this.buttonClose.Text = "Close";
      // 
      // groupBoxElements
      // 
      this.groupBoxElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxElements.Controls.Add(this.buttonSelectUnexported);
      this.groupBoxElements.Controls.Add(this.buttonSelectLatest);
      this.groupBoxElements.Controls.Add(this.buttonClearAllMeasures);
      this.groupBoxElements.Controls.Add(this.buttonSelectAllReported);
      this.groupBoxElements.Controls.Add(this.panelElemHolder);
      this.groupBoxElements.Controls.Add(this.buttonClearAllElem);
      this.groupBoxElements.Controls.Add(this.buttonSelAllElem);
      this.groupBoxElements.Controls.Add(this.buttonSelectAllMeasures);
      this.groupBoxElements.Location = new System.Drawing.Point(4, 0);
      this.groupBoxElements.Name = "groupBoxElements";
      this.groupBoxElements.Size = new System.Drawing.Size(684, 412);
      this.groupBoxElements.TabIndex = 13;
      this.groupBoxElements.TabStop = false;
      this.groupBoxElements.Text = "Elements to export";
      // 
      // buttonSelectUnexported
      // 
      this.buttonSelectUnexported.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSelectUnexported.Location = new System.Drawing.Point(536, 351);
      this.buttonSelectUnexported.Name = "buttonSelectUnexported";
      this.buttonSelectUnexported.Size = new System.Drawing.Size(140, 23);
      this.buttonSelectUnexported.TabIndex = 7;
      this.buttonSelectUnexported.Text = "Select unexported";
      this.buttonSelectUnexported.UseVisualStyleBackColor = true;
      this.buttonSelectUnexported.Click += new System.EventHandler(this.buttonSelectUnexported_Click);
      // 
      // buttonSelectLatest
      // 
      this.buttonSelectLatest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSelectLatest.Location = new System.Drawing.Point(390, 351);
      this.buttonSelectLatest.Name = "buttonSelectLatest";
      this.buttonSelectLatest.Size = new System.Drawing.Size(140, 23);
      this.buttonSelectLatest.TabIndex = 6;
      this.buttonSelectLatest.Text = "Select latest";
      this.buttonSelectLatest.UseVisualStyleBackColor = true;
      this.buttonSelectLatest.Click += new System.EventHandler(this.buttonSelectLatest_Click);
      // 
      // buttonClearAllMeasures
      // 
      this.buttonClearAllMeasures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonClearAllMeasures.Location = new System.Drawing.Point(536, 380);
      this.buttonClearAllMeasures.Name = "buttonClearAllMeasures";
      this.buttonClearAllMeasures.Size = new System.Drawing.Size(140, 23);
      this.buttonClearAllMeasures.TabIndex = 9;
      this.buttonClearAllMeasures.Text = "Clear all";
      this.buttonClearAllMeasures.Click += new System.EventHandler(this.buttonClearAllMeasures_Click);
      // 
      // buttonSelectAllReported
      // 
      this.buttonSelectAllReported.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonSelectAllReported.Location = new System.Drawing.Point(8, 351);
      this.buttonSelectAllReported.Name = "buttonSelectAllReported";
      this.buttonSelectAllReported.Size = new System.Drawing.Size(198, 23);
      this.buttonSelectAllReported.TabIndex = 3;
      this.buttonSelectAllReported.Text = "Select all reported";
      this.buttonSelectAllReported.Click += new System.EventHandler(this.buttonSelectAllReported_Click);
      // 
      // panelElemHolder
      // 
      this.panelElemHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panelElemHolder.Controls.Add(this.listViewMeasures);
      this.panelElemHolder.Controls.Add(this.splitter1);
      this.panelElemHolder.Controls.Add(this.treeViewElements);
      this.panelElemHolder.Location = new System.Drawing.Point(8, 16);
      this.panelElemHolder.Name = "panelElemHolder";
      this.panelElemHolder.Size = new System.Drawing.Size(668, 329);
      this.panelElemHolder.TabIndex = 12;
      // 
      // listViewMeasures
      // 
      this.listViewMeasures.CheckBoxes = true;
      this.listViewMeasures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.listViewMeasures.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listViewMeasures.GridLines = true;
      this.listViewMeasures.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listViewMeasures.Location = new System.Drawing.Point(379, 0);
      this.listViewMeasures.Name = "listViewMeasures";
      this.listViewMeasures.Size = new System.Drawing.Size(289, 329);
      this.listViewMeasures.SmallImageList = this.imageListlistViewIcons;
      this.listViewMeasures.TabIndex = 11;
      this.listViewMeasures.UseCompatibleStateImageBehavior = false;
      this.listViewMeasures.View = System.Windows.Forms.View.Details;
      this.listViewMeasures.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewMeasures_ItemCheck);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Measure";
      this.columnHeader1.Width = 145;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Extra information";
      this.columnHeader2.Width = 120;
      // 
      // imageListlistViewIcons
      // 
      this.imageListlistViewIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListlistViewIcons.ImageStream")));
      this.imageListlistViewIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListlistViewIcons.Images.SetKeyName(0, "listBoxIconJustMeasured.png");
      this.imageListlistViewIcons.Images.SetKeyName(1, "listBoxIconSaved.png");
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(376, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 329);
      this.splitter1.TabIndex = 7;
      this.splitter1.TabStop = false;
      // 
      // treeViewElements
      // 
      this.treeViewElements.ContextMenu = this.contextMenuTreeView;
      this.treeViewElements.Dock = System.Windows.Forms.DockStyle.Left;
      this.treeViewElements.Location = new System.Drawing.Point(0, 0);
      this.treeViewElements.Name = "treeViewElements";
      this.treeViewElements.Size = new System.Drawing.Size(376, 329);
      this.treeViewElements.TabIndex = 10;
      this.treeViewElements.Click += new System.EventHandler(this.treeViewElements_Click);
      // 
      // contextMenuTreeView
      // 
      this.contextMenuTreeView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.treeViewPopup_Expand,
            this.treeViewPopup_Collapse,
            this.menuItem3,
            this.treeViewPopup_ExpandAll});
      // 
      // treeViewPopup_Expand
      // 
      this.treeViewPopup_Expand.Index = 0;
      this.treeViewPopup_Expand.Text = "Expand";
      this.treeViewPopup_Expand.Click += new System.EventHandler(this.treeViewPopup_Expand_Click);
      // 
      // treeViewPopup_Collapse
      // 
      this.treeViewPopup_Collapse.Index = 1;
      this.treeViewPopup_Collapse.Text = "Collapse";
      this.treeViewPopup_Collapse.Click += new System.EventHandler(this.treeViewPopup_Collapse_Click);
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 2;
      this.menuItem3.Text = "-";
      // 
      // treeViewPopup_ExpandAll
      // 
      this.treeViewPopup_ExpandAll.Index = 3;
      this.treeViewPopup_ExpandAll.Text = "Expand all";
      this.treeViewPopup_ExpandAll.Click += new System.EventHandler(this.treeViewPopup_ExpandAll_Click);
      // 
      // buttonClearAllElem
      // 
      this.buttonClearAllElem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonClearAllElem.Location = new System.Drawing.Point(110, 380);
      this.buttonClearAllElem.Name = "buttonClearAllElem";
      this.buttonClearAllElem.Size = new System.Drawing.Size(96, 23);
      this.buttonClearAllElem.TabIndex = 5;
      this.buttonClearAllElem.Text = "Clear all";
      this.buttonClearAllElem.Click += new System.EventHandler(this.buttonClearAllElem_Click);
      // 
      // buttonSelAllElem
      // 
      this.buttonSelAllElem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonSelAllElem.Location = new System.Drawing.Point(8, 380);
      this.buttonSelAllElem.Name = "buttonSelAllElem";
      this.buttonSelAllElem.Size = new System.Drawing.Size(96, 23);
      this.buttonSelAllElem.TabIndex = 4;
      this.buttonSelAllElem.Text = "Select all";
      this.buttonSelAllElem.Click += new System.EventHandler(this.buttonSelAllElem_Click);
      // 
      // buttonSelectAllMeasures
      // 
      this.buttonSelectAllMeasures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSelectAllMeasures.Location = new System.Drawing.Point(390, 380);
      this.buttonSelectAllMeasures.Name = "buttonSelectAllMeasures";
      this.buttonSelectAllMeasures.Size = new System.Drawing.Size(140, 23);
      this.buttonSelectAllMeasures.TabIndex = 8;
      this.buttonSelectAllMeasures.Text = "Select all";
      this.buttonSelectAllMeasures.Click += new System.EventHandler(this.buttonSelectAllMeasures_Click);
      // 
      // columnHeaderMeasureName
      // 
      this.columnHeaderMeasureName.Text = "Measure part name";
      this.columnHeaderMeasureName.Width = 160;
      // 
      // imageListCheckBoxes
      // 
      this.imageListCheckBoxes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCheckBoxes.ImageStream")));
      this.imageListCheckBoxes.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListCheckBoxes.Images.SetKeyName(0, "UNCHECKED.png");
      this.imageListCheckBoxes.Images.SetKeyName(1, "CHECKED_DISABLED.png");
      this.imageListCheckBoxes.Images.SetKeyName(2, "CHECKED.png");
      // 
      // imageListButtons
      // 
      this.imageListButtons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageListButtons.ImageSize = new System.Drawing.Size(16, 16);
      this.imageListButtons.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // buttonExportSettings
      // 
      this.buttonExportSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonExportSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonExportSettings.Location = new System.Drawing.Point(12, 418);
      this.buttonExportSettings.Name = "buttonExportSettings";
      this.buttonExportSettings.Size = new System.Drawing.Size(198, 46);
      this.buttonExportSettings.TabIndex = 2;
      this.buttonExportSettings.Text = "Settings...";
      this.buttonExportSettings.UseVisualStyleBackColor = true;
      this.buttonExportSettings.Click += new System.EventHandler(this.buttonExportSettings_Click);
      // 
      // FormSPCAddIn
      // 
      this.AcceptButton = this.buttonExport;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.buttonClose;
      this.ClientSize = new System.Drawing.Size(692, 473);
      this.Controls.Add(this.buttonExportSettings);
      this.Controls.Add(this.groupBoxElements);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.buttonExport);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(700, 500);
      this.Name = "FormSPCAddIn";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SPC Export Add-In";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSPCAddIn_FormClosing);
      this.Load += new System.EventHandler(this.FormSPCAddIn_Load);
      this.groupBoxElements.ResumeLayout(false);
      this.panelElemHolder.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    #region Localization

    //=============================================================================
    /// <summary>
    /// Localization of all text-based messages.
    /// /// </summary>
    private void localize()
    {

      this.buttonExport.Text = LS.Lc("Export...");
      this.buttonClose.Text = LS.Lc("Close");
      this.groupBoxElements.Text = LS.Lc("Elements to export");
      this.buttonSelectUnexported.Text = LS.Lc("Select unexported");
      this.buttonSelectLatest.Text = LS.Lc("Select latest");
      this.buttonClearAllMeasures.Text = LS.Lc("Clear all");
      this.buttonSelectAllReported.Text = LS.Lc("Select all reported");
      this.columnHeader1.Text = LS.Lc("Measure");
      this.columnHeader2.Text = LS.Lc("Extra information");
      this.treeViewPopup_Expand.Text = LS.Lc("Expand");
      this.treeViewPopup_Collapse.Text = LS.Lc("Collapse");
      this.menuItem3.Text = LS.Lc("-");
      this.treeViewPopup_ExpandAll.Text = LS.Lc("Expand all");
      this.buttonClearAllElem.Text = LS.Lc("Clear all");
      this.buttonSelAllElem.Text = LS.Lc("Select all");
      this.buttonSelectAllMeasures.Text = LS.Lc("Select all");
      this.columnHeaderMeasureName.Text = LS.Lc("Measure part name");
      this.buttonExportSettings.Text = LS.Lc("Settings...");
      this.Text = String.Format(
        LS.Lc("SPC Export Add-In [Base measure - \"{0}\"]"),
        Tools.get_base_measure(PIConnector.instance.active_document).Name
      );
    }

    #endregion


    private delegate bool node_filter_callback(TreeNode a_tn);

    //=============================================================================
    /// <summary>
    /// Recursively sets node's state to aChecked
    /// </summary>
    /// <param name="aTreeNode">A tree node to work with</param>
    /// <param name="aChecked">Desired state of check-box</param>
    /// <param name="a_nfc">callback filter function</param>
    /// <returns>number of items that were filtered out</returns>
    private int set_nodes_check_recurse(
      TreeNode aTreeNode,
      TriState aChecked,      
      node_filter_callback a_nfc
    )
    {
      int items_filtered = 0;
      // Validation checks
      Debug.Assert(TriState.PartiallyChecked!=aChecked, "SetNodesCheckRecurse cannot work with partially checked nodes");
      // Set State

      // Check what nodes should be set up
      if (null != a_nfc) {
        if (a_nfc(aTreeNode)) {
          set_node_state(aTreeNode, aChecked);
        } else {
          items_filtered++;
        }
      } else {
        set_node_state(aTreeNode, aChecked);
      }

      // Check if we need to make a recursion
      if (aTreeNode.Nodes.Count > 0) {
        // Go through all sub-nodes
        foreach (TreeNode node in aTreeNode.Nodes) {
          items_filtered += set_nodes_check_recurse(node, aChecked, a_nfc);
        }
      }
      return items_filtered;
    }

    //=============================================================================
    /// <summary>
    /// Sets all nodes with name aNodeName to aChecked state, starting from 
    /// aTreeNode node.
    /// </summary>
    /// <param name="aTreeNode">A tree node to start from</param>
    /// <param name="aChecked">A state to set nodes with name aNodeName</param>    
    /// <param name="aNodeName">A nodes to be set</param>
    /// <param name="a_nfc">Callback filter function</param>
    /// <returns></returns>
    private int set_nodes_check_recurse(
      TreeNode aTreeNode,
      TriState aChecked,
      string aNodeName,
      node_filter_callback a_nfc
    )
    {
      int items_filtered = 0;
      // Validation checks
      Debug.Assert(TriState.PartiallyChecked!=aChecked, "SetNodesCheckRecurse cannot work with partially checked nodes");
      if (aTreeNode.Text == aNodeName) {
        // Set State
        if (null != a_nfc) {
          if (a_nfc(aTreeNode)) {
            set_node_state(aTreeNode, aChecked);
          }
        } else {
          set_node_state(aTreeNode, aChecked);
        }        
      }
      // Check if we need to make a recursion
      if (aTreeNode.Nodes.Count > 0) {
        // Go through all sub-nodes
        foreach (TreeNode node in aTreeNode.Nodes) {
          items_filtered += set_nodes_check_recurse(node, aChecked, aNodeName, a_nfc);
        }
      }
      return items_filtered;
    }

    //=============================================================================
    /// <summary>
    /// Builds complex image list that holds concatenations of check-boxes and 
    /// images
    /// </summary>
    private void build_complete_tree_image_list()
    {
      // Destination image width and height          
      int width = 32;
      int height = 16;
      // Create complete image list object      
      m_completeTreeImageList.ImageSize = new Size(width,height);
      // Fill it from both ImageListTreeIcons & ImageListCheckBoxes
      foreach (Image imgIcon in imageListTreeIcons.Images) {
        foreach (Image imgCheckBox in imageListCheckBoxes.Images) {          
          // Create destination image
          Image imgCBIcon = new Bitmap(width,height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
          Graphics gr = Graphics.FromImage(imgCBIcon);
          gr.DrawImage(imgCheckBox,0,0);
          gr.DrawImage(imgIcon,imgCheckBox.Width,0);
          gr.Dispose();
          m_completeTreeImageList.Images.Add(imgCBIcon);
        } // foreach
      } //foreach
    }

    //=============================================================================
    /// <summary>
    /// Gets tri-state of a node
    /// </summary>
    /// <param name="aNode">A node to get state of</param>
    /// <returns>Tri-state of a node</returns>
    private TriState get_node_state(TreeNode aNode)
    {
      // Get modulo of node's imageindex/3      
      return (TriState)(aNode.ImageIndex % 3);
    }

    //=============================================================================
    /// <summary>
    /// Sets the tri-state of a node
    /// </summary>
    /// <param name="aNode">A node</param>
    /// <param name="aState">A state to set</param>
    /// <param name="aImgIndex">An image index</param>
    private void set_node_state(TreeNode aNode, TriState aState, int aImgIndex)
    {
      aNode.ImageIndex = aImgIndex * 3 + (int)aState;
      aNode.SelectedImageIndex = aNode.ImageIndex;
    }

    //=============================================================================
    /// <summary>
    /// Sets the tri-state of a node
    /// </summary>
    /// <param name="aNode">A node</param>
    /// <param name="aState">A State to set</param>
    private void set_node_state(TreeNode aNode, TriState aState)
    {
      // Get an integer part of division. This will be
      // real image index in InageListIconCollection
      int newImageIndex = aNode.ImageIndex / 3;
      set_node_state(aNode,aState,newImageIndex);
    }

    //=============================================================================
    /// <summary>
    /// Watches for ticks hierarchy and they consistency
    /// </summary>
    /// <param name="aNode">A node to watch</param>
    /// <returns>Returns tri-state of the node aNode</returns>
    private TriState watch_checks_integrity(TreeNode aNode)
    {
      if (aNode.Nodes.Count>0) {
        int checkedCnt = 0;
        int partialCnt = 0;
        int uncheckedCnt = 0;
        foreach (TreeNode tn in aNode.Nodes) {
          switch(watch_checks_integrity(tn)) {
            case TriState.Checked:
              checkedCnt++;
               break;
            case TriState.PartiallyChecked:
              partialCnt++;
              break;
            case TriState.Unchecked:
              uncheckedCnt++;
              break;
          }//switch
        }//foreach

        // make a decision about state
        int totalCnt = checkedCnt + partialCnt + uncheckedCnt;

        if (totalCnt == checkedCnt) {
          // all sub elements are checked, so we are checked to
          set_node_state(aNode,TriState.Checked);
        } else if (totalCnt == uncheckedCnt) {
          // all sub elements are unchecked, so we are 
          set_node_state(aNode,TriState.Unchecked);
        } else {
          set_node_state(aNode,TriState.PartiallyChecked);
        }
      }//if
      return get_node_state(aNode);
    }


    //=========================================================================
    /// <summary>
    /// Builds measure list in control
    /// </summary>
    private void build_measure_list()
    {

      pi.IPIDocument doc = PIConnector.instance.active_document;

      Debug.Assert(doc!=null, "FormSPCAddIn.BuildMeasureList() : PIApp.ActiveDocument is null");

      listViewMeasures.BeginUpdate();

      listViewMeasures.Items.Clear();

      foreach (pi.IMeasure m in (doc.Measures as pi.IMeasures)) {
        if (!m.IsSimulated) {
          ListViewItem li = new ListViewItem(m.Name);
          li.Tag = m;
          li.ImageIndex = 0;
          li.Checked = false;
          listViewMeasures.Items.Add(li);
        }
      } // end foreach

      listViewMeasures.EndUpdate();

    }

    //=========================================================================
    /// <summary>
    /// Builds tree of sequence items
    /// </summary>
    private void build_tree()
    {
      // Assign image list of tree view 
      treeViewElements.ImageList = m_completeTreeImageList;

      // Get PowerINSPECT document reference
      pi.IPIDocument doc = PIConnector.instance.active_document;
      Debug.Assert(doc!=null, "FormSPCAddIn.BuildTree() : PIApp.ActiveDocument is null");

      // Tell tree view that we want to update it
      treeViewElements.BeginUpdate();
      treeViewElements.Nodes.Clear();

      // go through level of groups
      foreach (pi.ISequenceItem si in doc.SequenceItems)  {
        build_tree_nested_groups(si, treeViewElements.Nodes);
      }

      // clear a reference to the active document
      doc = null;
      // end update
      treeViewElements.EndUpdate();
    }

    //=========================================================================
    /// <summary>
    /// Builds a TreeNode to represent the tolerance zone from the GD&T item.
    /// </summary>
    /// <param name="gdt_item">PI Interface onto the GD&T item.</param>
    /// <param name="xml_gdt">Parent &lt;gdt&gt; node from the output.</param>
    /// <param name="xml_zone">
    ///   Child of the &lt;result&gt; node from the output.
    /// </param>
    /// <remarks>
    ///   Only builds a node for the first feature in a True Position item.
    /// </remarks>
    /// <returns>
    ///   Returns a TreeNode with a Tag depending on the type of zone:
    ///     ReportItemGDTDimention for dimension_result
    ///     ReportItemGDTToleranceZone for composite/cylindrical/planar
    ///   Returns null otherwise.
    /// </returns>
    private TreeNode build_gdt_result_node(
      pi.IGDTItem gdt_item,
      MSXML2.IXMLDOMElement xml_gdt,
      MSXML2.IXMLDOMElement xml_zone
    )
    {
      var condition = xml_gdt.getAttribute("type_str") as string;

      // Linear Dimension is a special case
      if (xml_zone.nodeName == "dimension_result") {
        #region dimension result
        var nominal_node = xml_gdt.selectSingleNode("dimension_nominal") as MSXML2.IXMLDOMElement;

        double upper_tol = ReportItemGDT.ParseDouble(nominal_node.getAttribute("uppertolerance") as string);
        double lower_tol = ReportItemGDT.ParseDouble(nominal_node.getAttribute("lowertolerance") as string);
        double nominal = ReportItemGDT.ParseDouble(nominal_node.getAttribute("nominaldimension") as string);

        TreeNode dimension_node = new TreeNode();
        dimension_node.Tag = new ReportItemGDTDimention(
          gdt_item,
          condition,
          upper_tol,
          lower_tol,
          nominal
        );
        dimension_node.Text = condition;
        dimension_node.ForeColor = Color.Blue;
        set_node_state(dimension_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);

        return dimension_node;
        #endregion
      }

      // Determine the type of tolerance zone from the node name
      string xpath = "";

      switch (xml_zone.nodeName) {
        case "composite_tolerancezone": {
          // Only use the zone for the first feature
          xml_zone = xml_gdt.selectSingleNode("feature/result/position_tolerancezone") as MSXML2.IXMLDOMElement;
          Debug.Assert(xml_zone != null, "Composite zone must have a result for each feature");
          xpath = "feature/result/position_tolerancezone/@width";
          break;
        }
        case "cylindrical_tolerancezone": {
          xpath = "result/cylindrical_tolerancezone/@width";
          break;
        }
        case "planar_tolerancezone": {
          xpath = "result/planar_tolerance_zone/@width";
          break;
        }
      }

      if (xpath == "") {
        return null;
      }

      // Get the original tolerance in the frame
      double original_tol = ReportItemGDT.ParseDouble(xml_zone.getAttribute("tolerance") as string);

      // Get the optional allowed width
      double allowed_tol;

      var xml_allowed = xml_zone.selectSingleNode("allowed") as MSXML2.IXMLDOMElement;
      if (xml_allowed != null) {
        allowed_tol = ReportItemGDT.ParseDouble(xml_allowed.getAttribute("value") as string);
      } else {
        allowed_tol = original_tol;
      }

      TreeNode tol_zone_node = new TreeNode();
      tol_zone_node.Tag = new ReportItemGDTToleranceZone(
        gdt_item,
        condition,
        xpath,
        original_tol,
        0,
        allowed_tol
      );
      tol_zone_node.Text = condition;
      tol_zone_node.ForeColor = Color.Blue;
      set_node_state(tol_zone_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);

      return tol_zone_node;
    }

    //=========================================================================
    /// <summary>
    /// Builds tree of sequence items
    /// </summary>
    /// <seealso cref="GathererElementsTree::GatherTree"/>
    private void build_tree_nested_groups(pi.ISequenceItem group, TreeNodeCollection parent_collection)
    {
      // Get PowerINSPECT document reference
      pi.IPIDocument doc = PIConnector.instance.active_document;
      Debug.Assert(
        doc != null,
        "FormSPCAddIn.build_tree_nested_groups() : active_document is null"
      );

      if (group.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGeometricGroup)) {
        #region IGeometricGroup
        pi.IGeometricGroup gg = group as pi.IGeometricGroup;
        Debug.Assert(
          gg.SequenceItems != null,
          "FormSPCAddIn.build_tree_nested_groups() : gg.SequenceItems == null"
        );

        if (gg.SequenceItems.Count == 0) return;

        // Create geometric group node with bold font
        TreeNode group_node = new TreeNode(gg.Name);
        set_node_state(group_node, TriState.Unchecked, IMG_IDX_ICON_GROUP_GEOMETRIC);

        parent_collection.Add(group_node);

        // go through level of elements in group
        foreach (pi.SequenceItem si in gg.SequenceItems) {
          TreeNode item_node = null;

          // let's determine type of element (i.e. GeometricItem, GDTItem ... etc.)
          if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_ISurfaceGroup) ||
              si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGeometricGroup)) {
            build_tree_nested_groups(si, group_node.Nodes);
          } else if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGDTItem)) {
            #region IGDTItem
            pi.IGDTItem gdt = si as pi.IGDTItem;

            pi.IMeasure measure = doc.get_ActiveMeasure();

            // Get the full XML output for this item
            MSXML2.IXMLDOMElement xml_root = null;
            try {
              xml_root = ReportItemGDT.GetXMLOutput(gdt, measure);
            } catch (Exception) {
            }
            if (xml_root == null) continue;

            // Get the inner <gdt> node
            MSXML2.IXMLDOMElement xml_gdt = ReportItemGDT.GetGDTItemXML(xml_root, measure);
            if (xml_gdt == null) continue;

            // Get the calculated result tolerancezone node
            var xml_zone = xml_gdt.selectSingleNode("result/*") as MSXML2.IXMLDOMElement;
            if (xml_zone == null) continue;

            string item_name = xml_root.getAttribute("Name") as string;

            item_node = new TreeNode(item_name);
            set_node_state(item_node, TriState.Unchecked, IMG_IDX_ICON_SUBGROUP);
            group_node.Nodes.Add(item_node);

            string condition = xml_root.getAttribute("GDTCondition") as string;

            TreeNode result_node = new TreeNode();
            item_node.Nodes.Add(result_node);

            result_node.Tag = new ReportItemGDTResult(
              si as pi.IGDTItem,
              LS.Lc("IsAccepted"),
              "result/@statusid"
            );
            result_node.Text = LS.Lc("IsAccepted");
            result_node.ForeColor = Color.Blue;
            set_node_state(result_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);

            TreeNode zone_node = build_gdt_result_node(gdt, xml_gdt, xml_zone);
            if (zone_node != null) {
              item_node.Nodes.Add(zone_node);
            }
            #endregion
          } else if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGeometricItem)) {
            #region IGeometricItem
            pi.IGeometricItem gi = si as pi.IGeometricItem;

            item_node = new TreeNode(gi.Name);
            set_node_state(item_node, TriState.Unchecked, IMG_IDX_ICON_SUBGROUP);
            group_node.Nodes.Add(item_node);

            int prop_count = gi.Properties.Count;
            for (int prop_index = 1; prop_index <= prop_count; prop_index++) {
              pi.IProperty prop = gi.Properties[prop_index];

              TreeNode property_node = new TreeNode(prop.Name);
              set_node_state(property_node, TriState.Unchecked, IMG_IDX_ICON_SUBGROUP);
              item_node.Nodes.Add(property_node);

              switch (prop.PropertyType) {
                #region IPropertyPoint3D
                case pi.PWI_PropertyType.pwi_property_Point3D: {
                  pi.IPropertyPoint3D prop_p3d = prop as pi.PropertyPoint3D;
                  int coord_count = prop_p3d.CoordinateType.NumberOfValue;
                  for (int i = 0; i < coord_count; i++) {
                    TreeNode coord_node = new TreeNode(prop_p3d.CoordinateType.get_Prefix(i));
                    coord_node.Tag = new ReportItemProperty3D(gi, prop_index, i);
                    coord_node.ForeColor = Color.Blue;
                    set_node_state(coord_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);
                    property_node.Nodes.Add(coord_node);
                  }
                  break;
                }
                #endregion

                #region IPropertyVector3D
                case pi.PWI_PropertyType.pwi_property_Vector3D: {
                  pi.IPropertyVector3D prop_v3d = prop as pi.IPropertyVector3D;
                  int coord_count = prop_v3d.CoordinateType.NumberOfValue;
                  for (int i = 0; i < coord_count; i++) {
                    TreeNode coord_node = new TreeNode(prop_v3d.CoordinateType.get_Prefix(i));
                    coord_node.Tag = new ReportItemProperty3D(gi, prop_index, i);
                    coord_node.ForeColor = Color.Blue;
                    set_node_state(coord_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);
                    property_node.Nodes.Add(coord_node);
                  }
                  break;
                }
                #endregion

                #region IPropertyUnitVector3D
                case pi.PWI_PropertyType.pwi_property_UnitVector3D: {
                  pi.IPropertyUnitVector3D prop_uv3d = prop as pi.IPropertyUnitVector3D;
                  int coord_count = prop_uv3d.CoordinateType.NumberOfValue;
                  for (int i = 0; i < coord_count; i++) {
                    TreeNode coord_node = new TreeNode(prop_uv3d.CoordinateType.get_Prefix(i));
                    coord_node.Tag = new ReportItemProperty3D(gi, prop_index, i);
                    coord_node.ForeColor = Color.Blue;
                    set_node_state(coord_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);
                    property_node.Nodes.Add(coord_node);
                  }
                  break;
                }
                #endregion

                #region Usual 1 - dimensional property
                default: {
                  property_node.Tag = new ReportItemProperty1D(gi, prop_index);
                  property_node.ForeColor = Color.Blue;
                  set_node_state(property_node, TriState.Unchecked, IMG_IDX_ICON_FEATURE);
                  break;
                }
                #endregion
              }
            }
            #endregion
          }
        }
        #endregion
      } else if (group.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_ISurfaceGroup)) {
        #region ISurfaceGroup

        // We don't want section groups
        if (group.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_ISurfaceSectionGroup)) {
          return;
        }

        // SequenceItem is actually a SurfaceGroup
        pi.ISurfaceGroup sg = group as pi.ISurfaceGroup;

        TreeNode group_node = new TreeNode(sg.Name);
        set_node_state(group_node, TriState.Unchecked, IMG_IDX_ICON_GROUP_SURFACE);
        parent_collection.Add(group_node);

        // Key index starts from 0
        int key_index = 0;


        // Get base measure
        pi.IMeasure measure_master_part = Tools.get_base_measure(doc);

        // Get SequenceItems for the base part
        pi.ISequenceItems sequence_items = sg.get_SequenceItemsForMeasure(measure_master_part);

        // Iterate through all items in the on-the-fly group
        foreach (pi.ISequenceItem pof in sequence_items) {
          // Ignore special items inside surface group
          if (pof.ItemType == pi.PWI_EntityItemType.pwi_ent_Comment_ ||
              pof.ItemType == pi.PWI_EntityItemType.pwi_ent_ProbeChangeItem_ ||
              pof.ItemType == pi.PWI_EntityItemType.pwi_ent_CADViewReport_) {
            key_index++;
            continue;
          }

          // create point on-the-fly tree node
          TreeNode pof_node = add_child_sub_group(group_node, pof.Name);

          // create "Coordinates" sub-group node
          TreeNode coords_node = add_child_sub_group(pof_node, LS.Lc("Coordinates"));

          // we have a 3-dimensional point
          const int coord_count = 3;
          // point may be `surface` or `edge` type
          switch (pof.ItemType) {
            // surface point type
            case pi.PWI_EntityItemType.pwi_srf_SurfacePointGuided_:
            case pi.PWI_EntityItemType.pwi_srf_SurfacePointOnTheFly_: {
              // iterate through all coordinate components
              for (int i = 0; i < coord_count; i++) {
                // create coordinate component's node
                TreeNode coord_node = add_child_feature(coords_node, ReportItemInspection.sPointCoordNames[i]);
                // create appropriate ReportItem for the guided surface point
                coord_node.Tag = new ReportItemSurfacePoint(sg, key_index, i);
              }
              break;
            }

            // edge point type
            case pi.PWI_EntityItemType.pwi_srf_EdgePointGuided_:
            case pi.PWI_EntityItemType.pwi_srf_EdgePointOnTheFly_: {
              // iterate through all coordinate components
              for (int i = 0; i < coord_count; i++) {
                // create coordinate component's node
                TreeNode coord_node = add_child_feature(coords_node, ReportItemInspection.sPointCoordNames[i]);
                // create appropriate ReportItem for the guided surface point
                coord_node.Tag = new ReportItemEdgePoint(sg, key_index, i);
              }
              break;
            }

            // Ignore special items inside surface group
            case pi.PWI_EntityItemType.pwi_ent_Comment_:
            case pi.PWI_EntityItemType.pwi_ent_ProbeChangeItem_:
            case pi.PWI_EntityItemType.pwi_ent_CADViewReport_: {
              break;
            }

            default: {
              Debug.Fail("enumeration has undefined value");
              break;
            }
          }  // switch
          // Add "Deviation" node
          TreeNode deviation_node = add_child_feature(pof_node, LS.Lc(ReportItemInspectionPointDeviation.c_node_name));
          deviation_node.Tag = new ReportItemInspectionPointDeviation(
            sg,
            key_index,
            Tools.entity_item_type_to_surf_edge_poiny_type(pof.ItemType)
          );
          // increase key index
          key_index++;
        } // for each
      }

      #endregion
    }

    //=============================================================================
    /// <summary>
    /// Routine collects all checked nodes of tree that has type of variable and
    /// add them into the ReportItemCollection
    /// </summary>
    /// <returns>ReportItem collection object</returns>
    public ReportItemCollection GetReportItems()
    {
      ReportItemCollection ric = new ReportItemCollection();
      collect_report_items_from_tree(treeViewElements.Nodes,ref ric);
      return ric;
    }

    //=============================================================================
    /// <summary>
    /// Sub-routine used by GetReportItems to walk through the tree recursively
    /// </summary>
    /// <param name="aTNC">Tree node collection to go through</param>
    /// <param name="aRIC">Reference to instance of ReportItemCollection</param>
    private void collect_report_items_from_tree(TreeNodeCollection aTNC, ref ReportItemCollection aRIC)
    {
      foreach (TreeNode tn in aTNC) {
        if (tn.Tag != null) {
          if (TriState.Checked == get_node_state(tn)) {
            aRIC.Add(tn.Tag as ReportItem);
          }          
        }
        if ((tn.Nodes != null) && (tn.Nodes.Count > 0)) {
          collect_report_items_from_tree(tn.Nodes,ref aRIC);
        }
      }      
    }

    //=============================================================================
    /// <summary>
    /// Add child of type "sub-group".
    /// </summary>
    /// <param name="a_parent">Parent node</param>
    /// <param name="a_name">New node name</param>
    /// <returns>New node</returns>
    private TreeNode add_child_sub_group(TreeNode a_parent, string a_name)
    {
      return add_child(a_parent, a_name, TriState.Unchecked, IMG_IDX_ICON_SUBGROUP, null, null);
    }

    //=============================================================================
    /// <summary>
    /// Add child of type "feature".
    /// </summary>
    /// <param name="a_parent">Parent node</param>
    /// <param name="a_name">New node name</param>
    /// <returns>New node</returns>
    private TreeNode add_child_feature(TreeNode a_parent, string a_name)
    {
      return add_child(a_parent, a_name, TriState.Unchecked, IMG_IDX_ICON_FEATURE, Color.Blue, null);
    }

    //==========================================================================
    /// <summary>
    /// Add custom child
    /// </summary>
    /// <param name="a_parent">Parent node</param>
    /// <param name="a_name">New node name</param>
    /// <param name="a_state">New node state</param>
    /// <param name="a_image_index">New node image index</param>
    /// <param name="a_fore_color">New node text fore color</param>
    /// <param name="a_back_color">New node text back color</param>
    /// <returns>New node</returns>
    private TreeNode add_child(
      TreeNode a_parent, 
      string a_name, 
      TriState a_state, 
      int a_image_index, 
      object a_fore_color,
      object a_back_color
    )
    {
      // create sub node
      TreeNode ret = new TreeNode(a_name);
      // make sub node unchecked & assign icon
      set_node_state(ret,a_state,a_image_index);
      // Assign fore color
      if (null != a_fore_color) ret.ForeColor = (Color)a_fore_color;
      // Assign back color
      if (null != a_back_color) ret.BackColor = (Color)a_back_color;
      // Add sub node to nodes collection of parent      
      a_parent.Nodes.Add(ret);
      return ret;
    }
    
    //==========================================================================
    /// <summary>
    /// Routine build the measures list in form
    /// </summary>
    /// <returns>Collection of measures</returns>
    private MeasureCollection GetMeasures()
    {

      MeasureCollection measures = new MeasureCollection();

      foreach (ListViewItem li in listViewMeasures.Items) {      
        if (li.Checked) {
          Debug.Assert(
            li.Tag!=null,
            "FormSPCAddIn.GetMeasures() : listViewMeasures.Items[..].Tag is null"
          );
          pi.IMeasure m = li.Tag as pi.IMeasure;
          Debug.Assert(m!=null, "Error casting to pi.IMeasure");
          measures.Add(m);
        }
      }

      return measures;
    }

    //=========================================================================
    /// <summary>
    /// Restores state of AddIn from .pwi_spc file
    /// </summary>
    private void restore_state()
    {      
      // Restore state of current form view  
      restore_state_measure_list();
      select_latest_measures(); // this is default selection method
      restore_state_variable_tree(treeViewElements.Nodes);
      foreach (TreeNode tn in treeViewElements.Nodes) {
        watch_checks_integrity(tn);
      }
      restore_export_settings();
    }

    //=========================================================================
    /// <summary>
    /// Restore measure list view state according to information, saved by
    /// StateKeeper
    /// </summary>
    private void restore_state_measure_list()
    {
      listViewMeasures.BeginUpdate();
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        lwi.Checked = true;
        string matchStr = (lwi.Tag as pi.IMeasure).UniqueID.ToString()+ "_0";
        foreach (string curStr in m_stateKeeper.ReportMeasures) {         
          if (curStr == matchStr) {
            lwi.ImageIndex = 0; // mark measure as it was NOT exported before
            //lwi.Checked = true; // this measure still requires an export
            break;
          }
        }
      }
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        string matchStr = (lwi.Tag as pi.IMeasure).UniqueID.ToString()+ "_0";
        foreach (string curStr in m_stateKeeper.ExportedMeasures) {         
          if (curStr == matchStr) {
            lwi.ImageIndex = 1; // mark measure as it was exported before
            // there's no need to export this measure again
            //lwi.Checked = false; 
            break;
          }
        }
      }
      listViewMeasures.EndUpdate();
    }

    //=========================================================================
    /// <summary>
    /// Restore state of variable tree (left part of main form) according to
    /// information, saved by StateKeeper
    /// </summary>
    /// <param name="aTreeNodes">Tree nodes to store the result</param>
    private void restore_state_variable_tree(TreeNodeCollection aTreeNodes)
    {
      treeViewElements.BeginUpdate();
      
      foreach (TreeNode tn in aTreeNodes) {
        if (tn.Tag!=null) {          
          foreach (string ri_key in m_stateKeeper.ReportVariables) {
            if ((tn.Tag as ReportItem).Key == ri_key) {
              // we've found one!
              //tn.Checked = true;
              set_node_state(tn,TriState.Checked);
            }
          }
        }
        if (tn.Nodes.Count>0) {
          restore_state_variable_tree(tn.Nodes);
        }
      }

      treeViewElements.EndUpdate();
    }

    //=========================================================================
    /// <summary>
    /// Restore export settings
    /// </summary>
    private void restore_export_settings()
    {
      foreach(ExportSettings es in m_export_settings.Values) {
        es.load_from_state_keeper(m_stateKeeper);
      }      
    }

    //=========================================================================
    /// <summary>
    /// Checks all reported items inside specific tree node
    /// </summary>
    /// <param name="aNode">a node to process</param>
    private int select_all_reported(TreeNode aNode, node_filter_callback a_nfc)
    {
      int items_filtered = 0;

      treeViewElements.BeginUpdate();

      //treeViewElements.ExpandAll();
      if (aNode.Tag != null) {
        // (null != a_nfc) && a_nfc(aNode) 
        ReportItem ri = aNode.Tag as ReportItem;
        Debug.Assert(null != ri, "Expecting ReportItem object");

        TriState state_to_set = TriState.Unchecked;

        if (ri.OutputToReport) {
          if (null != a_nfc) {
            if (a_nfc(aNode)) {
              state_to_set = TriState.Checked;
            } else {
              items_filtered++;
            }
          } else {
            state_to_set = TriState.Checked;
          }
        } 
        set_node_state(aNode, state_to_set);
      }
      if (aNode.Nodes.Count > 0) {
        foreach (TreeNode tn in aNode.Nodes) {
          items_filtered += select_all_reported(tn,a_nfc);
        }
      }
      treeViewElements.EndUpdate();

      return items_filtered;
    }

    //=========================================================================
    /// <summary>
    /// Checks if measure is exportable for all selected report items
    /// </summary>
    /// <param name="aMeasure">A measure to check for exportability</param>
    /// <returns>
    /// True, if measure is exportable for all selected report items
    /// </returns>
    private bool check_measure_is_ok(
      pi.IMeasure aMeasure, 
      out StringCollection aInvalidGroups
    )
    {
      bool allGroupsOK = true;
      aInvalidGroups = new StringCollection();
      // scan all nodes of tree
      foreach (TreeNode tn in treeViewElements.Nodes) {
        if (!check_measure_is_ok_recurse(aMeasure,tn))
        {
          aInvalidGroups.Add(tn.Text);
          allGroupsOK = false;
        }
      }
      return allGroupsOK;
    }

    //=========================================================================
    /// <summary>
    /// Recursively checks that measure is OK for the node all all it's 
    /// sub-nodes
    /// </summary>
    /// <param name="aMeasure">A measure to check</param>
    /// <param name="aNode">A node to start from</param>
    /// <returns></returns>
    private bool check_measure_is_ok_recurse(
      pi.IMeasure aMeasure, 
      TreeNode aNode
    )
    {
      // Check if we have a ReportItem assigned to the node
      if ( (get_node_state(aNode) == TriState.Checked) && (null!=aNode.Tag) ) {
        ReportItem ri = aNode.Tag as ReportItem;
        ri.Measure = aMeasure;
        return ri.HasValidValue;
      }
      // Check if we have a sub-nodes
      if (aNode.Nodes.Count == 0) return true;
      // Scan all sub-nodes
      foreach (TreeNode tn in aNode.Nodes) {
        if (!check_measure_is_ok_recurse(aMeasure,tn)) return false;
      }
      // if we still did not fail then all sub-nodes are valid for measure aMeasure.
      return true;
    }

    //=========================================================================
    /// <summary>
    /// Sets an extra-information message for the measure
    /// </summary>
    /// <param name="aIndex">Index of of item in listViewMeasures</param>
    /// <param name="aOK">
    /// True - if message is OK-like, otherwise - it's error message
    /// </param>
    /// <param name="aMessage">A message to be assigned to the measure</param>
    private void set_listview_measure_extra_info(
      int aIndex, 
      bool aOK, 
      string aMessage
    )
    {
      Debug.Assert(
        aIndex < listViewMeasures.Items.Count, 
        "FormSPCAddIn::SetListViewMeasureExtraInfo() - index out of range"
      );
      ListViewItem li = listViewMeasures.Items[aIndex];
      // remove previous text if it exists
      if (li.SubItems.Count > 1) {
        li.SubItems.RemoveAt(1);
      }
      // and add the new one
      li.SubItems.Add(
        aMessage,
        listViewMeasures.ForeColor,
        //aOK?Color.Green:Color.Red,
        aOK?Color.Green:Color.Red,
        treeViewElements.Font
      );
    }

    
    //=========================================================================
    /// <summary>
    /// Updates measure's extra info of the form's listview
    /// </summary>
    /// <param name="aIndex">Index of item in listview</param>
    private void update_measure_extra_info(int aIndex)
    {
      ListViewItem li = listViewMeasures.Items[aIndex];
      pi.IMeasure m = li.Tag as pi.IMeasure;          
      StringCollection invalidGroups;
      if (check_measure_is_ok(m,out invalidGroups)) {
        set_listview_measure_extra_info(
          aIndex,
          true,
          ""          
        );
      } else {
        string message = "";
        foreach (string groupName in invalidGroups) {
          message += groupName + ", ";
        }
        message = message.Remove(message.LastIndexOf(", "),2);
        set_listview_measure_extra_info(
          aIndex,
          false,
          string.Format(LS.Lc("Invalid for item(s): {0}"),message)
        );            
      }          
    }

    //=========================================================================
    /// <summary>
    /// Updates extra information for all the measures
    /// </summary>    
    private void update_all_measures_extra_info()
    {
      listViewMeasures.BeginUpdate();
      // check all measures
      foreach (ListViewItem lwi in listViewMeasures.Items) 
      {        
        update_measure_extra_info(lwi.Index);
      }
      listViewMeasures.EndUpdate();
    }

    //=========================================================================
    /// <summary>
    /// Selects all available measures
    /// </summary>
    private void select_all_measures()
    {
      listViewMeasures.BeginUpdate();
      // select all measures
      foreach (ListViewItem lwi in listViewMeasures.Items) 
      {
        lwi.Checked = true;        
      }
      listViewMeasures.EndUpdate();
    }

    private void clear_all_measures()
    {
      listViewMeasures.BeginUpdate();
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        lwi.Checked = false;
      }
      listViewMeasures.EndUpdate();
    }

    private void select_latest_measures()
    {
      listViewMeasures.BeginUpdate();
      clear_all_measures();
      // from last measure till first exported measure
      int items_count = listViewMeasures.Items.Count;
      if (items_count <= 0) return;
      for (int i = items_count-1; i >= 0; i-- ) {
        ListViewItem lwi = listViewMeasures.Items[i];
        if (lwi.ImageIndex == 0) { // check if this is a new measure
          lwi.Checked = true;
        } else {
          break; // stop iterations if measure has been already exported
        }
      }
      listViewMeasures.EndUpdate();
    }

    private void select_unexported_measures()
    {
      listViewMeasures.BeginUpdate();
      clear_all_measures();      
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        if (lwi.ImageIndex == 0) { // check if this is a new measure
          lwi.Checked = true;
        }
      }
      listViewMeasures.EndUpdate();            
    }

    private bool select_export_type(out SPCExportType a_export_type)
    {
      a_export_type = SPCExportType.SPC_ET_CSV; // default value
      using (FormFormatSelect ffs = new FormFormatSelect()) {
        ffs.load_from_stake_keeper(m_stateKeeper);
        if (ffs.ShowDialog() != DialogResult.OK) return false;
        ffs.save_to_state_keeper(m_stateKeeper);
        a_export_type = ffs.export_type;
        return true;
      }      
    }

    #region Event handlers

    //=========================================================================
    /// <summary>
    /// EXPORT button click event handler.
    /// </summary>
    /// <param name="sender">Reference to object-sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonExport_Click(object sender, System.EventArgs e)
    {

      // Gather measures
      MeasureCollection piMeasures = GetMeasures();

      // Check if there are measures to export
      if (piMeasures.Count==0) {
        MessageBox.Show(
          LS.Lc("There are no measures to export.\nPlease select at least one."),
          LS.Lc("Warning"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
          );
        return;
      }

      // Gather report items
      ReportItemCollection ric = GetReportItems();

      // Check if there are report items to exports
      if (ric.Count==0) {
        MessageBox.Show(
          LS.Lc("There are no variables to export.\nPlease select at least one."),
          LS.Lc("Warning"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
        return;
      }

      // Check that all measures are valid selected report items
      foreach (ListViewItem lvi in listViewMeasures.Items) {
        StringCollection invalidGroups;
        if (!lvi.Checked) continue;
        if (!check_measure_is_ok(lvi.Tag as pi.IMeasure,out invalidGroups)) {
          MessageBox.Show(
            LS.Lc("Some measures are invalid for selected variables.\nPlease check measure's extra information."),
            LS.Lc("Warning"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation
          );
          return;
        }
      }

      SPCExportType et;

      // let's see if we have information about export type inside state keeper
      if (m_stateKeeper.ExportOptions.Count >= 1) {
        // yes, we have one
        et = (SPCExportType)Convert.ToInt32(m_stateKeeper.ExportOptions[0]);
      } else {
        // looks like we have no configuration chunk on this.
        // let user select the export type.
        // if user did not select export type then stop export at all.
        if (!select_export_type(out et)) return;
      }

      // get export settings for selected export type
      ExportSettings es = m_export_settings[et] as ExportSettings;

      // Check if we have necessary settings to provide export
      // This is a mandatory settings which are usually made by 
      // inspector
      if (!es.check_for_necessary_data(true)) {       
        MessageBox.Show(
          LS.Lc("Initial setup should be made before export."),
          LS.Lc("Error"), 
          MessageBoxButtons.OK,
          MessageBoxIcon.Error
        );
        return;
      }

      // Check for operator input
      // At this place operator fills up the form made by inspector
      enumPreInputResult pre_inp_res = es.pre_export_input();
      switch (pre_inp_res) {
        case enumPreInputResult.PIR_ABORTED: {
          MessageBox.Show(
            LS.Lc("Export aborted by user")            
          );
          return;
        }
        case enumPreInputResult.PIR_WRONG_DATA: {
          MessageBox.Show(
            LS.Lc("Export has failed because of lack of input data"),
            LS.Lc("Warning"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
          );
          return;
        }
      }      

      // OK, we have all the data we need. Now we can do export.
      //
      // Create an exporter
      SPCExporter exporter = ExporterFactory.create(
        et, es, piMeasures, ric, m_stateKeeper
      );

      if (exporter.Export()) {
        MessageBox.Show(
          LS.Lc("Data has been successfully exported")
        );
        es.on_after_export();
        UpdateStateKeeperOnOK(es);
        m_stateKeeper.save();
        Close(); // close main AddIn window
      } else {
        MessageBox.Show(
          LS.Lc("Error while exporting data")
        );
      }

    }

    //=========================================================================
    /// <summary>
    /// Clear all elements Click event handler
    /// </summary>
    /// <param name="sender">Object-sender</param>
    /// <param name="e">Event-arguments</param>
    private void buttonClearAllElem_Click(object sender, System.EventArgs e)
    {
      foreach (TreeNode tn in treeViewElements.Nodes) {
        set_nodes_check_recurse(tn,TriState.Unchecked,null);
      }

      update_all_measures_extra_info();
    }

    //=========================================================================
    private bool filter_items_with_values(TreeNode a_tn)
    {
      ReportItem ri = a_tn.Tag as ReportItem;      
      if (null == ri) return true;
      return ri.CanGetValue;
    }

    //=========================================================================
    /// <summary>
    /// Select all elements Click event handler
    /// </summary>
    /// <param name="sender">Object-sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonSelAllElem_Click(object sender, System.EventArgs e)
    {
      // the amount of filtered items
      int items_was_filtered = 0;


      foreach (TreeNode tn in treeViewElements.Nodes) {
        items_was_filtered += set_nodes_check_recurse(tn, TriState.Checked, filter_items_with_values);
      }

      update_all_measures_extra_info();

      // Warn user if some items were filtered out
      if (items_was_filtered > 0) {
        warn_n_items_hasnt_been_selected(items_was_filtered);
      }
    }

    //=========================================================================
    private void warn_n_items_hasnt_been_selected(int a_items_count)
    {
      MessageBox.Show(
        String.Format(
          LS.Lc(" {0} items have not been selected because they are not applicable for active sort of measurement"),
          a_items_count
        ),
        LS.Lc("Attention"),
        MessageBoxButtons.OK,
        MessageBoxIcon.Exclamation
      );
    }

    //=========================================================================
    /// <summary>
    /// On load form event handler
    /// </summary>
    /// <param name="sender">Object-sender</param>
    /// <param name="e">Event arguments</param>
    private void FormSPCAddIn_Load(object sender, System.EventArgs e)
    {
      // init state keeper 
      m_stateKeeper.init(true);
      // load configuration
      m_stateKeeper.load();      
      // init settings dictionary
      foreach (SPCExportType et in Enum.GetValues(typeof(SPCExportType))) {
        m_export_settings[et] = ExportSettingsFactory.create(et);
      }

      build_complete_tree_image_list();
      build_tree();
      build_measure_list();      
      restore_state();      
      // Update extra information for measures no matter 
      // if they are checked or not.
      update_all_measures_extra_info();
    }

    //=========================================================================
    /// <summary>
    /// Event handler for ItemCheck event of measures list view
    /// </summary>
    /// <param name="sender">Object-sender</param>
    /// <param name="e">Event arguments</param>
    private void listViewMeasures_ItemCheck(
      object sender, 
      System.Windows.Forms.ItemCheckEventArgs e
    )
    {
      if (
        (e.CurrentValue == CheckState.Unchecked) &&
        (listViewMeasures.Items[e.Index].ImageIndex==1) &&
        (listViewMeasures.SelectedItems.Count<=1)
      ) {
        if (
          MessageBox.Show(
            LS.Lc("This measure have been exported before.\nRe-exporting measure may cause wrong statistic processing by SPC software.\n Are you sure you want to re-export data?"),
            LS.Lc("Attention"),
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation
          ) == DialogResult.No
        ) {
          e.NewValue = CheckState.Unchecked;
        } 
      }
      if (  e.CurrentValue == CheckState.Unchecked) {
        update_measure_extra_info(e.Index);
      }      
    }

    //=========================================================================
    /// <summary>
    /// Updates state when user presses OK button
    /// </summary>
    private void UpdateStateKeeperOnOK(ExportSettings a_export_settings)
    {
      m_stateKeeper.ExportedMeasures.Clear();
      m_stateKeeper.ReportMeasures.Clear();
      m_stateKeeper.ReportVariables.Clear();
      
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        if (
          lwi.Checked || // if item was just exported
          (lwi.ImageIndex == 1) // or has been exported in previous sessions
        ) {
          m_stateKeeper.ExportedMeasures.Add((lwi.Tag as pi.IMeasure).UniqueID + "_0");
        }
      }

      ReportItemCollection ric = GetReportItems();
      foreach (IReportItem ri in ric) {
        m_stateKeeper.ReportVariables.Add(ri.Key);
      }

      a_export_settings.save_to_state_keeper(m_stateKeeper);

    }

    //=========================================================================
    /// <summary>
    /// Updates state when user presses Cancel button
    /// </summary>
    private void UpdateStateKeeperOnClose()
    {
      if (PIConnector.instance.active_document.FullPathName.Length == 0) {
        return;
      }

      m_stateKeeper.ReportMeasures.Clear();
      m_stateKeeper.ReportVariables.Clear();
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        if (
          lwi.Checked && // if item ready to export
          lwi.ImageIndex == 0 // and hasn't been exported before
        ) {
          m_stateKeeper.ReportMeasures.Add((lwi.Tag as pi.IMeasure).UniqueID + "_0");
        }
      }
      ReportItemCollection ric = GetReportItems();
      foreach (IReportItem ri in ric) {
        m_stateKeeper.ReportVariables.Add(ri.Key);
      }
      m_stateKeeper.save();
    }

    //=========================================================================
    /// <summary>
    /// Event handler for `Select All Measures` button click
    /// </summary>
    /// <param name="sender">Object-sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonSelectAllMeasures_Click(object sender, System.EventArgs e)
    {      
      foreach (ListViewItem lwi in listViewMeasures.Items) {
        if (lwi.ImageIndex == 1) {
          // there are saved measures in list!
          // ask user if he want to overwrite them all
          if (
            MessageBox.Show(
              LS.Lc("There are already exported measures in the list.\nRe-exporting measures may cause some problems in statistics calculation process of SPC software.\nAre you sure you want to overwrite them all?"),
              LS.Lc("Already exported measures detected"),
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question
            ) == DialogResult.Yes
          ) {
            break;
          } else {
            return;
          }
        }
      }

      // disable event handler to prevent `already exported` dialogs
      this.listViewMeasures.ItemCheck -= 
        new System.Windows.Forms.ItemCheckEventHandler(this.listViewMeasures_ItemCheck);

      select_all_measures();

      // enable event handler after all
      this.listViewMeasures.ItemCheck += 
        new System.Windows.Forms.ItemCheckEventHandler(this.listViewMeasures_ItemCheck);

    }

    //=========================================================================
    /// <summary>
    /// Event handler for `Clear All Measures` button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonClearAllMeasures_Click(object sender, System.EventArgs e)
    {
      clear_all_measures();
    }

    //=========================================================================
    /// <summary>
    /// Event handler for `Select All Reported` button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonSelectAllReported_Click(object sender, System.EventArgs e)
    {
      int items_filtered = 0;
      treeViewElements.BeginUpdate();
      foreach (TreeNode tn in treeViewElements.Nodes) {
        items_filtered += select_all_reported(tn,filter_items_with_values);
        watch_checks_integrity(tn);
      }
      treeViewElements.EndUpdate();

      update_all_measures_extra_info();

      if (items_filtered > 0) {
        warn_n_items_hasnt_been_selected(items_filtered);
      }
    }


    //=========================================================================
    /// <summary>
    /// On mouse click event handler for tree view
    /// </summary>
    /// <param name="sender">Object-sender</param>
    /// <param name="e">Event arguments for an event</param>
    private void treeViewElements_Click(object sender, System.EventArgs e)
    {      
      Point mousePos = treeViewElements.PointToClient(Control.MousePosition);
      TreeNode tn = treeViewElements.GetNodeAt(mousePos);

      // Check if node exists
      if (null==tn) return;

      // Determine hit zone width
      int widthHitZone = tn.Bounds.X - mousePos.X - 16;

      // Zone of check-box is about 16 pixels
      if ((widthHitZone < 0) || (widthHitZone>16)) {
        // looks like we are not in check-box zone.
        // check if right mouse button is pressed
        if (MouseButtons.Right == Control.MouseButtons) {
          contextMenuTreeView.Show(treeViewElements,mousePos);
        }
        // we need not to proceed anymore
        return;
      }

      // FROM THAT MOMENT WE CAN BE SURE CHECK BOX RECTANGLE WAS CLICKED

      //tn.ExpandAll();

      int filtered_items = 0;

      if (
        (Keys.Control == Control.ModifierKeys) && // if CTRL key is pressed
        (tn.Nodes.Count==0) // and we've got a atomic-feature selected
      ) {
        // TRY TO INVERT SELECTION OF `SAME` FEATURES
        string nodeName = tn.Text;
        TriState nodeStateToSet = 
          get_node_state(tn)==TriState.Unchecked?TriState.Checked:TriState.Unchecked;
        // start from 2 levels upper than current
        int levelsToGoUp = 2;
        // find such node
        while ((levelsToGoUp-->0) && (null!=tn.Parent)) {
          tn = tn.Parent;
        }
        // Get tri-state and try to invert it
        if (nodeStateToSet != TriState.Unchecked) {
          filtered_items += set_nodes_check_recurse(
            tn, nodeStateToSet, nodeName,filter_items_with_values
          );
        } else {
          set_nodes_check_recurse(tn, nodeStateToSet, nodeName,null);
        }
      } else {
        // OTHERWISE - JUST INVERT AN ITEM'S CHECK
        // Get tri-state and try to invert it
        TriState nodeStateToSet = 
          get_node_state(tn)==TriState.Unchecked?TriState.Checked:TriState.Unchecked;
        
        if (nodeStateToSet != TriState.Unchecked) {
          filtered_items += set_nodes_check_recurse(
            tn, nodeStateToSet, filter_items_with_values
          );
        } else {
          set_nodes_check_recurse(tn, nodeStateToSet, null);
        }
      }
      // Find the root (super-parent)
      while (null!=tn.Parent) tn = tn.Parent;
      // Watch checks integrity
      watch_checks_integrity(tn);

      // Update measures extra information
      for (int i=0;i<listViewMeasures.Items.Count; i++) {
        if (listViewMeasures.Items[i].Checked) {
          update_measure_extra_info(i);
        }
      }

      if (filtered_items > 0) {
        warn_n_items_hasnt_been_selected(filtered_items);
      }

    }

    //=========================================================================
    /// <summary>
    /// treeViewPopup Expand_Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void treeViewPopup_Expand_Click(object sender, System.EventArgs e)
    {
      TreeNode tn = treeViewElements.SelectedNode;
      if (null==tn) return;
      tn.Expand();
    }

    //=========================================================================
    /// <summary>
    /// treeViewPopup Collapse_Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event argument</param>
    private void treeViewPopup_Collapse_Click(object sender, System.EventArgs e)
    {
      TreeNode tn = treeViewElements.SelectedNode;
      if (null==tn) return;
      tn.Collapse();
    }

    //=========================================================================
    /// <summary>
    /// treeViewPopup ExpandAll_Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void treeViewPopup_ExpandAll_Click(object sender, System.EventArgs e)
    {
      TreeNode tn = treeViewElements.SelectedNode;
      if (null==tn) return;
      tn.ExpandAll();
    }

    //=========================================================================
    /// <summary>
    /// buttonExportSettings Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonExportSettings_Click(object sender, EventArgs e)
    {
      // ask user about export type
      SPCExportType et;
      if (!select_export_type(out et)) return;

      if ((m_export_settings[et] as ExportSettings).load_from_dialog()) {
        // if we succeeded, then save settings into state keeper 
        // for future usage
        bool res = (m_export_settings[et] as ExportSettings).save_to_state_keeper(m_stateKeeper);
        Debug.Assert(res, "Failed to save export settings");
        if (res) {
          m_stateKeeper.save(); // Flush settings to .pwi_spc file
        }
      }      
    }

    //=========================================================================
    /// <summary>
    /// buttonSelectLatest_Click
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonSelectLatest_Click(object sender, EventArgs e)
    {
      select_latest_measures();
    }

    //=========================================================================
    /// <summary>
    /// buttonSelectUnexported Click event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void buttonSelectUnexported_Click(object sender, EventArgs e)
    {
      select_unexported_measures();
    }

    #endregion

    //=========================================================================
    /// <summary>
    /// FormSPCAddIn FormClosing event handler
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
    private void FormSPCAddIn_FormClosing(object sender, FormClosingEventArgs e)
    {
      UpdateStateKeeperOnClose();
    }

    #endregion

  } // end class FormSPCAddIn

} // end namespace Autodesk.PowerInspect.AddIns.SPC

