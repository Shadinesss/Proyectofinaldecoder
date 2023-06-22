using UnityEngine;

public class P889 : Weapon
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && m_gameManager.IsP889Equipped())
            PrimaryFire();
        if(Input.GetKeyDown(KeyCode.R) && m_gameManager.GetP889CurrentAmmo() != m_gameManager.GetP889MaxAmmo())
        {
            Reload();
            m_gameManager.ReloadP889Magazine();
        }
    }
    public override void PrimaryFire()
    {
        if (!m_canFire || m_isReloading || m_gameManager.GetP889CurrentAmmo() <= 0)
            return;
        m_gameManager.ReduceBulletQuantityP889();
        Shoot();
        Recoil(m_recoilUpForce);
        StartCoroutine(TimeBetweenShots(m_data.GetPrimaryFiresPerSecond()));
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, m_data.GetPrimaryMaxRange()))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                //Call enemy method to take damage. Instantiate blood particles.
            }
            else if (hit.collider.CompareTag("Ally"))
            {
                //Call method for a post processing effect and sound that you are not supposed to do this
            }
            else
            {
                //Instantiate a particle effect on the spot
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
        if(m_gameManager.GetPlayerFlatVelocity() > 1f)
            return true;
        else
            return false;
    }

    public void Reload(float reloadTime)
    {
        if (m_gameManager.GetP889CurrentMagazines() <= 0 || m_isReloading)
            return;
        m_isReloading = true;
        StartCoroutine(CompleteReload(reloadTime));
    }

    private System.Collections.IEnumerator CompleteReload(float reloadDuration)
    {
        yield return new WaitForSeconds(reloadDuration);
        m_gameManager.ReloadP889Magazine();
        m_isReloading = false;
    }

    private void Recoil(float recoilUpForce)
    {
        Camera.main.transform.Rotate(Vector3.right, recoilUpForce);
    }
}



