using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStones : MonoBehaviour {
    
    public float leftRightSpeed, upDownSpeed, turningSpeed;
    
    private List<StoneMovement> _leftRight;
    private List<StoneMovement> _upDown;
    private List<StoneMovement> _turning;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("LeftRight"))
        {
            foreach(StoneMovement stone in _leftRight)
            {
                stone.move(leftRightSpeed * Time.deltaTime,  (int)Input.GetAxisRaw("LeftRight"));
            }
        }
        if(Input.GetButton("UpDown"))
        {
            foreach (StoneMovement stone in _upDown)
            {
                stone.move(upDownSpeed * Time.deltaTime, (int) Input.GetAxisRaw("UpDown"));
            }
        }
        if(Input.GetButton("Turning"))
        {
            foreach (StoneMovement stone in _turning)
            {
                stone.transform.Rotate(Vector3.up, Input.GetAxisRaw("Turning")*turningSpeed * Time.deltaTime);
            }
        }
        
    }

    public List<StoneMovement> leftRight
    {
        set
        {
            _leftRight = value;
        }
    }
    public List<StoneMovement> upDown
    {
        set
        {
            _upDown = value;
        }
    }
    public List<StoneMovement> turning
    {
        set
        {
            _turning = value;
        }
    }
}
