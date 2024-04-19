using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance ;
    public Card FirstCard { get; set; }
    public Card SecondCard { get; set; }
    
    [Header("# Game Information")]
    public float maxTime;
    
    [Header("# Component")]
    [SerializeField] private ResultUI resultUI; // 매칭 시도 횟수 텍스트

    public ResultUI ResultUI { get => resultUI; }
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject MatchFailText; // 매칭이 실패했을 때 나오는 시간 차감 텍스트
    private AudioSource audioSource;
    [SerializeField] private AudioClip matchAudio;
    [SerializeField] private AudioClip bgm1; // 2024.04.16
    [SerializeField] private AudioClip FailAudio; // 2024.04.18
    [SerializeField] private AudioClip clearBGM; // 2024.04.19
    [SerializeField] private AudioClip overBGM; // 2024.04.19
    [SerializeField] private Animator Text_Animator; // 타이머 텍스트 애니메이션
    [SerializeField] private Score score;
    public string[] cardNames; // 이미지에 매칭될 이름 배열
    public Text displayText; // 텍스트를 표시할 UI(Text) 요소
    public float displayTime = 1f; // 텍스트가 표시될 시간
    private float displayTimer = 0f; // 텍스트 표시 타이머

    // 카드가 배치될 때 true
    [HideInInspector] public bool isMove;
    
    public int CardCount { get; set; }
    [HideInInspector]public bool isPlay;
    [HideInInspector]public float timer = 0;
    private bool isBgm1Played = false; // 2024.04.16
    private bool isClear = false; // 2024.04.19
    private MatchFailText Match_Fail;
    [HideInInspector]public int matchingCount; // 매칭 시도 횟수를 저장하는 변수
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
        isMove = true;
        timeText.text = $"{maxTime}";
        timeText.color = Color.white;
    }
    void Update()
    {
        if (!isMove)
        {
            timer -= Time.deltaTime;
            timeText.text = $"{timer:F2}";
            if ( !isBgm1Played && timer > 0.1f && timer <= 5f  )  // 2024.04.16
            {
                AudioManager.instance.SwitchBGM(bgm1); // 2024.04.16
                isBgm1Played = true;
                timeText.color = Color.red;
            }
            if (timer < 0 )
            {
                resultUI.TextChange(ResultTextType.Title,  $"Game Over !");
                timer = 0;
                GameEnd();
            }
            // 텍스트가 활성화되어 있고, 표시 시간이 지났으면 비활성화
            if (displayText.gameObject.activeSelf && displayTimer > 0f)
            {
                displayTimer -= Time.deltaTime;
                if (displayTimer <= 0f)
                {
                    displayText.gameObject.SetActive(false);
                }
            }
        }
    }
    private void GameEnd()
    {
        // 2024.04.19
        AudioManager.instance.StopBGM();
        if (isClear)
        {
            audioSource.PlayOneShot(clearBGM);
        }
        else
        {
            audioSource.PlayOneShot(overBGM);
        }
        // 매칭 시도 횟수 text 오브젝트에 저장S
       // resultText.text = $"매칭 시도 : <color=red>{matchingCount}</color>번";
       // 24.4.19 승도 => 코드 리팩토링
        string format = $"매칭 시도 : <color=red>{matchingCount}</color>번";
        resultUI.TextChange(ResultTextType.Match,  format);
        isPlay = true;
        Time.timeScale = 0f;
        resultUI.UI_OnOff(true);
    }
    public void Matched()
    {
        // 같은 카드라면
        if (FirstCard.idx == SecondCard.idx)
        {
            score.UpdateScoreText();
            
            // 매칭된 카드의 이미지에 해당하는 텍스트 표시
            displayText.text = cardNames[FirstCard.idx]; // 카드의 idx에 해당하는 이름을 가져와서 표시

            audioSource.PlayOneShot(matchAudio);
            FirstCard.DestroyCard();
            SecondCard.DestroyCard();
            CardCount -= 2;

            // 매칭이 이루어지면 텍스트를 활성화하고 displayTime 이후에 다시 비활성화합니다.
            displayText.gameObject.SetActive(true);
            displayTimer = displayTime; // 텍스트 표시 타이머 초기화

            if (CardCount == 0)
            {
                resultUI.TextChange(ResultTextType.Title,  $"Game Clear !");
                isClear = true;
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
            audioSource.PlayOneShot(FailAudio);
        }
        
        matchingCount++; // 매칭 시도 횟수 ++
        FirstCard = null;
        SecondCard = null;
    }
}