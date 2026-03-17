using UnityEngine;
using System.Collections.Generic; 

public class LightFlicker : MonoBehaviour
{
    public List<Light> lightSources = new List<Light>(); // one fixture has 3 lights so a list
    public float maxIntensity = 2.0f;
    private List<float> defaultIntensities = new List<float>(); 
    private bool isAnomaly = false;

    void Start()
    {
        // If the list is empty, find ALL lights inside this object
        if (lightSources.Count == 0)
        {
            lightSources.AddRange(GetComponentsInChildren<Light>()); // Spotlights are CHILDREN
        }

        // Save everyone's default brightness
        foreach (Light l in lightSources)
        {
            defaultIntensities.Add(l.intensity);
        }
    }

    public void EnableAnomaly() { isAnomaly = true; }
    
    public void DisableAnomaly() 
    { 
        isAnomaly = false; 
        for (int i = 0; i < lightSources.Count; i++)
        {
            lightSources[i].intensity = defaultIntensities[i]; // Return to default brightness
            lightSources[i].enabled = true;
        }
    }

    void Update()
    {
        if (!isAnomaly) return;

        if (Random.value < 0.02f)
        {
            float randIntensity = Random.Range(0.5f, maxIntensity);
            // bool shouldBeOn = Random.value > 0.1f;

            // Make all 3 lights in this fixture do the same thing
            foreach (Light l in lightSources)
            {
                l.intensity = randIntensity;
                // l.enabled = shouldBeOn;
            }
        }
    }
}