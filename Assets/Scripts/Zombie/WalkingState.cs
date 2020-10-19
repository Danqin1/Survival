using UnityEngine;
using UnityEngine.AI;

public class WalkingState : ZombieBehaviourState
{
    public WalkingState(ZombieStateMashine stateMashine, NavMeshAgent agent) : base(stateMashine, agent) 
    {
        stateMashine.animator.SetFloat("MovingSpeed", .5f);
    }

    #region variables

    private float previousCallTime = Time.time;
    private int changeDirectionDelayInSeconds = 5;

    #endregion

    #region public methods

    public override void Tick()
    {
        if (Vector3.Distance(zombieStateMashine.transform.position, Player.instance.transform.position) < zombieStateMashine.agroRadius)
        {
            Vector3 directionToPlayer = Player.instance.transform.position - zombieStateMashine.transform.position;

            if (Vector3.Angle(zombieStateMashine.transform.forward, directionToPlayer) < zombieStateMashine.fieldOfViewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(zombieStateMashine.transform.position, directionToPlayer, out hit, zombieStateMashine.agroRadius, zombieStateMashine.playerLayer))
                {
                    if (hit.transform.gameObject.GetComponent<Player>() != null)
                    {
                        zombieStateMashine.currentState = new ChasingState(zombieStateMashine, agent);
                    }
                }
            }
        }
        else
        {
            changeDirection(zombieStateMashine.agroRadius);
        }
    }

    public override void Attacking()
    {
        zombieStateMashine.currentState = new AttackingState(zombieStateMashine, agent);
    }

    public override void Chasing()
    {
        zombieStateMashine.currentState = new ChasingState(zombieStateMashine, agent);
    }

    #endregion

    #region private

    private void changeDirection(float radius)
    {
        if (Time.time - previousCallTime > changeDirectionDelayInSeconds && agent?.remainingDistance < 1)
        {
            Vector3 newDestination = UnityEngine.Random.insideUnitSphere * radius;
            newDestination += zombieStateMashine.transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(newDestination, out hit, radius, 1))
            {
                agent?.SetDestination(hit.position);
            }
            previousCallTime = Time.time;
        }
    }

    #endregion
}
