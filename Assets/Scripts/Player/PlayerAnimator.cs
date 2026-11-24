using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    private PlayerMovement playerMovement;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void OnEnable()
    {
        //playerMovement.OnMoveStateChanged += HandleMovingState;
        playerMovement.OnDirectionChanged += HandleDirectionState;
    }
    void OnDisable()
    {
        //playerMovement.OnMoveStateChanged -= HandleMovingState;
        playerMovement.OnDirectionChanged -= HandleDirectionState;
    }
    public void HandleMovingState(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }
    public void HandleDirectionState(Vector2 dir)
    {
        animator.SetInteger("MoveX", (int)dir.x);
        animator.SetInteger("MoveY", (int)dir.y);
    }
}
