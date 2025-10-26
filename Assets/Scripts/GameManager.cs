using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Score variables
    private int playerScore = 0;
    private int aiScore = 0;
    
    // UI Text references (we'll set these up later)
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    
    // Sound effect
    public AudioClip scoreSound;
    private AudioSource audioSource;
    
    // Reference to the ball
    private Ball ball;
    
    void Start()
    {
        // Add audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        
        // Find the ball
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        
        // Initialize score display
        UpdateScoreUI();
    }
    
    void Update()
    {
        // Check if ball went off screen
        Vector3 ballPosition = ball.transform.position;
        
        // Player scored (ball went past AI paddle)
        if (ballPosition.x > 10f)
        {
            PlayerScored();
        }
        // AI scored (ball went past player paddle)
        else if (ballPosition.x < -10f)
        {
            AIScored();
        }
    }
    
    void PlayerScored()
    {
        playerScore++;
        UpdateScoreUI();
        PlayScoreSound();
        ball.Reset();
    }

    void AIScored()
    {
        aiScore++;
        UpdateScoreUI();
        PlayScoreSound();
        ball.Reset();
    }
    
    void PlayScoreSound()
    {
        if (scoreSound != null)
            audioSource.PlayOneShot(scoreSound);
    }

    void UpdateScoreUI()
    {
        if (playerScoreText != null)
            playerScoreText.text = playerScore.ToString();

        if (aiScoreText != null)
            aiScoreText.text = aiScore.ToString();
    }
}