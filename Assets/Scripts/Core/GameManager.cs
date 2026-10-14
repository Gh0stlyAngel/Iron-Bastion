using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeScale;

    private bool _victoryTriggered;
    private bool _defeatTriggered;
    private bool _isPaused;

    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _endingButons;
    [SerializeField] private GameObject _menuButton;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _soundMenu;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        _victoryTriggered = false;
        _defeatTriggered = false;
    }

    private void Update()
    {
        if (!_victoryTriggered && !_defeatTriggered && !_isPaused)
            Time.timeScale = _timeScale;
    }

    public void Victory()
    {
        if (_victoryTriggered || _defeatTriggered)
            return;

        if (!_victoryTriggered)
        {
            _victoryTriggered = true;
            Time.timeScale = 0f;
            _victoryPanel.SetActive(true);
            _endingButons.SetActive(true);
            _menuButton.SetActive(false);

            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlaySFX(SoundType.Victory);
        }
    }

    public void Defeat()
    {
        if (_victoryTriggered || _defeatTriggered)
            return;

        if (!_defeatTriggered)
        {
            _defeatTriggered = true;
            Time.timeScale = 0f;
            _defeatPanel.SetActive(true);
            _endingButons.SetActive(true);
            _menuButton.SetActive(false);

            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlaySFX(SoundType.Defeat);
        }
    }

    public void Restart()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        Time.timeScale = 0f;
        _isPaused = true;
        Debug.Log($"Time scale: {Time.timeScale}");
        _pausePanel.SetActive(true);
        _playButton.SetActive(true);
        _endingButons.SetActive(true);
    }

    public void Unpause()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        Time.timeScale = 1f;
        _isPaused = false;
        _pausePanel.SetActive(false);
        _menuButton.SetActive(true);
        _endingButons.SetActive(false);
    }

    public void SoundSettings()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        _soundMenu.SetActive(true);
        _endingButons.SetActive(false);
    }

    public void Back()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        _soundMenu.SetActive(false);
        _endingButons.SetActive(true);
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
