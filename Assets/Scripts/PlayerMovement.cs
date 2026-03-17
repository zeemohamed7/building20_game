using UnityEngine;
using UnityEngine.InputSystem;
// chatgpted this tbh
public class PlayerMovement : MonoBehaviour
{   
    public float speed = 5f;
    public float mouseSensitivity = 50f;
    
    public Transform playerCamera; 

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f; // Tracks looking up/down
    private CharacterController controller;
    private Vector3 velocity; // Tracks gravity falling speed
    private float gravity = -9.81f;
    
    [Header("Footsteps")]
    public AudioSource footstepSource;
    public AudioClip[] footstepSounds; // Drop multiple sounds here for variety!
    public float footstepInterval = 0.5f; // How fast the steps are
    private float footstepTimer;
    
    public Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // This locks your mouse cursor to the center of the game screen!
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Catches WASD keys
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Catches Mouse movement
    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        // --- 1. LOOK AROUND ---
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Calculate up/down looking and prevent flipping upside down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        // Apply up/down rotation to the camera, and left/right rotation to the player body
        if (playerCamera != null) 
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        }
        transform.Rotate(Vector3.up * mouseX);

        // --- 2. WALKING ---
        // transform.right and transform.forward ensure you walk in the direction you are facing
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        // --- 3. GRAVITY ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps the player snapped to the floor
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        // --- 4. ANIMATION ---
        bool isMovingNow = moveInput.magnitude > 0.1f;
    
        if (anim != null)
        {
            anim.SetBool("isMoving", isMovingNow);
        }

        // --- 5. FOOTSTEPS ---
        if (isMovingNow && controller.isGrounded)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = footstepInterval; 
            }
        }
        else
        {
            // Stop the audio immediately if we stop walking
            if (footstepSource != null && footstepSource.isPlaying) 
            {
                footstepSource.Stop(); 
            }
            footstepTimer = 0.1f; 
        }
    }

    // Plays a random footstep sound from the array
    void PlayFootstep()
    {
        if (footstepSource != null && footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            
            footstepSource.clip = footstepSounds[index];
            
            // Add slight randomness to pitch and volume for realism
            footstepSource.volume = Random.Range(0.8f, 1.0f);
            footstepSource.pitch = Random.Range(0.9f, 1.1f);
            
            footstepSource.Play();
        }
    }
}