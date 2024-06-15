using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // to define the singleton instance of the game manager
    public static GameManager Instance { get; private set; }

    public GameObject gameOverText;
    public GameObject gamePausedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        // Ensure that there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameManager between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
