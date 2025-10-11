using UnityEngine;

public class WaterColision : MonoBehaviour
{
    public float nerf;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") Player.instance.speed /= nerf;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") Player.instance.speed *= nerf;
    }
}
