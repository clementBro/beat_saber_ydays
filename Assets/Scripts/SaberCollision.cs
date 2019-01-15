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

        if (target.tag == "Target")
        {
            Destroy(target.transform.parent);
            Debug.Log("Le GameObject '" + target.tag + "' a été détruit");

            if (GameManager.combo < 8)
            {
                GameManager.combo = GameManager.combo + 1;
            }

            GameManager.score += 1 * GameManager.combo;
            Debug.Log("Score : " + GameManager.score + " ,Combo : " + GameManager.combo);

            score.text = GameManager.score.ToString();
            combo.text = GameManager.combo.ToString();
        }
    }
}
