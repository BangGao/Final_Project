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
    public GameObject fertizerMenu;
    public GameObject haverstMenu;
    public GameObject warningCanvas;
    
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
    [SerializeField] private string CurrentPlant;

    public  bool _hasPlanted;
    public  bool _canbeHarvest;
    private bool _pMisOpened;
    private bool _hMisOpened;
    private bool _showCursor;
    private bool _canOpenPlantMenu;
    private bool _canOpenHarvestMenu;
    private int plantTime;

    private FieldStatus _fieldStatus;
    private PlantType _plantType;
    
    public int _fieldExhaustTime = 0;
    
    void Start()
    {
        _dayNightController = goDayNightController.GetComponent<DayNightController>();
    }
    
    void Update()
    {
        OpenPlantMenu();
        OpenHaverstMenu();
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
        _pMisOpened = false;
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
            _canbeHarvest = true;
        }
        else if(plantTime>4)
        {
            _canbeHarvest = true;
        }
    }
    
    //WaterMelon Part
    public void PlantWaterMelon()
    {
        _waterMelonStart.SetActive(true);
        _pMisOpened = false;
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
            _canbeHarvest = true;
        }
        else if(plantTime>4)
        {
            _canbeHarvest = true;
        }
    }
    
    //Cabbage Plant
    public void PlantCabbage()
    {
        _cabbageStart.SetActive(true);
        _pMisOpened = false;
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
            _canbeHarvest = true;
        }
        else if(plantTime>4)
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
            _pMisOpened = !_pMisOpened;
            _showCursor = !_showCursor;
            MouseController.ShowMouse(_showCursor);
        }
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
      
        _cornFinal.SetActive(false);
        _cabbageFinal.SetActive(false);
        _waterMelonFinal.SetActive(false);

        CurrentPlant = "";
        _canOpenHarvestMenu = false;
        _fieldExhaustTime++;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // //暂时规定不为0的时候土地即疲劳状态
            // if (_fieldExhaustTime != 0)
            // {
            //     warningCanvas.SetActive(true);
            //     Debug.Log("1");
            // }
            // else
            // {
            //     warningCanvas.SetActive(false);
            // }
            if (_fieldExhaustTime != 0)
            {
                warningCanvas.SetActive(true);
            }
            _canOpenPlantMenu = true;
            _canOpenHarvestMenu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        warningCanvas.SetActive(false);
        _canOpenPlantMenu = false;
        _canOpenHarvestMenu = false;
    }
    
}
