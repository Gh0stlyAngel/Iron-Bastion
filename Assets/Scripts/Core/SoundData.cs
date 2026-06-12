using System;
using UnityEngine;
[Serializable]
public class SoundData
{
    public SoundType Type;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume;
}
