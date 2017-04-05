using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour {

    public float power;
    public float spawnDistance;
    public Transform bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject go =  Instantiate(bullet.gameObject);
            go.transform.position = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
            go.transform.rotation = Camera.main.transform.rotation;
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * power, ForceMode.Impulse);
        }
	}
}
