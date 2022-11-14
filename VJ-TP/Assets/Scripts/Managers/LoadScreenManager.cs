using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreenManager : MonoBehaviour
{

    void Update()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level1");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            if(operation.progress >= .9f) operation.allowSceneActivation = true;
            
            yield return null;
        }
    }
}
