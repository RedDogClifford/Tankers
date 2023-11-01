using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasLevels : MonoBehaviour
{
    public void PlaySelectedLevel(int level)
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
