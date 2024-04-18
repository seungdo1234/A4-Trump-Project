using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private float lerpTime;
    [SerializeField] private bool isMainScene;
    private Image loadingImage;

    private void Awake()
    {
        loadingImage = GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }

    public void MoveScene()
    {
        Time.timeScale = 1.0f;
        if (isMainScene)
        {
            StartCoroutine(StartLoading(0, 1));
        }
        else
        {
            StartCoroutine(StartLoading(1, 0));
        }
    }

    private IEnumerator StartLoading(float start, float end)
    {
        float currentTime = 0f;

        while (currentTime < lerpTime)
        {
            currentTime += Time.deltaTime;

            loadingImage.fillAmount = Mathf.Lerp(start, end, currentTime);
                
            
            yield return null;
        }
        // 2024.04.18 - 은지, 시작 씬 로드될 때 배경음악 변경 & play
        AudioManager.instance.SwitchBGMtoStandard();
        SceneManager.LoadScene("StartScene");
    }
}
