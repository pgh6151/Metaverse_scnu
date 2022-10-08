using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TPSCharacterController : MonoBehaviourPunCallbacks, IPunObservable
{
    private static TPSCharacterController s_Instance = null;
    private float rotationX;
    
    private Rigidbody _rigidbody;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 10f;

    [SerializeField] private float animMod = 0.03f;
    
    [SerializeField] private Transform characterBody; //캐릭터
    [SerializeField] private Transform moveCamera; //카메라

    Animator animator;

    public int boost = 1;

    //건하 수정 포톤뷰에서 부드러운 움직임을 위한 추가
    Vector3 curPos;
    public PhotonView PV;
    public TextMesh NickNameText;

    [SerializeField] private GameObject Cam; // 자기 자신일때만 카메라 활성화
    [SerializeField] private GameObject MiniCanv;

    [SerializeField] private GameObject Joystick; // 자기 자신일때만 조이스틱 활성화

    [SerializeField] private int radius = 1;


    
    private void Awake() {
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.blue;

        if(PV.IsMine)
        {
            Cam.SetActive(true);
            Joystick.SetActive(true);
        }else
        {
            Cam.SetActive(false);
            Joystick.SetActive(false);
            
        }

        if(s_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
        if(SceneManagerHelper.ActiveSceneName == "Minigame1")
        {
            Joystick.SetActive(false);
            MiniCanv.SetActive(true);

        }else if(SceneManagerHelper.ActiveSceneName == "CinemachineScene")
        {
            Joystick.SetActive(true);
            MiniCanv.SetActive(false);

        }
    
        if(PV.IsMine)
        {
            
        }   
        else if ((transform.position - curPos).sqrMagnitude >= 100)transform.position = curPos;  // 위치동기화 시키는 부분
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);

        if(MIniGamemanager.Instance.ST == true)
        {
            transform.position += MIniGamemanager.Instance.MoveVec * 3f * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        if (Physics.CheckSphere(transform.position, radius))
        {
            animator.SetBool("Jump", false);
        }

        MoveAnimation();
    }

    private void MoveAnimation()
    {
        float animSpeed = Mathf.Clamp(_rigidbody.velocity.z * animMod, -1f, 1f);
        animator.SetFloat("Speed", animSpeed * boost);
    }

    public void Move(Vector2 inputDirection)
    {
        // 이동 방향 입력 값 가져오기. 입력과 이동 구분.
        Vector2 moveInput = inputDirection;
        // 이동 방향 입력 판정 : 이동 방향 벡터 길이가 0보다 크면 입력이 발생하고 있는 중
        bool IsWalking = moveInput.magnitude != 0;
        // 입력이 발생하는 중이라면 이동 애니메이션 재생
        // animator.SetBool("IsWalking", IsWalking);
        if (IsWalking)
        {
            /*// 앞뒤이동. y = 0f이라 up/down 고정
            Vector3 lookForward = new Vector3(characterBody.forward.x, 0f, characterBody.forward.z).normalized;
            // 좌우이동. y = 0f이라 up/down 고정
            Vector3 lookRight = new Vector3(characterBody.right.x, 0, characterBody.right.z).normalized;
            // 이동 방향 결정*/
            // Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            
            // 이동 시 정면 바라보기
            // characterBody.forward = lookForward;
            // 이동
            // transform.Translate();
            //transform.position += moveDir * Time.deltaTime * 5f;
            
            // 주찬 수정 => transform 이동은 벽을 뚫는 버그가 많아서 rigidbody.velocity 이동으로 바꿨다.
            // 속도 값 받아옴 (mouse input을 통해)
             Vector3 playerVelocity = new Vector3(moveInput.x, 0f, moveInput.y);
             // 로컬 -> 월드 좌표로 바꿨고 => 회전 이후에도 영향이 가기 위함
             // 대각선 이동이 직선 이동과 같게 만들어주기 위해 nomalized하였다.
             Vector3 moveDir = transform.TransformDirection(playerVelocity).normalized;
             // 동기화를 위한 Time.deltaTime, 조정 값인 moveSpeed
             _rigidbody.velocity = moveDir * (Time.deltaTime * moveSpeed * boost);
             
             
        }
    }
    
    public void Rotate(Vector3 inputDirection)
    {
        /*//이동 값
        Vector2 moveDelta = inputDirection;
        // 카메라의 원래 각도를 오일러 각으로 저장
        Vector3 camAngle = moveCamera.rotation.eulerAngles;
        // 카메라의 회전 값 계산
        float x = camAngle.x - moveDelta.y;

        // 카메라 회전 값을 제한
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 20f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 390f);
        }
        
        // moveCamera 회전 시키기. Euler로 변환
        moveCamera.rotation = Quaternion.Euler(x, camAngle.y + moveDelta.x, camAngle.z);*/
        // transform.rotation = Quaternion.Euler(transform.rotation.x,  transform.rotation.y + inputDirection.x * rotateSpeed, transform.rotation.z);
        rotationX -= inputDirection.y;
        transform.Rotate(Vector3.up * inputDirection.x * rotateSpeed);
        float tempRotation = Mathf.Clamp(rotationX, -90f, 90f);
        moveCamera.localRotation = Quaternion.Euler(tempRotation * rotateSpeed, 0f, 0f);
        
    }

    //변수동기화 필요시 여기에서 제어
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        //PV transform 보다 부드럽게 위치동기화
        if(stream.IsWriting)
        {
            //현재 포지션을 넘겨줌
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }

    public void SayHiButton()
    {
        animator.SetTrigger("Wave");
    }

    public void JumpButton()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            return;
        }
        animator.SetBool("Jump", true);
    }

}
