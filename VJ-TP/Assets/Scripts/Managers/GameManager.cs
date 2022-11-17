using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool _isVictory = false;
    

    void Start()
    {
        EventsManager.instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver(bool isVictory)
    {
        _isGameOver = true;
        _isVictory = isVictory;
        GlobalData.instance.SetVictoryField(_isVictory);
        if (isVictory) {
            Invoke("LoadVictoryScene", 3);
        } else {
            Invoke("LoadEndgameScene", 3);
        }
    }

    private void LoadEndgameScene() => SceneManager.LoadScene("Game Over");
    private void LoadVictoryScene() => SceneManager.LoadScene("Victory");

}
