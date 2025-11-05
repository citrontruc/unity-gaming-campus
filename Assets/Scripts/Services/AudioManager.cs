/*
A singleton that handles playing sound effects and environmental sounds.
It is an Immortal Singleton. Put it in your main menu scene and don't put it elsewhere.
*/

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

    #region Event Channels
    [SerializeField]
    private VoidEventChannelSO<int> _collectibleEventChannelSO;

    [SerializeField]
    private VoidEventChannelSO<int> _specialCollectibleEventChannelSOalSounds;
    #endregion

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

    #region Play sound effects
    private void PlayCollectibleSound(int value)
    {
        PlaySoundOnce(CollectibleSound, VolumeBackground);
    }

    private void PlaySpecialCollectibleSound(int value)
    {
        PlaySoundOnce(SpecialCollectibleSound, VolumeBackground);
    }

    private void PlaySoundOnce(AudioClip audioClip, float volumeBackground)
    {
        musicSource.PlayOneShot(audioClip, volumeBackground);
    }
    #endregion
}
