using System;
using UnityEngine;


public class GameInitializer : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    
    [SerializeField] private RealPlayer firstRealPlayer;
    
    [SerializeField] private RealPlayer secondRealPlayer;
    
    [SerializeField] private BoardView bordView;
    
    private BoardData _bordData;
    
        
    public static GameInitializer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple GameInitializer instances");
        }

        GameEvents.OnEndGame += HandleEndGame;
    }

    public void StartGame()
    {
        _bordData = new BoardData();
        _bordData.OnUpdateBoard += bordView.HandleUpdateBoard;
        
        _bordData.SetCellOwner(3, 4, CellOwner.First);
        _bordData.SetCellOwner(4, 3, CellOwner.First);
        _bordData.SetCellOwner(3, 3, CellOwner.Second);
        _bordData.SetCellOwner(4, 4, CellOwner.Second);
        
        if (GameConfiguration.Instance.GameType == GameType.PvP)
        {
            firstRealPlayer.Initialize(true);
            secondRealPlayer.Initialize(false);

            turnManager.Initialize(firstRealPlayer, secondRealPlayer, _bordData); 
            turnManager.StartGame();
            GameEvents.Instance.SendStartGame();
        }
    }

    private void HandleEndGame()
    {
        _bordData.OnUpdateBoard -= bordView.HandleUpdateBoard;
    }
}
