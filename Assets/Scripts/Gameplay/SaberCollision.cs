using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SaberCollision : MonoBehaviour {


    public Text score;
    public Text combo;

    //string chemin, jsonString;
    //bool test = true;

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

        /*
        if (test)
        {
            chemin = Application.dataPath + "/JSON/JsonHighScore.json";
            jsonString = File.ReadAllText(chemin);

            JSONHighScore myObject = JsonUtility.FromJson<JSONHighScore>(jsonString);
            Debug.Log("test : " + myObject.musicName);

            myObject.musicName = "test";
            myObject.playerName = "test";
            myObject.highScore = GameManager.score.ToString();

            string json = JsonUtility.ToJson(myObject);
            File.WriteAllText(chemin, jsonString);
            Debug.Log("test : " + myObject.highScore);
            Debug.Log("test : " + myObject.musicName);
            test = false;
        }*/

    }
}
