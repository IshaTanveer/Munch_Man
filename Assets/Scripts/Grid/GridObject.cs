using UnityEngine;

public class GridObject // Represents  a single gridcell
{
    private Vector2Int cellPosition;
    public GridObject(Vector2Int cellPosition)
    {
        this.cellPosition = cellPosition;
    }
    public override string ToString()
    {
        //return base.ToString();
        return "GridObject " + cellPosition;
    }
    public Vector2Int GetCellposition() => cellPosition;
}
