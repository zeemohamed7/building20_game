using UnityEngine;
using System.Collections.Generic;

public class GlitchAnomaly : MonoBehaviour
{
    public List<GameObject> glitchObjects;
    public float jitterAmount = 0.05f;
    public bool isGlitching = false;

    private GameObject activeObject;
    private Vector3 originalPos;
    private Renderer activeRend;

    public void EnableAnomaly()
    {
        if (glitchObjects.Count == 0) return;

        // Piick a random object
        int index = Random.Range(0, glitchObjects.Count);
        activeObject = glitchObjects[index];

        // Remember original position and renderer
        originalPos = activeObject.transform.localPosition;
        activeRend = activeObject.GetComponent<Renderer>();

        isGlitching = true;
    }

    public void DisableAnomaly()
    {
        isGlitching = false;

        if (activeObject != null)
        {
            // Reset
            activeObject.transform.localPosition = originalPos;
            if (activeRend != null) activeRend.material.color = Color.white;
        }
    }

    void Update()
    {
        if (!isGlitching || activeObject == null) return;

        // Jitter 
        Vector3 randomOffset = Random.insideUnitSphere * jitterAmount; // Random.insideUnitSphere gives a ranodm direction in all directions (3D)
        activeObject.transform.localPosition = originalPos + randomOffset;

        // Change color randomly for that glitch effect
        if (activeRend != null)
        {
            activeRend.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}