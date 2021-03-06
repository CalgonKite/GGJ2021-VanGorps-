using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Simple")]
    //public float speed;
    [Header("Scalar")]
    public float movement_scalar;
    public float max_speed;
    public float scalar_jump;
    public float jump_counter;
    public float jump_time;
    [Header("Components")]
    public bool isground;
    public bool isjump;
    public bool ismoving;
    public PhysicsMaterial2D friction;
    private Rigidbody2D rigidbody2D;
    private Vector3 Velocity = Vector3.zero;
    private float Smoothing = 0.05f;

    /// <summary>
    /// mechanics for movement
    /// Jump
    /// Wall jump (instant) - use small trigger
    /// Dash - Once in air
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        /// Jump gravity ///
        if (isground == false)
        {
            rigidbody2D.gravityScale += 0.04f;
        }
        else
        {
            rigidbody2D.gravityScale = 1;
        }

        /// jumping ///
        /*if (isground == true && Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpvel = new Vector2(0, scalar_jump);
            rigidbody2D.AddForce(jumpvel);
        }
        else
        {

        }*/

        //Move();
        scalarMove();
    }

    private void Update()
    {

    }

    /*public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 moving = new Vector2(moveHorizontal, moveVertical);
        rigidbody2D.AddForce(moving * speed);
    }*/

    //handles inputs for movement
    public void scalarMove()
    {
        /// movement code ///
        float movehoriz = Input.GetAxis("Horizontal");
        //ADD A GROUNDED BOOL
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            ismoving = true;
        }
        else
        {
            ismoving = false;
        }

        if (rigidbody2D.velocity.magnitude < max_speed)
        {
            Vector2 moving = new Vector2(movehoriz, 0).normalized;
            if (isground == true) // IF the player is on the ground, move as normal //
            {
                rigidbody2D.AddForce(movement_scalar * moving);
            }
            else // IF the player is mid-jump, the amount of movement is divided by 3 //
            {
                rigidbody2D.AddForce(movement_scalar * moving / 1.5f);
                Debug.Log("Moving whilst jumping");
            }
        }

        if (isground == true && Input.GetKey(KeyCode.Space))
        {
            isjump = true;
            jump_counter = jump_time;
            Vector2 jumpvel = Vector2.up.normalized;
            rigidbody2D.AddForce(jumpvel * scalar_jump);
        }
        if (Input.GetKey(KeyCode.Space) && isjump == true)
        {
            if (jump_counter > 0)
            {
                Vector2 jumpvel = Vector2.up.normalized;
                rigidbody2D.AddForce(jumpvel * scalar_jump);
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

        if (rigidbody2D.velocity.magnitude > 1 && isground == true)
        {
            if(ismoving == false)
            {
                for(int i = 0; i < 3; i++)
                {
                    friction.friction += 1000f;
                    Debug.Log(rigidbody2D.velocity.magnitude);
                }
                resetFriction();
            }
        }
    }

    public void resetFriction()
    {
        friction.friction = 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isground = true;
            isjump = false;
            Debug.Log("touching ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isground = false;
            Debug.Log("Left ground");
        }
    }
}


