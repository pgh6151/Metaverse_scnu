using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public Vector2 Position;
    [SerializeField]
    public Vector2 Distance;
    [SerializeField]
    public bool Touched;

    void Update()
    {
        if(Touched && Input.touchCount <= 1) //ture. ??범위 수정??
        {
            Debug.Log("touch");
            Debug.Log(Input.touchCount);  // 0
            Debug.Log(Position);
            Touch touch = Input.GetTouch(0); //터치한 손가락 순서에 해당하는 터치의 상태를 나타내는 Touch 구조체를 반환
            // Debug.Log(touch.position);
            // Debug.Log(Input.GetTouch(1).position);
                Distance = touch.position - Position;
                Position = touch.position;
                // if(touch.phase==TouchPhase.Began)
                // Debug.Log("Touch Began!");
        }
        else
        {
           Distance =  new Vector2(); //초기화
        }
    }

    public void OnPointerDown(PointerEventData eventData) //터치하는 순간 실행. 드래그 시 변화X
    {
        Touched = true;
        Position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventDate) //떼는 순간 실행
    {
        Touched = false;
    }

}
