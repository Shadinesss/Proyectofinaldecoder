using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "Data/NPC")]
public class NPCData : ScriptableObject
{
    [Header("NPC Stats")]
    public float m_maxHealth;
    public float m_maxShield;
    public float m_movementForce;
    public float m_generalDamage;
    public float m_weakPointDamage;
    public float m_attackCadency;
}
