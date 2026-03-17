using UnityEngine;

public class CrushingWallsAnomaly : MonoBehaviour
{
    [Header("First Hallway Section")] public Transform hallwayLegA;
    public Vector3 anomalyScaleA = new Vector3(0.7f, 1, 1); // Shrinks on X

    [Header("Second Hallway Section")] public Transform hallwayLegB;
    public Vector3 anomalyScaleB = new Vector3(1, 1, 0.7f); // Shrinks on Z

    [Header("Settings")] public float shrinkSpeed = 0.005f;
    public bool isShrinking = false;

    private Vector3 normalScale = new Vector3(1, 1, 1);

    public void EnableAnomaly()
    {
        isShrinking = true;
    }

    public void DisableAnomaly()
    {
        isShrinking = false;
        if (hallwayLegA != null) hallwayLegA.localScale = normalScale;
        if (hallwayLegB != null) hallwayLegB.localScale = normalScale;
    }

    void Update()
    {
        if (isShrinking)
        {
            if (hallwayLegA != null)
                hallwayLegA.localScale =
                    Vector3.MoveTowards(hallwayLegA.localScale, anomalyScaleA, shrinkSpeed * Time.deltaTime);

            if (hallwayLegB != null)
                hallwayLegB.localScale =
                    Vector3.MoveTowards(hallwayLegB.localScale, anomalyScaleB, shrinkSpeed * Time.deltaTime);


        }
    }
}