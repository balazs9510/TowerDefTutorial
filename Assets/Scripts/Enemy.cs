using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    
    [HideInInspector]
    public float speed;

    public float healt = 100f;

    public int worth = 20;

    public GameObject deathEffect;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        healt -= amount;

        if (healt <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    public void Slow(float percentage)
    {
        speed = startSpeed * (1f - percentage);
    }
}
