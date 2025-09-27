using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    private bool aggro;
    private Rigidbody2D rb;
    private int direction;
    public int health;
    private gameStateController stateController;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        aggro = false;
        player = GameObject.FindWithTag("Player");
        stateController = GameObject.FindWithTag("GameController").GetComponent<gameStateController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stateController.paused)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
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
                        rb.velocity = new Vector2(rb.velocity.x - (30 * Time.deltaTime), rb.velocity.y);
                        if (rb.velocity.x < -3)
                        {
                            rb.velocity = new Vector2(-3, rb.velocity.y);
                        }
                        direction = 1;
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x + (30 * Time.deltaTime), rb.velocity.y);
                        if (rb.velocity.x > 3)
                        {
                            rb.velocity = new Vector2(3, rb.velocity.y);
                        }
                        direction = 2;
                    }
                }
            }
        }
        else 
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "placeholder_attack")
        {
            if (direction == 1) 
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
            } else 
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
            }
            health -= 1;
            if (health == 0) 
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (direction == 1)
            {
                rb.velocity = new Vector2(6, rb.velocity.y);
            }
            else 
            {
                rb.velocity = new Vector2(-6, rb.velocity.y);
            }
        }
    }
}
