using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

// Armazena as funções de trocas de cenas;

public class ManagerScene : MonoBehaviour
{
    public Image transitionImage;
    public float duration;

    public AudioSource myAudioSource;

    public void ChangeScene(int nextScene)
    {
        if(transitionImage)StartCoroutine(TransitionCoroutine(nextScene));
        else SceneManager.LoadScene(nextScene);
    }

    // Transição entre cenas;
    IEnumerator TransitionCoroutine(int nextScene)
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.DOColor(new Color(0f, 0f, 0f, 1f), duration);
        if(myAudioSource) myAudioSource.Play();

        yield return new WaitForSeconds(duration + 2f);

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
