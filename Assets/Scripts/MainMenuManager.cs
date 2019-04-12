using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button buttonPlay, buttonChooseWeapon, buttonEasy, buttonMedium, buttonHard, buttonMusic;

    // Use this for initialization

    void Start()
    {
        //buttonMusic.onClick.AddListener(ClickMusicAction);
        //buttonEasy.onClick.AddListener(ClickEasyParty);
        //buttonMedium.onClick.AddListener(ClickMediumParty);
        //buttonHard.onClick.AddListener(ClickHardParty);
    }

    void Update()
    {
        GameParam.ShowConf();
    }

    //private void ClickMusicAction()
    //{
    //    GameParam.music = 0;
    //}

    //private void ClickEasyParty()
    //{
    //    GameParam.gameType = 0;
    //}

    
    //private void ClickMediumParty()
    //{
    //    GameParam.gameType = 1;
    //}

    //private void ClickHardParty()
    //{
    //    GameParam.gameType = 2;
    //}

}
