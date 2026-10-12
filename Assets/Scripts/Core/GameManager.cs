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

            AudioManager.Instance.PlaySFX(SoundType.Defeat);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _isPaused = true;
        Debug.Log($"Time scale: {Time.timeScale}");
        _pausePanel.SetActive(true);
        _playButton.SetActive(true);
        _endingButons.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        _pausePanel.SetActive(false);
        _menuButton.SetActive(true);
        _endingButons.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
