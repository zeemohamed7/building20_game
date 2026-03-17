using UnityEngine;

public class CameraFollowAnomaly : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 50f;
    public bool isFollowing = false; // Starts OFF
    
    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    public void EnableAnomaly() => isFollowing = true;

    public void DisableAnomaly() => isFollowing = false;

    void Update()
    {
        Quaternion target; // like a compass (x,y,x,w)

        if (isFollowing && player != null)
        {
            // Calculate direction to player
            Vector3 worldDir = player.position - transform.position;
            
            // chatgpted this - Convert to Local Space so it works relative to the wall/parent
            Vector3 localDir = transform.parent != null ? 
                transform.parent.InverseTransformDirection(worldDir) : worldDir;
            
            target = Quaternion.LookRotation(localDir);
        }
        else
        {
            target = startRotation;
        }

        // Move smoothly toward the target
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, followSpeed * Time.deltaTime);
    }
}