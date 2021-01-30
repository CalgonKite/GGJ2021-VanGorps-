using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class controller : MonoBehaviour
{
    [Header("Vitals")]
    public int Health= 3;
    public HP Ui;

    [Header("Booleans")]
    bool isjump = false; 
    bool isgrounded;
    bool dashed = false;

    [Header("Physics")]
    private Rigidbody2D rgbd2D;

    [Header("Movement Variables")]
    public float runspeed = 4f;
    private Vector3 Velocity = Vector3.zero;
    private float Smoothing = 0.05f;
    public float scalar_jump;
    public float jump_counter;
    public float jump_time;
    float horizMove = 0f;

    private void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>(); /// Assigns 2d rigidbody ///     
        //changeHP(1); /// Test remove health ///
    }

    private void Update()
    {
        horizMove = Input.GetAxisRaw("Horizontal") * runspeed; /// Read input values ///
    }

    /// <summary>
    /// HEALTH FUNCTION
    /// </summary>
    /// <param name="damage"></param>
    public void changeHP(int damage) /// Damage function, passed damage amount ///
    {
        if (Health - damage == 0) /// IF the current health - the damage is equal to 0 ///
        {
            Debug.Log("Game Over");
            Ui.changeDisplay(0); /// Change display to game over state ///
        }
        else /// Otherwise ///
        {
            Ui.changeDisplay(Health-damage); /// Apply the damage to the display ///
            Health -= damage; /// Apply the damage ///
        }
    }

    private void FixedUpdate()
    {
        move(horizMove); /// Calls the move function, passing through the Horizontal input value ///

        /// Jump gravity ///
        if (isgrounded == false) /// Whilst mid-air ///
        {
            rgbd2D.gravityScale += 0.0275f; /// Add gravityscale ///
        }
        else /// Once grounded ///
        {
            rgbd2D.gravityScale = 1; /// Reset gravityscale to normal value ///
        }
    }

    /// <summary>
    /// FUNCTION USED TO HANDLE MOVEMENT
    /// </summary>
    /// <param name="hmove"></param>
    public void move(float hmove) /// Movement script, uses horizMove to determine speed ///
    {
        if (isgrounded == true) /// IF the player is grounded ///
        {
            Vector3 targetVelocity = new Vector2(hmove * 10f, rgbd2D.velocity.y); /// Move the character by finding the target velocity ///
            rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, targetVelocity, ref Velocity, Smoothing); /// And then smoothing it out and applying it to the character ///
        }
        else /// Whilst in the air ///
        {
            Vector3 targetVelocity = new Vector2((hmove * 10f)/1.5f, rgbd2D.velocity.y); /// Moves by target velocity, with it being restricted by 1.5f //
            rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, targetVelocity, ref Velocity, Smoothing); /// Same as above ///
        }

        /// Jump controls ///
        if (isgrounded == true && Input.GetKey(KeyCode.Space)) /// IF the player is grounded & the Space key is presssed ///
        {
            isjump = true; /// Flag bool to state player is jumping ///
            jump_counter = jump_time; /// Increment counter whilst mid-air ///
            Vector2 jumpvel = Vector2.up.normalized; /// Normalise upward movement ///
            rgbd2D.AddForce(jumpvel * scalar_jump); /// Add force in upwards direction ///
        }
        if (Input.GetKey(KeyCode.Space) && isjump == true) /// Whilst mid-air & the jump button is continuely pressed ///
        {
            if (jump_counter > 0) /// IF the counter is higher that null ///
            {
                Vector2 jumpvel = Vector2.up.normalized; /// Normalise upward movement ///
                rgbd2D.AddForce(jumpvel * scalar_jump); /// Add additional force in upwards direction ///
            }
            else /// IF the player is grounded ///
            {
                isjump = false; /// Flag jump as over ///
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) /// IF the space button is released ///
        {
            isjump = false; /// Flag jump as over ///
        }
        /// END of Jump controls ///
        /// DASH Controls ///
        if (isgrounded == false && Input.GetKey(KeyCode.E)) 
        {
            if (dashed == false)
            {
                isjump = false;
                rgbd2D.gravityScale = 0;
                dashed = true;
                for (int i = 0; i < 30; i++) 
                {
                    Vector3 dashVelocity = new Vector2(hmove * 60f, 0); /// Move the character by finding the target velocity ///
                    rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, dashVelocity, ref Velocity, Smoothing); /// And then smoothing it out and applying it to the character ///
                }
            }
            /*else 
            {
                dashed = false;
            }*/
        }
        /// END of DASH Controls///
    }

    /// Collision functions, used to determine whether the player is on the ground ///
    private void OnCollisionEnter2D(Collision2D collision) /// When the player touches the ground ///
    {
        if (collision.gameObject.tag == "Floor") /// Checks against the collision object, IF it's the ground ///
        {
            isgrounded = true; /// Flags bool that states the player has touched the ground ///
            isjump = false; /// Flags bool that states the player isn't mid-air ///
            dashed = false;
            Debug.Log("touching ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision) /// When the player leaves the ground ///
    {
        if (collision.gameObject.tag == "Floor") /// Checks IF the player has left the ground ///
        {
            isgrounded = false; /// Flags bool that states the player is no longer grounded ///
            Debug.Log("Left ground");
        }
    }
}