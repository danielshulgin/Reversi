using System;
using System.Collections.Generic;
using System.Linq;

public class AIPlayer : IPlayer
{
    public event Action<int, int> OnDoTurn;
        
    private bool _firstPlayer;
        
        
    public void Initialize(bool firstPlayer)
    {
        _firstPlayer = firstPlayer;
    }

    public void StartTurn(Cell[,] currentBordData)
    {
        var possibleCells = ReversiRules.GetPossibleTurns(currentBordData, _firstPlayer);

        var cellValue = new Dictionary<Cell, int>();

        foreach (var cell in possibleCells)
        {
            cellValue[cell] = 1;
        }

        var optimalTurn = possibleCells.First();
         
        foreach (var cell in cellValue.Keys)
        {
            if (cellValue[cell] > cellValue[optimalTurn])
            {
                optimalTurn = cell;
            }
        }
        
        OnDoTurn?.Invoke(optimalTurn.x, optimalTurn.y);
    }
}
