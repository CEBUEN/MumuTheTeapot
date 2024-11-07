using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialPosition; // Store initial position to reset to
    public float damageAmount = 10f; // Amount of damage dealt to the player
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Initially disable gravity so it doesn't fall automatically
        initialPosition = transform.position; // Store the initial position
        ResetSpike(); // Ensure the spike starts at its initial position without falling
    }

    // Method to trigger the fall, only called when triggered by an external source
    public void StartFalling()
    {
        if (!hasFallen)
        {
            hasFallen = true;
            rb.gravityScale = 20; // Enable gravity to make the object fall
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null && playerHealth.isAlive)
            {
                playerHealth.TakeDamage(damageAmount); // Deal damage to the player
            }
            ResetSpike(); // Reset spike after dealing damage
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the spike hits the ground, reset it
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block"))
        {
            ResetSpike();
        }
    }

    // Method to reset the spike position and state without automatically falling again
    private void ResetSpike()
    {
        rb.gravityScale = 0; // Disable gravity to stop falling
        rb.velocity = Vector2.zero; // Stop any residual movement
        transform.position = initialPosition; // Reset to the initial position
        hasFallen = false; // Reset the fall state
    }
}
