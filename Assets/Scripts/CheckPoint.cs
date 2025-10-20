using UnityEngine;

// Respons�vel por interagir com o Player que estiver ajudando um aliado;

public class CheckPoint : MonoBehaviour
{
    public AudioSource myAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.instance.isHelping)
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, false); // Atualiza a anima��o do Player;
            Player.instance.speed *= GameManager.instance.nerf ; // Normaliza a velocidade do Player;
            Player.instance.isHelping = false;

            GameManager.instance.missingAllies--;

            myAudioSource.Play();
        }
    }
}
