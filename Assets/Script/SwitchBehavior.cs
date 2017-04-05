using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour {

    public Transform relatedDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Stay");
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Detected");
                relatedDoor.gameObject.GetComponent<DoorBehavior>().isClosed = false;
                relatedDoor.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }


}
