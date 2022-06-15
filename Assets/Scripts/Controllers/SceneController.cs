using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public delegate void buttonDelegate();

public class SceneController : SingleTon<SceneController>
{
    public Button startBtn;
    public Button exitBtn;
    public Animator animator;
    
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Start()
    {
        GameObject.DontDestroyOnLoad(this);
        startBtn.onClick.AddListener(EnterMainGame);
        exitBtn.onClick.AddListener(ExitGame);
    }
    
    private void EnterMainGame()
    {
        StartCoroutine(LoadLevel());
    }
    
    IEnumerator LoadLevel()
    {
        animator.SetBool("FadeIN",true);
        animator.SetBool("FadeOUT",false);
        //Delay one second
        yield return new WaitForSeconds(1.0f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            MouseController.ShowMouse(false);
            operation.allowSceneActivation = true;
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
