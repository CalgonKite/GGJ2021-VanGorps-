using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    //simple
    public float speed;
    //scalar
    public float movement_scalar;
    public float max_speed;
    public float scalar_jump;
    //components
    private Rigidbody2D rigidbody2D;
    public bool isground;
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

    public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 moving = new Vector2(moveHorizontal, moveVertical);
        rigidbody2D.AddForce(moving * speed);
    }

    //handles inputs for movement
    public void scalarMove()
    {
        /// movement code ///
        float movehoriz = Input.GetAxis("Horizontal");
        //ADD A GROUNDED BOOL
        if (rigidbody2D.velocity.magnitude < max_speed)
        {
            Vector2 moving = new Vector2(movehoriz, 0);
            rigidbody2D.AddForce(movement_scalar * moving);
        }
        //Vector2 moving = new Vector2(movehoriz, 0);
        //rigidbody2D.AddForce(movement_scalar * moving);

        if (isground == true && Input.GetKey(KeyCode.Space))
        {
            Vector2 jumpvel = new Vector2(0, scalar_jump);
            rigidbody2D.AddForce(jumpvel * 1.5f);
        }
        else
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isground = true;
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
