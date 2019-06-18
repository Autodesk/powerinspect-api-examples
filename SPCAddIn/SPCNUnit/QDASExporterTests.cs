//=============================================================================
//D QDASExporterTests.cs
//
// QDAS Export unit test
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
using System.Linq;
using NUnit.Framework;
using Autodesk.PowerInspect.AddIns.SPC.Tests.Mocks;
using System.IO;

namespace Autodesk.PowerInspect.AddIns.SPC.Tests
{
  [TestFixture]
  public class QDASExporterTests
  {
    //=========================================================================
    /// <summary>
    /// Action performed before the test
    /// </summary>
    [OneTimeSetUp]
    public void SetUpTest()
    {
      Console.WriteLine("NUnit test of SPC/QDAS start");
      Console.WriteLine("Running the set up for the test");

      var dll_path = new System.Uri(
        System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase).LocalPath;
      var bin_path = Path.GetDirectoryName(dll_path);
      m_output_path = Path.Combine(bin_path, "output");

      if (!Directory.Exists(m_output_path)) {
        Directory.CreateDirectory(m_output_path);
      }
    }

    //=========================================================================
    /// <summary>
    /// Action performed after the test
    /// </summary>
    [OneTimeTearDown]
    public void TearDownTest()
    {
      Console.WriteLine("Test of SPC/QDAS Done");
    }

    //=========================================================================
    /// <summary>
    /// Tests for proper export format for German locale
    /// (as a typical use case)
    /// </summary>
    [Test]
    [SetCulture("de-DE")]
    public void DateTimeFormatTest()
    {
      // Generate output file names
      string dfd_file = Path.Combine(m_output_path, "Part0100000001.DFD");
      string dfx_file = Path.Combine(m_output_path, "Part0100000001.DFX");

      // Make sure they don't exist
      new List<string> { dfd_file, dfx_file }.ForEach((fn) => {
        if (File.Exists(fn)) {
          File.Delete(fn);
        }
      });

      MeasureCollection measures = new MeasureCollection();
      measures.Add(new PIMeasureMock());

      IReportItem item1 = new ReportItemMock(
        "Variable1", "Var1", RIUnits.RIU_MILLIMETRES, 5.0, 5.05, -0.1, 0.1, 3,
        new DateTime(2014,10,15,23,56,58)
      );
      ReportItemCollection items = new ReportItemCollection();
      items.Add(item1);

      ExportSettingsQDAS settings = new ExportSettingsQDAS();
      settings.output_directory = m_output_path;
      settings.part_description = "Part description";
      settings.part_number = "Part01";
      settings.date_format = "DD.MM.YYYY";
      settings.time_format = "HH:MM:SS";

      StateKeeper state_keeper = new StateKeeper();

      SPCExporter exporter = ExporterFactory.create(
        SPCExportType.SPC_ET_QDAS,
        settings,
        measures,
        items,
        state_keeper
      );

      bool export_status = exporter.Export();
      Assert.True(export_status, "QDAS exporter failure");

      // Parse output files
      string[] dfd_lines = File.ReadAllLines(dfd_file);
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K0100 1"), "Expected K0100");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K1001 Part01"), "Expected K1001");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K1002 Part description"), "Expected K1002");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2002/1 Variable1"), "Expected K2002");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2003/1 Var1"), "Expected K2003");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2004/1 0"), "Expected K2004");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2022/1 3"), "Expected K2022");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2101/1 5.000"), "Expected K2101");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2110/1 4.900"), "Expected K2110");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2111/1 5.100"), "Expected K2111");
      Assert.AreEqual(1, dfd_lines.Count(s => s == "K2142/1 mm"), "Expected K2142");

      string[] dfx_lines = File.ReadAllLines(dfx_file);
      Assert.AreEqual(1, dfx_lines.Count(s => s == "K0001 5.050"), "Expected K0001");
      Assert.AreEqual(1, dfx_lines.Count(s => s == "K0004 15.10.2014/23:56:58"), "Expected K0004");
    }

    /// <summary> Output folder </summary>
    private string m_output_path;
  }
}
