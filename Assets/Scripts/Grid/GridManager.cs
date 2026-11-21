using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways] //run in the scene
public partial class GridManager : MonoBehaviour //initialize and visualize our grid
{
    public static GridManager Instance  {get; private set;} //Singleton
    public GridManager GetGridManager() => Instance;
    private GameGrid grid;
    private bool showGrid = false;
    [SerializeField] private Tilemap tilemap;
    GridRenderer gridRenderer;
    //methods
    void OnEnable()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        InitializeGrid();
    }
    private void InitializeGrid()
    {
        if(tilemap == null) return;
        tilemap.CompressBounds();//recalculate the bounds when shrunk
        grid = new GameGrid(tilemap.size.x, tilemap.size.y);
        gridRenderer = new GridRenderer(tilemap.origin, grid);

        foreach (GridObject gridObject in grid.GetGridObjects())
        {
            //Get tile position
            int cellPosX = gridObject.GetCellposition().X;
            int cellPosY = gridObject.GetCellposition().Y;
            //convert tile position to world position
            Vector3 worldPosition = gridRenderer.GetWorldPosition(gridObject.GetCellposition());

            int xVal = Mathf.FloorToInt(worldPosition.x);
            int yVal = Mathf.FloorToInt(worldPosition.y);
            //get the tile
            TileBase tile = tilemap.GetTile(new Vector3Int(xVal, yVal, 0));

            if(tile == null) continue;
            //Debug.Log($"cell ({cellPosX},{cellPosY}) tilemap cell ({xVal},{yVal}) -> {tile.name} / {tile.GetType().Name}");
            //get the tile type 
            Type tileType = tile.GetType();
            if(tileType == typeof(WallRuleTile))
            {
                gridObject.type = GridObjectType.Wall;
            }
            if(tileType == typeof(PathRuleTile))
            {
                gridObject.type = GridObjectType.Path;
            }
        }
    }
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if( tilemap == null || grid == null || showGrid == false) return;
        Gizmos.color = Color.yellow;

        GridObject[,] gridObjects = grid.GetGridObjects();
        foreach (GridObject gridObject in gridObjects)
        {
            int cellPosx = gridObject.GetCellposition().X;
            int cellPosy = gridObject.GetCellposition().Y;
            int worldPosX = (int)gridRenderer.GetWorldPosition(new GridCell(cellPosx, cellPosy)).x;
            int worldPosY = (int)gridRenderer.GetWorldPosition(new GridCell(cellPosx, cellPosy)).y;

            Vector3 startVertical = new Vector3(worldPosX, worldPosY);
            Vector3 endVertical = new Vector3(worldPosX, worldPosY + 1);
            Gizmos.DrawLine(startVertical, endVertical);

            Vector3 startHorizontal = new Vector3(worldPosX, worldPosY);
            Vector3 endHorizontal = new Vector3(worldPosX + 1, worldPosY);
            Gizmos.DrawLine(startHorizontal, endHorizontal);

            Vector3 cellCenter = new Vector3(worldPosX + 0.5f, worldPosY + 0.5f);
            
            if(gridObject.type == GridObjectType.Wall)
            {
                GUIStyle wallTextStyle = new GUIStyle();
                wallTextStyle.normal.textColor = Color.red;
                wallTextStyle.alignment = TextAnchor.MiddleCenter;
                wallTextStyle.fontStyle = FontStyle.Bold;
                Handles.Label(cellCenter, $"Wall", wallTextStyle);
            }
            if(gridObject.type == GridObjectType.Path)
            {
                GUIStyle wallTextStyle = new GUIStyle();
                wallTextStyle.normal.textColor = Color.yellow;
                wallTextStyle.alignment = TextAnchor.MiddleCenter;
                wallTextStyle.fontStyle = FontStyle.Bold;
                Handles.Label(cellCenter, $"Path", wallTextStyle);
            }
            else if(gridObject.type == GridObjectType.Empty)
            {
                GUIStyle textStyle = new GUIStyle();
                textStyle.normal.textColor = Color.white;
                textStyle.alignment = TextAnchor.MiddleCenter;
                Handles.Label(cellCenter, $"({cellPosx}, {cellPosy})", textStyle);
            }
        }
        int tilemapHeight = grid.Height;
        int tilemapWidth = grid.Width;

        int originWorldSpaceX = (int)gridRenderer.GetWorldPosition(new GridCell(0,0)).x;
        int originWorldSpaceY = (int)gridRenderer.GetWorldPosition(new GridCell(0,0)).y;

        int widthWorldSpace = (int)gridRenderer.GetWorldPosition(new GridCell(tilemapWidth, tilemapHeight)).x;
        int heightWorldSpace = (int)gridRenderer.GetWorldPosition(new GridCell(tilemapWidth, tilemapHeight)).y;

        Vector3 startVer = new Vector3(widthWorldSpace, originWorldSpaceY);
        Vector3 endVer = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(startVer, endVer);

        Vector3 startHor = new Vector3(originWorldSpaceX, heightWorldSpace);
        Vector3 endHor = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(startHor, endHor);
    }
    #endif
    public void RegenerateGrid()
    {
        InitializeGrid();
    }
    public void ToggleGrid()
    {
        showGrid = !showGrid;
    }
}
