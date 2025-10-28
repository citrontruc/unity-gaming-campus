/*
A singleton that handles playing sound effects and environmental sounds.
*/

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ImmortalSingleton<AudioManager>
{
    public AudioSource musicSource;
    public float VolumeBackground = 0.5f;

    #region Audio samples
    public AudioClip CollectibleSound;
    public AudioClip musicStart;
    #endregion

    [SerializeField]
    private VoidEventChannelSO<int> collectableEventChannelSO;

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

    #region Monobehaviour methods
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
    #endregion

    private void PlayCollectibleSound(int value)
    {
        musicSource.PlayOneShot(CollectibleSound, VolumeBackground);
    }
}
