using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    #region variables

    [SerializeField] private Animator animator;
    [SerializeField] private ZombieSettings zombieSettings;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private NavMeshAgent agent;

    private ZombieStateMashine zombieStateMashine;

    #endregion

    #region properties

    public float Health { get; private set; }
    public bool IsDead { get; set; }  = false;

    #endregion

    #region Unity methods

    private void Awake()
    {
        Health = zombieSettings.InitialHealth;
        zombieStateMashine = new ZombieStateMashine(animator, zombieSettings, agent, playerLayer);
    }

    private void Update()
    {
        zombieStateMashine.transform = transform;
        zombieStateMashine.currentState.Tick();
    }

    #endregion

    #region public methods

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            IsDead = true;
            zombieStateMashine.SetState(new DeadState(zombieStateMashine, agent, FindObjectOfType<SoundsManager>()));
            return;
        }
        if(zombieStateMashine.currentState is WalkingState state) zombieStateMashine.SetState(new ChasingState(zombieStateMashine, agent));
    }

    public void DoDamage()
    {
        Player.instance.TakeDamage(zombieSettings.ZombieDamage);
    }

    #endregion

    private void Die()
    {
        Destroy(gameObject);
    }
}
