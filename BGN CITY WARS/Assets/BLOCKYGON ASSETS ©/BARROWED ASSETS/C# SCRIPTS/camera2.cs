using UnityEngine;
using System.Collections;

public class camera2 : MonoBehaviour
{
    #region Inspector Properties

    public Transform target;
    [Tooltip("Lerp speed between Camera States")]
    public float smoothCameraRotation = 12f;
    [Tooltip("What layer will be culled")]
    public LayerMask cullingLayer = 1 << 0;
    [Tooltip("Debug purposes, lock the camera behind the character for better align the states")]
    public bool lockCamera;

    public float rightOffset = 0f;
    public float defaultDistance = 2.5f;
    public float height = 1.4f;
    public float smoothFollow = 10f;
    public float xMouseSensitivity = 3f;
    public float yMouseSensitivity = 3f;
    public float yMinLimit = -40f;
    public float yMaxLimit = 80f;

    [Tooltip("Spread factor for the culling rays")]
    public float raySpread = 1f;

    [Tooltip("Maximum distance for the culling rays")]
    public float rayDistance = 3f;

    [Tooltip("Speed of smooth culling transitions")]
    public float cullingSmoothSpeed = 5f;

    #endregion

    #region Hide Properties

    [HideInInspector]
    public int indexList, indexLookPoint;

    public float offSetPlayerPivot;
    [HideInInspector]
    public string currentStateName;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Vector2 movementSpeed;

    public Transform targetLookAt;

    private Vector3 currentTargetPos;
    private Vector3 current_cPos;
    private Vector3 desired_cPos;
    private Camera _camera;

    private float distance;
    private float targetDistance;
    private float mouseY;
    private float mouseX;

    private float currentHeight;
    private float targetHeight;
    public float cullingDistance;
    [SerializeField]
    private float checkHeightRadius = 0.4f;

    private float forward = -1f;
    [SerializeField]
    private float xMinLimit = -360f;
    [SerializeField]
    private float xMaxLimit = 360f;
    [SerializeField]
    private float cullingHeight = 0.2f;
    [SerializeField]
    private float cullingMinDist = 0.1f;

    #endregion

    #region Unity Methods

    void Start()
    {
        Init();

        xMouseSensitivity = ES3.Load<float>("GeneralSense");
        yMouseSensitivity = ES3.Load<float>("GeneralSense");
    }

    void LateUpdate()
    {
        if (target == null || targetLookAt == null) return;

        CameraMovement();
    }

    void OnDrawGizmos()
    {
        if (currentTarget == null || !Application.isPlaying) return;

        DrawCullingRays();
    }

    #endregion

    #region Initialization

    public void Init()
    {
        if (target == null)
            return;

        _camera = GetComponent<Camera>();
        currentTarget = target;
        currentTargetPos = new Vector3(currentTarget.position.x, currentTarget.position.y + offSetPlayerPivot, currentTarget.position.z);

        targetLookAt = new GameObject("targetLookAt").transform;
        targetLookAt.position = currentTarget.position;
        targetLookAt.hideFlags = HideFlags.HideInHierarchy;
        targetLookAt.rotation = currentTarget.rotation;

        mouseY = currentTarget.eulerAngles.x;
        mouseX = currentTarget.eulerAngles.y;

        distance = defaultDistance;
        targetDistance = defaultDistance;
        currentHeight = height;
        targetHeight = height;
    }

    #endregion

    #region Camera Control

    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget ? newTarget : target;
    }

    public void SetMainTarget(Transform newTarget)
    {
        target = newTarget;
        currentTarget = newTarget;
        mouseY = currentTarget.rotation.eulerAngles.x;
        mouseX = currentTarget.rotation.eulerAngles.y;
        Init();
    }

    public Ray ScreenPointToRay(Vector3 Point)
    {
        return this.GetComponent<Camera>().ScreenPointToRay(Point);
    }

    public void RotateCamera(float x, float y)
    {
        mouseX += x * xMouseSensitivity;
        mouseY -= y * yMouseSensitivity;

        movementSpeed.x = x;
        movementSpeed.y = -y;
        if (!lockCamera)
        {
            mouseY = ClampAngle(mouseY, yMinLimit, yMaxLimit);
            mouseX = ClampAngle(mouseX, xMinLimit, xMaxLimit);
        }
        else
        {
            mouseY = currentTarget.root.localEulerAngles.x;
            mouseX = currentTarget.root.localEulerAngles.y;
        }
    }

    void CameraMovement()
    {
        if (currentTarget == null)
            return;

        targetDistance = defaultDistance;
        targetHeight = height;

        cullingDistance = Mathf.Lerp(cullingDistance, targetDistance, smoothFollow * Time.deltaTime);
        var camDir = (forward * targetLookAt.forward) + (rightOffset * targetLookAt.right);
        camDir = camDir.normalized;

        var targetPos = new Vector3(currentTarget.position.x, currentTarget.position.y + offSetPlayerPivot, currentTarget.position.z);
        currentTargetPos = targetPos;
        desired_cPos = targetPos + new Vector3(0, height, 0);
        current_cPos = currentTargetPos + new Vector3(0, currentHeight, 0);
        RaycastHit hitInfo;

        if (Physics.SphereCast(targetPos, checkHeightRadius, Vector3.up, out hitInfo, cullingHeight + 0.2f, cullingLayer))
        {
            var t = hitInfo.distance - 0.2f;
            t -= height;
            t /= (cullingHeight - height);
            cullingHeight = Mathf.Lerp(height, cullingHeight, Mathf.Clamp(t, 0.0f, 1.0f));
        }

        if (AnyCullingRayCast(current_cPos, camDir, out hitInfo, rayDistance, cullingLayer))
        {
            targetDistance = hitInfo.distance - 0.2f;
            if (targetDistance < defaultDistance)
            {
                var t = hitInfo.distance;
                t -= cullingMinDist;
                t /= cullingMinDist;
                targetHeight = Mathf.Lerp(cullingHeight, height, Mathf.Clamp(t, 0.0f, 1.0f));
                current_cPos = currentTargetPos + new Vector3(0, targetHeight, 0);
            }
        }

        distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * cullingSmoothSpeed);
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * cullingSmoothSpeed);

        var lookPoint = current_cPos + targetLookAt.forward * 2f;
        lookPoint += (targetLookAt.right * Vector3.Dot(camDir * (distance), targetLookAt.right));
        targetLookAt.position = current_cPos;

        Quaternion newRot = Quaternion.Euler(mouseY, mouseX, 0);
        targetLookAt.rotation = Quaternion.Slerp(targetLookAt.rotation, newRot, smoothCameraRotation * Time.deltaTime);
        transform.position = current_cPos + (camDir * (distance));
        var rotation = Quaternion.LookRotation((lookPoint) - transform.position);

        transform.rotation = rotation;
        movementSpeed = Vector2.zero;
    }

    bool AnyCullingRayCast(Vector3 from, Vector3 camDir, out RaycastHit hitInfo, float maxDistance, LayerMask cullingLayer)
    {
        bool value = false;
        hitInfo = new RaycastHit();
        RaycastHit tempHitInfo;

        Vector3[] directions = new Vector3[]
        {
            camDir,
            camDir + (transform.up * raySpread),
            camDir - (transform.up * raySpread),
            camDir + (transform.right * raySpread),
            camDir - (transform.right * raySpread)
        };

        float closestDistance = maxDistance;
        bool anyHit = false;

        foreach (var direction in directions)
        {
            if (Physics.Raycast(from, direction, out tempHitInfo, maxDistance, cullingLayer))
            {
                anyHit = true;
                if (tempHitInfo.distance < closestDistance)
                {
                    hitInfo = tempHitInfo;
                    closestDistance = tempHitInfo.distance;
                }
            }
        }

        if (anyHit)
        {
            value = true;
            cullingDistance = closestDistance;
        }

        return value;
    }

    void DrawCullingRays()
    {
        Vector3[] directions = new Vector3[]
        {
            transform.forward * rayDistance,
            (transform.forward + transform.up * raySpread) * rayDistance,
            (transform.forward - transform.up * raySpread) * rayDistance,
            (transform.forward + transform.right * raySpread) * rayDistance,
            (transform.forward - transform.right * raySpread) * rayDistance
        };

        Gizmos.color = Color.red;

        foreach (var direction in directions)
        {
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }

        Gizmos.color = Color.yellow;

        foreach (var direction in directions)
        {
            Gizmos.DrawSphere(transform.position + direction, 0.1f);
        }
    }

    #endregion

    #region Helper Methods

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    #endregion
}
