using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("NewGameScene");
    }

    public void LoadGame()
    {

    }

    public void Options()
    {

    }

    public void About()
    {

    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game!!!");
        Application.Quit();
    }
}
