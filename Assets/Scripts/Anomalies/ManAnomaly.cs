using UnityEngine;

public class ManAnomaly : MonoBehaviour
{
    private Transform player;
    private bool isActive = false;

    [Header("Settings")]
    public float rotationSpeed = 5f;

    void Start()
    {
        // Find the player automatically
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        
        // Start hidden
        gameObject.SetActive(false);
    }

    public void EnableAnomaly()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void DisableAnomaly()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isActive && player != null)
        {
            // Calculate direction to player
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Keep him standing straight up

            if (direction != Vector3.zero)
            {
                // Smoothly rotate to face the player
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
    }
}