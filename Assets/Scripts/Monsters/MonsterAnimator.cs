using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void HandleDirectionState(Vector2 dir)
    {
        animator.SetInteger("MoveX", (int)dir.x);
        animator.SetInteger("MoveY", (int)dir.y);
    }
    public void HandleDefaultState(bool isDefault)
    {
        animator.SetBool("isDefault", isDefault);
    }
    public void HandleEatenState(bool isEaten)
    {
        animator.SetBool("isEaten", isEaten);
    }
    public void HandleScaredState(bool isScared)
    {
        animator.SetBool("isScared", isScared);
    }
}
