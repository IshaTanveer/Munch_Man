using UnityEngine;

public class PathVisuallizerSetup : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetUp(Color color, Vector2 direction)
    {
        spriteRenderer.color = color;
        float ZAxisAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0,0,ZAxisAngle);
    }
}
