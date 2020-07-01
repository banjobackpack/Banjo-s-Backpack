﻿// Decompiled with JetBrains decompiler
// Type: Collada141.float_array
// Assembly: BanjoKazooieLevelEditor, Version=2.0.19.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E4E8A9F-6E2F-4B24-B56C-5C2BF24BF813
// Assembly location: C:\Users\runem\Desktop\BanjosBackpack\BB.exe

using RummageAttributes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Collada141
{
  [GeneratedCode("xsd", "4.0.30319.1")]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true, Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
  [XmlRoot(IsNullable = false, Namespace = "http://www.collada.org/2005/11/COLLADASchema")]
  [RummageKeepReflectionSafe]
  [Serializable]
  public class float_array
  {
    private ulong countField;
    private short digitsField;
    private string idField;
    private short magnitudeField;
    private string nameField;
    private double[] textField;

    public float_array()
    {
      this.digitsField = (short) 6;
      this.magnitudeField = (short) 38;
    }

    [XmlAttribute(DataType = "ID")]
    public string id
    {
      get
      {
        return this.idField;
      }
      set
      {
        this.idField = value;
      }
    }

    [XmlAttribute(DataType = "NCName")]
    public string name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }

    [XmlAttribute]
    public ulong count
    {
      get
      {
        return this.countField;
      }
      set
      {
        this.countField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(typeof (short), "6")]
    public short digits
    {
      get
      {
        return this.digitsField;
      }
      set
      {
        this.digitsField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(typeof (short), "38")]
    public short magnitude
    {
      get
      {
        return this.magnitudeField;
      }
      set
      {
        this.magnitudeField = value;
      }
    }

    [XmlText]
    public string _Text_
    {
      get
      {
        return COLLADA.ConvertFromArray<double>((IList<double>) this.Values);
      }
      set
      {
        this.Values = COLLADA.ConvertDoubleArray(value);
      }
    }

    [XmlIgnore]
    public double[] Values
    {
      get
      {
        return this.textField;
      }
      set
      {
        this.textField = value;
      }
    }
  }
}
