using System;

public class BoardData
{
    public event Action<Cell[,]> OnUpdateBoard;
    
    private readonly Cell[,] _cells;

    public Cell[,] Cells => (Cell[,])_cells.Clone();

    public BoardData()
    {
        _cells = new Cell[8,8];
        for (var x = 0; x < 8;x++) 
        {
            for ( int y = 0; y < 8;y++)
            {
                _cells[x, y] = new Cell(x, y, CellOwner.None);
            }
        }
    }

    public void SetCellOwner(int x, int y, CellOwner cellOwner)
    {
        _cells[x, y] = new Cell(x, y, cellOwner);
        if (cellOwner != CellOwner.None)
        {
            var possibleLines = ReversiRules.GetRivalLinesAroundCell(_cells, x, y, cellOwner == CellOwner.First);
            
            foreach (var possibleLine in possibleLines)
            {
                foreach (var cell in possibleLine)
                {
                    _cells[cell.x, cell.y] = new Cell(cell.x, cell.y, cellOwner);
                }
            }
        }
        OnUpdateBoard?.Invoke(Cells);
    }
}
