using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuController : MonoBehaviour
{

    public GameObject _uIElement_Field;
    private GameObject _objectGetByRay; 
    public string ObjectName;
    
    private Camera _cameraRay;
    private bool isClosed;

    void Start()
    {
        _cameraRay = Camera.main;
    }

    void plantMenu()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            isClosed = !isClosed;
        }
        _uIElement_Field.SetActive(isClosed);
    }
   
    void Update()
    {
        plantMenu();
        CatchObjectByRay();
    }

    private void CatchObjectByRay()
    {
        Ray ray = _cameraRay.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            _objectGetByRay = hit.collider.gameObject;
            ObjectName = _objectGetByRay.name;
        }
    }
}
