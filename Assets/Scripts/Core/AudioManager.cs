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
        SoundData data = _gameAudio.GetSoundData(soundType);

        if (data != null)
        {
            _audioSource.PlayOneShot(data.Clip, data.Volume);
        }
        else
        {
            Debug.LogError("Sound data is null");
        }
        
    }
}
