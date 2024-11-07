using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public FallingSpikes fallingSpikes; // Reference to the FallingSpikes script on the falling object

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the fall when the player enters the trigger zone
            fallingSpikes.StartFalling();
        }
    }
}
