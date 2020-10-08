using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReversiRules
{
    public static List<List<Cell>> GetRivalLinesAroundCell(Cell[,] board, int x, int y, bool firstPlayer)
    {
        var result = new List<List<Cell>>();
        
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            result.Add(GetRivalCellsInDirection(board, x, y, firstPlayer, direction));
        }
        
        return result;
    }

    public static List<Cell> GetRivalCellsInDirection(Cell[,] board, int x, int y, bool firstPlayerTurn, Direction direction)
    {
        var (xFunction, yFunction) = GetCoordinatesMoveFunctions(direction);

        var xi = xFunction(x);
        var yi = yFunction(y);
        
        var turnOwner = BoolToTurnOwner(firstPlayerTurn);
        var rivalOwner = InverseCellOwner(turnOwner);
        
        var resultLine = new List<Cell>();
        while (!OutOfBoard(xi, yi) && board[xi, yi].cellOwner == rivalOwner)
        {
            resultLine.Add(board[xi, yi]);
            xi = xFunction(xi);
            yi = yFunction(yi);
        }
        
        if (OutOfBoard(xi, yi) || board[xi, yi].cellOwner != turnOwner)
        {
            return new List<Cell>();
        }
        
        return resultLine;
    }
    
    public static Tuple<Func<int, int>, Func<int, int>> GetCoordinatesMoveFunctions(Direction direction)
    {
        Func<int, int> xFunction = xv => xv + 1;
        Func<int, int> yFunction = yv => yv + 1;
        switch (direction)
        {
            case Direction.N:
                xFunction = xv => xv;
                yFunction = yv => yv + 1;
                break;
            case Direction.NE:
                xFunction = xv => xv + 1;
                yFunction = yv => yv + 1;
                break;
            case Direction.E:
                xFunction = xv => xv + 1;
                yFunction = yv => yv;
                break;
            case Direction.SE:
                xFunction = xv => xv + 1;
                yFunction = yv => yv - 1;
                break;
            case Direction.S:
                xFunction = xv => xv;
                yFunction = yv => yv - 1;
                break;
            case Direction.SW:
                xFunction = xv => xv - 1;
                yFunction = yv => yv - 1;
                break;
            case Direction.W:
                xFunction = xv => xv - 1;
                yFunction = yv => yv;
                break;
            case Direction.NW:
                xFunction = xv => xv - 1;
                yFunction = yv => yv + 1;
                break;
        }
        
        return new Tuple<Func<int, int>, Func<int, int>>(xFunction, yFunction);
    }

    public static bool OutOfBoard(int x, int y)
    {
        return y > 7 || y < 0 || x > 7 || x < 0;
    }

    public static bool BoardFull(Cell[,] board)
    {
        for (var x = 0; x < 8;x++) 
        {
            for ( int y = 0; y < 8;y++)
            {
                if (board[x, y].cellOwner == CellOwner.None)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public static GameResultType GetGameResultType(Cell[,] board)
    {
        var firstPlayerChipCount = 0;
        var secondPlayerChipCount = 0;
        for (var x = 0; x < 8;x++) 
        {
            for ( int y = 0; y < 8;y++)
            {
                if (board[x, y].cellOwner == CellOwner.First)
                {
                    ++firstPlayerChipCount;
                }

                if (board[x, y].cellOwner == CellOwner.Second)
                {
                    ++secondPlayerChipCount;
                }
            }
        }
        if (firstPlayerChipCount > secondPlayerChipCount)
        {
            return GameResultType.FirstWon;
        }
        if (firstPlayerChipCount < secondPlayerChipCount)
        {
            return GameResultType.FirstWon;
        }
        return GameResultType.Draw;
    }

    public static List<Cell> GetPossibleTurns(Cell[,] board, bool firstPlayerTurn)
    {
        var resultCells = new List<Cell>();
        for (var x = 0; x < 8; x++)
        {
            for (var y = 0; y < 8; y++)
            {
                if (board[x, y].cellOwner == CellOwner.None)
                {
                    if (CanPutCell(board, x, y, firstPlayerTurn))
                    {
                        resultCells.Add(board[x, y]);
                    }
                }
            }    
        }
        return resultCells;
    }

    public static bool CanPutCell(Cell[,] board, int x, int y, bool firstPlayerTurn)
    {
        var possibleLines =
            ReversiRules.GetRivalLinesAroundCell(board, x, y, firstPlayerTurn);

        return possibleLines.Find(line => line.Count > 0) != null;
    }

    public static CellOwner InverseCellOwner(CellOwner cellOwner)
    {
        return cellOwner == CellOwner.First ? CellOwner.Second : CellOwner.First;
    }

    public static CellOwner BoolToTurnOwner(bool firstPlayer)
    {
        return firstPlayer ? CellOwner.First : CellOwner.Second;
    }
}
