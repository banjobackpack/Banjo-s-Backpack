﻿// Decompiled with JetBrains decompiler
// Type: Collada141.polygonsPH
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
  public class polygonsPH
  {
    private string[] hField;
    private string pField;

    public string p
    {
      get
      {
        return this.pField;
      }
      set
      {
        this.pField = value;
      }
    }

    [XmlElement("h")]
    public string[] h
    {
      get
      {
        return this.hField;
      }
      set
      {
        this.hField = value;
      }
    }
  }
}
