﻿// Decompiled with JetBrains decompiler
// Type: Collada141.TargetableFloat
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
  [XmlType(Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
  [RummageKeepReflectionSafe]
  [Serializable]
  public class TargetableFloat
  {
    private string sidField;
    private double valueField;

    [XmlAttribute(DataType = "NCName")]
    public string sid
    {
      get
      {
        return this.sidField;
      }
      set
      {
        this.sidField = value;
      }
    }

    [XmlText]
    public double Value
    {
      get
      {
        return this.valueField;
      }
      set
      {
        this.valueField = value;
      }
    }
  }
}
