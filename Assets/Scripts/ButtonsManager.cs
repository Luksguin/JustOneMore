using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("DOTween")]
    public float duration;
    public float delay;
    public Ease ease = Ease.OutBack;

    private void OnEnable()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }

        ShowButtonsWithDelay();
    }

    private void ShowButtonsWithDelay()
    {
        int i = 0;
        foreach (var b in buttons)
        {
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i * delay).SetEase(ease);
            i++;
        }
    }
}
