using UnityEngine;

// Responsável por interagir com o Player quando antra no trigger;

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
