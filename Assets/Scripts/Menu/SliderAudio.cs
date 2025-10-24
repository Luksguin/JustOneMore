using UnityEngine;
using UnityEngine.Audio;

public class SliderAudio : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string volumeParam;

    public void ChangeValue(float f)
    {
        audioMixer.SetFloat(volumeParam, f);
    }
}
