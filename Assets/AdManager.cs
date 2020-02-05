using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class AdManager : MonoBehaviour
{
    //private BannerView bannerAd;
    //private InterstitialAd fullscreenAd;
    private RewardBasedVideoAd videoAd;
    //public GameObject videoReward;
    void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-6633468813011566~5952062465";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-6633468813011566~7514026693";
#else
        string appId = "unexpected_platform";
#endif
        MobileAds.Initialize(appId);
        RequestAds();
    }


    //VIDEO
    void RequestVideoAd()
    {
#if UNITY_ANDROID
        string videoAdId = "ca-app-pub-3940256099942544/5224354917"; // for testing ads id zonurg
#elif UNITY_IPHONE
        string videoAdId = "ca-app-pub-3940256099942544/1712485313"; 
#else
        string videoAdId = "unexpected_platform";
#endif

        videoAd = RewardBasedVideoAd.Instance;

        AdRequest adreq = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        //AdRequest adRequest = new AdRequest.Builder().Build(); // when not testing

        videoAd.LoadAd(adreq, videoAdId);
    }
    public void DisplayVideoAd()
    {
        if (videoAd.IsLoaded())
        {
            videoAd.Show();
            videoAd.OnAdRewarded += (sender, args) =>
            {
                //ÖDÜL
                //videoReward.SetActive(true);
                //videoReward.transform.GetChild(0).GetComponent<Text>().text = "VIDEO\nREWARD";
                Debug.Log("YOU ARE REWARDED");
            };
            videoAd.OnAdClosed += (sender, args) =>
            {
                this.RequestVideoAd();
                Debug.Log("AD IS CLOSED");
            };
        }
        else
        {
            Debug.Log("VideoAd not loaded");
            //DisplayVideoAd();
        }

    }
    public void RequestAds()
    {
        //this.RequestBanner();
        //this.RequestFullScreenAd();
        this.RequestVideoAd();
    }
    //public void HideAds()
    //{
    //    //bannerAd.Hide();
    //    //bannerAd.Destroy();
    //}


    //BANNER
    //void RequestBanner()
    //{
    //    //string bannerID = "ca-app-pub-6633468813011566/4548766284";
    //    string bannerID = "ca-app-pub-3940256099942544/6300978111";

    //    bannerAd = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);

    //    //AdRequest adreq = new AdRequest.Builder().Build();

    //    AdRequest adreq = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
    //    bannerAd.LoadAd(adreq);
    //}
    //public void DisplayBanner()
    //{
    //    bannerAd.Show();
    //}


    //FULLSCREEN
    //void RequestFullScreenAd()
    //{
    //    string fullScreenAdId = "ca-app-pub-3940256099942544/1033173712";

    //    fullscreenAd = new InterstitialAd(fullScreenAdId);

    //    AdRequest adreq = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
    //    //AdRequest adRequest = new AdRequest.Builder().Build();

    //    fullscreenAd.LoadAd(adreq);
    //}
    //public void DisplayFullScreenAd()
    //{
    //    fullscreenAd.Show();
    //    fullscreenAd.OnAdClosed += (sender, args) =>
    //    {
    //        videoReward.SetActive(true);
    //        videoReward.transform.GetChild(0).GetComponent<Text>().text = "FULLSCREEN\nREWARD";
    //    };
    //}
}
