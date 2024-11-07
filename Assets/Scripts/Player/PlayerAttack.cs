using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform waterPoint;
    [SerializeField] private GameObject[] waterballs;
    [SerializeField] private AudioClip waterballSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(waterballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        waterballs[FindWaterball()].transform.position = waterPoint.position;
        waterballs[FindWaterball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindWaterball()
    {
        for (int i = 0; i < waterballs.Length; i++)
        {
            if (!waterballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}