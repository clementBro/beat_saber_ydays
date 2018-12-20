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
        }
    }
}
