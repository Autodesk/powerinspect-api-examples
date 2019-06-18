//=============================================================================
//
//D ReportItemGDTDimention class
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

  //=========================================================================
  /// <summary>
  /// ReportItemGDTDimention class - implements GD&T dimension property
  /// </summary>
  [
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("3dfaa351-55eb-4239-a764-60cdcba9dc85")
  ]
  public class ReportItemGDTDimention : ReportItemGDT //, IReportItem
  {

    #region Constants

    //=============================================================================
    /// <summary>
    /// Path to average measure values
    /// </summary>
    private const string GDT_MEASURE_AVERAGE_XPATH = "result/dimension_result/@averagemeasured";

    //=============================================================================
    /// <summary>
    /// Path to min measure values
    /// </summary>
    private const string GDT_MEASURE_MAX_XPATH = "result/dimension_result/@maxmeasured";

    //=============================================================================
    /// <summary>
    /// Path to max measure values
    /// </summary>
    private const string GDT_MEASURE_MIN_XPATH = "result/dimension_result/@minmeasured";

    #endregion

    #region Methods

    //=========================================================================
    /// <summary>
    /// ReportItemGDTDimention class constructor
    /// </summary>
    /// <param name="aGDTItem">GDT report item</param>
    /// <param name="aName">property name</param>
    /// <param name="aUpperTolerance">upper value of tolerance</param>
    /// <param name="aLowerTolerance">lower value of tolerance</param>
    /// <param name="aNominal">nominal value</param>
    public ReportItemGDTDimention(
      pi.ISequenceItem aGDTItem,
      string aName,
      double aUpperTolerance,
      double aLowerTolerance,
      double aNominal
    ) : base (aGDTItem, aName)
    {
      m_upperTolerance = aUpperTolerance;
      m_lowerTolerance = aLowerTolerance;
      m_nominal = aNominal;
    }

    //=============================================================================
    /// <summary>
    /// Gets average measured value
    /// </summary>
    /// <param name="aMeasure">measure to get value of</param>
    /// <returns>value of measure</returns>
    public double MeasuredAverage(pi.IMeasure aMeasure)
    {
      return GetMeasuredResult(aMeasure, GDT_MEASURE_AVERAGE_XPATH);
    }

    //=============================================================================
    /// <summary>
    /// Gets minimum measured value
    /// </summary>
    /// <param name="aMeasure">measure to get value of</param>
    /// <returns>value of measure</returns>
    public double MeasuredMin(pi.IMeasure aMeasure)
    {
      return GetMeasuredResult(aMeasure, GDT_MEASURE_MIN_XPATH);
    }

    //=============================================================================
    /// <summary>
    /// Gets maximum measured value
    /// </summary>
    /// <param name="aMeasure">measure to get value of</param>
    /// <returns>value of measure</returns>
    public double MeasuredMax(pi.IMeasure aMeasure)
    {
      return GetMeasuredResult(aMeasure, GDT_MEASURE_MAX_XPATH);
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// Nominal value
    /// </summary>
    protected double m_nominal;

    //=============================================================================
    /// <summary>
    /// Value of upper tolerance
    /// </summary>
    protected double m_upperTolerance;

    //=============================================================================
    /// <summary>
    /// Value of lower tolerance
    /// </summary>
    protected double m_lowerTolerance;

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Gets the value of ReportItem for the current measure
    /// </summary>
    /// <returns>Value</returns>
    public override double Value
    {
      get
      {
        return MeasuredAverage(Measure);
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
        return m_sequenceItem.UniqueID.ToString() + "_" + GDT_MEASURE_AVERAGE_XPATH;
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns report variable name
    /// </summary>
    public override string ReportVariableName
    {
      get
      {
        Debug.Assert(SequenceItem!=null,"ReportItemGDTDimention.ReportVariableName:: SequenceItem is null");
        Debug.Assert(SequenceItem.Group!=null,"ReportItemGDTDimention.ReportVariableName:: SequenceItem.Group is null");

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


    //=============================================================================
    /// <summary>
    /// Returns nominal value
    /// </summary>
    public override double Nominal
    {
      get
      {
        return m_nominal;
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
        return m_lowerTolerance;
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
        return m_upperTolerance;
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns instance of class that provides ISequenceItem and IGDTItem interfaces
    /// </summary>
    public override pi.ISequenceItem SequenceItem
    {
      get
      {
        return base.SequenceItem;
      }
      set
      {
        Debug.Write("ReportItemGDTDimention.SequenceItem property must not be set");
      }
    }


    //=============================================================================
    /// <summary>
    /// Returns report item type
    /// </summary>
    public override SPCReportItemType ReportItemType
    {
      get
      {
        return SPCReportItemType.RIT_GEOMETRIC_GDT_DIMENTION;
      }
    }

    #endregion

  } // end class ReportItemGDTDimention

} // end namespace Autodesk.PowerInspect.AddIns.SPC

