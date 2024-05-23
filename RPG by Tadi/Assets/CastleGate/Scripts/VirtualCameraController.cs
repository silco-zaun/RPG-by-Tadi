using Cinemachine;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        // Ensure that the virtual camera reference is not null
        if (virtualCamera != null)
        {
            // Set the follow target of the virtual camera to the specified target
            virtualCamera.Follow = PlayerUnitController.Ins.gameObject.transform;
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera reference is missing.");
        }
    }
}
