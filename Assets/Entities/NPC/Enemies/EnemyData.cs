using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private string m_npcName;
    [SerializeField] private string m_npcDescription;
    [Header("Life stats")]
    [SerializeField] private float m_maxHealth;
    //[SerializeField] private float m_maxShield;
    [Header("Movement stats")]
    [SerializeField] private float m_movementForce;
    [Header("Attack stats")]
    [SerializeField] private float m_generalDamage;
    //[SerializeField] private float m_weakPointDamage;
    [SerializeField] private float m_attackCadency;
    [SerializeField] private PlayerManager.PiercingType m_attackPiercingType;
    [Header("Extra stats")]
    //[SerializeField] private float m_healPerSecondWhileNotInCombat;
    [SerializeField] private float m_distanceToStartTracking;
    [SerializeField] private float m_distanceToStartChasing;
    [SerializeField] private float m_distanceToStartAttacking;
    [SerializeField] private float m_distanceToStopTracking;
    [SerializeField] private float m_distanceToStopChasing;
    [SerializeField] private float m_distanceToStopAttacking;
    [SerializeField] private float m_rotationSpeed;
    public AudioClip fireSound;
    public AudioClip idleSound;
    public float GetRotationSpeed()
    {
        return m_rotationSpeed;
    }
    public PlayerManager.PiercingType GetAttackPiercingType()
    {
        return m_attackPiercingType;
    }
    public string GetNPCName()
    {
        return m_npcName;
    }
    public string GetNPCDescription()
    {
        return m_npcDescription;
    }
    public float GetMaxHealth()
    {
        return m_maxHealth;
    }
    public float GetMovementForce()
    {
        return m_movementForce;
    }
    public float GetGeneralDamage()
    {
        return m_generalDamage;
    }
    public float GetAttackCadency()
    {
        return m_attackCadency;
    }
    public float GetDistanceToStartTracking()
    {
        return m_distanceToStartTracking;
    }
    public float GetDistanceToStartChasing()
    {
        return m_distanceToStartChasing;
    }
    public float GetDistanceToStartAttacking()
    {
        return m_distanceToStartAttacking;
    }
    public float GetDistanceToStopTracking()
    {
        return m_distanceToStopTracking;
    }
    public float GetDistanceToStopChasing()
    {
        return m_distanceToStopChasing;
    }
    public float GetDistanceToStopAttacking()
    {
        return m_distanceToStopAttacking;
    }
}
