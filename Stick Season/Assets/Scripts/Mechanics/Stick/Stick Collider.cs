using UnityEngine;

public class StickCollider : MonoBehaviour
{
    private GameObject coll;
    private EnemyAI enemy;

    private void OnCollisionEnter(Collision collision)
    {
        coll = collision.gameObject;

        if (coll.CompareTag("Enemy"))
        {
            enemy = coll.GetComponent<EnemyAI>();

            enemy.Trip();
        }
    }
}
