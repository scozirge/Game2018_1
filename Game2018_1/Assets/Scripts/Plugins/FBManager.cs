using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
public class FBManager : MonoBehaviour
{

    public static bool IsSpawn;
    // Use this for initialization
    public static bool IsInit;
    public static bool IsLogin;
    public static bool IsGetIcon;
    public static Texture2D FBICon;
    static FBManager Myself;
    public static FBRequest MyRequest;
    //Uri link = new Uri("https://www.imdb.com/chart/top?sort=ir,desc&mode=simple&page=1");

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
        var perms = new List<string>() { "public_profile", "publish_actions" };//, "email"
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
            switch (MyRequest)
            {
                case FBRequest.Login:
                    break;
                case FBRequest.GetPhoto:
                    GetProfilePhoto();
                    break;
                case FBRequest.TakeScreenShot:
                    TakeScreenShot();
                    break;
                default:
                    break;
            }


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

    public static void TakeScreenShot()
    {
        Myself.StartCoroutine(TakeScreenshot());
    }
    static IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        var width = Screen.width;
        var height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        byte[] screenshot = tex.EncodeToPNG();

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
        FB.API("me/photos", HttpMethod.POST, HandleResult, wwwForm);
    }
    public static void Feed()
    {
        string feedTo = "";
        Uri link = new Uri("www.google.com");
        string feedTitle = "";
        string feedCaption = "";
        string feedDescription = "";
        Uri feedImage = new Uri("www.google.com");
        string feedMediaSource = "";
        FB.FeedShare(
        feedTo,
        link,
        feedTitle,
        feedCaption,
        feedDescription,
        feedImage,
        feedMediaSource,
        HandleResult);
    }
}
