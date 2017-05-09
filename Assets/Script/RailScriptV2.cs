using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailScriptV2 : MonoBehaviour {

    public enum AigPosition { aig1, aig2 };

    public Transform previousRail;
    public Transform nextRail;
    public Transform thirdRail;
    public Transform fourthRail;
    public bool isBlocked;
    public bool isSelected;
    public AigPosition aigPosition;

    private RailScriptV2[] aig1;
    private RailScriptV2[] aig2;
    [HideInInspector]
    public bool oneWay;

    // Use this for initialization
    void Start() {
        isSelected = false;
        if (previousRail != null && nextRail != null)
        {
            aig1 = new RailScriptV2[2];
            aig1[0] = previousRail.GetComponent<RailScriptV2>();
            aig1[1] = nextRail.GetComponent<RailScriptV2>();
            isBlocked = false;
        } else
        {
            isBlocked = true;
        }
        if (thirdRail != null && fourthRail != null)
        {
            aig2 = new RailScriptV2[2];
            aig2[0] = thirdRail.GetComponent<RailScriptV2>();
            aig2[1] = fourthRail.GetComponent<RailScriptV2>();
            oneWay = false;
        } else
        {
            oneWay = true;
        }
        aigPosition = AigPosition.aig1;
    }

    private void OnDrawGizmos()
    {
        if (isSelected)
        {
            Gizmos.color = Color.red;
        } else
        {
            Gizmos.color = Color.blue;
        }
        if (!isBlocked)
            Gizmos.DrawSphere(transform.position, .1f);
        else
            Gizmos.DrawWireSphere(transform.position, .1f);

        if(previousRail != null)
        {
                Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, previousRail.position, 0.5f));
        }
        
        if(nextRail != null)
        {
                Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, nextRail.position, 0.5f));
            
        }
        if(thirdRail != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, thirdRail.position, 0.5f));
        }
        if(fourthRail != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, fourthRail.position, 0.5f));
        }
    }

    public void changeAiguillage()
    {
        if(aigPosition == AigPosition.aig1)
        {
            if (oneWay)
            {
                aigPosition = AigPosition.aig1;
            }
            else
            {
                aigPosition = AigPosition.aig2;
            }
        } else
        {
            aigPosition = AigPosition.aig1;
        }
    }

    public RailScriptV2[] getUndisposedAiguillage()
    {
        if(aigPosition == AigPosition.aig1)
        {
            return aig2;
        } else
        {
            return aig1;
        }
    }

    public RailScriptV2[] getRailAiguillage()
    {
        if(aigPosition == AigPosition.aig1)
        {
            return aig1;
        } else
        {
            return aig2;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
