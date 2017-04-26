using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Script : MonoBehaviour {

    public Transform player;
    public float radiusDetection;
    public Transform relatedBlockedRail;

    private Animator tiltAnim;
    private Animator doorAnim;

    [HideInInspector]
    public bool isOnDoor;
    [HideInInspector]
    public bool isDoorOpen;
    //private bool firstTry;

    private SonPuzzle1 scriptSons;

    private void Awake()
    {
        
        tiltAnim = GetComponent<Animator>();
        doorAnim = transform.parent.GetComponent<Animator>();
        scriptSons = GetComponentInParent<SonPuzzle1>();
    }

    // Use this for initialization
    void Start () {
        isDoorOpen = false;
        isOnDoor = false;
        //firstTry = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.parent.position, player.position) < radiusDetection)
        {
            if (Input.inputString == "\n" || Input.GetButtonDown("Fire1"))
            {
                if (isOnDoor)
                {
                    //if (!firstTry)
                    //{
                        openingDoor();
                    //}
                    //else
                    //{
                    //    scriptSons.playFail();
                    //    firstTry = false;
                    //}
                } else
                {
                    scriptSons.playFail();
                }
            }
        }		
	}

    private void openingDoor()
    {
        isDoorOpen = true;
        scriptSons.playWin();
        doorAnim.SetBool("isOpen?", true);
        tiltAnim.speed = 0;
        wait(1f);
        scriptSons.playDoorOpen();
        relatedBlockedRail.GetComponent<RailBehavior>().isBlocked = false;
    }

    public void setOnDoor(int value)
    {
        if (value == 1)
            isOnDoor = true;
        else
            isOnDoor = false;
    }

    IEnumerator wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

}
