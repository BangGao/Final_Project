using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlantMenucontroller : MonoBehaviour
{
   public delegate void Test();
   
   public Test test;
   
   
   public GameObject _field;
   public Button _corn_plant;
   public Button _watermelon_plant;
   public Button _cabbage_plant;

   private bool getField;

   private void OnEnable()
   {
      //MenuController.instance._myevent.AddListener(OnEnable);
      if (MenuController._objectGetByRay!=null)
      {
         Debug.Log("1");
         _field = MenuController._objectGetByRay;
        _corn_plant.onClick.AddListener(UseThisFunc);
      }
   }

   private void Update()
   {
      if (Keyboard.current.spaceKey.wasPressedThisFrame )
      {
         UseThisFunc();
      }
   }

   public void UseThisFunc()
   {
      if (test!=null)
      {
         test();  
      }
      else
      {
         Debug.Log("Wrong");
      }
      
   }
}
