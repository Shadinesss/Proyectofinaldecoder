using System.Collections;
using UnityEngine;

public class Razor99 : Weapon
{
    //[Header("Weapon general data")]
    //[SerializeField] private WeaponData m_data;
    //[Header("Weapon specific data")]
    //[SerializeField] private float m_invulnerabilityTime;
    //[SerializeField] private float m_invulnerabilityCooldown;
    //[Header("Camera")]
    //[SerializeField] private Transform m_camera;
    //[Header("Capsule Cast")]
    //[SerializeField] private float m_capsuleRadius = 0.5f;
    //[SerializeField] private float m_capsuleHeight = 2f;
    //[SerializeField] private float m_capsuleDistance = 5f;
    //[Header("Particle Effect")]
    //[SerializeField] private GameObject m_particleEffectPrefab;
    //private PlayerManager m_playerManager;
    //private bool playerCanUseAbility = true;
    //private bool playerCanHit = true;

    //private void Awake()
    //{
    //    m_playerManager = GameObject.FindObjectOfType<PlayerManager>();
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //        PrimaryFire();
    //    if (Input.GetMouseButton(1))
    //        SecondaryFire();
    //}

    //protected override void PrimaryFire()
    //{
    //    if (!playerCanHit)
    //        return;
    //    float damage = m_data.GetPrimaryGeneralDamage();
    //    Vector3 playerPosition = transform.position;
    //    Vector3 attackDirection = transform.forward;
    //    Vector3 capsuleCastCenter = playerPosition + attackDirection * m_capsuleDistance * 0.5f;
    //    RaycastHit[] nonEnemyHits = Physics.CapsuleCastAll(
    //        capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f,
    //        capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f,
    //        m_capsuleRadius,
    //        attackDirection,
    //        m_capsuleDistance,
    //        ~LayerMask.GetMask("Enemy")
    //    );

    //    for (int i = 0; i < nonEnemyHits.Length; i++)
    //    {
    //        RaycastHit hit = nonEnemyHits[i];
    //        if (hit.collider.CompareTag("Player"))
    //            continue;
    //        InstantiateParticleEffect(hit.point, hit.normal);
    //    }

    //    RaycastHit[] enemyHits = Physics.CapsuleCastAll(
    //        capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f,
    //        capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f,
    //        m_capsuleRadius,
    //        attackDirection,
    //        m_capsuleDistance,
    //        LayerMask.GetMask("Enemy")
    //    );

    //    for (int i = 0; i < enemyHits.Length; i++)
    //    {
    //        RaycastHit hit = enemyHits[i];
    //        Enemy enemy = hit.collider.GetComponent<Enemy>();
    //        if (enemy != null)
    //        {
    //            enemy.TakeDamage(damage);
    //            InstantiateParticleEffect(hit.point, hit.normal);
    //        }
    //    }

    //    StartCoroutine(PrimaryFireDelay(m_data.GetPrimaryFiresPerSecond()));
    //}

    //protected override void SecondaryFire()
    //{
    //    if (!playerCanUseAbility)
    //        return;

    //    StartCoroutine(ApplyInvulnerability(m_invulnerabilityTime));
    //    StartCoroutine(AbilityCooldown(m_invulnerabilityCooldown));
    //}
    //private void InstantiateParticleEffect(Vector3 position, Vector3 normal)
    //{
    //    Quaternion rotation = Quaternion.LookRotation(normal);
    //    Instantiate(m_particleEffectPrefab, position, rotation);
    //}
    //private IEnumerator AbilityCooldown(float cooldownTime)
    //{
    //    playerCanUseAbility = false;
    //    yield return new WaitForSeconds(cooldownTime);
    //    playerCanUseAbility = true;
    //}

    //private IEnumerator ApplyInvulnerability(float duration)
    //{
    //    Debug.Log("Te has vuelto invulnerable");
    //    m_playerManager.SetInvulnerability(true);
    //    yield return new WaitForSeconds(duration);
    //    m_playerManager.SetInvulnerability(false);
    //    Debug.Log("Terminó la invulnerabilidad");
    //}

    //private IEnumerator PrimaryFireDelay(float cadency)
    //{
    //    playerCanHit = false;
    //    yield return new WaitForSeconds(1 / cadency);
    //    playerCanHit = true;
    //}
    //private void OnDrawGizmos()
    //{
    //    Vector3 capsuleCastCenter = transform.position + transform.forward * m_capsuleDistance * 0.5f;

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f, m_capsuleRadius);
    //    Gizmos.DrawWireSphere(capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f, m_capsuleRadius);
    //    Gizmos.DrawLine(
    //        capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f + transform.forward * m_capsuleRadius,
    //        capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f + transform.forward * m_capsuleRadius
    //    );
    //    Gizmos.DrawLine(
    //        capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f - transform.forward * m_capsuleRadius,
    //        capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f - transform.forward * m_capsuleRadius
    //    );
    //    Gizmos.DrawLine(
    //        capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f + transform.right * m_capsuleRadius,
    //        capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f + transform.right * m_capsuleRadius
    //    );
    //    Gizmos.DrawLine(
    //        capsuleCastCenter + Vector3.up * m_capsuleHeight * 0.5f - transform.right * m_capsuleRadius,
    //        capsuleCastCenter - Vector3.up * m_capsuleHeight * 0.5f - transform.right * m_capsuleRadius
    //    );
    //}
}


