using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelManager : MonoBehaviour
{
    public Animator TunnelExit;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.Level2Event += LoadLevelTwo;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TunnelExit"){
            EventsManager.instance.StartLevel2();
            Destroy(other.gameObject);
        }
    }

    void LoadLevelTwo()
    {
        StartCoroutine(LoadAsync("Level2"));
        GameObject door = GameObject.Find("Exit Door");
        TunnelExit = door.GetComponent<Animator>();
        TunnelExit.SetBool("isOpen",true);
    }
    
    IEnumerator LoadAsync(String name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            if(operation.progress >= .9f) operation.allowSceneActivation = true;
            
            yield return null;
        }
    }
}
