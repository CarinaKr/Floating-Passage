using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public Text infoText;
    public Image infoTextImage;
    public Text pointsText;
    public GameObject player;
    public PlayerMovement playerMovement;
    public MovingStones movingStones;
    public ExtraTime[] waypoints;
    public Transform[] cameraViews;
    private GameManager gameManager;
    public Camera mainCamera;
    public Transform[] ways;
    public StoneMovement w14Crossover;
    public int countdownTime;

    private bool _isPlaying;
    private int _currentPoints;
    private int _currentWay=3;
    private List<StoneMovement>[] _leftRight, _upDown, _turning;
    private List<GameObject>[] _time;
    private int _countdown;
    private IEnumerator _coroutine;
    private bool _coroutineIsRunning;
    private int[] _bestWayTimes;


    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _coroutine = Countdown();
        _countdown = countdownTime;
        _leftRight = new List<StoneMovement>[waypoints.Length-1];
        _upDown = new List<StoneMovement>[waypoints.Length-1];
        _turning = new List<StoneMovement>[waypoints.Length-1];
        _time = new List<GameObject>[waypoints.Length - 1];
        for (int i=0;i<ways.Length;i++)
        {
            _leftRight[i] = new List<StoneMovement>();
            _upDown[i] = new List<StoneMovement>();
            _turning[i] = new List<StoneMovement>();
            _time[i] = new List<GameObject>();
            foreach (Transform child in ways[i])
            {
                if(child.name=="LeftRight")
                {
                    _leftRight[i].Add(child.GetComponent<StoneMovement>());
                }
                else if (child.name == "UpDown" )
                {
                    _upDown[i].Add(child.GetComponent<StoneMovement>());
                }
                else if (child.name == "Cross" )
                {
                    _turning[i].Add(child.GetComponent<StoneMovement>());
                }
                else if (child.name == "Time")
                {
                    _time[i].Add(child.gameObject);
                }
            }
        }
        _leftRight[3].Add(w14Crossover);
        isPlaying = false;
        _bestWayTimes = gameManager.bestWayTimes;
        nextWay();
        restartWay();
       
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Move") && _isPlaying == false && _coroutineIsRunning==false)
        {
            restartWay();
            //isPlaying = true;
        }

    }

    public void reachCheckpoint(int pNumber)
    {
        if (pNumber == _currentWay)
        {
            isPlaying = false;
            nextWay();
        }
    }

    public void restartWay()
    {
        player.transform.position = waypoints[_currentWay-1].transform.position;
        player.transform.rotation = waypoints[_currentWay-1].transform.rotation;
        mainCamera.transform.position = cameraViews[_currentWay-1].position;
        mainCamera.transform.rotation = cameraViews[_currentWay-1].rotation;
        playerMovement.timeLeft = waypoints[_currentWay-1].extraTime;
        foreach(GameObject time in _time[_currentWay-1])
        {time.SetActive(true);}
        StartCoroutine("Countdown");
    }

    public void nextWay()
    {
        if (_currentWay < waypoints.Length - 1)
        {
            if (_currentWay > 0)
            { gameManager.checkHighscore(_currentWay - 1, playerMovement.timeLeft); }
            infoText.text = "You reached your next checkpoint! \n Press Move to continue.";
            _currentWay++;
            pointsText.text = "Highscore: " + _bestWayTimes[_currentWay-1];
            movingStones.leftRight = _leftRight[_currentWay-1];
            movingStones.upDown = _upDown[_currentWay-1];
            movingStones.turning = _turning[_currentWay-1];
        }
        else
        {
            if (_currentWay > 0)
            { gameManager.checkHighscore(_currentWay - 1, playerMovement.timeLeft); }
            gameManager.nextLevel();
        }
    }

    public void addPoints(int pPoints)
    {
        _currentPoints += pPoints;
    }

    public bool isPlaying
    {
        get
        {
            return _isPlaying;
        }
        set
        {
            _isPlaying = value;
            if (_isPlaying == false)
            {
                playerMovement.enabled = false;
                movingStones.enabled = false;
                infoText.enabled = true;
                infoTextImage.enabled = true;
            }
            else
            {
                playerMovement.enabled = true;
                movingStones.enabled = true;
                infoText.enabled = false;
                infoTextImage.enabled = false;
            }
        }
    }

    public int currentWay
    {
        get
        {
            return _currentWay;
        }
        set
        {
            _currentWay = value;
        }
    }

    IEnumerator Countdown()
    {
        playerMovement.animator.SetBool("isJumping", true);
        _coroutineIsRunning = true;
        infoTextImage.enabled = false;
        infoText.fontSize *= 2;
        while (true)
        {
            infoText.text = "" + _countdown;
            yield return new WaitForSeconds(1);
            _countdown--;
            if (_countdown < 0)
            {
                _countdown = countdownTime;
                isPlaying = true;
                infoText.fontSize /= 2;
                _coroutineIsRunning = false;
                playerMovement.animator.SetBool("isJumping", false);
                StopCoroutine("Countdown");
            }
        }
        

    }
}
