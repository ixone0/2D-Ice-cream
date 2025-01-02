using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource soundclick;
    public void UIStageOpen(GameObject UIStage)
    {
        soundclick.Play();
        UIStage.SetActive(true);
    }
    public void UIClose(GameObject UI)
    {
        soundclick.Play();
        UI.SetActive(false);
    }
}
