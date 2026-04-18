using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera cam1;
    public CinemachineCamera cam2;
    public CinemachineCamera cam3;

    void Start()
    {
        ActivateCamera(cam1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateCamera(cam1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateCamera(cam2);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateCamera(cam3);
    }

    void ActivateCamera(CinemachineCamera activeCam)
    {
        cam1.Priority = 0;
        cam2.Priority = 0;
        cam3.Priority = 0;

        activeCam.Priority = 10;
    }
}