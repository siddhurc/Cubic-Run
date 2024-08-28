using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Unity.Services.Core;

public class GooglePlayGamesManager : MonoBehaviour
{
    public static GooglePlayGamesManager Instance { get; private set; }

    public string Token { get; private set; }
    public string Error { get; private set; }
    public string PlayerName = "";

    public TextMeshProUGUI userInfo;

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayGamesPlatform.Activate();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    private async void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            PlayerName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();

            try
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, async authCode =>
                {
                    Debug.Log("Authorization code: " + authCode);
                    Token = authCode;
                    await SignInWithUnityAuthentication(authCode);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Error during Google Play Games authentication: " + ex);
            }

            userInfo.text = "Welcome " + PlayerName;

            
        }
        else
        {
            // Handle failure scenario, perhaps show a retry button
            Debug.LogWarning("Google Play Games authentication failed: " + status);
        }
    }

    private async Task SignInWithUnityAuthentication(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("Sign-in with Unity Authentication is successful.");

            await UpdatePlayerNameOnUnityWithGogglePlayName();
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError("AuthenticationException: " + ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError("RequestFailedException: " + ex);
        }
    }

    private async Task UpdatePlayerNameOnUnityWithGogglePlayName()
    {
        //if(googlePlayGamesInstance.PlayerName != "")
        {
            try
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(PlayerName);
                Debug.Log("Player name has been updated on unity cloud with google play name");
            }
            catch (Exception ex)
            {
                Debug.Log("Player name updation error : " + ex);
            }
        }

    }
}
