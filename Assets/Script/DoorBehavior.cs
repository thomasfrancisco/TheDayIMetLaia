using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {

    public bool isClosed;
    public float lengthOpened;

    private float yOrig;
    


	// Use this for initialization
	void Start () {
        yOrig = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isClosed)
        {
            transform.position = Vector3.up * (yOrig + lengthOpened);
        }
	}
}
