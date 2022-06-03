using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FieldController : MonoBehaviour
{
    private PlantMenucontroller pmc;

    public GameObject testObject;
    public GameObject _corn_Small;
    public GameObject _waterMelon_Small;
    public GameObject _cabbage_Small;
    
    
    
    private void Start()
    {

        pmc = testObject.GetComponent<PlantMenucontroller>();
        pmc.test = plantCorn;
    }

    private void Update()
    {
        
    }

    

    public void plantCorn()
    {
        Debug.Log("Worked so far");
    }
    
    private void plantWaterMelon()
    {
        _waterMelon_Small.SetActive(true);
    }
    
    private void plantCabbage()
    {
        _cabbage_Small.SetActive(true);
    }
    
}
