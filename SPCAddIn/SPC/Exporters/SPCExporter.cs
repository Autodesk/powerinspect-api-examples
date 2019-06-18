//=============================================================================
//
//D SPCExporter class
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
using System.Globalization;
using System.Runtime.InteropServices;
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //=============================================================================
  /// <summary>
  /// ISPCExporter -- common interface for all SPC exporters
  /// </summary>
  [
    ComVisible(true),
    Guid("0A2861EE-D2D4-439D-A967-E894FBA4B9F9")
  ]
  public interface ISPCExporter
  {
    bool Export();
    string FailureMsg();
  }

  //=============================================================================
  /// <summary>
  /// Base class for all exporters
  /// </summary>
  [  
    ComVisible(false)
  ]
  public abstract class SPCExporter
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// CustomExporter class constructor
    /// </summary>
    /// <param name="aMeasures">Measures to export</param></param>
    /// <param name="aReportItems">report items (variables / characteristics)</param>
    protected SPCExporter(
      MeasureCollection a_measures,
      ReportItemCollection a_report_items
    )
    {
      // Preconditions      
      Debug.Assert(a_measures!=null, "SPCExporter.SPCExporter() : aMeasures collection is null");
      Debug.Assert(a_report_items!=null, "SPCExporter.SPCExporter() : aReportItems collection id null");      
      m_measures = a_measures;
      m_reportItems = a_report_items;
      m_failure_msg = "";
    }
    
    //=============================================================================
    /// <summary>
    /// Exports data to SPC
    /// </summary>
    /// <returns></returns>
    public abstract bool Export();

    //=============================================================================
    /// <summary>
    /// Returns a message that explains the reason of failure
    /// </summary>
    /// <returns>Message text that explains the reason of failure</returns>
    public string FailureMsg()
    {
      return m_failure_msg;
    }

    //=============================================================================
    /// <summary>
    /// Sets up the message that explains the reason of failure
    /// </summary>
    /// <param name="a_msg">A message to set up</param>
    protected void FailureMsg(string a_msg)
    {
      m_failure_msg = a_msg;
    }

    //==========================================================================
    /// <summary>
    /// Formats double using format provider and number of decimal places
    /// </summary>
    /// <param name="format">Format provider (culture)</param>
    /// <param name="val">Value</param>
    /// <param name="num_digits">Number of decimal places</param>
    /// <returns></returns>
    protected string format_double_custom(
      IFormatProvider format, 
      double val, 
      long num_digits
    )
    {
      Debug.Assert(num_digits >= 0 && num_digits <= 10, "Wrong argument value");

      string zeroes = "";
      for (int i = 0; i < num_digits; i++) {
        zeroes += "0";
      }

      return String.Format(format, "{0:0." + zeroes + "}", val);
    }

    //=============================================================================
    /// <summary>
    /// Formats double as string with limited fractional part digits count
    /// </summary>
    /// <param name="aValue">A double value</param>
    /// <param name="aNumDigits">A quantity of fractional digits to output.</param>
    /// <returns></returns>
    protected virtual string format_double(double val, long num_digits)
    {
      return format_double_custom(CultureInfo.CurrentCulture, val, num_digits);
    }

    #endregion

    #region Fields
    /// <summary> Measures, that was selected in main AddIn's form </summary>
    protected MeasureCollection m_measures;

    /// <summary> Report items, selected in main AddIn's form </summary>
    protected ReportItemCollection m_reportItems;

    /// <summary> Failure message for exporter </summary>
    private string m_failure_msg;

    #endregion

    #region Properties
    #endregion

  }

}

