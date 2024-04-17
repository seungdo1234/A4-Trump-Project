using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnLock : MonoBehaviour
{
    private DifficultyManager dif;
    
    [Header("# Object")]
    // 0번 : Normal버튼, 1번 : Hard버튼
    [SerializeField] private Button[] difficultyBtns;
    // 난이도 잠김 표시 오브젝트
    [SerializeField] private GameObject[] touchBlocks;
    
    private void Awake()
    {
        dif = DifficultyManager.instance;
    }

    private void Start()
    {
        // 난이도 해금 0: easy, 1: normal, 2: Hard
        dif.unLockDifficulty = PlayerPrefs.GetInt("Difficulty_Info");
        // 1일 때 => normal 해금, 2일 떄 => Hard 해금
        for (int i = 1; i < dif.unLockDifficulty + 1; i++)
        {
            // normal과 Hard는 0,1 번째에 들어가 있기 때문에 -1 해서 접근함
            difficultyBtns[i - 1].interactable = true;
            touchBlocks[i - 1].SetActive(false); // 난이도 잠금 표시 해제
        }
    }
}
