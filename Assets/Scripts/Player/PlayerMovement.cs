using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    private bool shouldMove = false;
    //Vector2 moveInput;
    Vector2 previousInputDir;
    Vector2 currentInputDir;
    Vector2 moveTarget;
    float playerSize;
    GridManager grid;
    //public event Action<bool> OnMoveStateChanged;
    public event Action<Vector2> OnDirectionChanged;
    void Start()
    {
        shouldMove = true;
        currentInputDir = Vector2.right;
        previousInputDir = Vector2.right;
        playerSize = GetComponent<SpriteRenderer>().bounds.size.x;
        grid = GridManager.Instance;
    }
    void Update()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        if (!shouldMove) return;

        if (!grid.HasReachedCellCenter(previousInputDir, transform.position)) moveTarget = GetTarget(previousInputDir);
        else moveTarget = GetTarget(currentInputDir);
        Move();
    }

    private void Move()
    {
        Debug.DrawRay(transform.position, (Vector3)moveTarget - transform.position, Color.cyan);
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) { 
            //shouldMove = true; 
           // OnMoveStateChanged?.Invoke(true);
            return; 
        }
        if(context.phase == InputActionPhase.Canceled) { 
            //shouldMove = false; 
            //OnMoveStateChanged?.Invoke(false);
            return; 
        }
        Vector2 newDirection = context.ReadValue<Vector2>();
        if(!IsValidDirection(newDirection)) return;

        //fix for when oscillating, when player only needs to switch direction
        HandleDirectionsSwitch(newDirection);
        OnDirectionChanged?.Invoke(newDirection);
        
    }
    void HandleDirectionsSwitch(Vector2 moveDir)
    {
        previousInputDir = currentInputDir;//turn
        currentInputDir = moveDir; //direction switch
        if (ShouldSwitchDirection())
        {
            previousInputDir = moveDir;
            currentInputDir = moveDir;
        }
    }
    bool ShouldSwitchDirection() //to find if it is a direction switch 
    {
        if(currentInputDir == previousInputDir) return false;
        if(Mathf.Abs(previousInputDir.x) != Mathf.Abs(currentInputDir.x) ||
           Mathf.Abs(previousInputDir.y) != Mathf.Abs(currentInputDir.y)) return false;
        return true;
    }
    bool IsValidDirection(Vector2 dir)
    {
        if(Math.Abs(dir.x) == 1 && Math.Abs(dir.y) == 0) return true;
        if(Math.Abs(dir.x) == 0 && Math.Abs(dir.y) == 1) return true;
        return false;
    }
    Vector3 GetTarget(Vector2 moveDir)
    {
        if(moveDir == Vector2.zero) return transform.position;
        if(grid.IsNeighbourCellWalkable(transform.position, moveDir))
        {
            return grid.GetNeighbourPosition(transform.position, moveDir);
        }
        return grid.GetCurrentCellPosition(transform.position);
    }
}
