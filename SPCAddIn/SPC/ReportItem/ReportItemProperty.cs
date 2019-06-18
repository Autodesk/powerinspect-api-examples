//=============================================================================
//
//D ReportItemProperty class
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
using PWIMATHBOXLib;

// Aliases
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  /// <summary>
  /// ReportItemProperty - base class for all report items that represents properties
  /// </summary>
  [
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("bb02f134-d97d-40bf-98a5-1ede495cbc03")
  ]
  public abstract class ReportItemProperty : ReportItem //, IReportItemProperty
  {
    #region Methods

    protected ReportItemProperty(
      pi.IGeometricItem aGeometricItem,
      int aPropertyIndex
    ) : base(aGeometricItem as pi.ISequenceItem)
    {
      m_propertyIndex = aPropertyIndex;
      m_property = GetPropertyByIndex(aGeometricItem,m_propertyIndex);
      m_numberOfDigits = GetNumberOfDigits(aGeometricItem, m_property);
    }

    ~ReportItemProperty()
    {
      m_property = null;
    }

    //=========================================================================
    /// <summary>
    /// Returns property by it's index
    /// </summary>
    /// <param name="aSequenceItem">SequenceItem instance. 'Properties' property used here.</param>
    /// <param name="aPropertyIndex">index of property in aSequenceItem.Properties[] collection</param>
    /// <returns>Property object</returns>
    protected pi.IProperty GetPropertyByIndex(pi.ISequenceItem aSequenceItem, long aPropertyIndex)
    {
      Debug.Assert(aSequenceItem != null,"aSequenceItem not defined");
      return aSequenceItem.Properties[aPropertyIndex];      
    } // end GetPropertyByIndex()

    //=========================================================================
    /// <summary>
    /// Returns number of digits of value representation when it goes to report
    /// </summary>
    /// <param name="aSequenceItem"></param>
    /// <param name="aProperty"></param>
    /// <returns></returns>
    protected int GetNumberOfDigits(pi.ISequenceItem aSequenceItem, pi.IProperty aProperty)
    {
      Debug.Assert(aSequenceItem!=null, "aSequenceItem object don't exists");
      Debug.Assert(aProperty!=null, "aProperty object don't exists");

      pi.IPIDocument doc = aSequenceItem.Document;
      switch (aProperty.PropertyType)
      {
        case pi.PWI_PropertyType.pwi_property_Point3D:
        case pi.PWI_PropertyType.pwi_property_LinearDimension:
        case pi.PWI_PropertyType.pwi_property_LinearSlotDimension:
        case pi.PWI_PropertyType.pwi_property_RadialDimension:
        case pi.PWI_PropertyType.pwi_property_GeometricTolerance:
        case pi.PWI_PropertyType.pwi_property_MaxDeviation:
          return doc.NumberOfDigits;
        case pi.PWI_PropertyType.pwi_property_Angle:
          return doc.NumberOfDigitsAngle;
        case pi.PWI_PropertyType.pwi_property_Vector3D:
        case pi.PWI_PropertyType.pwi_property_UnitVector3D:
        case pi.PWI_PropertyType.pwi_property_UserAssisted:
          return doc.NumberOfDigitsNeutral;
        default: {
          Debug.Fail("ReportItem.GetNumberOfDigits() :: aProperty.PropertyType remain undefined");
          return doc.NumberOfDigitsNeutral;
        }
      }      
    } // end GetNumberOfDigits()

    #endregion

    #region Fields

    //=========================================================================
    /// <summary>
    /// Index of property
    /// </summary>
    protected int m_propertyIndex;

    //=========================================================================
    /// <summary>
    /// Property itself
    /// </summary>
    protected pi.IProperty m_property;

    #endregion

    #region Properties

    //=============================================================================
    /// <summary>
    /// Returns a unique KEY-string of report item (IGeometricItem)
    /// </summary>
    public override string Key
    {
      get
      {
        return base.Key + "_" + m_propertyIndex;
      }
    }

    //=========================================================================
    /// <summary>
    /// Property's index
    /// </summary>
    public long PropertyIndex
    {
      get
      {
        return m_propertyIndex;
      }
    }

    //=========================================================================
    /// <summary>
    /// This method used by SPC to determine of sequence item's property has 
    /// value. Sometimes some properties might have value ans in the same time 
    /// other properties might not have one - for example:
    /// property#1 - first intersection point of circle and line
    /// property#1 - second intersection point of circle and line
    /// 
    /// in this case there can be no intersection points, thus properties 
    /// won't have values. There can be only one intersection point - when line 
    /// touches circle.
    /// Circle might be intersected by line at two points.
    /// </summary>
    public override bool  HasValidValue
    {
      get  {
        return Property.get_HasValue(Measure);
      }
    }

    //=========================================================================
    /// <summary>
    /// Allows direct access to property through IProperty interface
    /// </summary>
    public virtual pi.IProperty Property
    {
      get
      {
        return m_property;
      }
    }

    //=========================================================================
    /// <summary>
    /// Sequence item used in ReportItem
    /// </summary>
    public override pi.ISequenceItem SequenceItem
    {
      get
      {
        return base.SequenceItem;
      }
      set
      {
        Debug.Assert(value!=null, "Trying to set ReportItem.SequenceItem property to null value");
        if (value==null) return;
        m_sequenceItem = value;
        m_property = GetPropertyByIndex(m_sequenceItem,m_propertyIndex);
        m_numberOfDigits = GetNumberOfDigits(m_sequenceItem, m_property);
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
        Debug.Assert(SequenceItem!=null, "ReportItemProperty.Units - SequenceItem is NULL");
        Debug.Assert(Property!=null, "ReportItemProperty.Units - Property is NULL");

        switch(Property.PropertyType)
        {
          case pi.PWI_PropertyType.pwi_property_Point3D:
          case pi.PWI_PropertyType.pwi_property_LinearDimension:
          case pi.PWI_PropertyType.pwi_property_LinearSlotDimension:
          case pi.PWI_PropertyType.pwi_property_RadialDimension:
          case pi.PWI_PropertyType.pwi_property_GeometricTolerance:
          {
            pi.IPIDocument doc = SequenceItem.Document;
            switch (doc.Units)
            {
              case PWI_Units.units_MILLIMETRES:
                return RIUnits.RIU_MILLIMETRES;
              case PWI_Units.units_INCHES:
                return RIUnits.RIU_INCHES;
              default:
                Debug.Assert(false,"Unknown SequenceItem.Document.Units type");
                break;
            }
          }
            break;
          case pi.PWI_PropertyType.pwi_property_Angle:
            return RIUnits.RIU_DEGREE;
          case pi.PWI_PropertyType.pwi_property_Vector3D:
          case pi.PWI_PropertyType.pwi_property_UnitVector3D:
          case pi.PWI_PropertyType.pwi_property_UserAssisted:
            return RIUnits.RIU_DIMENTIONLESS;
        }
        Debug.Assert(false,"ReportItemProperty.Units - Property.PropertyType is incorrect");
        return RIUnits.RIU_DIMENTIONLESS;
      }
    }

    #endregion
  }
}

