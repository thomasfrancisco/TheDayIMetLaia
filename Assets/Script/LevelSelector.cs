using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    public enum level
    {
        Beginning,
        CrewQuarter,
        Greenhouse,
        Engine,
        Epilogue
    }

    public level beginAt;
    public List<Transform> beginningTransform;
    public Transform railBeginning;
    public List<Transform> crewQuarterTransform;
    public Transform railCrewQuarter;
    public List<Transform> greenHouseTransform;
    public Transform railGreenhouse;
    public List<Transform> engineTransform;
    public Transform railEngine;
    public List<Transform> epilogueTransform;
    public Transform railEpilogue;

    private RailMovementV2 ugoMovement;
    private Transform ugo;

    private void Awake()
    {
        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();
    }

    // Use this for initialization
    void Start () {
        switch (beginAt)
        {
            case level.Beginning:
                setActive(beginningTransform, true);
                setActive(crewQuarterTransform, true);
                setActive(greenHouseTransform, false);
                setActive(engineTransform, false);
                setActive(epilogueTransform, false);
                ugoMovement.firstRail = railBeginning;
                
                break;
            case level.CrewQuarter:
                setActive(beginningTransform, false);
                setActive(crewQuarterTransform, true);
                setActive(greenHouseTransform, true);
                setActive(engineTransform, false);
                setActive(epilogueTransform, false);
                ugoMovement.firstRail = railCrewQuarter;
                ugoMovement.avanceDebloque = true;
                ugoMovement.reculeDebloque = true;
                break;
            case level.Greenhouse:
                setActive(beginningTransform, false);
                setActive(crewQuarterTransform, false);
                setActive(greenHouseTransform, true);
                setActive(engineTransform, false);
                setActive(epilogueTransform, false);
                ugoMovement.firstRail = railGreenhouse;
                ugoMovement.avanceDebloque = true;
                ugoMovement.reculeDebloque = true;
                break;
            case level.Engine:
                setActive(beginningTransform, false);
                setActive(crewQuarterTransform, false);
                setActive(greenHouseTransform, false);
                setActive(engineTransform, true);
                setActive(epilogueTransform, true);
                ugoMovement.firstRail = railEngine;
                ugoMovement.avanceDebloque = true;
                ugoMovement.reculeDebloque = true;


                break;
            case level.Epilogue:
                setActive(beginningTransform, false);
                setActive(crewQuarterTransform, false);
                setActive(greenHouseTransform, false);
                setActive(engineTransform, true);
                setActive(epilogueTransform, true);
                ugoMovement.avanceDebloque = true;
                ugoMovement.reculeDebloque = true;
                ugoMovement.firstRail = railEpilogue;
                break;
        }
		
	}
	
    private void setActive(List<Transform> transforms, bool value)
    {
        foreach(Transform thing in transforms)
        {
            thing.gameObject.SetActive(value);
        }
    }
}
