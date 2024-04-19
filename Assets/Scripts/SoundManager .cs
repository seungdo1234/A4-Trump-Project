using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour //2024.04.19 박재우
{
    private static SoundManager instance; // SoundManager 인스턴스
    private AudioSource soundSource; // 사운드를 재생할 AudioSource

    void Awake()
    {
        if (instance == null) // 인스턴스가 없으면
        {
            instance = this; // 현재 인스턴스를 설정
            DontDestroyOnLoad(gameObject); // 씬 전환이 되어도 파괴되지 않음
            soundSource = gameObject.AddComponent<AudioSource>(); // AudioSource를 추가하여 사운드를 재생할 준비
        }
        else // 인스턴스가 이미 존재하면?
        {
            Destroy(gameObject); // 새로 생성된 인스턴스 파괴
        }
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip); // 주어진 AudioClip을 재생
    }
}
