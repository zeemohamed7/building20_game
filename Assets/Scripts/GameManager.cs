using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public RandomNoises noisesScript;
    
    public AnomalyController anomalyController;
    public Transform player;     
    public Transform startPoint;  
    public TextMeshPro floorSign;

    public int currentFloor = 0;
    public int winFloor = 8;
    public int currentAnomalyIndex;
    
    public CanvasGroup endScreen;
    public TextMeshProUGUI endScreenText; // used for UI text on canvas, different to TextMeshPro like the floorsign 

    public GameObject pauseMenuPanel;
    bool isPaused = false;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        GenerateNewLoop();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) 
                ResumeGame();
            else 
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true); // Shows the menu
        Time.timeScale = 0f; // Freezes the game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        // Clears the "Selected" state from the button when pausing again
        EventSystem.current.SetSelectedGameObject(null); 
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ProcessPassage(bool wentForward)
    {
        // WIN CONDITIONS
        
        bool isAnomaly = anomalyController.isAnomalyActive;

        if (!isAnomaly && wentForward) // no anomaly and went forward
        {
            currentFloor++;
            Debug.Log("Correct! Floor: " + currentFloor);
        }

        else if (isAnomaly && !wentForward) // anomaly and went back
        {
            currentFloor++;
            Debug.Log("Correct! Floor: " + currentFloor);
        }
        // IS THIS LOSS | || || |-
        else // anomaly and went forward or no anomaly and went back
        {
            currentFloor = 0;
            Debug.Log("Wrong! Back to Floor 0.");
        }

        if (currentFloor >= winFloor)
        {
            floorSign.text = "Floor ???";
            TriggerEndScreen();
        }
        else
        {
            // reset random noises timer from randomnoises script
            noisesScript.ResetTimer();
            GenerateNewLoop();
        }
        floorSign.text = "Floor " + currentFloor.ToString();

    }
    

    void TriggerEndScreen()
    {
        Debug.Log("Escape Triggered!");
        
        // Disble player movement and cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        
        // Start the fade-to-black Coroutine
        StartCoroutine(FadeToWhite());
    }

    IEnumerator FadeToWhite()
    {
        float duration = 2f; 
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            // Gradually increase the alpha of the Canvas Group
            endScreen.alpha = Mathf.Lerp(0, 1, currentTime / duration);
            yield return null;
        }

        endScreen.alpha = 1;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // turn sound back on
        SceneManager.LoadScene("MainMenuScene");
    }
    
    public void GenerateNewLoop()
    {
        currentAnomalyIndex = GetRandomAnomaly(16); 
        anomalyController.ApplyAnomaly(currentAnomalyIndex);
        
        // Check due to player clipping through the ground during tilted hallways anomaly
        if (player != null && startPoint != null)
        {
            // We get the CharacterController because it usually blocks teleports
            CharacterController cc = player.GetComponent<CharacterController>();
        
            if (cc != null) cc.enabled = false; // Turn off physics briefly

            // Snap the player to your "Teleport Zone" coordinates
            player.position = startPoint.position;
            player.rotation = startPoint.rotation;

            if (cc != null) cc.enabled = true; // Turn physics back on
        }
    }
    
    private int lastAnomaly = -1;
    private int secondLastAnomaly = -1;

    public int GetRandomAnomaly(int totalAnomalies)
    {
        int newAnomaly;
        int safetyNet = 0; // Prevents infinite loops when there's only 1 or 2

        do {
            newAnomaly = Random.Range(0, totalAnomalies);
            safetyNet++;
        } while ((newAnomaly == lastAnomaly || newAnomaly == secondLastAnomaly) && safetyNet < 10);

        // Update the history
        secondLastAnomaly = lastAnomaly;
        lastAnomaly = newAnomaly;

        return newAnomaly;
    }
    


}