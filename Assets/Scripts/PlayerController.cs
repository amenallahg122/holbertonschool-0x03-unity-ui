using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    private int score = 0;
    public int health = 5;
    private Rigidbody rb;
    public Text scoreText;
    public Text healthText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreText();
        SetHealthText();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        rb.MovePosition(transform.position + movement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            UpdateScoreText();
            //Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();

            if (health <= 0)
            {
                Debug.Log("Game Over!");
            }
        }
        
        if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Game Over!");

            score = 0;
            UpdateScoreText();
            health = 5;
            SetHealthText();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void SetHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }
}
