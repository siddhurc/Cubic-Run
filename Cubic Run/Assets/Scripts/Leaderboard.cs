using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public GameObject tableCell_Prefab;
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
            GameObject entry = Instantiate(tableCell_Prefab, leaderboardTable);
            TextMeshProUGUI[] list_of_components = entry.GetComponentsInChildren<TextMeshProUGUI>();
            list_of_components[0].text = (score.rank + 1).ToString(); //adding 1 to rank since rank starts from 0
            list_of_components[1].text = score.playerName.ToString();
            list_of_components[2].text = score.score.ToString();
        }
    }
}
