using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public delegate void ofOne();
public delegate void buttonDelegate();

public class SceneController : SingleTon<SceneController>
{
    public Button startBtn;
    public Button exitBtn;
    public Animator animator;
    public buttonDelegate btnDelegate;
    
    //Try delegate and Coroutine
    
    protected override void Awake()
    {
        base.Awake();
        //Test1(Test2);
    }

    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    
    public void OpenMainGameScene(EditorApplication.CallbackFunction callback)
    {
        animator.SetBool("FadeIN",true);
    }

    private void ChangeToGameScene()
    {
        MouseController.ShowMouse(false);
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    // private void Test1(EditorApplication.CallbackFunction callback)
    // {
    //     Debug.Log("1");
    //     callback();
    // }
    //
    // private void Test2()
    // {
    //     Debug.Log("2");
    // }
}
