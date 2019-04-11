using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Entities;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;

public class SaberCollision : MonoBehaviour
{

    public Text score;
    public Text combo;

    private AudioSource music;
    private JSONHighScore.SaveData dataScore = new JSONHighScore.SaveData();

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

        CreateJson();
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
            dataScore = JsonConvert.DeserializeObject<JSONHighScore.SaveData>(fileData);
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

        JSONHighScore.InfoHighScore infoHighScore = new JSONHighScore.InfoHighScore(); ;

        infoHighScore.playerName = "Player";
        infoHighScore.musicName = "Musique";
        infoHighScore.highScore = GameManager.score;

        if (dataScore.SaveInfoHighScore != null)
        {
            if (dataScore.SaveInfoHighScore.Count == 10)
            {
                if (dataScore.SaveInfoHighScore[0].highScore < (infoHighScore.highScore))
                {
                    dataScore.SaveInfoHighScore[0] = infoHighScore;
                }
            }
            else
            {
                dataScore.SaveInfoHighScore.Add(infoHighScore);
            }
        }
        else
        {
            dataScore.SaveInfoHighScore = new List<JSONHighScore.InfoHighScore>();
            dataScore.SaveInfoHighScore.Add(infoHighScore);
        }   

        if (File.Exists(jsonDirectory + "/test.json"))
        {
            FileUtil.DeleteFileOrDirectory(jsonDirectory + "/test.json");
        }


        // tri par ordre croisssant des scores
        int n = dataScore.SaveInfoHighScore.Count - 1;
        for (int i = n; i >= 1; i--)
        {
            for (int j = 1; j <= i; j++)
            {
                if (dataScore.SaveInfoHighScore[j - 1].highScore > dataScore.SaveInfoHighScore[j].highScore)
                {
                    var temp = dataScore.SaveInfoHighScore[j - 1];
                    dataScore.SaveInfoHighScore[j - 1] = dataScore.SaveInfoHighScore[j];
                    dataScore.SaveInfoHighScore[j] = temp;
                }
            }
        }


        // Transformation de l'objet en Json
        string jsonMusic = JsonConvert.SerializeObject(dataScore);

        Debug.Log("save data " + jsonMusic);

        // Sauvegarde du json dans le fichier
        UnicodeEncoding uniEncoding = new UnicodeEncoding();
        fs.Write(uniEncoding.GetBytes(jsonMusic), 0, uniEncoding.GetByteCount(jsonMusic));        

        fs.Close();
    }
}
