using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the RespawnScript component from the player
            RespawnScript respawnScript = other.GetComponent<RespawnScript>();
            
            if (respawnScript != null)
            {
                // Update the respawn point to this checkpoint's position
                respawnScript.SetCheckpoint(transform);
            }
        }
    }
}
