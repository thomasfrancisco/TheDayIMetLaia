using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_4 : MonoBehaviour {

    public List<Transform> previousThing;

    public AudioClip n4_1;
    public AudioClip n4_2;
    public AudioClip n4_3;
    public AudioClip n4_4;
    public AudioClip n4_5;
    public AudioClip n4_6;

    public AudioClip eject_Sas;
    public AudioClip amb_ext;
    public AudioClip amb_feuill;
    public AudioClip amb_ocean;
    public AudioClip analysis;
    public AudioClip animal1;
    public AudioClip animal2;
    public AudioClip antenna;
    public AudioClip drone;

    public Transform transformAmb_ext;
    public Transform transformAmb_feuill;
    public Transform transformAmb_ocean;
    public Transform transformAnalysis;
    public Transform transformAnimal1;
    public Transform transformAnimal2;
    public Transform transformAntenna;
    public Transform transformDrone;
    public Transform railOutside;

    private SoundTemplate sound4_1;
    private SoundTemplate sound4_2;
    private SoundTemplate sound4_3;
    private SoundTemplate sound4_4;
    private SoundTemplate sound4_5;
    private SoundTemplate sound4_6;
    private SoundTemplate eject_sasSound;
    private SoundTemplate amb_extSound;
    private SoundTemplate amb_feuillSound;
    private SoundTemplate amb_oceanSound;
    private SoundTemplate analysisSound;
    private SoundTemplate animal1Sound;
    private SoundTemplate animal2Sound;
    private SoundTemplate antennaSound;
    private SoundTemplate droneSound;

    private AudioSource source;
    private AudioSource sourceAmb_ext;
    private AudioSource sourceAmb_feuill;
    private AudioSource sourceAmb_ocean;
    private AudioSource sourceAnalysis;
    private AudioSource sourceAnimal1;
    private AudioSource sourceAnimal2;
    private AudioSource sourceDrone;

    private Transform ugo;
    private RailMovementV2 ugoMovement;

    private bool needAntenna;
    private bool needAnalysis;
	// Use this for initialization
	void Awake () {
        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();
        source = GetComponentInChildren<AudioSource>();
        sourceAmb_ext = transformAmb_ext.GetComponent<AudioSource>();
        sourceAmb_feuill = transformAmb_feuill.GetComponent<AudioSource>();
        sourceAmb_ocean = transformAmb_ocean.GetComponent<AudioSource>();
        sourceAnalysis = transformAnalysis.GetComponent<AudioSource>();
        sourceAnimal1 = transformAnimal1.GetComponent<AudioSource>();
        sourceDrone = transformDrone.GetComponent<AudioSource>();
        sourceAnimal2 = transformAnimal2.GetComponent<AudioSource>();
        sound4_1 = new SoundTemplate(n4_1, source);
        sound4_2 = new SoundTemplate(n4_2, source);
        sound4_3 = new SoundTemplate(n4_3, source);
        sound4_4 = new SoundTemplate(n4_4, source);
        sound4_5 = new SoundTemplate(n4_5, source);
        sound4_6 = new SoundTemplate(n4_6, source);
        eject_sasSound = new SoundTemplate(eject_Sas, source);
        amb_extSound = new SoundTemplate(amb_ext, sourceAmb_ext);
        amb_feuillSound = new SoundTemplate(amb_feuill, sourceAmb_feuill);
        amb_oceanSound = new SoundTemplate(amb_ocean, sourceAmb_ocean);
        analysisSound = new SoundTemplate(analysis, source);
        animal1Sound = new SoundTemplate(animal1, sourceAnimal1);
        animal2Sound = new SoundTemplate(animal2, sourceAnimal2);
        antennaSound = new SoundTemplate(antenna, source);
        droneSound = new SoundTemplate(drone, sourceDrone);
        needAntenna = false;
        needAnalysis = false;
        
		
	}
	
	// Update is called once per frame
	void Update () {

        if (needAnalysis)
        {
            if (Input.GetButtonDown("Fire1") || Input.inputString == "\n")
            {
                ugoMovement.doAction = false;
                needAnalysis = false;
                StartCoroutine(routinePlayAfterAnalysis());
            }
        }

        if (needAntenna)
        {
            if (Input.GetButtonDown("Fire1") || Input.inputString == "\n")
            {
                ugoMovement.doAction = false;
                needAntenna = false;
                StartCoroutine(routinePlayAfterAntenna());
            }
        }
	}

    public void playFinal()
    {
            StartCoroutine(routinePlayFinal());
            foreach (Transform thing in previousThing)
            {
                thing.gameObject.SetActive(false);
            }
    }

    private IEnumerator routinePlayFinal()
    {
        eject_sasSound.play();
        ugoMovement.enabled = false;
        ugo.position = railOutside.position;
        yield return new WaitForSeconds(10f);
        StartCoroutine(amb_extSound.fadeIn(15f));
        StartCoroutine(amb_feuillSound.fadeIn(15f));
        StartCoroutine(amb_oceanSound.fadeIn(15f));
        StartCoroutine(droneSound.fadeIn(15f));
        yield return new WaitForSeconds(eject_Sas.length - 10f + 1f);
        sound4_2.play();
        needAntenna = true;
        
    }

    private IEnumerator routinePlayAfterAntenna()
    {
        antennaSound.play();
        yield return new WaitForSeconds(antenna.length);
        sound4_3.play();
        yield return new WaitForSeconds(n4_3.length + 1f);
        animal1Sound.play();
        animal2Sound.play();
        yield return new WaitForSeconds(3f);
        sound4_4.play();
        yield return new WaitForSeconds(n4_4.length + 1f);
        sound4_5.play();
        needAnalysis = true;
        
    }

    private IEnumerator routinePlayAfterAnalysis()
    {
        analysisSound.play();
        yield return new WaitForSeconds(analysis.length);
        sound4_6.play();
        StartCoroutine(amb_extSound.fadeOut(n4_6.length / 8));
        StartCoroutine(amb_feuillSound.fadeOut(n4_6.length / 8));
        StartCoroutine(amb_oceanSound.fadeOut(n4_6.length / 8));
        StartCoroutine(droneSound.fadeOut(n4_6.length / 8));
    }
    
    
    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }
}
