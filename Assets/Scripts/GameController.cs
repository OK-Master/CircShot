using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GUIText scoreText;
    public GUIText livesText;
    public GUIText gameOverText;
    public GUIText restartText;
    public GameObject enemy;
    public GameObject player;
    public GameObject star;
    public int enemiesPerWave;
    public float minTimeBetweenWaves;
    public float maxTimeBetweenWaves;
    public float waitTimeReduce;
    public float startWait;
    public float spawnWaitTime;
    public float minEnemySpeed;
    public float maxEnemySpeed;
    public int lives;
    public float timeBetweenStars;

    private int score;
    private float waveWaitTime;
    private bool spawnEnemiesIsRunning;
    private bool gameOver;

    // Use this for initialization
    void Start()
    {
        waveWaitTime = startWait;
        score = 0;
        updateScore();
        updateLives();
        gameOverText.text = string.Empty;
        restartText.text = string.Empty;
        StartCoroutine(SpawnStars());
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnEnemiesIsRunning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            spawnEnemiesIsRunning = true;
            StartCoroutine(SpawnEnemies());
        }
        if(gameOver && Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);

    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(waveWaitTime);
        float angle = Random.Range(0, 360);
        float speed = Random.Range(minEnemySpeed, maxEnemySpeed) * Random.Range(0, 2) == 0 ? -1 : 1;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject enemyObject = Instantiate(enemy, Vector3.zero, Quaternion.Euler(0, 0, angle)) as GameObject;
            enemyObject.GetComponentInChildren<Enemy>().speed = speed;
            yield return new WaitForSeconds(spawnWaitTime);
        }
        waveWaitTime = Mathf.Max(minTimeBetweenWaves, waveWaitTime - waitTimeReduce);
        spawnEnemiesIsRunning = false;
    }

    IEnumerator SpawnStars()
    {
        while (true)
        {
            Instantiate(star, Vector3.zero, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            yield return new WaitForSeconds(timeBetweenStars);
        }
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void updateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void AddScore(int value)
    {
        score += value;
        updateScore();
    }
    public void ChangeLives(int value)
    {
        lives += value;
        updateLives();
        if(lives == 0)
        {
            Destroy(player);
            gameOver = true;
            gameOverText.text = "Game Over";
            restartText.text = "Press 'R' to restart";
        }
    }
}
