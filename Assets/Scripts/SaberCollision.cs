using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaberCollision : MonoBehaviour {

    public Text score;
    public Text combo;

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        if (target.tag == "FirstCut") // Si le target a été cassé dans le bon sens
        {
            if (GameManager.combo < 8)
            {
                GameManager.combo = GameManager.combo + 1;
            }
            GameManager.score += 1 * GameManager.combo;
        }
        else if (target.tag == "SecondCut") // Si le target n'a pas été cassé dans le bon sens
        {
            GameManager.combo = 1;
        }

        Destroy(target.transform.parent.gameObject); // Destruction du target

        // Mise à jour du score et du combo
        score.text = GameManager.score.ToString();
        combo.text = GameManager.combo.ToString();

        Debug.Log("Cible cassée (Score : " + GameManager.score + ", Combo : " + GameManager.combo + ")");
    }
}
