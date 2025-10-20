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

    // Cores da luz dos inimigos;
    public Color startColor;
    public Color endColor;

    private bool _onLight; // Salva se as luzes estão ligadas;

    private void Update()
    {
        if (!_onLight) return;

        // Ativa as luzes;
        foreach (var c in GameManager.instance.circleLights)
        {
            c.enabled = true;
            c.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, frequency));
        }

        foreach (var c in GameManager.instance.circleRenderers) c.enabled = true; // Ativa as bordas das luzes; 
        foreach (var v in GameManager.instance.visionLights) v.enabled = false; // Desativa as luzes das visão;
    }

    // Retira os inimigoos do estado de alerta;
    private void OffLight()
    {
        _onLight = false;
        GameManager.instance.inTrap = false;

        foreach (var c in GameManager.instance.circleLights) c.enabled = false;
        foreach(var c in GameManager.instance.circleTriggers) c.radius = GameManager.instance.startRadius;
        foreach (var c in GameManager.instance.circleRenderers) c.enabled = false;
        foreach (var v in GameManager.instance.visionLights) v.enabled = true;
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
            GameManager.instance.inTrap = true;

            foreach (var c in GameManager.instance.circleTriggers) c.radius = GameManager.instance.newRadius;

            Invoke("OffLight", duration); // Deslida o estado de alerta;

            Destroy(myCollider);
        }
    }
}
