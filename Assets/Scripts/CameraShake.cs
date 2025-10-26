using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Singleton instance so other scripts can easily call it
    public static CameraShake Instance;
    
    private Vector3 originalPosition;
    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    
    void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        originalPosition = transform.localPosition;
    }
    
    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            
            // Calculate shake intensity (fades over time)
            float currentShakePower = shakePower * (shakeTimeRemaining / shakeFadeTime);
            
            // Random offset
            float xOffset = Random.Range(-1f, 1f) * currentShakePower;
            float yOffset = Random.Range(-1f, 1f) * currentShakePower;
            
            // Apply shake
            transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
        }
        else
        {
            // Reset to original position when done
            transform.localPosition = originalPosition;
        }
    }
    
    // Call this function to trigger a shake
    public void StartShake(float duration, float power)
    {
        shakeTimeRemaining = duration;
        shakePower = power;
        shakeFadeTime = duration;
    }
}