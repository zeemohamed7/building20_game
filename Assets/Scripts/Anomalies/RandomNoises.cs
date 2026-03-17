using UnityEngine;

public class RandomNoises : MonoBehaviour
{
    public AudioSource stingerSource;
    public AudioClip[] stingerClips; 

    [Header("Timing (in seconds)")]
    public float minTimeBetween = 45f; // Shortest time before a sound plays
    public float maxTimeBetween = 120; // Longest time before a sound plays

    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (stingerClips.Length == 0 || stingerSource == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayRandomStinger();
            ResetTimer();
        }
    }

    void PlayRandomStinger()
    {
        int index = Random.Range(0, stingerClips.Length);
        // If random choice chooses an empty slot, play nothing
        if (stingerClips[index] == null) return; 
        
        // Slightly alter the pitch and volume so the exact same clip sounds different every time
        stingerSource.pitch = Random.Range(0.8f, 1.1f);
        stingerSource.volume = Random.Range(0.6f, 1.0f);

        stingerSource.PlayOneShot(stingerClips[index]);
    }

    public void ResetTimer()
    {
        timer = Random.Range(minTimeBetween, maxTimeBetween);
    }
}