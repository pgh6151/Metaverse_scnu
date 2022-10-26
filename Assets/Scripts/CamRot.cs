using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //UI 클릭시 터치 제한

public class CamRot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

[SerializeField]
// private Rot camera;
private LookRotate cam;

public Vector2 BeginTouch;
public Vector2 DragTouch;
public Vector2 NowPos;
public bool isInput;
public Transform Player;
public Transform MainCam;

void Start () {
    Debug.Log("start");
    // Player = GameObject.Find("Dummy/Look").transform;
    // this.transform.rotation = Quaternion.Euler(0, 0, 0); //필요한가?
}

public void OnBeginDrag(PointerEventData eventData)
    {
        isInput = true;
        BeginTouch = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var DragTouch = BeginTouch - eventData.position;
        TouchRotate(DragTouch);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isInput = false;
        // MainCam.transform.position += new Vector3(10 ,10 ,10);
        Debug.Log("위치:"+MainCam.transform.localEulerAngles);
        Debug.Log("끝날때 위치:"+MainCam.transform.position);
        Debug.Log("끝날때 각:"+MainCam.transform.rotation);
    }

    private void TouchRotate(Vector2 pos)
        {
            NowPos = pos;
        }

    private void InputVector()
    {
        // Debug.Log(NowPos);
        cam.RotateCam(NowPos);
    }

    // public void Save(Vector3 currentAngles){
        // MainCam.transform.position = new Vector3(SavePos.x, SavePos.y, SavePos.z);
        // if(isInput==false){
        // Debug.Log("a" + currentAngles);
        // MainCam.transform.Rotate(currentAngles.x, currentAngles.y, currentAngles.z);
    // }
    // }

    void Update()
    {
        // transform.LookAt(Player);
        if(isInput){
        InputVector();
        }
        else{
            cam.Current(isInput);
        }
    }

// void Update () {
//     transform.LookAt(Player);
//     // Mathf.Clamp(MovedPos.x, -10, 10);    
//     // Mathf.Clamp(MovedPos.y, -10, 10);
//     Touch t = Input.GetTouch(0); //처음 터치된 손가락
//     if (Input.touchCount >= 1 && EventSystem.current.IsPointerOverGameObject(0) == false && t.position.x >= Screen.width / 2)
//     {
//         switch (t.phase)
//             {
//                 // 처음 터치 위치
//                 case TouchPhase.Began:
//                     // this.transform.rotation = Rotate; //>>>>>>>>>>>>>MovedPos를 저장해야 손을 떼도 위치가 이어지지않나?
//                     // 위치가 아니라 카메라 회전값이 들어가야함?
//                     TouchPos= t.position-t.deltaPosition; //Vector2 - 3 변환
//                     break;

//                 // 현재 터치 위치 - 처음 터치 위치 비교
//                 case TouchPhase.Moved:
                    
//                     // MovedPos = (curPos - TouchPos) * f * Time.deltaTime;
                   
//                     // MovedPos.Normalize();
//                     // MovedPos.x = Mathf.Clamp(MovedPos.x, -30, 30); //Quaternion ?? Euler ??
//                     // MovedPos.y = Mathf.Clamp(MovedPos.y, -40, -40);
//                     NowPos = t.position - t.deltaPosition;
//                     MovedPos = (Vector3)(TouchPos - NowPos); //* f 추가
//                     Rotate = Quaternion.Euler(MovedPos.y, -MovedPos.x, 0);
//                     this.transform.rotation = Rotate;
//                     TouchPos = t.position-t.deltaPosition;
//                     break;

//                 // 터치 끝
//                 case TouchPhase.Ended:
//                     break;
//             }
//         }    
//     }
}
