using UnityEngine;

// Responsável por interagir com o Player que estiver ajudando um aliado;

public class CheckPoint : MonoBehaviour
{
    public AudioSource myAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.instance.isHelping)
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, false); // Atualiza a animação do Player;
            Player.instance.speed *= GameManager.instance.nerfFriend ; // Normaliza a velocidade do Player;
            Player.instance.isHelping = false;

            GameManager.instance.leftFriends--;

            myAudioSource.Play();
        }
    }
}
