using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Follow,
        Return
    }

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    private NavMeshAgent agent;
    private Vector3 defaultPos;

    [Header("Chasing")]
    [SerializeField] private float range;
    [SerializeField] private bool playerInRange;

    public float privacy;

    #region Distance Tracking
    private Vector3 distanceToDefault;
    private Vector3 distanceToPlayer;
    #endregion


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        defaultPos = transform.position;
    }

    private void Update()
    {
        // Check if the player has entered the range of the enemy. This Range is calculated from its defaultPos
        playerInRange = Physics.CheckSphere(defaultPos, range, whatIsPlayer);

        distanceToDefault = transform.position - defaultPos;
        distanceToPlayer = player.position - transform.position;

        /* If the player is in range, the enemy starts to follow the player
         * If the enemy is close to the default position, it sits idle
         * If the enemy is not at the default position, it returns */
        if (playerInRange && distanceToPlayer.magnitude > privacy) UpdateBehaviour(EnemyState.Follow);
        if (distanceToDefault.magnitude < 0.1f) UpdateBehaviour(EnemyState.Idle);
        if (!playerInRange && distanceToDefault.magnitude > 0.1f) UpdateBehaviour(EnemyState.Return);
    }

    private void UpdateBehaviour(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Follow:
                Follow();
                break;

            case EnemyState.Return:
                Return();
                break;
        }
    }

    private void Idle()
    {
        Debug.Log("The enemy is idle");
    }

    private void Follow()
    {
        Debug.Log("The player will be chased");
        agent.SetDestination(player.position);
    }

    private void Return()
    {
        Debug.Log("The agent will return");
        agent.SetDestination(defaultPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(defaultPos, range);
    }



}