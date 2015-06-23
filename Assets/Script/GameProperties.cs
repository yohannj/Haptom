using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


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
    HashSet<int> listLevelSucceed = new HashSet<int>();

    string molecule_name;
    string molecule_hint;
    List<string> molecule_atoms;
    string explications;
    string mass_molar;
    string density;

    public void AddLevelSucceeded()
    {
        listLevelSucceed.Add(LevelId);
    }

    public List<int> getListSuccess()
    {
        List<int> res = new List<int>(listLevelSucceed);
        res.Sort();
        return res;
    }

    public void setLevel(int i)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "\\ressources\\haptom.tsv");
        Debug.Log(path);
        string[] lines = File.ReadAllLines(path);
        string[] data = lines[i].Split('\t');
        molecule_name = data[0];
        molecule_hint = data[1];
        molecule_atoms = new List<string>(data[2].Split('-'));
        explications = data[3];
        mass_molar = data[4];
        density = data[5];
        LevelId = i;
    }

    public int getLevel()
    {
        return LevelId;
    }

    public string getMoleculeName()
    {
        return molecule_name;
    }

    public string getMoleculeHint()
    {
        return molecule_hint;
    }

    public List<string> getMoleculeAtoms()
    {
        return molecule_atoms;
    }

    public string getExplications()
    {
        return explications;
    }

    public string getMassMolar()
    {
        return mass_molar;
    }

    public string getDensity()
    {
        return density;
    }

}
