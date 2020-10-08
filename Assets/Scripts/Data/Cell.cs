public struct Cell
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
}
