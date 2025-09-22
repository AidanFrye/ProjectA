using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    public AudioClip[] musicArray;
    public int[] bpm;
    public AudioSource audioSource;
    public int song = 1;
    public NoteController noteController;
    public GameObject Camera;
    void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            Instantiate(noteController.gameObject, new Vector3(noteController.gameObject.transform.position.x - i,
            noteController.gameObject.transform.position.y, 0), Quaternion.identity, Camera.transform);
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicArray[song];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
