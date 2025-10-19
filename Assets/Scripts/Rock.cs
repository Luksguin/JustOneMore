using UnityEngine;
using DG.Tweening;

// Gerencia o comportamento da pedra;

public class Rock : MonoBehaviour
{
    public float fallDuration; // Tempo da anima��o de queda;
    public AnimationCurve fallEase; // Ease da anima��o de queda;

    public float lifeDuration; // Tempo at� desaparecer;
    public AnimationCurve scaleEase; // Ease da anima��o de desaparecimento;

    public Collider2D myCollider;

    private void Awake()
    {
        myCollider.enabled = false; // Garante que a colis�o com os "SoundsTrigger" s� ir� acontecer quando estiver no ch�o;
    }

    void Start()
    {
        transform.DOMove(Player.instance.lastClick, fallDuration).SetEase(fallEase); // Anima��o de queda;
        Invoke("EnableCollider", fallDuration - .01f); // Ativando o collider quando estiver pr�ximo ao ch�o;

        transform.DOScale(0, lifeDuration).SetEase(scaleEase); // Anima��o de desaparecimento;
        Destroy(myCollider, fallDuration + .01f); // Destroi o collider para n�o atrair inimios por acaso;

        Destroy(gameObject, lifeDuration);
    }

    // Ativa o collider;
    private void EnableCollider()
    {
        myCollider.enabled = true;
    }
}
