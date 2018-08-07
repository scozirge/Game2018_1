using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAD : MonoBehaviour
{

    string GoogleStoreGameID = "2724925";
    string AppleStoreGameID = "";
    // Use this for initialization
    void Awake()
    {
        //---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//
#if UNITY_IOS
        Advertisement.Initialize(GoogleStoreGameID);
#elif UNITY_ANDROID
        Advertisement.Initialize(GoogleStoreGameID, false);
#endif
        //-------------------------------------------------------------------//

    }

    public static void ShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    }
    static void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            BattleManager.Revive();
            // Reward your player here.

        }
        else if (result == ShowResult.Skipped)
        {
            BattleManager.FailToRevive();
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            BattleManager.FailToRevive();
            Debug.LogError("Video failed to show");
        }
    }
}
