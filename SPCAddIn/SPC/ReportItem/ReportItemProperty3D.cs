//=============================================================================
//
//D ReportItemProperty3D class.
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
using pwiCOMGeometry;

namespace Autodesk.PowerInspect.AddIns.SPC
{

  //=========================================================================
  /// <summary>
  /// ReportItemProperty3D class - represents "three dimensional" properties (point3d, vector3d, etc...)
  /// </summary>
  [
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("4d9bd1ee-924b-4296-bf1f-c7c60fa7b7db")
  ]
  public class ReportItemProperty3D : ReportItemProperty //, IReportItemProperty
  {
    
    #region Methods

    //=========================================================================
    /// <summary>
    /// ReportItemProperty3D class constructor
    /// </summary>
    public ReportItemProperty3D(
      pi.IGeometricItem aGeometricItem,
      int aPropertyIndex,
      int aCoordinateIndex
    ) : base(aGeometricItem,aPropertyIndex)
    {
      m_coordinateIndex = aCoordinateIndex;      
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// Current coordinate index
    /// </summary>
    private int m_coordinateIndex;

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Gets the value of the report item the current measure
    /// </summary>
    /// <returns>Value of the ReportItem</returns>
    public override double Value
    {
      get
      {      
        Debug.Assert(Measure!=null, "ReportItemProperty3D.Value() :: Measure property is NULL");

        double dVal = 0.0;

        switch (Property.PropertyType)
        {
          case pi.PWI_PropertyType.pwi_property_Point3D:
            dVal = (Property as pi.IPropertyPoint3D).get_Measured((int)m_coordinateIndex,Measure);
            break;
          case pi.PWI_PropertyType.pwi_property_Vector3D:
            dVal = (Property as pi.IPropertyVector3D).get_Measured((int)m_coordinateIndex,Measure);
            break;
          case pi.PWI_PropertyType.pwi_property_UnitVector3D:
            dVal = (Property as pi.IPropertyUnitVector3D).get_Measured((int)m_coordinateIndex,Measure);
            break;
        }
        return ValueProcessingFunc(dVal);
      }
    } // end Value


    //=============================================================================
    /// <summary>
    /// Returns a unique KEY-string of report item
    /// </summary>
    public override string Key
    {
      get
      {
        return base.Key + "_" + m_coordinateIndex.ToString();
      }
    }

    //=========================================================================
    /// <summary>
    /// Checks if 'OutputToReport' flag is set for current report item
    /// </summary>
    public override bool OutputToReport
    {
      get
      {
        Debug.Assert(Property!=null,"ReportItemProperty.OutputToReport: ReportItemProperty.Property property is null");

        return (base.OutputToReport && Property.OutputToReport && (Property as pi.IProperty3).get_OutputCoordToReport((int)m_coordinateIndex));
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
        Debug.Assert(SequenceItem!=null, "ReportItemProperty3D.ReportVariableName :: SequenceItem is null");
        Debug.Assert(Property!=null, "ReportItemProperty3D.ReportVariableName :: Property is null");
        pi.ISequenceGroup sg = SequenceItem.Group;
        Debug.Assert(sg!=null, "ReportItemProperty3D.ReportVariableName :: SequenceItem.Group is null");


        ICoordFormat cf = null;

        Debug.Assert(
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Point3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Vector3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UnitVector3D,
          "ReportItemProperty3D.Property.PropertyType has wrong type!!!"
        );

        switch(Property.PropertyType)
        {
          case pi.PWI_PropertyType.pwi_property_Point3D:
            cf = (Property as pi.IPropertyPoint3D).CoordinateType;
            break;
          case pi.PWI_PropertyType.pwi_property_Vector3D:
            cf = (Property as pi.IPropertyVector3D).CoordinateType;
            break;
          case pi.PWI_PropertyType.pwi_property_UnitVector3D:
            cf = (Property as pi.IPropertyUnitVector3D).CoordinateType;
            break;
        }

        return (
          cf.get_Prefix((int)m_coordinateIndex) + ReportItem.spc_VAR_NAME_DELIMITER +
          Property.Name + ReportItem.spc_VAR_NAME_DELIMITER +
          SequenceItem.Name + ReportItem.spc_VAR_NAME_DELIMITER +
          sg.Name
        );
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
        Debug.Assert(SequenceItem!=null, "ReportItemProperty3D.ReportVariableName :: SequenceItem is null");
        Debug.Assert(Property!=null, "ReportItemProperty3D.ReportVariableName :: Property is null");
        pi.ISequenceGroup sg = SequenceItem.Group;
        Debug.Assert(sg!=null, "ReportItemProperty3D.ReportVariableName :: SequenceItem.Group is null");


        ICoordFormat cf = null;

        Debug.Assert(
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Point3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Vector3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UnitVector3D,
          "ReportItemProperty3D.Property.PropertyType has wrong type!!!"
          );

        switch(Property.PropertyType)
        {
          case pi.PWI_PropertyType.pwi_property_Point3D:
            cf = (Property as pi.IPropertyPoint3D).CoordinateType;
            break;
          case pi.PWI_PropertyType.pwi_property_Vector3D:
            cf = (Property as pi.IPropertyVector3D).CoordinateType;
            break;
          case pi.PWI_PropertyType.pwi_property_UnitVector3D:
            cf = (Property as pi.IPropertyUnitVector3D).CoordinateType;
            break;
        }

        return cf.get_Prefix((int)m_coordinateIndex);

      }
    }

    //=========================================================================
    /// <summary>
    /// Nominal value
    /// </summary>
    public override double Nominal
    {
      get
      {
        Debug.Assert(Property!=null, "Trying to get ReportItemProperty3D.Nominal property value, while ReportItemProperty.Property is null");
        

        // Get the actual property from the common class and extract Nominal from it
        Debug.Assert(Property.PropertyType == pi.PWI_PropertyType.pwi_property_Point3D||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Vector3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UnitVector3D,
          "Wrong type of property");

        // This is the default value for IPropertyGeometricTolerance
        double dVal = 0.0;
        pi.IProperty3 prop3 = Property as pi.IProperty3;
        if (null != prop3) {
          dVal = prop3.get_Nominal(m_coordinateIndex);
        }
        return ValueProcessingFunc(dVal);
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
        Debug.Assert(Property!=null, "Trying to get ReportItemProperty3D.UpperTolerance property value, while ReportItemProperty.Property is null");
        // Get the actual property from the common class and extract UpperTolerance from it
        Debug.Assert(Property.PropertyType == pi.PWI_PropertyType.pwi_property_Point3D||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Vector3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UnitVector3D,
          "Wrong type of property");

        // This is the default value for IPropertyGeometricTolerance
        double dVal = 0.0;
        pi.IProperty3 prop3 = Property as pi.IProperty3;
        if (null != prop3) {
          dVal = prop3.get_UpperTol(m_coordinateIndex);
        }
        return ValueProcessingFunc(dVal);
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
        Debug.Assert(Property!=null, "Trying to get ReportItemProperty.LowerTolerance property value, while ReportItemProperty.Property is null");
        // Get the actual property from the common class and extract LowerTolerance from it
        Debug.Assert(Property.PropertyType == pi.PWI_PropertyType.pwi_property_Point3D||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_Vector3D ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UnitVector3D,
          "Wrong type of property");

        // This is the default value for IPropertyGeometricTolerance
        double dVal = 0.0;
        pi.IProperty3 prop3 = Property as pi.IProperty3;
        if (null != prop3) {
          dVal = prop3.get_LowerTol(m_coordinateIndex);
        }
        return ValueProcessingFunc(dVal);
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns current ReportItem type
    /// </summary>
    public override SPCReportItemType ReportItemType
    {
      get
      {
        return SPCReportItemType.RIT_GEOMETRIC_PROPERTY_3D;
      }
    }
    
    #endregion

  } // end class ReportItemProperty3D

} // end namespace Autodesk.PowerInspect.AddIns.SPC

