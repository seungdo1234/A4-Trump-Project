using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Score : MonoBehaviour //2024.04.18      
{
    [SerializeField] private ResultUI resultUI;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }
    
    public void UpdateScoreText()
    {
        float timePercentage = gm.timer / gm.maxTime * 100f;
        int score = CalculateScore(timePercentage);
       // resultUI.TextChange(ResultTextType.Score,  $"<color=red>{score}</color> 점");
       gm.ResultUI.TextChange(ResultTextType.Score,  $"<color=red>{score}</color> 점");
    }

    private int CalculateScore(float timePercentage)
    {
        int Timescore = Mathf.Max(0, Mathf.RoundToInt(50f - (50f * (100f - timePercentage) / 100f)));
        
        float cardPercentage;
        float cardScore = 0f;
        int score = 0;
       
        int matchingCount = gm.matchingCount;
        
        
        if (matchingCount == 0)
        {
            cardScore = 0f;
        }
        else
        {
            cardPercentage = ((float)(gm.CardCount) / gm.CardCount) * 100f;
            
            if (matchingCount >= 0 && matchingCount <= gm.CardCount) // 0~8
            {
                cardScore = 50f; // 50  
            }
            else if (matchingCount > gm.CardCount && matchingCount <= gm.CardCount * 1.5f) // 9~12
            {
                cardScore = 40f; // 40  
            }
            else if (matchingCount > gm.CardCount * 1.5f && matchingCount <= gm.CardCount * 2) // 13~16
            {
                cardScore = 30f; // 30  
            }
            else if (matchingCount > gm.CardCount * 2 && matchingCount <= gm.CardCount * 2.5f) // 17~20
            {
                cardScore = 20f; // 20  
            }
            else if (matchingCount > gm.CardCount * 2.5f) // 21~
            {
                cardScore = 10f; // 10  
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
 