using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int levelAmount;
   
    
    private int currentLevel = 0;
    private LevelManager levelManager;
    private int _wayNum = 4;    //change this when playing with multiple levels!! (get number of Ways from Level Manager)
    private int[] _bestWayTimes=new int[4];
    private int[] _wayTimes = new int[4];
   

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);
        //PlayerPrefs.DeleteAll();
        for(int i=0;i<_wayNum;i++)
        {
            if(PlayerPrefs.HasKey("Way"+i))
            {
                _bestWayTimes[i] = PlayerPrefs.GetInt("Way"+i);
            }
            else
            {
                _bestWayTimes[i] = 0;
                PlayerPrefs.SetInt("Way" + i, _bestWayTimes[i]);
            }
            _wayTimes[i] = 0;
        }
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void nextLevel()
    {
        if (currentLevel < levelAmount+1)
        {
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
        }
    }
    public void restartGame()
    {
        currentLevel = 0;
        SceneManager.LoadScene(currentLevel);
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (currentLevel > 0 && currentLevel <= levelAmount)
        {
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.currentWay = 0;
        }
    }

    public void checkHighscore(int pWay, float pTime)
    {
        _wayTimes[pWay] = Mathf.RoundToInt(pTime);
        if (pTime > _bestWayTimes[pWay])
        {
            _bestWayTimes[pWay] = Mathf.RoundToInt(pTime);
        }
    }

    
    public int[] bestWayTimes
    {
        get
        {
            return _bestWayTimes;
        }
    }
    public int[] wayTimes
    {
        get
        {
            return _wayTimes;
        }
    }
}
