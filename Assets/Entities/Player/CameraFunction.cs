using UnityEngine;
using Cinemachine;
using System.Collections;
public class CameraFunction : MonoBehaviour
{
    [Header("Cameras setup")]
    [SerializeField] private GameObject m_firstPersonCamera;
    [SerializeField] private GameObject m_thirdPersonCamera;
    [SerializeField] private GameObject m_transitionCamera;
    //[SerializeField] private GameObject m_overlayCamera;
    [Header("Camera mathematics")]
    public float m_xSensibility;
    public float m_ySensibility;
    [SerializeField] private float m_timeForTransitionBetweenCameras = 0.1f;
    [SerializeField] private float m_timeForResetingCamerasRotation = 0.1f;
    [Header("Extra")]
    [SerializeField] private GameObject m_thingToMoveWeaponCrosshair;

    private float m_xRotation;
    private float m_yRotation;
    private void Awake()
    {
        m_firstPersonCamera.SetActive(true);
        m_thirdPersonCamera.SetActive(false);
        m_transitionCamera.SetActive(false);
        //m_overlayCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        GetAxisInputs();
        m_thingToMoveWeaponCrosshair.transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0f);
        MoveFirstPersonCamera();
        MoveThirdPersonCamera();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(m_firstPersonCamera.activeSelf)
            {
                ActivateTransition(m_firstPersonCamera, m_thirdPersonCamera, m_timeForTransitionBetweenCameras);
            }
            if(m_thirdPersonCamera.activeSelf) 
            {
                ActivateTransition(m_thirdPersonCamera, m_firstPersonCamera, m_timeForTransitionBetweenCameras);
            }
        }
    }
    private void GetAxisInputs()
    {
        float xInput = Input.GetAxisRaw("Mouse X") * m_xSensibility * Time.deltaTime;
        float yInput = Input.GetAxisRaw("Mouse Y") * m_ySensibility * Time.deltaTime;
        m_yRotation += xInput;
        m_xRotation -= yInput;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);
    }
    private void MoveFirstPersonCamera()
    {
        m_firstPersonCamera.transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0f);
        transform.rotation = Quaternion.Euler(0f, m_yRotation, 0f);
    }
    private void MoveThirdPersonCamera() 
    {
        m_thirdPersonCamera.transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0f);
        transform.rotation = Quaternion.Euler(0f, m_yRotation, 0f);
    }
    private void ActivateTransition(GameObject p_activeCamera, GameObject p_inactiveCamera, float p_transitionTime)
    {
        ResetCamerasRotation(m_timeForResetingCamerasRotation);
        p_activeCamera.SetActive(false);
        m_transitionCamera.SetActive(true);
        StartCoroutine(Transition(p_transitionTime, p_inactiveCamera));
    }
    private void ResetCamerasRotation(float p_time)
    {
        m_firstPersonCamera.transform.rotation = Quaternion.Lerp(m_firstPersonCamera.transform.rotation, Quaternion.identity, p_time);
        m_thirdPersonCamera.transform.rotation = Quaternion.Lerp(m_thirdPersonCamera.transform.rotation, Quaternion.identity, p_time);
    }
    public IEnumerator Transition(float p_time, GameObject p_inactiveCamera)
    {
        yield return new WaitForSeconds(p_time);
        p_inactiveCamera.SetActive(true);
    }
}














