﻿// Decompiled with JetBrains decompiler
// Type: Collada141.channel
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
  [XmlRoot(IsNullable = false, Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
  [RummageKeepReflectionSafe]
  [Serializable]
  public class channel
  {
    private string sourceField;
    private string targetField;

    [XmlAttribute]
    public string source
    {
      get
      {
        return this.sourceField;
      }
      set
      {
        this.sourceField = value;
      }
    }

    [XmlAttribute(DataType = "token")]
    public string target
    {
      get
      {
        return this.targetField;
      }
      set
      {
        this.targetField = value;
      }
    }
  }
}
