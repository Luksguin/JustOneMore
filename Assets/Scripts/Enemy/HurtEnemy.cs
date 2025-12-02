using TMPro;
using UnityEngine;
using DG.Tweening;

public class HurtEnemy : MonoBehaviour
{
    public GameObject buttons;
    public TextMeshProUGUI textUI;

    public AudioSource myAudioSource;
    public Collider2D myCollider;
    public SpriteRenderer myRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            textUI.text = "Por favor!";

            buttons.SetActive(true);
            buttons.transform.DOScale(1f, .5f).SetEase(Ease.Linear);

            // Trava o Player;
            Player.instance.canMove = false;
            Player.instance.speed = 0;
        }
    }

    public void WinGame()
    {
        // Destrava o Player;
        Player.instance.canMove = true;
        Player.instance.speed = 150;

        textUI.text = "Obrigado!";

        buttons.transform.DOScale(0f, .5f).SetEase(Ease.Linear);

        Player.instance.myAnimator.SetBool(Player.instance.helpingBool, true); // Atualiza a animação do Player;
        Player.instance.speed /= GameManager.instance.nerfFriend; // Nerfa a velocidade do Player;
        Player.instance.isHelping = true;

        myAudioSource.Play();

        Destroy(myCollider);
        Destroy(myRenderer);

        Destroy(gameObject, .4f);
    }
}
