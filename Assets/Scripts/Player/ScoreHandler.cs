using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{

    public int score { get; private set; } = 0;
    [SerializeField] TMP_Text scoreText;
    public static ScoreHandler instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

}
