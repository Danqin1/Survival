using UnityEngine;
using UnityEngine.AI;

class WalkNode : Node
{
    private NavMeshAgent agent;
    private Animator animator;
    private float previousChangeDestinationTime;
    private float changeDestinationDelay;
    private float walkDistanceRadius;

    public WalkNode(NavMeshAgent agent, Animator animator, float changeDestinationDelay, float walkDistanceRadius)
    {
        this.agent = agent;
        this.animator = animator;
        this.changeDestinationDelay = changeDestinationDelay;
        this.walkDistanceRadius = walkDistanceRadius;
    }

    public override NodeState Evaluate()
    {
        if(agent.isOnNavMesh)
        {
            if(Time.time - previousChangeDestinationTime < changeDestinationDelay)
            {
                if(agent.remainingDistance < agent.stoppingDistance) SetRandomDirection();

                state = NodeState.SUCCES;
                return state;
            }

            animator.SetFloat(ZombieAnimatorVariables.MovingName, ZombieAnimatorVariables.Walk);
            state = NodeState.RUNNING;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }

    private void SetRandomDirection()
    {
        Vector3 newDestination = Random.insideUnitSphere * walkDistanceRadius;
        newDestination += agent.transform.position;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(newDestination, out hit, walkDistanceRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
        previousChangeDestinationTime = Time.time;
    }
}
