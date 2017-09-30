using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {
    
    public LevelManager levelManager;
    public Text infoText;

    private int _pointsPerBlock = 5;
    private string _standingOn;

	void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Time")
        {
            GetComponent<PlayerMovement>().timeLeft += other.GetComponent<ExtraTime>().extraTime;
            other.gameObject.SetActive(false);
        }
        if(other.transform.tag=="Checkpoint")
        {
            levelManager.reachCheckpoint(other.GetComponent<ExtraTime>().checkpointNumber);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "MovingBlock")
        {
            transform.parent = other.transform.parent;
            _standingOn = other.transform.tag;
            levelManager.addPoints(_pointsPerBlock);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MovingBlock"  && transform.parent == other.transform.parent)
        {
            transform.parent = null;
        }


    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag=="Floor")
        {
            infoText.text = "You have no way out! \n Press Move to Respawn.";
            levelManager.isPlaying = false;
        }
    }
}
