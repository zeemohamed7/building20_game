using UnityEngine;

public class PosterAnomaly : MonoBehaviour
{
    public Material normalMaterial;
    public Material anomalyMaterial;
    private MeshRenderer rend;

    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material = normalMaterial;
    }

    public void EnableAnomaly()
    {
        rend.material = anomalyMaterial;
    }

    public void DisableAnomaly()
    {
        rend.material = normalMaterial;
    }
}