using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Assets.Scripts.Entities;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


public class SaberCollision : MonoBehaviour {

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

        } else if (target.tag == "Rouge")
        {
            GameManager.combo = 1;
        }

        Destroy(target.transform.parent.gameObject);

        Debug.Log("Score : " + GameManager.score + " ,Combo : " + GameManager.combo);

        score.text = GameManager.score.ToString();
        combo.text = GameManager.combo.ToString();

        
        if (exportToJson)
        {
            CreateJson();
        }
    }

    private void CreateJson()
    {
        music = this.GetComponent(typeof(AudioSource)) as AudioSource;
        DirectoryInfo jsonDirectory = new DirectoryInfo("Assets/JSON/JsonScore");
        List<FileInfo> jsonFiles = jsonDirectory.GetFiles().ToList();
        // Si le json existe déjà on le supprime
        if (jsonFiles.Any(file => file.Name == music.name + ".json"))
        {
            jsonFiles.Where(file => file.Name == music.name + ".json").First().Delete();
        }
        // On crée le répertoire
        FileStream fs = File.Create(jsonDirectory + "/" + music.name + ".json");

        JSONHighScore dataToSave = new JSONHighScore();
        dataToSave.playerName = "test";
        dataToSave.musicName = music.name;
        dataToSave.highScore = GameManager.score.ToString();

        string jsonMusic = JsonConvert.SerializeObject(dataToSave);


        // Sauvegarde du json dans le fichier
        UnicodeEncoding uniEncoding = new UnicodeEncoding();
        fs.Write(uniEncoding.GetBytes(jsonMusic), 0, uniEncoding.GetByteCount(jsonMusic));
        fs.Close();
        exportToJson = false;
    }
}
