using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroEnabledScript : MonoBehaviour {

    private Gyroscope gyro;

	// Use this for initialization
	void Start () {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
