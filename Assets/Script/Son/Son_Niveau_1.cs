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

    public Transform railTriggerClosingDoor;
    public Transform railDouille;
    public Transform railObstacle;
    public Transform intersectionTutoDemitour;
    public Transform intersectionSansAig;
    public Transform railEvent01;
    public Transform railDoorClosed;

    public float delayBeforeUTurnReminder;

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

    private RailScriptV2 railTriggerClosingDoorScript;
    private RailScriptV2 railDouilleScript;
    private RailScriptV2 railObstacleScript;
    private RailScriptV2 intersectionTutoDemiTourScript;
    private RailScriptV2 intersectionSansAigScript;
    private RailScriptV2 railEvent01Script;
    private RailScriptV2 railDoorClosedScript;

    private Transform ugo;
    private RailMovementV2 ugoMovement;

    //Permettant les trigger d'erreur du joueur
    private bool uTurnDone;
    

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

        railTriggerClosingDoorScript = railTriggerClosingDoor.GetComponent<RailScriptV2>();
        railDouilleScript = railDouille.GetComponent<RailScriptV2>();
        railObstacleScript = railObstacle.GetComponent<RailScriptV2>();
        intersectionTutoDemiTourScript = intersectionTutoDemitour.GetComponent<RailScriptV2>();
        intersectionSansAigScript = intersectionSansAig.GetComponent<RailScriptV2>();
        railEvent01Script = railEvent01.GetComponent<RailScriptV2>();
        railDoorClosedScript = railDoorClosed.GetComponent<RailScriptV2>();

        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();

        uTurnDone = false;

    }

	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(ugo.position, railTriggerClosingDoor.position) < 1f && ugo.position.z > railTriggerClosingDoor.position.z  && !sound1_1_1.isPlayed())
        {
            playSound(sound1_1_1);
            Debug.Log("On dirait bien qu'il ne reste qu'une direction... Elle ne va pas se rouvrir de suite");
            railTriggerClosingDoorScript.southRail = null;
            railTriggerClosingDoorScript.oneWay = false;
            railTriggerClosingDoorScript.isBlocked = true;
            railTriggerClosingDoorScript.connectRails();
        } else if (sound1_1_1.isPlayed() && sound1_1_1.isFinished() && !sound1_2_1.isPlayed())
        {
            playSound(sound1_2_1);
            Debug.Log("Hum… Il devrait y avoir au moins l’équipage de quart ici. On est au milieu des quartiers d’équipage et tu n’as croisé personne. Et je ne détecte aucun signe d’activités humaines dans les environs. Ni actuelles ni même récentes…Ils ne sont tout de même pas tous dans leur caisson d’hibernation... Il me faut l'aide des techniciens pour sortir de ma cabine, tout est détruit et coincé ici.");
        } else if (Vector3.Distance(ugo.position, railDouille.position) < 1f  && !sound1_2_2.isPlayed())
        {
            playSound(sound1_2_2);
            Debug.Log("C'est des ... Balles ? Quelqu'un a tiré à l'INTÉRIEUR du vaisseau ? Trouvons quelqu'un rapidement.");
        } else if (ugoMovement.getIntersection() == railObstacleScript && !sound1_3_1.isPlayed())
        {
            playSound(sound1_3_1);
            Debug.Log("Quoi ?! [court silence] Le chemin ne devrait pas être bloqué, c’était la nef des officiers par là. Quelque chose doit encombrer le couloir et les rails. Ce vaisseau a clairement des problèmes. Attends voir, je devrais avoir accès au système d'aiguillage.");
        } else if (!sound1_3_2.isPlayed() && sound1_3_1.isFinished()) // && sfxAigActivation.isFinished)
        {
            playSound(sound1_3_2);
            Debug.Log("Bingo. Fait un demi tour sur toi même et revient sur ton chemin, tu devrais tomber sur une plateforme d'aiguillage");
            intersectionTutoDemiTourScript.oneWay = false;
            StartCoroutine(testUturn());

        } else if(sound1_3_2.isPlayed() && !sound1_3_4.isPlayed() && Camera.main.transform.eulerAngles.y > 160f && Camera.main.transform.eulerAngles.y < 200f)
        {
            playSound(sound1_3_4);
            Debug.Log("Parfait, continue tout droit, tu devrais arriver sur la plateforme");
            uTurnDone = true;
        } else if (sound1_3_4.isPlayed() && !sound1_4_1.isPlayed() && ugoMovement.getIntersection() == intersectionTutoDemiTourScript)
        {
            playSound(sound1_4_1);
            Debug.Log("Ok, L'aiguillage devrait t'envoyer des signaux pour te donner une indication des directions proposées. Chaque son correspond a un point cardinal entre le Nord, Sud, Est et l'Ouest.Tu devrais pouvoir l'actionner en regardant dans la direction vers laquelle tu veux aller. Tiens, Essaye d'aller a gauche, en direction du Sud, ca nous permettra d'avancer");
        } else if (sound1_4_1.isPlayed() && ugoMovement.getIntersection().aigPosition == RailScriptV2.AigPosition.aig1 && ugoMovement.getIntersection() == intersectionTutoDemiTourScript 
            && ugoMovement.isMovingForward && !sound1_4_2.isPlayed())
        {
            playSound(sound1_4_2);
            Debug.Log("Je ne pense pas que ce soit utile de revenir sur tes pas UGO, retrourne sur l'aiguillage et prend la nouvelle direction");
        } else if (ugoMovement.getIntersection() == intersectionSansAigScript && !sound1_4_3.isPlayed())
        {
            playSound(sound1_4_3);
            Debug.Log("Je ne sais pas dans quel état sont tous les sytème du vaisseau, mais il est possible que certains aiguillages ne te proposent qu'un choix... Mais ils fonctionnent de la même manière");
        } else if (ugoMovement.getIntersection() == railEvent01Script && !sound1_5.isPlayed())
        {
            playSound(sound1_5);
            Debug.Log("Quelque chose nous a percuté ?! Si ce vaisseau est à la dérive il faut absolument qu'on le sâche. Si nous ne trouvons personne il faudra se rendre à la salle des machines pour checker l'état du vaisseau et du réacteur.");
        } else if (ugoMovement.getIntersection() == railDoorClosedScript && !sound1_6_1.isPlayed())
        {
            playSound(sound1_6_1);
            Debug.Log("On dirait que la porte est bloqué comme celle du local de maintenance...");
        }
        		
	}

    private IEnumerator playSoundDelayed(SoundTemplate sound, float sec)
    {
        yield return new WaitForSeconds(sec);

        playSound(sound);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    private IEnumerator testUturn()
    {
        yield return new WaitForSeconds(delayBeforeUTurnReminder);
        if (!uTurnDone)
        {
            playSound(sound1_3_3);
            Debug.Log("Je ne sais pas si ton moteur est entièrement fonctionnel, mais en tournant ta tête a 180° tu devrais enclencher ton mécanisme de demi-tour");
        }
    }
}
