using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField] private Collectable m_shield;
    [SerializeField] private StoryGameManager m_gameManager;
    [SerializeField] private string m_playerTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_playerTag) && m_gameManager.GetPlayerShield() < m_gameManager.GetPlayerMaxShield())
        {
            m_gameManager.RestorePlayerShield(m_shield.CollectablePointsGiven());
            Destroy(gameObject);
        }
    }
}

