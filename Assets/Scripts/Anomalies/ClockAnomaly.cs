using UnityEngine;

public class ClockAnomaly : MonoBehaviour
{
    public float rotateSpeed = 500f;
    public GameObject minHand;
    public GameObject hourHand;
    public bool isSpinning = false;

    public void EnableAnomaly()
    {   
        isSpinning = true;
    }

    public void DisableAnomaly()
    {
        isSpinning = false;
        // Reset hands to 12:00
        minHand.transform.localEulerAngles = Vector3.zero;
        hourHand.transform.localEulerAngles = Vector3.zero; 
    }

    void Update()
    {
        if (isSpinning)
        {
            // Rotates on the Y axis (Change the middle 0 to rotateSpeed if it's the wrong axis)
            minHand.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); 
            hourHand.transform.Rotate(0, (rotateSpeed / 12f) * Time.deltaTime, 0);
        }
    }
}