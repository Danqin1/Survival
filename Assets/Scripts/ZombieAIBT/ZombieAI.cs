using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    #region variables

    public GameObject indicator;

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

    #region public methods

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

    public void SetIndicatorColor(Color color)
    {
        indicator.GetComponent<MeshRenderer>().material.color = color;
    }

    #endregion

    #region private methods

    private void ConstructBehaviourTree()
    {
        DieNode dieNode = new DieNode(this, animator);
        AttackNode attackNode = new AttackNode(animator);
        IsCloserNode isCloseToAttack = new IsCloserNode(this.transform, Player.instance.transform, agent.stoppingDistance);
        ChaseNode chaseNode = new ChaseNode(Player.instance.transform, agent, animator);
        WalkNode walkNode = new WalkNode(agent, animator, changeDestinationDelay, walkRadius);
        WasHittedNode wasHittedNode = new WasHittedNode(this);
        IsPlayerVisibleNode isPlayerVisibleNode = new IsPlayerVisibleNode(transform, zombieSettings.ZombieAgroRadius, zombieSettings.FieldsOfViewAngle, playerLayer);

        Sequence attackSequence = new Sequence(new List<Node>() { isCloseToAttack, attackNode });
        Sequence chaseSequence = new Sequence(new List<Node>() { isPlayerVisibleNode, chaseNode });
        Sequence hittedSequence = new Sequence(new List<Node>() { wasHittedNode, chaseNode });

        topNode = new Selector(new List<Node>() { dieNode, attackSequence, hittedSequence, chaseSequence, walkNode});
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}