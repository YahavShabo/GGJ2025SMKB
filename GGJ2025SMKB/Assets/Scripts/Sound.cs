using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource of the object
    public float maxDistance = 10f;  // Maximum distance for the sound to play at full volume
    public float maxSound = 1f;     // Maximum sound volume (can be set lower than 1)
    private Transform player;        // Reference to the player object

    void Start()
    {
        // Assuming the player has the tag "Player"
        player = GameObject.Find("Player").transform;
        audioSource= GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calculate the distance between the object and the player
        float distance = Vector2.Distance(player.position, transform.position);

        // If the object is within the max distance
        if (distance <= maxDistance)
        {
            // Calculate the volume based on distance (inverse distance model)
            float volume = Mathf.Clamp01(1 - distance / maxDistance);
            volume = Mathf.Min(volume, maxSound); // Apply maxSound cap

            // Set the audio source volume
            audioSource.volume = volume;
        }
        else
        {
            // Stop the audio when the object is outside the sound range
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("Audio stopped"); // Debugging line
            }
        }
    }
}
