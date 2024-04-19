using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartSound : MonoBehaviour //2024.04.19 박재우
{
    public AudioClip soundClip; // 재생할 사운드 클립 
    private AudioSource soundSource; // 오디오 소스

    void Start()
    {
        soundSource = GetComponent < AudioSource >(); // 해당 게임 오브젝트의 AudioSource 컴포넌트 가져오기
        Button button = GetComponent<Button>(); // 해당 게임 오브젝트의 Button 컴포넌트 가져오기
        button.onClick.AddListener(PlaySound); // 버튼 클릭 이벤트에 PlaySound 메소드를 추가
    }

    public void PlaySound()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>(); // Scene에서 SoundManager 오브젝트를 찾아서 가져옴
        if (soundManager != null) // SoundManager가 존재하면
        {
            soundManager.PlaySound(soundClip); // SoundManager의 PlaySound 메소드를 사용하여 사운드를 재생
        }
    }
}
