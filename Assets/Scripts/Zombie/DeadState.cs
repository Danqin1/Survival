using UnityEngine.AI;

public class DeadState : ZombieBehaviourState
{
    public DeadState(ZombieStateMashine stateMashine, NavMeshAgent agent, SoundsManager sounds) : base(stateMashine, agent) 
    {
        zombieStateMashine.animator.SetBool("isDead", true);
        if (sounds != null) sounds.OnZombieKilled();
    }

    public override void Tick()
    {
        agent.isStopped = true;
    }
}