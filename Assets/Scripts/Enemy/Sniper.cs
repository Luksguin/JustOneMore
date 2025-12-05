using DG.Tweening;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public float duration;
    public float delay;
    public float min;
    public float max;

    private Tween _rotate;

    private void Start()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = min;
        transform.eulerAngles = rot;

        IrParaMax();
    }

    private void Update()
    {
        if (Player.instance.speed == 0 && _rotate != null)
        {
            _rotate.Pause();
        }
        else
        {
            _rotate.Play();
        }
    }

    private void IrParaMax()
    {
        _rotate = transform.DORotate(new Vector3(0, 0, max), duration).SetDelay(delay).SetEase(Ease.Linear).OnComplete(IrParaMin);
    }

    private void IrParaMin()
    {
        _rotate = transform.DORotate(new Vector3(0, 0, min), duration).SetDelay(delay).SetEase(Ease.Linear).OnComplete(IrParaMax);
    }
}
