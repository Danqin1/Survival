using UnityEngine;

class DieNode : Node
{
    private Animator animator;

    public DieNode(Animator animator)
    {
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        animator.SetBool(ZombieAnimatorVariables.DieBool, true);
        state = NodeState.SUCCES;
        return state;
    }
}
