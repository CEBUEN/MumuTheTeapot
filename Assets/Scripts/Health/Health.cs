using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public bool isAlive => !dead; // Property to indicate if the player is alive

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        dead = false; // Initialize as alive
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void TakeDamage(float _damage)
    {
        if (dead) return; // If already dead, do nothing

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            // Add invincibility frames (iframes) here if needed
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true; // Mark as dead
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        
        // If health is restored above zero, mark as alive
        if (currentHealth > 0)
        {
            dead = false;
            anim.ResetTrigger("die"); // Optional: reset death animation trigger
        }
    }


    public void SetCurrentHealth(float health)
{
    currentHealth = Mathf.Clamp(health, 0, startingHealth); // Ensure health stays within valid range
    dead = currentHealth <= 0; // Update dead status based on health

    if (dead)
    {
        anim.SetTrigger("die");
        GetComponent<PlayerMovement>().enabled = false;
        SoundManager.instance.PlaySound(deathSound);
    }
    else
    {
        anim.ResetTrigger("die"); // Reset death animation trigger if health is above 0
    }
}

}
