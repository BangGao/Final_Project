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
    public GameObject goDayNightController;
    
    private PickPlant _pickPlant;
    private DayNightController _dayNightController;
    
    [Header("Plants")] 
    [SerializeField] private GameObject cornStart;
    [SerializeField] private GameObject cornMiddle;
    [SerializeField] private GameObject cornFinal;
    [SerializeField] private GameObject waterMelonStart;
    [SerializeField] private GameObject waterMelonMiddle;
    [SerializeField] private GameObject waterMelonFinal;
    [SerializeField] private GameObject cabbageStart;
    [SerializeField] private GameObject cabbageMiddle;
    [SerializeField] private GameObject cabbageFinal;
    
    [Header("Menus")]
    public GameObject plantMenu;
    public GameObject fertizerMenu;
    public GameObject blockWindow;
    public GameObject haverstMenu;
    public GameObject statusWindow;
    public GameObject warningCanvas;

    [Header("WinCondition")] public GameObject gowinController;
    //[Header("FieldEnergyController")] public GameObject goFieldEnergyController;
    private WinController _winController;
    private FieldEnergyControllers _fieldEnergyControllers;
    
    [Header("Buttons in Plant Menu")]
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button closeButton;

    [Header("Buttons in Haverst Menu")]
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    
    [Header("PlantInformation")]
    [SerializeField] private int plantDate;
    [HideInInspector] public string currentPlant;
    [HideInInspector] public string lastPlant;

    
        
    public float fieldExhaustTime = 0.0f;
    
    [HideInInspector]
    public string havestType;

    private  bool _hasPlanted;
    private  bool _canbeHarvest;
    private bool _pMisOpened;
    private bool _hMisOpened;
    private bool _showCursor;
    private bool _canOpenPlantMenu;
    private bool _canOpenHarvestMenu;
    private int _plantTime;
    
    private PlantType _plantType;
    
    void Start()
    {
        _dayNightController = goDayNightController.GetComponent<DayNightController>();
        _winController = gowinController.GetComponent<WinController>();
        _fieldEnergyControllers = transform.GetComponent<FieldEnergyControllers>();
    }
    
    void Update()
    {
        OpenPlantMenu();
        OpenHaverstMenu();
        MonitorButtons();
        PlantGrowProcess(currentPlant);
    }
    
    private void MonitorButtons()
    {
        button1.onClick.AddListener(PlantCabbage);
        button2.onClick.AddListener(PlantWaterMelon);
        button3.onClick.AddListener(PlantCorn);
        closeButton.onClick.AddListener(ClosePlantMenu);
    }

    //Plant
    private void PlantGrowProcess(string _currentPlant)
    {
        if (_currentPlant == "Corn")
        {
            _pickPlant += CornGrow;
            _pickPlant.Invoke();
        }
        else if (_currentPlant == "WaterMelon")
        {
            _pickPlant += WaterMelonGrow;
            _pickPlant.Invoke();
        }
        else if (_currentPlant == "Cabbage")
        {
            _pickPlant += CabbageGrow;
            _pickPlant.Invoke();
        }
    }
    
    //Corn Part
    public void PlantCorn()
    {
        cornStart.SetActive(true);
        _pMisOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        currentPlant = "Corn";
        plantDate = _dayNightController.currentDay;
    }

    private void CornGrow()
    {
        _plantTime = _dayNightController.currentDay - plantDate;
        if (_plantTime == 2 && currentPlant == "Corn")
        {
            cornStart.SetActive(false);
            cornMiddle.SetActive(true);
        }
        else if (_plantTime == 4 && currentPlant == "Corn")
        {
            cornMiddle.SetActive(false);
            cornFinal.SetActive(true);
            _canbeHarvest = true;
        }
        else if(_plantTime>4)
        {
            _canbeHarvest = true;
        }
    }
    
    //WaterMelon Part
    public void PlantWaterMelon()
    {
        waterMelonStart.SetActive(true);
        _pMisOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        currentPlant = "WaterMelon";
        plantDate = _dayNightController.currentDay;
    }
    
    private void WaterMelonGrow()
    {
        _plantTime = _dayNightController.currentDay - plantDate;
        if (_plantTime == 2 && currentPlant == "WaterMelon")
        {
            waterMelonStart.SetActive(false);
            waterMelonMiddle.SetActive(true);
        }
        else if (_plantTime == 4 && currentPlant == "WaterMelon")
        {
            waterMelonMiddle.SetActive(false);
            waterMelonFinal.SetActive(true);
            _canbeHarvest = true;
        }
        else if(_plantTime>4)
        {
            _canbeHarvest = true;
        }
    }
    
    //Cabbage Plant
    public void PlantCabbage()
    {
        cabbageStart.SetActive(true);
        _pMisOpened = false;
        ClosePlantMenu();
        _hasPlanted = true;
        currentPlant = "Cabbage";
        plantDate = _dayNightController.currentDay;
    }
    
    private void CabbageGrow()
    {
        _plantTime = _dayNightController.currentDay - plantDate;
        if (_plantTime == 2 && currentPlant == "Cabbage")
        {
            cabbageStart.SetActive(false);
            cabbageMiddle.SetActive(true);
        }
        else if (_plantTime == 4 && currentPlant == "Cabbage")
        {
            cabbageMiddle.SetActive(false);
            cabbageFinal.SetActive(true);
            _canbeHarvest = true;
        }
        else if(_plantTime>4)
        {
            _canbeHarvest = true;
        }
    }
    
    //PlantMenu Part
    private void OpenPlantMenu()
    {
        plantMenu.SetActive(_pMisOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame && _canOpenPlantMenu && _hasPlanted == false)
        {
            if (_fieldEnergyControllers.fieldStatusSlider.value == 0)
            {
                blockWindow.SetActive(true);
                _showCursor = !_showCursor;
                MouseController.ShowMouse(_showCursor);
            }
            else
            {
                _pMisOpened = !_pMisOpened;
                _showCursor = !_showCursor;
                MouseController.ShowMouse(_showCursor);
            }
        }
    }

    public void CloseBlockWindow()
    {
        blockWindow.SetActive(false);
        _showCursor = false;
        MouseController.ShowMouse(_showCursor);
    }

    private void ClosePlantMenu()
    {
        _pMisOpened = false;
        _showCursor = false;
        MouseController.ShowMouse(_showCursor);
    }
    
    //Harvest Menu
    private void OpenHaverstMenu()
    {
        haverstMenu.SetActive(_hMisOpened);
        if (Keyboard.current.eKey.wasPressedThisFrame &&_canOpenHarvestMenu && _canbeHarvest)
        {
            _hMisOpened = !_hMisOpened;
            _showCursor = !_showCursor;
            MouseController.ShowMouse(_showCursor);
        }
    }

    public void HaverstPlant()
    {
        _hMisOpened = false;
        _hasPlanted = false;
        _canbeHarvest = false;
        _showCursor = false;
        MouseController.ShowMouse(_showCursor);
        
        lastPlant = currentPlant;
        switch (currentPlant)
        {
            case "Corn":
                cornFinal.SetActive(false);
                havestType = "Corn";
                _winController.cornSum++;
                Debug.Log(_winController.cornSum);
                break;
            case "WaterMelon":
                waterMelonFinal.SetActive(false);
                havestType = "WaterMelon";
                _winController.waterMelonSum++;
                break;
            case "Cabbage":
                cabbageFinal.SetActive(false);
                havestType = "Cabbage";
                _winController.cabbageSum++;
                break;
        }
        currentPlant = "";
        _canOpenHarvestMenu = false;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (_fieldEnergyControllers.fieldStatusSlider.value < 1.0f)
            {
                warningCanvas.SetActive(true);
            }
            _canOpenPlantMenu = true;
            _canOpenHarvestMenu = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            statusWindow.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        warningCanvas.SetActive(false);
        statusWindow.SetActive(false);
        _canOpenPlantMenu = false;
        _canOpenHarvestMenu = false;
    }
    
}
