using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovementV2 : MonoBehaviour {

    public Transform firstRail;
    public float angleMaxDirection; //Angle entre la vue et le prochain waypoint maximum. Si inferieur, on considere que le joueur n'est pas face à la bonne direction
    public float angleIntersection; //Angle permettant de valider un changement d'aiguillage
    public float speed;

    [HideInInspector]
    public bool isMovingForward;
    [HideInInspector]
    public bool isMovingBackward;

    private RailScriptV2 previous;
    private RailScriptV2 next;

    private float alphaPosition;
    private bool isOnIntersection;
    private RailScriptV2 intersection;
    private bool needDeathPoint;

	// Use this for initialization
	void Start () {
        alphaPosition = 0.01f;
        isMovingForward = false;
        isMovingBackward = false;
        isOnIntersection = false;
        needDeathPoint = false;

        previous = firstRail.GetComponent<RailScriptV2>();
        next = previous.nextRail.GetComponent<RailScriptV2>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isOnIntersection)
        {
            if (!intersection.isBlocked)
            {
                if (!needDeathPoint)
                {
                    if(Input.GetAxis("Vertical") != 0)
                    {
                        nextTrack();
                    } else if(Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                    {
                        updateAiguillage();
                    }
                    
                } else
                {
                    if (Input.GetAxis("Vertical") == 0) needDeathPoint = false;
                }
            } else
            {
                cancelMovement();
            }
        } else
        {
            move();
            testIntersection();
        }
	}


    //Met a jour l'aiguillage
    private void updateAiguillage()
    {
        RailScriptV2[] choiceUndisposedIntersection = intersection.getUndisposedAiguillage();
        if(getAngleWithObject(choiceUndisposedIntersection[0].transform) < angleIntersection
            || getAngleWithObject(choiceUndisposedIntersection[1].transform) < angleIntersection)
        {
            intersection.changeAiguillage();
        }
    }

    //Place le joueur sur la nouvelle voie
    private void nextTrack()
    {
        RailScriptV2[] choiceIntersection = intersection.getRailAiguillage();
        if(Input.GetAxis("Vertical") > 0)
        {
            if(getAngleWithObject(choiceIntersection[0].transform) < angleIntersection)
            {
                previous = intersection;
                next = choiceIntersection[0];
                isOnIntersection = false;
                alphaPosition = 0.01f;
            } else if (getAngleWithObject(choiceIntersection[1].transform) < angleIntersection)
            {
                previous = intersection;
                next = choiceIntersection[1];
                isOnIntersection = false;
                alphaPosition = 0.01f;
            }
        } else if(Input.GetAxis("Vertical") < 0)
        {
            if(getAngleWithObject(choiceIntersection[0].transform) < angleIntersection)
            {
                previous = choiceIntersection[1];
                next = intersection;
                isOnIntersection = false;
                alphaPosition = 0.99f;
            } else if(getAngleWithObject(choiceIntersection[1].transform) < angleIntersection)
            {
                previous = choiceIntersection[1];
                next = intersection;
                isOnIntersection = false;
                alphaPosition = 0.99f;
            }
        }
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
            intersection = previous;
            isOnIntersection = true;
            needDeathPoint = true;
        } else if (alphaPosition > 1)
        {
            intersection = next;
            isOnIntersection = true;
            needDeathPoint = true;
        }
    }

    //Retourne l'angle entre le vecteur de la camera et l'objet
    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
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
            }
            else
            {
                isMovingForward = false;
                isMovingBackward = true;
            }
        } else
        {
            isMovingBackward = false;
            isMovingForward = false;
        }
    }

    //Met a jour l'alpha permettant le déplacement
    private float newAlphaPosition()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        if(verticalAxis != 0)
        {
            float angleNextElement = getAngleWithObject(previous.transform);
            float angleSecondElement = getAngleWithObject(next.transform);
            if(angleSecondElement < angleMaxDirection)
            {
                updateMovementState(verticalAxis, true);
                return verticalAxis * speed * Time.deltaTime;
            } else if (angleNextElement < angleMaxDirection)
            {
                updateMovementState(verticalAxis, true);
                return verticalAxis * speed * Time.deltaTime * -1;
            } else
            {
                updateMovementState(verticalAxis, false);
                return 0f;
            }
        } else
        {
            updateMovementState(verticalAxis, false);
            return 0f;
        }
    }

    private void cancelMovement()
    {
        if(alphaPosition < 0)
        {
            alphaPosition += speed * Time.deltaTime;
        } else
        {
            alphaPosition -= speed * Time.deltaTime;
        }
        isOnIntersection = false;
    }
}
