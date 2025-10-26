using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    // Speed of the AI paddle
    public float speed = 7f;

    // Reference to the ball
    private Transform ball;

    // Movement boundaries
    public float boundaryY = 4.5f;

    // Dead zone to prevent jittering
    public float deadZone = 0.5f;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Find the ball in the scene
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
    }
    
    void FixedUpdate()
    {
        // Only move if the ball exists
        if (ball != null)
        {
            // Calculate desired position (follow the ball's Y position)
            Vector2 newPosition = rb.position;

            // Calculate distance to ball
            float distanceToBall = ball.position.y - rb.position.y;

            // Only move if outside the dead zone
            if (Mathf.Abs(distanceToBall) > deadZone)
            {
                if (distanceToBall > 0)
                {
                    newPosition.y += speed * Time.fixedDeltaTime;
                }
                else
                {
                    newPosition.y -= speed * Time.fixedDeltaTime;
                }
            }

            // Clamp within boundaries
            newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);

            // Move the paddle
            rb.MovePosition(newPosition);
        }
    }
}
    
