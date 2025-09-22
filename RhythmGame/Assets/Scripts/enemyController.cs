using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    private bool aggro;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        aggro = false;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x - 1.5f, player.transform.position.y - 1.5f, 0f);
        Vector3 enemyPosition = new Vector3(gameObject.transform.position.x - 1.5f, gameObject.transform.position.y - 1.5f, 0f);

        float playerDistance = Mathf.Abs(Vector3.Distance(playerPosition, enemyPosition));


        if (playerDistance < 4)
        {
            aggro = true;
        }
        if (aggro)
        {
            if (Vector3.Distance(playerPosition, enemyPosition) < 10)
            {
                if (playerPosition.x - enemyPosition.x < 0)
                {
                    rb.velocity = new Vector2(-3, rb.velocity.y);
                }
                else 
                {
                    rb.velocity = new Vector2(3, rb.velocity.y);
                }
            }
        }

    }
}
