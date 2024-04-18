using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Board : MonoBehaviour
{
    [SerializeField] private Card[] cards;
    [SerializeField] private GameObject cardPrefab;
    private Vector3[] cards_pos;

    private float speed = 5.0f;

    private IEnumerator StartAnim;

    void Start()
    {
        // 난이도 별로 카드  (Easy = 8, Normal = 16, Hard = 24)
        Init(8 * (int)DifficultyManager.instance.difficulty);
        SetPosition();
        for (int i=0; i<cards.Length; i++)
        {
            StartAnim = Card_Move(i);
            StartCoroutine(StartAnim);
        }
    }

    private void SetPosition() // 카드 배치 함수
    {
        // 카드 배정
        for (int i = 0; i < cards.Length; i++)
        {

            float x = (i % 4) * 1.2f - 1.8f;
            float y = (i / 4) * 1.2f - 2.6f;

            cards_pos[i] = new Vector3(x, y, 0);
            //cards[i].transform.position = new Vector3(x, y , 0);
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

    private IEnumerator Card_Move(int idx)
    {
        while(cards[idx].transform.position != cards_pos[idx])
        {
            cards[idx].transform.position = Vector3.MoveTowards(cards[idx].transform.position, cards_pos[idx], speed * Time.deltaTime);
            yield return null;
        }
    }
}
    