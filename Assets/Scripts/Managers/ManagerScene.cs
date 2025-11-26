using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

// Armazena as funções de trocas de cenas;

public class ManagerScene : MonoBehaviour
{
    public Image transitionImage;
    public float delay;
    public float startTransitionDuration;
    public float endTransitionDuration;

    public AudioSource myAudioSource;

    public bool startTransition;

    private void Start()
    {
        if (startTransition) StartCoroutine(StartTransitionCoroutine());
    }

    IEnumerator StartTransitionCoroutine()
    {
        transitionImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        transitionImage.DOColor(new Color(0f, 0f, 0f, 0f), startTransitionDuration);

        yield return new WaitForSeconds(startTransitionDuration - 3);

        if(Player.instance) Player.instance.canMove = true;
        transitionImage.gameObject.SetActive(false);
    }

    public void ChangeScene(int nextScene)
    {
        if(transitionImage)StartCoroutine(EndTransitionCoroutine(nextScene));
        else SceneManager.LoadScene(nextScene);
    }

    // Transição entre cenas;
    IEnumerator EndTransitionCoroutine(int nextScene)
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.DOColor(new Color(0f, 0f, 0f, 1f), endTransitionDuration);

        if(myAudioSource) myAudioSource.Play();

        yield return new WaitForSeconds(endTransitionDuration);

        SceneManager.LoadScene(nextScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Usado apenas em SCN_Menu;
    // Reseta o progresso do Player;
    public void NewGame()
    {
        PlayerPrefs.SetInt("Level", 1);
    }
}
