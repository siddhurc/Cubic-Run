using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;

public class GooglePlayGamesManager : MonoBehaviour
{
    public TextMeshProUGUI userInfo;
    // Start is called before the first frame update
    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            string userName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            
            PlayGamesPlatform.Instance.RequestServerSideAccess(true, async authCode =>
            {
                Debug.Log("Authorization code: " + authCode);
                await SignInWithGooglePlayGamesAsync(authCode);
            });

            userInfo.text = "Welcome " + userName;
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            userInfo.text="Sign In failed."+status;
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
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            userInfo.text = ex.ToString();
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            userInfo.text = ex.ToString();
        }
    }


}
