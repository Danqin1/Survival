using UnityEngine;
using UnityEngine.AI;

public class AttackingState : ZombieBehaviourState
{
    public AttackingState(ZombieStateMashine stateMashine, NavMeshAgent agent) : base(stateMashine, agent) 
    {
        zombieStateMashine.animator.SetBool("isAttacking", true);
    }

    #region public methods

    public override void Tick()
    {
        if(Vector3.Distance(zombieStateMashine.transform.position, Player.instance.transform.position) > zombieStateMashine.zombieSettings.StopAttackingDistance)
        {
            Chasing();
        }
    }

    public override void Chasing()
    {
        zombieStateMashine.currentState = new ChasingState(zombieStateMashine, agent);
        zombieStateMashine.animator.SetBool("isAttacking", false);
    }

    public override void Walking()
    {
        zombieStateMashine.currentState = new WalkingState(zombieStateMashine, agent);
        zombieStateMashine.animator.SetBool("isAttacking", false);
    }

    public void Attack()
    {
        Player.instance.TakeDamage(zombieStateMashine.damage);
    }

    #endregion
}
