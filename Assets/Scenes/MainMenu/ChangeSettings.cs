using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeSettings : MonoBehaviour
{
    public AudioMixer Mixer;

    public void SetVolume(float SliderValue)
    {
        Mixer.SetFloat("MusicVolume", Mathf.Log10(SliderValue) * 20);
    }
}
