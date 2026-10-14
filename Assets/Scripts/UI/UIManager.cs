using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject SoundSettingsMenu;

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

    public void SoundSettings()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);

        mainMenuButtons.SetActive(false);
        SoundSettingsMenu.SetActive(true);
    }

    public void Menu()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);

        mainMenuButtons.SetActive(true);
        SoundSettingsMenu.SetActive(false);
    }
}
