using System;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject goDayNightController;
    private DayNightController _dayNightController;

    [Header("Event List")] public UnityEvent Event;

    [Header("Plants")] 
    [SerializeField] private GameObject _cornStart;
    [SerializeField] private GameObject _waterMelonStart;
    [SerializeField] private GameObject _cabbageStart;
    
    [Header("Menus")]
    public GameObject plantMenu;
    private CanvasGroup _canvasGroup;
    
    [Header("Buttons")]
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button closeButton;

    [Header("PlantInformation")] 
    [SerializeField] private int plantDate;
    [SerializeField] private string CurrentPlant;
    
    public  bool _hasPlanted;
    private bool _isOpened;
    private bool _showCursor;
    private bool _canOpenPlantMenu;


    void Start()
    {
        _canvasGroup = plantMenu.GetComponent<CanvasGroup>();
        _dayNightController = goDayNightController.GetComponent<DayNightController>();
    }
    
    void Update()
    {
        OpenPlantMenu();
        MonitorButtons();
    }

    private void MonitorButtons()
    {
        button1.onClick.AddListener(PlantCorn);
        button2.onClick.AddListener(PlantWaterMelon);
        button3.onClick.AddListener(PlantCabbage);
        closeButton.onClick.AddListener(ClosePlantMenu);
    }

    public void PlantCorn()
    {
        _cornStart.SetActive(true);
        _isOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        CurrentPlant = "Corn";
        plantDate = _dayNightController.currentDay;
    }

    public void PlantWaterMelon()
    {
        _waterMelonStart.SetActive(true);
        _isOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        CurrentPlant = "WaterMelon";
        plantDate = _dayNightController.currentDay;
    }
    
    public void PlantCabbage()
    {
        _cabbageStart.SetActive(true);
        _isOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        CurrentPlant = "Cabbage";
        plantDate = _dayNightController.currentDay;
    }
    
    private void OpenPlantMenu()
    {
        plantMenu.SetActive(_isOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame && _canOpenPlantMenu && _hasPlanted == false)
        {
            _isOpened = !_isOpened;
            _showCursor = !_showCursor;
            MouseController.ShowMouse(_showCursor);
        }
    }

    private void ClosePlantMenu()
    {
        _isOpened = false;
        _showCursor = false;
        MouseController.ShowMouse(_showCursor);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canOpenPlantMenu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canOpenPlantMenu = false;
    }
    
}
