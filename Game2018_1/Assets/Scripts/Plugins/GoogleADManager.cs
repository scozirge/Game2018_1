using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleADManager : MonoBehaviour
{
    RewardBasedVideoAd rewardBasedVideo;
    InterstitialAd interstitial;
    BannerView bannerView;
    static GoogleADManager MySelf;

    string SampleRewardUnitID = "ca-app-pub-3940256099942544/5224354917";
    string SampleInterstirialID = "ca-app-pub-3940256099942544/1033173712";
    string MyRewardUnitID = "ca-app-pub-6853317566550401/8253800285";
    string MyInterstitialID = "ca-app-pub-6853317566550401/5405125729";
    bool IsTest = false;
    public static bool IsInit;

    public void Start()
    {
        MySelf = this;
        Debug.Log(MySelf);
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();
        //this.RequestInterstitial();
        DontDestroyOnLoad(gameObject);
        IsInit = true;
    }

    void RequestRewardBasedVideo()
    {
        string adUnitId;
#if UNITY_ANDROID
        if (IsTest)
            adUnitId = SampleRewardUnitID;
        else
            adUnitId = MyRewardUnitID;
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    void RequestInterstitial()
    {
        string adUnitId;
#if UNITY_ANDROID
        if (IsTest)
            adUnitId = SampleInterstirialID;
        else
            adUnitId = MyInterstitialID;
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
        BattleManager.Revive();
    }
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }
    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }
    
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        BattleManager.FailToRevive();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log("REVIVE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        BattleManager.Revive();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
        BattleManager.FailToRevive();
    }


    public static void CallRewardBasedVideo()
    {
        Debug.Log("////////////////////////////////////");
        if(Application.platform== RuntimePlatform.WindowsEditor)
        {
            Debug.LogWarning("編輯器直接復活");
            BattleManager.Revive();
        }
        else
        {
            if (!MySelf || !MySelf.rewardBasedVideo.IsLoaded())
            {
                BattleManager.Revive();
                Debug.LogWarning("googleAD尚未初始化:nofill");
                return;
            }
            if (MySelf.rewardBasedVideo.IsLoaded())
            {
                MySelf.rewardBasedVideo.Show();
            }
        }
    }
    public static void CallInterstitialAD()
    {
        if (!MySelf)
            return;
        if (MySelf.interstitial.IsLoaded())
        {
            MySelf.interstitial.Show();
        }
    }
}