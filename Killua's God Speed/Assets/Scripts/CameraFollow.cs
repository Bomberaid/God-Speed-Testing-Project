 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    Camera playerCamera;
    public Camera effectCamera;
    public CinemachineFreeLook freeLook;
    public CinemachineVirtualCamera virtualCam;
    CinemachineBrain[] brain = new CinemachineBrain[2];
    //Cinemachine
    //Animator animator;

    //private float distance = 2;
    //public float distanceDefault = 2;

    //private float mouseSensitivity = 750f;
    //public float zoomSensitivity;

    //readonly float smoothSpeed = 0.125f;

    //float MouseXAxis;
    //float MouseYAxis;

    //float mouseZoom;

    //Vector2 MouseMinMax = new Vector2(-90f, 45f);

    //public Vector3 currentRotationVelocity;
    //Vector3 currentRotation;
    CinemachineFreeLook.Orbit[] originalOrbits;

    [Tooltip("The minimum scale for the orbits")]
    [Range(0.01f, 1f)]
    public float minScale = 0.5f;

    [Tooltip("The maximum scale for the orbits")]
    [Range(1f, 5f)]
    public float maxScale = 1;

    [Tooltip("The zoom axis.  Value is 0..1.  How much to scale the orbits")]
    [AxisStateProperty]
    public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "Mouse ScrollWheel", false);

    //private float regularDelay = 0f;
    //private float followDelay;

    private void OnValidate()
    {
        minScale = Mathf.Max(0.01f, minScale);
        maxScale = Mathf.Max(minScale, maxScale);
    }

    private void Awake()
    {
        //freeLook = GetComponentInChildren<CinemachineFreeLook>();
        if (freeLook != null)
        {
            originalOrbits = new CinemachineFreeLook.Orbit[freeLook.m_Orbits.Length];
            for (int i = 0; i < originalOrbits.Length; i++)
            {
                originalOrbits[i].m_Height = freeLook.m_Orbits[i].m_Height;
                originalOrbits[i].m_Radius = freeLook.m_Orbits[i].m_Radius;
            }
#if UNITY_EDITOR
            SaveDuringPlay.SaveDuringPlay.OnHotSave -= RestoreOriginalOrbits;
            SaveDuringPlay.SaveDuringPlay.OnHotSave += RestoreOriginalOrbits;
#endif
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        SaveDuringPlay.SaveDuringPlay.OnHotSave -= RestoreOriginalOrbits;
    }

    private void RestoreOriginalOrbits()
    {
        if (originalOrbits != null)
        {
            for (int i = 0; i < originalOrbits.Length; i++)
            {
                freeLook.m_Orbits[i].m_Height = originalOrbits[i].m_Height;
                freeLook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius;
            }
        }
    }
#endif


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        //playerCamera = GetComponent<Camera>();
        //brain = GetComponent<CinemachineBrain>();
        //animator = player.gameObject.GetComponent<Animator>();

        //distance = distanceDefault;
    }

    #region Unused Code
    //public void CameraSwitch(CinemachineVirtualCamera cinemachineCamera, CinemachineBrain cBrain)
    //{
    //    //if (!CinemachineCore.Instance.IsLive(cinemachineCamera))
    //    //{
    //    //    regularCamera.activ

    //    //    brain.ActiveVirtualCamera.Priority = cinemachineCamera.Priority - 1;
    //    //    cinemachineCamera.Priority++;

    //    //    //brain.OutputCamera.depth;
    //    //}
    //}

    //public void Follow(bool delayed = false)
    //{
    //    //Vector3 smoothFollow = Vector3.Lerp(player.position - transform.forward * distance, transform.position, Time.deltaTime);
    //    //transform.position = smoothFollow;
    //    transform.position = player.position - transform.forward * distance;
    //}

    //public void ChangeDelay(float newDelay)
    //{
    //    followDelay = regularDelay;
    //}

    //void CameraRotation()
    //{
    //    MouseXAxis += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //    MouseYAxis -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //    MouseYAxis = Mathf.Clamp(MouseYAxis, MouseMinMax.x, MouseMinMax.y);

    //    //if (animator.GetBool("GodSpeed") == true)
    //    //{
    //    //    //currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(MouseYAxis, 0f, 0f), ref currentRotationVelocity, smoothSpeed);
    //    //    //transform.eulerAngles = currentRotation;
    //    //}
    //    //if(animator.GetBool("GodSpeed") == false || animator.GetFloat("MovementSpeed") <= 0)
    //    //{
    //    //    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(MouseYAxis, MouseXAxis, 0f), ref currentRotationVelocity, smoothSpeed);
    //    //    transform.eulerAngles = currentRotation;
    //    //}

    //    //currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(MouseYAxis, MouseXAxis, 0f), ref currentRotationVelocity, smoothSpeed);
    //    //transform.eulerAngles = currentRotation;

    //}

    //void ZoomOut()
    //{
    //    mouseZoom = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;

    //    distance = Mathf.Clamp(distance + mouseZoom, -1, 35);

    //    if(Input.GetKeyDown(KeyCode.Mouse2))
    //    {
    //        distance = distanceDefault;
    //    }
    //
    #endregion


    // Update is called once per frame
    void Update()
    {
        //Follow();
        //CameraRotation();
        //ZoomOut();
        //Debug.Log(brain.ActiveVirtualCamera);

        if (originalOrbits != null)
        {
            zAxis.Update(Time.deltaTime);
            float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);
            for (int i = 0; i < originalOrbits.Length; i++)
            {
                freeLook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                freeLook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
            }
        }
    }
}
