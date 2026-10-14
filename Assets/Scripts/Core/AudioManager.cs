using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private GameAudio _gameAudio;
    private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _gameAudio = gameObject.GetComponent<GameAudio>();
        _sfxSource = gameObject.GetComponent<AudioSource>();

        _musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.25f);
        _sfxSource.volume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
    }

    private void Start()
    {
        _musicSource.Play();

        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.25f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
    }

    public void PlaySFX(SoundType soundType)
    {
        SoundData data = _gameAudio.GetSoundData(soundType);

        if (data != null)
        {
            _sfxSource.PlayOneShot(data.Clip, data.Volume);
        }
        else
        {
            Debug.LogError("Sound data is null");
        }
        
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;

        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSfxVolume(float volume)
    {
        _sfxSource.volume = volume;

        PlayerPrefs.SetFloat(SfxVolumeKey, volume);
    }
}
