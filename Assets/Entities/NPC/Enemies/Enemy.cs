using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    private Transform m_playerTransform;
    [SerializeField] private float m_raycastMovementMaxDistance;
    [SerializeField] private Transform m_raycastOrigin;
    private float m_attackCooldown;
    private float m_currentHealth;
    private Rigidbody m_rigidbody;
    private bool m_canAttack = true;
    private Vector3 m_directionToPlayer;
    [SerializeField] private EnemyData m_data;
    [SerializeField] private GameObject m_shield;
    [SerializeField] private LayerMask Walls;
    private PlayerManager m_playerManager;
    private Animator m_animator;
    private void Awake()
    {
        m_playerManager = FindObjectOfType<PlayerManager>();
        m_playerTransform = FindObjectOfType<PlayerManager>().transform;
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerManager = FindObjectOfType<PlayerManager>();
        m_currentHealth = m_data.GetMaxHealth();
    }
    private void Start()
    {
        m_animator.SetBool("IsMoving", false);
    }
    private void Update()
    {
        if (m_currentHealth <= 0)
        {
            Die();
        }
        m_directionToPlayer = m_playerTransform.position - transform.position;
        if (IDontSeePlayer())
            return;
        else if (m_directionToPlayer.magnitude < m_data.GetDistanceToStartChasing() && m_directionToPlayer.magnitude > m_data.GetDistanceToStartAttacking())
        {
            ChaseTarget();
        }
        else if (m_directionToPlayer.magnitude < m_data.GetDistanceToStartAttacking())
        {
            AttackTarget();
        }
            
    }
    private bool IDontSeePlayer()
    {
        return Physics.Raycast(transform.position, m_directionToPlayer, Mathf.Infinity, Walls);
    }
    public void TakeDamage(float damage)
    {
        m_currentHealth -= damage;
    }
    protected void Move(Vector3 direction)
    {
        m_rigidbody.AddForce(direction.normalized * m_data.GetMovementForce(), ForceMode.Impulse);
    }
    protected override void ChaseTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(m_directionToPlayer, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * m_data.GetRotationSpeed());
        Vector3 chaseDirection = m_directionToPlayer.normalized;
        Move(chaseDirection);
    }
    protected override void AttackTarget()
    {
        if(!m_canAttack)
            return;
        else
        {
            m_playerManager.DamagePlayer(m_data.GetGeneralDamage(), m_data.GetAttackPiercingType());
            StartCoroutine(AttackCooldown(1/m_data.GetAttackCadency()));
        }
    }
    protected override void Die()
    {
        Instantiate(m_shield, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public IEnumerator AttackCooldown(float timeToDamageAgain)
    {
        m_canAttack = false;
        yield return new WaitForSeconds(timeToDamageAgain);
        m_canAttack = true;
    }
    protected void StopMoving()
    {
        m_rigidbody.velocity = Vector3.zero;
    }
}


