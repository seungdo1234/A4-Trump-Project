using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private float lerpTime;
    [SerializeField] private bool isMainScene;
    private Image loadingImage;

    private void Awake()
    {
        loadingImage = GetComponentInChildren<Image>();
        
        StartCoroutine(StartLoading(0, 1));
    }

    public void MoveScene()
    {
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

    }
}
