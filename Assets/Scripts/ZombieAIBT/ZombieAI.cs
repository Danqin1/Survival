using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    #region variables

    [SerializeField] private ZombieSettings zombieSettings;
    [SerializeField] private LayerMask playerLayer;
    
    [SerializeField] private float walkRadius;
    [SerializeField] private float changeDestinationDelay;

    private Animator animator;
    private NavMeshAgent agent;
    private Node topNode;

    #endregion

    #region properties

    public float Health { get; private set; } = 100;
    public bool IsDead { get; set; } = false;
    public bool Hitted { get; private set; } = false;
    public bool HasAgro { get; private set; } = false;

    #endregion

    #region Unity methods

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ConstructBehaviourTree();
    }

    private void Update()
    {
        topNode.Evaluate();
    }

    #endregion

    #region private methods

    private void ConstructBehaviourTree()
    {
        IsDeadNode isDeadNode = new IsDeadNode(this, animator);
        AttackNode attackNode = new AttackNode(animator);
        HasAgroNode hasAgroNode = new HasAgroNode(this);
        IsCloserNode isCloseToAttack = new IsCloserNode(Player.instance.transform, transform, 1.5f);
        ChaseNode chaseNode = new ChaseNode(Player.instance.transform, agent, animator);
        WalkNode walkNode = new WalkNode(agent, animator, changeDestinationDelay, walkRadius);
        WasHittedNode wasHittedNode = new WasHittedNode(this);
        IsPlayerVisibleNode isPlayerVisibleNode = new IsPlayerVisibleNode(transform, zombieSettings.ZombieAgroRadius, zombieSettings.FieldsOfViewAngle, playerLayer);

        Sequence attackSequence = new Sequence(new List<Node>() {attackNode });

        Sequence isAttackPossibleSequence = new Sequence(new List<Node>() { isCloseToAttack, attackSequence });

        Sequence agroSequence = new Sequence(new List<Node>() { hasAgroNode, chaseNode });

        Selector canAttackSelector = new Selector(new List<Node>() { isAttackPossibleSequence, agroSequence });

        Sequence playerVisibleSequence = new Sequence(new List<Node>() { isPlayerVisibleNode, canAttackSelector });

        Sequence hittedSequence = new Sequence(new List<Node>() { canAttackSelector });

        topNode = new Selector(new List<Node>() {isDeadNode, hittedSequence, playerVisibleSequence, walkNode});
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        Hitted = true;
        HasAgro = true;
        if (Health <= 0)
        {
            IsDead = true;
            return;
        }
    }

    public void DoDamage()
    {
        Player.instance.TakeDamage(zombieSettings.ZombieDamage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}