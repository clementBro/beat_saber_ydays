using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Target")
        {
            Debug.Log("Joueur touché");
            Destroy(collision.gameObject);
            GameManager.combo = 0;
        }
    }
}
