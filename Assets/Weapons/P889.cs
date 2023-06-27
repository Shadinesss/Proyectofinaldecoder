using System.Collections;
using UnityEngine;

public class P889 : Weapon
{
    [Header("Weapon general data")]
    [SerializeField] private WeaponData m_data;
    [Header("Weapon specific data")]
    [SerializeField] private float m_recoilUpForce;
    [SerializeField] private float m_reloadTime;
    [SerializeField] private GameObject m_particleEffectPrefab;
    [Header("Weapon shooting point")]
    [SerializeField] private Transform m_camera;
    private PlayerManager m_playerManager;
    private bool m_isReloading;
    private bool m_canFire = true;
    private float m_accuracyAngle = 0f;
    [SerializeField] private AudioSource m_shootSound;
    private bool m_canPlaySound1 = true;
    private void Awake()
    {
        m_playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            PrimaryFire();
        //if (Input.GetKeyDown(KeyCode.R) && m_playerManager.GetP889CurrentAmmo() != m_playerManager.GetP889MaxAmmo())
        //{
        //    Reload();
        //    m_playerManager.ReloadP889Magazine();
        //}
    }
    protected override void PrimaryFire()
    {
        if (!m_canFire) //|| m_isReloading || m_playerManager.GetP889CurrentAmmo() <= 0)
            return;
        //m_playerManager.ReduceBulletQuantityP889();
        ShootSound();
        Shoot(m_data.GetPrimaryGeneralDamage());
        StartCoroutine(TimeBetweenShots(m_data.GetPrimaryFiresPerSecond()));
    }
    private void Shoot(float damage)
    {
        if (PlayerIsMoving())
        {
            float randomAngle = Random.Range(-5f, 5f);
            m_accuracyAngle = randomAngle;
        }
        else
        {
            m_accuracyAngle = 0f;
        }
        Vector3 direction = GetFireDirection();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, m_data.GetPrimaryMaxRange()))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                Debug.Log("Le diste a un enemigo " + damage);
            }
            if(hit.collider.CompareTag("Untagged") || hit.collider.CompareTag("Player"))
                Instantiate(m_particleEffectPrefab, hit.point, Quaternion.identity);
            Debug.Log("Le diste a algo");
        }
    }

    private IEnumerator TimeBetweenShots(float cadency)
    {
        m_canFire = false;
        yield return new WaitForSeconds(1/cadency);
        m_canFire = true;
    }

    private Vector3 GetFireDirection()
    {
        Vector3 fireDirection = transform.forward;
        fireDirection = Quaternion.Euler(0f, m_accuracyAngle, 0f) * fireDirection;
        return fireDirection;
    }

    private bool PlayerIsMoving()
    {
        if(m_playerManager.GetPlayerFlatVelocity() > 1f)
            return true;
        else
            return false;
    }

    public void Reload(float reloadTime)
    {
        if (m_playerManager.GetP889CurrentMagazines() <= 0 || m_isReloading)
            return;
        m_isReloading = true;
        StartCoroutine(CompleteReload(reloadTime));
    }

    private IEnumerator CompleteReload(float reloadDuration)
    {
        yield return new WaitForSeconds(reloadDuration);
        m_playerManager.ReloadP889Magazine();
        m_isReloading = false;
    }
    private void OnDrawGizmos()
    {
        Vector3 direction = GetFireDirection();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * m_data.GetPrimaryMaxRange());
    }
    private void ShootSound()
    {
        if (!m_canPlaySound1)
            return;
        m_shootSound.Play();
        StartCoroutine(ShootSoundCooldown(1/ m_data.GetPrimaryFiresPerSecond()));
    }
    private IEnumerator ShootSoundCooldown(float time)
    {
        m_canPlaySound1 = false;
        yield return new WaitForSeconds(time);
        m_canPlaySound1 = true;
    }
}



