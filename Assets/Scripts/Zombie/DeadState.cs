using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;

public class DeadState : ZombieBehaviourState
{
    public DeadState(ZombieStateMashine stateMashine, NavMeshAgent agent) : base(stateMashine, agent) 
    {
        zombieStateMashine.animator.SetBool("isDead", true);
    }

    public override void Tick()
    {
        agent.isStopped = true;
    }
}