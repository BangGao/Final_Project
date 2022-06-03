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
    
    public UnityAction _action;
    public UnityAction _plantButtomAction;
    
    public UnityEvent _myevent;
    
    [Header("Select Field")]
    public GameObject _plantMenu;
   

    private GameObject temp; 
    public bool _aimField;
    
    private Camera _cameraRay;
    private bool _isOpened;
    private bool _canOpenPlantMenu;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        _action = new UnityAction(OpenPlantMenu);
        _myevent = new UnityEvent();
        _myevent.AddListener(_action);
        
        _cameraRay = Camera.main;
        GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        _myevent.Invoke();
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
        
        _plantMenu.SetActive(_isOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame && _canOpenPlantMenu && _aimField)
        {
            _isOpened = !_isOpened;
        }
    }

    public GameObject PassDataToMenu()
    {
        if (_objectGetByRay != null)
            return _objectGetByRay;
        else
            return temp;
    }
}
