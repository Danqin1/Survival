using UnityEngine;
using UnityEngine.AI;

public abstract class ZombieBehaviourState
{
    protected ZombieStateMashine zombieStateMashine;
    protected NavMeshAgent agent;

    public ZombieBehaviourState(ZombieStateMashine zombieStateMashine, NavMeshAgent agent)
    {
        this.zombieStateMashine = zombieStateMashine;
        this.agent = agent;
    }

    public virtual void Tick() { }
    public virtual void Chasing() { }
    public virtual void Walking() { }
    public virtual void Attacking() { }
}
