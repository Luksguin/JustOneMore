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
            if (_delay > 1)
            {
                RandomAudioSand();
                audioSource.Play();

                _delay = 0;
            }
        }
        else if (_currentTilemap == "Grass")
        {
            if (_delay > 2)
            {
                RandomAudioGrass();
                audioSource.Play();

                _delay = 0;
            }
        }
        else if (_currentTilemap == "Water")
        {
            if (_delay > 3)
            {
                RandomAudioWater();
                audioSource.Play();

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
                RandomAudioHelpingSand();
                audioSource.Play();

                _delay = 0;
            }
        }
        else if (_currentTilemap == "Grass")
        {
            if (_delay > 0)
            {
                RandomAudioHelpingGrass();
                audioSource.Play();

                _delay = 0;
            }
        }
        else if (_currentTilemap == "Water")
        {
            if (_delay > 1)
            {
                RandomAudioHelpingWater();
                audioSource.Play();

                _delay = 0;
            }
        }

        _delay++;
    }

    #region AUX FUNCTIONS
    // Aleatoriza os aúdios das listas;
    private void RandomAudioSand()
    {
        audioSource.clip = audiosWalkSand[Random.Range(0, audiosWalkSand.Count)];
    }

    private void RandomAudioHelpingSand()
    {
        audioSource.clip = audiosHelpingSand[Random.Range(0, audiosHelpingSand.Count)];
    }

    private void RandomAudioGrass()
    {
        audioSource.clip = audiosWalkGrass[Random.Range(0, audiosWalkGrass.Count)];
    }

    private void RandomAudioHelpingGrass()
    {
        audioSource.clip = audiosHelpingGrass[Random.Range(0, audiosHelpingGrass.Count)];
    }

    private void RandomAudioWater()
    {
        audioSource.clip = audiosWalkWater[Random.Range(0, audiosWalkWater.Count)];
    }

    private void RandomAudioHelpingWater()
    {
        audioSource.clip = audiosHelpingWater[Random.Range(0, audiosHelpingWater.Count)];
    }
    #endregion
}
