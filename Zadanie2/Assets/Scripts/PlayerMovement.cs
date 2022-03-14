using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpForce = 6;
    private bool isGrounded;

    // dash tylko jednorazowo w powietrzu
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float startDashTime = 0.2f;
    private float dashTime;
    private bool canDash = true, dashing = false; //aby nie dało się robic dash kilka razy pod rząd
    private int direction;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    void Update()
    {
        // RUN
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 2 * speed;
        }
        else
            moveSpeed = speed;


        float xDisplacement = Input.GetAxis("Horizontal");
        Vector3 dispacementVector = new Vector3(xDisplacement, 0, 0);
        transform.Translate(dispacementVector * Time.deltaTime * moveSpeed);

        // JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        // DASH


        if (xDisplacement < 0)
            direction = 1;
        else if (xDisplacement > 0)
            direction = 2;
        else direction = 0;
        
        if(Input.GetKeyDown(KeyCode.LeftAlt) && canDash && !dashing)
        {
            canDash = false;
            dashing = true;
            if(direction == 1)
            {
                rb.velocity = Vector2.left * dashSpeed;
            } else if(direction == 2)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            
        }

        if (dashing) dashTime -= Time.deltaTime;
        if(dashTime <= 0)
        {
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
            dashing = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDash = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            canDash = true;
        }
    }

}
