using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberCollision : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        if (target.name == "Cube")
        {
            Destroy(target);
            Debug.Log("Le GameObject '" + target.name + "' a été détruit");
        }
    }
}
