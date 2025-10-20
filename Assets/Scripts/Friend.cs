using UnityEngine;

// Respons�vel pela intera��o do Player com Aliados;

public class Friend : MonoBehaviour
{
    public AudioSource myAudioSource;
    public GameObject myCollider;
    public GameObject myRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent.tag == "Player" && !Player.instance.isHelping) // Garante que ir� carregar apenas um aliado por vez;
        {
            Player.instance.myAnimator.SetBool(Player.instance.helpingBool, true); // Atualiza a anima��o do Player;
            Player.instance.speed /= GameManager.instance.nerf; // Nerfa a velocidade do Player;
            Player.instance.isHelping = true;

            myAudioSource.Play();

            Destroy(myCollider);
            Destroy(myRenderer);

            Destroy(gameObject, .4f);
        }
    }
}
