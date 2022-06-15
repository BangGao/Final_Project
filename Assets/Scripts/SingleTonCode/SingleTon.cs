using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T :SingleTon<T>
{
   private static T _instance;

   public static T Instance
   {
      get { return _instance; }
   }

   protected virtual void Awake()
   {
      if (_instance !=null)
      {
         Destroy(gameObject);
      }
      else
      {
         _instance = (T) this;
      }
   }

   public static bool IsInitialized
   {
      get { return _instance != null; }
   }

   protected void OnDestroy()
   {
      if (_instance == this)
      {
         _instance = null;
      }
   }
}
