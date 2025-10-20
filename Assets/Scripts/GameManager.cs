using Luksguin.Singleton;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

// Gerencia condi��es de vit�ria e derrota, trocas de cenas, controla mec�nicas;

public class GameManager : Singleton<GameManager>
{
    public int missingAllies; // Quantidade de aliados restantes;
    public float nerf; // Nerf de velocidade quando estiver carregando um aliado ou estiver na �gua;
    public float maxTime;

    [Header("Rock")]
    public int rockAmount;
    public GameObject rockPrefab;

    [Header("Menus")]
    public GameObject winMenu;
    public GameObject gameOverMenu;

    [Header("Audios")]
    public AudioSource myAudioSource;
    public AudioClip winAudio;
    public AudioClip gameOverAudio;

    [Header("Enemies")]
    public CircleCollider2D[] circleTriggers; // Triggers de presen�a;
    public Light2D[] circleLights; // Luzes dos triggers de presen�a;
    public Light2D[] visionLights; // Luzes de vis�o;
    public SpriteRenderer[] circleRenderers; // Borda das luzes de presen�a;

    private float _time = 0;

    private void Update()
    {
        if(missingAllies == 0)
        {
            Invoke("WinGame", 2f);
        }

        _time += Time.deltaTime;

        print(_time);

        if(_time > maxTime)
        {
            GameOver();
        }
    }

    public void WinGame()
    {
        Player.instance.speed = 0; // Trava a velocidade do Player;

        myAudioSource.clip = winAudio;
        myAudioSource.Play();

        winMenu.SetActive(true);
    }

    public void GameOver()
    {
        myAudioSource.clip = gameOverAudio;
        myAudioSource.Play();

        gameOverMenu.SetActive(true);
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
