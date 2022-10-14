using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

    
    public class UI_ButtonsLogic : MonoBehaviour
    {
        public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");
        public void LoadLevelScene() => SceneManager.LoadScene("Loading Screen");
        
        public void LoadEndgameScene() => SceneManager.LoadScene("Game Over");
        public void LoadInfoScene() => Debug.Log("Information scene in development!!!");
        public void LoadSettingsScene() => Debug.Log("Settings scene in development!!!");
        public void CloseGame() => Application.Quit();

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
