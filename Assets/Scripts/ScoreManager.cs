using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour //2024.04.18 �����
{
    public Text endText;
    public GameManager gm;
    private DifficultyManager dif;
    int cardCount = 0;
    
    private void Start()
    {
        gm = GameManager.instance;
        dif = DifficultyManager.instance;
        
        switch (dif.difficulty)
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
    
    public void UpdateScoreText()
    {
        Debug.Log("ddd");
        float timePercentage = gm.timer / gm.maxTime * 100f;
        Debug.Log("ddd");
        int score = CalculateScore(timePercentage);
        endText.text = $"결과 : {score}";
    }

    private int CalculateScore(float timePercentage)
    {
        int Timescore = Mathf.Max(0, Mathf.RoundToInt(50f - (50f * (100f - timePercentage) / 100f)));
        
        Debug.Log("ddd");
        float cardPercentage;
        float cardScore = 0f;
        int score = 0;
       
        int matchingCount = gm.matchingCount;

        Debug.Log("ddd");
        
        if (matchingCount == 0)
        {
            cardScore = 0f;
        }
        else
        {
            cardPercentage = ((float)(gm.CardCount) / cardCount) * 100f;
            Debug.Log("ddd");
            if (matchingCount >= 0 && matchingCount <= cardCount) // 0~8
            {
                cardScore = 50f; // 50��
            }
            else if (matchingCount > cardCount && matchingCount <= cardCount * 1.5f) // 9~12
            {
                cardScore = 40f; // 40��
            }
            else if (matchingCount > cardCount * 1.5f && matchingCount <= cardCount * 2) // 13~16
            {
                cardScore = 30f; // 30��
            }
            else if (matchingCount > cardCount * 2 && matchingCount <= cardCount * 2.5f) // 17~20
            {
                cardScore = 20f; // 20��
            }
            else if (matchingCount > cardCount * 2.5f) // 21~
            {
                cardScore = 10f; // 10��
            }
            Debug.Log("ddd");
        }

        Debug.Log("TimeScore : " + Timescore);
        Debug.Log("cardScore : " + cardScore);
        Debug.Log("score : " + score);
        Debug.Log("matchingCount : " + matchingCount);
        score = Timescore + (int)cardScore;
        Debug.Log("ddd");
        return score;
    }
}
