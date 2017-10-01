using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public GameManager gameManager;
    public Canvas main, instructions;

    private bool inInstructions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Move")&&inInstructions)
        {
            gameManager.nextLevel();
        }
        else if(Input.GetButtonDown("Move"))
        {
            main.gameObject.SetActive(false);
            instructions.gameObject.SetActive(true);
            inInstructions = true;
        }
	}
}
