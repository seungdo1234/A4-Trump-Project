using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty {Default, Easy, Normal, Hard}

public class DifficultyManager : MonoBehaviour
{
    
    public static DifficultyManager instance;

    // 게임의 난이도
    public Difficulty difficulty;
    
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
}
