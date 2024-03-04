using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region Enum
    public enum EnemyState
    {
        Idle,
        Follow,
        Return
    }

    public EnemyState currentState;
    #endregion

    #region References
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    private NavMeshAgent agent;
    private Vector3 defaultPos;
    #endregion

    #region Chasing
    [Header("Chasing")]
    [SerializeField] private float range;
    [SerializeField] private float minDistance;
    private bool playerInRange;
    #endregion

    #region Tripping
    [Header("Tripping")]
    [SerializeField] private float tripTime;
    [HideInInspector] public bool hasTripped;
    #endregion

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
        if (!hasTripped)
        {
            #region Setup
            // Check if the player has entered the range of the enemy. This Range is calculated from its defaultPos
            playerInRange = Physics.CheckSphere(defaultPos, range, playerLayer);

            distanceToDefault = transform.position - defaultPos;
            distanceToPlayer = transform.position - player.position;

            float distanceToDefaultMagnitude = distanceToDefault.magnitude;
            float distanceToPlayerMagnitude = distanceToPlayer.magnitude;
            #endregion

            #region Decision Making
            /* If the enemy is close to the starting point, they should idle.
             * If the player is out of range and the enemy is not at the starting point, they should return
             * If the player is in range and the enemy is not yet too close, they should follow
             * If the player is in range but the enemy is too close, they should idle */

            if (!playerInRange && distanceToDefaultMagnitude < 0.1f) UpdateBehaviour(EnemyState.Idle);
            if (!playerInRange && distanceToDefaultMagnitude > 0.1f) UpdateBehaviour(EnemyState.Return);
            if (playerInRange && distanceToPlayerMagnitude > minDistance) UpdateBehaviour(EnemyState.Follow);
            if (playerInRange && distanceToPlayerMagnitude < minDistance) UpdateBehaviour(EnemyState.Idle);
            #endregion
        }
    }

    private void UpdateBehaviour(EnemyState state)
    {
        currentState = state;

        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
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
        animator.SetTrigger("Idle");
        agent.SetDestination(transform.position);
    }

    private  void Follow()
    {
        animator.SetTrigger("Walk");
        agent.SetDestination(player.position);
    }

    private void Return()
    {
        animator.SetTrigger("Walk");
        agent.SetDestination(defaultPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(defaultPos, range);
    }

    public void Trip()
    {
        if (!hasTripped)
        {
            agent.SetDestination(transform.position);
            StartCoroutine(TripTimer());
            hasTripped = true;
        }
    }

    private IEnumerator TripTimer()
    {
        animator.SetTrigger("Tripped");
        yield return new WaitForSeconds(tripTime);
        hasTripped = false;
        Idle();
    }
}