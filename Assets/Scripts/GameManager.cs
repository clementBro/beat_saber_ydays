using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private long score = 0;
    public long Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    private int combo = 0;
    public int Combo
    {
        get
        {
            return combo;
        }
        set
        {
            combo = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
