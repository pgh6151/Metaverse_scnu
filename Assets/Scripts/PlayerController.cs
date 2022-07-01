using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields
    float gravity = 9.81f;
    // 현재 속도
    Vector3 vel;
    // 수직 속도
    Vector3 vSpeed;
    float xRotation;
    CharacterController characterController;
    float headRotationOffset;
    #endregion
    #region Public Fields
    [SerializeField]
    float jumpSpeed = 5f;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float mouseSensivity = 100f;
    [SerializeField]
    Transform playerHeadTrans;
    [SerializeField]
    GameObject topViewCam;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() 
    {
        headRotationOffset = playerHeadTrans.rotation.y;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 양 옆 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        vel = transform.right * h + transform.forward * v;
        characterController.Move(vel.normalized * speed * Time.deltaTime);
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        
        RotatePlayer(mouseX, mouseY);

        // 캐릭터가 땅에 닿았을 때
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
        {
            gravity = 0f;
            if (Input.GetKeyDown("space"))
            {
                vSpeed.y += jumpSpeed;        
            }
        }
        // 캐릭터가 공중에 떠 있을 때
        else
        {  
            gravity = 9.81f;
            vSpeed.y -= gravity * Time.deltaTime;
        }

        // 수직 이동 (점프)
        characterController.Move(vSpeed * Time.deltaTime);

    #endregion
    }

    #region Public Methods
    public void RotatePlayer(float X, float Y)
    {
        if (topViewCam.activeSelf) return;

        transform.Rotate(Vector3.up * X);

        xRotation -= Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerHeadTrans.localRotation = Quaternion.Euler(0f, xRotation, 0);
    }
    #endregion
}
