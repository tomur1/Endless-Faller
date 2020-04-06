using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager managerInstance;
    public LevelFader levelFader;
    //used for showing score in main menu
    public int highscore;

    public enum GameState
    {
        HomeMenu,
        PauseMenu,
        Gameplay,
        GameOver
    }

    [SerializeField] private string gameScene;
    [SerializeField] private string menuScene;

    public GameState currentState;

    private void Awake()
    {
        if (managerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            managerInstance = this;
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        DontDestroyOnLoad(gameObject);
        highscore = SaverLoader.Load();
        currentState = GameState.HomeMenu;
        CheckIfInitaialValuesExists();
    }

    //if the initial values don't exists it will crash the application so in that case we want to create some default ones.
    private void CheckIfInitaialValuesExists()
    {
        if (!SaverLoader.InitialValuesExist())
        {
            SaverLoader.SaveStartingValues(new Vector3(0, 5, 0), 2);
        }
    }

    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
        currentState = GameState.Gameplay;
    }

    public void GoToHomeMenu()
    {
        StartCoroutine(LoadScene(menuScene));
        currentState = GameState.HomeMenu;
        SaveGame();
    }

    public void PauseGame()
    {
        //just changes the status. actual game stop is performed by level manager.
        currentState = GameState.PauseMenu;
    }

    public void ResumeGame()
    {
        currentState = GameState.Gameplay;
    }

    public void SaveGame()
    {
        SaverLoader.Save(highscore);
    }

    private void OnSceneLoad(Scene loadedScene, LoadSceneMode mode)
    {
        
        //if the home has been loaded set the highscore text
        if (loadedScene.name == "Home")
        {
            GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(delegate { Play(); });
            GameObject.Find("Highscore").GetComponent<TMPro.TextMeshProUGUI>().SetText("Highscore: " + highscore);
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        levelFader.FadeIn();
        yield return new WaitUntil(() => levelFader.fadedIn);
        SceneManager.LoadScene(sceneName);
        levelFader.FadeOut();
    }

}