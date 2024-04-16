using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyUI : MonoBehaviour
{
    
    // 난이도 설정
    public void SetDifficulty(int dif)
    {
        DifficultyManager.instance.difficulty = (Difficulty)dif;
        
        SceneManager.LoadScene("MainScene");
    }
}
