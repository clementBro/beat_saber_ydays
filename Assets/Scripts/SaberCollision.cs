using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberCollision : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        if (target.tag == "Target")
        {
            Destroy(target);
            Debug.Log("Le GameObject '" + target.tag + "' a été détruit");

            if (GameManager.combo < 8)
            {
                GameManager.combo = GameManager.combo + 1;
            }

            GameManager.score += 1 * GameManager.combo;
        }
    }
}
