using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Script : MonoBehaviour {

    public float detectionRange;
    public float hitAngle;

    private Transform player;
    private SphereSound[] childSphere;
    private SphereSound lastPlayed;
    private bool atLeastOneHit;


    private void Awake()
    {
        player = transform.Find("/Player");
        childSphere = new SphereSound[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            childSphere[i] = transform.GetChild(i).GetComponent<SphereSound>();
        }
        atLeastOneHit = false;
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(player.position, transform.position) < detectionRange)
        {
            atLeastOneHit = false;
            for(int i = 0; i < transform.childCount; i++)
            {
                if(Vector3.Angle(transform.GetChild(i).position - Camera.main.transform.position, Camera.main.transform.forward) < hitAngle)
                {
                    atLeastOneHit = true;
                    if (lastPlayed == null || lastPlayed != childSphere[i])
                    {
                        childSphere[i].playSound();
                        lastPlayed = childSphere[i];
                    }
                    if(Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                    {
                        childSphere[i].increaseSoundValue();
                        childSphere[i].playSound();
                    }
                }
            } 
        }
        if (!atLeastOneHit) lastPlayed = null;
	}

    
}
