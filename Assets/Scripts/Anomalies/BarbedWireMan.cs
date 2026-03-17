using System.Collections;
using UnityEngine;

public class BarbedWireMan : MonoBehaviour
{
    private Transform player;
    public GameObject areaLights;    
    
    public AudioSource glitchSound;  
    public AudioSource vanishSound; 
    public AudioSource goofySound; 

    public float jitterStrength = 0.05f; 
    public float vanishDistance = 2.5f;

    private bool isActive = false;
    private Vector3 originalPos;

    public void EnableAnomaly()
    {
        // Save positions
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPos = transform.position; 
        
        isActive = true;
        gameObject.SetActive(true); 
        
        // Hardcode the scale back to 1, 1, 1 (Vector3.one)
        transform.localScale = Vector3.one;
        
        glitchSound.Play();
        StartCoroutine(FlickerRoutine());
    }
    
    //chatgpted this
    IEnumerator FlickerRoutine() // IEnumerator allows for something to happen over time, void happens instantly while enumerator allows you to run a little bit of code, pause, let the rest of the game keep playing on exactly wher you left off
    {
        while (isActive)
        {
            // turn lights off
            areaLights.SetActive(false); 
            // pause THIS function for however many seconds
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); 
            
            // turn them back on
            areaLights.SetActive(true);
            // pause it again
            yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
        }
    }
    
    public void DisableAnomaly()
    {
        StopAllCoroutines(); // relates to Enumerator above - instantly stops the FlickerRoutine
        isActive = false;
        gameObject.SetActive(false);
        transform.localScale = Vector3.one; 
        
        areaLights.SetActive(true);
        glitchSound.Stop();
    }

    void Update()
    {
        if (!isActive) return;
        // shake it up!    
        transform.position = originalPos + Random.insideUnitSphere * jitterStrength;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        
        // if player is too close, disappear
        if (Vector3.Distance(originalPos, player.position) < vanishDistance) 
        {
            Vanish();
        }
    }

    void Vanish()
    {
        isActive = false;
        StopAllCoroutines(); // relates to Enumerator above - instantly stops the FlickerRoutine

        transform.localScale = Vector3.zero; // shrink to 0 (disappears)
        
        if (glitchSound != null) glitchSound.Stop();
        
        if (goofySound != null && Random.value <= 0.2)
        {
            goofySound.Play();
        }
        else if (vanishSound != null) // Otherwise, do the normal scary sound
        {
            vanishSound.Play();
        }
        areaLights.SetActive(true); 
    }
}