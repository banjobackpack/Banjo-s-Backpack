﻿// Decompiled with JetBrains decompiler
// Type: Collada141.physics_materialTechnique_common
// Assembly: BanjoKazooieLevelEditor, Version=2.0.19.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E4E8A9F-6E2F-4B24-B56C-5C2BF24BF813
// Assembly location: C:\Users\runem\Desktop\BanjosBackpack\BB.exe

using RummageAttributes;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Collada141
{
  [GeneratedCode("xsd", "4.0.30319.1")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
  [RummageKeepReflectionSafe]
  [Serializable]
  public class physics_materialTechnique_common
  {
    private TargetableFloat dynamic_frictionField;
    private TargetableFloat restitutionField;
    private TargetableFloat static_frictionField;

    public TargetableFloat dynamic_friction
    {
      get
      {
        return this.dynamic_frictionField;
      }
      set
      {
        this.dynamic_frictionField = value;
      }
    }

    public TargetableFloat restitution
    {
      get
      {
        return this.restitutionField;
      }
      set
      {
        this.restitutionField = value;
      }
    }

    public TargetableFloat static_friction
    {
      get
      {
        return this.static_frictionField;
      }
      set
      {
        this.static_frictionField = value;
      }
    }
  }
}
