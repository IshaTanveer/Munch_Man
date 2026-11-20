using UnityEngine;

public class GameGrid 
{
    private int width, height;
    public int Width => width;
    public int Height => height;
    private GridObject[,] gridObjects;
    public GameGrid(int width, int height)
    {
        this.height = height;
        this.width = width;
        gridObjects = new GridObject[width, height]; 
        InitializeGrid();
    }
    private void InitializeGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2Int cellPosition = new Vector2Int(i, j);
                gridObjects[i, j] = new GridObject(cellPosition); //making a new GridObject at every position (populating)
            }
        }
    }
    public GridObject[,] GetGridObjects () => gridObjects; //access the grid from outside
}
