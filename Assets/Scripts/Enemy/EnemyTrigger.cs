using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.KillPlayer();
        }
    }
}
