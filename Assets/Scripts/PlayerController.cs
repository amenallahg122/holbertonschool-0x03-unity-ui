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
    public Text winLoseText;
    public Image winLoseBG;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreText();
        SetHealthText();

        if (winLoseText != null)
            winLoseText.gameObject.SetActive(false);
        if (winLoseBG != null)
            winLoseBG.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
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
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();

            if (health <= 0)
            {
                if (winLoseText != null)
                {
                    winLoseText.gameObject.SetActive(true);
                    winLoseText.text = "Game Over!";
                    winLoseText.color = Color.white;
                }

                if (winLoseBG != null)
                {
                    winLoseBG.gameObject.SetActive(true);
                    winLoseBG.color = Color.red;
                }

                StartCoroutine(LoadScene(3f));
            }
        }
        
        if (other.CompareTag("Goal"))
        {
            if (winLoseText != null)
            {
                winLoseText.gameObject.SetActive(true);
                winLoseText.text = "You Win!";
                winLoseText.color = Color.black;
            }

            if (winLoseBG != null)
            {
                winLoseBG.gameObject.SetActive(true);
                winLoseBG.color = Color.green;
            }

            StartCoroutine(LoadScene(3f));
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

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        score = 0;
        health = 5;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
