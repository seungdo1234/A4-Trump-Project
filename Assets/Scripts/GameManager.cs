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

    [SerializeField] public float maxTime;//2024.04.18 박재우
    [SerializeField] private Text resultText; // 매칭 시도 횟수 텍스트
    public int matchingCount { get; private set; } // 2024.04.18 박재우
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject result_UI;
    [SerializeField] private GameObject MatchFailText; // 매칭이 실패했을 때 나오는 시간 차감 텍스트
    private AudioSource audioSource;
    [SerializeField] private AudioClip matchAudio;
    [SerializeField] private AudioClip bgm1; // 2024.04.16 박재우
    [SerializeField] private Animator Text_Animator; // 타이머 텍스트 애니메이션

    public int CardCount { get; set; } //2024.04.18 박재우
    [HideInInspector] public bool isPlay; //2024.04.18 박재우
    public float timer = 0; //2024.04.18 박재우
    private bool isBgm1Played = false; //// 2024.04.16 박재우
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