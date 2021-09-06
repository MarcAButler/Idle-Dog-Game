using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    // - Will be used for input but independent of physics
    void Update()
    {

    }

    // DOC: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    // FixedUpdate is called many times per second but it is executed on a fixed timer: the default is 50 calls per second - independent of framerate
    // - Contains operations that require physics so Update() does not get called depending on framerate
    //   - Movement is one of these
    void FixedUpdate()
    {
        

        // Default uses WASD or ARROW KEYS for input
        //Debug.Log( Input.GetAxisRaw("Horizontal") );

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);


        // rb.position will change according to the new movement times its speed times the elapse time since the function was last called
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
