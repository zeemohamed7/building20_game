using UnityEngine;

public class EyeAnomaly : MonoBehaviour
{
    public GameObject eyesObject; 
    
    public float blinkChance = 0.001f; 
    
    private bool isAnomaly = false;
    private float blinkTimer = 0;

    public void EnableAnomaly() 
    { 
        isAnomaly = true; 
        blinkTimer = 0f; 
        if(eyesObject) eyesObject.SetActive(true);
    }

    public void DisableAnomaly() 
    { 
        isAnomaly = false; 
        if(eyesObject) eyesObject.SetActive(false); 
    }
    void Update()
    {
        if (!isAnomaly || !eyesObject) return;

        blinkTimer -= Time.deltaTime;

        if (blinkTimer <= 0)
        {
            // Toggle eyes
            bool currentlyOpen = eyesObject.activeSelf;
            eyesObject.SetActive(!currentlyOpen);

            if (currentlyOpen) 
            {
                // Just closed them: stay shut for 0.15s
                blinkTimer = 0.15f; 
            }
            else 
            {
                // Just opened them: wait 3 to 6 seconds for next blink
                blinkTimer = Random.Range(3f, 6f);
            }
        }
    }
}