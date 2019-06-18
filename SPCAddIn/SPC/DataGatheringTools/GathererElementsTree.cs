//=============================================================================
//D GathererElementsTree class
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

using pi = PowerINSPECT;
using System.Globalization;

namespace Autodesk.PowerInspect.AddIns.SPC
{


  //=============================================================================
  /// <summary>
  /// GathererElementsTree class - gather elements tree from PIApp
  /// </summary>
  internal sealed class GathererElementsTree
  {
    //=============================================================================
    /// <summary>
    /// GathererElementsTree class constructor
    /// </summary>
    internal GathererElementsTree()
    {
    }
    
    //=========================================================================
    /// <summary>
    /// Gets the tree from PIApp (as an XTreeNodeCollection object).
    /// </summary>
    /// <returns>
    /// An XTreeNodeCollection object representing the sequence tree.
    /// </returns>
    /// <seealso cref="FormSPCAddIn::build_tree_nested_groups"/>
    static public XTreeNodeCollection GatherTree()
    {
      pi.IPIDocument doc = PIConnector.instance.active_document;
      Debug.Assert(
        doc != null,
        "GathererElementTree.GatherTree() : active_document is null"
      );

      XTreeNodeCollection root = new XTreeNodeCollection();
      gather_items(doc.SequenceItems, root);
      return root;
    }


    //=========================================================================
    /// <summary>
    /// Adds a tree nodes for all sequence items in the given collection to the
    /// given tree nodes.
    /// </summary>
    /// <param name="sequence_items">
    /// Collection of sequence items from the document or a group.
    /// </param>
    /// <param name="parent_nodes">
    /// Tree node collection that is filled with information from the sequence
    /// items.
    /// </param>
    static private void gather_items(
      pi.ISequenceItems sequence_items,
      XTreeNodeCollection parent_nodes
    )
    {
      // Loop through the sequence items in this collection
      foreach (pi.ISequenceItem si in sequence_items) {
        if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGeometricGroup)) {
          gather_geometric_group(si as pi.IGeometricGroup, parent_nodes);

        } else if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_ISurfaceGroup)) {
          // We don't want section groups
          if (!si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_ISurfaceSectionGroup)) {
            gather_surface_group(si as pi.ISurfaceGroup, parent_nodes);
          }

        } else if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGDTItem)) {
          gather_gdt_item(si as pi.IGDTItem, parent_nodes);

        } else if (si.IsA(pi.PWI_SequenceItemInterfaceType.pwi_sq_IGeometricItem)) {
          gather_geometric_item(si as pi.IGeometricItem, parent_nodes);
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Adds a tree node for a GD&T item to the given tree nodes.
    /// </summary>
    /// <param name="gdt">
    /// GD&T item to get information from.
    /// </param>
    /// <param name="parent_nodes">
    /// Tree node collection that is filled with information from the GD&T
    /// item.
    /// </param>
    static private void gather_gdt_item(
      pi.IGDTItem gdt,
      XTreeNodeCollection parent_nodes
    )
    {
      pi.IMeasure measure = gdt.Document.get_ActiveMeasure();

      MSXML2.IXMLDOMElement xml_root = null;
      try {
        xml_root = ReportItemGDT.GetXMLOutput(gdt, measure);
      } catch (Exception) {
      }
      if (xml_root == null) return;

      MSXML2.IXMLDOMElement xml_gdt = ReportItemGDT.GetGDTItemXML(xml_root, measure);
      if (xml_gdt == null) return;

      MSXML2.IXMLDOMElement xml_result = xml_gdt.selectSingleNode("result/*") as MSXML2.IXMLDOMElement;
      if (xml_result == null) return;

      string item_name = xml_root.getAttribute("Name") as string;

      XTreeNode item_node = new XTreeNode(item_name);
      parent_nodes.Add(item_node);

      string condition = xml_root.getAttribute("GDTCondition") as string;

      XTreeNode result_node = new XTreeNode();
      item_node.Nodes.Add(result_node);

      result_node.Tag = new ReportItemGDTResult(
        gdt,
        LS.Lc("IsAccepted"),
        "result/@statusid"
      );
      result_node.Text = LS.Lc("IsAccepted");

      switch (xml_result.nodeName) {
        #region tolerance zone
        case "composite_tolerancezone":
        case "tolerancezone": {
          string xpath = "";
          if (xml_result.nodeName == "composite_tolerancezone") {
            xml_result = xml_gdt.selectSingleNode("feature/result/*") as MSXML2.IXMLDOMElement;
            xpath = "feature/result/tolerancezone/";
          } else {
            xpath = "result/tolerancezone/";
          }

          if (xml_result != null && xml_result.nodeName == "tolerancezone") {
            xpath += "@width";
            double original_tol = Convert.ToDouble(xml_result.selectSingleNode("@original").text.Replace(',', '.'), CultureInfo.InvariantCulture);
            double allowed_tol = Convert.ToDouble(xml_result.selectSingleNode("@allowance").text.Replace(',','.'), CultureInfo.InvariantCulture);

            XTreeNode tol_zone_node = new XTreeNode();
            item_node.Nodes.Add(tol_zone_node);

            tol_zone_node.Tag = new ReportItemGDTToleranceZone(
              gdt,
              condition,
              xpath,
              original_tol,
              0,
              allowed_tol
            );
            tol_zone_node.Text = condition;
          }
          break;
        }
        #endregion

        #region dimension result
        case "dimension_result": {
          double upper_tol = Convert.ToDouble(xml_gdt.selectSingleNode("dimension_nominal/@uppertolerance").text.Replace(',', '.'), CultureInfo.InvariantCulture);
          double lower_tol = Convert.ToDouble(xml_gdt.selectSingleNode("dimension_nominal/@lowertolerance").text.Replace(',', '.'), CultureInfo.InvariantCulture);
          double nominal = Convert.ToDouble(xml_gdt.selectSingleNode("dimension_nominal/@nominaldimension").text.Replace(',', '.'), CultureInfo.InvariantCulture);

          XTreeNode dimension_node = new XTreeNode();
          item_node.Nodes.Add(dimension_node);

          dimension_node.Tag = new ReportItemGDTDimention(
            gdt,
            condition,
            upper_tol,
            lower_tol,
            nominal
          );
          dimension_node.Text = condition;

          break;
        }
        #endregion
      }
    }

    //=========================================================================
    /// <summary>
    /// Adds a tree node for a geometric group to the given tree nodes.
    /// </summary>
    /// <param name="gg">
    /// Geometric group to get information from.
    /// </param>
    /// <param name="parent_nodes">
    /// Tree node collection that is filled with information from the geometric
    /// group.
    /// </param>
    static private void gather_geometric_group(
      pi.IGeometricGroup gg,
      XTreeNodeCollection parent_nodes
    )
    {
      Debug.Assert(
        gg.SequenceItems != null,
        "GathererElementsTree.gather_geometric_group() : null gg.SequenceItems"
      );

      if (gg.SequenceItems.Count == 0) return;

      XTreeNode group_node = new XTreeNode(gg.Name);
      parent_nodes.Add(group_node);

      // Add items from within the group
      gather_items(gg.SequenceItems, group_node.Nodes);
    }

    //=========================================================================
    /// <summary>
    /// Adds a tree node for a geometric item to the given tree nodes.
    /// </summary>
    /// <param name="gi">
    /// Geometric item to get information from.
    /// </param>
    /// <param name="parent_nodes">
    /// Tree node collection that is filled with information from the geometric
    /// item.
    /// </param>
    static private void gather_geometric_item(
      pi.IGeometricItem gi,
      XTreeNodeCollection parent_nodes
    )
    {
      XTreeNode item_node = new XTreeNode(gi.Name);
      parent_nodes.Add(item_node);

      int prop_count = gi.Properties.Count;
      for (int prop_index = 1; prop_index <= prop_count; prop_index++) {
        pi.IProperty prop = gi.Properties[prop_index];

        XTreeNode property_node = new XTreeNode(prop.Name);
        item_node.Nodes.Add(property_node);

        switch (prop.PropertyType) {
          #region IPropertyPoint3D
          case pi.PWI_PropertyType.pwi_property_Point3D: {
            pi.IPropertyPoint3D prop_p3d = prop as pi.PropertyPoint3D;
            int coord_count = prop_p3d.CoordinateType.NumberOfValue;
            for (int i = 0; i < coord_count; i++) {
              XTreeNode coord_node = new XTreeNode(prop_p3d.CoordinateType.get_Prefix(i));
              coord_node.Tag = new ReportItemProperty3D(gi, prop_index, i);
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
              XTreeNode coord_node = new XTreeNode(prop_v3d.CoordinateType.get_Prefix(i));
              coord_node.Tag = new ReportItemProperty3D(gi, prop_index, i);
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
              XTreeNode coord_node = new XTreeNode(prop_uv3d.CoordinateType.get_Prefix(i));
              coord_node.Tag = new ReportItemProperty3D(gi, prop_index, i);
              property_node.Nodes.Add(coord_node);
            }
            break;
          }
          #endregion

          #region Usual 1 - dimensional property
          default: {
            property_node.Tag = new ReportItemProperty1D(gi, prop_index);
            break;
          }
          #endregion
        }
      }
    }

    //=========================================================================
    /// <summary>
    /// Adds a tree node for a surface group to the given tree nodes.
    /// </summary>
    /// <param name="gdt">
    /// Surface group to get information from.
    /// </param>
    /// <param name="parent_nodes">
    /// Tree node collection that is filled with information from the surface
    /// group.
    /// </param>
    static private void gather_surface_group(
      pi.ISurfaceGroup sg,
      XTreeNodeCollection parent_nodes
    )
    {
      XTreeNode group_node = new XTreeNode(sg.Name);
      parent_nodes.Add(group_node);

      // Key index starts from 0
      int key_index = 0;

      // Quantity of points is undetermined

      // Get base measure
      pi.IMeasure measure_master_part = Tools.get_base_measure(sg.Document);

      // Get SequenceItems for the master part
      pi.ISequenceItems sequence_items = sg.get_SequenceItemsForMeasure(measure_master_part);

      // Iterate through all items in the on-the-fly group
      foreach (pi.ISequenceItem pof in sequence_items) {
        // create point on-the-fly tree node
        XTreeNode pof_node = new XTreeNode(pof.Name);
        group_node.Nodes.Add(pof_node);

        // create "Coordinates" sub-group node
        XTreeNode coords_node = new XTreeNode("Coordinates");
        pof_node.Nodes.Add(coords_node);

        // we have a 3-dimensional point
        const int coord_count = 3;
        // point may be `surface` or `edge` type
        switch (pof.ItemType) {
          // surface point type
          case pi.PWI_EntityItemType.pwi_srf_SurfacePointOnTheFly_:
          case pi.PWI_EntityItemType.pwi_srf_SurfacePointGuided_: {
            // iterate through all coordinate components
            for (int i = 0; i < coord_count; i++) {
              // create coordinate component's node
              XTreeNode coord_node = new XTreeNode(ReportItemInspection.sPointCoordNames[i]);
              coords_node.Nodes.Add(coord_node);
              // create appropriate ReportItem for the guided surface point
              coord_node.Tag = new ReportItemSurfacePoint(sg, key_index, i);
            }
            break;
          }

          // edge point type
          case pi.PWI_EntityItemType.pwi_srf_EdgePointOnTheFly_: 
          case pi.PWI_EntityItemType.pwi_srf_EdgePointGuided_: {
            // iterate through all coordinate components
            for (int i = 0; i < coord_count; i++) {
              // create coordinate component's node
              XTreeNode coord_node = new XTreeNode(ReportItemInspection.sPointCoordNames[i]);
              coords_node.Nodes.Add(coord_node);
              // create appropriate ReportItem for the guided surface point
              coord_node.Tag = new ReportItemEdgePoint(sg, key_index, i);
            }
            break;
          }

          default: {
            Debug.Fail("enumeration has undefined value");
            break;
          }
        }

        // Add "Deviation" node
        XTreeNode deviation_node = new XTreeNode(ReportItemInspectionPointDeviation.c_node_name);
        deviation_node.Tag = new ReportItemInspectionPointDeviation(
          sg,
          key_index,
          Tools.entity_item_type_to_surf_edge_poiny_type(pof.ItemType)
        );
        pof_node.Nodes.Add(deviation_node);

        // increase key index
        key_index++;
      }
      // clear the reference
      sequence_items = null;
    }
  }
}
