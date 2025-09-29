using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsController : MonoBehaviour
{
    public void Click() 
    {
        if (gameObject.transform.name == "Level Select Button")
        {
            SceneManager.LoadScene("Level Select");
        }
        else if (gameObject.transform.name == "Credits Button") 
        {
            SceneManager.LoadScene("Credits");
        }
        else if (gameObject.transform.name == "Options Button")
        {
            SceneManager.LoadScene("OptionsScene");
        }
        else if (gameObject.transform.name == "Controls Button")
        {
            SceneManager.LoadScene("ControlsScene");
        }
    }
}
