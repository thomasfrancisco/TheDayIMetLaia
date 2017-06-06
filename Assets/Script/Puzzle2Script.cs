using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Script : MonoBehaviour {

    public Transform switchButton;
    public float radiusDetection;
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

    private RailMovementV2 ugoMovement;

    private void Awake()
    {
        player = transform.Find("/Player");
        ugoMovement = player.GetComponent<RailMovementV2>();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusDetection);
    }

    // Update is called once per frame
    void Update () {
        if(Vector3.Distance(player.position, transform.position) < radiusDetection)
        {
            if (panelHidden)
            {
                source.clip = PUZ2_Panel_Show;
                source.Play();
                panelHidden = false;
                StartCoroutine(playSequenceDelayed());
            }
            atLeastOneHit = false;
           
            for(int i = 0; i < transform.childCount; i++)
            {
                if(Vector3.Angle(transform.GetChild(i).position - Camera.main.transform.position, Camera.main.transform.forward) < hitAngle)
                {
                    childSphere[i].playHover();
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
                } else
                {
                    childSphere[i].stopHover();
                }
            } 
        } else if (!panelHidden)
        {
            source.clip = PUZ2_Panel_Hide;
            source.Play();
            panelHidden = true;
            foreach (var child in childSphere)
            {
                child.stopHover();
                
            }
        }

        if (!atLeastOneHit) lastPlayed = null;
	}

    public IEnumerator playSequenceDelayed()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < childSphere.Length; i++)
        {
            childSphere[i].playSound();
            yield return new WaitForSeconds(0.5f);
        } 
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
                yield return new WaitForSeconds(switchBehavior.PUZ2_Win.length - 5f);
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
