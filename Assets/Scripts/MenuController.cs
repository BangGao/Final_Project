using System;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    public UnityAction _action;
    public UnityEvent _myevent; 
    
    public GameObject _uIElement_Field;
    public string ObjectName;
    
    public GameObject _objectGetByRay;
    private Camera _cameraRay;
    private bool _isOpened;
    private bool _canOpenPlantMenu;
    private bool _aimField;
    
    void Start()
    {
        _action = new UnityAction(plantMenu);
        _myevent = new UnityEvent();
        _myevent.AddListener(_action);
        
        _cameraRay = Camera.main;
        GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        _myevent.Invoke();
        //CatchObjectByRay();
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

    private void plantMenu()
    {
        Ray ray = _cameraRay.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            _objectGetByRay = hit.collider.gameObject;
            if (_objectGetByRay.tag == "Field")
            {
                _aimField = true;
            }
            else
            {
                _aimField = false;
            }
        }
        
        _uIElement_Field.SetActive(_isOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame && _canOpenPlantMenu && _aimField)
        {
            _isOpened = !_isOpened;
        }
    }
}
