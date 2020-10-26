using UnityEngine;

class AttackNode : Node
{
    private Animator animator;
    private int randomNumber;
    private float attack;

    public AttackNode(Animator animator)
    {
        this.animator = animator;
        randomNumber = Random.Range(0, 3);
        attack = 0;
        switch (randomNumber)
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
    }

    public override NodeState Evaluate()
    {
        animator.SetBool(ZombieAnimatorVariables.AttackingBool, true);
        animator.SetFloat(ZombieAnimatorVariables.AttackNumber, attack);

        animator.gameObject.GetComponent<ZombieAI>().SetIndicatorColor(Color.red);

        state = NodeState.SUCCES;
        return state;
    }
}
