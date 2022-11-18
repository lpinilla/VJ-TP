using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    static public GlobalData instance;

    public bool IsVictory => _isVictory;
    // public float PointsTotal => _pointsTotal;
    [SerializeField] private bool _isVictory;
    // [SerializeField] private float _pointsTotal;
    private static float totalScore;

    private void Awake()
    {

        if (instance != null) Destroy(this.gameObject);
        instance = this;
        // transform.parent = null;
        DontDestroyOnLoad(this);

    }

    public void SetVictoryField(bool isVictory) => _isVictory = isVictory;
    // public void SetPointsTotal(float points) => _pointsTotal = points;

    public void AddPoints(float points)
    {
        totalScore += points;
        // SetPointsTotal(_pointsTotal + points);;
        // Debug.Log(PointsTotal.ToString() + "in gd");
    }

    public String GetPoints()
    {
        return totalScore.ToString();
    }

    public void ResetScore()
    {
        totalScore = 0;
    }
   
}
