using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRaycastClick : MonoBehaviour
{
    private GameObject rayCastObject;
    public GameObject viseur;
    private Vector3 initialViseurPos;
    private bool clickPressed = false;

    public GameObject PanelMainMenu;
    public GameObject PanelMenuWeapon;
    public GameObject PanelMenuHighScore;

    void Start()
    {

    }

    private void Update()
    {
        RaycastHit rayHit;
        //Check if raycast hits anything
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 999999999999999999), Color.red, 0.1f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 999999999999999999, LayerMask.GetMask("UI")))
        {
            Debug.Log("RAYCAST HIT");

            if ((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0.07) && !clickPressed)
            {
                clickPressed = true;

                string tag = rayHit.transform.tag;

                Debug.Log("Click : " + tag);

                switch (tag)
                {
                    case "MenuButtonWhenGoodPrevail":

                        GameParam.music = "WhenGoodPrevail";
                        break;

                    case "MenuButtonPlay":

                        // Il faut avoir une musique de sélectionné
                        if (GameParam.music != "")
                        {
                            SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
                        }

                        break;

                    case "MenuButtonHighScore":

                        PanelMenuWeapon.SetActive(false);
                        PanelMainMenu.SetActive(false);
                        PanelMenuHighScore.SetActive(true);

                        break;

                    case "MenuButtonChooseWeapon":

                        PanelMenuHighScore.SetActive(false);
                        PanelMainMenu.SetActive(false);
                        PanelMenuWeapon.SetActive(true);
                        break;


                    case "MenuButtonEasy":

                        GameParam.gameType = 0;
                        break;


                    case "MenuButtonMedium":

                        GameParam.gameType = 1;
                        break;


                    case "MenuButtonHard":

                        GameParam.gameType = 2;
                        break;


                    case "MenuWeaponReturnToMenu":

                        PanelMenuHighScore.SetActive(false);
                        PanelMenuWeapon.SetActive(false);
                        PanelMainMenu.SetActive(true);
                        break;


                    default:

                        break;
                }

                //rayCastObject = rayHit.collider.transform.gameObject;
                //viseur.transform.position = rayHit.point; 
            }
            else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) <= 0.07 && clickPressed)
            {
                clickPressed = false;
            }
        }
    }
}