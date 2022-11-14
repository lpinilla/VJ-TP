using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    static public GlobalData instance;

    public bool IsVictory => _isVictory;
    [SerializeField] private bool _isVictory;

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        instance = this;
        transform.parent = null;
        DontDestroyOnLoad(this);
    }

    public void SetVictoryField(bool isVictory) => _isVictory = isVictory;
}
