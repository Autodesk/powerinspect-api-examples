//=============================================================================
//
//D ReportItem class. Base abstract class for all report items
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
using System.Collections;
// Aliases
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  #region Type definitions

  //=========================================================================
  /// <summary>
  /// Unit types used by ReportItem
  /// </summary>
  public enum RIUnits
  {
    RIU_DIMENTIONLESS = 0,
    RIU_MILLIMETRES = 1,
    RIU_INCHES = 2,
    RIU_DEGREE = 3
  };

  //=========================================================================
  /// <summary>
  /// Type that defined type of ReportItem's derived class instance
  /// </summary>
  public enum SPCReportItemType 
  {
    RIT_GEOMETRIC_PROPERTY_1D = 0,
    RIT_GEOMETRIC_PROPERTY_3D = 1,
    RIT_GEOMETRIC_GDT_DIMENTION = 2,
    RIT_GEOMETRIC_GDT_RESULT = 3,
    RIT_GEOMETRIC_GDT_TOLZONE = 4,
    RIT_INSPECTION_SURFACE_POINT_GUIDED = 5,
    RIT_INSPECTION_SURFACE_POINT_ON_THE_FLY = 6,
    RIT_INSPECTION_EDGE_POINT_GUIDED = 7,
    RIT_INSPECTION_EDGE_POINT_ON_THE_FLY = 8,
    RIT_INSPECTION_POINT_DEVIATION = 9
  };

  #endregion

  //=============================================================================
  /// <summary>
  /// IReportItem interface. Supported by every class based on ReportItem
  /// </summary>
  [    
    GuidAttribute("a56f9fb8-3f1f-4715-bff0-01f823f5cd4d"),
    ComVisibleAttribute(true),    
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)
  ]
  public interface IReportItem
  {
    #region Methods


    #endregion

    #region Properties

    //=============================================================================
    /// <summary>
    /// Determines if value is valid for measure aMeasure
    /// </summary>
    /// <param name="aMeasure">measure that variable is contained in</param>
    /// <returns>true - if value is valid</returns>
    bool HasValidValue
    {
      get;
    }


    //=============================================================================
    /// <summary>
    /// Returns a unique KEY-string of report item
    /// </summary>
    string Key
    {
      get;      
    }

    SPCReportItemType ReportItemType
    {
      get;
    }

    RIUnits Units
    {
      get;
    }

    //=============================================================================
    /// <summary>
    /// Gets value of report item for selected measure
    /// </summary>
    double Value 
    {
      get;
    }



    //=============================================================================
    /// <summary>
    /// If value of this property is true, then such properties as .Normal, 
    /// .UpperTolerance, .LowerTolerance, ... etc. will use NumberOfDigits
    /// property in their output.
    /// </summary>
    bool UseNumberOfDigits
    {
      get;
      set;      
    }

    //=========================================================================
    /// <summary>
    /// Units property
    /// </summary>
    string UnitsStr
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Gets date & time when report item has been measured
    /// </summary>
    DateTime MeasureDate
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Nominal value
    /// </summary>
    double Nominal
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Upper measure tolerance
    /// </summary>
    double UpperTolerance
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Lower measure tolerance
    /// </summary>
    double LowerTolerance
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Number of digits user want to see in report
    /// </summary>
    long NumberOfDigits
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Sequence item used in ReportItem
    /// </summary>
    pi.ISequenceItem SequenceItem
    {
      get;
      set;
    }

    //=========================================================================
    /// <summary>
    /// Checks if 'OutputToReport' flag is set for current report item
    /// </summary>
    bool OutputToReport
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Returns unique report variable name 
    /// </summary>
    string ReportVariableName
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Returns unique report variable name 
    /// </summary>
    string ReportVariableNameShort
    {
      get;
    }
    
    //=============================================================================
    /// <summary>
    /// The currently used measure
    /// </summary>
    pi.IMeasure Measure
    {
      get;    
      set;
    }

    
    #endregion

  } // end interface IReportItem



  //=============================================================================
  /// <summary>
  /// Base class for all report items
  /// </summary>
  [
    ComVisible(true),    
    ClassInterface(ClassInterfaceType.AutoDual),
    GuidAttribute("e6d688ec-4944-4b03-9a50-ebece1811379")
  ]
  public abstract class ReportItem : IReportItem
  {


    #region Constants

    //=============================================================================
    /// <summary>
    /// Delimiter that used by ReportVariableName function to separare variable 
    /// names in hierarchy.
    /// </summary>
    public static string spc_VAR_NAME_DELIMITER = "::";

    #endregion

    #region Methods

    //=============================================================================
    /// <summary>
    /// ReportItem class constructor
    /// </summary>
    /// <param name="aSequenceItem">Sequence item, report item uses to extract necessary data</param>
    protected ReportItem(pi.ISequenceItem aSequenceItem)
    {
      // set `Master part` measure as default
      m_measure = Tools.get_base_measure(PIConnector.instance.active_document);
      // initialize sequence item
      m_sequenceItem = aSequenceItem;
      // find a super group
      // first of all check it our sequence item is a group
      m_sequenceGroup = m_sequenceItem as pi.ISequenceGroup;
      if (null==m_sequenceGroup) {
        // if not, get the group of sequence item
        m_sequenceGroup = m_sequenceItem.Group;
        Debug.Assert(null!=m_sequenceGroup, "ReportItem::ReportItem() : m_sequenceGroup is NULL");
      }
      // then find a super-group
      while (null != m_sequenceGroup.Group)
      {
        m_sequenceGroup = m_sequenceGroup.Group;
      }
      // user number of digits limitation by default
      m_useNumberOfDigits = true;
      // by default, use number of digits from ActiveDocument's settings
      m_numberOfDigits = PIConnector.instance.active_document.NumberOfDigits;
      // initialize stack for Push/Pop digits limitation functions
      m_useNumberOfDigitsStates = new Stack();
    }

    //=============================================================================
    /// <summary>
    /// ReportItem class destructor
    /// </summary>
    ~ReportItem()
    {
      m_sequenceItem = null;
    }

    //=============================================================================
    /// <summary>
    /// This method process aValue according to property UseNumberOfDigits
    /// </summary>
    /// <param name="aValue">original value</param>
    /// <returns>returns processed value</returns>
    protected double ValueProcessingFunc(double aValue)
    {      
      return UseNumberOfDigits ? Math.Round(aValue, (int)NumberOfDigits) : aValue;
    }

    //=============================================================================
    /// <summary>
    /// Returns one of coordinates (x,y or z) by its index (zero-based).
    /// </summary>
    /// <param name="aX">X coordinate</param>
    /// <param name="aY">Y coordinate</param>
    /// <param name="aZ">Y coordinate</param>
    /// <param name="aIndex">Index of coordinate which value is returned</param>
    /// <returns>X, Y or Z value, depending of aIndex value</returns>
    protected static double Get3DValueByIndex(double aX, double aY, double aZ, int aIndex)
    {
      Debug.Assert((aIndex>=0) && (aIndex<3), "Get3DValueByIndex() - index should be from 0 to 2");
      switch(aIndex) {
        case 0:
          return aX;        
        case 1:
          return aY;
        case 2:
          return aZ;
      default:
          return 0.0;
      }
    }

    //=============================================================================
    /// <summary>
    /// Stores previous value of UseNumberOfDigits property and sets a new one.
    /// </summary>
    /// <param name="aValue">A new value of UseNumberOfDigits property</param>
    protected void PushUseNumberOfDigitsState(bool aValue)
    {
      m_useNumberOfDigitsStates.Push(UseNumberOfDigits);
      UseNumberOfDigits = aValue;
    }

    //=============================================================================
    /// <summary>
    /// Restores previous value of the UseNumberOfDigits property from state stack.
    /// </summary>
    protected void PopUseNumberOfDigitsState()
    {
      Debug.Assert(m_useNumberOfDigitsStates.Count > 0,"ReportItem.PopUseNumberOfDigitsState() - Cannot extract previous value. Stack is empty.");
      if (m_useNumberOfDigitsStates.Count > 0) {
        UseNumberOfDigits = (bool)(m_useNumberOfDigitsStates.Pop());
      }
    }
    

    #endregion

    #region Fields

    //=========================================================================
    /// <summary>
    /// Number of digits. used in formated output
    /// </summary>
    protected int m_numberOfDigits;

    //=========================================================================
    /// <summary>
    /// Sequence item
    /// </summary>
    protected pi.ISequenceItem m_sequenceItem;

    //=========================================================================
    /// <summary>
    /// Sequence group, a super parent of current SequenceItem
    /// </summary>
    protected pi.ISequenceGroup m_sequenceGroup;

    //=============================================================================
    /// <summary>
    /// If value of this field is true, then such properties as .Normal, 
    /// .UpperTolerance, .LowerTolerance, ... etc.
    /// </summary>
    private bool m_useNumberOfDigits;

    //=============================================================================
    /// <summary>
    /// Stack that keeps the states of UseNumberOfDigits property.
    /// Used by PushNumberOfDigitsState & PopNumberOfDigitsState methods
    /// </summary>
    private Stack m_useNumberOfDigitsStates;

    //=============================================================================
    /// <summary>
    /// Selected measure for the report item
    /// </summary>
    private pi.IMeasure m_measure;


    #endregion

    #region Properties


    //=============================================================================
    /// <summary>
    /// The currently used measure
    /// </summary>
    public pi.IMeasure Measure
    {
      get
      {
        Debug.Assert(null!=m_measure,"ReportItemInspection.Measure_get - m_measure is NULL");
        return m_measure;
      }
      set
      {
        Debug.Assert(null!=value, "Trying to set Measure property to NULL");
        m_measure = value;
      }
    }

    //=============================================================================
    /// <summary>
    /// Determines if ReportItem has valid value for the current measure (Measure property value)
    /// </summary>
    public virtual bool HasValidValue
    {
      get {
        pi.ISequenceItem si = SequenceItem;
        Debug.Assert(null!=si,"ReportItem::HasValidValue - SequenceItem property has NULL value");
        return si.get_ResultValid(Measure);
      }      
    }

    //=============================================================================
    /// <summary>
    /// Checks if it is possible to get the value from item with base measure
    /// </summary>
    public bool CanGetValue
    {
      get {
        // store currently selected measure
        pi.IMeasure tmpMeasure = Measure;
        Measure = Tools.get_base_measure(PIConnector.instance.active_document); //SPCAddin.PIApp.ActiveDocument.Measures["Master part"];        
        bool res = true;
        try {
          double d = Value;
        } catch (System.Exception) {
          res = false;
        }
        Measure = tmpMeasure;
        return res;
      }
    }

    //=============================================================================
    /// <summary>
    /// Gets value of report item for selected measure
    /// </summary>
    public abstract double Value
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Gets date & time when report item has been measured
    /// </summary>
    public DateTime MeasureDate
    {
      get {
        return SequenceItem.get_MeasureDate(Measure);
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns a unique KEY-string of report item
    /// </summary>
    public virtual string Key
    {
      get
      {
        Debug.Assert(null!=m_sequenceItem,"ReportItem.Key - m_sequenceItem field is NULL");
        return m_sequenceItem.UniqueID.ToString();
      }
    }

    //=============================================================================
    /// <summary>
    /// If value of this property is true, then such properties as .Normal, 
    /// .UpperTolerance, .LowerTolerance, ... etc.
    /// </summary>
    public bool UseNumberOfDigits
    {
      get
      {
        return m_useNumberOfDigits;
      }
      set
      {
        m_useNumberOfDigits = value;
      }
    }

    //=========================================================================
    /// <summary>
    /// Units property. Should be overridden in descendants.
    /// </summary>
    public abstract RIUnits Units
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Units property
    /// </summary>
    public string UnitsStr
    {
      get
      {
        switch(Units) 
        {
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

    //=========================================================================
    /// <summary>
    /// Nominal value
    /// </summary>
    public abstract double Nominal
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Upper measure tolerance
    /// </summary>
    public abstract double UpperTolerance
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Lower measure tolerance
    /// </summary>
    public abstract double LowerTolerance
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Number of digits user want to see in report
    /// </summary>
    public virtual long NumberOfDigits
    {
      get
      {
        return m_numberOfDigits;
      }
    }

    //=========================================================================
    /// <summary>
    /// Sequence item used in ReportItem
    /// </summary>
    public virtual pi.ISequenceItem SequenceItem
    {
      get
      {
        return m_sequenceItem;
      }
      set
      {
        Debug.Assert(false, "Call to ReportItem.SequenceItem set property. It must be overridden.");
      }
    }

    //=============================================================================
    /// <summary>
    /// SequenceGroup, the SequenceItem belongs to.
    /// </summary>
    public virtual pi.ISequenceGroup SequenceGroup
    {
      get
      {
        return m_sequenceGroup;
      }
    }

    //=========================================================================
    /// <summary>
    /// Checks if 'OutputToReport' flag is set for current report item
    /// </summary>
    public virtual bool OutputToReport
    {
      get
      {
        Debug.Assert(null!=SequenceGroup,"ReportItem.OutputToReport: SequenceGroup is NULL");
        return SequenceGroup.OutputToReport;
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns unique report variable name 
    /// </summary>
    public abstract string ReportVariableName
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Returns unique report variable name 
    /// </summary>
    public abstract string ReportVariableNameShort
    {
      get;
    }

    //=========================================================================
    /// <summary>
    /// Returns current ReportItem type. This property must be overridden by derived classes
    /// </summary>
    public abstract SPCReportItemType ReportItemType
    {
      get;
    }


    #endregion

  }
}

