using UnityEngine;

public class Ball : MonoBehaviour
{
    // Initial speed of the ball
    public float startSpeed = 6f;
    
    // How much the speed increases with each paddle hit
    public float speedIncrease = 0.3f;

    // Maximum speed the ball can reach
    public float maxSpeed = 15f;
    
    // Sound effects
    public AudioClip paddleHitSound;
    public AudioClip wallHitSound;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    
    // Get or create AudioSource component
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    audioSource.playOnAwake = false;
    
    // Make sure collision detection is set
    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    
    // Launch the ball in a random direction
    Launch();
}
    
    void Launch()
    {
        // Random X direction (-1 or 1)
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        
        // Random Y direction between -0.5 and 0.5
        float y = Random.Range(-0.5f, 0.5f);
        
        // Create velocity vector and normalize it
        Vector2 velocity = new Vector2(x, y).normalized * startSpeed;
        
        // Set the ball's velocity
        rb.linearVelocity = velocity;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
{
    // When ball hits a paddle
    if (collision.gameObject.CompareTag("Paddle"))
    {
        // Play paddle sound with safety check
        if (audioSource != null && paddleHitSound != null)
        {
            audioSource.PlayOneShot(paddleHitSound);
        }
        
        // SCREEN SHAKE - stronger shake for paddle hits
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.StartShake(0.15f, 0.1f);
        }
        
        // Increase speed
        float currentSpeed = rb.linearVelocity.magnitude;
        currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
        
        // Calculate bounce direction
        Vector2 direction = rb.linearVelocity.normalized;
        
        // Add some variation based on where the ball hits the paddle
        float hitFactor = (transform.position.y - collision.transform.position.y) 
                        / collision.collider.bounds.size.y;
        
        // Adjust the Y direction based on hit position
        direction.y = hitFactor;
        direction = direction.normalized;
        
        // Apply new velocity
        rb.linearVelocity = direction * currentSpeed;
    }
    else // Hit a wall
    {
        // Play wall sound with safety check
        if (audioSource != null && wallHitSound != null)
        {
            audioSource.PlayOneShot(wallHitSound);
        }
        
        // SCREEN SHAKE - lighter shake for walls
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.StartShake(0.1f, 0.05f);
        }
    }
}
    
    // Reset ball to center (we'll call this when someone scores)
    public void Reset()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        
        // Wait a moment then launch
        Invoke("Launch", 1f);
    }
}