using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using System.Collections;

public class RewardedBtn : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
     [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    private bool Ad_End = false;
        
    void Start()
    {   
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Disable the button until the ad is ready to show:
        // _showAdButton.interactable = false;
        LoadAd();
    }
    
 
    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Advertisement.Load(_adUnitId, this);
        
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        
        if (adUnitId.Equals(_adUnitId))
        {
            Debug.Log("광고 로드 성공");
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // 2024.04.18 - 은지, 광고 보여질 때 BGM 멈추기
        AudioManager.instance.StopBGM();
        // 2024.04.19 - 시원, 비동기 LoadScene Coroutine 시작
        StartCoroutine(LoadScene());
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            // 2024.04.19 - 시원, 기존의 LoadScene 삭제 및 광고가 끝났다는 Bool 값 변경
            Ad_End = true;
            // 2024.04.18 - 은지, 시작 씬 로드될 때 배경음악 변경 & play
            AudioManager.instance.SwitchBGMtoStandard();
            Time.timeScale = 1.0f;
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }

    //2024.04.19 시원 - 비동기식 LoadScene Coroutine
    IEnumerator LoadScene()
    {
        //비동기식으로 LoadScene 시작, LoadSceneMode.Additive > LoadScene방식을 현재 Scene에 추가로 Scene을 더 Load하는 방식
        UnityEngine.AsyncOperation asyncOper = SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Additive);

        //광고가 끝나고 LoadSceneAsync가 완료될때까지 While 반복
        while (!Ad_End || !asyncOper.isDone)
        {
            yield return null;
        }

        //광고가 끝나고 SceneLoad가 끝났다면 ActiveScene을 StartScene으로 변경해주고 MainScene은 Unload
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("StartScene"));
        SceneManager.UnloadSceneAsync("MainScene");
    }
}