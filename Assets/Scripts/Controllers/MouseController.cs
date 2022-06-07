using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseController
{
    public static bool CameraDisable;
    
    //Control the mouse to be Visible or Invisible
    public static void ShowMouse(bool showMouse)
    {
        if (showMouse)
        {
            Cursor.visible = true;
            CameraDisable = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            CameraDisable = false;
            Cursor.visible = false;
        }
    }
}
