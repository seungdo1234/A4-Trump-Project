using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    void Start()
    {
        // 카드 번호
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        
        // 카드 셔플
        arr = arr.OrderBy(_ => Random.Range(0, 8)).ToArray();
        
        // 카드 배정
        for (int i = 0; i < 16; i++)
        {
            GameObject card = Instantiate(cardPrefab, transform);

            float x = (i % 4) * 1.2f - 1.8f;
            float y = (i / 4) * 1.2f - 2.6f;
            
            card.transform.position = new Vector3(x, y , 0);
            card.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.instance.CardCount = arr.Length;
    }

}
    