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

        // 해당 난이도의 최단 시간 정보를 불러옴
        float sT = PlayerPrefs.GetFloat(dif.difficulty + "ShortTime");
        // 만약 해당 난이도에 저장된 최단 시간이 없다면 ?, 있다면 소수점 2자리까지 문자열로 변환 후 저장
        string shortTime = sT != 0 ? sT.ToString("F2") : "?";

        // 난이도 마다 컬러를 설정해줌
        string color = dif.difficulty switch // swith 식
        {
            Difficulty.Easy => "yellow",
            Difficulty.Normal => "aqua",
            Difficulty.Hard => "red",
            _ => throw new ArgumentException($"난이도 설정이 잘못됐습니다 !")// 예외 처리 
        };
        
        // 텍스트에 저장
        shortTimeText.text = $"<color={color}>{dif.difficulty}</color> 기록 {shortTime} 초";

    }
}