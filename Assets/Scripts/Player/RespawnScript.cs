using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; // This can now be set in the Inspector as a respawn point
    [SerializeField] private float respawnDelay = 2f; // Time delay before respawning the player
    [SerializeField] private AudioClip recallSound;
    private Health playerHealth;
    private PlayerMovement playerMovement;
    private Animator anim;
    private bool isRespawning = false;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        // Set the respawn point to the player's starting position if none is assigned in the Inspector
        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= 0 && !isRespawning)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        isRespawning = true;

        // Disable player movement while dead
        playerMovement.enabled = false;

        // Play death animation
        anim.SetTrigger("die");

        yield return new WaitForSeconds(respawnDelay);

        // Reset health
        playerHealth.AddHealth(playerHealth.startingHealth); // Restore to max health

        // Temporarily disable the collider to avoid collision issues on respawn
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
        SoundManager.instance.PlaySound(recallSound);
        // Set the respawn position with a slight offset above the ground
        Vector3 offsetPosition = respawnPoint.position + new Vector3(0, 1.0f, 0); // Adjust Y value as needed
        transform.position = offsetPosition;

        // Reset the "die" trigger and set to idle state
        anim.ResetTrigger("die");
        anim.SetTrigger("idle"); // Trigger idle animation after respawn

        // Re-enable player movement
        playerMovement.enabled = true;

        // Re-enable the collider after a short delay to allow the player to settle
        yield return new WaitForSeconds(0.1f);
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        isRespawning = false;
    }

    // Method to set the respawn point when a checkpoint is reached
    public void SetCheckpoint(Transform checkpointTransform)
    {
        respawnPoint = checkpointTransform;
    }
}
