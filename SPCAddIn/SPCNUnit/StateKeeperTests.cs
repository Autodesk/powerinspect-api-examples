// ----------------------------------------------------------------------------
// Copyright 2019 Autodesk, Inc. All rights reserved.
//
// Use of this software is subject to the terms of the Autodesk license 
// agreement provided at the time of installation or download, or which 
// otherwise accompanies this software in either electronic or hard copy form.
// ----------------------------------------------------------------------------
//
// ----------------------------------------------------------------------------

using System;
using System.IO;
using NUnit.Framework;


namespace Autodesk.PowerInspect.AddIns.SPC.Tests
{
  [TestFixture]
  /// <summary>
  /// This is the nunit test for the Vector3D class
  /// </summary>
  public class StateKeeperTests
  {    

    /// <summary>The default constructor of the class</summary>
    public StateKeeperTests()
    {
    }

    /// <summary>
    /// Action performed before the test
    /// </summary>
    [OneTimeSetUp]  
    public void SetUpTest()
    {
      Console.WriteLine("NUnit test of SPC/StateKeeper start");
      Console.WriteLine("Running the set up for the test");
    }

    /// <summary>
    /// Action performed after the test
    /// </summary>
    [OneTimeTearDown]
    //=========================================================================
    public void TearDownTest()
    {
      Console.WriteLine("Test of SPC/StateKeeper Done");
    }
    
    /// <summary>
    /// Test that StateKeeper uses proper encoding for streams
    /// </summary>
    [Test]
    [SetCulture("ru-RU")]
    //=========================================================================
    public void EncodingTest()
    {
      StateKeeper sk = new StateKeeper();
      string tmp_file = Path.GetTempFileName();
      sk.init(tmp_file);

      ExportSettingsCSV settings_csv = new ExportSettingsCSV();

      string original_fn = "Òåñò.cvs";

      settings_csv.output_file_name = original_fn;

      bool save_status = settings_csv.save_to_state_keeper(sk);

      Assert.IsTrue(save_status, "Problem saving to state keeper");

      sk.save();

      sk.load();

      ExportSettingsCSV other_settings_csv_ = new ExportSettingsCSV();

      bool load_status = other_settings_csv_.load_from_state_keeper(sk);

      Assert.IsTrue(load_status, "Problem loading from state keeper");

      string read_fn = other_settings_csv_.output_file_name;

      Assert.AreEqual(
        original_fn, 
        read_fn, 
        "StateKeeper has broken the string encoding"
      );
    }
  }
}

