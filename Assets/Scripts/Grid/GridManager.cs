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
    /*private void OnValidate() //when changes made in instructor initialize again
    {
        if(!Application.isPlaying) InitializeGrid();
    }*/
    private void InitializeGrid()
    {
        if(tilemap == null) return;
        grid = new GameGrid(tilemap.size.x, tilemap.size.y);
    }
    private void OnDrawGizmos()
    {
        if( tilemap == null || grid == null) return;
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position;
        GridObject[,] gridObjects = grid.GetGridObjects();
        foreach (GridObject gridObject in gridObjects)
        {
            int x = gridObject.GetCellposition().x;
            int y = gridObject.GetCellposition().y;

            Vector3 startVertical = origin + new Vector3(x, y);
            Vector3 endVertical = origin + new Vector3(x, y + 1);
            Gizmos.DrawLine(startVertical, endVertical);

            Vector3 startHorizontal = origin + new Vector3(x, y);
            Vector3 endHorizontal = origin + new Vector3(x + 1, y);
            Gizmos.DrawLine(startHorizontal, endHorizontal);

            GUIStyle textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.white;
            textStyle.alignment = TextAnchor.MiddleCenter;
            
            Vector3 cellCenter = origin + new Vector3(x + 0.5f, y + 0.5f);
            Handles.Label(cellCenter, $"({x}, {y})", textStyle);
        }
        int tilemapHeight = grid.Height;
        int tilemapWidth = grid.Width;

        Vector3 startVer = origin + new Vector3(tilemapWidth, 0);
        Vector3 endVer = origin + new Vector3(tilemapWidth, tilemapHeight);
        Gizmos.DrawLine(startVer, endVer);

        Vector3 startHor = origin + new Vector3(0, tilemapHeight);
        Vector3 endHor = origin + new Vector3(tilemapWidth, tilemapHeight);
        Gizmos.DrawLine(startHor, endHor);
    }
}
