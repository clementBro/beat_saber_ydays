using UnityEngine;
using UnityEngine.UI;

public class SkinMenuManager : MonoBehaviour {
    
    public Button ButtonArme1;
    public Button ButtonArme2;
    public Button ButtonArme3;
    public Button ButtonArme4;
    

    // Use this for initialization
    void Start() {
        
        ButtonArme1.onClick.AddListener(ChooseYourWeapon1);
        ButtonArme2.onClick.AddListener(ChooseYourWeapon2);
        ButtonArme3.onClick.AddListener(ChooseYourWeapon3);
        ButtonArme4.onClick.AddListener(ChooseYourWeapon4);
       
    }

    private void ChooseYourWeapon1()
    {
        GameParam.rightWeapon = 0;
    }

    private void ChooseYourWeapon2()
    {
        GameParam.rightWeapon = 1;
    }

    private void ChooseYourWeapon3()
    {
        GameParam.leftWeapon = 0;
    }

    private void ChooseYourWeapon4()
    {
        GameParam.leftWeapon = 1;
    }

    void Update()
    {
        GameParam.ShowConf();
    }
}

