using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SaberCollision : MonoBehaviour {
    public GameObject cibleToucheBonSens;
    public GameObject cibleToucheMauvaisSens;

    public Text score;
    public Text combo;

    public int comparateurSabre;

    public AudioClip coupSuccesUn;
    public AudioClip coupEchecUn;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;
        comparateurSabre++;

        if (target.tag == "FirstCut") // Si le target a été cassé dans le bon sens
        {

                audioSource.clip = coupSuccesUn;
                audioSource.Play();
            if (cibleToucheMauvaisSens.activeSelf == false) {
                cibleToucheBonSens.SetActive(true);
            }
                
                StartCoroutine(WaitToDeactivateCube());


            GameManager.score += 1 * GameManager.combo;
            if (GameManager.combo < 8)
            {
                GameManager.combo++;
            }
            Debug.Log("Bon sens");
        }
        else if (target.tag == "SecondCut") // Si le target n'a pas été cassé dans le bon sens
        {
            GameManager.combo = 1;
            Debug.Log("Mauvais sens");

                audioSource.clip = coupEchecUn;
                audioSource.Play();
            if (cibleToucheBonSens.activeSelf == false)
            {
                cibleToucheMauvaisSens.SetActive(true);
            }
                
                StartCoroutine(WaitToDeactivateCube());
        }

        Destroy(target.transform.parent.gameObject); // Destruction du target

        // Mise à jour du score et du combo
        score.text = GameManager.score.ToString();
        combo.text = GameManager.combo.ToString();

        Debug.Log("Cible cassée (Score : " + GameManager.score + ", Combo : " + GameManager.combo + ")");
    }


    private IEnumerator WaitToDeactivateCube()
    {
        yield return new WaitForSeconds(0.1f);
        cibleToucheBonSens.SetActive(false);
        cibleToucheMauvaisSens.SetActive(false);
    }
}
