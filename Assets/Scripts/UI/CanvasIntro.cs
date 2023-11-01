using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasIntro : MonoBehaviour
{
    public Button playButton;

    public GameObject loadingCircle;

    public RectTransform progressBar;
    private float rotationSpeed = 200f;

    private AsyncOperation menuLoad;
    private bool loading = false;

    private void Start()
    {
        loading = true;
        StartCoroutine(LoadMenuScene());
    }

    private void Update()
    {
        if (loading)
        {
            progressBar.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator LoadMenuScene()
    {
        menuLoad = SceneManager.LoadSceneAsync("MenuScene");
        menuLoad.allowSceneActivation = false;

        //Wait
        while (!menuLoad.isDone)
        {
            //Last 10% can't be multi-threaded
            if(menuLoad.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }
        
        //Enable Play Button
        loadingCircle.SetActive(false);
        playButton.gameObject.SetActive(true);

        //Complete async scene loading
        loading = false;
    }

    public void StartGame()
    {
        menuLoad.allowSceneActivation = true;
    }
}
