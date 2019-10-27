using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    bool facingRight = true;
    Animator anim;

    // not grounded
    bool grounded = false;

    //transform at mario foot to see if he is touching the ground
    public Transform groundCheck;

    //how big the circle is going to be when we check distance to the ground
    float groundRadius = 0.2f;

    //force of the jump
    public float jumpForce = 300f;

    //what layer is concidered ground
    public LayerMask whatIsGround;

    //check double jump
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        //pos = transform.position;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //true or false did the ground transform hit the whatIsGround with the groundRadius
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        
        //tell the animator that we are grounded
        anim.SetBool("Ground", grounded);

        //reset double jump
        if (grounded)
            doubleJump = false;

        //get how fast we are moving up or down from the rigidbody
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

   

    // Update is called once per frame
    void Update()
    {

        
        /*if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                pos.x += speed;
            
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                pos.x -= speed;
               
                GetComponent<SpriteRenderer>().flipX = false;
            }
            transform.position = pos;
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
            GetComponent<Animator>().SetBool("isRunning", false);
            */
        
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
          
            //not on the ground
            anim.SetBool("Ground", false);

            //add jump force to the Y axsis of the rigidbody
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
                doubleJump = true;
        }
    }
}
