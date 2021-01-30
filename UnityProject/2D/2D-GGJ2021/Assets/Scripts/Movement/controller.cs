using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    [Header("Booleans")]
    public bool isJumping = false; 
    public bool isGrounded;
    public bool hasDashed = false;

    [Header("Physics")]
    private Rigidbody2D rgbd2D;
    private float gravityValue = 1;

    [Header("Movement Variables")]
    public float runSpeed = 4f;
    private Vector3 Velocity = Vector3.zero;
    private float Smoothing = 0.05f;
    public float scalarJump;
    public float jumpCounter;
    public float jumpTime;
    float horizMove = 0f;

    [Header("Wall Jump")]
    public Transform wallGrab;
    public bool canGrab = false;
    public bool isGrabbing = false;
    public LayerMask isSurface;
    public float wallJumpTime = 0.75f;
    private float wallJumpSpeed = 3.25f;
    private float wallJumpCounter;
    private float wallJumpForce = 8f;

    private void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>(); /// Assigns 2d rigidbody ///     
    }

    private void Update()
    {
        horizMove = Input.GetAxisRaw("Horizontal") * runSpeed; /// Read input values ///
    }

    private void FixedUpdate()
    {
        move(horizMove); /// Calls the move function, passing through the Horizontal input value ///

        /// Jump gravity ///
        if (!isGrounded) /// Whilst mid-air ///
        {
            rgbd2D.gravityScale += 0.0275f; /// Add gravityscale /// // 0.0275f
            //jump_counter -= 0.005f;
        }
        else /// Once grounded ///
        {
            rgbd2D.gravityScale = gravityValue; /// Reset gravityscale to normal value ///
        }
    }

    /// <summary>
    /// FUNCTION USED TO HANDLE MOVEMENT
    /// </summary>
    /// <param name="hmove"></param>
    public void move(float hmove) /// Movement script, uses horizMove to determine speed ///
    {
        if (wallJumpCounter <= 0)
        {
            if (isGrounded) /// IF the player is grounded ///
            {
                Vector3 targetVelocity = new Vector2(hmove * 10f, rgbd2D.velocity.y); /// Move the character by finding the target velocity ///
                rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, targetVelocity, ref Velocity, Smoothing); /// And then smoothing it out and applying it to the character ///
            }
            else /// Whilst in the air ///
            {
                Vector3 targetVelocity = new Vector2((hmove * 10f) / 1.5f, rgbd2D.velocity.y); /// Moves by target velocity, with it being restricted by 1.5f //
                rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, targetVelocity, ref Velocity, Smoothing); /// Same as above ///
            }

            /// DASH Controls ///
            if (!isGrounded && Input.GetKey(KeyCode.E) && jumpCounter <= jumpTime - 0.025f) /// IF the E key is pressed, isn't grounded & the player is partway into a jump ///
            {
                if (!hasDashed) /// IF the player hasn't dashed ///
                {
                    isJumping = false; /// IF the player isn't jumping ///
                    rgbd2D.gravityScale = 0; /// Set rigidbody gravity to 0 ///
                    hasDashed = true; /// Set as has dashed ///
                    for (int i = 0; i < 30; i++)
                    {
                        Vector3 dashVelocity = new Vector2(hmove * 60f, 0); /// Move the character by finding the target velocity ///
                        rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, dashVelocity, ref Velocity, Smoothing); /// And then smoothing it out and applying it to the character ///
                    }
                }
            }
            /// END of DASH Controls///

            /// Jump controls ///
            if (Input.GetKey(KeyCode.Space) && isGrounded && !isGrabbing)
            {
                //rgbd2D.velocity = new Vector2(rgbd2D.velocity.x, scalarJump);
                //isJumping = true;

                isJumping = true; /// Flag bool to state player is jumping ///
                jumpCounter = jumpTime; /// Increment counter whilst mid-air ///
                Vector2 jumpvel = Vector2.up.normalized; /// Normalise upward movement ///
                rgbd2D.AddForce(jumpvel * scalarJump); /// Add force in upwards direction ///
            }
            if (Input.GetKey(KeyCode.Space) && isJumping == true && !isGrabbing) /// Whilst mid-air & the jump button is continuely pressed ///
            {
                if (jumpCounter > 0) /// IF the counter is higher that null ///
                {
                    Vector2 jumpvel = Vector2.up.normalized; /// Normalise upward movement ///
                    rgbd2D.AddForce(jumpvel * scalarJump); /// Add additional force in upwards direction ///
                }
                else /// IF the player is grounded ///
                {
                    isJumping = false; /// Flag jump as over ///
                }
                jumpCounter -= 0.0025f;
            }

            /// IF statement to determine directional facing ///
            if (rgbd2D.velocity.x > 0) /// IF moving rightwards ///
            {
                transform.localScale = Vector3.one;
            }
            else if (rgbd2D.velocity.x < 0) /// IF moving leftwards ///
            {
                transform.localScale = new Vector3(-1f, 1, 1f);
            }

            /// Wall jumping section ///
            canGrab = Physics2D.OverlapCircle(wallGrab.position, 0.2f, isSurface); /// Bool to check if an object that can be walljumped is touching the player ///

            isGrabbing = false; /// Set the is-currently-grabbing bool to false ///
            if (canGrab && !isGrounded) /// IF walljump object is touching the player and they aren't grounded ///
            {
                if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0)) /// Determene whether they're moving against the object, in either direction ///
                {
                    isGrabbing = true; /// Set is-currently-grabbing bool to true ///
                }
            }

            if (isGrabbing == true) /// IF the is-currently-grabbing bool is true ///
            {
                rgbd2D.gravityScale = 0; /// Set rigidbody gravity to 0 ///
                rgbd2D.velocity = Vector2.zero; /// Remove all its velocity ///
                if (Input.GetKeyDown(KeyCode.Space)) /// IF the spacebar is pressed ///
                {
                    wallJumpCounter = wallJumpTime; /// Assign the walljumpcounter to be equal to the maxtime ///

                    rgbd2D.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * wallJumpSpeed, wallJumpForce); /// Applies velocity upwards and opposite direction ///
                    rgbd2D.gravityScale = gravityValue; /// Reset gravity to normal ///
                    isGrabbing = false; /// Flag grabbing as false ///
                }
            }
            else /// IF the play isn't grabbing a wall ///
            {
                rgbd2D.gravityScale = gravityValue; /// Flag grabbing as false /// 
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
            Debug.Log("Wallcount = " + wallJumpCounter);
        }
        /// END of Jump controls ///
    }

    /// Collision functions, used to determine whether the player is on the ground ///
    private void OnCollisionEnter2D(Collision2D collision) /// When the player touches the ground ///
    {
        if (collision.gameObject.tag == "Floor") /// Checks against the collision object, IF it's the ground ///
        {
            isGrounded = true; /// Flags bool that states the player has touched the ground ///
            isJumping = false; /// Flags bool that states the player isn't mid-air ///
            hasDashed = false;
            canGrab = false;
            //Debug.Log("touching ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision) /// When the player leaves the ground ///
    {
        if (collision.gameObject.tag == "Floor") /// Checks IF the player has left the ground ///
        {
            isGrounded = false; /// Flags bool that states the player is no longer grounded ///
            //Debug.Log("Left ground");
        }
    }
}

/*if (isgrounded == true && Input.GetKey(KeyCode.Space)) /// IF the player is grounded & the Space key is presssed ///
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
}*/


/*if (walljump == true && Input.GetKeyDown(KeyCode.Q))//walljump code
{
    for (int i = 0; i < 30; i++)
    {
        Vector3 dashVelocity = new Vector2(hmove * 60f, i); /// Move the character by finding the target velocity ///
        rgbd2D.velocity = Vector3.SmoothDamp(rgbd2D.velocity, dashVelocity, ref Velocity, Smoothing); /// And then smoothing it out and applying it to the character ///
    }
}*/
