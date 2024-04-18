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
     public int idx = 0;

    private float Time_Limit; //카드가 열린 후 다음 카드 선택까지의 시간제한 변수
    private bool isOpen = false; //카드가 열린 것을 Update에서 확인하기 위한 bool 변수

    [SerializeField] private AudioClip flipAudio;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    private AudioSource audioSource;

    private IEnumerator AnimCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (isOpen)
        {
            Time_Limit -= Time.deltaTime;
            if(Time_Limit <= 0.0f)
            {
                CloseCardInvoke();
                isOpen = false;
                GameManager.instance.FirstCard = null;
            }
        }
    }

    public void Setting(int number) // 카드 초기화
    {
     //   Debug.Log(number);
        idx = number;
        
        // Resources.Load<형태>("리소스 이름");
        rtanImage.sprite =  Resources.Load<Sprite>($"four{idx}");
    }

    public void OpenCard() // 카드 오픈
    {
        if (GameManager.instance.isPlay)
        {
            return;
        }
        
        if(GameManager.instance.SecondCard != null) return;

        isOpen = true; //카드가 뒤집힌 것을 확인해주는 Bool을 true로 변경
        Time_Limit = 2.0f; //카드가 뒤집힌 시점을 기준으로 시간제한 초기화
        //Card에 붙여둔 Card_Flipped 스크립트를 가져와 FlippedCard 실행
        GetComponent<Card_Flipped>().Flipped_Card();
        audioSource.PlayOneShot(flipAudio); // PlayOneShot : 오디오끼리 겹치지 않음
        anim.SetBool("isOpen",true);

        AnimCoroutine = WaitForAnim(0.1f);
        StartCoroutine(AnimCoroutine);
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
        StopCoroutine(AnimCoroutine);
        anim.SetBool("isOpen",false);
        back.SetActive(true);
        front.SetActive(false);
    }

    private IEnumerator WaitForAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        back.SetActive(false);
        front.SetActive(true);
        yield return new WaitForSeconds(waitTime);

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
}
