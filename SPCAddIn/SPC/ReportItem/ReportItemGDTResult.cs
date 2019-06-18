//=============================================================================
//
//D ReportItemGDTResult class
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
  /// ReportItemGDTResult class - represents GD&T result
  /// </summary>
  [
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("6be7251f-134c-4c55-8b24-ce39c3c1e56c")
  ]
  public class ReportItemGDTResult : ReportItemGDT //, IReportItem
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// ReportItemGDTResult class constructor
    /// </summary>
    /// <param name="aSequenceItem">a SequenceItem to use</param>
    /// <param name="aName">name of report item</param>
    /// <param name="aResultXPath">x-path to result in XML document</param>
    public ReportItemGDTResult(
      pi.ISequenceItem aGDTItem,
      string aName,
      string aResultXPath
    ) : base (aGDTItem, aName)
    {
      m_resultXPath = aResultXPath;
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// x-path in XML where the result is located
    /// </summary>
    protected string m_resultXPath;

    #endregion

    #region Properties

    //=============================================================================
    /// <summary>
    /// Gets the value of report item for the current measure
    /// </summary>
    /// <returns>value of report item</returns>
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
        return 1.0;
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
        return 0.0;
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
        return 0.0;
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
        return SPCReportItemType.RIT_GEOMETRIC_GDT_RESULT;
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns report variable name
    /// </summary>
    public override string ReportVariableName
    {
      get
      {
        Debug.Assert(SequenceItem!=null,"ReportItemGDTResult.ReportVariableName:: SequenceItem is null");
        Debug.Assert(SequenceItem.Group!=null,"ReportItemGDTResult.ReportVariableName:: SequenceItem.Group is null");

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

  } // end class ReportItemGDTResult

} // end namespace Autodesk.PowerInspect.AddIns.SPC

