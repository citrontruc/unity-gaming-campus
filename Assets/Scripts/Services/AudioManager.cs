/*
A singleton that handles playing sound effects and environmental sounds.
*/

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ImmortalSingleton<AudioManager>
{
    public AudioSource musicSource;
    public AudioClip CollectibleSound;
    public AudioClip musicStart;
    public float VolumeBackground = 0.5f;

    [SerializeField]
    private VoidEventChannelSO<int> collectableEventChannelSO;

    public override void Awake()
    {
        base.Awake();
    }

    #region Subscribe to events
    void OnEnable()
    {
        collectableEventChannelSO.onEventRaised += PlayCollectibleSound;
    }

    void OnDisable()
    {
        collectableEventChannelSO.onEventRaised -= PlayCollectibleSound;
    }
    #endregion

    public void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicStart;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void PlayCollectibleSound(int value)
    {
        musicSource.PlayOneShot(CollectibleSound, VolumeBackground);
    }
}
