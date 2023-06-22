using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameManager : MonoBehaviour

{
    [Header("Player entity")]
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private Rigidbody m_playerRigidBody;
    [Header("Player statistics")]
    [SerializeField] private float m_playerMaxHealth;
    [SerializeField] private float m_playerMaxShield;
    [SerializeField] private float m_playerMaxStamina;
    [Header("Stamina")]
    [SerializeField] private float m_playerStaminaConsumptionPerSecond;
    [SerializeField] private float m_playerStaminaRegenerationPerSecond;
    [Header("Player damage parameters")]
    [SerializeField][Range(0f, 1f)] private float m_noPiercingDamagePercentageToHealth;
    [SerializeField][Range(0f, 1f)] private float m_noPiercingDamagePercentageToShield;
    [SerializeField][Range(0f, 1f)] private float m_lowPiercingDamagePercentageToShield;
    [SerializeField][Range(0f, 1f)] private float m_lowPiercingDamagePercentageToHealth;
    [SerializeField][Range(0f, 1f)] private float m_mediumPiercingDamagePercentageToShield;
    [SerializeField][Range(0f, 1f)] private float m_mediumPiercingDamagePercentageToHealth;
    [SerializeField][Range(0f, 1f)] private float m_highPiercingDamagePercentageToShield;
    [SerializeField][Range(0f, 1f)] private float m_highPiercingDamagePercentageToHealth;
    [SerializeField][Range(0f, 1f)] private float m_fullPiercingDamagePercentageToShield;
    [SerializeField][Range(0f, 1f)] private float m_fullPiercingDamagePercentageToHealth;
    [Header("Upgradable statistics")]
    [SerializeField] private float m_upgradeMaxHealthParameter;
    [SerializeField] private float m_upgradeMaxShieldParameter;
    [SerializeField] private float m_upgradeMaxStaminaParameter;
    [Header("Weapons Max Ammo")]
    [SerializeField] private int m_maxMagazinesP889;
    [SerializeField] private int m_maxMagazinesKern38;
    [SerializeField] private int m_maxAmmoP889;
    [SerializeField] private int m_maxAmmoKern38;

    private float m_playerHealth;
    private float m_playerShield;
    private float m_playerStamina;
    private int m_P889Ammo;
    private int m_Kern38Ammo;
    private int m_P889Magazines;
    private int m_Kern38Magazines;
    private List<GameObject> m_collectedOrbs;
    private float m_score;
    private bool m_playerIsInvicible = false;
    public bool m_Razor99Equipped;
    public bool m_P889Equipped;
    public bool m_Kern38Equipped;

    public enum PiercingType
    {
        NoPiercing,
        LowPiercing,
        MediumPiercing,
        HighPiercing,
        FullPiercing
    }
    public static StoryGameManager instance;

    // Patrón Singleton
    public static StoryGameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Se equipó la espada laser Razor99");
            m_Razor99Equipped = true;
            m_P889Equipped = false;
            m_Kern38Equipped = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Se equipó la pistola electrónica P-889");
            m_Razor99Equipped = false;
            m_P889Equipped = true;
            m_Kern38Equipped = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Se equipó el fusil de asalto multifuncional Kern38");
            m_Razor99Equipped = false;
            m_P889Equipped = false;
            m_Kern38Equipped = true;
        }
    }

    //Getters
    public int GetP889CurrentAmmo()
    {
        return m_P889Ammo;
    }
    public int GetKern38CurrentAmmo()
    {
        return m_Kern38Ammo;
    }
    public int GetP889CurrentMagazines()
    {
        return m_P889Magazines;
    }
    public int GetKern38CurrentMagazines()
    {
        return m_Kern38Magazines;
    }
    public int GetP889MaxAmmo()
    {
        return m_maxAmmoP889;
    }
    public int GetKern38MaxAmmo()
    {
        return m_maxAmmoKern38;
    }
    public Vector3 GetPlayerPosition()
    {
        return m_playerTransform.position;
    }
    public float GetPlayerVelocity()
    {
        return m_playerRigidBody.velocity.magnitude;
    }
    public float GetPlayerFlatVelocity()
    {
        Vector3 playerVelocity = m_playerRigidBody.velocity;
        Vector3 yPlayerVelocity = new Vector3(0f, m_playerRigidBody.velocity.y, 0f);
        Vector3 flatVelocity = playerVelocity - yPlayerVelocity;
        return flatVelocity.magnitude;
    }
    public bool IsRazor99Equipped()
    {
        return m_Razor99Equipped;
    }
    public bool IsP889Equipped()
    {
        return m_P889Equipped;
    }
    public bool IsKern38Equipped()
    {
        return m_Kern38Equipped;
    }
    public float GetPlayerMaxHealth()
    {
        return m_playerMaxHealth;
    }
    public float GetPlayerMaxShield()
    {
        return m_playerMaxShield;
    }
    public float GetPlayerMaxStamina()
    {
        return m_playerMaxStamina;
    }
    public float GetPlayerHealth()
    {
        if (IsPlayerAlive())
            return m_playerHealth;
        else
            return 0;
    }
    public float GetPlayerShield()
    {
        if(IsPlayerShielded())
            return m_playerShield;
        else
            return 0;
    }
    public float GetPlayerStamina()
    {
        if(HasPlayerStamina())
            return m_playerStamina;
        else
            return 0;
    }
    public float GetScore()
    {
        return m_collectedOrbs.Count * 25f;
    }
    public bool IsPlayerAlive()
    {
        if (m_playerHealth > 0)
            return true;
        else return false;
    }
    public bool IsPlayerShielded()
    {
        if(m_playerShield > 0)
            return true;
        else return false;
    }
    public bool HasPlayerStamina()
    {
        if (m_playerStamina > 0)
            return true;
        else return false;
    }
    //Setters
    public void FallDamage()
    {

    }
    public void SubstractShield()
    {
        if(m_playerShield > 0)
            m_playerShield -= 0.16f;
    }
    public void ReduceBulletQuantityP889()
    {
        if (m_P889Ammo > 0)
            m_P889Ammo--;
    }
    public void ReduceBulletQuantityKern38()
    {
        if (m_Kern38Ammo > 0)
            m_Kern38Ammo--;
    }
    public void ReloadP889Magazine()
    {
        if(m_P889Magazines > 0)
        {
            m_P889Magazines -= 1;
            m_P889Ammo = m_maxAmmoP889;
        }
    }
    public void ReloadKern38Magazine()
    {
        if (m_Kern38Magazines > 0)
        {
            m_Kern38Magazines -= 1;
            m_Kern38Ammo = m_maxAmmoKern38;
        }
    }
    public void UpgradeMaxHealth()
    {
        if(m_collectedOrbs.Count > 0)
        {
            m_playerMaxHealth += m_upgradeMaxHealthParameter;
            m_collectedOrbs.Remove(m_collectedOrbs[m_collectedOrbs.Count-1]);
        }
    }

    public void UpgradeMaxShield()
    {
        if (m_collectedOrbs.Count > 0)
        {
            m_playerMaxShield += m_upgradeMaxShieldParameter;
            m_collectedOrbs.Remove(m_collectedOrbs[m_collectedOrbs.Count - 1]);
        }
    }

    public void UpgradeMaxStamina()
    {
        if (m_collectedOrbs.Count > 0)
        {
            m_playerMaxStamina += m_upgradeMaxStaminaParameter;
            m_collectedOrbs.Remove(m_collectedOrbs[m_collectedOrbs.Count - 1]);
        }
    }
    public void RestorePlayerHealth(float p_healthToRestore)
    {
        m_playerHealth += p_healthToRestore;
        if(m_playerHealth > m_playerMaxHealth)
            m_playerHealth = m_playerMaxHealth;
    }

    public void HealthRegeneration()
    {
        if (IsPlayerShielded() && m_playerHealth < m_playerMaxHealth)
            m_playerHealth += (m_playerMaxHealth - m_playerHealth) * 0.01f;
        else if (m_playerHealth > m_playerMaxHealth)
            m_playerHealth = m_playerMaxHealth;
    }

    public void DamagePlayer(float p_healthToDamage, PiercingType p_piercingType)
    {
        if (m_playerIsInvicible)
            return;
        switch (p_piercingType)
        {
            case PiercingType.NoPiercing:
                m_playerHealth -= p_healthToDamage * m_noPiercingDamagePercentageToHealth;
                m_playerShield -= p_healthToDamage * m_noPiercingDamagePercentageToShield;
                break;

            case PiercingType.LowPiercing:
                m_playerHealth -= p_healthToDamage * m_lowPiercingDamagePercentageToHealth;
                m_playerShield -= p_healthToDamage * m_lowPiercingDamagePercentageToShield;
                break;

            case PiercingType.MediumPiercing:
                m_playerHealth -= p_healthToDamage * m_mediumPiercingDamagePercentageToHealth;
                m_playerShield -= p_healthToDamage * m_mediumPiercingDamagePercentageToShield;
                break;

            case PiercingType.HighPiercing:
                m_playerHealth -= p_healthToDamage * m_highPiercingDamagePercentageToHealth;
                m_playerShield -= p_healthToDamage * m_highPiercingDamagePercentageToShield;
                break;

            case PiercingType.FullPiercing:
                m_playerHealth -= p_healthToDamage * m_fullPiercingDamagePercentageToHealth;
                m_playerShield -= p_healthToDamage * m_fullPiercingDamagePercentageToShield;
                break;
        }

        if (m_playerHealth <= 0)
        {
            m_playerHealth = 0;
        }

        if (m_playerShield <= 0)
        {
            m_playerShield = 0;
        }
    }

    public void RestorePlayerShield(float p_shieldToRestore) 
    {
        m_playerShield += p_shieldToRestore;
        if(m_playerShield < m_playerMaxShield)
            m_playerShield = m_playerMaxShield;
    }

    public void StaminaRegeneration()
    {
        if (m_playerStamina < m_playerMaxStamina)
            m_playerStamina += m_playerStaminaRegenerationPerSecond * Time.deltaTime;
        else if(m_playerStamina > m_playerMaxStamina)
            m_playerStamina = m_playerMaxStamina;
    }

    public void StaminaConsumption()
    {
        if (m_playerStamina > 0)
            m_playerStamina -= m_playerStaminaConsumptionPerSecond * Time.deltaTime;
        else if(m_playerStamina < 0)
            m_playerStamina = 0;
    }


    public void CollectOrb(GameObject orb)
    {
        m_collectedOrbs.Add(orb);
    }
    public void SetInvulnerability(bool p_newValue)
    {
        m_playerIsInvicible = p_newValue;
    }

    //Generic timer for habilities and stuff
    public IEnumerator GenericTimer(float p_habilityCooldown)
    {
        yield return new WaitForSeconds(Time.deltaTime);
    }

    public IEnumerator GenericTimer(float p_habilityCooldown, Action FirstMethod)
    {
        FirstMethod();
        yield return new WaitForSeconds(Time.deltaTime);
    }

    public IEnumerator GenericTimer(float p_habilityCooldown, Action FirstMethod, Action LastMethod)
    {
        FirstMethod();
        yield return new WaitForSeconds(p_habilityCooldown);
        LastMethod();
    }

    public IEnumerator GenericTimer(float p_habilityCooldown, Action LastMethod, int ThisIsJustToAdviceYouThatThisIsAnOverload)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        LastMethod();
    }
}


