using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class windowRailCreation : EditorWindow {
    
    private Object rail;
    private bool isPainting;
    private Material lineMat;

    private Transform parent;
    private RaycastHit hit;

    private Transform lastObject;
    private Transform beforeLast; //Pour annuler un placement

    [MenuItem("Window/The Day I Met Laia/Creation de rail")]
    public static void CreateWindow()
    {
        windowRailCreation window = EditorWindow.GetWindow<windowRailCreation>();
        window.Show();
        window.wantsMouseMove = true;
    }

    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    private void OnGUI()
    {
        rail = EditorGUILayout.ObjectField("Rail", rail, typeof(GameObject), false);
        lineMat = (Material)EditorGUILayout.ObjectField("Materiel Dessin", lineMat, typeof(Material), false);
        isPainting = EditorGUILayout.Toggle("Paint", isPainting);

    }
    
    void OnSceneGUI(SceneView sceneView)
    {
        if(parent == null)
        {
            parent = (new GameObject()).transform;
            parent.name = "Rails";
            RailDraw railDraw = parent.gameObject.AddComponent<RailDraw>();
            railDraw.lineMaterial = lineMat;
            
        }

        if (isPainting)
        {
            Event e = Event.current;
            if (e.button == 0 && e.type == EventType.mouseDown)
            {
                Debug.Log(lastObject);
                Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                if (Physics.Raycast(r, out hit))
                {
                    if(hit.collider.tag == "Rail")
                    {
                        if(lastObject != null)
                        {
                            lastObject.GetComponent<RailBehavior>().isSelected = false;
                        }
                        lastObject = hit.collider.gameObject.transform;
                        lastObject.GetComponent<RailBehavior>().isSelected = true;
                    } else if (hit.collider.tag == "Ground")
                    {
                        GameObject go = (GameObject)Instantiate(rail, hit.point + Vector3.up, Quaternion.identity, parent);
                        Undo.RegisterCreatedObjectUndo(go, "Creation de rail");
                        go.tag = "Rail";
                        
                        if(lastObject == null)
                        {
                            lastObject = go.transform;
                            lastObject.GetComponent<RailBehavior>().isSelected = true;
                        } else
                        {
                            Undo.RecordObject(lastObject, "Creation de rail");
                            setIntersection(go);
                            lastObject.GetComponent<RailBehavior>().isSelected = false;
                            lastObject = go.transform;
                            lastObject.GetComponent<RailBehavior>().isSelected = true;
                        }
                    }
                }
            }
        } else
        {
            if(lastObject != null) 
                lastObject.GetComponent<RailBehavior>().isSelected = false;
            lastObject = null;
        }
    }

    void setIntersection(GameObject current)
    {
        RailBehavior script = lastObject.GetComponent<RailBehavior>();
        //Undo.RecordObject(script, "Creation de rail");
        if (script.nextRail == null) {
            script.nextRail = current.transform;
        } else if (script.thirdRail == null)
        {
            script.thirdRail = current.transform;
        } else 
        {
            if (script.fourthRail != null)
            {
                Undo.DestroyObjectImmediate(script.fourthRail.gameObject);
            }
            script.fourthRail = current.transform;
        }

        script = current.GetComponent<RailBehavior>();
        //Undo.RecordObject(script, "Creation de rail");
        script.previousRail = lastObject;
    }
    
}
