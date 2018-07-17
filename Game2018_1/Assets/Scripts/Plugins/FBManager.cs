using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
public class FBManager : MonoBehaviour
{

    public static bool IsSpawn;
    // Use this for initialization
    public static bool IsInit;
    public static bool IsLogin;
    public static bool IsGetIcon;
    public static Texture2D FBICon;
    static FBManager Myself;

    void Awake()
    {
        Myself = this;
        DontDestroyOnLoad(gameObject);
        IsSpawn = true;
    }
    public static void Init()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    static void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            IsInit = true;
            FBManager.Login();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    static void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    public static void Login()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }
    static void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            IsLogin = true;
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Player.SetFBUserID(aToken.UserId);
            GetProfilePhoto();
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
    public static void GetProfilePhoto()
    {
        if (!IsLogin)
            return;
        FB.API("/me/picture", HttpMethod.GET, ProfilePhotoCallback);
    }

    static void ProfilePhotoCallback(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
        {
            FBICon = result.Texture;
            IsGetIcon = true;
            RecordUI.RefreshFBIcon();
        }

        HandleResult(result);
    }

    static void HandleResult(IResult result)
    {
        if (result == null)
        {
            Debug.LogWarning("Null Response\n");
            return;
        }

        // Some platforms return the empty string instead of null.
        if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.LogWarning("Error Response:\n" + result.Error);
        }
        else if (result.Cancelled)
        {
            Debug.LogWarning("Cancelled Response:\n" + result.RawResult);
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            //Debug.LogWarning("Success Response:\n" + result.RawResult);
        }
        else
        {
            Debug.LogWarning("Empty Response\n");
        }
    }
    public static void GetChampionIcon(IEnumerator _cb)
    {
        Myself.StartCoroutine(_cb);
    }
}
