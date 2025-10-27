using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Score variables
    private int playerScore = 0;
    private int aiScore = 0;

    // Win condition
    public int scoreToWin = 5;

    // UI Text references (we'll set these up later)
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;

    // Game Over UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;

    // Sound effect
    public AudioClip scoreSound;
    private AudioSource audioSource;

    // Reference to the ball
    private Ball ball;

    // Game state
    private bool gameOver = false;
    
    void Start()
    {
        // Add audio source
        audioSource = gameObject.AddComponent<AudioSource>();

        // Find the ball
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            ball = ballObject.GetComponent<Ball>();
        }
        else
        {
            Debug.LogError("GameManager: Cannot find GameObject with tag 'Ball'. Make sure the Ball is tagged correctly!");
        }

        // Make sure game over panel is hidden at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Initialize score display
        UpdateScoreUI();
    }
    
    void Update()
    {
        // Don't check for scoring if ball doesn't exist or game is over
        if (ball == null || gameOver) return;

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

        // Check if player won
        if (playerScore >= scoreToWin)
        {
            EndGame("PAUL WINS!");
        }
        else
        {
            ball.Reset();
        }
    }

    void AIScored()
    {
        aiScore++;
        UpdateScoreUI();
        PlayScoreSound();

        // Check if AI won
        if (aiScore >= scoreToWin)
        {
            EndGame("AI WINS!");
        }
        else
        {
            ball.Reset();
        }
    }
    
    void PlayScoreSound()
    {
        if (scoreSound != null)
            audioSource.PlayOneShot(scoreSound);
    }

    void EndGame(string winner)
    {
        gameOver = true;

        // Stop the ball
        if (ball != null)
        {
            ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        // Show game over screen
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (winnerText != null)
            {
                winnerText.text = winner;
            }
        }
    }

    void UpdateScoreUI()
    {
        if (playerScoreText != null)
            playerScoreText.text = playerScore.ToString();

        if (aiScoreText != null)
            aiScoreText.text = aiScore.ToString();
    }

    // Button functions (call these from UI buttons)
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        // Load main menu scene (you'll need to create this scene later)
        SceneManager.LoadScene("MainMenu");
    }
}