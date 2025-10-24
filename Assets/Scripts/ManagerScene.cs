using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;


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

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator TransitionCoroutine(int nextScene)
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.DOColor(new Color(0f, 0f, 0f, 1f), duration);
        myAudioSource.Play();

        yield return new WaitForSeconds(duration + 2f);

        SceneManager.LoadScene(nextScene);
    }
}
