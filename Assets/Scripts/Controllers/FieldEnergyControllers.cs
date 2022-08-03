using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldEnergyControllers : MonoBehaviour
{
  public MenuController menucController;
  
  [Header("UI Text Component")] 
  public Text textInStatus;
  public Slider fieldStatusSlider; 
  
  private int _maxPlantCorn = 2;
  private int _maxPlantCabbage = 2;
  private int _maxPlantWaterMalon = 2;

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
      Debug.Log("1");
      textInStatus.text = "CURRENT PLANT:";
    }
  }

  private void changeSlider()
  {
    
  }
  
  
  private void EnergyCalculator()
  {
    if (menucController.havestType == "")
    {
      return;
    }
    else
    {
      if (menucController.havestType == "Corn")
      {
        _currentPlantTimesCorn++;
      }
      else if(menucController.havestType == "Cabbage")
      {
        _currentPlantTimesCabbage++;
      }
      else if(menucController.havestType == "WaterMelon")
      { 
        _currentPlantTimesWaterMelon++;
      }
    }
  }
  
  

}
