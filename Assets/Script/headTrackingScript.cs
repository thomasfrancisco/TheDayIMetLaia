using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headTrackingScript : MonoBehaviour {

    public float rotationSpeed;

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.Rotate(-Input.gyro.rotationRateUnbiased.x * rotationSpeed, -Input.gyro.rotationRateUnbiased.y * rotationSpeed, 0);
	}
}
