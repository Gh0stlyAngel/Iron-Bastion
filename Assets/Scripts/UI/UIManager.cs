using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneNames.MainScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
