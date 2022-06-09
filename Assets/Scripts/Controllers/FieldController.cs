using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController :MonoBehaviour
{
    private MenuController _menuController;
    
    public bool isWatered;
    public bool isPlanted;

    private void OnEnable()
    {
        _menuController = this.GetComponent<MenuController>();
        isPlanted = _menuController._hasPlanted;
    }

    private void PlantGrow()
    {
        //With time goings, the plant grows, 2 day one type
    }

    private void TimeCalculater()
    {
        
    }
}
