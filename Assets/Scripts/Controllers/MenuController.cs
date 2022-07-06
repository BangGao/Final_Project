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
    public delegate void PickPlant();

    private PickPlant _pickPlant;
    
    public GameObject goDayNightController;
    private DayNightController _dayNightController;

    [Header("Event List")] public UnityEvent Event;

    [Header("Plants")] 
    [SerializeField] private GameObject _cornStart;
    [SerializeField] private GameObject _cornMiddle;
    [SerializeField] private GameObject _cornFinal;
    [SerializeField] private GameObject _waterMelonStart;
    [SerializeField] private GameObject _waterMelonMiddle;
    [SerializeField] private GameObject _waterMelonFinal;
    [SerializeField] private GameObject _cabbageStart;
    [SerializeField] private GameObject _cabbageMiddle;
    [SerializeField] private GameObject _cabbageFinal;
    
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
    private int plantTime;

    void Start()
    {
        _canvasGroup = plantMenu.GetComponent<CanvasGroup>();
        _dayNightController = goDayNightController.GetComponent<DayNightController>();
    }
    
    void Update()
    {
        OpenPlantMenu();
        MonitorButtons();
        PlantGrowProcess(CurrentPlant);
    }
    
    private void MonitorButtons()
    {
        button1.onClick.AddListener(PlantCabbage);
        button2.onClick.AddListener(PlantWaterMelon);
        button3.onClick.AddListener(PlantCorn);
        closeButton.onClick.AddListener(ClosePlantMenu);
    }

    //Plant
    private void PlantGrowProcess(string currentPlant)
    {
        if (currentPlant == "Corn")
        {
            _pickPlant += CornGrow;
            _pickPlant.Invoke();
        }
        else if (currentPlant == "WaterMelon")
        {
            _pickPlant += WaterMelonGrow;
            _pickPlant.Invoke();
        }
        else if (currentPlant == "Cabbage")
        {
            _pickPlant += CabbageGrow;
            _pickPlant.Invoke();
        }
        
    }
    
    //Corn Part
    public void PlantCorn()
    {
        _cornStart.SetActive(true);
        _isOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        CurrentPlant = "Corn";
        plantDate = _dayNightController.currentDay;
    }

    private void CornGrow()
    {
        plantTime = _dayNightController.currentDay - plantDate;
        if (plantTime == 2)
        {
            _cornStart.SetActive(false);
            _cornMiddle.SetActive(true);
        }
        else if (plantTime == 4)
        {
            _cornMiddle.SetActive(false);
            _cornFinal.SetActive(true);
        }
    }
    
    //WaterMelon Part
    public void PlantWaterMelon()
    {
        _waterMelonStart.SetActive(true);
        _isOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        CurrentPlant = "WaterMelon";
        plantDate = _dayNightController.currentDay;
    }
    
    private void WaterMelonGrow()
    {
        plantTime = _dayNightController.currentDay - plantDate;
        if (plantTime == 2)
        {
            _waterMelonStart.SetActive(false);
            _waterMelonMiddle.SetActive(true);
        }
        else if (plantTime == 4)
        {
            _waterMelonMiddle.SetActive(false);
            _waterMelonFinal.SetActive(true);
        }
    }
    
    //Cabbage Plant
    public void PlantCabbage()
    {
        _cabbageStart.SetActive(true);
        _isOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        CurrentPlant = "Cabbage";
        plantDate = _dayNightController.currentDay;
    }
    
    private void CabbageGrow()
    {
        plantTime = _dayNightController.currentDay - plantDate;
        if (plantTime == 2)
        {
            _cabbageStart.SetActive(false);
            _cabbageMiddle.SetActive(true);
        }
        else if (plantTime == 4)
        {
            _cabbageMiddle.SetActive(false);
            _cabbageFinal.SetActive(true);
        }
    }

    
    //PlantMenu Part
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
