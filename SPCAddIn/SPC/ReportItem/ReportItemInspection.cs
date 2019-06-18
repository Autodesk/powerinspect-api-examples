//=============================================================================
//
//D ReportItemSEPoint class.
//D Surface & Edge points report item.
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

using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //=============================================================================
  /// <summary>
  /// Inspection point type. I.e. Guided or OnTheFly.
  /// </summary>
  [
    ComVisibleAttribute(true)    
  ]
  public enum RI_InspectionPointType
  {
    RIIPT_GUIDED = 0,
    RIIPT_ON_THE_FLY = 1
  };

  
  //=========================================================================
  /// <summary>
  /// ReportItemInspection class. 
  /// Base class for inspection report items.
  /// </summary>
  [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDual),
    GuidAttribute("8f2427bf-1739-4153-be4c-6415e55b81bf")
  ]
  public abstract class ReportItemInspection : ReportItem//, IReportItemInspection
  {

    #region Constants

    public static string[]sPointCoordNames = {"X", "Y", "Z"};

    #endregion

    #region Type Definitions 

    #endregion


    #region Methods

    //=============================================================================
    /// <summary>
    /// ReportItemInspection class constructor
    /// </summary>
    /// <param name="aSurfaceGroup">Surface group used by ReportItem</param>
    /// <param name="aKeyIndex">Index used in Key property</param>
    /// <param name="aCoordinateIndex">Coordinate (zero-base) index. All inspection report items are 3-dimentional.</param>
    /// <param name="aPointType">Type of point</param>
    protected ReportItemInspection(
      pi.ISurfaceGroup aSurfaceGroup,
      int aKeyIndex, 
      int aCoordinateIndex
    )
      : base(aSurfaceGroup as pi.ISequenceItem)
    {
      m_numberOfDigits = PIConnector.instance.active_document.NumberOfDigits;
      m_keyIndex = aKeyIndex;
      m_coordinateIndex = aCoordinateIndex;
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// Index that used for a Key property
    /// </summary>
    protected int m_keyIndex;

    //=============================================================================
    /// <summary>
    /// Current coordinate index
    /// </summary>
    protected int m_coordinateIndex;

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Sequence item used in ReportItem.    
    /// </summary>
    public override pi.ISequenceItem SequenceItem
    {
      get
      {
        // Dynamically get appropriate sequence item. We can use m_keyIndex, that 
        // was created for Key property, instead of creating the new one.

        pi.ISurfaceGroup sg = m_sequenceItem as pi.ISurfaceGroup;
        Debug.Assert(null!=sg,"ReportItemInspection.SequenceItem - m_sequenceItem does not support ISurfaceGroup interface");
        
        pi.ISequenceItem si = null;

        pi.ISequenceItems sis = sg.get_SequenceItemsForMeasure(Measure);
        Debug.Assert(null!=sis,"ReportItemInspection.SequenceItem - get_SequenceItemsForMeasure(Measure) returns NULL");
        if (m_keyIndex<sis.Count) {
          si = sis[m_keyIndex+1];
          Debug.Assert(null!=si,"ReportItemInspection.SequenceItem - Cannot get property value");
        } else {
          si = null;
        }
        return si;
      }
      set
      {
        Debug.Assert(false, "Call to ReportItemInspection.SequenceItem set property.");
      }
    }


    //=============================================================================
    /// <summary>
    /// Key string for inspection report item
    /// </summary>
    public override string Key
    {
      get
      {        
        return base.Key + "_" + 
               m_keyIndex.ToString() + "_" +
               m_coordinateIndex.ToString();
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns unique report variable name 
    /// </summary>
    public override string ReportVariableName
    {
      get
      {
        // store currently selected measure
        pi.IMeasure tmpMeasure = Measure;
        Measure = Tools.get_base_measure(PIConnector.instance.active_document); //SPCAddin.PIApp.ActiveDocument.Measures["Master part"];
        string siName = SequenceItem.Name;
        Measure = tmpMeasure;        
        return ReportVariableNameShort + spc_VAR_NAME_DELIMITER +
               siName + spc_VAR_NAME_DELIMITER +
               m_sequenceItem.Name;
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns unique report variable name 
    /// </summary>
    public override string ReportVariableNameShort
    {
      get
      {
        return sPointCoordNames[m_coordinateIndex];
      }
    }

    //=========================================================================
    /// <summary>
    /// Units property. Should be overridden in descendants.
    /// </summary>
    public override RIUnits Units
    {
      get
      {
        return RIUnits.RIU_DIMENTIONLESS;
      }
    }
    
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public override bool HasValidValue
    {
      get
      {
        pi.ISequenceItem si = SequenceItem;
        if (null!=si) {
          return si.get_ResultValid(Measure);
        } else {
          return false;
        }        
      }
    }


    #endregion

  }
}

