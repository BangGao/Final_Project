using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FieldEnergyControllers : MonoBehaviour
{
  public MenuController menucController;
  
  [Header("UI Text Component")] 
  public Text textInStatus;
  public Slider fieldStatusSlider;

  private int _currentPlantTimesCorn = 0;
  private int _currentPlantTimesCabbage = 0;
  private int _currentPlantTimesWaterMelon = 0;

  private void Update()
  {
    updateText();
    EnergyCalculator();
  }


  private void updateText()
  {
    if (menucController.currentPlant != "")
    {
      textInStatus.text = "CURRENT PLANT:" + menucController.currentPlant;
    }
    else
    {
      textInStatus.text = "CURRENT PLANT:";
    }
  }

  public void ChangeSlider()
  {
    if (menucController.lastPlant == menucController.currentPlant)
    {
      fieldStatusSlider.value -= 0.5f;
    }
    else
    {
      fieldStatusSlider.value -= 0.2f;
    }
    
  }
  
  private void EnergyCalculator()
  {
    if (menucController.havestType != "")
    {
      switch (menucController.havestType)
      {
        case "Corn":
          _currentPlantTimesCorn++;
          break;
        case "Cabbage":
          _currentPlantTimesCabbage++;
          break;
        case "WaterMelon":
          _currentPlantTimesWaterMelon++;
          break;
      }
    }
  }
  
}
