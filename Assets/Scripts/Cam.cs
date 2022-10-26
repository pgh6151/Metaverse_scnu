using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //UI 클릭시 터치 제한

public class Cam : MonoBehaviour
{
public Vector2 TouchPos;
public Vector2 NowPos;
public Vector3 MovedPos;
Quaternion Rotate;
private float Pos;

[SerializeField]
private float f = 1f;

public Transform Player; //카메라 전환 시 LookAt

void Start () {
    Player = GameObject.Find("Dummy/Look").transform; //플레이어 시점에서 보기
    transform.LookAt(Player); //플레이어 위치 바라보기
    // this.transform.rotation = Quaternion.Euler(0, 0, 0); //필요한가?
}

void Update () {
    transform.LookAt(Player);
    // Mathf.Clamp(MovedPos.x, -10, 10);    
    // Mathf.Clamp(MovedPos.y, -10, 10);
    Touch t = Input.GetTouch(0); //처음 터치된 손가락
    if (Input.touchCount >= 1 && EventSystem.current.IsPointerOverGameObject(0) == false && t.position.x >= Screen.width / 2)
    {
        switch (t.phase)
            {
                // 처음 터치 위치
                case TouchPhase.Began:
                    // this.transform.rotation = Rotate; //>>>>>>>>>>>>>MovedPos를 저장해야 손을 떼도 위치가 이어지지않나?
                    // 위치가 아니라 카메라 회전값이 들어가야함?
                    TouchPos= t.position-t.deltaPosition; //Vector2 - 3 변환
                    break;

                // 현재 터치 위치 - 처음 터치 위치 비교
                case TouchPhase.Moved:
                    
                    // MovedPos = (curPos - TouchPos) * f * Time.deltaTime;
                   
                    // MovedPos.Normalize();
                    // MovedPos.x = Mathf.Clamp(MovedPos.x, -30, 30); //Quaternion ?? Euler ??
                    // MovedPos.y = Mathf.Clamp(MovedPos.y, -40, -40);
                    NowPos = t.position - t.deltaPosition;
                    MovedPos = (Vector3)(TouchPos - NowPos); //* f 추가
                    Rotate = Quaternion.Euler(MovedPos.y, -MovedPos.x, 0);
                    this.transform.rotation = Rotate;
                    TouchPos = t.position-t.deltaPosition;
                    break;

                // 터치 끝
                case TouchPhase.Ended:
                    break;
            }
        }    
    }
}
