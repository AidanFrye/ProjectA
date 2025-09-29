using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButtonController : MonoBehaviour
{
    public static bool mute = false;
    public void Click()
    {
        if (gameObject.transform.name == "Back Button")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (gameObject.transform.name == "Fullscreen Button")
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
        else if (gameObject.transform.name == "Mute Button")
        {
            mute = !mute;
        }
    }
}
