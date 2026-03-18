using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ONLY trigger if it's the player
        if (other.CompareTag("Player")) 
        {
            // Teleport logic here
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            other.transform.position = GameManager.Instance.startPoint.position;
        
            if (cc != null) cc.enabled = true;
        }
    }
}
