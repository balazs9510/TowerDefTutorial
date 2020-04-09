using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100f;
    private float health;

    public int worth = 20;

    [Header("Optional")]
    public GameObject deathEffect;
    public Animator playerAnimator;

    [Header("Unity Stuff")]
    public Image heathBar;
    public Canvas healthBarCanvas;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        heathBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    [HideInInspector]
    public bool isDead = false;

    void Die()
    {
        isDead = true;

        PlayerStats.Money += worth;
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            Destroy(gameObject);
        }
        else
        {
            playerAnimator.SetBool("die", true);
            healthBarCanvas.enabled = false;

            Destroy(gameObject, 1f);
        }

        WaveSpawner.EnemiesAlive--;

        
    }

    public void Slow(float percentage)
    {
        speed = startSpeed * (1f - percentage);
    }
}
