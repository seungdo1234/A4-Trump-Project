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
    
    [SerializeField] private float maxTime;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject endText;

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

        timer -= Time.deltaTime;
        timeText.text = $"{timer:F2}";

        if (timer <= 0 )
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
