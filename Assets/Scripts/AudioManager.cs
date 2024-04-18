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

    // 2024.04.18 - 은지, 기본 배경 음악으로 변경해주는 함수
    public void SwitchBGMtoStandard()
    {
        BGM_On(bgm);
    }
    public void BGM_On(AudioClip bgm)
    {
        audioSource.clip = bgm;
        audioSource.Play();
    }
    public void SwitchBGM(AudioClip newBGM)
    {
        audioSource.Stop();
        BGM_On(newBGM);
    }
    //2024.04.16
    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    // 2024.04.16
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}