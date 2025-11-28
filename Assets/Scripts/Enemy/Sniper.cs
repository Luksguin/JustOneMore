using DG.Tweening;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public float duration;
    public float delay;
    public float min;
    public float max;

    private void Start()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = min;
        transform.eulerAngles = rot;

        IrParaMax();
    }

    private void IrParaMax()
    {
        transform.DORotate(new Vector3(0, 0, max), duration).SetDelay(delay).SetEase(Ease.Linear).OnComplete(IrParaMin);
    }

    private void IrParaMin()
    {
        transform.DORotate(new Vector3(0, 0, min), duration).SetDelay(delay).SetEase(Ease.Linear).OnComplete(IrParaMax);
    }
}
