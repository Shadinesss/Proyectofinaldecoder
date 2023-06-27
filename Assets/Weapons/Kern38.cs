using System.Collections;
using UnityEngine;

public class Kern38 : Weapon
{
    //[Header("Weapon general data")]
    //[SerializeField] private WeaponData m_data;
    //[Header("Weapon specific data")]
    //[SerializeField] private float m_recoilUpForce;
    //[SerializeField] private float m_reloadTime;
    //[SerializeField] private GameObject m_particleEffectPrefab;
    //[Header("Weapon shooting point")]
    //[SerializeField] private Transform m_camera;
    //private PlayerManager m_playerManager;
    //private bool m_isReloading;
    //private bool m_canFire = true;
    //private float m_accuracyAngle = 0f;

    //private void Awake()
    //{
    //    m_playerManager = GameObject.FindObjectOfType<PlayerManager>();
    //}
    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.Mouse0))
    //        PrimaryFire();
    //    if (Input.GetKey(KeyCode.Mouse1))
    //        SecondaryFire();
    //    if (Input.GetKeyDown(KeyCode.R) && m_playerManager.GetKern38CurrentAmmo() != m_playerManager.GetKern38MaxAmmo())
    //    {
    //        Reload();
    //        m_playerManager.ReloadKern38Magazine();
    //    }
    //}
    //protected override void PrimaryFire()
    //{
    //    if (!m_canFire || m_isReloading || m_playerManager.GetKern38CurrentAmmo() <= 0)
    //        return;
    //    m_playerManager.ReduceBulletQuantityKern38();
    //    Shoot(m_data.GetPrimaryGeneralDamage());
    //    StartCoroutine(TimeBetweenShots(m_data.GetPrimaryFiresPerSecond()));
    //}
    //protected override void SecondaryFire()
    //{
    //    if (!m_canFire || m_isReloading || m_playerManager.GetKern38CurrentAmmo() <= 0)
    //        return;
    //    m_playerManager.ReduceBulletQuantityKern38();
    //    Shoot(m_data.GetSecondaryGeneralDamage());
    //    StartCoroutine(TimeBetweenShots(m_data.GetSecondaryFiresPerSecond()));
    //}

    //private void Shoot(float damage)
    //{
    //    if (PlayerIsMoving())
    //    {
    //        float randomAngle = Random.Range(-15f, 15f);
    //        m_accuracyAngle = randomAngle;
    //    }
    //    else
    //    {
    //        m_accuracyAngle = 0f;
    //    }
    //    Vector3 direction = GetFireDirection();
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, direction, out hit, m_data.GetPrimaryMaxRange()))
    //    {
    //        if (hit.collider.CompareTag("Enemy"))
    //        {
    //            Enemy enemy = hit.collider.GetComponent<Enemy>();
    //            enemy.TakeDamage(damage);
    //        }
    //        if (hit.collider.CompareTag("Untagged") || hit.collider.CompareTag("Player"))
    //            Instantiate(m_particleEffectPrefab, hit.point, Quaternion.identity);
    //    }
    //}

    //private IEnumerator TimeBetweenShots(float cadency)
    //{
    //    m_canFire = false;
    //    yield return new WaitForSeconds(1 / cadency);
    //    m_canFire = true;
    //}

    //private Vector3 GetFireDirection()
    //{
    //    Vector3 fireDirection = transform.forward;
    //    fireDirection = Quaternion.Euler(0f, m_accuracyAngle, 0f) * fireDirection;
    //    return fireDirection;
    //}

    //private bool PlayerIsMoving()
    //{
    //    if (m_playerManager.GetPlayerFlatVelocity() > 1f)
    //        return true;
    //    else
    //        return false;
    //}

    //public void Reload(float reloadTime)
    //{
    //    if (m_playerManager.GetKern38CurrentMagazines() <= 0 || m_isReloading)
    //        return;
    //    m_isReloading = true;
    //    StartCoroutine(CompleteReload(reloadTime));
    //}

    //private IEnumerator CompleteReload(float reloadDuration)
    //{
    //    yield return new WaitForSeconds(reloadDuration);
    //    m_playerManager.ReloadKern38Magazine();
    //    m_isReloading = false;
    //}
}


