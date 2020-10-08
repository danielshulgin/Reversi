using System;
using UnityEngine;

public class RealPlayer : MonoBehaviour, IPlayer
{
    public event Action<int, int> OnDoTurn;
    
    [SerializeField] private PlayerInput playerInput;
    
    private Cell[,] _currentBordData;
    
    private bool _firstPlayer;
    
    
    public void Initialize(bool firstPlayer)
    {
        _firstPlayer = firstPlayer;
    }

    public void StartTurn(Cell[,] currentBordData)
    {
        PlayerInput.OnTouchBoard += HandlePlayerClick;
        _currentBordData = currentBordData;
    }

    public void HandlePlayerClick(int x, int y)
    {
        if (!ReversiRules.CanPutCell(_currentBordData, x, y, _firstPlayer))
        {
            return;
        }
        
        OnDoTurn?.Invoke(x, y);
        PlayerInput.OnTouchBoard -= HandlePlayerClick;
    }
}
