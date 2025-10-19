using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

// Gerencia o funcionamento das traps;

public class Trap : MonoBehaviour
{
    public ParticleSystem vfx; // Efeito de fumaça;
    public AudioSource sfx; // Efeito de som;
    public Collider2D myCollider;
    public SpriteRenderer[] myRenderers; // Renderers que compõem a trap;

    public float duration; // Duração do efeito;
    public float frequency; // Frequência da luz;

    [Header("Enemies")]
    public float newRadius_oMsmDaCircleLight; // Raio de percepção do inimigo durante o estado de alerta;

    // Cores da luz dos inimigos;
    public Color startColor;
    public Color endColor;

    public CircleCollider2D[] circleTriggers; // Triggers de presença;
    public Light2D[] circleLights; // Luzes dos triggers de presença;
    public Light2D[] visionLights; // Luzes de visão;
    public SpriteRenderer[] circleRenderers; // Borda das luzes de presença;

    private bool _onLight; // Salva se as luzes estão ligadas;
    private List<float> _startRadius = new List<float>(); // Salva os tamanhos originais dos triggers de presença;

    private void Awake()
    {
        // Preenche o vetor com os tamanhos oriinais;
        for(int i = 0; i < circleTriggers.Length; i++) _startRadius.Add(circleTriggers[i].radius);
    }

    private void Update()
    {
        if (!_onLight) return;

        // Ativa as luzes;
        foreach (var c in circleLights)
        {
            c.enabled = true;
            c.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, frequency));
        }

        foreach (var c in circleRenderers) c.enabled = true; // Ativa as bordas das luzes; 
        foreach (var v in visionLights) v.enabled = false; // Desativa as luzes das visão;
    }

    // Retira os inimigoos do estado de alerta;
    private void OffLight()
    {
        _onLight = false;
        foreach (var c in circleLights) c.enabled = false;
        for (int i = 0; i < circleTriggers.Length; i++) circleTriggers[i].radius = _startRadius[i];
        foreach (var c in circleRenderers) c.enabled = false;
        foreach (var v in visionLights) v.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            vfx.Play();
            sfx.Play();

            foreach(var r in myRenderers) r.DOColor(Color.gray, duration); // Muda a cor da trap para sinalizar que já foi ativada;

            // Liga o estado de alerta;
            _onLight = true; 
            foreach (var t in circleTriggers) t.radius = newRadius_oMsmDaCircleLight;

            Invoke("OffLight", duration); // Deslida o estado de alerta;

            Destroy(myCollider);
        }
    }
}
