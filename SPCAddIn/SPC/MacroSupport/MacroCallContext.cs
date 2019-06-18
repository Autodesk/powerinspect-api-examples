//=============================================================================
//D MacroCallContext.cs
//
// Call context which is passed to each macro when it's being executed.
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

namespace Autodesk.PowerInspect.AddIns.SPC
{
  public class MacroCallContext
  {
    //=============================================================================
    /// <summary> 
    /// Constructor
    /// </summary>
    /// <param name="pi_doc">Document</param>
    /// <param name="pi_measure">Measure</param>
    public MacroCallContext(
      pi.IPIDocument pi_doc,
      pi.IMeasure pi_measure
    )
    {
      this.pi_doc = pi_doc;
      this.pi_measure = pi_measure;
    }

    /// <summary> Document reference </summary>
    public pi.IPIDocument pi_doc { get; private set; }

    /// <summary> PI's IMeasure reference </summary>
    public pi.IMeasure pi_measure { get; private set; }
  }
}
