using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelPreludeScene");
    }

    public void GotoLevels()
    {
        SceneManager.LoadScene("LevelsScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
