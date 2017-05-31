using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RailPosition { North, East, South, West};


public class RailScriptV2 : MonoBehaviour {

    public enum AigPosition { aig1, aig2 };

    public Transform northRail;
    public Transform southRail;
    public Transform eastRail;
    public Transform westRail;
    public bool isBlocked;
    public bool isSelected;
    public AigPosition aigPosition;
    public bool oneWay; //Ne bloque pas le joueur si il passe dessus
    public bool mute;

    public AudioClip northSound;
    public AudioClip eastSound;
    public AudioClip southSound;
    public AudioClip westSound;

    private AudioSource source;
    private RailScriptV2[] aig1;
    private RailScriptV2[] aig2;

    //Pour jouer les sons sur un aiguillage
    [HideInInspector]
    public RailScriptV2[] allRails;
    private RailPosition[] allRailsPosition;
    private int itRails;
    private int emptyHit = 2;

    //Pour trigger les collisions
    [HideInInspector]
    public bool isCollided;
    [HideInInspector]
    public bool doRailSounds;

    // Use this for initialization
    void Awake() {
        doRailSounds = true;
        connectRails();
        isSelected = false;
        itRails = 0;
        source = GetComponent<AudioSource>();
        

    }

    public void connectRails()
    {
        ArrayList tmpAig1 = new ArrayList();
        ArrayList tmpAig2 = new ArrayList();
        ArrayList tmpAllRails = new ArrayList();
        ArrayList tmpPositions = new ArrayList();
        if (northRail != null)
        {
            tmpAig1.Add(northRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(northRail.GetComponent<RailScriptV2>());
            tmpPositions.Add(RailPosition.North);
        }

        if (eastRail != null)
        {
            tmpAig2.Add(eastRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(eastRail.GetComponent<RailScriptV2>());
            tmpPositions.Add(RailPosition.East);
        }
        if (southRail != null)
        {
            tmpAig1.Add(southRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(southRail.GetComponent<RailScriptV2>());
            tmpPositions.Add(RailPosition.South);
        }



        if (westRail != null)
        {
            tmpAig2.Add(westRail.GetComponent<RailScriptV2>());
            tmpAllRails.Add(westRail.GetComponent<RailScriptV2>());
            tmpPositions.Add(RailPosition.West);
        }

        aig1 = (RailScriptV2[])tmpAig1.ToArray(typeof(RailScriptV2));
        aig2 = (RailScriptV2[])tmpAig2.ToArray(typeof(RailScriptV2));
        allRails = (RailScriptV2[])tmpAllRails.ToArray(typeof(RailScriptV2));
        allRailsPosition = (RailPosition[])tmpPositions.ToArray(typeof(RailPosition));
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

        if(northRail != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, northRail.position, 0.5f));
        }
        
        if(southRail != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, southRail.position, 0.5f));
        }
        if(eastRail != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, eastRail.position, 0.5f));
        }
        if(westRail != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, westRail.position, 0.5f));
        }

        if(aigPosition == AigPosition.aig1)
        {
            Gizmos.color = Color.green;
            if (northRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, northRail.position, 0.5f));
            if (southRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, southRail.position, 0.5f));
            Gizmos.color = Color.red;
            if (eastRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, eastRail.position, 0.5f));
            if (westRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, westRail.position, 0.5f));
        } else
        {
            Gizmos.color = Color.red;
            if (northRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, northRail.position, 0.5f));
            if (southRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, southRail.position, 0.5f));
            Gizmos.color = Color.green;
            if (eastRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, eastRail.position, 0.5f));
            if (westRail)
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, westRail.position, 0.5f));
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

    //Fais sonner les autres voies une par une
    public void playNextRailSound()
    {
            if (itRails < allRails.Length)
                allRails[itRails].playMySound(allRailsPosition[itRails]);

            itRails++;
            if (itRails > allRails.Length)
                doRailSounds = false;
        
    }

    public void resetNextRailSound()
    {
        doRailSounds = true;
        itRails = 0;
    }

    public void playMySound(RailPosition railPosition)
    {
        switch (railPosition)
        {
            case RailPosition.North:
                source.clip = northSound;
                break;
            case RailPosition.South:
                source.clip = southSound;
                break;
            case RailPosition.East:
                source.clip = eastSound;
                break;
            case RailPosition.West:
                source.clip = westSound;
                break;
        }
        source.Play();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
