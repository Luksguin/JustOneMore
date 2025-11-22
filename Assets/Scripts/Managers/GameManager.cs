using Luksguin.Singleton;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using DG.Tweening;

// Gerencia condições de vitória e derrota, menus, controla mecânicas;

public class GameManager : Singleton<GameManager>
{
    public int missingAllies; // Quantidade de aliados restantes;
    public float nerf; // Nerf de velocidade quando estiver carregando um aliado ou estiver na água;
    public float maxTime;

    [Header("Rock")]
    public int rockAmount;
    public GameObject rockPrefab;

    [Header("Menus")]
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public float animDuration;
    public Ease animEase;

    [Header("Audios")]
    public AudioSource myAudioSource;
    public AudioClip winAudio;
    public AudioClip gameOverAudio;

    [Header("Trap")]
    public CircleCollider2D[] circleTriggers; // Triggers de presença;
    public Light2D[] circleLights; // Luzes dos triggers de presença;
    public Light2D[] visionLights; // Luzes de visão;
    public SpriteRenderer[] circleRenderers; // Borda das luzes de presença;
    [HideInInspector] public bool inTrap;

    public float startRadius;
    public float newRadius;

    private float _time; // Tempo atual do jogo;
    private bool _finish; // Variavel de controle;

    private void Start()
    {
        _time = 0;
        _finish = false;
        inTrap = false;

        winMenu.transform.localScale = new Vector2(0, 0);
        gameOverMenu.transform.localScale = new Vector2(0, 0);

        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        if (_finish) return;
        _time += Time.deltaTime;

        if(missingAllies == 0)
        {
            WinGame();
            _finish = true;
        }


        if(_time > maxTime)
        {
            GameOver();
            _finish = true;
        }
    }

    public void WinGame()
    {
        // Trava o Player;
        Player.instance.speed = 0;
        Destroy(Player.instance.myAnimator, .5f);

        // Toca audio de vitória;
        myAudioSource.clip = winAudio;
        myAudioSource.Play(); 

        // Ativa o menu de vitória;
        winMenu.SetActive(true);
        winMenu.transform.DOScale(1f, animDuration).SetEase(animEase);

        // Atualiza o progresso;
        int level = PlayerPrefs.GetInt("Level");
        PlayerPrefs.SetInt("Level", level + 1);
    }

    public void GameOver()
    {
        // Toca audio de derrota;
        myAudioSource.clip = gameOverAudio;
        myAudioSource.Play();

        Player.instance.speed = 0; // Trava o Player;

        // Ativa o menu de derrota;
        gameOverMenu.SetActive(true);
        gameOverMenu.transform.DOScale(1f, animDuration).SetEase(animEase);
    }

    // Instancia uma pedra;
    public void SpawnRock()
    {
        Instantiate(rockPrefab, null);
        rockAmount--;
    }
}
