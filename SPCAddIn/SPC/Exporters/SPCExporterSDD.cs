//=============================================================================
//
//D SPCExporterSDD class
//
// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
//------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

using pi = PowerINSPECT;
using System.Diagnostics;


namespace Autodesk.PowerInspect.AddIns.SPC
{
  //=============================================================================
  /// <summary>
  /// SPCExporterSDD class - implements exporter to SDD format
  /// </summary>
  [
    Guid("1EA17C55-6AA6-457E-9294-F5040C24211D"),
    ClassInterface(ClassInterfaceType.None),
    ComVisible(true)
  ]
  public sealed class SPCExporterSDD : SPCExporter, ISPCExporter
  {

    #region Methods

    //=============================================================================
    /// <summary>
    /// SPCExporterSDD class constructor
    /// </summary>
    /// <param name="aFileName">Destination file name</param>
    /// <param name="aMeasures">Measures to export</param></param>
    /// <param name="aReportItems">Report items (variables / characteristics)</param>
    internal SPCExporterSDD(
      ExportSettingsSDD a_export_settings,
      MeasureCollection a_measures,
      ReportItemCollection a_report_items
    ) : base(a_measures, a_report_items)
    {
      export_settings = a_export_settings;
      m_instanceNum++;
    }

    //==========================================================================
    /// <summary>
    /// Do export to SDD
    /// </summary>
    /// <returns>True - if data was successfully exported</returns>
    public override bool Export()
    {
      // Create SDD writer using late binding 
      // (because this DLL is not always installed on user's machine)

      try {
        Type tAcXSDDWriter = Type.GetTypeFromProgID("AcXSDDWriter.AxCSDDWriter");
        Object sddWriter = Activator.CreateInstance(tAcXSDDWriter);
        string part = PIConnector.instance.active_document.Name;
        string operation = "PI Inspection";

        ///////////////////////////////////////////////////////////////////////////
        // Invoke AddSDDFile method
        ///////////////////////////////////////////////////////////////////////////
        string sddFile = DynamicInvoke(tAcXSDDWriter, sddWriter, "AddSDDFile",
          part + "~" + operation
        ).ToString();

        ///////////////////////////////////////////////////////////////////////////        
        // Invoke WriteHeader method
        ///////////////////////////////////////////////////////////////////////////
        DynamicInvoke(tAcXSDDWriter, sddWriter, "WriteHeader", 
          sddFile, // SDD's ID of the file
          "8", // text SDD file, according to documentation
          part,// Part name
          operation, // Operation name
          m_instanceNum.ToString(), // Instance number string
          Convert.ToInt16(m_reportItems.Count), // Count of report items
          0,
          null,
          null,
          null,
          0          
        );

        ///////////////////////////////////////////////////////////////////////////        
        // Invoke WriteFileDefn method
        ///////////////////////////////////////////////////////////////////////////
        DynamicInvoke(tAcXSDDWriter, sddWriter, "WriteFileDefn", 
          sddFile,
          1, // X, moving R study
          0, // no study
          0,
          1, // by sample
          null,
          null,
          0,
          0,
          0,
          0,
          null
        );


        // Output information item by item
        foreach (IReportItem ri in m_reportItems) 
        {
          //////////////////////////////////////////////////////////////////////////
          // Invoke WriteVarDefnSection method
          //////////////////////////////////////////////////////////////////////////
          DynamicInvoke(tAcXSDDWriter,sddWriter,"WriteVarDefnSection",
            sddFile, // SDD's ID of the file
            4, // Subgroup size; recommended value for XMR study between 3 and 5
            8, // Specify nominal and +/- tolerance
            ri.ReportVariableName, // Variable name
            Convert.ToInt16(ri.NumberOfDigits), // Precision
            ri.UnitsStr, // Units
            0, // Preset
            1, // Multiplier
            ri.Nominal, // Nominal value
            Math.Abs(ri.UpperTolerance), // Upper tolerance
            Math.Abs(ri.LowerTolerance), // Lower tolerance
            null // Prompt string
          );

        }

        //////////////////////////////////////////////////////////////////////////
        // Invoke WriteDefinitionEnd
        // Tell that we have finished writing definitions
        //////////////////////////////////////////////////////////////////////////
        DynamicInvoke(tAcXSDDWriter, sddWriter, "WriteDefinitionEnd",
          sddFile
        );

        //////////////////////////////////////////////////////////////////////////
        // Invoke WriteSampleDataSection
        // And now we are ready to write real samples
        //////////////////////////////////////////////////////////////////////////
        DynamicInvoke(tAcXSDDWriter, sddWriter, "WriteSampleDataSection",
          sddFile
        );

        // define data array
        double[] data = {0};
        
        foreach (pi.IMeasure measure in m_measures) 
        {
          // for each data row
          //////////////////////////////////////////////////////////////////////////
          // Invoke WriteSampleSection
          //////////////////////////////////////////////////////////////////////////          
          DynamicInvoke(tAcXSDDWriter,sddWriter,"WriteSampleSection",
            sddFile,data,null,null,null
          );

          foreach (IReportItem ri in m_reportItems) 
          {
            ri.Measure = measure;
            // for each data column
            if (!ri.HasValidValue) 
            {
              MessageBox.Show(
                String.Format(
                LS.Lc("The item {0} is not valid for the measure {1}"),
                ri.SequenceItem.Name,
                measure.Name
                ),
                LS.Lc("Attention"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
              return false;
            }

            //////////////////////////////////////////////////////////////////////////
            // Invoke WriteVarSampleData
            //////////////////////////////////////////////////////////////////////////            
            DynamicInvoke(tAcXSDDWriter,sddWriter,"WriteVarSampleData",
              sddFile,
              ri.ReportVariableName,
              ri.Value,
              1
            );
          }
        }

        // split full file name to path & filename
        string sPath = export_settings.output_folder;
        string sFileName = export_settings.output_file_name_base;

        // rename temporary file
        DynamicInvoke(tAcXSDDWriter,sddWriter,"RenameSDDFile",
          sddFile,sPath,sFileName
        );

        // delete temporary file
        DynamicInvoke(tAcXSDDWriter,sddWriter,"RemoveSDDFile",
          sddFile
        );
  
        sddWriter = null;

      } catch (Exception) {
        MessageBox.Show(
          LS.Lc("AcxSDDWrite ActiveX object is not installed or registered properly.\n")+
          LS.Lc("It is usually contained in AcxSDDWriter.dll.\n")+
          LS.Lc("Please, contact Autodesk for assistance.")
          );   
        return false;
      }
      return true;
      
    }

    //=============================================================================
    /// <summary>
    /// Invokes Method from DLL with custom arguments
    /// </summary>
    /// <param name="aMethodName">Name of the method</param>
    /// <param name="aMethodArgs">Arguments of the method</param>
    /// <returns></returns>
    private Object DynamicInvoke(Type aType, Object aInstance, string aMethodName,  params Object[] aMethodArgs)
    {      
      return aType.InvokeMember(aMethodName,BindingFlags.InvokeMethod,null,aInstance,aMethodArgs);
    }

    #endregion

    #region Fields

    private static int m_instanceNum = 0;

    private ExportSettingsSDD m_export_settings;

    #endregion

    #region Properties

    public ExportSettingsSDD export_settings
    {
      get
      {
        return m_export_settings;
      }
      set
      {
        Debug.Assert(null != value, "Export settings can't be set to null");
        // set only if value is not null
        if (null != value) {
          m_export_settings = value;
        }
      }
    }
    #endregion


  }
}

