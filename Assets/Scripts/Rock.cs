using UnityEngine;
using DG.Tweening;

// Gerencia o comportamento da pedra;

public class Rock : MonoBehaviour
{
    public float fallDuration; // Tempo da animação de queda;
    public AnimationCurve fallEase; // Ease da animação de queda;

    public float lifeDuration; // Tempo até desaparecer;
    public AnimationCurve scaleEase; // Ease da animação de desaparecimento;

    public Collider2D myCollider;

    private void Awake()
    {
        myCollider.enabled = false; // Garante que a colisão com os "SoundsTrigger" só irá acontecer quando estiver no chão;
    }

    void Start()
    {
        transform.DOMove(Player.instance.lastClick, fallDuration).SetEase(fallEase); // Animação de queda;
        Invoke("EnableCollider", fallDuration - .01f); // Ativando o collider quando estiver próximo ao chão;

        transform.DOScale(0, lifeDuration).SetEase(scaleEase); // Animação de desaparecimento;
        Destroy(myCollider, fallDuration + .01f); // Destroi o collider para não atrair inimios por acaso;

        Destroy(gameObject, lifeDuration);
    }

    // Ativa o collider;
    private void EnableCollider()
    {
        myCollider.enabled = true;
    }
}
