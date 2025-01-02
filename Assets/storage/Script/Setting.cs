using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{   
    public int stage;
    public int customersPerChapter;
    public int score = 0;
    public bool check = false;
    public bool check2 = false;
    public bool GameOver = false;
    public float Timelimit;
    public GameObject[] ThingTask;
    public GameObject UIVictory;
    public GameObject UIGameover;
    public AudioSource soundvictory;
    public AudioSource soundgameover;
    void Start()
    {
        StageSetting();
    }

    void Update()
    {
        if (score == customersPerChapter && !check)
        {
            check = true;
            Debug.Log("Victory");
            StartCoroutine(functimer(5));
            soundvictory.Play();
            UIVictory.SetActive(true);
            Time.timeScale = 0;
        }

        if (GameOver && !check2)
        {
            check2 = true;
            Debug.Log("GameOver");
            soundgameover.Play();
            UIGameover.SetActive(true);
            Time.timeScale = 0;
        }
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

    void StageSetting()
    {
        Time.timeScale = 1;
        check = false;
        check2 = false;
        GameOver = false;
        if(stage == 1)
        {
            customersPerChapter = 2;
            Timelimit = 80f;
            ThingTask[0].SetActive(false);
            ThingTask[1].SetActive(false);
        }
        //------------------------------------------------------------
        if(stage == 2)
        {
            customersPerChapter = 3;
            Timelimit = 70f;
            ThingTask[0].SetActive(true);
            ThingTask[1].SetActive(false);
        }
        //------------------------------------------------------------
        if(stage == 3)
        {
            customersPerChapter = 5;
            Timelimit = 65f;
            ThingTask[0].SetActive(true);
            ThingTask[1].SetActive(false);
        }
        //------------------------------------------------------------
        if(stage == 4)
        {
            customersPerChapter = 5;
            Timelimit = 60f;
        }
        //------------------------------------------------------------
        if(stage == 5)
        {
            customersPerChapter = 6;
            Timelimit = 55f;
        }
        //------------------------------------------------------------
        if(stage == 6)
        {
            customersPerChapter = 6;
            Timelimit = 50f;
        }
        //------------------------------------------------------------
        if(stage == 7)
        {
            customersPerChapter = 6;
            Timelimit = 45;
        }
        //------------------------------------------------------------
        if(stage == 8)
        {
            customersPerChapter = 7;
            Timelimit = 40;
        }
        //------------------------------------------------------------

        if (stage > 3)
        {
            ThingTask[0].SetActive(true);
            ThingTask[1].SetActive(true);
        }
    }
}
