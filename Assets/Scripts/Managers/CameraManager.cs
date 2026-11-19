using UnityEngine;

public enum eCameraPosition { main, center, overhead }

/// <summary>
/// Manager: Controls camera movement & viewing angles
/// </summary>
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [NamedArray(typeof(eCameraPosition))] public Transform[] CameraPositions;
    [SerializeField] public eCameraPosition CurrentCameraPosition;
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraTransform;

    void Awake()
    {
        Instance = this;
    }

    public static void SetCurrentCamera(eCameraPosition _cameraPosition, Transform _target = null)
    {
        Instance.CurrentCameraPosition = _cameraPosition;
        Instance.target = _target;

        if (Instance.CurrentCameraPosition != eCameraPosition.overhead)
        {
            Instance.cameraTransform.SetParent(Instance.CameraPositions[(int)Instance.CurrentCameraPosition]);
        }
        else
        {
            Instance.cameraTransform.SetParent(PlayerManager.GetCurrentPlayer().CameraPosition);
        }

        Instance.cameraTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    void LateUpdate()
    {
        if (target && CurrentCameraPosition == eCameraPosition.center)
        {
            cameraTransform.LookAt(target);
        }
    }
}
