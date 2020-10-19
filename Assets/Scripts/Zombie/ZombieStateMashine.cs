using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMashine
{
    #region variables

    public ZombieBehaviourState currentState;
    public Animator animator;
    public ZombieSettings zombieSettings;
    public LayerMask playerLayer;
    public Transform transform;
    private NavMeshAgent agent;
    
    #endregion

    #region properties

    public float agroRadius => zombieSettings.ZombieAgroRadius;
    public int damage => zombieSettings.ZombieDamage;
    public float fieldOfViewAngle => zombieSettings.FieldsOfViewAngle;

    #endregion

    #region Init

    public ZombieStateMashine(Animator animator, ZombieSettings zombieSettings, NavMeshAgent agent, LayerMask playerLayer)
    {
        this.animator = animator;
        this.zombieSettings = zombieSettings;
        this.agent = agent;
        this.playerLayer = playerLayer;
        currentState = new WalkingState(this, agent);
    }

    #endregion

    #region public methods

    public void SetState(ZombieBehaviourState state)
    {
        if(!currentState.GetType().Equals(state.GetType())) currentState = state;
    }

    #endregion
}