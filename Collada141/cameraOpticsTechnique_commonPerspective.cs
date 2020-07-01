﻿// Decompiled with JetBrains decompiler
// Type: Collada141.cameraOpticsTechnique_commonPerspective
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
  public class cameraOpticsTechnique_commonPerspective
  {
    private ItemsChoiceType1[] itemsElementNameField;
    private TargetableFloat[] itemsField;
    private TargetableFloat zfarField;
    private TargetableFloat znearField;

    [XmlElement("aspect_ratio", typeof (TargetableFloat))]
    [XmlElement("xfov", typeof (TargetableFloat))]
    [XmlElement("yfov", typeof (TargetableFloat))]
    [XmlChoiceIdentifier("ItemsElementName")]
    public TargetableFloat[] Items
    {
      get
      {
        return this.itemsField;
      }
      set
      {
        this.itemsField = value;
      }
    }

    [XmlElement("ItemsElementName")]
    [XmlIgnore]
    public ItemsChoiceType1[] ItemsElementName
    {
      get
      {
        return this.itemsElementNameField;
      }
      set
      {
        this.itemsElementNameField = value;
      }
    }

    public TargetableFloat znear
    {
      get
      {
        return this.znearField;
      }
      set
      {
        this.znearField = value;
      }
    }

    public TargetableFloat zfar
    {
      get
      {
        return this.zfarField;
      }
      set
      {
        this.zfarField = value;
      }
    }
  }
}
