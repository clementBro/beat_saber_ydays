using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;

        Debug.Log("Collision détéctée");
        if (target.name == "Cube")
        {
            Destroy(target);
            Debug.Log("Le GameObject '" + target.name + "' a été détruit");
        }
    }
}
