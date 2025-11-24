using System.Collections.Generic;
using UnityEngine;

// Gerencia os sons de passos do Player;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    // Listas de audios em todas as circunstâncias;
    public List<AudioClip> audiosWalkSand;
    public List<AudioClip> audiosHelpingSand;

    public List<AudioClip> audiosWalkGrass;
    public List<AudioClip> audiosHelpingGrass;

    public List<AudioClip> audiosWalkWater;
    public List<AudioClip> audiosHelpingWater;

    private string _currentTilemap = "Sand"; // Recebe o tilemp que o Player está;
    private int _delay; // Delay aplicado nos aúdios;

    private void Update()
    {
        // Raycast usado para acessar o tilemap que o Player está;
        int mask = LayerMask.GetMask("Water", "Sand", "Grass");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, mask);
        //Debug.DrawRay(transform.position, Vector2.down * 1);

        if (!ray.transform) _currentTilemap = "Sand";
        else _currentTilemap = ray.transform.tag;
    }

    // Chamada por evento nas animações;
    // Controla quando o som de passo deve tocar;
    // Player em modo Idle;
    public void WalkIdle()
    {
        if (_currentTilemap == "Sand")
        {
            if (_delay > -1)
            {
                audioSource.PlayOneShot(RandomAudioSand());
                _delay = 0;
            }
        }
        else if (_currentTilemap == "Grass")
        {
            if (_delay > -2)
            {
                audioSource.PlayOneShot(RandomAudioGrass());
                _delay = 0;
            }
        }
        else if (_currentTilemap == "Water")
        {
            if (_delay > 1)
            {
                audioSource.PlayOneShot(RandomAudioWater());
                _delay = 0;
            }
        }
        _delay++;
    }

    // Chamada por evento nas animações;
    // Controla quando o som de passo deve tocar;
    // Player em modo Helping;
    public void WalkHelping()
    {
        if (_currentTilemap == "Sand")
        {
            if (_delay > 0)
            {
                audioSource.PlayOneShot(RandomAudioHelpingSand());
                _delay = 0;
            }
        }
        else if (_currentTilemap == "Grass")
        {
            if (_delay > 0)
            {
                audioSource.PlayOneShot(RandomAudioHelpingGrass());
                _delay = 0;
            }
        }
        else if (_currentTilemap == "Water")
        {
            if (_delay > 1)
            {
                audioSource.PlayOneShot(RandomAudioHelpingWater());
                _delay = 0;
            }
        }
        _delay++;
    }

    #region RANDOM FUNCTIONS
    // Aleatoriza os aúdios das listas;
    private AudioClip RandomAudioSand()
    {
        return audiosWalkSand[Random.Range(0, audiosWalkSand.Count)];
    }

    private AudioClip RandomAudioHelpingSand()
    {
        return audiosHelpingSand[Random.Range(0, audiosHelpingSand.Count)];
    }

    private AudioClip RandomAudioGrass()
    {
        return audiosWalkGrass[Random.Range(0, audiosWalkGrass.Count)];
    }

    private AudioClip RandomAudioHelpingGrass()
    {
        return audiosHelpingGrass[Random.Range(0, audiosHelpingGrass.Count)];
    }

    private AudioClip RandomAudioWater()
    {
        return audiosWalkWater[Random.Range(0, audiosWalkWater.Count)];
    }

    private AudioClip RandomAudioHelpingWater()
    {
        return audiosHelpingWater[Random.Range(0, audiosHelpingWater.Count)];
    }
    #endregion
}
