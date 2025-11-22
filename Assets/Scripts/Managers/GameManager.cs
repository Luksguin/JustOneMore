using Luksguin.Singleton;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

// Gerencia condições de vitória e derrota, menus, controla mecânicas;

public class GameManager : Singleton<GameManager>
{
    public int leftFriends; // Quantidade de aliados restantes;
    public float nerf; // Nerf de velocidade quando estiver carregando um aliado ou estiver na água;
    public float time; // Tempo para finalizar o jogo;

    [Header("UI")]
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI leftFriendsUI;
    public TextMeshProUGUI rockAmountUI;

    [Header("Rock")]
    public int rockAmount;
    public GameObject rockPrefab;

    [Header("Menus")]
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public GameObject playerUI;
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

    private bool _finish; // Variavel de controle;

    private void Start()
    {
        _finish = false;
        inTrap = false;

        winMenu.transform.localScale = new Vector2(0, 0);
        gameOverMenu.transform.localScale = new Vector2(0, 0);

        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        // Atualiza UIs;
        timerUI.text = "0" + ((int)time / 60) + ":" + ((int)time % 60);
        leftFriendsUI.text = "x" + leftFriends;
        rockAmountUI.text = "x" + rockAmount;

        if (_finish) return;
        time -= Time.deltaTime;

        if(leftFriends == 0)
        {
            WinGame();
            _finish = true;
        }

        if(time <= 0)
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

        playerUI.SetActive(false); // Desativa UI;

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

        playerUI.SetActive(false); // Desativa UI;

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
