using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Script : MonoBehaviour {

    public Transform player;
    public float radiusDetection;
    public Transform relatedBlockedRail;
    public Transform nextRail;

    private Animator tiltAnim;
    private Animator doorAnim;

    [HideInInspector]
    public bool isOnDoor;
    [HideInInspector]
    public bool isDoorOpen;
    //private bool firstTry;

    private SonPuzzle1 scriptSons;
    private RailScriptV2 relatedScript;
    private RailScriptV2 nextRailScript;

    private void Awake()
    {
        
        tiltAnim = GetComponent<Animator>();
        doorAnim = transform.parent.GetComponent<Animator>();
        scriptSons = GetComponentInParent<SonPuzzle1>();
        relatedScript = relatedBlockedRail.GetComponent<RailScriptV2>();
        nextRailScript = nextRail.GetComponent<RailScriptV2>();
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
                    openingDoor();
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
        relatedScript.isBlocked = false;
        relatedScript.northRail = nextRail;
        nextRailScript.southRail = relatedBlockedRail;
        nextRailScript.connectRails();
        relatedScript.connectRails();
        relatedScript.oneWay = true;

    }
    

    IEnumerator wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

}
