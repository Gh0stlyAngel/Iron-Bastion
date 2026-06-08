using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeScale;

    private bool _victoryTriggered;
    private bool _defeatTriggered;

    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private GameObject _restartButton;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        _victoryTriggered = false;
        _defeatTriggered = false;
    }

    private void Update()
    {
        if (!_victoryTriggered && !_defeatTriggered)
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
            _restartButton.SetActive(true);

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
            _restartButton.SetActive(true);

            AudioManager.Instance.PlaySFX(SoundType.Defeat);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
