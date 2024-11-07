using UnityEngine;

public class SlowTrigger : MonoBehaviour
{
    [SerializeField] private SlowSpikes slowSpikes; // Reference to the SlowSpikes script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.CompareTag("Player"))
        {
            // Trigger the SlowSpikes to fall
            if (slowSpikes != null)
            {
                slowSpikes.StartFalling();
            }
            else
            {
                Debug.LogWarning("SlowSpikes reference is not assigned in SlowTrigger.");
            }
        }
    }
}
