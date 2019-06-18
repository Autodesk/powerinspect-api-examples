//=============================================================================
//
//D ReportItemProperty1D class.
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
  /// ReportItemPropery class - represents simple properties.
  /// </summary>
  [
  ComVisible(true),
  ClassInterface(ClassInterfaceType.AutoDual),
  GuidAttribute("1d56d792-f04f-43b0-b4a6-fa20ba89fdd5")
  ]
  public class ReportItemProperty1D : ReportItemProperty //, IReportItemProperty
  {

    #region Methods

    //=========================================================================
    /// <summary>
    /// ReportItemProperty1D class constructor
    /// </summary>
    public ReportItemProperty1D(
      pi.IGeometricItem aGeometricItem,
      int aPropertyIndex
    ) : base(aGeometricItem,aPropertyIndex)
    {
    }


    #endregion

    #region Properties

    //=============================================================================
    /// <summary>
    /// Gets the value of report item for the current measure.
    /// </summary>
    /// <returns>Value</returns>
    public override double Value
    {
      get
      {
        Debug.Assert(Measure!=null, "ReportItemProperty1D.Value() :: Measure property is null");
        double dVal = 0.0;
        switch (Property.PropertyType) {
          case pi.PWI_PropertyType.pwi_property_Angle: {
            dVal = (Property as pi.IPropertyAngle).get_Measured(Measure);
            break;
          }
          case pi.PWI_PropertyType.pwi_property_LinearDimension: {
            dVal = (Property as pi.IPropertyLinearDimension).get_Measured(Measure);
            break;
            }
          case pi.PWI_PropertyType.pwi_property_LinearSlotDimension: {
            if (Property is pi.IPropertyLinearSlotWidthDimension) {
              dVal = (Property as pi.IPropertyLinearSlotWidthDimension).get_Measured(Measure);
            } else if (Property is pi.IPropertyLinearSlotDimension) {
              dVal = (Property as pi.IPropertyLinearSlotDimension).get_Measured(Measure);
            } else {
              Debug.Fail("Property not supported");
            }
            break;
          }
          case pi.PWI_PropertyType.pwi_property_RadialDimension: {
            dVal = (Property as pi.IPropertyRadialDimension).get_Measured(Measure);
            break;
          }
          case pi.PWI_PropertyType.pwi_property_GeometricTolerance: {
            dVal = (Property as pi.IPropertyGeometricTolerance).get_Measured(Measure);
            break;
          }
          case pi.PWI_PropertyType.pwi_property_MaxDeviation: {
              dVal = (Property as pi.IPropertyMaxDeviation).get_Measured(Measure);
              break;
          }
          case pi.PWI_PropertyType.pwi_property_UserAssisted: {
            dVal = (Property as pi.IPropertyUserAssisted).get_ValueForMeasure(Measure);
            break;
          }
          default: {
            Debug.Fail("ReportItemProperty::Value() - switch enumeration value is undefined");
            break;
          }
        }
        return ValueProcessingFunc(dVal);
      }
    } // end Value

    //=========================================================================
    /// <summary>
    /// Checks if 'OutputToReport' flag is set for current report item
    /// </summary>
    public override bool OutputToReport
    {
      get
      {
        Debug.Assert(Property!=null,"ReportItemProperty1D.OutputToReport :: ReportItemProperty1D.Property property is null");
        return (base.OutputToReport && Property.OutputToReport);
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
        Debug.Assert(SequenceItem!=null, "ReportItemProperty1D.ReportVariableName :: SequenceItem is null");
        Debug.Assert(Property!=null, "ReportItemProperty1D.ReportVariableName :: Property is null");
        pi.ISequenceGroup sg = SequenceItem.Group;
        Debug.Assert(sg!=null, "ReportItemProperty1D.ReportVariableName :: SequenceItem.Group is null");
        return (
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
        return Property.Name;
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
        Debug.Assert(Property!=null, "Trying to get ReportItemProperty1D.Nominal property value, while ReportItemProperty1D.Property is null");

        // Get the actual property from the common class and extract nominal from it
        Debug.Assert(Property.PropertyType == pi.PWI_PropertyType.pwi_property_Angle ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_LinearDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_LinearSlotDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_RadialDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_GeometricTolerance ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_MaxDeviation ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UserAssisted,
          "Wrong type of property");

        // This is the default value for IPropertyGeometricTolerance
        double dVal = 0.0;
        pi.IProperty1 prop1 = Property as pi.IProperty1;
        if (null != prop1) {
          dVal = prop1.Nominal;
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
        Debug.Assert(Property!=null, "Trying to get ReportItemProperty1D.UpperTolerance property value, whlie ReportItemProperty1D.Property is null");

        // Get the actual property from the common class and extract UpperTol from it
        Debug.Assert(Property.PropertyType == pi.PWI_PropertyType.pwi_property_Angle ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_LinearDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_LinearSlotDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_RadialDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_GeometricTolerance ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_MaxDeviation ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UserAssisted,
          "Wrong type of property");

        // This is the default value for IPropertyGeometricTolerance
        double dVal = 0.0;
        pi.IProperty1 prop1 = Property as pi.IProperty1;
        if (null != prop1) {
          dVal = prop1.UpperTol;
        }
        return ValueProcessingFunc(dVal);
      }
    }

    //=========================================================================
    /// <summary>
    /// Lower tolerance of the measure
    /// </summary>
    public override double LowerTolerance
    {
      get
      {
        Debug.Assert(Property!=null, "Trying to get ReportItemProperty1D.LowerTolerance property value, while ReportItemProperty1D.Property is null");
        // Get the actual property from the common class and extract LowerTol from it
        Debug.Assert(Property.PropertyType == pi.PWI_PropertyType.pwi_property_Angle ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_LinearDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_LinearSlotDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_RadialDimension ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_GeometricTolerance ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_MaxDeviation ||
          Property.PropertyType == pi.PWI_PropertyType.pwi_property_UserAssisted,
          "Wrong type of property");

        // This is the default value for IPropertyGeometricTolerance
        double dVal = 0.0;
        pi.IProperty1 prop1 = Property as pi.IProperty1;
        if (null != prop1) {
          dVal = prop1.LowerTol;
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
        return SPCReportItemType.RIT_GEOMETRIC_PROPERTY_1D;
      }
    }
    
    
    #endregion

  } // end class ReportItemProperty1D

} // end namespace Autodesk.PowerInspect.AddIns.SPC

