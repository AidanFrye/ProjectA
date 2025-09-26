using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarController : MonoBehaviour
{
    public CharacterController player;
    private int changeInX;
    void Start()
    {
        transform.localScale = new Vector3(2.7f, 0.7f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(2.7f - (0.54f * (5 - player.health)), transform.localScale.y);
            transform.localPosition = new Vector3(-9.17f - (0.27f * (5 - player.health)), transform.localPosition.y, 10);
        }

    }
}
