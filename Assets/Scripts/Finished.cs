using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finished : MonoBehaviour {

    private GameManager gameManager;
    public Text myTime,bestTime;

    private int[] _bestWayTimes, _wayTimes;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _bestWayTimes = gameManager.bestWayTimes;
        _wayTimes = gameManager.wayTimes;
        myTime.text = "";
        bestTime.text = "";
        for(int i=0;i<_wayTimes.Length;i++)
        {
            myTime.text += "Way " + (i+1) + ": " + _wayTimes[i]+"\n";
            //if(_bestWayTimes[i]>PlayerPrefs.GetInt("Way"+i))
            //{
            //    bestTime.color = Color.yellow;
            //}
            bestTime.text += "Way " + (i+1) + ": " + _bestWayTimes[i] + "\n";
            //bestTime.color = Color.black;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Move"))
        {
            gameManager.restartGame();
            for(int i=0;i< gameManager.bestWayTimes.Length;i++)
            {
                PlayerPrefs.SetInt("Way" + i, gameManager.bestWayTimes[i]);
            }
        }
    }
}
