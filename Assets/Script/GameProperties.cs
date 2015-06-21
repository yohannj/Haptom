using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameProperties : MonoBehaviour {

    private static GameProperties _instance;

    public static GameProperties instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameProperties>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    int LevelId;
    List<int> listLevelSucceed=new List<int>();

    public void AddLevelSucceeded(int i)
    {
        listLevelSucceed.Add(i);
    }

    public List<int> getListSuccess()
    {
        return listLevelSucceed;
    }

    public void setLevel(int i)
    {
        LevelId = i;
    }

    public int getLevel()
    {
        return LevelId;
    }
}
