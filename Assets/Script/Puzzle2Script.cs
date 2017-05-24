using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Script : MonoBehaviour {

    public Transform switchButton;
    public float detectionRange;
    public float hitAngle;
    public int[] sequence;
    public bool unlocked;
    public AudioClip PUZ2_Panel_Hide;
    public AudioClip PUZ2_Panel_Show;

    private Transform player;
    private RailMovementV2 movementScript;
    private SphereSound[] childSphere;
    private SphereSound lastPlayed;
    private bool atLeastOneHit;
    private SwitchBehavior switchBehavior;
    private AudioSource source;
    private bool panelHidden;
    private int nbMissed;
    


    private void Awake()
    {
        player = transform.Find("/Player");
        childSphere = new SphereSound[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            childSphere[i] = transform.GetChild(i).GetComponent<SphereSound>();
        }
        atLeastOneHit = false;
        movementScript = player.GetComponent<RailMovementV2>();
        switchBehavior = switchButton.GetComponent<SwitchBehavior>();
        source = GetComponent<AudioSource>();
        panelHidden = true;
        nbMissed = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(player.position, transform.position) < detectionRange)
        {
            if (panelHidden)
            {
                source.clip = PUZ2_Panel_Show;
                source.Play();
                panelHidden = false;
            }
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
                        movementScript.doAction = false;
                        childSphere[i].increaseSoundValue();
                        childSphere[i].playSound();
                    }
                }
            } 
        } else if (!panelHidden)
        {
            source.clip = PUZ2_Panel_Hide;
            source.Play();
            panelHidden = true;
        }

        if (!atLeastOneHit) lastPlayed = null;
	}

    public IEnumerator playSequence(bool check = true)
    {
        for(int i = 0; i < childSphere.Length; i++)
        {
            childSphere[i].playSound();
            yield return new WaitForSeconds(0.5f);
        }
        if (check)
        {
            if (checkWin())
            {
                switchBehavior.playWin();
                yield return new WaitForSeconds(switchBehavior.PUZ2_Win.length);
                unlocked = true;
            } else
            {
                switchBehavior.playFail();
                nbMissed++;
            }
        }
    }

    public int getNbMissed()
    {
        return nbMissed;
    }

    public bool checkWin()
    {
        for(int i = 0; i < childSphere.Length; i++)
        {
            if (childSphere[i].currentSound != sequence[i])
                return false;
        }
        return true;
    }
    
}
