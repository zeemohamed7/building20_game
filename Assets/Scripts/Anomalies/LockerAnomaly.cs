using UnityEngine;

public class LockerAnomaly : MonoBehaviour
{
    public AudioSource rattleSound;
    public float shakeIntensity = 0.075f;
    public float shakeDuration = 1f;
    private float timer = 0f;
    private bool isAnomaly = false;
    private Vector3 originalPos;
    private bool didPlay = false;

    void Awake()
    {
        originalPos = transform.localPosition; 
    }
    
    public void EnableAnomaly() { isAnomaly = true; }

    public void DisableAnomaly() 
    { 
        isAnomaly = false; 
        timer = 0;
        transform.localPosition = originalPos; 
        didPlay = false; 
    }



    void OnTriggerEnter(Collider other)
    {
        // If the "player" tag hits the trigger and it's activated
        if (other.CompareTag("Player") && isAnomaly && !didPlay)
        {
            timer = shakeDuration; // start the shake by setting the timer
            if (rattleSound != null) rattleSound.Play();
        }
        didPlay = true;
    }

    void Update()
    {
        if (timer > 0)
        {   // Shake logic
            float xOffset = Random.Range(-1f, 1f) * shakeIntensity;

            // Apply ONLY to the X axis (side-to-side)
            transform.localPosition = originalPos + new Vector3(xOffset, 0, 0);
        
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                transform.localPosition = originalPos;
                if (rattleSound != null) rattleSound.Stop(); 
            }
        }
    }
}