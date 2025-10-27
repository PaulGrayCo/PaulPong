using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    // Movement speed of the paddle
    public float speed = 10f;
    
    // The boundaries the paddle can move within
    public float boundaryY = 4.5f;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    
    void FixedUpdate()
    {
        // Get input using the new Input System
        float movement = 0f;

        // Check keyboard input
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            {
                movement = 1f;
            }
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            {
                movement = -1f;
            }
        }

        // Calculate the new position
        Vector2 newPosition = rb.position;
        newPosition.y += movement * speed * Time.fixedDeltaTime;

        // Clamp the position to keep paddle within boundaries
        newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);

        // Move the paddle
        rb.MovePosition(newPosition);
    }
}