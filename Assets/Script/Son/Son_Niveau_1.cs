using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_1 : MonoBehaviour {
    public AudioClip N1_1_1;
    public AudioClip N1_2_1;
    public AudioClip N1_2_2;
    public AudioClip N1_3_1;
    public AudioClip N1_3_2;
    public AudioClip N1_3_3;
    public AudioClip N1_3_4;
    public AudioClip N1_4_1;
    public AudioClip N1_4_2;
    public AudioClip N1_4_3;
    public AudioClip N1_5;
    public AudioClip N1_6_1;

    public Transform railClosingDoor;
    public Transform railDouille;
    public Transform railObstacle;
    public Transform intersectionTutoDemitour;
    public Transform intersectionSansAig;
    public Transform N1_Event01;
    public Transform railDoorClosed;

    private List<AudioSource> sources;
    private SoundTemplate sound1_1_1;
    private SoundTemplate sound1_2_1;
    private SoundTemplate sound1_2_2;
    private SoundTemplate sound1_3_1;
    private SoundTemplate sound1_3_2;
    private SoundTemplate sound1_3_3;
    private SoundTemplate sound1_3_4;
    private SoundTemplate sound1_4_1;
    private SoundTemplate sound1_4_2;
    private SoundTemplate sound1_4_3;
    private SoundTemplate sound1_5;
    private SoundTemplate sound1_6_1;

    private RailScriptV2 railClosingDoorScript;
    private RailScriptV2 railDouilleScript;
    private RailScriptV2 railObstacleScript;
    private RailScriptV2 intersectionTutoDemiTourScript;
    private RailScriptV2 intersectionSansAigScript;
    private RailScriptV2 railDoorClosedScript;




    private void Awake()
    {
        sources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        sound1_1_1 = new SoundTemplate(N1_1_1, sources);
        sound1_2_1 = new SoundTemplate(N1_2_1, sources);
        sound1_2_2 = new SoundTemplate(N1_2_2, sources);
        sound1_3_1 = new SoundTemplate(N1_3_1, sources);
        sound1_3_2 = new SoundTemplate(N1_3_2, sources);
        sound1_3_3 = new SoundTemplate(N1_3_3, sources);
        sound1_3_4 = new SoundTemplate(N1_3_4, sources);
        sound1_4_1 = new SoundTemplate(N1_4_1, sources);
        sound1_4_2 = new SoundTemplate(N1_4_2, sources);
        sound1_4_3 = new SoundTemplate(N1_4_3, sources);
        sound1_5 = new SoundTemplate(N1_5, sources);
        sound1_6_1 = new SoundTemplate(N1_6_1, sources);

        railClosingDoorScript = railClosingDoor.GetComponent<RailScriptV2>();
        railDouilleScript = railDouille.GetComponent<RailScriptV2>();
        railObstacleScript = railObstacle.GetComponent<RailScriptV2>();
        intersectionTutoDemiTourScript = intersectionTutoDemitour.GetComponent<RailScriptV2>();
        intersectionSansAigScript = intersectionSansAig.GetComponent<RailScriptV2>();
        railDoorClosedScript = railDoorClosed.GetComponent<RailScriptV2>();
    }

	// Update is called once per frame
	void Update () {

		
	}

    private void playSound(AudioClip clip)
    {

    }
}
