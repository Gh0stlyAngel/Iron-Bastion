using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static class SceneNames
    {
        public const string MainMenu = "MainMenu";
        public const string MainScene = "MainScene";
    }

    [SerializeField] private Scene mainScene;
    public void Play()
    {
        SceneManager.LoadScene(SceneNames.MainScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
