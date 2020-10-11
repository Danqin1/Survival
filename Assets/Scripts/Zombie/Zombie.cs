using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Rigidbody))]
public class Zombie : MonoBehaviour
{
    #region variables

    public SurviveSettings surviveSettings;

    [SerializeField] private LayerMask playerLayer;

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
        agent.stoppingDistance = 1.5f;
        hasAgro = false;
        changeDirectionCR = false;
    }

    private void Update()
    {
        CheckFieldOfView();

        if(!hasAgro && targetPosition != Vector3.zero && agent != null)
        {
            agent.SetDestination(targetPosition);
        }

        if(!hasAgro & !changeDirectionCR)
        {
            StartCoroutine(changeDirection(agroRadius));
        }

        SetAnimation(); 
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

    #endregion

    #region public methods

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            IsDead = true;
            animator.SetBool("isDead", true);
        }
        hasAgro = true;
        wasHitted = true;
        if (agent)
        {
            agent.SetDestination(Player.instance.transform.position);
        }
    }

    #endregion


    #region private methods

    private void DoDamage(Player player)
    {
        player.TakeDamage(damage);
    }

    private void SetAnimation()
    {
        if (animator && agent)
        {

            if (!hasAgro) animator.SetBool("isWalking", (agent.velocity.magnitude > .5f ? true : false));
            else animator.SetBool("isWalking", false);
            if (hasAgro) animator.SetBool("isRunning", (agent.velocity.magnitude > 1f ? true : false));
            else animator.SetBool("isRunning", false);
            if (agent.remainingDistance < 1.5f && hasAgro) animator.SetBool("isAttacking", true);
            else animator.SetBool("isAttacking", false);
        }
    }

    private void CheckFieldOfView()
    {
        if (Vector3.Distance(transform.position, Player.instance.transform.position) < agroRadius)
        {
            Vector3 directionToPlayer = Player.instance.transform.position - transform.position;

            if (Vector3.Angle(transform.forward , directionToPlayer) < 70)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, agroRadius, playerLayer))
                {
                    if (hit.transform.gameObject.GetComponent<Player>() != null )
                    {
                        GetAgro();
                    }
                    else DontAttack();
                }
            }
            else DontAttack();
        }
        else  DontAttack();
    }

    private void GetAgro()
    {
        if (!hasAgro)
        {
            Player.OnTakeAgro += Agro;
            hasAgro = true;
            agent.speed = 2;
        }   
    }
    private void DontAttack()
    {
        if (hasAgro && !wasHitted)
        {
            Player.OnTakeAgro -= Agro;
            agent.SetDestination(transform.position);
            hasAgro = false;
            agent.speed = 1;
        }    
    }

    private void Agro()
    {
        if (Player.instance && agent)
        {
            agent.SetDestination(Player.instance.transform.position);
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
