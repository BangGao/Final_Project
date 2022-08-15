using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    public delegate void WinDelegate();

    private WinDelegate _wD;
    
    public int cornSum;
    public int waterMelonSum;
    public int cabbageSum;

    public Text cornSumText;
    public Text waterMelonSumText;
    public Text cabbageSumText;

    public GameObject winUI;

    private void Awake()
    {
        //_wD = GameWin;
    }

    private void Update()
    {
        ShowSums();
        
        //if (cornSum == 10 && waterMelonSum == 10 && cabbageSum == 10)
        if (cornSum == 1)
        {
            FirstCallback(GameWin);
        }
    }

    private void ShowSums()
    {
        cornSumText.text = "CornTotal:" + cornSum;
        waterMelonSumText.text ="WaterMelonTotal:" + waterMelonSum;
        cabbageSumText.text = "cabbageTotal:" + cabbageSum;
    }

    // private void FirstCallback(EditorApplication.CallbackFunction callbackFunction)
    // {
    //     Debug.Log("Start Callback");
    //     callbackFunction();
    // }
    
    private void FirstCallback(WinDelegate _winDelegate)
    {
        Debug.Log("Start Callback");
        _winDelegate();
    }

    
    
    private void GameWin()
    {
        winUI.SetActive(true);
        MouseController.ShowMouse(true);
        Debug.Log("You win this Game !!!");
    }
    
}
