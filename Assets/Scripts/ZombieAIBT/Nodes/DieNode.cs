using UnityEngine;

class DieNode : Node
{
    private ZombieAI zombieAI;
    private Animator animator;

    public DieNode(ZombieAI zombieAI, Animator animator)
    {
        this.zombieAI = zombieAI;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        if (zombieAI.IsDead) animator.SetBool(ZombieAnimatorVariables.DieBool, true);
        return zombieAI.IsDead ? NodeState.SUCCES : NodeState.FAILURE;
    }
}
