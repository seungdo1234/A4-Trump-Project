using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rtanImage;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [HideInInspector] public int idx = 0;

    [SerializeField] private AudioClip flipAudio;
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number) // 카드 초기화
    {
     //   Debug.Log(number);
        idx = number;
        
        // Resources.Load<형태>("리소스 이름");
        rtanImage.sprite =  Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard() // 카드 오픈
    {
        if (GameManager.instance.isPlay)
        {
            return;
        }
        
        if(GameManager.instance.SecondCard != null) return;
        
        audioSource.PlayOneShot(flipAudio); // PlayOneShot : 오디오끼리 겹치지 않음
        anim.SetBool("isOpen",true);
        back.SetActive(false);
        front.SetActive(true);
        
        // 카드 비교
        if (GameManager.instance.FirstCard == null)
        {
            // FirstCard에 내 정보를 넘겨준다.
            GameManager.instance.FirstCard = this;
        } // FirstCard가 비어 있지 않다면
        else
        {
            // SecondCard에 내 정보를 넘겨준다.
            GameManager.instance.SecondCard = this;
            GameManager.instance.Matched(); // 매치
        }
    }
    public void DestroyCard() // 카드 파괴
    {
        Invoke(nameof(DestroyCardInvoke), 0.5f);
    }
    public void DestroyCardInvoke() // 카드 파괴
    {
        Destroy(gameObject);
    }

    public void CloseCard() // 카드 클로즈
    {
        Invoke(nameof(CloseCardInvoke), 0.5f);
    }
    public void CloseCardInvoke() // 카드 클로즈
    {
        anim.SetBool("isOpen",false);
        back.SetActive(true);
        front.SetActive(false);
    }
}
