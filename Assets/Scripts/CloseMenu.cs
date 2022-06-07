using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CloseMenu : MonoBehaviour,IPointerClickHandler
{
    public UnityEvent closeMenuClick;
    public GameObject plantMenu;

    private void Start()
    {
        closeMenuClick.AddListener(new UnityAction(ButtonLeftClick));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            closeMenuClick.Invoke();
        }
    }

    private void ButtonLeftClick()
    {
        //plantMenu.SetActive(false);
        plantMenu.GetComponent<CanvasGroup>().alpha = 0;
        MouseController.ShowMouse(false);
    }
}
