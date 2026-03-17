using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Transform leftHinge;
    public Transform rightHinge;
    
    public float openAngle = 90f; // Degrees to open
    public float speed = 3f;      // How fast it swings
    
    private bool isPlayerNear = false;
    
    // We use local rotations to make sure they swing relative to their starting point
    private Quaternion leftClosed;
    private Quaternion leftOpen;
    private Quaternion rightClosed;
    private Quaternion rightOpen;

    void Start()
    {
        // Save the starting rotations
        leftClosed = leftHinge.localRotation;
        rightClosed = rightHinge.localRotation;
        
        // Calculate the open positions (left goes negative, right goes positive)
        leftOpen = Quaternion.Euler(leftHinge.localEulerAngles.x, leftHinge.localEulerAngles.y - openAngle, leftHinge.localEulerAngles.z);
        rightOpen = Quaternion.Euler(rightHinge.localEulerAngles.x, rightHinge.localEulerAngles.y + openAngle, rightHinge.localEulerAngles.z);
    }

    void Update()
    {
        if (isPlayerNear)
        {
            leftHinge.localRotation = Quaternion.Slerp(leftHinge.localRotation, leftOpen, Time.deltaTime * speed);
            rightHinge.localRotation = Quaternion.Slerp(rightHinge.localRotation, rightOpen, Time.deltaTime * speed);
        }
        else
        {
            leftHinge.localRotation = Quaternion.Slerp(leftHinge.localRotation, leftClosed, Time.deltaTime * speed);
            rightHinge.localRotation = Quaternion.Slerp(rightHinge.localRotation, rightClosed, Time.deltaTime * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = false;
    }
}