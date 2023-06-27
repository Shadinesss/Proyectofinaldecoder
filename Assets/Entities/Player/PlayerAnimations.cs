using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator m_playerAnimator;

    private void Awake()
    {
        m_playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento hacia adelante
        if (Input.GetKey(KeyCode.W))
        {
            m_playerAnimator.SetBool("MovingForward", true);
            m_playerAnimator.SetBool("MovingLeft", false);
            m_playerAnimator.SetBool("MovingRight", false);
            m_playerAnimator.SetBool("MovingBackward", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_playerAnimator.SetBool("MovingForward", false);
            m_playerAnimator.SetBool("MovingLeft", true);
            m_playerAnimator.SetBool("MovingRight", false);
            m_playerAnimator.SetBool("MovingBackward", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_playerAnimator.SetBool("MovingForward", false);
            m_playerAnimator.SetBool("MovingLeft", false);
            m_playerAnimator.SetBool("MovingRight", true);
            m_playerAnimator.SetBool("MovingBackward", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_playerAnimator.SetBool("MovingForward", false);
            m_playerAnimator.SetBool("MovingLeft", false);
            m_playerAnimator.SetBool("MovingRight", false);
            m_playerAnimator.SetBool("MovingBackward", true);
        }
        else
        {
            m_playerAnimator.SetBool("MovingForward", false);
            m_playerAnimator.SetBool("MovingLeft", false);
            m_playerAnimator.SetBool("MovingRight", false);
            m_playerAnimator.SetBool("MovingBackward", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
            m_playerAnimator.SetTrigger("Jumping");
        if (Input.GetMouseButtonDown(0))
            m_playerAnimator.SetTrigger("Shooting");
    }
}

