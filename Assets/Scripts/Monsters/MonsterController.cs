using UnityEngine;

public class MonsterController : MonoBehaviour{
    Vector2 currentTarget;
    Vector2 currentDirection;
    [SerializeField] float speed  = 3.0f;
    [SerializeField] Transform targetTransform;
    Vector2 finalTarget;
    public Vector2 FinalTarget => finalTarget;
    public Vector2 CurrentTarget {
        get => currentTarget;
        set => currentTarget = value;
    }
    public Vector2 CurrentDirection {
        get => currentDirection;
        set => currentDirection = value;
    }
    GridManager gridManager;
    void Start()
    {
        gridManager = GridManager.Instance;
        GetIntermediateTarget(transform.position, targetTransform.position);
        finalTarget = gridManager.GetCurrentCellPosition(targetTransform.position);
    }
    void Update()
    {
        finalTarget = gridManager.GetCurrentCellPosition(targetTransform.position);
        //if(gridManager.HasReachedCellCenter(currentDirection, transform.position)) //here is the problem
        if(MonsterAINavigation.HasReachedTargetCellCenter(currentDirection, transform.position, currentTarget)) 
        {
            GetIntermediateTarget(transform.position, finalTarget);
        }
        Debug.DrawRay(transform.position, (Vector3)currentTarget - transform.position, Color.red); //drawing the line
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime); //simple move
    }
    void GetIntermediateTarget(Vector3 npcPosition, Vector3 finalTarget)
    {
        (Vector2 newDir, Vector3 newTarget) result = MonsterAINavigation.GetNextInterMediateTarget(currentDirection, npcPosition, finalTarget);
        currentDirection = result.newDir;
        currentTarget = result.newTarget;
    }
}