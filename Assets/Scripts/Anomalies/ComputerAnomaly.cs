using UnityEngine;

public class ComputerAnomaly : MonoBehaviour
{
    public MeshRenderer screenMesh;
    public int screenMaterialIndex = 1; 
    public Material normalDesktop; 
    
    [Header("Anomaly Options")]
    public Material blueScreenMat;
    public Material cctvGhostMat;

    [Header("Audio")]
    public AudioSource computerHumSource; 
    public AudioSource scareSource; // Changed name and type to AudioSource
    
    private Transform player;
    private bool hasPlayedScare = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void EnableAnomaly()
    {
        if (screenMesh == null) return;
        hasPlayedScare = false;

        Material[] mats = screenMesh.materials;
        float chance = Random.value; 
        mats[screenMaterialIndex] = (chance > 0.5f) ? blueScreenMat : cctvGhostMat;
        screenMesh.materials = mats;

        if (computerHumSource != null) computerHumSource.Play();
    }

    public void DisableAnomaly()
    {
        if (screenMesh != null)
        {
            Material[] mats = screenMesh.materials;
            mats[screenMaterialIndex] = normalDesktop;
            screenMesh.materials = mats;
        }
        if (computerHumSource != null) computerHumSource.Stop();
        if (scareSource != null) scareSource.Stop(); // Stop scare if loop ends
    }

    void Update()
    {
        // Trigger if anomaly is active and player gets close
        if (screenMesh.materials[screenMaterialIndex] != normalDesktop && !hasPlayedScare)
        {
            if (Vector3.Distance(player.position, transform.position) < 1.5f)
            {
                if (scareSource != null)
                {
                    // 1. Move the scare source object 2 meters behind player
                    Vector3 scarePos = player.position - (player.forward * 2f);
                    scareSource.transform.position = scarePos;

                    // 2. Play the sound
                    scareSource.Play();
                }
                
                hasPlayedScare = true;
            }
        }
    }
}