using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointsUi;
    
    private void Start()
    {
        _pointsUi.text = GlobalData.instance.GetPoints();
        GlobalData.instance.ResetScore();
    }

}