using UnityEngine;

public class GameParam
{
    public static int music = 0;
    public static int gameType = 0;
    public static int rightWeapon = 0;
    public static int leftWeapon = 0;
  

    public static void ShowConf ()
    {
        Debug.Log ("music : "+ music);
        Debug.Log("gameType : " + gameType);
        Debug.Log("rightWeapon : " + rightWeapon);
        Debug.Log("leftWeapon : " + leftWeapon);
    }

}

