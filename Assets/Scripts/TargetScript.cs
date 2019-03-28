using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.targetStates.Add(this.gameObject, false);
        Debug.Log("Added GameObject : " + this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
