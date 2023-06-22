using UnityEngine;

public class Kern38 : Weapon
{
    [Header("Weapon general data")]
    [SerializeField] private WeaponData m_data;
    [SerializeField] private StoryGameManager m_gameManager;
    [Header("Weapon specific data")]
    [SerializeField] private float m_recoilUpForce;
    [SerializeField] private float m_reloadTime;

    private bool m_isReloading;
    private bool m_canFire = true;
    private float m_accuracyAngle = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && m_gameManager.IsKern38Equipped())
        {
            PrimaryFire();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && m_gameManager.IsKern38Equipped())
        {
            SecondaryFire();
        }
        if (Input.GetKeyDown(KeyCode.R) && m_gameManager.GetKern38CurrentAmmo() != m_gameManager.GetKern38MaxAmmo())
        {
            Reload();
        }
    }

    public override void PrimaryFire()
    {
        if (!m_canFire || m_isReloading || m_gameManager.GetKern38CurrentAmmo() <= 0 || m_gameManager.GetPlayerShield() <= 0)
            return;
        m_gameManager.ReduceBulletQuantityKern38();
        m_gameManager.SubstractShield();
        Shoot();
        StartCoroutine(TimeBetweenShots(m_data.GetPrimaryFiresPerSecond()));
    }

    public override void SecondaryFire()
    {
        if (!m_canFire || m_isReloading || m_gameManager.GetKern38CurrentAmmo() <= 0)
            return;
        if (m_gameManager.GetKern38CurrentAmmo() <= 0)
        {
            Reload(m_reloadTime);
            return;
        }
        m_gameManager.ReduceBulletQuantityKern38();
        Shoot2();
        StartCoroutine(TimeBetweenShots(m_data.GetSecondaryFiresPerSecond()));
    }

    private void Shoot()
    {
        Vector3 direction = GetFireDirection();
        if (PlayerIsMoving())
        {
            float randomAngle = Random.Range(-5f, 5f);
            m_accuracyAngle = randomAngle;
        }
        else
        {
            m_accuracyAngle = 0f;
        }

        // Disparar el raycast con la dirección ajustada
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, m_data.GetPrimaryMaxRange()))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
            }
            else if (hit.collider.CompareTag("Ally"))
            {
            }
            else
            {
            }
        }
    }

    private void Shoot2()
    {
        Vector3 direction = GetFireDirection();
        Recoil(m_recoilUpForce);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, m_data.GetSecondaryMaxRange()))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
            }
            else if (hit.collider.CompareTag("Ally"))
            {
            }
            else
            {
            }
        }
    }
    private System.Collections.IEnumerator TimeBetweenShots(float cadency)
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
        if (m_gameManager.GetPlayerFlatVelocity() > 1f)
            return true;
        else
            return false;
    }

    public void Reload(float reloadTime)
    {
        if (m_gameManager.GetKern38CurrentMagazines() <= 0 || m_isReloading)
            return;
        m_isReloading = true;
        StartCoroutine(CompleteReload(reloadTime));
    }

    private System.Collections.IEnumerator CompleteReload(float reloadDuration)
    {
        yield return new WaitForSeconds(reloadDuration);
        m_gameManager.ReloadKern38Magazine();
        m_isReloading = false;
    }

    private void Recoil(float recoilUpForce)
    {
        Camera.main.transform.Rotate(Vector3.right, recoilUpForce);
    }
}


