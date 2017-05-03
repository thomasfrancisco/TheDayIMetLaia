﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class windowsRailCreationV2 : EditorWindow {

    private Object rail;
    private bool isPainting;

    private Transform parent;
    private RaycastHit hit;

    private Transform lastObject;
    private Transform beforeLast; // Pour annuler un placement

    [MenuItem("Window/The Day I Met Laia/Creation de Rail V2")]
    public static void CreateWindow()
    {
        windowsRailCreationV2 window = EditorWindow.GetWindow<windowsRailCreationV2>();
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
        isPainting = EditorGUILayout.Toggle("Paint", isPainting);
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if(parent == null)
        {
            parent = (new GameObject()).transform;
            parent.name = "Rails";
        }

        if (isPainting)
        {
            Event e = Event.current;
            if(e.button == 0 && e.type == EventType.mouseDown)
            {
                Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                if(Physics.Raycast(r, out hit)){
                    if(hit.collider.tag == "Rail")
                    {
                        if(lastObject != null)
                        {
                            lastObject.GetComponent<RailScriptV2>().isSelected = false;
                        }
                        lastObject = hit.collider.gameObject.transform;
                        lastObject.GetComponent<RailScriptV2>().isSelected = true;
                    } else if(hit.collider.tag == "Ground")
                    {
                        GameObject go = (GameObject)Instantiate(rail, hit.point + Vector3.up, Quaternion.identity, parent);
                        Undo.RegisterCreatedObjectUndo(go, "Creation de rail");
                        go.tag = "Rail";
                        if(lastObject == null)
                        {
                            lastObject = go.transform;
                            lastObject.GetComponent<RailScriptV2>().isSelected = true;
                        } else
                        {
                            Undo.RecordObject(lastObject, "Creation de rail");
                            setIntersection(go);
                            lastObject.GetComponent<RailScriptV2>().isSelected = false;
                            lastObject = go.transform;
                            lastObject.GetComponent<RailScriptV2>().isSelected = true;
                        }
                    }
                }
            }
        } else
        {
            if(lastObject != null)
            {
                lastObject.GetComponent<RailScriptV2>().isSelected = false;
            }
            lastObject = null;
        }
    }

    private void setIntersection(GameObject current)
    {
        RailScriptV2 script = lastObject.GetComponent<RailScriptV2>();
        if (script.nextRail == null)
        {
            script.nextRail = current.transform;
        }
        else if (script.thirdRail == null)
        {
            script.thirdRail = current.transform;
        }
        else
        {
            if (script.fourthRail != null)
            {
                Undo.DestroyObjectImmediate(script.fourthRail.gameObject);
            }
            script.fourthRail = current.transform;
        }

        script = current.GetComponent<RailScriptV2>();
        script.previousRail = lastObject;
    }
}
