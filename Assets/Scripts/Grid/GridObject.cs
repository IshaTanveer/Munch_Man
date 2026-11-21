using UnityEngine;

public class GridObject // Represents  a single gridcell
{
    //private fields
    private GridCell cellPosition;
    //properties and accesses
    public GridObjectType type {get; set;} = GridObjectType.Empty; 
    public GridCell GetCellposition() => cellPosition;
    //constructor
    public GridObject(GridCell cellPosition)
    {
        this.cellPosition = cellPosition;
    }
    //methods
    public override string ToString()
    {
        //return base.ToString();
        return "GridObject " + cellPosition;
    }
}
public enum GridObjectType {Wall, Path, Empty}
