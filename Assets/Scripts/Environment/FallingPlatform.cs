using UnityEngine;

public class FallingPlatform2D : MonoBehaviour
{
    public float fallDelay = 1.0f;       // Delay before the platform falls
    public float resetDelay = 2.0f;      // Delay before the platform resets

    private Rigidbody2D rb;
    private Vector2 initialPosition;

    void Start()
    {
        // Store the initial position
        initialPosition = transform.position;

        // Get the Rigidbody2D component and disable gravity initially
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // Keeps the platform in place initially
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player stepped on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallDelay); // Call the fall function after a delay
        }
    }

    void Fall()
    {
        // Enable gravity to make the platform fall
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Start the reset timer
        Invoke("ResetPlatform", resetDelay);
    }

    void ResetPlatform()
    {
        // Reset the platform's position and disable gravity again
        rb.isKinematic = true;
        rb.velocity = Vector2.zero; // Reset any movement velocity
        transform.position = initialPosition; // Teleport back to the starting position
    }
}
