using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;

    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        transform.position = (Vector2)transform.position + new Vector2(moveValue.x, 0) * moveSpeed * Time.deltaTime;
        
    }

    // Check if player is on ground
    public bool IsGrounded()
    {
        RaycastHit2D[] hitList = Physics2D.BoxCastAll((Vector2)transform.position - new Vector2(0, 1), new Vector2(1, 0.05f), 0f, new Vector2(0, -1), 0f, groundLayer);

        return hitList.Length > 0;
    }
}
