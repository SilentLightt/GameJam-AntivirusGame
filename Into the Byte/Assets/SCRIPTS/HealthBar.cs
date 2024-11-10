using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    public Slider healthSlider;
    public Slider easeHealthSlider;

    public float maxHP = 100f;
    public float HP;
    private float lerpSpeed = 0.05f;
    private Rigidbody2D rbplayer;

   // public GameObject GameOverCanvas;
    public GameObject Player;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HP = maxHP;
        healthSlider.maxValue = maxHP;
        healthSlider.value = HP;
        easeHealthSlider.maxValue = maxHP;
        easeHealthSlider.value = HP;

        // Initialize Rigidbody2D component for the player
        rbplayer = Player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Smoothly interpolate easeHealthSlider value towards HP
        if (easeHealthSlider.value != HP)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, HP, lerpSpeed);
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
        healthSlider.value = HP;
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Activate Game Over Canvas and disable player
        //if (GameOverCanvas != null)
        //{
        //    GameOverCanvas.SetActive(true);
        //}

        if (Player != null)
        {
            Player.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}

