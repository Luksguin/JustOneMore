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

        if(min == 0 && max == 360)
            _rotate = transform.DORotate(new Vector3(0, 0, max), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        else 
            IrParaMax();
    }

    private void Update()
    {
        if (Player.instance.speed == 0)
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
