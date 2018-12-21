using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberCollision : MonoBehaviour {

    private GameManager gameManager = new GameManager();

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        if (target.tag == "Target")
        {
            Destroy(target);
            Debug.Log("Le GameObject '" + target.tag + "' a été détruit");

            if (gameManager.Combo < 8)
            {
                gameManager.Combo = gameManager.Combo + 1;
            }

            gameManager.Score += 1 * gameManager.Combo;
        }
    }
}
