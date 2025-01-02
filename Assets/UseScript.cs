using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject uistage;
    public AudioSource soundclick;
    
    public void closeUIstage()
    {
        uistage.SetActive(false);
        soundclick.Play();
    }
}
