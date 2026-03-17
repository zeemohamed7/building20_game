using System;
using UnityEngine;
// https://www.youtube.com/watch?v=wdYlAyF2Q7c
public class ClockAnomaly : MonoBehaviour
{
    public float rotateSpeed = 500;
    public GameObject minHand;
    public GameObject hourHand;
    public bool isSpinning = false;
    private bool isVisible;



    public void EnableAnomaly()
    {
        isSpinning = true;
    }

    public void DisableAnomaly()
    {
        isSpinning = false;
        minHand.transform.localEulerAngles = Vector3.zero;
        hourHand.transform.localEulerAngles = Vector3.zero; 
    }

    void OnBecameVisible() { isVisible = true; }
    void OnBecameInvisible() { isVisible = false; }
    
    void Update()
    {
        // Move if not on screen
        if (isSpinning && !isVisible)
        {
            minHand.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); // Time.deltaTime amount of time in seconds that passed since the last frame ( so instead of move per frame, it's move per second)
            hourHand.transform.Rotate(0, (rotateSpeed / 12f) * Time.deltaTime, 0);
        }
    }
    

}
 