using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeReference] private AudioClip[] audios;

    public AudioSource controlAudio;

    private void Awake()
    {
        

    }

    public void audioSelect(int idx, float volume)
    {
        controlAudio.PlayOneShot(audios[idx], volume);
    }
}
