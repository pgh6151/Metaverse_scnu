using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // [SerializeField]
    // public Vector2 Position;
    // [SerializeField]
    // public Vector2 Distance;

    public bool Touched;

    public float ax;
    public float by;

    // public Touch touch = new Touch();
    [SerializeField]
    private Transform cineCam;

    [SerializeField, Range(0f, 100f)]
    private float xSpeed = 50f;  // 좌우 회전 속도

    [SerializeField, Range(0f, 100f)]
    private float ySpeed = 100f; // 상하 회전 속도

    // void Start() {
    //     GameObject cine = GameObject.Find("CM FreeLook1");
    //     cineCam = cine.GetComponent<Transform>();
    // }
    public float rotateSpeed = 50f;
    public Vector3 axis = new Vector3(0f, 1f, 0f);
    public Vector3 diff = new Vector3(4f, 0f, 0f);
    private float t = 0;

    [SerializeField]
    public GameObject Player;

    void Start() {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if(Touched && Input.touchCount <= 2 ) //ture. ??범위 수정??
        {
            Debug.Log("touch");
            Debug.Log(Input.touchCount);  // 0
            // Debug.Log(Position);
            Touch touch = Input.GetTouch(0); //터치한 손가락 순서에 해당하는 터치의 상태를 나타내는 Touch 구조체를 반환
            // Debug.Log(touch.position);
            // Debug.Log(Input.GetTouch(1).position);
                
        


            RotateAround2(axis, diff, rotateSpeed, ref t);


            // float ax = Input.GetTouch(0).position.x * xSpeed * Time.deltaTime;
            // float by = -Input.GetTouch(0).position.y * ySpeed * Time.deltaTime;

            // Quaternion axR = Quaternion.AngleAxis(ax, Vector3.up);
            // Quaternion byR = Quaternion.AngleAxis(by, Vector3.right);

            // cineCam.RotateAround *= axR.normalized;
            // cineCam.rotation *= byR.normalized;
            
            




            // Debug.Log("0");
            // Debug.Log(Input.GetTouch(0).position);
            // Debug.Log("1");
            // Debug.Log(Input.GetTouch(1).position);
                // Distance = touch.position - Position;
                // Position = touch.position;
                
                
                // if(touch.phase==TouchPhase.Began)
                // Debug.Log("Touch Began!");
        }
        else
        {
        //    Distance =  new Vector2(); //초기화
        Debug.Log("untouched");
        }
    }

    public void OnPointerDown(PointerEventData eventData) //터치하는 순간 실행. 드래그 시 변화X
    {
        Touched = true;
        // ax -= touch.position.x;
        // by -= touch.position.y;
        Debug.Log("okok");
        // Position = eventData.position;
        
    }

    public void OnPointerUp(PointerEventData eventDate) //떼는 순간 실행
    {
        Touched = false;
    }
    private void RotateAround2(in Vector3 axis, in Vector3 diff, float speed, ref float t)
        {
            t += speed * Time.deltaTime;

            Vector3 offset = Quaternion.AngleAxis(t, Vector3.up) * diff;
            // transform.position = Player.position + offset;

            Quaternion rot = Quaternion.LookRotation(-offset, axis);
            transform.rotation = rot;
        }
}
