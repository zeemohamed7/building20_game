using UnityEngine;

public class GroupHideAnomaly : MonoBehaviour
{
    public GameObject[] objectsToHide;

    public void EnableAnomaly()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    public void DisableAnomaly()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null) obj.SetActive(true);
        }
    }
}