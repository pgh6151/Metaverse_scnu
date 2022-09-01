using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원


public class RightVirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform joystick;

    [SerializeField, Range(10, 200)]
    private float leverRange; //레버 범위 제한

    private Vector2 inputDirection;
    private bool isInput;

    [SerializeField] //TPS 사용
    private TPSCharacterController controller;

    public float sensitivity = 1f;

    private void Awake()
    {
        joystick = GetComponent<RectTransform>(); //컴포넌트 불러오기
    }

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData) //터치 or 마우스 클릭
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    // 오브젝트 클릭 후 드래그 하는 도중 계속 호출. 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    //드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero; //레버 중심으로.
        isInput = false;
        controller.Rotate(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        //var inputPosition = eventData.position - joystick.anchoredPosition;

        // 앵커 우하단이라 코드 rect.center로 수정할려고 했으나
        // rect.center가 앵커위치 기준이라 -150 150이 나와서
        // 스크린 사이즈 + 앵커에서 -x 만큼 떨어진 값인 rect x
        // 앵커에서 y만큼 떨어진 값인 rect y를 벡터에 넣었다.
        var rectPos = new Vector2(Screen.width + joystick.rect.x , joystick.rect.y) ;
        var inputPosition = eventData.position - rectPos;
        //normalized : 백터 길이 1로 정규화, 이동 속도 일정
        Debug.Log("eventData.position : " + eventData.position); 
        Debug.Log("joystick.rect.center : " + joystick.rect.center); 
        var inputVector = inputPosition.magnitude < leverRange ? inputPosition : inputPosition.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange; //inputVector는 해상도 기반이라 값이 매우 커 정규화 함
    }

    private void InputControlVector()
    {
        // 캐릭터에게 입력벡터를 전달
        controller.Rotate(inputDirection * sensitivity);
        //Debug.Log(inputDirection.x + " / " + inputDirection.y); 입력받은 벡터의 값
    }

    void Start()
    {
    }

    void Update()
    {
        if(controller.PV.IsMine && isInput)
        {
            InputControlVector();
        }
    }
}