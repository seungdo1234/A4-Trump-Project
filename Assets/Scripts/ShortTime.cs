using System;
using UnityEngine;
using UnityEngine.UI;

public class ShortTime : MonoBehaviour
{

    private DifficultyManager dif;
    private Text shortTimeText;
    private void Awake()
    {
        shortTimeText = GetComponent<Text>();
        dif = DifficultyManager.instance;
    }

    private void Start()
    {

        float sT = PlayerPrefs.GetFloat(dif.difficulty + "ShortTime");
        // object shortTime = sT != 0 ? sT : "?"; 박싱 언박싱 위험 발생 !
        string shortTime = sT != 0 ? sT.ToString("F2") : "?";

        // 난이도 마다 컬러를 설정해줌
        string color = dif.difficulty switch // swith 식
        {
            Difficulty.Easy => "yellow",
            Difficulty.Normal => "aqua",
            Difficulty.Hard => "red",
            _ => throw new ArgumentException($"난이도 설정이 잘못됐습니다 !") // default
        };
        
        shortTimeText.text = $"<color={color}>{dif.difficulty}</color> 기록 {shortTime} 초";

    }
}
