using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Removed below code lines since the GameManager doesnt need to singleton class
    // to define the singleton instance of the game manager
    //public static GameManager Instance { get; private set; }

    public GameObject gameOverText;
    public GameObject gamePausedText;
    public TextMeshProUGUI scoreBoard;
    public int score;
    public GameObject pauseButton;
    public GameObject playButton;
    public GameObject playerPrefab;
    private GameObject player;

    //UNITY CLOUD GAMING SERVICES
    Leaderboards leaderboards;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        //work on future builds since loading player from script disables player movement
        //Vector3 player_spawnPoint = new Vector3(0.01f, 0.54f, 27.05f);
        //player = Instantiate(playerPrefab, player_spawnPoint, Quaternion.identity);

        leaderboards = Leaderboards.FindObjectOfType<Leaderboards>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pause the game
            togglePlayPause();
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            togglePlayPause();
        }
    }

    public void GameOver()
    {
        // Pause the gameplay since the player is hit by obstacle
        Time.timeScale = 0f;


        //If the player is hit by a obstacle then the game is over 
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }

        leaderboards.AddScore(score);

        StartCoroutine(LoadMainScene(4f));

    }

    private IEnumerator LoadMainScene(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        // Resume time to avoid freezing the new scene loading
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore(int value)
    {
        score += value;

        //Updating the scoreboard to update the current score
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        scoreBoard.text = "Score : " + score.ToString();
    }

    public void togglePlayPause()
    {
        if(playButton.activeInHierarchy == true)
        {
            playButton.SetActive(false);
            pauseButton.SetActive(true);
            gamePausedText.SetActive(false);
        }
        else
        {
            playButton.SetActive(true);
            pauseButton.SetActive(false);
            gamePausedText.SetActive(true);
        }
    }
}
