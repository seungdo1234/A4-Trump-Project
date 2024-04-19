using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 텍스트의 타입을 나타내는 열거형
public enum ResultTextType {Title, Match, Score}

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Text[] resultTexts;
    [SerializeField] private GameObject result_UI;

    // 텍스트를 바꾸는 함수
    /// <summary>
    ///  설명
    /// </summary>
    /// <param name="type">타입설명</param>
    /// <param name="text"></param>
    public void TextChange(ResultTextType type, string text)
    {
        resultTexts[(int)type].text = text;
    }

    // result UI On/Off
    public void UI_OnOff(bool isOn)
    {
        result_UI.SetActive(isOn);
    }
}
