using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    public static AudioManager Instance;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip bgm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬을 이동해도 AudioManager가 파괴되지않음
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BGM_On(bgm);
    }

    
    // 효율적인 코딩
    public void BGM_On(AudioClip bgm)
    {
        // bgm을 플레이하는 코드
        audioSource.clip = bgm;
        audioSource.Play(); 
    }
    public void PlayUrgentMusic() // 긴박한 음악 재생 메서드 추가
    {
        audioSource.Stop(); // 배경 음악 중지
        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void StopUrgentMusic() // 긴박한 음악 중지 메서드 추가
    {
        audioSource.Stop();
        audioSource.clip = bgm;
        audioSource.Play(); // 배경 음악 재생
    }
}
