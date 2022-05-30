using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    public UnityAction _action;
    public UnityEvent _myevent; 
    
    public GameObject _uIElement_Field;
    private GameObject _objectGetByRay; 
    public string ObjectName;

    
    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    
    private Camera _cameraRay;
    private bool _isOpened;
    
    
    void Start()
    {
        _action = new UnityAction(plantMenu);
        _myevent = new UnityEvent();
        _myevent.AddListener(_action);
        
        _cameraRay = Camera.main;
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        _myevent.Invoke();
        CatchObjectByRay();
    }
    
    private void plantMenu()
    {
        _uIElement_Field.SetActive(_isOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _isOpened = !_isOpened;
            Debug.Log("Press Find");
        }
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
