using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject pauseScreen;
    public GameObject titleScreen;
    public bool isGameActive;
    
    private AudioSource bgMusic;
    private int score;
    private float defaultLives = 3;
    private float spawnRate = 1.0f;
    private bool isGamePaused = false;    
    private int lives;

    private void Start()
    {
        bgMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        lives = Mathf.CeilToInt(defaultLives / difficulty);

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.gameObject.SetActive(false);
    }

    void ChangePaused()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            bgMusic.Pause();
            isGamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            bgMusic.Play();
            isGamePaused = false;
        }
    }
}
