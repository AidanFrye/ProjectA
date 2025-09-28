using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsController : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

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
    }
}
