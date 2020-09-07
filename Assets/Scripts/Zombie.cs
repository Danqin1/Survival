using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour
{
    private const float AGRO_RADIUS = 20f;
    private NavMeshAgent agent;
    private MeshCollider meshCollider;
    Vector3 finalPos = Vector3.zero;
    public LayerMask playerLayer;
    [SerializeField] private bool hasAgro;
    bool changeDirectionCR;
    public bool isDead = false;
    private bool wasHitted = false;

    private float Health = 100;

    Animator animator;
    Rigidbody rb;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.5f;
        hasAgro = false;
        changeDirectionCR = false;
        animator = GetComponent<Animator>();
        meshCollider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFieldOfView();
        if(!hasAgro && finalPos != Vector3.zero)
        {
            agent.SetDestination(finalPos);
        }
        if(!hasAgro & !changeDirectionCR)
        {
            StartCoroutine(changeDirection(AGRO_RADIUS));
        }
        if(animator && agent)
        {
            if(!hasAgro) animator.SetBool("isWalking", (agent.velocity.magnitude > .5f ? true : false));
                else animator.SetBool("isWalking", false);
            if (hasAgro) animator.SetBool("isRunning", (agent.velocity.magnitude > 1f ? true : false));
                else animator.SetBool("isRunning", false);
            if(agent.remainingDistance < 1.5f && hasAgro) animator.SetBool("isAttacking", true);
                else animator.SetBool("isAttacking", false);
        }   
    }
    void CheckFieldOfView()
    {
        if (Vector3.Distance(transform.position, Player.instance.GetPosition()) < AGRO_RADIUS)
        {
            
            Vector3 directionToPlayer = Player.instance.GetPosition() - GetPosition();
            if (Vector3.Angle(transform.forward , directionToPlayer) < 70)
            {
                
                RaycastHit hit;
                if (Physics.Raycast(GetPosition(), directionToPlayer, out hit, AGRO_RADIUS,playerLayer))
                {
                    if (hit.transform.gameObject.CompareTag("Player") )
                    {
                        Attack();
                    }
                    else DontAttack();
                }
            }
            else DontAttack();
        }
        else  DontAttack();
    }
    void Attack()
    {
        if (!hasAgro)
        {
            Player.OnTakeAgro += Agro;
            hasAgro = true;
            agent.speed = 2;
        }   
    }
    void DontAttack()
    {
        if (hasAgro && !wasHitted)
        {
            Player.OnTakeAgro -= Agro;
            agent.SetDestination(transform.position);
            hasAgro = false;
            agent.speed = 1;
        }    
    }
    Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Agro()
    {
        if (Player.instance && agent)
        {
            agent.SetDestination(Player.instance.GetPosition());
        }
    }
    public void TakeDamage(float dmg)
    {
        if (Health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
        hasAgro = true;
        wasHitted = true;
        if(agent)
        {
            agent.SetDestination(Player.instance.GetPosition());
        }
        Health -= dmg;
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator changeDirection(float radius)
    {
        changeDirectionCR = true;
        Vector3 newDestination = Random.insideUnitSphere * radius;
        newDestination += transform.position;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(newDestination, out hit, radius, 1))
        {
            finalPos = hit.position;
        }
        yield return new WaitForSeconds(5);
        changeDirectionCR = false;
    }
}
