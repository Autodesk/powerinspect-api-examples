//=============================================================================
//
//D ReportItemGDT class - base class for all GD&T's report items
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
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using System.Globalization;
using MSXML2;
using PWIMATHBOXLib;

using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC
{
  /// <summary>
  /// ReportItemGDT class - base class for all GD&T's report items
  /// </summary>
  [
    ComVisible(true),
    ClassInterfaceAttribute(ClassInterfaceType.AutoDual),
    Guid("1BC0AF5F-CB94-42E9-B0C5-1E103B680EED")
  ]
  public abstract class ReportItemGDT : ReportItem
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// Constructor of ReportItemGDT class
    /// </summary>
    /// <param name="aGDTItem"></param>
    /// <param name="aName"></param>
    protected ReportItemGDT(
      pi.ISequenceItem aGDTItem,
      string aName     
    ) : base(aGDTItem)
    {
      m_name = aName;
    }

    //=============================================================================
    /// <summary>
    /// Gets measured value from XMLOuput by using aXPath 
    /// </summary>
    /// <param name="aMeasure">measure to get value of</param>
    /// <param name="aXPath">x-path string</param>
    /// <returns>value of measure</returns>
    protected double GetMeasuredResult(pi.IMeasure aMeasure, string aXPath)
    {
      pi.IGDTItem gdti = SequenceItem as pi.IGDTItem;

      // Get XML data using ActiveX

      MSXML2.DOMDocument xmlDocTmp = PIConnector.instance.power_inspect.CreateLocalObject(
        "MSXML2.DOMDocument"
      ) as MSXML2.DOMDocument;

      if (xmlDocTmp == null) {
        MessageBox.Show(LS.Lc("Can't create MSXML2.DOMDocument object"));
        return 0.0;
      }
      xmlDocTmp.async = false;
      MSXML2.IXMLDOMElement xmlRootTmp = xmlDocTmp.createElement("MyRoot");
      xmlDocTmp.documentElement = xmlRootTmp;
      Object xmlRootWrp = new DispatchWrapper(xmlRootTmp);
      Object measureWrp = new DispatchWrapper(aMeasure);
      gdti.XMLOutput(ref xmlRootWrp,0,measureWrp);
      xmlDocTmp.documentElement = xmlRootTmp.firstChild as MSXML2.IXMLDOMElement;

      // back to .NET

      string xmlContent = xmlDocTmp.xml;

      XmlDocument xmlDoc = new XmlDocument(); // System.Xml document
      xmlDoc.LoadXml(xmlContent);
      XmlElement xmlRoot = xmlDoc.DocumentElement; // root element

      string measureID = ReportItemGDT.GetMeasureXMLID(aMeasure as pi.IMeasure);

      XmlElement xmlGDT = xmlRoot.SelectSingleNode(
        "Results/MeasureResult[@MeasureID='"+measureID+"']/gdt"
      ) as XmlElement;

      XmlAttribute xmlRes = xmlGDT.SelectSingleNode(aXPath) as XmlAttribute;

      return ValueProcessingFunc(ParseDouble(xmlRes.InnerText));

    }

    //=============================================================================
    /// <summary>
    /// Returns result of specific measure of GDT item
    /// </summary>
    /// <param name="aGDTItemXML">an XML element that represents GDT item</param>
    /// <param name="aMeasure"></param>
    /// <returns></returns>
    internal static MSXML2.IXMLDOMElement GetGDTItemXML(
      MSXML2.IXMLDOMElement aGDTItemXML, 
      pi.IMeasure aMeasure
    )
    {
      Debug.Assert(aGDTItemXML!=null, "GetGDTItemXML() : aGDTItemXML == null");
      Debug.Assert(aMeasure!=null, "GetGDTItemXML() : aMeasure == null");
      string measureID = GetMeasureXMLID(aMeasure);
      string gdtXPath = "Results/MeasureResult[@MeasureID='" + measureID + "']/gdt";
                     
      return aGDTItemXML.selectSingleNode(gdtXPath) as MSXML2.IXMLDOMElement;

    }

    //=============================================================================
    /// <summary>
    /// Returns an ID string for specified
    /// </summary>
    /// <param name="aMeasure">Measure, which ID is calculated</param>
    /// <returns>identificator string</returns>
    internal static string GetMeasureXMLID(pi.IMeasure aMeasure)
    {
      // this "_0" has a Visual Basic roots...
      return "IDM" + aMeasure.UniqueID.ToString();
    }


    internal static MSXML2.IXMLDOMElement GetXMLOutput(
      pi.IGDTItem aGDTItem,
      pi.IMeasure aMeasure
    )
    {
      Debug.Assert(aGDTItem!=null, "GetMeasureXMLID() : aGDTItem is null");
      Debug.Assert(aMeasure!=null, "GetMeasureXMLID() : aMeasure is null");

      MSXML2.DOMDocument xmlDoc = aGDTItem.Document.Application.CreateLocalObject(
        "MSXML2.DOMDocument"
      ) as MSXML2.DOMDocument;      
      xmlDoc.async = false;
      MSXML2.IXMLDOMElement xmlRoot = xmlDoc.createElement("MyRoot");
      xmlDoc.documentElement = xmlRoot;

      Object xmlRootWrp = new DispatchWrapper(xmlRoot);
      Object measureWrp = new DispatchWrapper(aMeasure);

      aGDTItem.XMLOutput(ref xmlRootWrp, 0, measureWrp);

      xmlDoc.documentElement = xmlRoot.firstChild as MSXML2.IXMLDOMElement;
      
      return xmlDoc.documentElement;
    }

    //=========================================================================
    /// <summary>
    /// Converts a string to a double in a culture-independent manner.
    /// </summary>
    /// <param name="xml_string">String containing a double</param>
    /// <returns>The converted double</returns>
    internal static double ParseDouble(string xml_string)
    {
      return Convert.ToDouble(xml_string.Replace(",", "."), CultureInfo.InvariantCulture);
    }

    #endregion

    #region Fields

    //=============================================================================
    /// <summary>
    /// Name or report item
    /// </summary>
    protected string m_name;

    #endregion

    #region Properties

    //=========================================================================
    /// <summary>
    /// Returns measurement units
    /// </summary>
    public override RIUnits Units
    {
      get
      {
        pi.IPIDocument doc = SequenceItem.Document;
        switch(doc.Units) 
        {
          case PWIMATHBOXLib.PWI_Units.units_INCHES:
            return RIUnits.RIU_INCHES;
          case PWIMATHBOXLib.PWI_Units.units_MILLIMETRES:
            return RIUnits.RIU_MILLIMETRES;
          default:
            Debug.Write("ReportItemGDTDimention.Units: SequenceItem.Document.Units is incorrect");
            return RIUnits.RIU_DIMENTIONLESS;
        }
      }
    }

    //=============================================================================
    /// <summary>
    /// Returns number of digits
    /// </summary>
    public override long NumberOfDigits
    {
      get
      {
        return SequenceItem.Document.NumberOfDigits;
      }
    }

    //=========================================================================
    /// <summary>
    /// Determine if ReportItem has valid value for the current measure
    /// </summary>
    public override bool HasValidValue
    {
      get
      {
        Debug.Assert(null != Measure, "ReportItemGeometric::HasValidValue() - Measure property is NULL");
        return SequenceItem.get_ResultValid(Measure);
      }
    }

    #endregion

  } // class ReportItemGDT

} // end namespace Autodesk.PowerInspect.AddIns.SPC

