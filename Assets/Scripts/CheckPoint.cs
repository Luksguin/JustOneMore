using UnityEngine;

// Responsável por interagir com o Player que estiver ajudando um aliado;

public class CheckPoint : MonoBehaviour
{
    public float weight; // Nerf de velocidade; Trocar para um GameManager futuramente;
    public AudioSource myAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.instance.isHelping)
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, false); // Atualiza a animação do Player;
            Player.instance.speed *= weight; // Normaliza a velocidade do Player;
            Player.instance.isHelping = false;

            myAudioSource.Play();
        }
    }
}
