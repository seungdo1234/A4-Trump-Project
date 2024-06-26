using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Card[] cards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject easyBoard;
    [SerializeField] private GameObject normalBoard;
    [SerializeField] private GameObject hardBoard;
    private Vector3[] cards_pos;

    private float speed = 5.0f;

    private IEnumerator StartAnim;

    void Start()
    {
        // 난이도 별로 카드  (Easy = 8, Normal = 16, Hard = 24)
        Init(8 * (int)DifficultyManager.instance.difficulty);
        SetPosition();
        //SetPosition으로 설정된 각 카드의 위치로 이동시키는 코루틴 실행
        for (int i=0; i<cards.Length; i++)
        {
            StartAnim = Card_Move(i);
            StartCoroutine(StartAnim);
        }

        StartCoroutine(CardPos_Match());
        ActivateBoardByDifficulty(); // 난이도에 따른 보드 활성화
    }

    private void SetPosition() // 카드 배치 함수
    {
        // 카드 배정
        for (int i = 0; i < cards.Length; i++)
        {

            float x = (i % 4) * 1.2f - 1.8f;
            float y = (i / 4) * 1.2f - 2.6f;

            cards_pos[i] = new Vector3(x, y, 0);
            //cards[i].transform.position = new Vector3(x, y , 0); 기존코드
        }

        // 난이도 별로 카드 위치 조정
        if (DifficultyManager.instance.difficulty == Difficulty.Easy)
        {
            transform.position += new Vector3(0, 1.5f, 0);
            for (int i = 0; i < cards.Length; i++)
                cards_pos[i] += new Vector3(0, 1.5f, 0);
        }
        else if (DifficultyManager.instance.difficulty == Difficulty.Hard)
        {
            transform.position += new Vector3(0, -1.5f, 0);
            for (int i = 0; i < cards.Length; i++)
                cards_pos[i] += new Vector3(0, -1.5f, 0);
        }
    }

    // 난이도에 따른 보드 활성화
    private void ActivateBoardByDifficulty()
    {
        if (easyBoard != null) easyBoard.SetActive(DifficultyManager.instance.difficulty == Difficulty.Easy);
        if (normalBoard != null) normalBoard.SetActive(DifficultyManager.instance.difficulty == Difficulty.Normal);
        if (hardBoard != null) hardBoard.SetActive(DifficultyManager.instance.difficulty == Difficulty.Hard);
    }
      
    // 초기화 함수
    public void Init(int count)
    {
        // Cards 배열 초기화
        cards = new Card[count];
        cards_pos = new Vector3[count];
        
        int num = 0;
        for (int i = 0; i < count; i++) // 생성된 카드마다 번호 부여
        {
            // 카드 생성
            GameObject card = Instantiate(cardPrefab, transform);
            cards[i] = card.GetComponent<Card>();
            cards[i].Setting(num); // 번호 부여

            if (i % 2 == 1) // 홀수가 됐을때 카드 번호 ++
            {
                num++;
            }
        }

        Shuffle(); // 카드 섞는 함수

        GameManager.instance.CardCount = cards.Length;
    } 
    private void Shuffle()
    {
        int random1, random2;
        Card tmp;
 
        // 2개의 랜덤 변수를 저장한 뒤 Swap
        for (int index = 0; index < cards.Length; ++index)
        {
            random1 = Random.Range(0, cards.Length);
            random2 = Random.Range(0, cards.Length);
 
            tmp = cards[random1];
            cards[random1] = cards[random2];
            cards[random2] = tmp;
        }
    }

    // 게임시작 카드 이동 애니메이션 코루틴
    private IEnumerator Card_Move(int idx)
    {
        //카드가 이동하는 동안에 버튼 컴포넌트를 비활성화 하여 이동중에 클릭이 되는 현상을 방지
        cards[idx].FlipBtn.enabled = false;
        while(cards[idx].transform.position != cards_pos[idx])
        {
            //Vector3.MoveTowards(시작지점,도착지점,이동량(속도)
            //현재 카드의 위치를 각 카드의 도착지점까지 speed * Time.deltaTime의 속도로 이동
            cards[idx].transform.position = Vector3.MoveTowards(cards[idx].transform.position, cards_pos[idx], speed * Time.deltaTime);

            //yield return null 은 1프레임당 이 코루틴을 1프레임당 실행시켜줌
            //도착할때까지 while문 반복을 통해 각자의 위치로 이동
            yield return null;
        }
        cards[idx].FlipBtn.enabled = true;
    }

    // 모든 카드가 제자리로 이동했는지 확인하는 코루틴
    private IEnumerator CardPos_Match() 
    {
        int i = 0;
        
        while (true)
        {
            if (cards[i].transform.position != cards_pos[i]) // 한 카드라도 지정된 위치에 도달하지 못했을 때
            {
                i = 0;
            }
            else 
            {
                i++;
                if (i == cards.Length - 1) // 모든 카드가 지정된 위치에 도달했을 때
                {
                    // 카드 배치 완료
                    GameManager.instance.isMove = false;
                    break;
                }
            }
            yield return null;
        }
    }
}
    