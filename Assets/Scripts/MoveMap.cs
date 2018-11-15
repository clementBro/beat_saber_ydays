using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{

    public int BeatPerMinute;
    public bool hideSubGridAtStart;

    private const float CONSTANT = 0.01665f;

    // Use this for initialization
    void Start()
    {
        // Si on veut cacher la grille au démarrage du projet
        if (hideSubGridAtStart)
        {
            foreach (GameObject subGrid in GameObject.FindGameObjectsWithTag("SubGrid"))
            {
                Destroy(subGrid);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.back * Time.deltaTime * BeatPerMinute * CONSTANT);
    }
}
