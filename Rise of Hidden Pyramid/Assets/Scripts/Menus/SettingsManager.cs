using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMusicVolume (float value)
    {
        audioMixer.SetFloat("musicVol", Mathf.Log10(value) * 20);
    }

    public void SetSoundVolume (float value)
    {
        audioMixer.SetFloat("soundVol", Mathf.Log10(value) * 20);
    }
}
