using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Collectable m_data;
    private PlayerManager m_playerManager;
    private void Awake()
    {
        m_playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && m_playerManager.GetPlayerShield() < m_playerManager.GetPlayerMaxShield())
        {
            m_playerManager.RestorePlayerShield(m_data.CollectablePointsGiven());
            Destroy(gameObject);
        }
    }
}

