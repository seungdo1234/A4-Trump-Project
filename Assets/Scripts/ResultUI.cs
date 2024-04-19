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
    public void TextChange(ResultTextType type, string format)
    {
        resultTexts[(int)type].text = format;
    }

    // result UI On/Off
    public void UI_OnOff(bool isOn)
    {
        result_UI.SetActive(isOn);
    }
}
