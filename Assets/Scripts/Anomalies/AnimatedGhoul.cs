using UnityEngine;

public class AnimatedGhoul : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private bool isActive = false;

    [Header("Settings")]
    public float rotationSpeed = 2f; // Keep it slow for a creepy "tracking" feel

    void Awake()
    {
        anim = GetComponent<Animator>();
        
        // Find player via tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // Start hidden and paused
        gameObject.SetActive(false);
    }

    public void EnableAnomaly()
    {
        isActive = true;
        gameObject.SetActive(true);
        if (anim != null) anim.speed = 1f; // Start the animation
    }

    public void DisableAnomaly()
    {
        isActive = false;
        if (anim != null) anim.speed = 0f; // Freeze the animation
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isActive && player != null)
        {
            // Face the player
            Vector3 direction = player.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
    }
}