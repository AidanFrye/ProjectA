using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStateController : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            paused = !paused;
        }
        pauseMenu.SetActive(paused);

    }
}
