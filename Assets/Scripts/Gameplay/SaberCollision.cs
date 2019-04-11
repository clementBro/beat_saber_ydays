using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Entities;
using System.Text;
using Newtonsoft.Json;

public class SaberCollision : MonoBehaviour
{

    public Text score;
    public Text combo;

    private bool exportToJson = true;
    private AudioSource music;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollision : " + collision);
        var target = collision.gameObject;

        if (target.tag == "Vert")
        {
            Debug.Log("Le GameObject '" + target.tag + "' a été détruit");

            if (GameManager.combo < 8)
            {
                GameManager.combo = GameManager.combo + 1;
            }
            GameManager.score += 1 * GameManager.combo;

        }
        else if (target.tag == "Rouge")
        {
            GameManager.combo = 1;
        }

        Destroy(target.transform.parent.gameObject);

        Debug.Log("Score : " + GameManager.score + " ,Combo : " + GameManager.combo);

        score.text = GameManager.score.ToString();
        combo.text = GameManager.combo.ToString();

        importJson();

        if (exportToJson)
        {
            CreateJson();
        }
    }

    private void importJson()
    {
        DirectoryInfo jsonDirectory = new DirectoryInfo("Assets/JSON/JsonScore");

        // Si on ne trouve pas la musique
        if (!File.Exists(jsonDirectory + "/test.json"))
        {
            Debug.Log("La musique '" + "music" + "' n'a pas été trouvée.");
        } else {
            // Récupère le json de la musique
            string fileData = ReadFileToString(jsonDirectory + "/test.json");

            // Récupère les infos de la musique
            JSONHighScore.SaveData dataToLoad = JsonConvert.DeserializeObject<JSONHighScore.SaveData>(fileData);
            Debug.Log("taille tableau / " + dataToLoad.SaveInfoHighScore.Count);
        }
    }

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

    private void CreateJson()
    {
        music = this.GetComponent(typeof(AudioSource)) as AudioSource;
        DirectoryInfo jsonDirectory = new DirectoryInfo("Assets/JSON/JsonScore");
        List<FileInfo> jsonFiles = jsonDirectory.GetFiles().ToList();
        // Si le json existe déjà on le supprime
        if (jsonFiles.Any(file => file.Name == "test.json"))
        {
            jsonFiles.Where(file => file.Name == "test.json").First().Delete();
        }
        // On crée le répertoire
        FileStream fs = File.Create(jsonDirectory + "/test.json");

        //JSONHighScore dataToSave = new JSONHighScore();
        /*dataToSave.playerName = "Player";
        dataToSave.musicName = "Musique";
        dataToSave.highScore = GameManager.score.ToString();*/

        JSONHighScore.SaveData saveData = new JSONHighScore.SaveData();
        saveData.SaveInfoHighScore = new List<JSONHighScore.InfoHighScore>();

        JSONHighScore.InfoHighScore infoHighScore = new JSONHighScore.InfoHighScore(); ;

        infoHighScore.playerName = "Player";
        infoHighScore.musicName = "Musique";
        infoHighScore.highScore = GameManager.score.ToString();

        saveData.SaveInfoHighScore.Add(infoHighScore);


        infoHighScore.playerName = "test";
        infoHighScore.musicName = "test";
        infoHighScore.highScore = GameManager.score.ToString();

        saveData.SaveInfoHighScore.Add(infoHighScore);

        // Transformation de l'objet en Json
        string jsonMusic = JsonConvert.SerializeObject(saveData);

        Debug.Log("save data " + jsonMusic);

        // Sauvegarde du json dans le fichier
        UnicodeEncoding uniEncoding = new UnicodeEncoding();
        fs.Write(uniEncoding.GetBytes(jsonMusic), 0, uniEncoding.GetByteCount(jsonMusic));        

        fs.Close();
        exportToJson = false;
    }
}
