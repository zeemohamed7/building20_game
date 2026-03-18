using UnityEngine;
// credits to: https://www.youtube.com/watch?v=HTXKVkOVpeA
public class NewPlayerTeleporter : MonoBehaviour
{
    public Transform TeleportZoneObject; // holds teleport zone cube
    
    public bool turn180Degrees = false; 
    public bool isForwardTrigger = false;

    public AnomalyController anomalyController;
    
    private void OnTriggerEnter(Collider other) // built in method to check if something entered its zone
    {
        if (other.CompareTag("Player")) // checking if player collided with this object
        {
            GameManager.Instance.ProcessPassage(isForwardTrigger);
            Debug.Log("Forward trigger: " + isForwardTrigger);
            
            Vector3 localOffSet = transform.InverseTransformPoint(other.transform.position); // maintains player "offsetness" when they teleport relative to trigger zone

            Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation); // calculates how player's rotation should change depending on teleport zone orientation
            CharacterController cc = other.GetComponent<CharacterController>();  // temporarily disables character component of player

            if (cc != null)
            {
                cc.enabled = false; 
    
                // Calculate the exact new position first
                Vector3 newPosition = TeleportZoneObject.TransformPoint(localOffSet);
                
                // Apply the bumped position
                other.transform.position = newPosition;
    
                // Calculate the base relative rotation
                Quaternion finalRotation = relativeRotation * other.transform.rotation;
                
                // If the toggle is checked, apply a 180-degree spin so the player faces the other way
                if (turn180Degrees)
                {
                    finalRotation *= Quaternion.Euler(0, 180f, 0);
                }
                
                // Apply the final calculated rotation
                other.transform.rotation = finalRotation;
                
                cc.enabled = true; 
                
   
            }
        }
    }
}