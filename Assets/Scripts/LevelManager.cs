using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public Text CurrentScoreText;
    public int Score { get; private set; }
    
    void Start()
    {
        
    }

    void Update()
    {
        
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
