using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtonController : MonoBehaviour
{
    public static int selectedLevel;
    public void Click()
    {
        if (gameObject.transform.name == "Level1")
        {
            Debug.Log("Load into level 1");
            selectedLevel = 1;
            SceneManager.LoadScene("GameplayScene");
        }
        else if (gameObject.transform.name == "Level2")
        {
            Debug.Log("Load into level 2");
            selectedLevel = 2;
            SceneManager.LoadScene("GameplayScene");
        }
        else if (gameObject.transform.name == "Back Button") 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}