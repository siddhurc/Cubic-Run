using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using System;
using Unity.Services.Core;

public class GooglePlayGamesManager : MonoBehaviour
{
    public static GooglePlayGamesManager Instance { get; private set; }

    public string Token;
    public string Error;

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

    // Start is called before the first frame update
    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }
        else
        {
            Debug.Log("Player is already signed in.");
            userInfo.text = "Welcome back " + PlayGamesPlatform.Instance.GetUserDisplayName();
        }
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            string userName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();

            if (!string.IsNullOrEmpty(Token))
            {
                Debug.Log("Player is already signed in with Unity Authentication.");
                userInfo.text = "Welcome, " + userName;
                return;
            }

            try
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, async authCode =>
                {
                    Debug.Log("Authorization code: " + authCode);
                    Token = authCode;
                    await SignInWithGooglePlayGamesAsync(authCode);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Error during authentication: " + ex.ToString());
            }

            userInfo.text = "Welcome, " + userName;
        }
        else
        {
            Debug.LogError("Authentication failed with status: " + status);
            // Optionally, show a login button to allow the user to retry
        }
    }

    async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("SignIn with Unity Authentication is successful.");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            // Handle the exception (e.g., show an error message to the player)
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            // Handle the exception (e.g., show an error message to the player)
        }
    }
}
