using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver = false;

    public GameObject gameOverUI;

    public GameObject completeLevelUI;

    public string nextLevel = "Level02";

    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    void Update()
    {
        if (gameIsOver) return;
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void Start()
    {
        gameIsOver = false;
    }
    private void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void LevelWon()
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
