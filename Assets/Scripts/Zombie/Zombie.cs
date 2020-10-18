using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Zombie : MonoBehaviour
{
    #region variables

    [SerializeField] private SurviveSettings surviveSettings;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float fieldOfViewAngle = 70;

    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rb;
    private Vector3 targetPosition = Vector3.zero;
    
    private bool changeDirectionCR;
    private bool wasHitted = false;
    private bool hasAgro;

    private float Health = 100;
    private float agroRadius;
    private int damage;
    private float atackDelay;
    private float previousTime = 0;

    #endregion

    #region properties

    public bool IsDead { get; set; }  = false;

    #endregion

    #region Unity methods

    private void Start()
    {
        damage = surviveSettings.ZombieDamage;
        agroRadius = surviveSettings.ZombieAgroRadius;
        atackDelay = surviveSettings.ZombieDamageRate;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1f;
        hasAgro = false;
        changeDirectionCR = false;
    }

    private void Update()
    {
        if(!IsDead)
        {
            CheckFieldOfView();

            if (!hasAgro && targetPosition != Vector3.zero && agent != null)
            {
                agent.SetDestination(targetPosition);
            }

            if (!hasAgro & !changeDirectionCR)
            {
                StartCoroutine(changeDirection(agroRadius));
            }

            SetAnimation();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (Time.realtimeSinceStartup - previousTime > atackDelay)
            {
                DoDamage(collision.gameObject.GetComponent<Player>());
                previousTime = Time.realtimeSinceStartup;
            }
        }
        
    }

    private void OnDestroy()
    {
        Player.OnTakeAgro -= FollowPlayer;
    }

    #endregion

    #region public methods

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            IsDead = true;
            animator.SetBool("isDead", true);
            agent.isStopped = true;
            return;
        }
        wasHitted = true;
        if (agent)
        {
            StartFollowingPlayer();
        }
    }

    #endregion


    #region private methods

    private void CheckFieldOfView()
    {
        if (Vector3.Distance(transform.position, Player.instance.transform.position) < agroRadius)
        {
            Vector3 directionToPlayer = Player.instance.transform.position - transform.position;

            if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfViewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, agroRadius, playerLayer))
                {
                    if (hit.transform.gameObject.GetComponent<Player>() != null)
                    {
                        StartFollowingPlayer();
                    }
                    else StopAttacking();
                }
            }
            else StopAttacking();
        }
        else StopAttacking();
    }

    private void DoDamage(Player player)
    {
        player.TakeDamage(damage);
    }

    private void SetAnimation()
    {
        if (animator)
        {
            if (!hasAgro) animator.SetFloat("MovingSpeed", .5f);

            if (hasAgro) animator.SetFloat("MovingSpeed", 1 );

            if (Vector3.Distance(transform.position, Player.instance.transform.position) < 1)
            {
                animator.SetBool("isAttacking", true);
            }
            else animator.SetBool("isAttacking", false);
        }
    }

    private void StartFollowingPlayer()
    {
        if (!hasAgro)
        {
            Player.OnTakeAgro += FollowPlayer;
            hasAgro = true;
        }   
    }

    private void FollowPlayer()
    {
        if (Player.instance && agent)
        {
            agent.SetDestination(Player.instance.transform.position);
        }
    }

    private void StopAttacking()
    {
        if (hasAgro && !wasHitted)
        {
            Player.OnTakeAgro -= FollowPlayer;
            agent.SetDestination(transform.position);
            hasAgro = false;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator changeDirection(float radius)
    {
        changeDirectionCR = true;
        Vector3 newDestination = Random.insideUnitSphere * radius;
        newDestination += transform.position;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(newDestination, out hit, radius, 1))
        {
            targetPosition = hit.position;
        }

        yield return new WaitForSeconds(5);
        changeDirectionCR = false;
    }

    #endregion
}
