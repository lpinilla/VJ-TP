using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelManager : MonoBehaviour
{
    public Animator TunnelExit;
    private bool charger = false;
    

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.Level2Event += LoadLevelTwo;
        StartCoroutine(LoadAsync2());
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
        // StartCoroutine(LoadAsync("Level2"));
        // GameObject door = GameObject.Find("Exit Door");
        // TunnelExit = door.GetComponent<Animator>();
        // TunnelExit.SetBool("isOpen",true);
        
        charger = true;
    }
    
    IEnumerator LoadAsync(String name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            // if(operation.progress >= .9f) operation.allowSceneActivation = true;
            
            yield return null;
        }

        while (!charger)
        {
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
    IEnumerator LoadAsync2()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level2", LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        Debug.Log("in LEVEL 2");			

        while (operation.progress >= .9f)//!operation.isDone)
        {

            // if(operation.progress >= .9f) operation.allowSceneActivation = true;
            
            yield return null;
        }
        while (!charger)
        {
            yield return null;
        }

        Debug.Log("charger true LEVEL 2");
        operation.allowSceneActivation = true;
        GameObject door = GameObject.Find("Exit Door");
        TunnelExit = door.GetComponent<Animator>();
        
        while (!operation.isDone)
        {
            yield return null;
        }  
        
        TunnelExit.SetBool("isOpen",true);
    }
}
