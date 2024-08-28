using System;
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

    const string LeaderboardId = "CubicRun_Leaderboard";
    Leaderboard leaderboard;
    PopupActions popup;

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

    private void Start()
    {
        leaderboard = FindObjectOfType<Leaderboard>();
        popup = FindObjectOfType<PopupActions>();
    }

    private async Task InitializeCloudServices()
    {
        await UnityServices.InitializeAsync();
    }

    public async void AddScore(int score)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async void GetTopScores()
    {
        popup_gameobject.SetActive(true);
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        string jsonResponse = JsonConvert.SerializeObject(scoresResponse);

        LeaderboardResponse response = JsonConvert.DeserializeObject<LeaderboardResponse>(jsonResponse);

        if (response != null)
        {
            leaderboard.populateTable(response.results);
        }
    }

    public async Task<int> GetPlayerScore()
    {
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        return (int)scoreResponse.Score;
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
