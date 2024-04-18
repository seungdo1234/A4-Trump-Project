using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour //2024.04.18 박재우
{
    public Text endText;
    private GameManager gameManager;
    public Difficulty dif;
    int cardCount = 0;

    private void Start()
    {
        gameManager = GameManager.instance;
        UpdateScoreText();

        switch (dif)
        {
            case Difficulty.Easy:
                cardCount = 8;
                break;
            case Difficulty.Normal:
                cardCount = 16;
                break;
            case Difficulty.Hard:
                cardCount = 24;
                break;
            default:
                cardCount = 8;
                break;
        }
    }

    private void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        float timePercentage = gameManager.timer / gameManager.maxTime * 100f;
        int score = CalculateScore(timePercentage);
        endText.text = "점수: " + score.ToString();
    }

    private int CalculateScore(float timePercentage)
    {
        int Timescore = Mathf.Max(0, Mathf.RoundToInt(50f - (50f * (100f - timePercentage) / 100f)));
        
        float cardPercentage;
        float cardScore = 0f;
        int score = 0;
       
        int matchingCount = GameManager.instance.matchingCount;

        if (matchingCount == 0)
        {
            cardScore = 0f;
        }

        else
        {
            cardPercentage = ((float)(GameManager.instance.CardCount) / cardCount) * 100f;
            if (matchingCount >= 0 && matchingCount <= cardCount) // 0~8
            {
                cardScore = 50f; // 50점
            }
            else if (matchingCount > cardCount && matchingCount <= cardCount * 1.5f) // 9~12
            {
                cardScore = 40f; // 40점
            }
            else if (matchingCount > cardCount * 1.5f && matchingCount <= cardCount * 2) // 13~16
            {
                cardScore = 30f; // 30점
            }
            else if (matchingCount > cardCount * 2 && matchingCount <= cardCount * 2.5f) // 17~20
            {
                cardScore = 20f; // 20점
            }
            else if (matchingCount > cardCount * 2.5f) // 21~
            {
                cardScore = 10f; // 10점
            }
        }

        Debug.Log("TimeScore : " + Timescore);
        Debug.Log("cardScore : " + cardScore);
        Debug.Log("score : " + score);
        Debug.Log("matchingCount : " + matchingCount);
        score = Timescore + (int)cardScore;

        return score;
    }
}
