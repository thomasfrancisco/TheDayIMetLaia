using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiltReceiver : MonoBehaviour {


    private DoorScript doorScript;

    private void Awake()
    {
        doorScript = transform.parent.GetComponent<DoorScript>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setOnDoor(int value)
    {
        if (value == 1)
            doorScript.isOnDoor = true;
        else
            doorScript.isOnDoor = false;
    }
}
