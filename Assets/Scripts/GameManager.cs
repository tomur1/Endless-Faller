using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        HomeMenu,
        PauseMenu,
        Gameplay,
        EndGameMenu
    }

    [SerializeField] private string gameScene;
    [SerializeField] private string menuScene;

    public GameState currnetState;


    private void Awake()
    {
        Object.DontDestroyOnLoad(this);
        currnetState = GameState.HomeMenu;
    }

    private void Update()
    {

    }

    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
        currnetState = GameState.Gameplay;
    }

    public void GoToHomeMenu()
    {
        StartCoroutine(LoadScene(menuScene));
        currnetState = GameState.HomeMenu;
    }

    public void PauseGame()
    {
        //just changes the status. actual game stop is performed by level manager.
        currnetState = GameState.PauseMenu;
    }

    public void ResumeGame()
    {
        currnetState = GameState.Gameplay;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(sceneName);
    }
}