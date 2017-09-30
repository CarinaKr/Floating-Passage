using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public Transform start,end;
    public float speed;
    //public float time;
    public Text timeText;
    //public GameManager gameManager;
    public Animator animator;

    private Rigidbody rigBody;
    private bool isWalking=true;
    private float _timeLeft;

	// Use this for initialization
	void Start () {
        rigBody = GetComponent<Rigidbody>();
        
        //_timeLeft = time;
        timeText.text="Time left: "+ _timeLeft.ToString("F0");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Move"))
        {
            isWalking = false;
            _timeLeft -= Time.deltaTime;
        }
        else if(Input.GetButtonUp("Move"))
        {
            isWalking = true;
        }

        if(_timeLeft<=0)
        {
            _timeLeft = 0;
            isWalking = true;
        }

        if (isWalking)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime,Space.World);
        }

        timeText.text = "Time left: " + _timeLeft.ToString("F0");
    }

    public float timeLeft
    {
        get
        {
            return _timeLeft;
        }
        set
        {
            _timeLeft = value;
            timeText.text = "Time left: " + _timeLeft.ToString("F0");
        }
    }
    
}
