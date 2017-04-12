using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RailDraw : MonoBehaviour
{

    public Material lineMaterial;

    private GameObject lines;
    /*
    private List<Vector3> positionsRail;
    private LineRenderer line;
    */

    // Use this for initialization
    void Start()
    {
        if(transform.Find("/Lines") != null)
        {
            DestroyImmediate(transform.Find("/Lines").gameObject);
        }
        lines = new GameObject("Lines");
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3[] positions = new Vector3[2];
            if (transform.GetChild(i).GetComponent<RailBehavior>().nextRail != null)
            {
                positions[0] = transform.GetChild(i).position;
                positions[1] = transform.GetChild(i).GetComponent<RailBehavior>().nextRail.position;
                addLineRenderer(positions);
            }
            if (transform.GetChild(i).GetComponent<RailBehavior>().thirdRail != null)
            {
                positions[1] = transform.GetChild(i).GetComponent<RailBehavior>().thirdRail.position;
                addLineRenderer(positions);
            }
            if(transform.GetChild(i).GetComponent<RailBehavior>().fourthRail != null)
            {
                positions[1] = transform.GetChild(i).GetComponent<RailBehavior>().fourthRail.position;
                addLineRenderer(positions);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //En attendant de trouver un truc meilleur
        Start();
    }

    void addLineRenderer(Vector3 [] positions)
    {
        GameObject go = new GameObject("Line");
        go.transform.parent = lines.transform;
        LineRenderer line = go.AddComponent<LineRenderer>();
        line.startWidth = 0.5f;
        line.textureMode = LineTextureMode.Tile;
        line.material = lineMaterial;
        line.numPositions = positions.Length;
        line.SetPositions(positions);
    }
}
