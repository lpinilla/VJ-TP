using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelTwoManager : MonoBehaviour
{

    private AsyncOperation _operation;
    private void Start()
    {
        // EventsManager.instance.Level1FinaleEvent += ActivateScene;
        //StartCoroutine(LoadAsync(false));
    }

    void Update()
    {
    }

    IEnumerator LoadAsync(Boolean state)
    {
        _operation = SceneManager.LoadSceneAsync("SciFi_Warehouse");
        _operation.allowSceneActivation = state;
        yield return null;
    }

    void ActivateScene()
    {
        Debug.Log("IN LOADING SCENE");
        Debug.Log(_operation.progress >= .9f);
        _operation.allowSceneActivation = true;
        LoadAsync(true);
    }

}
