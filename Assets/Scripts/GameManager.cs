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
    [SerializeField] private AudioClip bgm1; // 2024.04.16 박재우
    [SerializeField] private Animator Text_Animator; // 타이머 텍스트 애니메이션

    public int CardCount { get; set; }
    [HideInInspector] public bool isPlay;
    private float timer = 0;
    private bool isBgm1Played = false; //// 2024.04.16 박재우
    private MatchFailText Match_Fail;
    public Text endText; // 2024.04.17 박재우

    //2024.04.18 박재우

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    public Difficulty difficulty;
    int cardCount = 0;

    //2024.04.18--박재우


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

        //2024.04.18 박재우

        switch (difficulty)
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

        //2024.04.18 -- 박재우


    }
    void Update()
    {

        timer -= Time.deltaTime;
        timeText.text = $"{timer:F2}";
        if (!isBgm1Played && timer > 0.1f && timer <= 5f)  // 2024.04.16 박재우
        {
            AudioManager.instance.SwitchBGM(bgm1); // 2024.04.16 박재우
            isBgm1Played = true;
            timeText.color = Color.red;
        }
        if (timer <= 0)
        {
            timer = 0;
            GameEnd();
            
        if (timer <= 0)

                    isPlay = true;
        Time.timeScale = 0f;
        result_UI.SetActive(true);

        }
    }
    private void GameEnd()
    {
        // 매칭 시도 횟수 text 오브젝트에 저장

        isPlay = true;
        Time.timeScale = 0f;
        result_UI.SetActive(true);

        //2024.04.18 박재우
        float cardPercentage; // 이거 2개는 그냥 float 변수 생성
        float cardScore = 0f;

        if (matchingCount == 0) // 시간 다 쓸때까지 매칭시도를 한번도 안했을 때
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

        float timePercentage = (timer / maxTime) * 100f; // 타이머 사용 시간 백분율
        float timeScore = Mathf.Max(0, 50f - (50f * (100f - timePercentage) / 100f)); // 타이머 시간을 기준으로 한 점수


        float totalScore = timeScore + cardScore; // 총 점수 박재우

        // Debug.Log("카드 백분율: " + cardPercentage);
        Debug.Log("매칭시도횟수: " + matchingCount);
        Debug.Log("카드 점수: " + cardScore);
        // Debug.Log("타이머 백분율: " + timePercentage);
        Debug.Log("타이머 점수: " + timeScore);
        Debug.Log("총 점수: " + totalScore);

        // Debug.Log("GameManager CardCount: " + GameManager.instance.CardCount);
        // Debug.Log("CardCount: " + CardCount);

        endText.text = $"점수 : {totalScore:F0}"; // 점수 표시

        //2024.04.18 박재우

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
                float shortTime = maxTime - timer; 
                DifficultyManager.instance.UnLock(shortTime);
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