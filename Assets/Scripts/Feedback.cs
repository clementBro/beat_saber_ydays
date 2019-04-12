using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Feedback : MonoBehaviour {

    public AudioClip coupSuccesUn;
    public AudioClip coupSuccesDeux;
    public AudioClip coupEchecUn;
    public AudioClip coupEchecDeux;

    private AudioSource audioSource;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "poign_1_polySurface2_Contour")
        {
            int chiffreRandom = UnityEngine.Random.Range(1, 4);
            chiffreRandom = UnityEngine.Random.Range(1, 4);
            if (chiffreRandom == 1)
            {
                audioSource.clip = coupSuccesUn;
                audioSource.Play();
                Debug.Log("chiffreRandomSucces=" + chiffreRandom);

            }
            if (chiffreRandom == 2)
            {
                audioSource.clip = coupSuccesUn;
                audioSource.Play();
                Debug.Log("chiffreRandomSucces=" + chiffreRandom);
            }

           
        }
    }
    void Start () {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update () {
		
	}
    
}
