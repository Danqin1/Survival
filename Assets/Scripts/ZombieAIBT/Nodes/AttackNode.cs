using UnityEngine;

class AttackNode : Node
{
    private Animator animator;

    public AttackNode(Animator animator)
    {
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        animator.SetBool(ZombieAnimatorVariables.AttackingBool, true);

        int attackNumber = Random.Range(0, 3);
        float attack = 0;
        switch (attackNumber)
        {
            case 0:
                attack = 0;
                break;
            case 1:
                attack = .5f;
                break;
            case 2:
                attack = 1;
                break;
            default:
                break;
        }

        animator.SetFloat(ZombieAnimatorVariables.AttackNumber, attack);

        state = NodeState.SUCCES;
        return state;
    }
}
