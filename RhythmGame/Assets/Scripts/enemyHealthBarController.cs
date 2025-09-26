using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthBarController : MonoBehaviour
{
    private enemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(2.7f, 0.7f, 0);
        enemy = transform.parent.parent.GetComponent<enemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(2.7f - (0.54f * (3 - enemy.health)), transform.localScale.y);
            transform.localPosition = new Vector3(-9.17f - (0.27f * (3 - enemy.health)), transform.localPosition.y, 10);
        }
    }
}
