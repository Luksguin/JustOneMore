using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> listAudiosWalk;
    public List<AudioClip> listAudiosHelping;

    public void RandomAudio()
    {
        audioSource.clip = listAudiosWalk[Random.Range(0, listAudiosWalk.Count)];
        audioSource.Play();
    }

    public void RandomAudioHelping()
    {
        audioSource.clip = listAudiosHelping[Random.Range(0, listAudiosHelping.Count)];
        audioSource.Play();
    }
}
