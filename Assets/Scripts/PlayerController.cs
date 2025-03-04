﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    private int score = 0;
	public int health = 5;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            Debug.Log("Health: " + health);

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
            health = 5;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
