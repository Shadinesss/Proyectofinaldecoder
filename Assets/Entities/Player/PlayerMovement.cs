using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private Rigidbody m_playerRigidBody;
    [SerializeField] private Transform m_playerTransform;
    [Header("Movement Forces")]
    [SerializeField] private float m_runForce;
    [SerializeField] private float m_walkForce;
    [SerializeField] private float m_sprintForce;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private float m_crouchForce;
    [SerializeField] private float m_planeForce;
    [Header("Movement conditions")]
    [SerializeField] private float m_raycastMaxDistanceToValidateFloor;
    [SerializeField] private LayerMask m_floor;
    [Header("Crouch Parameters")]
    [SerializeField] private float m_playerCrouchScale;
    [SerializeField] private float m_downForce;
    [SerializeField] private float m_crouchExitYPositon;
    [Header("Movement Drag")]
    [SerializeField] private float m_dragGrounded;
    [SerializeField] private float m_dragFlying;
    [Header("Extra parameters from Habilities")]
    [SerializeField] private float m_gravityCounterForce;
    [SerializeField] private float m_sprintCooldownTime;

    private StoryGameManager m_gameManager;
    private bool m_playerIsCrouching;
    private bool m_playerCanSprint;
    private float m_xInput;
    private float m_yInput;
    private Vector3 m_direction;
    private Vector3 m_normalScale;

    private void Start()
    {
        m_gameManager = StoryGameManager.Instance;
        m_playerCanSprint = true;
        m_playerIsCrouching = false;
        m_normalScale = transform.localScale;
    }
    private void Update()
    {
        GetMovement();
        DragModifier();
        CrouchManager();
    }

    private void FixedUpdate()
    {
        if(IsGrounded())
        {
            Run();
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            if (m_playerIsCrouching)
            {
                Crawl();
            }
            if (Input.GetKeyUp(KeyCode.CapsLock) || m_gameManager.GetPlayerStamina() < 1)
            {
                m_gameManager.StaminaRegeneration();
                StartCoroutine(SprintCooldown());
            }
            else if (Input.GetKey(KeyCode.CapsLock) && m_playerCanSprint && m_gameManager.IsPlayerShielded()) 
            {
                m_gameManager.StaminaConsumption();
                Sprint();
            }
            else
            {
                m_gameManager.StaminaRegeneration();
            }
            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Plane();
                GravityCounter();
            }
        }
        if(m_gameManager.GetPlayerFlatVelocity() > 0) //&& //Bool m_canSound)
        {
            //Ejecutar una corrutina con un audio
        }
    }

    private void CrouchManager()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ExitCrouch();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private IEnumerator SprintCooldown()
    {
        m_playerCanSprint = false;
        yield return new WaitForSeconds(m_sprintCooldownTime);
        m_playerCanSprint = true;
    }

    private void GetMovement()
    {
        m_xInput = Input.GetAxisRaw("Horizontal");
        m_yInput = Input.GetAxisRaw("Vertical");
        m_direction = transform.forward * Time.fixedDeltaTime * m_yInput + transform.right * m_xInput * Time.fixedDeltaTime;
    }

    private void Run()
    {
        if (m_gameManager.IsPlayerShielded())
            m_playerRigidBody.AddForce(m_direction.normalized * m_runForce, ForceMode.Impulse);
        else
            m_playerRigidBody.AddForce(m_direction.normalized * m_runForce * 0.75f, ForceMode.Impulse);
    }

    private void Walk()
    {
        m_playerRigidBody.AddForce(m_direction.normalized * m_walkForce, ForceMode.Impulse);
    }

    private void Sprint()
    {
        m_playerRigidBody.AddForce(m_direction.normalized * m_sprintForce, ForceMode.Impulse);
    }

    private void Jump()
    {
        m_playerRigidBody.drag = m_dragFlying;
        if (m_gameManager.IsPlayerShielded())
            m_playerRigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        else
            m_playerRigidBody.AddForce(Vector3.up * m_jumpForce * 0.75f, ForceMode.Impulse);
    }

    private void Crouch()
    {
        m_playerIsCrouching = true;
        m_playerTransform.localScale = new Vector3(m_playerTransform.localScale.x, m_playerCrouchScale, m_playerTransform.localScale.z);
        m_playerRigidBody.AddForce(Vector3.down * m_downForce, ForceMode.Impulse);
    }

    private void ExitCrouch()
    {
        m_playerIsCrouching = false;
        m_playerTransform.localScale = new Vector3(m_playerTransform.localScale.x, m_normalScale.y, m_playerTransform.localScale.z);
        m_playerTransform.position += new Vector3(0f, m_playerTransform.position.y, 0f);
    }

    private void Crawl()
    {
        m_playerRigidBody.AddForce(m_direction.normalized * m_crouchForce, ForceMode.Impulse);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(m_playerTransform.position, Vector3.down, m_raycastMaxDistanceToValidateFloor, m_floor);
    }

    private void DragModifier()
    {
        if(IsGrounded())
        {
            m_playerRigidBody.drag = m_dragGrounded;
        }
        else
        {
            m_playerRigidBody.drag = m_dragFlying;
        }
    }

    private void GravityCounter()
    {
        m_playerRigidBody.AddForce(Vector3.up * m_gravityCounterForce, ForceMode.Impulse);
    }

    private void Plane()
    {
        m_playerRigidBody.AddForce(m_direction * m_planeForce, ForceMode.Impulse);
    }

    public bool IsPlayerMoving()
    {
        if (m_playerRigidBody.velocity.magnitude > 0f)
            return true;
        else
            return false;
    }

}
