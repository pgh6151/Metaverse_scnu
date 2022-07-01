using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTest : MonoBehaviour
{
    int rotation;
    float xRotation;
    void Start()
    {

    }

    void Update()
    {

        float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;
        // 아래로 회전 
        // transform.Rotate(Vector3.up);
        // rotation++;
        // 아래로 회전
        xRotation += mouseY;
        transform.localRotation = Quaternion.Euler(0f, xRotation, 0);
        // y축이 왼쪽으로 되었음
    }
}
