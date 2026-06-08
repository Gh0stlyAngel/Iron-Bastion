using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private GameAudio _gameAudio;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _gameAudio = gameObject.GetComponent<GameAudio>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySFX(SoundType soundType)
    {
        AudioClip clip = _gameAudio.GetClipByType(soundType);

        float volume;
        switch (soundType)
        {
            case SoundType.ArcherShot:
                volume = 0.85f;
                break;
            case SoundType.CannonShot:
                volume = 0.12f;
                break;
            case SoundType.MageShot:
                volume = 0.06f;
                break;
            case SoundType.Victory:
                volume = 0.25f;
                break; ;
            case SoundType.Defeat:
                volume = 0.25f;
                break;
            default:
                volume = 1.0f;
                break;
        }

        _audioSource.PlayOneShot(clip, volume);
    }
}
