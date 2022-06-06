using System;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [Header("Globo Vary")]
    public static GameObject _objectGetByRay;
    
    public UnityAction action;

    public UnityEvent myEvent;
    
    [Header("Select Field")]
    public GameObject plantMenu;
   

    private GameObject _temp; 
    public bool _aimField;
    
    private Camera _cameraRay;
    private bool _isOpened;
    private bool _showCursor;
    private bool _canOpenPlantMenu;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        action = new UnityAction(OpenPlantMenu);
        myEvent = new UnityEvent();
        myEvent.AddListener(action);
        
        _cameraRay = Camera.main;
        GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        myEvent.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag =="CollisionDetector")
        {
            _canOpenPlantMenu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canOpenPlantMenu = false;
    }

    private void OpenPlantMenu()
    {
        Ray ray = _cameraRay.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider.gameObject.tag == "Field")
            {
                _aimField = true;
                _objectGetByRay = hit.collider.gameObject;
            }
            else
            {
                _aimField = false;
            }
        }
        
        plantMenu.SetActive(_isOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame && _canOpenPlantMenu && _aimField)
        {
            _isOpened = !_isOpened;
            
            //Show Mouse Cursor or not
            _showCursor = !_showCursor;
            MouseController.ShowMouse(_showCursor);
        }
    }
}
