using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{

    public int BeatPerMinute;
    public bool hideSubGridAtStart;
    public int offsetDuree;
    
    /// <summary>
    /// La musique attachée au GameObject
    /// </summary>
    private AudioSource music;
    private const float CONSTANT = 0.01665f;

    // Use this for initialization
    void Start()
    {
        music = this.GetComponent(typeof(AudioSource)) as AudioSource;

        // Si on veut cacher la grille au démarrage du projet
        if (hideSubGridAtStart)
        {
            foreach (GameObject subGrid in GameObject.FindGameObjectsWithTag("SubGrid"))
            {
                Destroy(subGrid);
            }
        }

        // Si on veut appliquer un offset sur la durée (pour le déboguage)
        if (offsetDuree > 0)
        {
            // Fais démarrer la musique avec un retard
            music.Stop();
            music.time = offsetDuree;
            music.Play();

            // Le nombre de beats à passer
            int nbBeatsToSkip = (offsetDuree * BeatPerMinute) / 60;

            // On déplace la caméra pour garder le timing
            Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + nbBeatsToSkip);

            Camera.main.transform.position = cameraPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.back * Time.deltaTime * BeatPerMinute * CONSTANT);
    }
}
