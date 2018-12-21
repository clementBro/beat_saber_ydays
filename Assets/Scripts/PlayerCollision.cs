using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    
    private GameManager gameManager = new GameManager();

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
            gameManager.Combo = 0;
        }
    }
}
