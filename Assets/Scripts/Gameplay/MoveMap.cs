using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Entities;
using System;

public class MoveMap : MonoBehaviour
{

    public int BeatPerMinute;
    public bool hideSubGridAtStart;
    public int offsetDuree;
    public bool exportToJson;

    public bool loadMusicFromJson;
    public string musicName;

    public GameObject prefabNote;

    /// <summary>
    /// La musique attachée au GameObject
    /// </summary>
    private AudioSource music;
    private const float CONSTANT = 0.01665f;
    private const float CONSTANTMOVE = 5f;
    private const float CONSTANTPOSNOTE = 0.0134f;

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

        // Si on veut charger une musique
        if (loadMusicFromJson)
        {
            LoadMusicFromJson(musicName);
        }
        // si on veut transformer la musique en fichier Json
        else if (exportToJson)
        {
            CreateJson();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loadMusicFromJson)
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * CONSTANTMOVE);
        }
        else
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * BeatPerMinute * CONSTANT);
        }
    }

    /// <summary>
    /// Transforme la musique en fichier Json
    /// </summary>
    private void CreateJson()
    {
        // On récupère les cibles
        List<GameObject> targets = GameObject.FindGameObjectsWithTag("Target").ToList();
        targets = targets.OrderBy(note => note.transform.position.z).ToList();

        DirectoryInfo jsonDirectory = new DirectoryInfo("Assets/JSON/JsonMusic");
        Scene scene = SceneManager.GetActiveScene();
        List<FileInfo> jsonFiles = jsonDirectory.GetFiles().ToList();

        // Si le json existe déjà on le supprime
        if (jsonFiles.Any(file => file.Name == musicName + ".json"))
        {
            jsonFiles.Where(file => file.Name == musicName + ".json").First().Delete();
        }

        // On crée le répertoire
        FileStream fs = File.Create(jsonDirectory + "/" + musicName + ".json");

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

    /// <summary>
    /// Charge une musique à partir d'un Json
    /// </summary>
    private void LoadMusicFromJson(string music)
    {
        DirectoryInfo jsonDirectory = new DirectoryInfo("Assets/JSON/JsonMusic");

        // Si on ne trouve pas la musique
        if (!File.Exists(jsonDirectory + "/" + music + ".json"))
        {
            Debug.Log("La musique '" + music + "' n'a pas été trouvée.");
            return;
        }

        // Récupère le json de la musique
        string fileData = ReadFileToString(jsonDirectory + "/" + music + ".json");

        // Récupère les infos de la musique
        JsonEntities.DataToSave dataToLoad = JsonConvert.DeserializeObject<JsonEntities.DataToSave>(fileData);

        // AJOUT DES NOTES SUR LA PISTE
        foreach(var note in dataToLoad.NotesToSave)
        {
            Instantiate(prefabNote, new Vector3(note.PositionX, note.PositionY, note.PositionZ * dataToLoad.BPM * CONSTANTPOSNOTE), new Quaternion(0, 0, note.RotationZ, 0), this.transform);
        }
    }

    /// <summary>
    /// Lit un fichier et retourne son contenue en string
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private string ReadFileToString(string file)
    {
        using (FileStream fsSource = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            // Read the source file into a byte array.
            byte[] bytes = new byte[fsSource.Length];
            int numBytesToRead = (int)fsSource.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = bytes.Length;


            UnicodeEncoding uniEncoding = new UnicodeEncoding();


            return uniEncoding.GetString(bytes);
        }
    }
}
