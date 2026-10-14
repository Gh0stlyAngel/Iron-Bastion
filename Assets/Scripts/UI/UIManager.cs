using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        SceneManager.LoadScene(SceneNames.MainScene);
    }

    public void Exit()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        Application.Quit();
    }
}
