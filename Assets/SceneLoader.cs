using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Name of the scene you want to load
    public AudioSource soundclick;
    public float time = 1f;

    public void LoadScene()
    {
        StartCoroutine(ExampleCoroutine(time));
    }
    

    IEnumerator ExampleCoroutine(float x)
    {
        soundclick.Play();
        yield return new WaitForSeconds(x);
        SceneManager.LoadScene(sceneName);
    }
}