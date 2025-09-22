using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    public float yVelocity;
    private float xVelocity;
    private float maxSpeed = 8;
    public bool grounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            xVelocity -= 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xVelocity += 1;
        }
        else
        {
            xVelocity -= 1;
            if (xVelocity < 0)
            {
                xVelocity = 0;
            }
        }
        if (Mathf.Abs(xVelocity) > maxSpeed)
        {
            if (xVelocity < 0)
            {
                xVelocity = -maxSpeed;
            }
            else
            {
                xVelocity = maxSpeed;
            }
        }

        if (Input.GetKey(KeyCode.Space) && grounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
        }
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            grounded = true;
            yVelocity = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
