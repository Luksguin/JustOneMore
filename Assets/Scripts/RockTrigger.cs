using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public Enemy enemy;
    public float minDistance;

    private Transform _rockTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock")
        {
            enemy.patrolling = false;
            enemy.nextPoint = collision.transform;

            _rockTransform = collision.transform;
            Destroy(collision);
        }
    }

    private void Update()
    {
        if (_rockTransform)
        {
            if (Vector2.Distance(enemy.transform.position, _rockTransform.position) <= minDistance)
            {
                enemy.patrolling = true;
                _rockTransform = null;
            }
        }
    }
}
