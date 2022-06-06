using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void Test();
   
    public Test test;
   
    public GameObject _field;
    public Button _corn_plant;
    public Button _watermelon_plant;
    public Button _cabbage_plant;

    private void OnEnable()
    {
        if (MenuController._objectGetByRay!=null)
        {
            Debug.Log("1");
            _field = MenuController._objectGetByRay; 
            //_corn_plant.onClick.AddListener(UseThisFunc);
        }
    }
}
