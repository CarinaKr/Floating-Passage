using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour {

    public Transform[] waypoints;
    public int startDirection=1;
    
    private float _speed;

	// Use this for initialization
	void Start () {
        //transform.position = waypoints[0].position;
	}
    
	
	public void move(float speed, int direction)
    {
        if(direction*startDirection>0)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
        if(Vector3.Distance(transform.position,waypoints[direction].position)>0.1)
        {
            transform.position=Vector3.MoveTowards(transform.position, waypoints[direction].position, speed);
        }
    }
    
}
