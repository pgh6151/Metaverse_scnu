using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
Vector3 TouchPos;
Vector3 MovedPos;

[SerializeField]
private float f = 1f;

public GameObject Player; //카메라 전환 시 LookAt

void Start () {
    Player = GameObject.Find("Player(Clone)");
    transform.LookAt(Player.transform);
    this.transform.rotation = Quaternion.Euler(MovedPos.y*f, -MovedPos.x*f, 0);
}

void Update () {
    // transform.LookAt(Player.transform.position);
    
    if (Input.touchCount > 0) //터치 입력
    {
        Touch t = Input.GetTouch(0); //처음 터치된 손가락

        switch (t.phase)
            {
                // 처음 터치 위치
                case TouchPhase.Began:
                    TouchPos = t.position;
                    Debug.Log("BeganBeganBegan");
                    break;

                // 현재 터치 위치 - 처음 터치 위치 비교
                case TouchPhase.Moved:
                    Vector3 curPos= t.position; //Vector2 - 3 변환
                    MovedPos = curPos - TouchPos;
                    this.transform.rotation = Quaternion.Euler(MovedPos.y*f, -MovedPos.x*f, 0);
                    Debug.Log("MovedMovedMoved");
                    break;

                // 터치 끝
                case TouchPhase.Ended:
                    Debug.Log("EndedEndedEnded");
                    break;
            }
        }    
    }
}
