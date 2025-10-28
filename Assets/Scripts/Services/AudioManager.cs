/*
A singleton that handles playing sound effects and environmental sounds.
*/

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ImmortalSingleton<AudioManager>
{
    public AudioSource musicSource;
    public AudioClip musicStart;

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicStart;
        musicSource.loop = true;
        musicSource.Play();
    }
}
