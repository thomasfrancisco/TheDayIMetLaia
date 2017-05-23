using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestSequence : MonoBehaviour {
    private Transform[] puzzles;
    private Puzzle2Script [] puzScripts;
    private int currentLevel;


	// Use this for initialization
	void Awake () {
        puzzles = new Transform[transform.childCount+1];
        puzScripts = new Puzzle2Script[transform.childCount+1];
        currentLevel = 0;
		for(int i = 0; i < transform.childCount; i++)
        {
            puzzles[i] = transform.GetChild(i);
            puzScripts[i] = puzzles[i].GetChild(0).GetComponent<Puzzle2Script>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (currentLevel < transform.childCount)
        {
            if (puzScripts[currentLevel].unlocked)
            {
                nextLevel();
            }
        }
	}

    void nextLevel()
    {
        puzzles[currentLevel].gameObject.SetActive(false);
        currentLevel++;
        if(currentLevel < transform.childCount)
            puzzles[currentLevel].gameObject.SetActive(true);
    }

}
