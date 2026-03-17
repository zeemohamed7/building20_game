using System.Collections;
using System.Collections.Generic;
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
    
    private List<int> seenAnomalies = new List<int>();
    
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
        seenAnomalies.Clear();
        
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
        int chance = Random.Range(0, 100);

        // If they are on Floor 0, ALWAYS force a normal hallway
        if (currentFloor == 0)
        {
            chance = 100; 
        }

        // 50/50 Chance Logic
        if (chance < 50) 
        {
            // Anomaly exists
            currentAnomalyIndex = GetRandomAnomaly(16); 
            anomalyController.ApplyAnomaly(currentAnomalyIndex);
        }
        else 
        {
            // Normal Hallway
            currentAnomalyIndex = 0; // Optional: track that no anomaly is active
            anomalyController.ResetAllAnomalies(); 
        }

        // --- TELEPORT LOGIC ---
        if (player != null && startPoint != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
    
            if (cc != null) cc.enabled = false; // Disable physics to prevent clipping

            player.position = startPoint.position;
            player.rotation = startPoint.rotation;

            if (cc != null) cc.enabled = true; // Re-enable physics
        }
    }

    public int GetRandomAnomaly(int totalAnomalies)
    {
        // If all anomalies have been seen, clear the list
        if (seenAnomalies.Count >= totalAnomalies)
        {
            seenAnomalies.Clear();
        }

        int newAnomaly;
        int safetyNet = 0;

        // Keep picking a number until we find one NOT in the list
        do {
            newAnomaly = Random.Range(0, totalAnomalies);
            safetyNet++;
        } while (seenAnomalies.Contains(newAnomaly) && safetyNet < 100);

        // Add the new one to the list so we don't pick it again this run
        seenAnomalies.Add(newAnomaly);
    
        return newAnomaly;
    }
    


}