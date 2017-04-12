using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovement : MonoBehaviour
{

    public float angleMaxDirection;
    public float angleMaxDirectionIntersection;
    public float speed;
    public Transform firstRail;

    private Transform firstElement;
    private Transform secondElement;
    private Transform nextElement; //Save it to test in update loop
    private Transform previousElement;  //Save it to test in update loop
    private float alpha;

    private bool isOnIntersection;
    private bool needDeathPoint;
    private Transform[] threeWaysIntersection = new Transform[4];
    private Transform intersection;

    // Use this for initialization
    void Start()
    {
        alpha = 0.01f;
        firstElement = firstRail;
        secondElement = firstRail.gameObject.GetComponent<RailBehavior>().nextRail;
        nextElement = secondElement.gameObject.GetComponent<RailBehavior>().nextRail;
        previousElement = null;
        transform.position = firstElement.position;
        isOnIntersection = false;
        needDeathPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnIntersection)
        {
            if (!needDeathPoint)
            {
                chooseDirection();
            } else
            {
                if(Input.GetAxis("Vertical") == 0)
                {
                    needDeathPoint = false;
                }
            }
            
        }
        else
        {
            transform.position = Vector3.Lerp(firstElement.position, secondElement.position, alpha);
            alpha += newAlphaPosition();
            if (alpha > 1)
            {
                if (nextElement != null && !secondElement.GetComponent<RailBehavior>().isBlocked)
                {
                    if (secondElement.GetComponent<RailBehavior>().thirdRail != null)
                    {
                        isOnIntersection = true;
                        needDeathPoint = true;
                        intersection = secondElement;
                        threeWaysIntersection = fillThreeWays(secondElement);
                    }
                    else
                    {
                        nextTrack();
                    }
                }
                else
                {
                    alpha = 0.99f;
                }
            }

            else if (alpha < 0)
            {
                if (previousElement != null && !firstElement.GetComponent<RailBehavior>().isBlocked)
                {
                    if (firstElement.GetComponent<RailBehavior>().thirdRail != null)
                    {
                        isOnIntersection = true;
                        needDeathPoint = true;
                        intersection = firstElement;
                        threeWaysIntersection = fillThreeWays(firstElement);
                    }
                    else
                    {
                        previousTrack();
                    }
                }
                else
                {
                    alpha = 0;
                }
            }
        }


    }

    private void nextTrack()
    {
        previousElement = firstElement;
        firstElement = secondElement;
        secondElement = nextElement;
        nextElement = nextElement.GetComponent<RailBehavior>().nextRail;
        alpha -= 1;
    }

    private void previousTrack()
    {
        nextElement = secondElement;
        secondElement = firstElement;
        firstElement = previousElement;
        previousElement = previousElement.GetComponent<RailBehavior>().previousRail;
        alpha += 1;
    }

    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
    }

    private float newAlphaPosition()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            float angleFirstElement = getAngleWithObject(firstElement);
            float angleSecondElement = getAngleWithObject(secondElement);
            if (angleSecondElement < angleMaxDirection)
            {
                return Input.GetAxis("Vertical") * speed * Time.deltaTime;
            }
            else if (angleFirstElement < angleMaxDirection)
            {
                return -Input.GetAxis("Vertical") * speed * Time.deltaTime;
            }
            else
            {
                return 0f;
            }

        }
        else
        {
            return 0f;
        }
    }

    private void chooseDirection()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            Transform nearestTrack = getNearestTrackOnIntersection();
            if (nearestTrack != null)
            {
                nextElement = nearestTrack.GetComponent<RailBehavior>().nextRail;
                secondElement = nearestTrack;
                firstElement = intersection;
                previousElement = intersection.GetComponent<RailBehavior>().previousRail;
                isOnIntersection = false;
                alpha = 0.1f;
            }
        }
    }

    private Transform[] fillThreeWays(Transform intersection)
    {
        return new Transform[] {
            intersection.GetComponent<RailBehavior>().nextRail,
            intersection.GetComponent<RailBehavior>().previousRail,
            intersection.GetComponent<RailBehavior>().thirdRail,
            intersection.GetComponent<RailBehavior>().fourthRail
        };
    }

    // Devrait y avoir moyen d'optimiser ça un peu
    //Retourne la direction a prendre la plus proche de l'angle de la cam
    //Retourne null si aucune n'est assez proche
    private Transform getNearestTrackOnIntersection()
    {

        float[] angles = new float[threeWaysIntersection.Length];
        for (int i = 0; i < angles.Length; i++)
        {
            angles[i] = getAngleWithObject(threeWaysIntersection[i]);
        }
        float minAngle = Mathf.Min(angles);
        if (minAngle < angleMaxDirectionIntersection)
        {
            for (int i = 0; i < angles.Length; i++)
            {
                if (angles[i] == minAngle)
                {
                    return threeWaysIntersection[i];
                }
            }
        }

        return null;
    }
    
}
