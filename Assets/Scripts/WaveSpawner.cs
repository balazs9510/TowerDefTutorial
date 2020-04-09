using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnLoc;

    public float timeBetweenWaves = 3f;

    private float countdown = 2f;

    public Text CountdownText;

    private int waveIndex = 0;

    public GameManager gameManager;

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (waveIndex == waves.Length)
        {
            gameManager.LevelWon();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        CountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnLoc.position, spawnLoc.rotation);
    }
}
