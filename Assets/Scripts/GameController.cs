using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour, IGlobalSpeedMultiplierContainer
{
    private const string livesFormat = "Lives: {0}";
    private const string scoreFormat = "Score: {0}";
    private const string finalScoreFormat = "Final Score: {0}";

    [SerializeField] private Text lives;
    [SerializeField] private Text score;
    [SerializeField] private Text finalScore;
    [SerializeField] private GameObject sphere;
    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private ObstaclesGenerator obstaclesGenerator;
    [SerializeField] private ScoreThreshold[] speedThresholds; 

    private GameObject player;
    private PlayerStats playerStats;

    private float currentSpeedMultiplier = 1f;

    public float Multiplier { get => currentSpeedMultiplier; }

    public Action OnGameEnd;

    private void Awake()
    {
        obstaclesGenerator.SetSpeedMultiplier(this);
        backgroundScroller.SetAnimationRateMultiplier(this);
    }

    public void StartGame()
    {
        player = Instantiate(sphere);
        playerStats = player.GetComponent<PlayerStats>();
        
        UpdateGameState();
        playerStats.PlayerStatsChanged += UpdateGameState;

        obstaclesGenerator.StartGeneration();
    }

    public void EndGame()
    {
        
        finalScore.text = string.Format(finalScoreFormat, playerStats.score);
        playerStats.PlayerStatsChanged -= UpdateGameState;
        Destroy(player);
        
        obstaclesGenerator.StopGeneration();
        OnGameEnd?.Invoke();

        StartCoroutine(ChangeSpeed(currentSpeedMultiplier, 1f, 2f));
    }

    private void UpdateGameState()
    {
        UpdateUI();

        UpdateGameSpeed();

        if (playerStats.lives == 0)
        {
            EndGame();
        }
    }

    private void UpdateGameSpeed()
    {
        var threshold = Array.Find(speedThresholds, x => x.Score == playerStats.score);
        if (threshold != null)
        {
            StartCoroutine(ChangeSpeed(currentSpeedMultiplier, threshold.Multiplier, 2f));
        }
    }

    private void UpdateUI()
    {
        lives.text = string.Format(livesFormat, playerStats.lives);
        score.text = string.Format(scoreFormat, playerStats.score);
    }

    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            currentSpeedMultiplier = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentSpeedMultiplier = v_end;
    }
}
