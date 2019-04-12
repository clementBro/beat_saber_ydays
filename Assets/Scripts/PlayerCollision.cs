using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {

    public Text combo;
    public GameObject gameManager;
    public GameObject indicateurPlayerFail;

    public Image missedTargetImage;
    public float missedTargetTime = 1f;
    public AudioClip missedTargetSong;
    public string targetTag = "Target";

    public AudioSource audioSource;

    // Use this for initialization
    void Start () {
        
    }
	
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

        }

        if(target.tag == targetTag)
        {
            indicateurPlayerFail.SetActive(true);
            WaitToDeactivateCube();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.tag == targetTag)
        {
            indicateurPlayerFail.SetActive(true);
            StartCoroutine(WaitToDeactivateCube());
            
        }
    }

    private IEnumerator WaitToDeactivateCube()
    {
        yield return new WaitForSeconds(0.1f);
        indicateurPlayerFail.SetActive(false);
    }

}
