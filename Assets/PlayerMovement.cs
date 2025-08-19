using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;

    [SerializeField] float moveSpeed;
    [SerializeField] float gravity;
    [SerializeField] float jumpHeight;
    [SerializeField] float maxjumpTime;
    [SerializeField] float maxCoolDown;
    private float jumpCoolDown = 0;
    private float jumpTime = 0;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get movement input
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal movement
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.linearVelocityX = moveValue.x * moveSpeed;

        //Vertical Jumping if cooldown is 0, on ground and jump button is pressed
        if (jumpCoolDown <= 0 && IsGrounded() && jumpAction.IsPressed())
        {
            //Sets vertical velocity and increases cool down and jump time
            rb.linearVelocityY = jumpHeight;
            jumpTime = maxjumpTime;
            jumpCoolDown = maxCoolDown;
        }
        //Decreases cooldown every second
        else
        {
            jumpCoolDown -= 1 * Time.deltaTime;
        }

        //Apply gravity when player is not on ground or duration of jump is over
        if(!IsGrounded() && jumpTime <= 0)
        {
            rb.linearVelocityY -= gravity;
        }
        else
        {
            jumpTime -= 1 * Time.deltaTime;
        }
        
    }

    // Check if player is on ground
    public bool IsGrounded()
    {
        RaycastHit2D[] hitList = Physics2D.BoxCastAll((Vector2)transform.position - new Vector2(0, 1), new Vector2(1, 0.05f), 0f, new Vector2(0, -1), 0f, groundLayer);

        Debug.Log( hitList.Length > 0);
        return hitList.Length > 0;
    }

    //Makes sure gameobject jumps to top of surface
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer.value)
        {
            gameObject.transform.position = collision.transform.position;
        }
    }
}
