//=============================================================================
//D PIMeasureMock.cs
//
// A very basic mock class for IMeasure interface.
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
using System.Text;
using System.Threading.Tasks;
using pi = PowerINSPECT;

namespace Autodesk.PowerInspect.AddIns.SPC.Tests.Mocks
{
  public class PIMeasureMock : pi.IMeasure
  {
    //=========================================================================
    /// <summary>
    /// Active alignment item
    /// </summary>
    public pi.ISequenceItem ActiveAlignment
    {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    //=========================================================================
    /// <summary>
    /// Apply shrink factor
    /// </summary>
    /// <param name="factor">Shrink factor</param>
    public void ApplyShrinkFactor(double factor)
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Apply temperature compensation
    /// </summary>
    /// <param name="Temperature">Temperature</param>
    /// <param name="coeff">Coefficient</param>
    public void ApplyTemperatureCompensation(double Temperature, double coeff)
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Copy report variables from other measure
    /// </summary>
    /// <param name="from_measure"></param>
    public void CopyReportVariables(pi.measure from_measure)
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Deletes measurement
    /// </summary>
    public void Delete()
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Document reference
    /// </summary>
    public pi.Document Document
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Expansion coefficient
    /// </summary>
    public double ExpansionCoefficient
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Imports data from other measurement
    /// </summary>
    /// <param name="source_meas"></param>
    public void ImportMeasuredData(pi.measure source_meas)
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Invalidates measurement
    /// </summary>
    public void Invalidate()
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Returns true if alignment set correctly
    /// </summary>
    public bool IsAlignmentSet
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Returns true if this measure is built in (can't be deleted)
    /// </summary>
    public bool IsBuiltIn
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Returns true if measure is simulated
    /// </summary>
    public bool IsSimulated
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Returns measure state
    /// </summary>
    public pi.PWI_MeasureState MeasureState
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Returns measure name
    /// </summary>
    public string Name
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    //=========================================================================
    /// <summary>
    /// Returns part compensation factor
    /// </summary>
    public double PartCompensationFactor
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Returns a list of report variables for this measure
    /// </summary>
    public pi.ReportVariables ReportVariables
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Makes last calculated alignment active
    /// </summary>
    public void SetLastCalculatedAlignmentActive()
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Returns shrink factor
    /// </summary>
    public double ShrinkFactor
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Returns the temperature this measure has been taken at
    /// </summary>
    public double Temperature
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Unique ID of the measurement
    /// </summary>
    public int UniqueID
    {
      get { throw new NotImplementedException(); }
    }

    //=========================================================================
    /// <summary>
    /// Unsets active alignment
    /// </summary>
    public void UnsetActiveAlignment()
    {
      throw new NotImplementedException();
    }

    //=========================================================================
    /// <summary>
    /// Returns position matric
    /// </summary>
    /// <param name="coef3x4">Matrix</param>
    /// <returns>True if succeeded</returns>
    public bool get_ActivePositioningMatrix(ref object coef3x4)
    {
      throw new NotImplementedException();
    }
  }
}
