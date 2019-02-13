using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {

    
    public Text combo;
    
    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        if (target.tag == "FirstCut" || target.tag == "SecondCut")
        {
            Destroy(target.transform.parent.gameObject); // Destruction du target

            // Mise à jour du combo
            GameManager.combo = 1;
            combo.text = GameManager.combo.ToString();

            Debug.Log("Joueur touché");
        }
    }
}
