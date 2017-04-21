using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBehavior : MonoBehaviour {

    public Transform previousRail;
    public Transform nextRail;
    public Transform thirdRail;
    public Transform fourthRail;
    public bool isBlocked;

    public bool isSelected;

    private void OnDrawGizmos()
    {
        
        if (isSelected)
        {
            Gizmos.color = Color.red;
        } else
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawSphere(transform.position, .1f);
    }

    // Use this for initialization
    void Start () {
        isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
