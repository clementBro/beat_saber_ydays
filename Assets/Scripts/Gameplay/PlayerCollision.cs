using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {

    
    public Text combo;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Joueur touché");
            Destroy(collision.gameObject);

            GameManager.combo = 0;
            combo.text = "1";
        }
        
    }
}
