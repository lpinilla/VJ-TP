using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    static public GlobalData instance;

    public bool IsVictory => _isVictory;
    public float points => _points;
    [SerializeField] private bool _isVictory;
    [SerializeField] private float _points;

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        instance = this;
        transform.parent = null;
        DontDestroyOnLoad(this);
    }

    public void SetVictoryField(bool isVictory) => _isVictory = isVictory;
    public void AddPoints(float points) => _points = points + points;

}
