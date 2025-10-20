using UnityEngine;

// Responsável por detectar o Player;

public class EnemyTrigger : MonoBehaviour
{
    public Enemy enemy; // Referência para o inimigo pai;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Enquanto o player estiver dentro da colisão...
        if (collision.tag == "Player")
        {
            // .. um raycast é atirado na direção dele...
            int mask = LayerMask.GetMask("Default", "BodyPlayer");
            RaycastHit2D ray = Physics2D.Raycast(transform.position, (Player.instance.transform.position - transform.position).normalized, 10f, mask);

            if(ray.collider.tag == "Player") enemy.KillPlayer(); // ... se pegar no player game over;
        }
    }
}
