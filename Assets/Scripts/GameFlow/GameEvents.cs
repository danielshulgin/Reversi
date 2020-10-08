using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{    
    public static event Action OnStartGame;
    
    public static event Action OnEndGame;

    
    public static GameEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple GameEvents instances");
        }
    }

    public void SendStartGame()
    {
        OnStartGame?.Invoke();
    }
    
    public void SendEndGame()
    {
        OnEndGame?.Invoke();
    }
}
