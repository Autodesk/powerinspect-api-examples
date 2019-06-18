//=============================================================================
//
//D ReportItemGDTToleranceZone class - represents tolerance zone report item
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
  /// ReportItemGDTToleranceZone class - represents tolerance zone report item
  /// </summary>
  [
    ComVisible(true),   
    ClassInterface(ClassInterfaceType.AutoDual),
    Guid("51648545-EF9A-4602-8BBC-30DC3D38E2A1")
  ]
  public class ReportItemGDTToleranceZone : ReportItemGDT //, IReportItem
  {

    #region Methods

    
    //=============================================================================
    /// <summary>
    /// ReportItemGDTToleranceZone class constructor
    /// </summary>
    /// <param name="aSequenceItem">GDT report item</param>
    /// <param name="aName">Property name</param>
    /// <param name="aResultXPath">XML's X-Path where result is stored</param>
    /// <param name="aTolUp">Upper tolerance value</param>
    /// <param name="aTolLo">Lower tolerance value</param>
    /// <param name="aTolAndBonus"></param>
    public ReportItemGDTToleranceZone(
      pi.ISequenceItem aSequenceItem,
      string aName,
      string aResultXPath,
      double aTolUp,
      double aTolLo,
      double aTolAndBonus
    ) : base (aSequenceItem, aName)
    {
      m_resultXPath = aResultXPath;
      m_lowerTolerance = aTolLo;
      m_upperTolerance = aTolUp;
      m_toleranceAndBonus = aTolAndBonus;
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// Lower tolerance value
    /// </summary>
    protected double m_lowerTolerance;

    //=============================================================================
    /// <summary>
    /// Upper tolerance value
    /// </summary>
    protected double m_upperTolerance;

    //=============================================================================
    /// <summary>
    /// ToleranceAndBonusValue
    /// </summary>
    protected double m_toleranceAndBonus;

    //=============================================================================
    /// <summary>
    /// X-Path string where result is located
    /// </summary>
    protected string m_resultXPath;

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Gets the value of report item for the current measure
    /// </summary>
    /// <returns>Value of the ReportItem</returns>
    public override double Value
    {
      get
      {
        return GetMeasuredResult(Measure,m_resultXPath);
      }      
    }
    //=============================================================================
    /// <summary>
    /// Returns a unique KEY-string of report item
    /// </summary>
    public override string Key
    {
      get
      {
        return m_sequenceItem.UniqueID.ToString() + "_" + m_resultXPath;
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns nominal value
    /// </summary>
    public override double Nominal
    {
      get
      {
        return 0.0;
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns lower tolerance value
    /// </summary>
    public override double LowerTolerance
    {
      get
      {
        return ValueProcessingFunc(m_lowerTolerance);
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns upper tolerance value
    /// </summary>
    public override double UpperTolerance
    {
      get
      {
        return ValueProcessingFunc(m_upperTolerance);
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns tolerance and bonus value
    /// </summary>
    public virtual double ToleranceAndBonus
    {
      get
      {
        return ValueProcessingFunc(m_toleranceAndBonus);
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns type of report item
    /// </summary>
    public override SPCReportItemType ReportItemType
    {
      get
      {
        return SPCReportItemType.RIT_GEOMETRIC_GDT_TOLZONE;
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns name of report variable
    /// </summary>
    public override string ReportVariableName
    {
      get
      {
        Debug.Assert(SequenceItem!=null,"ReportItemGDTToleranceZone.ReportVariableName:: SequenceItem is null");
        Debug.Assert(SequenceItem.Group!=null,"ReportItemGDTToleranceZone.ReportVariableName:: SequenceItem.Group is null");

        return m_name + spc_VAR_NAME_DELIMITER + SequenceItem.Name + 
          spc_VAR_NAME_DELIMITER + SequenceItem.Group.Name;
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
        return m_name;
      }
    }

    #endregion

  } // class ReportItemGDTToleranceZone

} // end namespace Autodesk.PowerInspect.AddIns.SPC

