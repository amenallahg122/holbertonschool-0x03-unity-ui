using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float rotationSpeed = 45f;
	
	// Update is called once per frame
	void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAmount, 0, 0);
    }
}
