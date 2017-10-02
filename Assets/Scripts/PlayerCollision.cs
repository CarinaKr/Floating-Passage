using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {
    
    public LevelManager levelManager;
    public Text infoText;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private int _pointsPerBlock = 5;
    private string _standingOn;

    //void Update()
    //{
    //    if(transform.position.y<-100)
    //    {
    //        audioSource.clip = audioClips[0];
    //        audioSource.Play();
    //        StartCoroutine("StopPlaying");
    //    }
    //}

	void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Time")
        {
            audioSource.clip = audioClips[1];
            audioSource.volume = 0.5f;
            audioSource.Play();
            audioSource.volume = 1f;
            GetComponent<PlayerMovement>().timeLeft += other.GetComponent<ExtraTime>().extraTime;
            other.gameObject.SetActive(false);
        }
        if(other.transform.tag=="Checkpoint")
        {
            levelManager.reachCheckpoint(other.GetComponent<ExtraTime>().checkpointNumber);
        }
        if (other.transform.tag=="Floor")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            StartCoroutine("StopPlaying");
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

    IEnumerator StopPlaying()
    {
        yield return new WaitForSeconds(0.5f);
        infoText.text = "You fall into the void! \n Press Move to Respawn.";
        levelManager.isPlaying = false;
    }
}
