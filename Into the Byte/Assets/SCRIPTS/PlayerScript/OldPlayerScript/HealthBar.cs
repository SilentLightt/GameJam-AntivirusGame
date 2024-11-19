using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene


public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    public Slider healthSlider;
    public Slider easeHealthSlider;

    public float maxHP = 100f;
    public float HP;
    private float lerpSpeed = 0.05f;
    private Rigidbody2D rbplayer;

   public GameObject GameOverCanvas;
    public GameObject Player;

    public string sceneName; // Name of the scene to reload


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
        //if (Mathf.Abs(easeHealthSlider.value - HP) > 0.1f) // Change 0.1f to a suitable tolerance
        //{
        //    easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, HP, lerpSpeed);
        //}
        //else
        //{
        //    easeHealthSlider.value = HP; // Directly set the value when it's close enough
        //}
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
    public void AddHealth(float healthAmount)
    {
        HP += healthAmount;
        if (HP > maxHP)
        {
            HP = maxHP;
        }
        healthSlider.value = HP;
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Activate Game Over Canvas and disable player
        if (GameOverCanvas != null)
        {
           GameOverCanvas.SetActive(true);
        }

        

        if (Player != null)
        {
            Player.SetActive(false);
            Time.timeScale = 0f;
            SceneLoader();
        }
    }

    public void SceneLoader()
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f; // Resume the game
    }


}

