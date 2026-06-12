using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private List<SoundData> _sounds;

    public SoundData GetSoundData(SoundType soundType)
    {
        return _sounds.Find(sound => sound.Type == soundType);
    }
}
