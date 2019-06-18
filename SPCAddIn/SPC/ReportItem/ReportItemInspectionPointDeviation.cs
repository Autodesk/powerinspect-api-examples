//=============================================================================
//D ReportItemInspectionPointDeviation.cs
//
// Report item: Deviation of inspection point
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
  //===========================================================================
  /// <summary>
  /// Deviation value for inspection point
  /// </summary>
  [
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("596E12F3-E613-44d4-A3E1-C2F67C6DFA39")
  ]
  public sealed class ReportItemInspectionPointDeviation : ReportItemInspection //, IReportItemInspection
  {
    #region Constants
    /// <summary> Node name. Used by BuildTree() method. </summary>
    internal const string c_node_name = "Deviation";
    #endregion
    #region Type Definitions
    /// <summary> Point type </summary>
    public enum RI_InspectionSurfEdgePointType {
      RIIPT_SURFACE = 0,
      RIIPT_EDGE = 1
    };
    #endregion

    //=========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="a_surface_group">Surface group of inspection point</param>
    /// <param name="a_key_index">Key index. Used by Key property.</param>
    /// <param name="a_point_type">Type of inspection point (guided/on the fly)</param>
    /// <param name="a_surf_edge_point_type">Type of inspection point (surface/edge)</param>
    public ReportItemInspectionPointDeviation(
      pi.ISurfaceGroup a_surface_group,
      int a_key_index,
      RI_InspectionSurfEdgePointType a_surf_edge_point_type
    )
      : base ( a_surface_group, a_key_index, -1)
    {        
      m_key_index = a_key_index;      
      m_surf_edge_point_type = a_surf_edge_point_type;
    }

    //=========================================================================
    /// <summary>
    /// Unique key value of report item
    /// </summary>
    public override string Key
    {      
      get {        
        Debug.Assert(null!=m_sequenceItem,"m_sequenceItem field is NULL");
        return m_sequenceItem.UniqueID.ToString() + "_" +
          m_keyIndex.ToString() + "_" +
          "deviation";
      }      
    }
    
    //=========================================================================
    /// <summary>
    /// Lower tolerance 
    /// </summary>
    public override double LowerTolerance
    {
      get {
        double ret = 0.0;
        switch (SurfEdgePointType)
        {
          case RI_InspectionSurfEdgePointType.RIIPT_EDGE: 
          {
            pi.IEdgePointItem si = SequenceItem as pi.IEdgePointItem;
            Debug.Assert(null!=si, "Cannot cast SequenceItem to IEdgePointItem");
            if (null == si) break;            
            ret = ValueProcessingFunc(si.LowerTolerance);            
            break;
          }
          case RI_InspectionSurfEdgePointType.RIIPT_SURFACE : 
          {
            pi.ISurfacePointItem si = SequenceItem as pi.ISurfacePointItem;
            Debug.Assert(null!=si, "Cannot cast SequenceItem to ISurfacePointItem");
            if (null == si) break;           
            ret = ValueProcessingFunc(si.LowerTolerance);            
            break;
          }
          default: 
          {
            Debug.Fail("Wrong enumeration value");
            break;
          }          
        }
        return ret;
      }
    }

    //=========================================================================
    /// <summary>
    /// Upper tolerance
    /// </summary>
    public override double UpperTolerance
    {
      get {
        double ret = 0.0;
        switch (SurfEdgePointType)
        {
          case RI_InspectionSurfEdgePointType.RIIPT_EDGE: 
          {
            pi.IEdgePointItem si = SequenceItem as pi.IEdgePointItem;
            Debug.Assert(null!=si, "Cannot cast SequenceItem to IEdgePointItem");
            if (null == si) break;            
            ret = ValueProcessingFunc(si.UpperTolerance);            
            break;
          }
          case RI_InspectionSurfEdgePointType.RIIPT_SURFACE : 
          {
            pi.ISurfacePointItem si = SequenceItem as pi.ISurfacePointItem;
            Debug.Assert(null!=si, "Cannot cast SequenceItem to ISurfacePointItem");
            if (null == si) break;           
            ret = ValueProcessingFunc(si.UpperTolerance);            
            break;
          }
          default: 
          {
            Debug.Fail("Wrong enumeration value");
            break;
          }          
        }
        return ret;
      }
    }

    //=========================================================================
    /// <summary>
    /// Type of SPC report item
    /// </summary>
    public override SPCReportItemType ReportItemType
    {
      get {
        return SPCReportItemType.RIT_INSPECTION_POINT_DEVIATION;
      }
    }

    //=========================================================================
    /// <summary>
    /// Short name of report variable
    /// </summary>
    public override string ReportVariableNameShort
    {
      get
      {
        return c_node_name;
      }
    }

    //=========================================================================
    /// <summary>
    /// Nominal value
    /// </summary>
    public override double Nominal
    {
      get
      {
        // As well as we are exporting deviation, the nominal
        // (ideal) value is zero
        return 0.0;
      }
    }

    //=========================================================================
    /// <summary>
    /// Deviation value
    /// </summary>
    public override double Value
    {
      get
      {
        double ret = 0.0;
        switch (SurfEdgePointType)
        {
          case RI_InspectionSurfEdgePointType.RIIPT_EDGE: {
            pi.IEdgePointItem si = SequenceItem as pi.IEdgePointItem;
            Debug.Assert(null!=si, "Cannot cast SequenceItem to IEdgePointItem");
            if (null == si) break;
            pi.IEdgePointResult epr = si.MeasuredPoint(Measure);
            Debug.Assert(null!=epr, "Cannot get edge point result");
            if (null == epr) break;
            ret = ValueProcessingFunc(epr.Deviation);            
            break;
          }
          case RI_InspectionSurfEdgePointType.RIIPT_SURFACE : {
            pi.ISurfacePointItem si = SequenceItem as pi.ISurfacePointItem;
            Debug.Assert(null!=si, "Cannot cast SequenceItem to ISurfacePointItem");
            if (null == si) break;
            pi.ISurfacePointResult spr = si.MeasuredPoint(Measure);
            Debug.Assert(null!=spr, "Cannot get surface point result");
            if (null == spr) break;
            ret = ValueProcessingFunc(spr.Deviation);            
            break;
          }
          default: {
            Debug.Fail("Wrong enumeration value");
            break;
          }          
        }
        return ValueProcessingFunc(ret);
      }
    }

    //=========================================================================
    /// <summary>
    /// Type of inspection point (surface or edge)
    /// </summary>
    public RI_InspectionSurfEdgePointType SurfEdgePointType
    {
      get
      { 
        return m_surf_edge_point_type;
      }
    }

    /// <summary> Key index </summary>
    private int m_key_index;

    /// <summary> Type of inspection point (surface or edge) </summary>
    private RI_InspectionSurfEdgePointType m_surf_edge_point_type;
    
  }
}

