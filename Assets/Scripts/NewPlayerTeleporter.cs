using UnityEngine;

// credits to: https://www.youtube.com/watch?v=HTXKVkOVpeA
public class NewPlayerTeleporter : MonoBehaviour
{
    public Transform TeleportZoneObject; // holds teleport zone cube
    
    public bool turn180Degrees = false; 
    public bool isForwardTrigger = false;

    public AnomalyController anomalyController;
    
    // Cooldown variables added to prevent infinite teleport loop glitches
    private static float lastTeleportTime;
    private const float TeleportCooldown = 0.75f; 
    
    private void OnTriggerEnter(Collider other) // built in method to check if something entered its zone
    {
        // checking if player collided with this object (and checking teleport cooldown!)
        if (other.CompareTag("Player") && Time.time > lastTeleportTime + TeleportCooldown) 
        {
            lastTeleportTime = Time.time; // Lock the door for a split second

            CharacterController cc = other.GetComponent<CharacterController>();  // temporarily disables character component of player
            if (cc != null) cc.enabled = false;

            // 1. Calculate math BEFORE the hallway tilts
            Vector3 localOffSet = transform.InverseTransformPoint(other.transform.position); // maintains player "offsetness" when they teleport relative to trigger zone
            Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation); // calculates how player's rotation should change depending on teleport zone orientation
            
            // 2. Process the passage and let anomalies trigger
            GameManager.Instance.ProcessPassage(isForwardTrigger);
            Debug.Log("Forward trigger: " + isForwardTrigger);

            if (cc != null)
            {
                // Calculate the exact new position first
                Vector3 newPosition = TeleportZoneObject.TransformPoint(localOffSet);
                newPosition.y += 0.1f; // Tiny bump so the player drops perfectly onto the floor
                
                // Apply the bumped position
                other.transform.position = newPosition;
    
                // Calculate the base relative rotation
                Quaternion finalRotation = relativeRotation * other.transform.rotation;
                
                // If the toggle is checked, apply a 180-degree spin so the player faces the other way
                if (turn180Degrees)
                {
                    finalRotation *= Quaternion.Euler(0, 180f, 0);
                }
                
                // Force upright so the character doesn't glitch into the tilted floor
                Vector3 euler = finalRotation.eulerAngles;
                euler.x = 0; 
                euler.z = 0; 

                // Apply the final calculated rotation
                other.transform.rotation = Quaternion.Euler(euler);
                
                Physics.SyncTransforms(); // Force Unity to see the flat floor before moving
                cc.enabled = true; 
            }
        }
    }
}