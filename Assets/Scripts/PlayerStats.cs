using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int lives;
    public int score;

    public Action PlayerStatsChanged;

    private void Awake()
    {
        lives = 3;
        score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            lives--;
        }
        if (collision.collider.CompareTag("Bonus"))
        {
            score++;
        }
        PlayerStatsChanged?.Invoke();
    }
}
