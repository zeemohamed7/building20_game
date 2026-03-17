using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        GenerateNewLoop();
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
        AudioListener.pause = false; // turn sound back on
        SceneManager.LoadScene("MainMenuScene");
    }
    
    public void GenerateNewLoop()
    {
        currentAnomalyIndex = Random.Range(0, 16); 
        anomalyController.ApplyAnomaly(currentAnomalyIndex);
    }
    
    


}