using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class Trap : MonoBehaviour
{
    public ParticleSystem vfx;
    public AudioSource sfx;
    public Collider2D myCollider;
    public SpriteRenderer[] myRenderers;

    public Color startColor;
    public Color endColor;
    public float duration;
    public float frequency;

    [Header("Enemies")]
    public float newRadius_oMsmDaCircleLight;
    public CircleCollider2D[] circleTriggers;
    public Light2D[] circleLights;

    private bool _onLight;
    private List<float> _startRadius = new List<float>();

    private void Awake()
    {
        for(int i = 0; i < circleTriggers.Length; i++)
        {
            _startRadius.Add(circleTriggers[i].radius);
        }
    }

    private void Update()
    {
        if (!_onLight) return;

        foreach (var c in circleLights)
        {
            c.enabled = true;
            c.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, frequency));
        }
    }

    private void OffLight()
    {
        _onLight = false;
        foreach (var c in circleLights) c.enabled = false;
        for (int i = 0; i < circleTriggers.Length; i++)
        {
            circleTriggers[i].radius = _startRadius[i];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            vfx.Play();
            sfx.Play();

            foreach(var r in myRenderers) r.DOColor(Color.gray, duration);

            _onLight = true;
            foreach (var t in circleTriggers) t.radius = newRadius_oMsmDaCircleLight;

            Invoke("OffLight", duration);

            Destroy(myCollider);
        }
    }
}
