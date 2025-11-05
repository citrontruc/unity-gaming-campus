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
    public AudioClip SpecialCollectibleSound;
    public AudioClip musicStart;
    #endregion

    [SerializeField]
    private VoidEventChannelSO<int> _collectibleEventChannelSO;

    [SerializeField]
    private VoidEventChannelSO<int> _specialCollectibleEventChannelSOalSounds;

    #region Subscribe to events
    void OnEnable()
    {
        _collectibleEventChannelSO.onEventRaised += PlayCollectibleSound;
        _specialCollectibleEventChannelSOalSounds.onEventRaised += PlaySpecialCollectibleSound;
    }

    void OnDisable()
    {
        _collectibleEventChannelSO.onEventRaised -= PlayCollectibleSound;
        _specialCollectibleEventChannelSOalSounds.onEventRaised -= PlaySpecialCollectibleSound;
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
        playSoundOnce(CollectibleSound, VolumeBackground);
    }

    private void PlaySpecialCollectibleSound(int value)
    {
        playSoundOnce(SpecialCollectibleSound, VolumeBackground);
    }

    private void playSoundOnce(AudioClip audioClip, float volumeBackground)
    {
        musicSource.PlayOneShot(audioClip, volumeBackground);
    }
}
