using UnityEngine;
using DG.Tweening;

public class Rock : MonoBehaviour
{
    public float fallDuration;
    public AnimationCurve fallEase;

    public float lifeDuration;
    public AnimationCurve scaleEase;

    public Collider2D myCollider;

    private void Awake()
    {
        myCollider.enabled = false;
    }

    void Start()
    {
        transform.DOMove(Player.instance.lastClick, fallDuration).SetEase(fallEase);
        Invoke("EnableCollider", fallDuration - .01f);

        transform.DOScale(0, lifeDuration).SetEase(scaleEase);
        Destroy(myCollider, fallDuration + .01f);

        Destroy(gameObject, lifeDuration);
    }

    private void EnableCollider()
    {
        myCollider.enabled = true;
    }
}
