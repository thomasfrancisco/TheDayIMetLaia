using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Playtest_Rails : MonoBehaviour {
    public AudioClip aig_1;
    public AudioClip aig_2_1;
    public AudioClip aig_2_2;
    public AudioClip aig_2_3;
    public AudioClip aig_3_1;
    public AudioClip aig_3_2;
    public AudioClip aig_3_3;
    public AudioClip aig_5_1;
    public AudioClip aig_5_2;
    public AudioClip aig_Err;
    public Transform aig1;
    public Transform aig2;
    public Transform aig3;
    public Transform aig4;
    public Transform aig5;
    //Aiguillage a regarder lorsqu'on est sur l'aiguillage 3
    public Transform aigToFind1;
    public Transform aigToFind2;
    public Transform aigToFind3;
    public Transform aigToFind4;
    public Transform aigToFind5;
    public Transform aigToFind6;
    public string DEBUGSample;


    private AudioSource source;
    private SoundTemplate sound_1;
    private SoundTemplate sound_2_1;
    private SoundTemplate sound_2_2;
    private SoundTemplate sound_2_3;
    private SoundTemplate sound_3_1;
    private SoundTemplate sound_3_2;
    private SoundTemplate sound_3_3;
    private SoundTemplate sound_5_1;
    private SoundTemplate sound_5_2;
    private SoundTemplate sound_Err;
    private Transform ugo;
    private RailMovementV2 ugoMovement;
    private RailScriptV2 aig1_rail;
    private RailScriptV2 aig2_rail;
    private RailScriptV2 aig3_rail;
    private RailScriptV2 aig4_rail;
    private RailScriptV2 aig5_rail;

    //Necessaire sur l'aiguillage 3
    private bool found1;
    private bool found2;
    private bool found3;
    private bool found4;
    private bool found5;
    private bool found6;

    public bool isGoingBack;


    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
        sound_1 = new SoundTemplate(aig_1,source);
        sound_2_1 = new SoundTemplate(aig_2_1, source);
        sound_2_2 = new SoundTemplate(aig_2_2, source);
        sound_2_3 = new SoundTemplate(aig_2_3, source);
        sound_3_1 = new SoundTemplate(aig_3_1, source);
        sound_3_2 = new SoundTemplate(aig_3_2, source);
        sound_3_3 = new SoundTemplate(aig_3_3, source);
        sound_5_1 = new SoundTemplate(aig_5_1, source);
        sound_5_2 = new SoundTemplate(aig_5_2, source);
        sound_Err = new SoundTemplate(aig_Err, source);
        aig1_rail = aig1.GetComponent<RailScriptV2>();
        aig2_rail = aig2.GetComponent<RailScriptV2>();
        aig3_rail = aig3.GetComponent<RailScriptV2>();
        aig4_rail = aig4.GetComponent<RailScriptV2>();
        aig5_rail = aig5.GetComponent<RailScriptV2>();
        found1 = false;
        found2 = false;
        isGoingBack = false;
        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();
        DEBUGSample = "";
    }

    
    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!sound_1.isPlayed())
        {
            sound_1.play();
        }
        if(ugoMovement.getIntersection() == aig1_rail && isGoingBack)
        {
            if(!sound_5_2.isPlayed())
                sound_5_2.play();
        }else if(ugoMovement.getIntersection() == aig2_rail)
        {
            if (!sound_2_1.isPlayed())
            {
                sound_2_1.play();
            }
            if (getAngleWithObjects(aigToFind3) < 30f)
                found3 = true;
            if (getAngleWithObjects(aigToFind4) < 30f)
                found4 = true;
            if (getAngleWithObjects(aigToFind5) < 30f)
                found5 = true;
            if (getAngleWithObjects(aigToFind6) < 30f)
                found6 = true;
            if(found3 && found4 && found5 && found6)
            {
                if (!sound_2_2.isPlayed())
                {
                    sound_2_2.play();
                    
                }
            }
            if(aig2_rail.aigPosition == RailScriptV2.AigPosition.aig2)
            {
                if(!sound_2_3.isPlayed())
                    sound_2_3.play();
            }
            
            
        } else if (ugoMovement.getIntersection() == aig3_rail)
        {
            if (!sound_3_1.isPlayed())
            {
                sound_3_1.play();
            }
            if (getAngleWithObjects(aigToFind1) < 30f)
                found1 = true;
            if (getAngleWithObjects(aigToFind2) < 30f)
                found2 = true;
            if(found1 && found2)
            {
                if(!sound_3_2.isPlayed())
                    sound_3_2.play();
            }

            if(Input.GetAxis("Vertical") > 0 && ugoMovement.getIntersection().aigPosition == RailScriptV2.AigPosition.aig1
                && ugoMovement.isMovingForward)
            {
                if(!sound_3_3.isPlayed())
                    sound_3_3.play();
            }

        }  else if (ugoMovement.getIntersection() == aig5_rail)
        {
            if(!sound_5_1.isPlayed())
                sound_5_1.play();
            isGoingBack = true;
        } else if (ugoMovement.getIntersection() != null && ugoMovement.getIntersection().isBlocked)
        {
            if (!sound_Err.isPlayed())
            {
                sound_Err.play();
            }
        } else
        {
            sound_Err.setIsPlayed(false);
        }
        DEBUGSample = source.timeSamples + "/" + source.clip.samples;
	}

    IEnumerator playDelayed(float seconds, SoundTemplate sound)
    {
        yield return new WaitForSeconds(seconds);
        sound.play();
    }

    private float getAngleWithObjects(Transform target)
    {
        Vector3 targetCameraVec = target.position - Camera.main.transform.position;
        Vector2 targetCamVec2 = new Vector2(targetCameraVec.x, targetCameraVec.z);
        Vector2 forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        return Vector2.Angle(targetCamVec2, forward);
    }
}
