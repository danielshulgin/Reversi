using System;
using UnityEngine;


public class GameConfiguration : MonoBehaviour
{
    public static event Action<GameType> OnUpdateGameType;

    public GameType GameType { get; private set; } = GameType.PvP;

    
    public static GameConfiguration Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple GameConfiguration instances");
        }
    }

    public void UpdateGameType(GameType gameType)
    {
        GameType = gameType;
        OnUpdateGameType?.Invoke(gameType);
    }
}
