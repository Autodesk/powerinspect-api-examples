//=============================================================================
//
//D ReportItemSurfacePoint class.
//D Base class for all report items that represent surface points.
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
using System.Runtime.InteropServices;
// Aliases
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// Summary description for ReportItemSurfacePoint.
  /// </summary>
  [  
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("c8dff306-d5c0-4536-8418-0b0236850919")
  ]
  public class ReportItemSurfacePoint : ReportItemInspection //, IReportItemInspection
  {

    #region Methods

    //=========================================================================
    /// <summary>
    /// ReportItemSurfacePoint class constructor
    /// </summary>
    /// <param name="aSurfaceGroup">Surface group used by report item</param>
    /// <param name="aKeyIndex">Index used in Key property</param>
    /// <param name="aCoordinateIndex">Coordinate (zero-base) index. All inspection report items are 3-dimentional.</param>
    /// <param name="aPointType">Type of point</param>
    public ReportItemSurfacePoint(
      pi.ISurfaceGroup aSurfaceGroup,
      int aKeyIndex,
      int aCoordinateIndex
    )
      : base(aSurfaceGroup,aKeyIndex,aCoordinateIndex)
    {
    }

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Gets the value of current report item according to measure aMeasure
    /// </summary>
    public override double Value
    {
      get
      {
        pi.ISurfacePointItem si = SequenceItem;
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
    /// In case of surface point it is an object that supports ISurfacePointItem
    /// interface.
    /// </summary>
    public new pi.ISurfacePointItem SequenceItem
    {
      get
      {
        // Dynamically get appropriate sequence item. We can use m_keyIndex, that 
        // was created for Key property, instead of creating the new one.
        pi.ISurfacePointItem spi = base.SequenceItem as pi.ISurfacePointItem;
        //Debug.Assert(null!=spi,"ReportItemSurfacePoint.SequenceItem - cannot get surface point item");
        return spi;
      }
      set
      {
        Debug.Assert(false, "Call to ReportItemSurfacePoint.SequenceItem set property.");
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
    /// Lower measure tolerance
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
    /// Nominal value (actual)
    /// </summary>
    public override double Nominal
    {
      get
      {
        Debug.Assert(null!=SequenceItem,"ReportItemSurfacePoint::Nominal - SequenceItem property is NULL");
        Debug.Assert(null!=Measure,"ReportItemSurfacePoint::Nominal - Measure property is NULL");
        double x,y,z;
        SequenceItem.MeasuredPoint(Measure).SurfacePoint.Point.GetCoordinates(out x, out y, out z);
        return ValueProcessingFunc(Get3DValueByIndex(x,y,z,m_coordinateIndex));
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
            return SPCReportItemType.RIT_INSPECTION_SURFACE_POINT_GUIDED;
          case RI_InspectionPointType.RIIPT_ON_THE_FLY:
            return SPCReportItemType.RIT_INSPECTION_SURFACE_POINT_ON_THE_FLY;
          default:
            Debug.Fail("incorrect enumeration value in the switch statement");
            return SPCReportItemType.RIT_INSPECTION_SURFACE_POINT_GUIDED;
        }
      }
    }

    #endregion

  }
}

