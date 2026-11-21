using UnityEngine;

public class GridRenderer 
{
    Vector3 origin;
    float size = 1.0f;
    GameGrid grid;
    public GridRenderer(Vector3 origin, GameGrid grid)
    {
        this.origin = origin;
        this.grid = grid;
    }
    public float GetSize => size;
    public Vector3 GetWorldPosition(GridCell cell) //translate cell position into world positions
    {
        return new Vector3(cell.X, cell.Y, 0) + origin;
    }
    public GridCell GetCellPosition(Vector3 position)
    {
        Vector3 cellPosition = position - origin;
        var cellCandidate =  new GridCell(Mathf.FloorToInt(cellPosition.x), Mathf.FloorToInt(cellPosition.y));
        if(!grid.IsValidCell(cellCandidate)) throw new System.Exception("current cell is outside the bounds of the grid.");
        return cellCandidate;
    }
    public Direction GetDirection(Vector2 moveInput)
    {
        if(moveInput == Vector2.up) return Direction.Up;
        if(moveInput == Vector2.down) return Direction.Down;
        if(moveInput == Vector2.left) return Direction.Left;
        if(moveInput == Vector2.right) return Direction.Right;
        return Direction.Invalid;
    }
    public Vector3 GetCellCenter(GridCell cell)
    {
        return GetWorldPosition(cell) + new Vector3(size/2, size/2, 0);
    }
    public bool HasReachedCellCenter(Vector2 moveDir, Vector3 playerPosition)
    {
        Direction direction = GetDirection(moveDir);
        Vector3 posIngridcoordinates = GetPositionInWorldCoordinates(playerPosition);

        float xDistanceFromCellStart = posIngridcoordinates.x - Mathf.Floor(posIngridcoordinates.x); //remove the part before decimal
        float yDistanceFromCellStart = posIngridcoordinates.y - Mathf.Floor(posIngridcoordinates.y);

        float distanceToCenter = size/2;

        switch (direction)
        {
            case Direction.Up : return yDistanceFromCellStart >= distanceToCenter;
            case Direction.Down : return yDistanceFromCellStart <= distanceToCenter;
            case Direction.Right : return xDistanceFromCellStart >= distanceToCenter;
            case Direction.Left : return xDistanceFromCellStart <= distanceToCenter;
            default: return false;
        }
    }
    public Vector3 GetPositionInWorldCoordinates(Vector3 worldPosition)
    {
        return worldPosition - origin;
    }
}
