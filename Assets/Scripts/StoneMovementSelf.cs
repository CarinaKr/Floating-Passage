using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovementSelf: MonoBehaviour {

    public Transform[] waypoints;
    public float speed, turningSpeed;
    public bool isTurning;
    public int turningDirection;

    private int _nextWaypoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(!isTurning)
        {
            transform.position=Vector3.MoveTowards(transform.position, waypoints[_nextWaypoint].position, speed * Time.deltaTime);
            if (transform.position == waypoints[_nextWaypoint].position)
            {
                _nextWaypoint = (_nextWaypoint + 1) % waypoints.Length;
            }
        }
        else
        {
            transform.Rotate(Vector3.up,  turningDirection*turningSpeed * Time.deltaTime);
        }
            
    }
}
