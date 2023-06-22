using UnityEngine;

public class Razor99 : Weapon
{
    [Header("Weapon general data")]
    [SerializeField] private WeaponData m_data;
    [Header("Weapon specific data")]
    [SerializeField] private float m_invulnerabilityTime;
    [SerializeField] private float m_invulnerabilityCooldown;
    
    private StoryGameManager m_gameManager;
    private bool playerCanUseAbility = true;
    private bool playerCanHit = true;
    private void Awake()
    {
        m_gameManager = StoryGameManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && m_gameManager.IsRazor99Equipped())
            PrimaryFire();
        if (Input.GetKeyDown(KeyCode.Mouse1) && m_gameManager.IsRazor99Equipped())
            SecondaryFire();
    }

    public override void PrimaryFire()
    {
        if (!playerCanHit)
            return;
        else
        {
            float damage = m_data.GetPrimaryGeneralDamage();
            float range = m_data.GetPrimaryMaxRange();
            Vector3 playerPosition = transform.position;
            Vector3 attackDirection = transform.forward;
            Vector3 attackStartPosition = playerPosition + attackDirection * range * 0.1f;
            Vector3 attackEndPosition = playerPosition + attackDirection * range;
            Collider[] hitColliders = Physics.OverlapCapsule(attackStartPosition, attackEndPosition, range * 0.5f);
            foreach (Collider collider in hitColliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    //enemy.TakeDamage(damage); and instantiate blood particles
                }
                else if(enemy == null)
                {
                    //Shoot a raycast and instantiate particles
                }
            }
            StartCoroutine(PrimaryFireDelay(m_data.GetPrimaryFiresPerSecond()));
        }
    }

    public override void SecondaryFire()
    {
        if (!playerCanUseAbility)
            return;
        StartCoroutine(ApplyInvulnerability(m_invulnerabilityTime));
        StartCoroutine(AbilityCooldown(m_invulnerabilityCooldown));
    }

    private System.Collections.IEnumerator AbilityCooldown(float cooldownTime)
    {
        playerCanUseAbility = false;
        yield return new WaitForSeconds(cooldownTime);
        playerCanUseAbility = true;
    }

    private System.Collections.IEnumerator ApplyInvulnerability(float duration)
    {
        m_gameManager.SetInvulnerability(true);
        yield return new WaitForSeconds(duration);
        m_gameManager.SetInvulnerability(false);
    }

    private System.Collections.IEnumerator PrimaryFireDelay(float cadency)
    {
        playerCanHit = false;
        yield return new WaitForSeconds(1/cadency);
        playerCanHit = true;
    }
}


