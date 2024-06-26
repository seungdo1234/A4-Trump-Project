    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer a4_Image;
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

    [HideInInspector] public Button FlipBtn;
    
    //카드 뒤집기 애니메이션 실행 중 대기 코루틴
    private IEnumerator AnimCoroutine;

    private void Awake()
    {
        FlipBtn = back.GetComponent<Button>();
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
        a4_Image.sprite =  Resources.Load<Sprite>($"four{idx}");
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
        anim.SetTrigger("isOpen");

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
        // 24.04.18 승도 => 카드 자동 Close. 이슈 해결
        isOpen = false;
        Invoke(nameof(CloseCardInvoke), 0.5f);
    }
    public void CloseCardInvoke() // 카드 클로즈
    {
        StopCoroutine(AnimCoroutine);
        anim.SetTrigger("isOpen");
        back.SetActive(true);
        front.SetActive(false);
    }

    //카드 애니메이션 재생 대기 코루틴
    private IEnumerator WaitForAnim(float waitTime)
    {
        //yield return new WaitForSeconds(float)는 매개변수의 값만큼의 Seconds 이후 뒤의 작업 실행
        yield return new WaitForSeconds(waitTime);
        back.SetActive(false);
        front.SetActive(true);
        yield return new WaitForSeconds(waitTime);

        // 카드 애니메이션이 재생되는 동안 카드가 매치 되어 사라지는 것을 방지하기 위해 코드 이동
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
