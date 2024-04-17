using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty {Default, Easy, Normal, Hard}

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;
    
    [Header("# Game Difficulty Information")]
    // 게임의 난이도
    public Difficulty difficulty;
    // 지금까지 언락된 난이도 Normal : 1
    [HideInInspector]public int unLockDifficulty;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬을 이동해도 AudioManager가 파괴되지않음
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    public void UnLock()
    {
        // 현재 언락된 난이도가 현재 진행중인 게임의 난이도이고 언락된 난이도가 이지, 노말일 때
        if (unLockDifficulty == (int)difficulty - 1 && unLockDifficulty < 2)
        {
            // 난이도 데이터 저장 (이지 -> 노말) (노말 -> 하드)
            PlayerPrefs.SetInt("Difficulty_Info",++unLockDifficulty );
        }
        
    } 
}
