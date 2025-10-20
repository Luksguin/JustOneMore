using UnityEngine;

// Diminui a velocidade do Player enquanto estiver na �gua;

public class WaterColision : MonoBehaviour
{
    // Aplica o nerf;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") Player.instance.speed /= GameManager.instance.nerf;
    }

    // Resmove o nerf;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") Player.instance.speed *= GameManager.instance.nerf;
    }
}
