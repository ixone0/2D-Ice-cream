using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StopScript : MonoBehaviour
{
    public AudioSource soundclick;
    public GameObject PausePanel;
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        soundclick.Play();
    }
    public void next(string scene)
    {
        Time.timeScale = 1;
        soundclick.Play();
        SceneManager.LoadScene(scene);
    }
    public void Home()
    {
        Time.timeScale = 1;
        soundclick.Play();
        SceneManager.LoadScene("Menu");
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        soundclick.Play();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        soundclick.Play();
    }

    public void Quit()
    {
        soundclick.Play();
        Application.Quit();
    }

    public IEnumerator functimer(float t)
    {
        float elapsedTime = 0f;
        while (elapsedTime < t)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
