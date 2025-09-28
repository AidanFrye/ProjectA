using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButtonController : MonoBehaviour
{
    public void Click()
    {
        if (gameObject.transform.name == "Back Button")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
