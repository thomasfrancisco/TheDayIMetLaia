using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour {



	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
		
	}
}
