using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public GameObject cellEntryPrefab;
    public Transform leaderboardTable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populateTable(List<LeaderboardScore> leaderboardScores)
    {
        for (int i = 3; i < leaderboardTable.transform.childCount; i++)
            Destroy(leaderboardTable.transform.GetChild(i).gameObject);

        Debug.Log(leaderboardScores.Count);

        foreach(LeaderboardScore score in leaderboardScores)
        {
            GameObject entry = Instantiate(cellEntryPrefab, leaderboardTable);
            TextMeshProUGUI text = entry.GetComponent<TextMeshProUGUI>();
            text.text = score.rank.ToString();

            GameObject entry_2 = Instantiate(cellEntryPrefab, leaderboardTable);
            TextMeshProUGUI text_2 = entry_2.GetComponent<TextMeshProUGUI>();
            text_2.text = score.playerName;

            GameObject entry_3 = Instantiate(cellEntryPrefab, leaderboardTable);
            TextMeshProUGUI text_3 = entry_3.GetComponent<TextMeshProUGUI>();
            text_3.text = score.score.ToString();
        }
    }
}
