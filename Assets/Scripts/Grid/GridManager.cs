using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways] //run in the scene
public class GridManager : MonoBehaviour //visualize our grid
{
    [SerializeField] private Tilemap tilemap;
    private GameGrid grid;
    private static GridManager Instance; //Singleton
    public GridManager GetGridManager() => Instance;
    private bool showGrid = true;
    GridRenderer gridRenderer;
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
        tilemap.CompressBounds();//recalculate the bounds
        grid = new GameGrid(tilemap.size.x, tilemap.size.y);
        gridRenderer = new GridRenderer(tilemap.origin);
    }
    private void OnDrawGizmos()
    {
        if( tilemap == null || grid == null || showGrid == false) return;
        Gizmos.color = Color.yellow;

        GridObject[,] gridObjects = grid.GetGridObjects();
        foreach (GridObject gridObject in gridObjects)
        {
            int cellPosx = gridObject.GetCellposition().x;
            int cellPosy = gridObject.GetCellposition().y;
            int worldPosX = (int)gridRenderer.GetWorldPosition(cellPosx, cellPosy).x;
            int worldPosY = (int)gridRenderer.GetWorldPosition(cellPosx, cellPosy).y;

            Vector3 startVertical = new Vector3(worldPosX, worldPosY);
            Vector3 endVertical = new Vector3(worldPosX, worldPosY + 1);
            Gizmos.DrawLine(startVertical, endVertical);

            Vector3 startHorizontal = new Vector3(worldPosX, worldPosY);
            Vector3 endHorizontal = new Vector3(worldPosX + 1, worldPosY);
            Gizmos.DrawLine(startHorizontal, endHorizontal);

            GUIStyle textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.white;
            textStyle.alignment = TextAnchor.MiddleCenter;
            
            Vector3 cellCenter = new Vector3(worldPosX + 0.5f, worldPosY + 0.5f);
            Handles.Label(cellCenter, $"({cellPosx}, {cellPosy})", textStyle);
        }
        int tilemapHeight = grid.Height;
        int tilemapWidth = grid.Width;

        int originWorldSpaceX = (int)gridRenderer.GetWorldPosition(0,0).x;
        int originWorldSpaceY = (int)gridRenderer.GetWorldPosition(0,0).y;

        int widthWorldSpace = (int)gridRenderer.GetWorldPosition(tilemapWidth, tilemapHeight).x;
        int heightWorldSpace = (int)gridRenderer.GetWorldPosition(tilemapWidth, tilemapHeight).y;

        Vector3 startVer = new Vector3(widthWorldSpace, originWorldSpaceY);
        Vector3 endVer = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(startVer, endVer);

        Vector3 startHor = new Vector3(originWorldSpaceX, heightWorldSpace);
        Vector3 endHor = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(startHor, endHor);
    }
    public void RegenerateGrid()
    {
        InitializeGrid();
    }
    public void ToggleGrid()
    {
        showGrid = !showGrid;
    }
}
