using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

    Scene final;

    private void Awake()
    {
        final = SceneManager.GetSceneByName("Final");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.inputString == "\n" || Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadSceneAsync("Final", LoadSceneMode.Single);
        }
	}
}
