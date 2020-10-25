using UnityEngine;
using UnityEngine.AI;

class ChaseNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private ZombieAI zombieAI;

    public ChaseNode(Transform target, NavMeshAgent agent, Animator animator)
    {
        this.target = target;
        this.agent = agent;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        if(agent.isOnNavMesh)
        {
            if(zombieAI.HasAgro)
            {
                agent.SetDestination(target.position);
                animator.SetFloat(ZombieAnimatorVariables.MovingName, ZombieAnimatorVariables.Run);
                state = NodeState.SUCCES;
            }
        }
        state = NodeState.FAILURE;
        
        return state;
    }
}
