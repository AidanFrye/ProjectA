using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    public AudioClip[] musicArray;
    public int[] bpm;
    public AudioSource audioSource;
    public int song = 1;
    public GameObject Camera;
    void Start()
    {

    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicArray[song];
        if(!OptionsButtonController.mute)
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
