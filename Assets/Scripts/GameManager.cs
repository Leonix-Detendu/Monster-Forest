using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    public int health;

    [HideInInspector] public int score;
    [HideInInspector] public bool targetWasClicked;

    private int scoreToIncreaseDifficulty;
    

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    [HideInInspector] public bool gameOver;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    public Button returnToMainMenuButton;

    [HideInInspector] public AudioSource audioSource;
    [SerializeField] private AudioClip missingSound, backgroundMusic, gameOverMusic, gameOverSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        score = 0;
        scoreToIncreaseDifficulty = SpawnManager.instance.scoreInterval;

        targetWasClicked = false;
        gameOver = false;
        gameOverText.enabled = false;

        ActualizeHealth();
        ActualizeScore();

        restartButton.gameObject.SetActive(false);
        returnToMainMenuButton.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !targetWasClicked && !gameOver)
        {
            health--;
            ActualizeHealth();
            audioSource.PlayOneShot(missingSound);
        }
        targetWasClicked = false;

        if (health <= 0 && !gameOver)
        {

            health = 0;

            Target[] targetScripts;
            targetScripts = Object.FindObjectsByType<Target>(FindObjectsSortMode.None);
            foreach (Target script in targetScripts)
            {
                GameObject Monster = script.gameObject;
                Destroy(Monster);
            }
            gameOver = true;
            DisplayGameOver();
            if (score >= DataPersistence.instanceData.highestScore)
            {
                DataPersistence.instanceData.highestScore = score;
                DataPersistence.instanceData.playerNameWithHs = DataPersistence.instanceData.currentPlayerName;
            }
            audioSource.Stop();

            StartCoroutine(GameOverSoundsRoutine());
        }

        if (score >= scoreToIncreaseDifficulty)
        {
            IncreaseDifficulty();
        }
    }

    public void ActualizeScore()
    {
        scoreText.text = "Score : " + score;
    }

    public void ActualizeHealth()
    {
        healthText.text = "Health : " + health;
    }

    private void DisplayGameOver()
    {
        gameOverText.enabled = true;
        restartButton.gameObject.SetActive(true);
        returnToMainMenuButton.gameObject.SetActive(true);
    }

    private void IncreaseDifficulty()
    {
        scoreToIncreaseDifficulty += SpawnManager.instance.scoreInterval;


        SpawnManager.instance.currentBonusSpeed += SpawnManager.instance.speedIncrease;
        SpawnManager.instance.currentDespawnReduction += SpawnManager.instance.spawnDelayDecrease;

        if (SpawnManager.instance.spawnDelayMin > SpawnManager.instance.minOfSpawnDelay)
        {
            SpawnManager.instance.spawnDelayMin -= SpawnManager.instance.spawnDelayDecrease;

            if ( SpawnManager.instance.spawnDelayMin <= SpawnManager.instance.minOfSpawnDelay )
            {
                SpawnManager.instance.spawnDelayMin = SpawnManager.instance.minOfSpawnDelay;
            }
            
        }

        if (SpawnManager.instance.spawnDelayMax > SpawnManager.instance.spawnDelayMin)
        {
            SpawnManager.instance.spawnDelayMax -= SpawnManager.instance.spawnDelayDecrease;

            if (SpawnManager.instance.spawnDelayMax <= SpawnManager.instance.spawnDelayMin)
            {
                SpawnManager.instance.spawnDelayMax = SpawnManager.instance.spawnDelayMin;
            }
          
        }
    }

    IEnumerator GameOverSoundsRoutine()
    {
        audioSource.PlayOneShot(gameOverSound);

        yield return new WaitForSeconds(gameOverSound.length);

        audioSource.clip = gameOverMusic;
        audioSource.Play();
    }
}
