using UnityEngine;

public class GridRenderer 
{
    Vector3 origin;
    public GridRenderer(Vector3 origin)
    {
        this.origin = origin;
    }
    public Vector3 GetWorldPosition(int x, int y) //translate cell position into world positions
    {
        return new Vector3(x, y, 0) + origin;
    }
}
