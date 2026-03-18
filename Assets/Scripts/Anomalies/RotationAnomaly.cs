using UnityEngine;

public class RotationAnomaly : MonoBehaviour
{
    public Transform hallwayLeg;

    public Vector3 normalRotation = new Vector3(0, 0, 0);
    
    public Vector3 anomalyRotation = new Vector3(0, 0, 15f); 

    public void EnableAnomaly()
    {
        if (hallwayLeg != null)
        {
            hallwayLeg.localEulerAngles = anomalyRotation;
        }
    }

    public void DisableAnomaly()
    {
        if (hallwayLeg != null)
        {
            hallwayLeg.localEulerAngles = normalRotation;
        }
    }
}