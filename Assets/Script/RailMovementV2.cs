using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovementV2 : MonoBehaviour {

    public Transform firstRail;
    public float angleMaxDirection; //Angle entre la vue et le prochain waypoint maximum. Si inferieur, on considere que le joueur n'est pas face à la bonne direction
    public float angleIntersection; //Angle permettant de valider un changement d'aiguillage
    public float speed;

    public bool avanceDebloque;
    public bool reculeDebloque;

    //boolean pour les sons avancer et reculer
    //[HideInInspector]
    public bool isMovingForward;
    [HideInInspector]
    public bool isMovingBackward;
    [HideInInspector]
    public bool isMovingBlocked;

    private RailScriptV2 previous;
    private RailScriptV2 next;

    public float alphaPosition;  

    private bool isOnIntersection;
    private RailScriptV2 intersection;
    private bool needDeathPoint;
    private bool isAigMoving;

    //Timer pour jouer les sons
    private float timerSound;
    public float frequenceSons;

    //Autre source sonore correspondant au corp d'UGO
    private SonChoc sonCollision;
    private SonAiguillage sonAiguillage;
    private SonRoue sonRoue;
    [HideInInspector]
    public bool doAction;
    private SonAction sonAction;

	// Use this for initialization
	void Start () {
        alphaPosition = 0.01f;
        isMovingForward = false;
        isMovingBackward = false;
        isOnIntersection = false;
        needDeathPoint = false;
        doAction = false;
        isAigMoving = false;

        previous = firstRail.GetComponent<RailScriptV2>();
        next = previous.allRails[0];
        transform.position = firstRail.position;

        timerSound = 0f;

        sonCollision = transform.FindChild("Choc").GetComponent<SonChoc>();
        sonAiguillage = transform.FindChild("Aiguillage").GetComponent<SonAiguillage>();
        sonRoue = transform.FindChild("Roues").GetComponent<SonRoue>();
        sonAction = transform.FindChild("Action").GetComponent<SonAction>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") || Input.inputString == "\n")
            doAction = true;
        if (isOnIntersection)
        {
            if (!intersection.isBlocked)
            {
                playSoundsRails();
                sonAiguillage.playIntersection();
                if (!needDeathPoint)
                {
                    if (isAigMoving)
                    {
                        if (sonAiguillage.playMouvement())
                        {
                            isAigMoving = false;
                        }
                    }
                    else
                    {
                        if (Input.GetAxis("Vertical") != 0)
                        {
                            if (nextTrack())
                            {
                                sonAiguillage.reset();
                                sonAiguillage.playDecroche();
                            }
                        }
                        else if (Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                        {
                            updateAiguillage();
                        }
                    }
                    
                } else
                {
                    updateMovementState(0f, false);
                    if (Input.GetAxis("Vertical") == 0) needDeathPoint = false;
                }
            } else
            {
                if (!nextTrack())
                    cancelMovement();
                else
                    sonCollision.resetCollision();
                
            }
        } else
        {
            if (movementUnlocked())
            {
                move();
                testIntersection();
            } else
            {
                updateMovementState(0f, false);
            }
            
        }
	}

    
    //Met a jour l'aiguillage
    private void updateAiguillage()
    {
        RailScriptV2[] choiceUndisposedIntersection = intersection.getUndisposedAiguillage();
        if (choiceUndisposedIntersection.Length == 2)
        {
            if (getAngleWithObject(choiceUndisposedIntersection[0].transform) < angleIntersection
                || getAngleWithObject(choiceUndisposedIntersection[1].transform) < angleIntersection)
            {
                doAction = false;
                intersection.changeAiguillage();
                isAigMoving = true;

            }
        } else if (choiceUndisposedIntersection.Length == 1)
        {
            if(getAngleWithObject(choiceUndisposedIntersection[0].transform) < angleIntersection)
            {
                doAction = false;
                intersection.changeAiguillage();
                isAigMoving = true;
            }
        } else
        {
            //Pas d'intersection

        }
    }

    //Place le joueur sur la nouvelle voie
    private bool nextTrack()
    {
        RailScriptV2[] choiceIntersection = intersection.getRailAiguillage();
        if(Input.GetAxis("Vertical") > 0)
        {
            if (choiceIntersection.Length > 0)
            {
                if (getAngleWithObject(choiceIntersection[0].transform) < angleIntersection)
                {
                    previous = intersection;
                    next = choiceIntersection[0];
                    isOnIntersection = false;
                    alphaPosition = 0.01f;
                    return true;
                }
            }
            if (choiceIntersection.Length > 1)
            {
                if (getAngleWithObject(choiceIntersection[1].transform) < angleIntersection)
                {
                    previous = intersection;
                    next = choiceIntersection[1];
                    isOnIntersection = false;
                    alphaPosition = 0.01f;
                    return true;
                }
            }
        } else if(Input.GetAxis("Vertical") < 0)
        {
            if (choiceIntersection.Length > 0)
            {
                if (getAngleWithObject(choiceIntersection[0].transform, true)<  angleIntersection)
                {
                    previous = choiceIntersection[0];
                    next = intersection;
                    isOnIntersection = false;
                    alphaPosition = 0.99f;
                    return true;
                }
            }
            if (choiceIntersection.Length > 1)
            {
                if (getAngleWithObject(choiceIntersection[1].transform, true)< angleIntersection)
                {
                    previous = choiceIntersection[1];
                    next = intersection;
                    isOnIntersection = false;
                    alphaPosition = 0.99f;
                    return true;
                }
            }
        }
        return false;
    }

    //Mouvement classique entre deux voies
    private void move()
    {

        transform.position = Vector3.Lerp(previous.transform.position, next.transform.position, alphaPosition);
        alphaPosition += newAlphaPosition();

    }

    //Verifie que le joueur se trouve sur une intersection.
    private void testIntersection()
    {
        if(alphaPosition < 0)
        {
            if (!previous.oneWay)
            {
                intersection = previous;
                isOnIntersection = true;
                needDeathPoint = true;
            } else
            {
                //Cas d'un rail "OneWay"
                if (!previous.isBlocked)
                {
                    if (previous.getRailAiguillage().Length > 1)
                    {
                        next = previous;
                        previous = previous.getRailAiguillage()[1];
                        alphaPosition = 0.99f;
                    }
                }
                
            }
        } else if (alphaPosition > 1)
        {
            if (!next.oneWay)
            {
                intersection = next;
                isOnIntersection = true;
                needDeathPoint = true;
            } else
            {
                //Cas d'un rail "OneWay"
                if (!next.isBlocked)
                {
                    if (next.getRailAiguillage().Length > 1)
                    {
                        previous = next;
                        next = next.getRailAiguillage()[0];
                        alphaPosition = 0.01f;
                    }
                }
                
            }
        }
    }

    //Retourne l'angle entre le vecteur de la camera et l'objet
    private float getAngleWithObject(Transform target, bool isBack = false)
    {
        if (!isBack)
            return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
        else
            return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward * -1);
    }

    //Met a jour les variables de déplacements (Pour le son)
    private void updateMovementState(float verticalAxis, bool isMoving)
    {
        if (isMoving)
        {
            if (verticalAxis > 0)
            {
                isMovingForward = true;
                isMovingBackward = false;
                isMovingBlocked = false;
            }
            else
            {
                isMovingBackward = true;
                isMovingForward = false;
                isMovingBlocked = false;
            }
        } else
        {
            isMovingBlocked = false;
            isMovingForward = false;
            isMovingBackward = false;

        }
    }

    //Met a jour l'alpha permettant le déplacement
    private float newAlphaPosition()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float angleNextElement = getAngleWithObject(previous.transform);
        float angleSecondElement = getAngleWithObject(next.transform);
        
        if(angleSecondElement < angleMaxDirection)
        {
            if (verticalAxis != 0)
            {
                updateMovementState(verticalAxis, true);
                return verticalAxis * (speed / Vector3.Distance(previous.transform.position, next.transform.position)) * Time.deltaTime;
            }
        } else if(angleNextElement < angleMaxDirection)
        {
            if(verticalAxis != 0)
            {
                updateMovementState(verticalAxis, true);
                return verticalAxis * (speed / Vector3.Distance(previous.transform.position, next.transform.position)) * Time.deltaTime * -1;
            }
        }
        updateMovementState(verticalAxis, false);
        return 0f;

    }


    // Vérifie si la fonction "avancer" ou "reculer" est bien débloqué
    private bool movementUnlocked()
    {
        //Peut etre possible d'éviter ça...
        //return true;
        return ((Input.GetAxis("Vertical") > 0 && avanceDebloque)
            || (Input.GetAxis("Vertical") < 0 && reculeDebloque));

    }

    //Annule un mouvement en cas de collision avec une bordure
    private void cancelMovement()
    {
        updateMovementState(0f, false);
        if (Input.GetAxis("Vertical") != 0)
        {
            isMovingBlocked = true;
            intersection.isCollided = true;
            sonCollision.playCollision();
        }
        else
        {
            isMovingBlocked = false;
        }
    }

    //Joue les sons des voies empruntables lorsqu'on est sur une intersection
    private void playSoundsRails()
    {
        timerSound += Time.deltaTime;
        if(timerSound > frequenceSons)
        {
            timerSound = 0f;
            intersection.playNextRailSound();
        }
        
    }

    public RailScriptV2 getIntersection()
    {
        return intersection;
    }

   
}
