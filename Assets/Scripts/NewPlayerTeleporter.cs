using UnityEngine;

// credits to: https://www.youtube.com/watch?v=HTXKVkOVpeA
public class NewPlayerTeleporter : MonoBehaviour
{
    public Transform TeleportZoneObject; // holds teleport zone destination (The "Start" of the next hall)
    
    public bool turn180Degrees = false; 
    public bool isForwardTrigger = false;
    
    private static float lastTeleportTime;
    private const float TeleportCooldown = 0.75f; // Increased slightly for stability
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Time.time > lastTeleportTime + TeleportCooldown) 
        {
            lastTeleportTime = Time.time;

            // 1. RESET THE WORLD FIRST
            // This calls ResetAllAnomalies() inside GameManager
            GameManager.Instance.ProcessPassage(isForwardTrigger);
        
            // 2. FORCE PHYSICS TO RE-CALCULATE THE FLAT FLOOR
            // This ensures the "Tilted" mesh is gone before the player lands
            Physics.SyncTransforms();

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; 

                // 3. NOW TELEPORT THE PLAYER
                Vector3 localOffSet = transform.InverseTransformPoint(other.transform.position); 
                Vector3 targetPosition = TeleportZoneObject.TransformPoint(localOffSet);
            
                // Pop them up to be safe
                targetPosition.y += 1.1f; 
                other.transform.position = targetPosition;
            
                // Reset rotation to upright
                other.transform.rotation = Quaternion.Euler(0, other.transform.rotation.eulerAngles.y, 0);

                // 4. SYNC AGAIN AFTER MOVE
                Physics.SyncTransforms();
                cc.enabled = true;
            }
        }
    }
}