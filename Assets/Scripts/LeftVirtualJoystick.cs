using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원


public class LeftVirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public RectTransform lever;
    public RectTransform joystick;
    [SerializeField] private float boostTimer = 0f;

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
        boostTimer = 0;
        ControlJoystickLever(eventData);
        isInput = true;
    }

    // 오브젝트 클릭 후 드래그 하는 도중 계속 호출. 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnDrag(PointerEventData eventData)
    {
        CharacterBoost();
        ControlJoystickLever(eventData);
    }

    private void CharacterBoost()
    {
        // TODO 나중에 버튼으로 추가할 생각이 있음
        /*boostTimer += Time.deltaTime;
        if (boostTimer >= 1f)
        {
            controller.boost = 2;
        }
        else
        {
            controller.boost = 1;
        }*/
    }

    //드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero; //레버 중심으로.
        isInput = false;
        controller.Move(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        //var inputPosition = eventData.position - joystick.anchoredPosition;
        // 앵커 좌하단이라 코드 rect.center로 수정
        var inputPosition = eventData.position - joystick.rect.center;
        //normalized : 백터 길이 1로 정규화, 이동 속도 일정
        var inputVector = inputPosition.magnitude < leverRange ? inputPosition : inputPosition.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange; //inputVector는 해상도 기반이라 값이 매우 커 정규화 함
    }

    private void InputControlVector()
    {
        // 캐릭터에게 입력벡터를 전달
        controller.Move(inputDirection * sensitivity);
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