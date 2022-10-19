using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RotateCamera : MonoBehaviour
{
    // [SerializeField]
    // public Transform cineCamera;

    GameObject Player;

    [SerializeField]
    RotateField RotateField;
    public Vector3 Rotate;

    public float senstivity = 3f;

    void Start() {
        // Player = GameObject.Find("Player");
    }

    void Update()
    {
        // Mathf.Clamp(cineXValue, -45, 45);
        // Mathf.Clamp(cineYValue, 0, 1);


        // xAngle = RotateField.ax * 3f * Time.deltaTime;
        // yAngle = RotateField.by * 3f * Time.deltaTime;


        // rotX -= RotateField.by * Time.deltaTime;
        // rotY += RotateField.ax * Time.deltaTime;
        // cineCamera.rotation = Quaternion.Euler(rotX, rotY, 0f);


        Debug.Log("good");
        

        // cineCamera.Rotate = New Vector3(0,0,0);
        // cineCamera.Rotate(cineCamera.Rotate.y, cineCamera.Rotate.x, Space.World);
        // cineCamera.rotation.x = yAngle;
        // cineCamera.rotation.y = xAngle;


        // cineCamera.rotation = Quaternion.Euler(cineCamera.rotation.y, cineCamera.rotation.x, 0);
   
   
    }

    // public void TouchField()

}
