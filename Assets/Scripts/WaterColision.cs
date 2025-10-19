using UnityEngine;

// Diminui a velocidade do Player enquanto estiver na água;

public class WaterColision : MonoBehaviour
{
    public float nerf; // Valor do nerf de velocidade;

    // Aplica o nerf;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") Player.instance.speed /= nerf;
    }

    // Resmove o nerf;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") Player.instance.speed *= nerf;
    }
}
