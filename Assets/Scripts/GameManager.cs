using Luksguin.Singleton;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using DG.Tweening;

// Gerencia condições de vitória e derrota, trocas de cenas, controla mecânicas;

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

    [Header("Enemies")]
    public CircleCollider2D[] circleTriggers; // Triggers de presença;
    public Light2D[] circleLights; // Luzes dos triggers de presença;
    public Light2D[] visionLights; // Luzes de visão;
    public SpriteRenderer[] circleRenderers; // Borda das luzes de presença;

    private float _time; // Tempo atual do jogo;
    private bool _finish; // Variavel de controle;

    private void Start()
    {
        _time = 0;
        _finish = false;

        winMenu.transform.localScale = new Vector2(0, 0);
        gameOverMenu.transform.localScale = new Vector2(0, 0);

        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        if(missingAllies == 0 && !_finish)
        {
            WinGame();
            _finish = true;
        }

        _time += Time.deltaTime;

        if(_time > maxTime && !_finish)
        {
            GameOver();
            _finish = true;
        }
    }

    public void WinGame()
    {
        Player.instance.speed = 0; // Trava a velocidade do Player;
        Destroy(Player.instance.myAnimator, .5f); // Trava a animação do Player;

        myAudioSource.clip = winAudio;
        myAudioSource.Play();

        winMenu.SetActive(true);
        winMenu.transform.DOScale(1f, animDuration).SetEase(animEase);
    }

    public void GameOver()
    {
        myAudioSource.clip = gameOverAudio;
        myAudioSource.Play();

        Player.instance.speed = 0; // Trava a velocidade do Player;

        gameOverMenu.SetActive(true);
        gameOverMenu.transform.DOScale(1f, animDuration).SetEase(animEase);
    }

    // Instancia uma pedra;
    public void SpawnRock()
    {
        Instantiate(rockPrefab, null);
        rockAmount--;
    }

    public void ChangeScene(int nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
