using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float yVelocity;
    private float xVelocity;
    private float maxSpeed = 8;
    public bool grounded;
    public bool attackState;
    private float elapsedTime;
    public GameObject attack;
    public Animator animator;
    public int direction;
    public int health = 5;
    void Start()
    {
        grounded = true;
        attackState = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(rb.velocity.x - 1, rb.velocity.y);
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(rb.velocity.x + 1, rb.velocity.y);
            direction = 2;
        } else 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x + 0.1f, rb.velocity.y);
            }
            else 
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.1f, rb.velocity.y);
            }
            if (Mathf.Abs(rb.velocity.x) < 0.2)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) & !attackState) 
        {
            attack.SetActive(true);
            animator.SetBool("isRunning", true);
            Debug.Log("Attacking now");
            attackState = true;
            elapsedTime = 0;
        }
        if (attackState) 
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 0.3) 
            {
                animator.SetBool("isRunning", false);
                attack.SetActive(false);
            }
            if (elapsedTime > 1) 
            {
                Debug.Log("Cooldown finished");
                attackState = false;
            }
        }

        if (Input.GetKey(KeyCode.Space) && grounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
        }
        //rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.x < -33.5 && direction == 1) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            grounded = true;
            yVelocity = 0;
        }

        if (collision.gameObject.CompareTag("Enemy")) 
        {
            if (direction == 2)
            {
                Debug.Log("player hit");
                rb.AddForce(new Vector2(-12, 0), ForceMode2D.Impulse);
            }
            else 
            {
                Debug.Log("player hit");
                rb.AddForce(new Vector2(12, 0), ForceMode2D.Impulse);
            }
            health -= 1;
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
