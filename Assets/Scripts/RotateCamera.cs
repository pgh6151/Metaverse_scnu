using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineFreeLook cineCamera;

    [SerializeField]
    RotateField rotateField;

    public float senstivity = 3f;

    void Update()
    {
        Mathf.Clamp(cineCamera.m_XAxis.Value, -45, 45);
        Mathf.Clamp(cineCamera.m_YAxis.Value, 0, 1);
        cineCamera.m_XAxis.Value += rotateField.Distance.x * senstivity * 100 * Time.deltaTime;
        cineCamera.m_YAxis.Value += rotateField.Distance.y * senstivity * Time.deltaTime;
    }
}
