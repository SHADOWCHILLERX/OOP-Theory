using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] hoopPrefabs;
    private float spawnRangeX = 13;
    private float minSpawnRangeY = 0.5f;
    private float maxSpawnRangeY = 2.58f;
    private float minSpawnRangeZ = 7.5f;
    private float maxSpawnRangeZ = 15;
    private float startDelay = 3;
    private float spawnInterval = 4f;
    public TextMeshProUGUI scoreText;
    private int score;
    private int highScore;
    private float spawnRate = 1;
    public TextMeshProUGUI dot;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;
    public bool isGameActive = false;
    public Button restartButton;
    public GameObject titleScreen;

    private TimerController timerController;

    // Start is called before the first frame update
    void Start()
    {
        //This is Abstraction working with SpawnRandomHoop()
        InvokeRepeating("SpawnRandomHoop", startDelay, spawnInterval);

        // Load the high score from player prefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRandomHoop()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(minSpawnRangeY, maxSpawnRangeY), Random.Range(minSpawnRangeZ, maxSpawnRangeZ));
            int hoopIndex = Random.Range(0, hoopPrefabs.Length);
            Instantiate(hoopPrefabs[hoopIndex], spawnPos, hoopPrefabs[hoopIndex].transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnRandomHoop());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        dot.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        highScoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        highScoreText.gameObject.SetActive(true);
        dot.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;

        // Check if the current score is higher than the high score
        if (score > highScore)
        {
            highScore = score;
            // Save the new high score to player prefs
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        // Display the high score
        highScoreText.text = "High Score: " + highScore;
    }

    

}