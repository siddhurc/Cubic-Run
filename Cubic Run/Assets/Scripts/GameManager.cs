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
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Removed since the GameManager doesnt need to singleton class
    //private void Awake()
    //{
    //    // Ensure that there's only one instance of GameManager
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject); // Keep this GameManager between scene loads
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pause the game
            gamePausedText.SetActive(true);
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            gamePausedText.SetActive(false);
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
}
