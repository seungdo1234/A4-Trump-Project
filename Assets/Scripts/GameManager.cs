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
    [SerializeField] private GameObject result_UI;
    [SerializeField] private GameObject MatchFailText; // 매칭이 실패했을 때 나오는 시간 차감 텍스트
    private AudioSource audioSource;
    [SerializeField] private AudioClip matchAudio;
    [SerializeField] private AudioClip bgm1; // 2024.04.16
    [SerializeField] private Animator Text_Animator; // 타이머 텍스트 애니메이션

    public int CardCount { get; set; }
    [HideInInspector] public bool isPlay;
    private float timer = 0;
    private bool isBgm1Played = false; //// 2024.04.16
    private MatchFailText Match_Fail;

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
        Match_Fail = MatchFailText.GetComponent<MatchFailText>();
    }
    private void Start()
    {
        Time.timeScale = 1f;

        timeText.color = Color.white;
    }
    void Update()
    {

        timer -= Time.deltaTime;
        timeText.text = $"{timer:F2}";
        if (!isBgm1Played && timer > 0.1f && timer <= 5f)  // 2024.04.16
        {
            AudioManager.instance.SwitchBGM(bgm1); // 2024.04.16
            isBgm1Played = true;
            timeText.color = Color.red;
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
            FirstCard.CloseCard();
            SecondCard.CloseCard();
            MatchFailText.SetActive(true);
            timer -= 2;
            Match_Fail.Fail();
            Text_Animator.SetTrigger("Fail");
        }
        matchingCount++; // 매칭 시도 횟수 ++
        FirstCard = null;
        SecondCard = null;
    }
}