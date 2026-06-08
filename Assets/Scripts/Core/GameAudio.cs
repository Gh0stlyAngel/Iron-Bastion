using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _arrowClip;
    [SerializeField] private AudioClip _ShellClip;
    [SerializeField] private AudioClip _missileClip;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _defeatClip;
    public AudioClip GetClipByType(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.ArcherShot:
                return _arrowClip;
            case SoundType.CannonShot:
                return _ShellClip;
            case SoundType.MageShot:
                return _missileClip;
            case SoundType.Victory:
                return _victoryClip;
            case SoundType.Defeat:
                return _defeatClip;
            default:
                Debug.LogError("Unexpected sound type");
                return null;
        }
    }
}
