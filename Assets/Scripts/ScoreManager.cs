using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour //2024.04.18      
{
    public Text endText; // 결과를 표시할 UI 텍스트
    public GameManager gm; // 게임 매니저 객체
    private DifficultyManager dif; // 난이도 매니저 객체

    private void Start()
    {
        gm = GameManager.instance; // 게임 매니저 객체를 가져옴
    }

    public void UpdateScoreText() // 점수를 UI에 업데이트하는 메서드
    {
        float timePercentage = gm.timer / gm.maxTime * 100f; // 타이머, 최대 시간에 대한 백분율
        int score = CalculateScore(timePercentage); // CalculateScore 메서드를 사용하여 점수 계산
        endText.text = $"결과 : {score}"; // Text에 점수를 표시
    }

    private int CalculateScore(float timePercentage) // 점수를 계산하는 메서드
    {
        int Timescore = Mathf.Max(0, Mathf.RoundToInt(50f - (50f * (100f - timePercentage) / 100f))); // 위에서 가져온 timePercentage에 대한 계산식

        float cardPercentage; // 카드 백분율 변수
        float cardScore = 0f; // 카드 점수
        int score = 0; // 최종 점수

        int matchingCount = gm.matchingCount; // 일치하는 카드의 수 가져와서 int 로 선언

        if (matchingCount == 0) // 카드 매칭을 한번도 시도 안했다면?
        {
            cardScore = 0f; // 점수는 0점
        }
        else // 아니면?
        {
            cardPercentage = ((float)(gm.CardCount) / gm.CardCount) * 100f; // 카드 매칭점수 계산식

            // 매칭 횟수에 따른 점수 범위
            if (matchingCount >= 0 && matchingCount <= gm.CardCount) // 0 ~ 1배
            {
                cardScore = 50f; // 50점
            }
            else if (matchingCount > gm.CardCount && matchingCount <= gm.CardCount * 1.5f) // 1배초과 ~ 1.5배
            {
                cardScore = 40f; // 40점
            }
            else if (matchingCount > gm.CardCount * 1.5f && matchingCount <= gm.CardCount * 2) // 1.5배 초과 ~ 2배
            {
                cardScore = 30f; // 30점
            }
            else if (matchingCount > gm.CardCount * 2 && matchingCount <= gm.CardCount * 2.5f) // 2배 초과~ 2.5배
            {
                cardScore = 20f; // 20점
            }
            else if (matchingCount > gm.CardCount * 2.5f) // 2.5배 초과 (최소점수)
            {
                cardScore = 10f; // 10점
            }
        } // 1배수부터 시작해서 2.5배수 이상까지의 점수를 계산하는 계산식입니다.

        Debug.Log("TimeScore : " + Timescore); // TimeScore 로그
        Debug.Log("cardScore : " + cardScore); // cardScore 로그
        Debug.Log("score : " + score); // score 로그
        Debug.Log("matchingCount : " + matchingCount); // matchingCount 로그
        //: 콘솔에서 수치를 확인하기 위해서

        score = Timescore + (int)cardScore; // 최종 점수 계산

        return score; // 최종 점수 처음으로 반환
    }
}