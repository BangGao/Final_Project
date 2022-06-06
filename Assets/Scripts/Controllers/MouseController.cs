using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseController  
{
    // public static MouseController Instance;
    //
    // private void Awake()
    // {
    //     if (Instance != null)
    //     {
    //         Instance = this;
    //     }
    // }

    //Control the mouse to be Visible or Invisible
    public static void ShowMouse(bool showMouse)
    {
        if (showMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
        }
    }
}
