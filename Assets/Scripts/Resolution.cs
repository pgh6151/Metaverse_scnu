using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class thisCanvas : MonoBehaviour
{
    //Default 해상도 비율
    float fixedAspectRatio = 9f / 16f;
    // //현재 해상도의 비율
    float currentAspectRatio = (float)Screen.width / (float)Screen.height;
    // int matchWidthOrHeight =  0;

    // Canvas.matchWidthOrHeight = currentAspectRatio > fixedAspectRatio ? 0 : 1;

    // int matchWidthOrHeight = (currentAspectRatio > fixedAspectRatio) ? 0  : 1;

    // // //현재 해상도 가로 비율이 더 길 경우
    // if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;       
    // // //현재 해상도의 세로 비율이 더 길 경우
    // else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;
}
