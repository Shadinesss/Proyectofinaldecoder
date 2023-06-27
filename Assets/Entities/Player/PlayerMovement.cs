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
    [SerializeField] private LayerMask m_prop;
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
    [SerializeField] private AudioSource m_footstep;
    //[SerializeField] private AudioSource m_footstep2;
    [SerializeField] private AudioSource m_jump;

    private bool m_canPlaySound1 = true;
    private bool m_canPlaySound2 = true;
    private PlayerManager m_playerManager;
    private bool m_playerIsCrouching;
    private bool m_playerCanSprint;
    private float m_xInput;
    private float m_yInput;
    private Vector3 m_direction;
    private Vector3 m_normalScale;

    private void Start()
    {
        m_playerManager = PlayerManager.Instance;
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
            if (m_xInput != 0 || m_yInput != 0)
                FootstepSound();
            Run();
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            if (m_playerIsCrouching)
            {
                Crawl();
            }
            if (Input.GetKeyUp(KeyCode.CapsLock) || m_playerManager.GetPlayerStamina() < 1)
            {
                m_playerManager.StaminaRegeneration();
                StartCoroutine(SprintCooldown());
            }
            else if (Input.GetKey(KeyCode.CapsLock) && m_playerCanSprint && m_playerManager.IsPlayerShielded()) 
            {
                m_playerManager.StaminaConsumption();
                Sprint();
            }
            else
            {
                m_playerManager.StaminaRegeneration();
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
        m_playerRigidBody.AddForce(m_direction.normalized * m_runForce, ForceMode.Impulse);
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
        JumpSound();
        m_playerRigidBody.drag = m_dragFlying;
        m_playerRigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
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
        if (Physics.Raycast(m_playerTransform.position, Vector3.down, m_raycastMaxDistanceToValidateFloor, m_floor) || Physics.Raycast(m_playerTransform.position, Vector3.down, m_raycastMaxDistanceToValidateFloor, m_prop))
            return true;
        else
            return false;
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
    private void FootstepSound()
    {
        if (!m_canPlaySound1)
            return;
        m_footstep.Play();
        StartCoroutine(FootstepSoundCooldown(0.4f));
    }
    private void JumpSound()
    {
        if (!m_canPlaySound2)
            return;
        m_jump.Play();
        StartCoroutine(JumpSoundCooldown(0.7f));
    }
    private IEnumerator FootstepSoundCooldown(float time)
    {
        m_canPlaySound1 = false;
        yield return new WaitForSeconds(time);
        m_canPlaySound1 = true;
    }
    private IEnumerator JumpSoundCooldown(float time)
    {
        m_canPlaySound2 = false;
        yield return new WaitForSeconds(time);
        m_canPlaySound2 = true;
    }
}
