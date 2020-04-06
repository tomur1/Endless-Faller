using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public Text CurrentScoreText;
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public GameObject PauseMenuPanel;
    public GameObject EndMenuPanel;
    public GameObject Player;
    public GameManager gameManager;
    public Vector3 PlayerStartingPoint;

    void Awake()
    {
        gameManager = GameManager.managerInstance;
        HighScore = gameManager.highscore;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        LoadStartingValuesFromJSON();
        SetUpPlayer();
    }

    void Update()
    {
        bool escPressed = Input.GetKey(KeyCode.Escape);
        
        if (escPressed && gameManager.currentState == GameManager.GameState.Gameplay)
        {
            //Stop time and show pause menu
            PauseGame();
        }
    }

    private void LoadStartingValuesFromJSON()
    {
        InitialPreferences initialPreferences = SaverLoader.LoadStartingValues();
        PlayerStartingPoint = initialPreferences.initialPlayerPosition;
    }

    public void GameOver()
    {
        
        TMPro.TextMeshProUGUI ScoreText = EndMenuPanel.transform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI HighscoreText = EndMenuPanel.transform.Find("HighscoreText").GetComponent<TMPro.TextMeshProUGUI>();
        if (Score > HighScore)
        {
            HighScore = Score;
        }
        ScoreText.SetText("Your Score: " + Score);
        HighscoreText.SetText("Highscore: " + HighScore);
        EndMenuPanel.SetActive(true);
        Time.timeScale = 0;
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
        //has to be here to if the player decides to go home from pause menu
        if (Score > HighScore)
        {
            HighScore = Score;
        }
        Time.timeScale = 1.0f;
        EndMenuPanel.SetActive(false);
        PauseMenuPanel.SetActive(false);
        gameManager.highscore = HighScore;
        
        gameManager.GoToHomeMenu();
    }

    public void IncrementScore()
    {
        Score++;
        CurrentScoreText.text = "Score: " + Score;
    }

    private void SetUpPlayer()
    {
        Player.transform.position = PlayerStartingPoint;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void Reset()
    {
        Score = 0;
        CurrentScoreText.text = "Score: " + Score;
        if (EndMenuPanel != null)
        {
            EndMenuPanel.SetActive(false);
        }
        
        //Well, reloading the scene would be the easiest. But hey! Bonus points!
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            Destroy(platform);
        }
        SetUpPlayer();
        GetComponent<PlatformGenerator>().ResetGeneration();
        Time.timeScale = 1.0f;
    }
}
