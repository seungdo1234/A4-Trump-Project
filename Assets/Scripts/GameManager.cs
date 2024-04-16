using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card FirstCard { get; set; }
    public Card SecondCard { get; set; }

    [SerializeField] private float maxTime;
    
    [SerializeField] private Text resultText; // 매칭 시도 횟수 텍스트
    private int matchingCount; // 매칭 시도 횟수를 저장하는 변수
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject endText;
    [SerializeField] private Image timerBackground; // 타이머 배경 이미지
    [SerializeField] private AudioClip speedAudioClip; // 긴박한 배경음악 클립
    [SerializeField] private Color timerTextColor = Color.red;


    private AudioSource audioSource;
    [SerializeField] private GameObject result_UI;

    [SerializeField] private Animator Timer_Anim;
    [SerializeField] private AudioClip matchAudio;
    [SerializeField] private AudioClip failAudio;
    public int CardCount { get; set; }
    public bool isPlay;
    private float timer = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
        matchingCount = 0; // 매칭 시도 횟수 초기화
        isPlay = false;
        timer = maxTime;
        audioSource = GetComponent<AudioSource>();
        timeText.color = Color.white; // 초기 색상 설정
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        //시간경과 > 타이머 숫자 감소
        timer -= Time.deltaTime;
        //타이머값 > ui 표시
        timeText.text = $"{timer:F2}";



        // 5초 이하일 때
        if (timer <= 5.1f && timer > 5f)

        {
            //텍스트색 붉게 변경[1f=red 0.5f=green/blue]
            timeText.color = timerTextColor;

            AudioManager.Instance.ChangeBGM(speedAudioClip);

        }
        else
        {
            // 타이머 이미지 배경색 원래대로 변경
            timerBackground.color = Color.white;

            Debug.LogError("player is null in GameManager.Update()");

            //배경음악 중지
            audioSource.Stop();
        }

        if (timer <= 0)
        {
            timer = 0;
            GameEnd();
            
        }
    }

    private void GameEnd()
    {
        // 매칭 시도 횟수 text 오브젝트에 저장
        resultText.text = $"매칭 시도 : <color=red>{matchingCount}</color>";
        isPlay = true;
        Time.timeScale = 0f;
        result_UI.SetActive(true);
    }

    public void Matched()
    {
        // 같은 카드라면
        if (FirstCard.idx == SecondCard.idx)
        {
            audioSource.PlayOneShot(matchAudio);
            FirstCard.DestroyCard();
            SecondCard.DestroyCard();
            CardCount -= 2;
            
            if (CardCount == 0)
            {
                GameEnd();
            }
        }
        else // 같지 않다면
        {
            audioSource.PlayOneShot(failAudio);
            FirstCard.CloseCard();
            SecondCard.CloseCard();
            Timer_Anim.SetTrigger("Fail");
            timer -= 2;
        }
        
        matchingCount++; // 매칭 시도 횟수 ++
        FirstCard = null;
        SecondCard = null;
    }
}
