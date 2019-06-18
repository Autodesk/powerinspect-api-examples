//=============================================================================
//D ReportItemMock.cs
//
// Mock class for IReportItem interface
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.PowerInspect.AddIns.SPC;
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC.Tests.Mocks
{
  public class ReportItemMock : IReportItem
  {
    //==========================================================================
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Full name of report item</param>
    /// <param name="short_name">Short name of report item</param>
    /// <param name="units">Units</param>
    /// <param name="nominal">Nominal value</param>
    /// <param name="actual">Actual value</param>
    /// <param name="lo_tol">Lower tolerance</param>
    /// <param name="up_tol">Upper tolerance</param>
    /// <param name="num_digits">Number of decimal digits</param>
    /// <param name="measure_date">Date of the measurement</param>
    public ReportItemMock(
      string name,
      string short_name,
      RIUnits units,
      double nominal, 
      double actual, 
      double lo_tol, 
      double up_tol,
      long num_digits,
      DateTime measure_date
    )
    {
      HasValidValue = true;
      Key = "0";      
      Measure = null;
      LowerTolerance = lo_tol;
      UpperTolerance = up_tol;
      Nominal = nominal;
      NumberOfDigits = num_digits;
      OutputToReport = true;
      ReportItemType = SPCReportItemType.RIT_GEOMETRIC_PROPERTY_1D;
      ReportVariableName = name;
      ReportVariableNameShort = short_name;
      Units = units;
      SequenceItem = null;
      UseNumberOfDigits = true;
      Value = actual;
      MeasureDate = measure_date;
    }

    //==========================================================================
    /// <summary>
    /// Returns true if report item has measured/calculated value
    /// </summary>
    public bool HasValidValue
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Key used by state keeper
    /// </summary>
    public string Key
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Lower tolerance
    /// </summary>
    public double LowerTolerance
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Assigned measurement
    /// </summary>
    public pi.IMeasure Measure
    {
      get;
      set;
    }

    //==========================================================================
    /// <summary>
    /// Nominal value
    /// </summary>
    public double Nominal
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Number of decimal digits
    /// </summary>
    public long NumberOfDigits
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Returns true if item has output to report flag set
    /// </summary>
    public bool OutputToReport
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Report item type
    /// </summary>
    public SPCReportItemType ReportItemType
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Report variable full name
    /// </summary>
    public string ReportVariableName
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Report variable short name
    /// </summary>
    public string ReportVariableNameShort
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Assosiated sequence item
    /// </summary>
    public pi.ISequenceItem SequenceItem
    {
      get;
      set;
    }

    //==========================================================================
    /// <summary>
    /// Units of report item
    /// </summary>
    public RIUnits Units
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Units as string
    /// </summary>
    public string UnitsStr
    {
      get
      {
        switch (Units) {
          case RIUnits.RIU_DEGREE:
            return "deg";
          case RIUnits.RIU_MILLIMETRES:
            return "mm";
          case RIUnits.RIU_INCHES:
            return "in";
          case RIUnits.RIU_DIMENTIONLESS:
            return "";
          default:
            Debug.Fail("Incorrect enumeration value in the switch statement");
            return "";
        }
      }
    }

    //==========================================================================
    /// <summary>
    /// Upper tolerance
    /// </summary>
    public double UpperTolerance
    {
      get;
      private set;
    }

    //==========================================================================
    /// <summary>
    /// Use or not decimal numbers truncations
    /// </summary>
    public bool UseNumberOfDigits
    {
      get;
      set;
    }

    //==========================================================================
    /// <summary>
    /// Actual value of report item
    /// </summary>
    public double Value
    {
      get;
      private set;
    }


    //==========================================================================
    /// <summary>
    /// Date of the measurement
    /// </summary>
    public DateTime MeasureDate
    {
      get;
      private set;
    }
  }
}
