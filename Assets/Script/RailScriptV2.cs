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
    public bool oneWay; //Ne bloque pas le joueur si il passe dessus

    private AudioSource source;
    private RailScriptV2[] aig1;
    private RailScriptV2[] aig2;

    //Pour jouer les sons sur un aiguillage
    private RailScriptV2[] allRails;
    private int itRails;


    // Use this for initialization
    void Start() {
        isSelected = false;
        ArrayList tmp = new ArrayList();
        ArrayList tmpAllRails = new ArrayList();
        if (previousRail != null)
        {
            tmp.Add(previousRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(previousRail.GetComponent<RailScriptV2>());
        }
        if (nextRail != null)
        {
            tmp.Add(nextRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(nextRail.GetComponent<RailScriptV2>());
        }

        aig1 = (RailScriptV2[])tmp.ToArray(typeof(RailScriptV2));


        tmp.Clear();

        if (thirdRail != null)
        {
            tmp.Add(thirdRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(thirdRail.GetComponent<RailScriptV2>());
        }
        if (fourthRail != null)
        {
            tmp.Add(fourthRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(fourthRail.GetComponent<RailScriptV2>());
        }

        aig2 = (RailScriptV2[])tmp.ToArray(typeof(RailScriptV2));
        allRails = (RailScriptV2[])tmpAllRails.ToArray(typeof(RailScriptV2));

        aigPosition = AigPosition.aig1;
        itRails = 0;
        source = GetComponent<AudioSource>();



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

    public void playNextRailSound()
    {
        allRails[itRails].playMySound();
        itRails = (itRails + 1) % allRails.Length;
    }

    public void playMySound()
    {
        source.Play();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
