using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class controller : MonoBehaviour
{
    public float runspeed = 4f;

    float horizMove = 0f;
    bool isjump = false;
    bool isgrounded;

     private Rigidbody2D rgbd2D;
    private Vector3 Velocity = Vector3.zero;
    private float Smoothing = 0.05f;
    public float scalar_jump;
    public float jump_counter;
    public float jump_time;

    private void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizMove = Input.GetAxisRaw("Horizontal") * runspeed;

        if (Input.GetButtonDown("Jump")) 
        {
            isjump = true;
        }
    }

    private void FixedUpdate()
    {
        //move
        move(horizMove);
        /// Jump gravity ///
        if (isgrounded == false)
        {
            rgbd2D.gravityScale += 0.0275f;
        }
        else
        {
            rgbd2D.gravityScale = 1;
        }
    }

    public void move(float hmove)
    {
        if (isgrounded == true)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(hmove * 10f, rgbd2D.velocity.y);
            // And then smoothing it out and applying it to the character
            rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, targetVelocity, ref Velocity, Smoothing);
        }
        else 
        {
            Vector3 targetVelocity = new Vector2((hmove * 10f)/1.5f, rgbd2D.velocity.y);
            // And then smoothing it out and applying it to the character
            rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, targetVelocity, ref Velocity, Smoothing);
        }
        // If the player presses jump
        /*if (isgrounded == true && isjump == true)
        {
            // Add a vertical force to the player.
            isgrounded = false;
            rgbd2D.AddForce(new Vector2(0f, scalar_jump));
        }
        isjump = false;*/

        if (isgrounded == true && Input.GetKey(KeyCode.Space))
        {
            isjump = true;
            jump_counter = jump_time;
            Vector2 jumpvel = Vector2.up.normalized;
            rgbd2D.AddForce(jumpvel * scalar_jump);
        }
        //JUMP
        if (Input.GetKey(KeyCode.Space) && isjump == true)
        {
            if (jump_counter > 0)
            {
                Vector2 jumpvel = Vector2.up.normalized;
                rgbd2D.AddForce(jumpvel * scalar_jump);
            }
            else
            {
                isjump = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isjump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isgrounded = true;
            isjump = false;
            Debug.Log("touching ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isgrounded = false;
            Debug.Log("Left ground");
        }
    }
}
