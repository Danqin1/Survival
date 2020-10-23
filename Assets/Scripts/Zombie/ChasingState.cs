using UnityEngine;
using UnityEngine.AI;

public class ChasingState : ZombieBehaviourState
{
    public ChasingState(ZombieStateMashine stateMashine, NavMeshAgent agent) : base(stateMashine, agent) 
    {
        zombieStateMashine.animator.SetFloat("MovingSpeed", 1);
    }

    #region public methods

    public override void Tick()
    {
        agent?.SetDestination(Player.instance.transform.position);
        if (Vector3.Distance(zombieStateMashine.transform.position, Player.instance.transform.position) < zombieStateMashine.zombieSettings.ZombieStartAttackingDistance)
        {
            Attacking();
        }
    }

    public override void Attacking()
    {
        zombieStateMashine.currentState = new AttackingState(zombieStateMashine, agent);
    }

    public override void Walking()
    {
        zombieStateMashine.currentState = new WalkingState(zombieStateMashine, agent);
    }

    #endregion
}