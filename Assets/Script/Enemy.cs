using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D rigid2d;
    Transform trans;
    float myWidth;

    // Start is called before the first frame update
    void Start()
    {
        trans = this.transform;
        rigid2d = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Ground
        Vector2 lineCastPos = trans.position - trans.right * myWidth;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        // turn around
        if(!isGrounded)
        {
            Vector3 vector3 = trans.eulerAngles;
            vector3.y += 180;
            trans.eulerAngles = vector3;
        }

        //move forward
        Vector2 Vel = rigid2d.velocity;
        Vel.x = -trans.right.x * speed;
        rigid2d.velocity = Vel;
    }
}
