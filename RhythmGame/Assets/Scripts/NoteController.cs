using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    public musicController musicController;
    public int bpm;
    public float bps;
    public Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector3(2.4f, gameObject.transform.position.y, 0);
        //gameObject.transform.position = originalPosition; 
        bpm = musicController.bpm[musicController.song];
        bps = bpm / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x - (originalPosition.x - 4) > 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - ((bps) * Time.deltaTime), gameObject.transform.position.y, 0);
        }
        else 
        {
            Debug.Log(gameObject.transform.position.x - originalPosition.x);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 4, gameObject.transform.position.y, 0);
        }
    }
}
