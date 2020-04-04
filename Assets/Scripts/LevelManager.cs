using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public Text CurrentScoreText;
    public int Score { get; private set; }
    public GameObject PauseMenuPanel;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        bool escPressed = Input.GetKey(KeyCode.Escape);
        
        if (escPressed && gameManager.currnetState == GameManager.GameState.Gameplay)
        {
            //Stop time and show pause menu
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenuPanel.SetActive(true);
        gameManager.PauseGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        PauseMenuPanel.SetActive(false);
        gameManager.ResumeGame();
    }

    public void GoToHomeMenu()
    {
        //need to resume the game to actually go back
        gameManager.GoToHomeMenu();
        Time.timeScale = 1.0f;
    }

    public void IncrementScore()
    {
        Score++;
        CurrentScoreText.text = "Score: " + Score;
        Debug.Log(Score);
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }
}
