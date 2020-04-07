using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver = false;
    public GameObject gameOverUI;

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
}
