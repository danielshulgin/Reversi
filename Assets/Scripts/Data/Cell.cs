using System;

public struct Cell : IEquatable<Cell>
{
    public CellOwner cellOwner;

    public int x;
    
    public int y;

    public Cell(int x, int y, CellOwner cellOwner)
    {
        this.cellOwner = cellOwner;
        this.x = x;
        this.y = y;
    }

    public bool Equals(Cell other)
    {
        return cellOwner == other.cellOwner && x == other.x && y == other.y;
    }

    public override bool Equals(object obj)
    {
        return obj is Cell other && Equals(other);
    }
}
