//=============================================================================
//
//D Tools class.
//D Contains some helpful static methods.
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
using System.Runtime.InteropServices;
using System.Diagnostics;
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// Summary description for Tools.
  /// </summary>
  [ComVisible(false)]
  public class Tools
  {
    //=========================================================================
    /// <summary>
    /// Gets master part of a document
    /// </summary>
    /// <param name="a_doc">A PI document</param>
    /// <returns>Master part measure</returns>
    public static pi.IMeasure get_master_part(pi.IPIDocument a_doc)
    {
      int idx = 1;
      foreach (pi.IMeasure m in a_doc.Measures) {
        if (!m.IsSimulated) break;        
        idx++;
      }
      pi.IMeasure ret_measure = a_doc.Measures[idx];
      Debug.Assert(null != ret_measure, "Cannot find master part measure");
      return ret_measure;
    }

    //=========================================================================
    /// <summary>
    /// Gets active measure or if it is not possible returns master part measure
    /// This is called also as BASE measure in terms of SPC.
    /// </summary>
    /// <param name="a_doc">A PI document</param>
    /// <returns>Active measure or master part measure</returns>
    public static pi.IMeasure get_base_measure(pi.IPIDocument a_doc)
    {
      pi.IMeasure active_measure = a_doc.get_ActiveMeasure();
      if (null != active_measure) {
        if (!active_measure.IsSimulated) return active_measure;        
      }
      return get_master_part(a_doc);      
    }

    //=============================================================================
    /// <summary>
    /// Maps PI surface group type to ReportItem's inspection point type
    /// </summary>
    /// <param name="a_surface_group_type">PI surface group type</param>
    /// <returns>ReportItem's inspection point type</returns>
    public static RI_InspectionPointType surface_group_type_to_inspection_point_type(
      pi.PWI_SurfaceGroupType a_surface_group_type
    )
    {
      switch (a_surface_group_type) {
        case pi.PWI_SurfaceGroupType.pos_GuidedPoints: {
          return RI_InspectionPointType.RIIPT_GUIDED;
        }
        case pi.PWI_SurfaceGroupType.pos_PointsOnTheFly: {
          return RI_InspectionPointType.RIIPT_ON_THE_FLY;
        }
        default: {
          Debug.Fail("Wrong enumeration value");
          return RI_InspectionPointType.RIIPT_ON_THE_FLY;
        }
      }
    }

    //=============================================================================
    /// <summary>
    /// Maps PI entity item type to ReportItem's inspection surf/edge point type
    /// </summary>
    /// <param name="a_entity_item_type">PI entity item type</param>
    /// <returns>ReportItem's inspection surf/edge point type</returns>
    public static ReportItemInspectionPointDeviation.RI_InspectionSurfEdgePointType entity_item_type_to_surf_edge_poiny_type(       
      pi.PWI_EntityItemType a_entity_item_type
    )
    {
      switch (a_entity_item_type)
      {
        case pi.PWI_EntityItemType.pwi_srf_EdgePointGuided_:
        case pi.PWI_EntityItemType.pwi_srf_EdgePointOnTheFly_: {
          return ReportItemInspectionPointDeviation.RI_InspectionSurfEdgePointType.RIIPT_EDGE;
        }          
        case pi.PWI_EntityItemType.pwi_srf_SurfacePointGuided_:
        case pi.PWI_EntityItemType.pwi_srf_SurfacePointOnTheFly_: {
          return ReportItemInspectionPointDeviation.RI_InspectionSurfEdgePointType.RIIPT_SURFACE;
        }        
        default: {
          Debug.Fail("Wrong enumeration value");
          return ReportItemInspectionPointDeviation.RI_InspectionSurfEdgePointType.RIIPT_SURFACE;
        }
      }
    }

  }
}

