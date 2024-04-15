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
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject endText;
    [SerializeField] private Image timerBackground; // 타이머 배경 이미지
    [SerializeField] private AudioClip speedAudioClip; // 긴박한 배경음악 클립

    private AudioSource audioSource;
    [SerializeField] private AudioClip matchAudio;
    public int CardCount { get; set; }
    public bool isPlay;
    private float timer = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isPlay = false;
        timer = maxTime;
        audioSource = GetComponent<AudioSource>();
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

        // 10초 이하일 때
        if (timer <= 10f) 
        {
            //배경색 붉게 변경[1f=red 0.5f=green/blue]
            timerBackground.color = new Color(1f, 0.5f, 0.5f);

            //배경음악이 재생 중이 아닌 경우
            if (!audioSource.isPlaying)
            {
                //audioSource의 클립 = 긴박한 배경음악 클립
                audioSource.clip = speedAudioClip;
                //긴박한 배경음악 재생
                audioSource.Play();
            }
        }
        else
        {
            // 타이머 이미지 배경색 원래대로 변경
            timerBackground.color = Color.white;

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
        isPlay = true;
        Time.timeScale = 0f;
        endText.SetActive(true);
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
            FirstCard.CloseCard();
            SecondCard.CloseCard();
        }

        FirstCard = null;
        SecondCard = null;
    }
}
