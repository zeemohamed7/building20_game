using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing that fell is the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell through floor! Resetting to start.");
        
            // Use your existing teleport logic
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            other.transform.position = GameManager.Instance.startPoint.position;
            other.transform.rotation = GameManager.Instance.startPoint.rotation;

            if (cc != null) cc.enabled = true;
        }
    }
}
