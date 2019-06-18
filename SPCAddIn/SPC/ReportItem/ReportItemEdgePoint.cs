//=============================================================================
//
//D ReportItemEdgepoint class
//D Base class for all report items that represent edge points.
//
// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
//------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// Aliases
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //============================================================================
  /// <summary>
  /// Summary description for ReportItemEdgepoint.
  /// </summary>
  [  
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("1646b365-adfe-43cd-b584-8b607a8b4c33")
  ]
  public class ReportItemEdgePoint : ReportItemInspection //, IReportItemInspection
  {

    #region Methods

    //=========================================================================
    /// <summary>
    /// ReportItemSurfacePoint class constructor
    /// </summary>
    /// <param name="aSurfaceGroup">Surface group used by report item</param>
    /// <param name="aKeyIndex">Index used in Key property</param>
    /// <param name="aCoordinateIndex">Coordinate (zero-base) index. All inspection report items are 3-dimentional.</param>
    public ReportItemEdgePoint(
      pi.ISurfaceGroup aSurfaceGroup,
      int aKeyIndex,
      int aCoordinateIndex
    )
     : base (aSurfaceGroup, aKeyIndex, aCoordinateIndex)
    {
    }

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Gets the value of current report item according to the current measure
    /// </summary>
    public override double Value
    {
      get
      {
        pi.IEdgePointItem si = SequenceItem;
        Debug.Assert(null!=si, "ReportItemSurfacePoint::Value() - SequenceItem property is NULL");
        pi.IMeasure m = Measure;
        Debug.Assert(null!=m, "ReportItemSurfacePoint::Value() - Measure property is NULL");
        double x,y,z;
        si.MeasuredPoint(m).ContactPoint.GetCoordinates(out x, out y, out z);
        return ValueProcessingFunc(Get3DValueByIndex(x,y,z,m_coordinateIndex));
      }      
    }

    //=========================================================================
    /// <summary>
    /// Sequence item used in ReportItem.
    /// In case of edge point it is an object that supports IEdgePointItem interface.
    /// </summary>
    public new pi.IEdgePointItem SequenceItem
    {
      get
      {
        pi.IEdgePointItem epi = base.SequenceItem as pi.IEdgePointItem;
        return epi;
      }
      set
      {
        Debug.Assert(false, "Call to ReportItemEdgePoint.SequenceItem set property.");
      }
    }

    //=========================================================================
    /// <summary>
    /// Nominal value (actual)
    /// </summary>
    public override double Nominal
    {
      get
      {
        Debug.Assert(null!=SequenceItem,"ReportItemEdgePoint::Nominal - SequenceItem property is NULL");
        Debug.Assert(null!=Measure,"ReportItemEdgePoint::Nominal - Measure property is NULL");
        double x,y,z;
        SequenceItem.MeasuredPoint(Measure).EdgePoint.Point.GetCoordinates(out x, out y, out z);
        return ValueProcessingFunc(Get3DValueByIndex(x,y,z,m_coordinateIndex));
      }
    }

    //=========================================================================
    /// <summary>
    /// Lower tolerance of the measure
    /// </summary>
    public override double LowerTolerance
    {
      get
      {
        return ValueProcessingFunc(SequenceItem.LowerTolerance);
      }
    }

    //=========================================================================
    /// <summary>
    /// Upper measure tolerance
    /// </summary>
    public override double UpperTolerance
    {
      get
      {
        return ValueProcessingFunc(SequenceItem.UpperTolerance);
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns current ReportItem type. This property must be overridden by derived classes
    /// </summary>
    public override SPCReportItemType ReportItemType
    {
      get
      {
        RI_InspectionPointType point_type = 
          Tools.surface_group_type_to_inspection_point_type(
            SequenceItem.PointType
          );

        switch(point_type) {
          case RI_InspectionPointType.RIIPT_GUIDED:
            return SPCReportItemType.RIT_INSPECTION_EDGE_POINT_GUIDED;
          case RI_InspectionPointType.RIIPT_ON_THE_FLY:
            return SPCReportItemType.RIT_INSPECTION_EDGE_POINT_ON_THE_FLY;
          default:
            Debug.Fail("Incorrect enumeration value in the switch statement");
            return SPCReportItemType.RIT_INSPECTION_SURFACE_POINT_ON_THE_FLY;
        }
        
      }
    }

    #endregion

  }
}

