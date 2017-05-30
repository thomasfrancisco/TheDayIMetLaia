using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_2 : MonoBehaviour {

    public AudioClip N2_0_1;
    public AudioClip N2_1_1;
    public AudioClip N2_1_2;
    public AudioClip N2_1_3;
    public AudioClip N2_2_1;
    public AudioClip N2_2_2;
    public AudioClip N2_3_1;
    public AudioClip N2_3_2;
    public AudioClip N2_3_3;
    public AudioClip N2_3_4;
    public AudioClip N2_4_1;
    public AudioClip N2_4_2;
    public AudioClip N2_5_1;
    public AudioClip N2_5_2;
    public AudioClip N2_5_3;
    public AudioClip N2_5_4;
    public AudioClip N2_5_5;
    public AudioClip N2_6_1;
    public AudioClip N2_6_2_1;
    public AudioClip N2_6_2_2;
    public AudioClip N2_6_3;
    public AudioClip N2_7_1;
    public AudioClip N2_7_2_1;
    public AudioClip N2_7_2_2;
    public AudioClip N2_8_1_1;
    public AudioClip N2_8_1_2;
    public AudioClip N2_8_2_1;
    public AudioClip N2_8_2_2;
    public AudioClip N2_8_3;
    public AudioClip N2_8_4_1;
    public AudioClip N2_8_4_2;
    public AudioClip N2_8_5_1;
    public AudioClip N2_8_5_2;

    public Transform triggerClosingDoor;
    public Transform railGlasses;
    public Transform railObstacleGlassHouse1;
    public Transform triggerAnimalGlassHouse1;
    public Transform trigger1GlassHouse2;
    public Transform trigger2GlassHouse2;
    public Transform audiologGlassHouse3;
    public Transform railCorpse;
    public Transform audiolog1GlassHouse5;
    public Transform audiolog2GlassHouse5;
    public Transform railObstacleExit;
    public Transform railElevator;
    public Transform switchElevator;
    public Transform triggerPuzzleRoom;
    public Transform panelPuzzle;
    public Transform puzzleSequence;

    public float timeBeforeAudiologReminder;
    
    private SoundTemplate sound2_0_1;
    private SoundTemplate sound2_1_1;
    private SoundTemplate sound2_1_2;
    private SoundTemplate sound2_1_3;
    private SoundTemplate sound2_2_1;
    private SoundTemplate sound2_2_2;
    private SoundTemplate sound2_3_1;
    private SoundTemplate sound2_3_2;
    private SoundTemplate sound2_3_3;
    private SoundTemplate sound2_3_4;
    private SoundTemplate sound2_4_1;
    private SoundTemplate sound2_4_2;
    private SoundTemplate sound2_5_1;
    private SoundTemplate sound2_5_2;
    private SoundTemplate sound2_5_3;
    private SoundTemplate sound2_5_4;
    private SoundTemplate sound2_5_5;
    private SoundTemplate sound2_6_1;
    private SoundTemplate sound2_6_2_1;
    private SoundTemplate sound2_6_2_2;
    private SoundTemplate sound2_6_3;
    private SoundTemplate sound2_7_1;
    private SoundTemplate sound2_7_2_1;
    private SoundTemplate sound2_7_2_2;
    private SoundTemplate sound2_8_1_1;
    private SoundTemplate sound2_8_1_2;
    private SoundTemplate sound2_8_2_1;
    private SoundTemplate sound2_8_2_2;
    private SoundTemplate sound2_8_3;
    private SoundTemplate sound2_8_4_1;
    private SoundTemplate sound2_8_4_2;
    private SoundTemplate sound2_8_5_1;
    private SoundTemplate sound2_8_5_2;

    private RailScriptV2 triggerClosingDoorScript;
    private RailScriptV2 railGlassesScript;
    private RailScriptV2 railObstacleGlassHouse1Script;
    private RailScriptV2 trigger1GlassHouse2Script;
    private RailScriptV2 trigger2GlassHouse2Script;
    private AudioLogBehaviour audiologGlassHouse3Script;
    private RailScriptV2 railCorpseScript;
    private AudioLogBehaviour audiolog1GlassHouse5Script;
    private AudioLogBehaviour audiolog2GlassHouse5Script;
    private RailScriptV2 railObstacleExitScript;
    private RailScriptV2 railElevatorScript;
    private SwitchBehavior switchElevatorScript;
    private RailScriptV2 triggerPuzzleRoomScript;
    private Puzzle2Script puzzleSequenceScript;

    private Transform ugo;
    private RailMovementV2 ugoMovement;

    private bool isDoorSeen;
    private bool isElevatorSeen;


    private List<AudioSource> sources;

    private void Awake()
    {
        sources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        sound2_0_1 = new SoundTemplate(N2_0_1, sources);
        sound2_1_1 = new SoundTemplate(N2_1_1, sources);
        sound2_1_2 = new SoundTemplate(N2_1_2, sources);
        sound2_1_3 = new SoundTemplate(N2_1_3, sources);
        sound2_2_1 = new SoundTemplate(N2_2_1, sources);
        sound2_2_2 = new SoundTemplate(N2_2_2, sources);
        sound2_3_1 = new SoundTemplate(N2_3_1, sources);
        sound2_3_2 = new SoundTemplate(N2_3_2, sources);
        sound2_3_3 = new SoundTemplate(N2_3_3, sources);
        sound2_3_4 = new SoundTemplate(N2_3_4, sources);
        sound2_4_1 = new SoundTemplate(N2_4_1, sources);
        sound2_4_2 = new SoundTemplate(N2_4_2, sources);
        sound2_5_1 = new SoundTemplate(N2_5_1, sources);
        sound2_5_2 = new SoundTemplate(N2_5_2, sources);
        sound2_5_3 = new SoundTemplate(N2_5_3, sources);
        sound2_5_4 = new SoundTemplate(N2_5_4, sources);
        sound2_5_5 = new SoundTemplate(N2_5_5, sources);
        sound2_6_1 = new SoundTemplate(N2_6_1, sources);
        sound2_6_2_1 = new SoundTemplate(N2_6_2_1, sources);
        sound2_6_2_2 = new SoundTemplate(N2_6_2_2, sources);
        sound2_6_3 = new SoundTemplate(N2_6_3, sources);
        sound2_7_1 = new SoundTemplate(N2_7_1, sources);
        sound2_7_2_1 = new SoundTemplate(N2_7_2_1, sources);
        sound2_7_2_2 = new SoundTemplate(N2_7_2_2, sources);
        sound2_8_1_1 = new SoundTemplate(N2_8_1_1, sources);
        sound2_8_1_2 = new SoundTemplate(N2_8_1_2, sources);
        sound2_8_2_1 = new SoundTemplate(N2_8_2_1, sources);
        sound2_8_2_2 = new SoundTemplate(N2_8_2_2, sources);
        sound2_8_3 = new SoundTemplate(N2_8_3, sources);
        sound2_8_4_1 = new SoundTemplate(N2_8_4_1, sources);
        sound2_8_4_2 = new SoundTemplate(N2_8_4_2, sources);
        sound2_8_5_1 = new SoundTemplate(N2_8_5_1, sources);
        sound2_8_5_2 = new SoundTemplate(N2_8_5_2, sources);

        ugo = transform.Find("/Player");
        ugoMovement = GetComponent<RailMovementV2>();
        isDoorSeen = false;
        isElevatorSeen = false;
    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(ugo.position, triggerClosingDoor.position) < 1f && ugo.position.z > triggerClosingDoor.position.z && !sound2_0_1.isPlayed())
        {
            playSound(sound2_0_1);
            Debug.Log("Si aucun des membres de quart n’est debout ça m’étonnerait qu’on croise quiconque dans les serres mais sait-on jamais. Les scientifiques qui bossaient ici pendant le voyage étaient de vrais acharnés, convaincus du bien fondé de la mission. Je les vois mal abandonner leur poste et les réserves du vaisseau. Il y a plusieurs serres le long de la salle, tu devrais aller checker à l’intérieur.");
            triggerClosingDoorScript.southRail = null;
            triggerClosingDoorScript.oneWay = false;
            triggerClosingDoorScript.isBlocked = true;
            triggerClosingDoorScript.connectRails();
        } else if (Vector3.Distance(ugo.position, railGlasses.position) < 1f && ugo.position.x < railGlasses.position.x
            && !sound2_1_1.isPlayed())
        {
            playSound(sound2_1_1);
            Debug.Log("Je t'entends rouler sur des débris... C'est pas normal. C'est le verre de la serre?");
        } else if (ugoMovement.getIntersection() == railObstacleGlassHouse1Script && !sound2_1_2.isPlayed())
        {
            playSound(sound2_1_2);
            Debug.Log("On dirait que le chemin est obstrué par des débris. Cet endroit est sensé être dégagé.");
        } else if (ugo.position.x > railObstacleGlassHouse1.position.x && ugo.position.x < railGlasses.position.x
            && ugo.eulerAngles.y > -70f && ugo.eulerAngles.y < -110f && !sound2_1_3.isPlayed())
        {
            playSound(sound2_1_3);
            Debug.Log("C'est quoi ce bruit ?");
        } else if (Vector3.Distance(ugo.position, trigger1GlassHouse2.position) < 1f && !sound2_2_1.isPlayed())
        {
            playSound(sound2_2_1);
            Debug.Log("Toutes les cultures semblent mortes ici");
        } else if (Vector3.Distance(ugo.position, trigger2GlassHouse2.position) < 1f && !sound2_2_2.isPlayed())
        {
            playSound(sound2_2_2);
            Debug.Log("Oh. Quelque chose qui n'est manifestement pas à sa place.");
        } else if (Vector3.Distance(ugo.position, audiologGlassHouse3.position) < audiologGlassHouse3Script.trigger_dist
            && !sound2_3_1.isPlayed())
        {
            playSound(sound2_3_1);
            Debug.Log("UGO, tu es proche d'un AudioLog ! Il devait être a un des scientifiques du vaisseau. Rapproche toi et essaye de lancer la lecture");
            StartCoroutine(testAudiologPlayed());
        } else if (audiologGlassHouse3Script.isFinished && !sound2_3_3.isPlayed())
        {
            playSound(sound2_3_3);
            Debug.Log("Je n'étais pas réveillée pendant que cette équipe était en rotation. Mais je me souviens avoir vus ces résultats la dernière fois que j'ai pris mon quart. J'avais consulté les avancées des équipes scientifiques à bord.");
        } else if (Vector3.Distance(ugo.position, audiologGlassHouse3.position) > audiologGlassHouse3Script.trigger_dist
            && sound2_3_1.isPlayed() && !audiologGlassHouse3Script.isFinished && !sound2_3_4.isPlayed())
        {
            playSound(sound2_3_4);
            Debug.Log("Attends UGO, on ferait mieux d'écouter ce journal de bord jusqu'à la fin");
        } else if (ugoMovement.getIntersection() == railCorpse && !sound2_4_1.isPlayed())
        {
            playSound(sound2_4_1);
            Debug.Log("Heu... Qu'est-ce qui s'est passé ici ? On dirait un... Non ce n'est sûrement pas ça.Je pense qu'il vaut mieux sortir d'ici UGO, ce n'est pas ici qu'on va trouver un moyen d'appeler à l'aide.");
        } else if (sound2_4_1.isFinished() && Input.GetAxis("Vertical") != 0 && !sound2_4_2.isPlayed())
        {
            playSound(sound2_4_2);
            Debug.Log("Là ! UGO ! Qu'est-ce que c'était ? On aurait dit un animal !");
        } else if (Vector3.Distance(ugo.position, audiolog1GlassHouse5.position) < audiolog1GlassHouse5Script.trigger_dist
            && !audiolog1GlassHouse5Script.audioLog_Played && isDoorSeen && !sound2_5_2.isPlayed())
        {
            playSound(sound2_5_2);
            Debug.Log("Un nouvel Audiolog ! Avec un peu de chance on trouvera un moyen d'ouvrir la porte avec ça");
        } else if (audiolog1GlassHouse5Script.isFinished && !isDoorSeen && !sound2_5_1.isPlayed())
        {
            playSound(sound2_5_1);
            Debug.Log("Ces procédures ne sont pas réglementaires. Qu'est ce qui les a poussé à changer l'amménagement des serres et son utilité ? Continuons à explorer un peu plus.");
        } else if (Vector3.Distance(ugo.position, audiolog1GlassHouse5.position) < audiolog1GlassHouse5Script.trigger_dist && audiolog1GlassHouse5Script.isFinished && isDoorSeen && !sound2_5_3.isPlayed())
        {
            //Faudra penser a reset la valeur isFinished quand la porte sera vu
            playSound(sound2_5_3);
            Debug.Log("Il me semble avoir entendu quelqu'un arriver par la porte sécurisée à la fin. Est-ce qu'il était en train de taper un code ?");
        } else if (Vector3.Distance(ugo.position, audiolog2GlassHouse5.position) < audiolog2GlassHouse5Script.trigger_dist
            && !audiolog2GlassHouse5Script.audioLog_Played && !sound2_5_4.isPlayed())
        {
            playSound(sound2_5_4);
            Debug.Log("UGO il me semble qu'il y a un autre audiolog plus loin.");
        } else if (audiolog2GlassHouse5Script.isFinished && !sound2_5_5.isPlayed())
        {
            playSound(sound2_5_5);
            Debug.Log("De quoi parle-t-il ? Un accident ? Une mutinerie ? Les conditions de vie semblent s'être extrêmement dégradées sur ce vaisseau...");
        } else if (ugoMovement.getIntersection() == railObstacleExitScript && railObstacleExitScript.isBlocked
            && !sound2_6_1.isPlayed())
        {
            playSound(sound2_6_1);
            Debug.Log("Merde, le chemin est bloqué... On peut peut être trouver un autre chemin en explorant un peu plus le labo/les serres");
        } else if (!railObstacleExitScript.isBlocked && sound2_4_2.isPlayed() && ugo.position.x == railObstacleExit.position.x
            && Camera.main.transform.eulerAngles.y > -20f && Camera.main.transform.eulerAngles.y < 20f &&
            !sound2_6_2_2.isPlayed() && !sound2_6_2_1.isPlayed())
        {
            playSound(sound2_6_2_1);
            Debug.Log("Le chemin est débloqué on dirait...");
        } else if (!railObstacleExitScript.isBlocked && !sound2_4_2.isPlayed() && ugo.position.x == railObstacleExit.position.x
            && Camera.main.transform.eulerAngles.y > -20f && Camera.main.transform.eulerAngles.y < 20f
            && !sound2_6_2_1.isPlayed() && !sound2_6_2_2.isPlayed())
        {
            playSound(sound2_6_2_2);
            Debug.Log("On dirait que la voie était bloquée il y a peu... ");
        } else if ((sound2_6_2_1.isFinished() || sound2_6_2_2.isFinished()) && !sound2_6_3.isPlayed())
        {
            playSound(sound2_6_3);
            Debug.Log("Ca doit être la chose qu'on a entendu qui nous a libéré le chemin en fuyant... Je n'ai pas vraiment envie de t'emmener vers les ennuis, mais c'est par là qu'il faut aller");
        } else if (ugoMovement.getIntersection() == railElevatorScript && !sound2_7_1.isPlayed())
        {
            playSound(sound2_7_1);
            
            Debug.Log("Cet ascenseur devrait nous mener à la salle des machines, de là on devrait trouver un moyen de booster la liaison distante et tenter d'appeler le QG. Ce n'est pas une solution sure a 100% mais elle mérite qu'on essaye");
        } else if(Vector3.Distance(ugo.position, switchElevator.position) < switchElevatorScript.minDistTrigger
            && getAngleWithObject(switchElevator) < switchElevatorScript.minAngle 
            && (Input.GetButtonDown("Fire1") || Input.inputString == "\n"))
        {
            if (!puzzleSequenceScript.unlocked && !sound2_7_2_1.isPlayed())
            {
                isElevatorSeen = true;
                playSound(sound2_7_2_1);
                Debug.Log("Mince, on dirait que l'ascenseur est hors service. Attends voir... [bip boup] Oui, voilà, tu n'est pas loin d'un panneau de contrôle, essaie de trouver la salle dans laquelle il se trouve");
            } else if (puzzleSequenceScript.unlocked && !sound2_7_2_2.isPlayed())
            {
                playSound(sound2_7_2_2);
                Debug.Log("Ok maintenant on y est. Cet ascenseur t'envoie directement dans la salle des machines à côté du réacteur principal. Toute l'enceinte du réacteur est confinée par des panneaux thermiques et des boucliers radiations. Le signal radio ne passera surement plus tu vas devoir te débrouiller sans moi. Si tu ne trouve personne continue et sort de la salle, le signal radio reviendra au bout d'un moment. [Bruit ascenceur + montée]");
            }
        } else if (Vector3.Distance(ugo.position, triggerPuzzleRoom.position) < 1f && ugo.position.x < triggerPuzzleRoom.position.x)
        {
            if(isElevatorSeen && !sound2_8_1_1.isPlayed())
            {
                playSound(sound2_8_1_1);
                Debug.Log("C'est bien cette salle. En avancant tu devrais te trouver devant le panneau de commande de l'ascenseur");
            } else if (!isElevatorSeen && !sound2_8_1_2.isPlayed())
            {
                playSound(sound2_8_1_2);
                Debug.Log("Je ne me rapelle pas de cette salle... [blip bloup] Ah, si, on dirait un centre de controle des systemes electroniques");
            }
        } else if (Vector3.Distance(ugo.position, puzzleSequence.position) < 1f)
        {
            /**
             * Ce passage risque d'avoir des incohérences..
             * A corriger apres test
             * */
            if(!isElevatorSeen && !sound2_8_2_1.isPlayed())
            {
                playSound(sound2_8_2_1);
                Debug.Log("On dirait un panneau de commande. Ca doit servir à activer ou desactiver certains services du vaisseau");
            } else if (isElevatorSeen && !sound2_8_2_2.isPlayed())
            {
                playSound(sound2_8_2_2);
                Debug.Log("Ca doit être ce panneau qui contrôle les système des ascenseurs. Essaye de voir pour le réactiver");
            } else if (puzzleSequenceScript.unlocked && !sound2_8_3.isPlayed())
            {
                playSound(sound2_8_3);
                Debug.Log("Super, on dirait que cette séquence fonctionne ! L'ascenseur devrait être fonctionnel à présent");
            } else if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() == 1 
                && !audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_4_1.isPlayed())
            {
                playSound(sound2_8_4_1);
                Debug.Log("Je n'ai pas l'impression qu'on va y arriver comme ça. Peut être qu'on peut trouver quelque chose dans la serre qui pourra nous aider ?");
            } else if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() == 1 
                && audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_4_2.isPlayed())
            {
                playSound(sound2_8_4_2);
                Debug.Log("Ces sons là... On dirait une séquence de code d'accès... Ca ressemble à la fin de l'audiolog de la serre 5 non ? Peut être qu'on devrait le ré-écouter.");
            } else if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() > 3
                && audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_5_1.isPlayed())
            {
                playSound(sound2_8_5_1);
                Debug.Log("Je ne sais pas comment fonctionne tout ces systèmes, ce sont les techniciens qui s'occupent de ça habituellement. Mais on dirait que ca te demande de reproduire une séquence");
            } else if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() > 8 
                && audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_5_2.isPlayed())
            {
                playSound(sound2_8_5_2);
                Debug.Log("Retiens la séquence de code qu'on a entendu dans l'audiologue et essayes de la restituer sur les interrupteurs");
            }
        }
		

    }

    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    private IEnumerator testAudiologPlayed()
    {
        yield return new WaitForSeconds(timeBeforeAudiologReminder);
        if (!audiologGlassHouse3Script.audioLog_Played)
            playSound(sound2_3_2);
    }


}
