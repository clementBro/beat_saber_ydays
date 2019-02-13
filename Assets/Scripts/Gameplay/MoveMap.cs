using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Entities;

public class MoveMap : MonoBehaviour
{

    public int BeatPerMinute;
    public bool hideSubGridAtStart;
    public int offsetDuree;
    public bool exportToJson;

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

        // si on veut transformer la musique en fichier Json
        if (exportToJson)
        {
            CreateJson();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.back * Time.deltaTime * BeatPerMinute * CONSTANT);
    }

    /// <summary>
    /// Transforme la musique en fichier Json
    /// </summary>
    private void CreateJson()
    {
        // On récupère les cibles
        List<GameObject> targets = GameObject.FindGameObjectsWithTag("Target").ToList();
        targets = targets.OrderBy(note => note.transform.position.z).ToList();

        //Scene.name;
        DirectoryInfo jsonDirectory = new DirectoryInfo("Assets/JSON/JsonMusic");

        Scene scene = SceneManager.GetActiveScene();

        List<FileInfo> jsonFiles = jsonDirectory.GetFiles().ToList();

        // Si le json existe déjà on le supprime
        if (jsonFiles.Any(file => file.Name == scene.name + ".json"))
        {
            jsonFiles.Where(file => file.Name == scene.name + ".json").First().Delete();
        }

        // On crée le répertoire
        FileStream fs = File.Create(jsonDirectory + "/" + scene.name + ".json");

        JsonEntities.DataToSave dataToSave = new JsonEntities.DataToSave();
        dataToSave.NotesToSave = new List<JsonEntities.NoteToSave>();
        JsonEntities.NoteToSave noteToSave;

        dataToSave.BPM = BeatPerMinute;

        // On enregistre les infos souhaitées de toutes les cibles
        foreach (GameObject target in targets)
        {
            noteToSave = new JsonEntities.NoteToSave();

            noteToSave.PositionX = target.transform.position.x;
            noteToSave.PositionY = target.transform.position.y;
            noteToSave.PositionZ = target.transform.position.z;

            noteToSave.RotationZ = target.transform.rotation.eulerAngles.z;

            dataToSave.NotesToSave.Add(noteToSave);
        }

        // Transformation de l'objet en Json
        string jsonMusic = JsonConvert.SerializeObject(dataToSave);

        // Sauvegarde du json dans le fichier
        UnicodeEncoding uniEncoding = new UnicodeEncoding();
        fs.Write(uniEncoding.GetBytes(jsonMusic), 0, uniEncoding.GetByteCount(jsonMusic));
        fs.Close();
    }
}
