using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        Debug.Log("Collision détéctée");
        if (target.name == "Cube")
        {
            Destroy(target);
            Debug.Log("Le GameObject '" + target.name + "' a été détruit");
        }
    }
}
