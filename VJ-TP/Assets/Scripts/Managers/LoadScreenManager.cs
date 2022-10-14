using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreenManager : MonoBehaviour
{
    [SerializeField] private string _targetScene = "Level1";

    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_targetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            if(operation.progress >= .9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
