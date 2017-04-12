using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Script : MonoBehaviour {

    public Transform player;
    public float radiusDetection;
    public Transform relatedBlockedRail;

    private Animator tiltAnim;
    private Animator doorAnim;
    private bool isOnDoor;


	// Use this for initialization
	void Start () {
        tiltAnim = GetComponent<Animator>();
        doorAnim = transform.parent.GetComponent<Animator>();
        isOnDoor = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.parent.position, player.position) < radiusDetection)
        {
            if (Input.inputString == "\n" && isOnDoor)
            {
                doorAnim.SetBool("isOpen?", true);
                tiltAnim.speed = 0;
                relatedBlockedRail.GetComponent<RailBehavior>().isBlocked = false;
            } 
        }
        
        
		
	}

    public void setOnDoor(int value)
    {
        if (value == 1)
            isOnDoor = true;
        else
            isOnDoor = false;
    }

}
