using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Removed below code lines since the GameManager doesnt need to singleton class
    // to define the singleton instance of the game manager
    //public static GameManager Instance { get; private set; }

    public TextMeshProUGUI scoreBoard;
    public int score;
    public GameObject pauseButton;
    public GameObject playerPrefab;
    private GameObject player;
    public GameObject pauseMenuPopup;
    public GameObject gameOverPopup;
    public TextMeshProUGUI scoreBoard_PausePopupMenu;
    public TextMeshProUGUI scoreBoard_GameoverPopupMenu;
    public GameObject currentPlayerHighScore_UI;
    private int currentPlayerHighScore = 0;

    public Slider health_slider;
    private int playerHealth = 100;

    //UNITY CLOUD GAMING SERVICES
    CloudManager cloudManager;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerHealth = 100;

        //work on future builds since loading player from script disables player movement
        //Vector3 player_spawnPoint = new Vector3(0.01f, 0.54f, 27.05f);
        //player = Instantiate(playerPrefab, player_spawnPoint, Quaternion.identity);

        cloudManager = CloudManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthUI();
    }

    public void PauseButtonOnClick()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pause the game
            scoreBoard_PausePopupMenu.text = "SCORE : "+score;
            pauseMenuPopup.SetActive(true);
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            pauseMenuPopup.SetActive(false);
        }
    }

    public async void GameOver()
    {
        // Pause the gameplay since the player is hit by obstacle
        Time.timeScale = 0f;

        cloudManager.AddScore(score);
        scoreBoard_GameoverPopupMenu.text = "SCORE : " + score;

        currentPlayerHighScore = await cloudManager.GetPlayerScore();
        if(currentPlayerHighScore == 0)
        {
            currentPlayerHighScore = score;
        }

        currentPlayerHighScore_UI.GetComponent<TextMeshProUGUI>().text = currentPlayerHighScore.ToString();

        //display game over popup
        gameOverPopup.SetActive(true);
    }

    public void GoToMainMenu()
    {
        StartCoroutine(LoadMainScene(1f));
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

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Game over as plaer health reached below zero");
            GameOver();
        }
        else
        {
            updateHealthUI();
        }
    }

    public void PlayerHeal(int heal_value)
    {
        playerHealth += heal_value;
        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
        updateHealthUI();
    }

    void updateHealthUI()
    {
        health_slider.value = playerHealth;
    }

}
