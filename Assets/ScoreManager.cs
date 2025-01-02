using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UnityEngine.UI namespace

public class ScoreManager : MonoBehaviour
{
    public Setting setting;
    public Text scoreText;
    private int currentScore = 0;
    private int maxScore = 5;
    void Update()
    {
        UpdateScoreUI();
    }

    // Update the UI text
    void UpdateScoreUI()
    {
        maxScore = setting.customersPerChapter;
        currentScore = setting.score;
        scoreText.text = "Score : " + currentScore + " / " + maxScore;
    }

}