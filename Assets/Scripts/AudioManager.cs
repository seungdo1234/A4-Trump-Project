using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip bgm;

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
}
