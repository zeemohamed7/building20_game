using UnityEngine;

public class BlackoutAnomaly : MonoBehaviour
{
    public GameObject lightsParent; 
    public AudioSource loudBangSound; 
    private bool anomalyActive = false;
    private bool didPlay = false;

    public void EnableAnomaly()
    {
        anomalyActive = true;
        didPlay = false;
    }

    public void DisableAnomaly()
    {
        anomalyActive = false;
        lightsParent.SetActive(true);
        didPlay =  false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (anomalyActive && other.CompareTag("Player") && !didPlay)
        {
            TriggerBlackout();
        }
    }

    void TriggerBlackout()
    {
        didPlay = true;
        lightsParent.SetActive(false);
        if (loudBangSound != null) loudBangSound.Play();
        
    }
}