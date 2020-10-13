﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceDust.Overlay
{
  public class BandResourceElement : MonoBehaviour
  {

    public bool active = false;
    public RectTransform rect;
    public Text bandName;
    public Text concentration;

    ResourceBand associatedBand;
    CelestialBody associatedBody;

    void Awake()
    {
      FindElements();
    }

    void FindElements()
    {
      rect = this.transform as RectTransform;

      bandName = transform.FindDeepChild("Title").GetComponent<Text>();
      concentration = transform.FindDeepChild("Conc").GetComponent<Text>();

    }

    public void Start()
    {
    }
    public void SetBand(CelestialBody body, ResourceBand bnd)
    {

      if (bandName == null) FindElements();

      associatedBand = bnd;
      associatedBody = body;
      SetState(associatedBody,associatedBand);
    }

    void SetState(CelestialBody body, ResourceBand bnd)
    {

      if (SpaceDustScenario.Instance.IsIdentified(bnd.ResourceName, bnd.name, body))
      {
        bandName.enabled = true;
        concentration.enabled = true;
        bandName.text = bnd.name;
        concentration.text = String.Format("{0} u/m³", bnd.Abundance.ToString("G3"));
      }
      else if (SpaceDustScenario.Instance.IsDiscovered(bnd.ResourceName, bnd.name, body))
      {

        bandName.enabled = true;
        concentration.enabled = true;
        bandName.text = bnd.name;
        concentration.text = "unknown";
      }
      else
      {
        bandName.enabled = false;
        concentration.enabled = false;
      }
    }
    public void SetVisible(bool state)
    {
      active = state;
      rect.gameObject.SetActive(state);
      if (state)
      {
        SetState(associatedBody, associatedBand);
      }  
    }
  }
}
