using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    public Transform Player; //카메라 전환 시 LookAt
    // public Transform moveCamera;

    void Start(){
        // Player = GameObject.Find("Dummy/Look").transform; //플레이어 시점에서 보기
        // transform.LookAt(Player); //플레이어 위치 바라보기
    }

    public void RotateCam(Vector3 NowPos)
        {
            // this.transform.rotation = Quaternion.Euler(NowPos);
            Vector2 movedPos = NowPos.normalized;
            Vector3 Angle = transform.position;
            float x = Angle.x + movedPos.x;
            float y = Angle.y - movedPos.y;
            transform.position = new Vector3(x, y, 0);
        }
}
