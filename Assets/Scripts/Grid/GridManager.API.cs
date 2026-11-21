using UnityEngine;

public partial class GridManager
{
    public float GetGridSize()
    {
        return gridRenderer.GetSize;
    }
    public Vector3 GetNeighbourPosition(Vector3 playerPosition, Vector2 moveDir)
    {
        GridCell neighbourCell = GetNeighbourCell(playerPosition, moveDir); //find the neighbour cell
        return gridRenderer.GetCellCenter(neighbourCell); //get cell center
    }
    public bool IsNeighbourCellWalkable(Vector3 playerPosition, Vector2 moveDir)
    {
        GridCell neighbourCell = GetNeighbourCell(playerPosition, moveDir); 
        GridObject gridObject = grid.GetGridObject(neighbourCell);
        return gridObject.type == GridObjectType.Path;
    }
    public GridCell GetNeighbourCell(Vector3 playerPosition, Vector2 moveDir)
    {
        GridCell currentcell = gridRenderer.GetCellPosition(playerPosition); //get current grid cell (we have the world position)
        Direction dir = gridRenderer.GetDirection(moveDir); //get the direction(up, down, left, right, invalid)
        return grid.GetNeighbourCell(currentcell, dir); //find the neighbour cell
    }
    public bool HasReachedCellCenter(Vector2 moveDir, Vector3 playerPosition)
    {
        return gridRenderer.HasReachedCellCenter(moveDir, playerPosition);
    }
    public Vector3 GetCurrentCellPosition(Vector3 position)
    {
        GridCell cell = gridRenderer.GetCellPosition(position);
        return gridRenderer.GetCellCenter(cell);
    }
}