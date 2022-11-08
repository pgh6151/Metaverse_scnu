using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resolution : MonoBehaviour
{
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)20 / 9);
        float scaleWidth = 1f / scaleHeight;
        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        camera.rect = rect;
    }

    void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
    /*//Default 해상도 비율
    float fixedAspectRatio = 9f / 16f;
    // //현재 해상도의 비율
    float currentAspectRatio = (float)Screen.width / (float)Screen.height;
    // int matchWidthOrHeight =  0;

    // Canvas.matchWidthOrHeight = currentAspectRatio > fixedAspectRatio ? 0 : 1;

    // int matchWidthOrHeight = (currentAspectRatio > fixedAspectRatio) ? 0  : 1;

    // // //현재 해상도 가로 비율이 더 길 경우
    // if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;       
    // // //현재 해상도의 세로 비율이 더 길 경우
    // else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;*/
}
