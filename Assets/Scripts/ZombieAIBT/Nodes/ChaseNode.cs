using UnityEngine;
using UnityEngine.AI;

class ChaseNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    public ChaseNode(Transform target, NavMeshAgent agent, Animator animator)
    {
        this.target = target;
        this.agent = agent;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
            animator.SetBool(ZombieAnimatorVariables.AttackingBool, false);
            animator.SetFloat(ZombieAnimatorVariables.MovingName, ZombieAnimatorVariables.Run);
            state = NodeState.SUCCES;
        }
        else state = NodeState.FAILURE;

        agent.GetComponent<ZombieAI>().SetIndicatorColor(Color.yellow);

        return state;
    }
}
