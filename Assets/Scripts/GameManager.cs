using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject startPage;
    public GameObject gameOverPage;
    public Text gameOverText; 

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static bool IsInputEnabled = false;

    enum PageState 
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    void OnEnable() {
        PlayerController.OnPlayerWon += OnPlayerWon;
        PlayerController.OnPlayerDied += OnPlayerDied;
    }

    void OnDisable() {
        PlayerController.OnPlayerWon -= OnPlayerWon;
        PlayerController.OnPlayerDied -= OnPlayerDied;
    }

    void OnPlayerDied() 
    {
        gameOverText.text = "You lose!";
        SetPageState(PageState.GameOver);
    }

    void OnPlayerWon()
    {
        gameOverText.text = "You win!";
        SetPageState(PageState.GameOver);
    }

    void Awake()
    {
        Instance = this;
        SetPageState(PageState.Start);
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        // Activated wh en "Play" button pressed
        SetPageState(PageState.None);
        IsInputEnabled = true;
        OnGameStarted();
    }
}
