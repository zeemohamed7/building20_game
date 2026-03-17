using UnityEngine;
// credits to: https://www.youtube.com/watch?v=HTXKVkOVpeA
public class NewPlayerTeleporter : MonoBehaviour
{
    public Transform TeleportZoneObject; // holds teleport zone cube
    
    public bool turn180Degrees = false; 
    public bool isForwardTrigger = false;
    public AnomalyController anomalyController;
    
    private static float lastTeleportTime;
    private const float TeleportCooldown = 0.1f;
    
    private void OnTriggerEnter(Collider other) // built in method to check if something entered its zone
    {
        if (other.CompareTag("Player") && Time.time > lastTeleportTime + TeleportCooldown) // checking if player collided with this object and cooldown
        {
            GameManager.Instance.ProcessPassage(isForwardTrigger);
            Debug.Log("Forward trigger: " + isForwardTrigger);
            
            Vector3 localOffSet = transform.InverseTransformPoint(other.transform.position); // maintains player "offsetness" when they teleport relative to trigger zone
            
            // remember relative position.rotation
            Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation); // calculates how player's rotation should change depending on teleport zone orientation
            CharacterController cc = other.GetComponent<CharacterController>();  // temporarily disables character component of player

            if (cc != null)
            {
                cc.enabled = false; 
    
                // 3. Apply New Position
                other.transform.position = TeleportZoneObject.TransformPoint(localOffSet);
    
                // 4. Apply New Rotation
                Quaternion finalRotation = relativeRotation * other.transform.rotation;
                if (turn180Degrees)
                {
                    finalRotation *= Quaternion.Euler(0, 180f, 0);
                }
                other.transform.rotation = finalRotation;
                
                // This forces Unity to update the position IMMEDIATELY and prevents the "one-frame jitter"
                Physics.SyncTransforms();
                
                cc.enabled = true; 
            }
        }
    }
}