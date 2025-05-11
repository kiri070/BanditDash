using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public abstract class AdmobUnitBase : MonoBehaviour
{
    private void Awake()
    {
        AppStateEventNotifier.AppStateChanged += OnAppStateChangedBase;
    }

    private void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChangedBase;
    }

    private void OnAppStateChangedBase(AppState state)
    {
        Debug.Log("App State changed to : " + state);
        OnAppStateChanged(state);
    }

    private IEnumerator Start()
    {
        while (AdmobManager.Instance.IsReady == false)
        {
            yield return 0;
        }
        Initialize();
    }
    protected virtual void Initialize()
    {
        // AdsManagerÇÃèâä˙âªÇ™èIÇÌÇ¡ÇΩÇ†Ç∆Ç…åƒÇŒÇÍÇÈ
    }
    protected virtual void OnAppStateChanged(AppState state)
    {
    }
}