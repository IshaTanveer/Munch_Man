using UnityEngine;

public class GameGrid 
{
    //private fields
    private int width, height;
    private GridObject[,] gridObjects;
    //properties and accesses
    public int Width => width;
    public int Height => height;
    public GridObject[,] GetGridObjects () => gridObjects; //access the grid from outside
    //constructor
    public GameGrid(int width, int height)
    {
        this.height = height;
        this.width = width;
        gridObjects = new GridObject[width, height]; 
        InitializeGrid();
    }
    //methods
    private void InitializeGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GridCell cellPosition = new GridCell(i, j);
                gridObjects[i, j] = new GridObject(cellPosition); //making a new GridObject at every position (populating)
            }
        }
    }
    public bool IsValidCell(GridCell cell)
    {
        if(cell.X > Width || cell.Y > height) return false;
        if(cell.X < 0 || cell.Y < 0) return false;
        return true;
    }
    public GridObject GetGridObject(GridCell cell)
    {
        return gridObjects[cell.X, cell.Y];
    }
    public GridCell GetNeighbourCell(GridCell currentCell, Direction dir)
    {
        GridCell neighbourCell;
        switch (dir)
        {
            case Direction.Up:
                neighbourCell = new GridCell(currentCell.X, currentCell.Y + 1);
                break;
            case Direction.Down:
                neighbourCell = new GridCell(currentCell.X, currentCell.Y - 1);
                break;
            case Direction.Right:
                neighbourCell = new GridCell(currentCell.X + 1, currentCell.Y);
                break;
            case Direction.Left:
                neighbourCell = new GridCell(currentCell.X - 1, currentCell.Y);
                break;
            default:
            throw new System.Exception("Invalid Direction in the GetNeighbourCell");
        }
        if(!IsValidCell(neighbourCell)) throw new System.Exception("Neighbour Cell is outside the bounds of the grid.");
        return neighbourCell;
    }
}
public readonly struct GridCell
{
    public int X {get;}
    public int Y {get;}
    public GridCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}
public enum Direction {Up, Down, Right, Left, Invalid}
