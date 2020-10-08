using System;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    private IPlayer _first;
    
    private IPlayer _second;
    
    private BoardData _boardData;

    private bool _firstPlayerTurn;

    public GameResultType GameResultType { get; private set; }


    public void Initialize(IPlayer firstPlayer, IPlayer secondPlayer, BoardData boardData)
    {
        _boardData = boardData;
        _first = firstPlayer;
        _second = secondPlayer;
        _firstPlayerTurn = true;
    }

    public void StartGame()
    {
        _first.StartTurn(_boardData.Cells);
        _first.OnDoTurn += HandleDoTurn;
    }

    private void StartTurn()
    {
        if (_firstPlayerTurn)
        {
            _first.StartTurn(_boardData.Cells);
            _first.OnDoTurn += HandleDoTurn;
        }
        else
        {
            _second.StartTurn(_boardData.Cells);
            _second.OnDoTurn += HandleDoTurn;
        }
    }
    
    private void HandleDoTurn(int x, int y)
    {
        if (_boardData.Cells[x, y].cellOwner == CellOwner.None)
        {
            _boardData.SetCellOwner(x, y, _firstPlayerTurn ? CellOwner.First : CellOwner.Second);    
        }

        _first.OnDoTurn -= HandleDoTurn;
        _second.OnDoTurn -= HandleDoTurn;
        
        if (CheckEndGame())
        {
            GameResultType = ReversiRules.GetGameResultType(_boardData.Cells);
            GameEvents.Instance.SendEndGame();
            return;
        }

        if (ReversiRules.GetPossibleTurns(_boardData.Cells, !_firstPlayerTurn).Count > 0)
        {
            _firstPlayerTurn = !_firstPlayerTurn;
        }
        
        StartTurn();
    }
    
    private bool CheckEndGame()
    {
        if (ReversiRules.BoardFull(_boardData.Cells))
        {
            GameEvents.Instance.SendEndGame();
            return true;
        }

        return false;
    }
}
