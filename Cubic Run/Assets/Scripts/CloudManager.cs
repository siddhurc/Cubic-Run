using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public static CloudManager Instance { get; private set; }
    public GameObject popup_gameobject;

    // Create a leaderboard with this ID in the Unity Cloud Dashboard
    const string LeaderboardId = "CubicRun_Leaderboard";
    Leaderboard leaderboard;
    PopupActions popup;

    string VersionId { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist the CloudManager across scenes
            await InitializeCloudServices();
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    public async void Start()
    {
        leaderboard = Leaderboard.FindObjectOfType<Leaderboard>();
        popup = PopupActions.FindObjectOfType<PopupActions>();
    }

    private async Task InitializeCloudServices()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymously();
    }

    async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void AddScore(int score)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GetScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async void GetTopScores()
    {
        popup_gameobject.SetActive(true);
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        string jsonResponse = JsonConvert.SerializeObject(scoresResponse);

        LeaderboardResponse response = JsonConvert.DeserializeObject<LeaderboardResponse>(jsonResponse);

        if(response != null)
        {
            
            leaderboard.populateTable(response.results);
        }
    }

    
    public async void GetPlayerScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

}

[System.Serializable]
public class LeaderboardResponse
{
    public int limit;
    public int offset;
    public int total;
    public List<LeaderboardScore> results;
}

[System.Serializable]
public class LeaderboardScore
{
    public string playerId;
    public string playerName;
    public int rank;
    public float score;
}
